Public Class Envios
    Inherits Orders
    Private _ENVIO_ID As String
    Public Property ENVIO_ID() As String
        Get
            Return _ENVIO_ID
        End Get
        Set(value As String)
            _ENVIO_ID = value
        End Set
    End Property

    Private _ID_EMPRESA As String
    Public Property ID_EMPRESA() As String
        Get
            Return _ID_EMPRESA
        End Get
        Set(value As String)
            _ID_EMPRESA = value
        End Set
    End Property

    Private _SN_INTERNO As String
    Public Property SN_INTERNO() As String
        Get
            Return _SN_INTERNO
        End Get
        Set(value As String)
            _SN_INTERNO = value
        End Set
    End Property
    Private _ORDER_ID As String

    Public Property ORDER_ID() As String
        Get
            Return _ORDER_ID
        End Get
        Set(value As String)
            _ORDER_ID = value
        End Set
    End Property

    Private _COD_EMP_INTERNO As String
    Public Property COD_EMP_INTERNO() As String
        Get
            Return _COD_EMP_INTERNO
        End Get
        Set(value As String)
            _COD_EMP_INTERNO = value
        End Set
    End Property
End Class
