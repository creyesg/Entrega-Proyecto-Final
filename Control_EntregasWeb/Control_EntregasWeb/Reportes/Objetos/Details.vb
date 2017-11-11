Public Class Details
    Inherits Orders


    'select ord.order_id, ord.nombre_factura, ord.nit, ord.direccion, ord.fec_order, ord.peso_total, 
    'ord.tamanio_total, det.det_id, det.cod_producto, det.peso, det.tamaño from ORDERS ord
    'left join orders_det det on det.ORDER_ID = ord.ORDER_ID

    Private _det_id As String
    Public Property det_id() As String
        Get
            Return _det_id
        End Get
        Set(value As String)
            _det_id = value
        End Set
    End Property

    Private _cod_producto As String
    Public Property cod_producto() As String
        Get
            Return _cod_producto
        End Get
        Set(value As String)
            _cod_producto = value
        End Set
    End Property

    Private _peso As String
    Public Property peso() As String
        Get
            Return _peso
        End Get
        Set(value As String)
            _peso = value
        End Set
    End Property

    Private _tamanio As String
    Public Property tamanio() As String
        Get
            Return _tamanio
        End Get
        Set(value As String)
            _tamanio = value
        End Set
    End Property

End Class
