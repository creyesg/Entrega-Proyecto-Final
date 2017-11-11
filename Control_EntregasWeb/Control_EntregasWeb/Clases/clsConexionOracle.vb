Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Xml
Imports Oracle.DataAccess.Client


Public Class clsConexionOracle
    Dim vnConn As New Oracle.DataAccess.Client.OracleConnection
    Dim vcCmm As New Oracle.DataAccess.Client.OracleCommand
    Dim vrRdr As Oracle.DataAccess.Client.OracleDataReader

    Public Shared vdbasededatos As Integer = 0

    Private Function fAbreConexion() As Boolean

        'Abre conexión con base de datos
        If vnConn.State = Data.ConnectionState.Open Then
            vnConn.Close()
        End If
        vnConn.ConnectionString = ConfigurationManager.AppSettings("cnfConexionOracle")
        vnConn.Open()

        fAbreConexion = True
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
        Dim vaAdp As Oracle.DataAccess.Client.OracleDataAdapter
        fCierraConexion()
        If fAbreConexion() Then
            vcCmm.Connection = vnConn
            vcCmm.CommandType = Data.CommandType.Text
            vcCmm.CommandText = psQuery
            vcCmm.CommandTimeout = 3600
            vaAdp = New Oracle.DataAccess.Client.OracleDataAdapter(vcCmm)
            vaAdp.Fill(dsDatos, "Datos")
            fCierraConexion()
            Return dsDatos
        End If
        Return Nothing
    End Function
    Public Function fEjecutaQueryRD(ByVal psQuery As String) As Oracle.DataAccess.Client.OracleDataReader
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
    Public Function fEjecutaQueryRDParam(ByVal psQuery As String, parameters As String) As OracleDataReader
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
                vcCmm.Parameters.Add(New OracleParameter("p" + (abcParam).ToString(), OracleDbType.Varchar2)).Value = parametersArray(0)
            ElseIf parametersArray(0).Length > 0 AndAlso parametersArray.Length > 1 Then
                For Each param In parametersArray
                    vcCmm.Parameters.Add(New OracleParameter("p" + (abcParam).ToString(), OracleDbType.Varchar2)).Value = parametersArray(numberParam)
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
        Dim ocmdComando As New Oracle.DataAccess.Client.OracleCommand
        Dim vadAdapter As New Oracle.DataAccess.Client.OracleDataAdapter
        Dim vsValorPar() As String

        If fAbreConexion() Then
            With ocmdComando
                .Connection = vnConn
                .CommandType = CommandType.StoredProcedure
                .CommandText = psSP

                'Toma desde 0 hasta menos 2 porque el último es el cursor
                For piPar As Integer = 0 To psParametros.Length - 2
                    'Formato Valores: {Nombre Parámetro}|{Tipo: 1 Numero, 2 Varchar}|{Valor}
                    vsValorPar = Split(psParametros(piPar), "|")

                    'Tipo de dato
                    If vsValorPar(1) = 1 Then
                        .Parameters.Add(vsValorPar(0), Oracle.DataAccess.Client.OracleDbType.Decimal)
                    ElseIf vsValorPar(1) = 2 Then
                        '.Parameters.Add(vsValorPar(0), OracleClient.OracleType.VarChar, 4000)
                        .Parameters.Add(vsValorPar(0), Oracle.DataAccess.Client.OracleDbType.Varchar2, 10000)
                    ElseIf vsValorPar(1) = 3 Then
                        .Parameters.Add(vsValorPar(0), Oracle.DataAccess.Client.OracleDbType.Date)
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

                'Agregando el tipo de parámetro cursor
                vsValorPar = Split(psParametros(psParametros.Length - 1), "|")
                .Parameters.Add(vsValorPar(0), Oracle.DataAccess.Client.OracleDbType.RefCursor)
                .Parameters(vsValorPar(0)).Direction = ParameterDirection.Output
            End With

            vadAdapter = New Oracle.DataAccess.Client.OracleDataAdapter(ocmdComando)

            vadAdapter.Fill(vdsDatos)

            fCierraConexion()
        End If

        Return vdsDatos
    End Function


End Class
