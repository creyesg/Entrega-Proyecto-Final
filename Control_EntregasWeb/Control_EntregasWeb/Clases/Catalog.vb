Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Catalog
    ''' <summary>
    ''' Representa un item de un catalogo
    ''' </summary>
    Public Class Catalog
        ''' <summary>
        ''' Identificador del item
        ''' Se coloca en string para contemplar aquellas llaves compuestas o que son texto como cod_usuario
        ''' </summary>
        Public Property Id() As String
            Get
                Return m_Id
            End Get
            Set(value As String)
                m_Id = value
            End Set
        End Property
        Private m_Id As String

        ''' <summary>
        ''' Descripción a mostrar del catalogo
        ''' </summary>
        Public Property Description() As String
            Get
                Return m_Description
            End Get
            Set(value As String)
                m_Description = value
            End Set
        End Property
        Private m_Description As String

        Public Property ErrorPrm() As String
            Get
                Return _error
            End Get
            Set(value As String)
                _error = value
            End Set
        End Property
        Private _error As String
        ''' <summary>
        ''' Conjunto de datos complentarios para el catalogo
        ''' </summary>
        Public Property Data() As Dictionary(Of String, Object)
            Get
                Return m_Data
            End Get
            Set(value As Dictionary(Of String, Object))
                m_Data = value
            End Set
        End Property
        Private m_Data As Dictionary(Of String, Object)
    End Class
End Namespace