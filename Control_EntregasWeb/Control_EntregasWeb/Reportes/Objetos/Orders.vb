Public Class Orders
    Inherits Empresas
    Private _idOrder As String
    Public Property idOrder() As String
        Get
            Return _idOrder
        End Get
        Set(value As String)
            _idOrder = value
        End Set
    End Property


    Private _NombreCliente As String
    Public Property NombreCliente() As String
        Get
            Return _NombreCliente
        End Get
        Set(value As String)
            _NombreCliente = value
        End Set
    End Property


    Private _idCliente As String
    Public Property idCliente() As String
        Get
            Return _idCliente
        End Get
        Set(value As String)
            _idCliente = value
        End Set
    End Property

    Private _Direction As String
    Public Property Direction() As String
        Get
            Return _Direction
        End Get
        Set(value As String)
            _Direction = value
        End Set
    End Property


    Private _fecSend As String
    Public Property fecSend() As String
        Get
            Return _fecSend
        End Get
        Set(value As String)
            _fecSend = value
        End Set
    End Property


    Private _fecOrder As String
    Public Property fecOrder() As String
        Get
            Return _fecOrder
        End Get
        Set(value As String)
            _fecOrder = value
        End Set
    End Property

    Private _fecEntrega As String
    Public Property fecEntrega() As String
        Get
            Return _fecEntrega
        End Get
        Set(value As String)
            _fecEntrega = value
        End Set
    End Property

    Private _NIT As String
    Public Property NIT() As String
        Get
            Return _NIT
        End Get
        Set(value As String)
            _NIT = value
        End Set
    End Property
    Private _PESO_TOTAL As String
    Public Property PESO_TOTAL() As String
        Get
            Return _PESO_TOTAL
        End Get
        Set(value As String)
            _PESO_TOTAL = value
        End Set
    End Property

    Private _TAMANIO_T As String
    Public Property TAMANIO_T() As String
        Get
            Return _TAMANIO_T
        End Get
        Set(value As String)
            _TAMANIO_T = value
        End Set
    End Property


    Private _estado As String
    Public Property estado() As String
        Get
            Return _estado
        End Get
        Set(value As String)
            _estado = value
        End Set
    End Property

End Class
