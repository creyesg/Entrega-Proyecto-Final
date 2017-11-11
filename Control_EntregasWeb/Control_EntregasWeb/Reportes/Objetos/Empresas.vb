Public Class Empresas
    Private _idEmpresa As String
    Public Property idEmpresa() As String
        Get
            Return _idEmpresa
        End Get
        Set(value As String)
            _idEmpresa = value
        End Set
    End Property

    Private _NombreEmpresa As String
    Public Property NombreEmpresa() As String
        Get
            Return _NombreEmpresa
        End Get
        Set(value As String)
            _NombreEmpresa = value
        End Set
    End Property

    Private _Tarifa As String
    Public Property Tarifa() As String
        Get
            Return _Tarifa
        End Get
        Set(value As String)
            _Tarifa = value
        End Set
    End Property

    Private _TrackAlias As String
    Public Property TrackAlias() As String
        Get
            Return _TrackAlias
        End Get
        Set(value As String)
            _TrackAlias = value
        End Set
    End Property


End Class
