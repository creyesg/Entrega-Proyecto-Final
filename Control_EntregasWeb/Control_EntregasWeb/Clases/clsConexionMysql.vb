Imports Microsoft.VisualBasic
Imports System
Imports MySql.Data.MySqlClient
Public Class clsConexionMysql
    Dim vnConn As New MySqlConnection
    Dim vcCmm As New MySqlCommand
    Dim vrRdr As MySqlDataReader

    Public Function getConexion() As String
        Dim conect = ConfigurationManager.AppSettings("cnfConexion")
        Return conect
    End Function

    Public Function fAbreConexion() As Boolean
        'Abre conexión con base de datos
        Try
            If vnConn.State = Data.ConnectionState.Open Then
                vnConn.Close()
            End If

            vnConn.ConnectionString = ConfigurationManager.AppSettings("cnfConexionMysql")

            vnConn.Open()
            fAbreConexion = True
        Catch NoConecta As Exception
            fAbreConexion = False
        End Try
    End Function
    Public Function fCierraConexion() As Boolean
        'Cierra conexión con base de datos
        Try
            If vnConn.State = Data.ConnectionState.Open Then
                vnConn.Close()
            End If
            fCierraConexion = True
        Catch NoCierra As Exception
            fCierraConexion = False
        End Try
    End Function
    Public Function fEjecutaQueryDS(ByVal psQuery As String) As Data.DataSet
        'Ejecuta un query y retorna el data set
        Dim dsDatos As New Data.DataSet
        Dim vaAdp As MySqlDataAdapter
        fCierraConexion()
        If fAbreConexion() Then
            vcCmm.Connection = vnConn
            vcCmm.CommandType = Data.CommandType.Text
            vcCmm.CommandText = psQuery
            vcCmm.CommandTimeout = 3600
            vaAdp = New MySqlDataAdapter(vcCmm)
            vaAdp.Fill(dsDatos, "Datos")
            fCierraConexion()
            Return dsDatos
        End If
        Return Nothing
    End Function
    Public Function fAccesoOpcion(ByVal psUsuario As String, ByVal piOpcion As Integer) As Boolean
        'Función que verifica si el usuario tiene acceso a una determinada opción
        Dim vdDatos As Data.DataSet
        Dim vbAcceso As Boolean
        Try
            vbAcceso = True

            'Verificación acceso a página
            vdDatos = fEjecutaQueryDS("pr_sg_seguridad 5, '" & psUsuario & "', null, null, " & piOpcion)

            If Not vdDatos Is Nothing Then
                If vdDatos.Tables(0).Rows(0).Item(0) = "0" Then
                    vbAcceso = False
                End If
            End If

            Return vbAcceso
        Catch noEntra As Exception
            Return False
        End Try
    End Function

    Public Function fEjecutaQueryRD(ByVal psQuery As String) As MySqlDataReader
        'Ejecuta query y retorna el reader
        If fAbreConexion() Then
            vcCmm.Connection = vnConn
            vcCmm.CommandType = Data.CommandType.Text
            vcCmm.CommandText = psQuery
            vcCmm.CommandTimeout = 3600
            vrRdr = vcCmm.ExecuteReader()
            Return vrRdr
            fCierraConexion()
        End If
        Return Nothing
    End Function

    'Gcombariza ejecuta un Query utilizando parametros
    Public Function fEjecutaQueryRDParam(ByVal psQuery As String, parameters As String) As MySqlDataReader
        'Ejecuta query y retorna el reader
        If fAbreConexion() Then
            vcCmm.Connection = vnConn
            vcCmm.CommandType = Data.CommandType.Text
            vcCmm.CommandText = psQuery
            vcCmm.CommandTimeout = 3600
            Dim parametersArray = parameters.Split("|")
            Dim abcParam As Char = "a"
            Dim numberParam = 0
            If parametersArray(0).Length > 0 AndAlso parametersArray.Length = 1 Then
                vcCmm.Parameters.Add(New MySqlParameter("@p" + (abcParam).ToString(), MySqlDbType.VarChar)).Value = parametersArray(0)
            ElseIf parametersArray(0).Length > 0 AndAlso parametersArray.Length > 1 Then
                For Each param In parametersArray
                    vcCmm.Parameters.Add(New MySqlParameter("@p" + (abcParam).ToString(), MySqlDbType.VarChar)).Value = parametersArray(numberParam)
                    numberParam += 1
                    abcParam = getCharToNumber(numberParam)
                Next
            End If
            vrRdr = vcCmm.ExecuteReader()
            Return vrRdr
            fCierraConexion()
        End If
        Return Nothing
    End Function
    Private Function getCharToNumber(ByVal index As Integer) As String
        Const abcString As String = "abcdfghijklmnopqrstuvwxyz" 'letras del abecedario
        For i = 0 To abcString.Length - 1
            If i = index Then
                Return abcString(i)
            End If
        Next
        Return ""
    End Function
    Public Function fdEjecutaSPDatos(ByVal psSP As String, ByVal psParametros() As String) As DataSet
        Dim vdsDatos As New Data.DataSet
        Dim ocmdComando As New MySqlCommand
        Dim vadAdapter As New MySqlDataAdapter
        Dim vsValorPar() As String

        If fAbreConexion() Then
            With ocmdComando
                .Connection = vnConn
                .CommandType = CommandType.StoredProcedure
                .CommandText = psSP

                For piPar As Integer = 0 To psParametros.Length - 1 'en sql no utiliza refcursors
                    'Formato Valores: {Nombre Parámetro}|{Tipo: 1 Numero, 2 Varchar}|{Valor}
                    vsValorPar = Split(psParametros(piPar), "|")

                    'Tipo de dato
                    If vsValorPar(1) = 1 Then
                        .Parameters.Add(vsValorPar(0), MySqlDbType.Decimal)
                    ElseIf vsValorPar(1) = 2 Then
                        '.Parameters.Add(vsValorPar(0), OracleClient.OracleType.VarChar, 4000)
                        .Parameters.Add(vsValorPar(0), MySqlDbType.VarChar, 10000)
                    ElseIf vsValorPar(1) = 3 Then
                        .Parameters.Add(vsValorPar(0), MySqlDbType.Date)
                    End If
                    .Parameters(vsValorPar(0)).Direction = ParameterDirection.Input
                    'Valor de parámetro
                    If vsValorPar(2).Length > 0 Then
                        If vsValorPar(1) = 3 Then   'Fecha
                            .Parameters(vsValorPar(0)).Value = CDate(vsValorPar(2))
                        Else
                            .Parameters(vsValorPar(0)).Value = vsValorPar(2)
                        End If
                    Else
                        .Parameters(vsValorPar(0)).Value = DBNull.Value
                    End If
                Next

            End With

            vadAdapter = New MySqlDataAdapter(ocmdComando)

            vadAdapter.Fill(vdsDatos)

            fCierraConexion()
        End If

        Return vdsDatos
    End Function




End Class
