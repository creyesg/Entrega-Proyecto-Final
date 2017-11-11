Public Class Empleados

    Private _COD_EMPLEADO As String
    Public Property COD_EMPLEADO() As String
        Get
            Return _COD_EMPLEADO
        End Get
        Set(value As String)
            _COD_EMPLEADO = value
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
