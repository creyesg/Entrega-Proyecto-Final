Public Class Tracking


    '    trk.ORDER_ID, trk.FEC_TRACK, trk.EMISOR_ALIAS AS emisor, trk.RESEPTOR_ALIAS AS receptor, 
    'trk.TXT_OBSERVACIONES, trk.ID_STATUS, trk.coordenadas

    Private _idOrder As String
    Public Property idOrder() As String
        Get
            Return _idOrder
        End Get
        Set(value As String)
            _idOrder = value
        End Set
    End Property

    Private _fecTrack As String
    Public Property fecTrack() As String
        Get
            Return _fecTrack
        End Get
        Set(value As String)
            _fecTrack = value
        End Set
    End Property

    Private _EMISOR_ALIAS As String
    Public Property EMISOR_ALIAS() As String
        Get
            Return _EMISOR_ALIAS
        End Get
        Set(value As String)
            _EMISOR_ALIAS = value
        End Set
    End Property


    Private _RESEPTOR_ALIAS As String
    Public Property RESEPTOR_ALIAS() As String
        Get
            Return _RESEPTOR_ALIAS
        End Get
        Set(value As String)
            _RESEPTOR_ALIAS = value
        End Set
    End Property


    Private _OBSERVACIONES As String
    Public Property OBSERVACIONES() As String
        Get
            Return _OBSERVACIONES
        End Get
        Set(value As String)
            _OBSERVACIONES = value
        End Set
    End Property


    Private _ID_STATUS As String
    Public Property ID_STATUS() As String
        Get
            Return _ID_STATUS
        End Get
        Set(value As String)
            _ID_STATUS = value
        End Set
    End Property


    Private _coordenadas As String
    Public Property coordenadas() As String
        Get
            Return _coordenadas
        End Get
        Set(value As String)
            _coordenadas = value
        End Set
    End Property


End Class
