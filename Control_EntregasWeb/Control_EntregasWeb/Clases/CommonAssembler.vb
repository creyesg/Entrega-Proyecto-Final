Imports System.Collections.Generic
Imports System.Data
Imports System.Web.Configuration.ExpressionBuilder
Imports System.Linq
Imports System.Text
Public Class CommonAssembler
    Public Shared Function GetCatalogs(dt As DataTable) As List(Of Catalog.Catalog)
        Dim catalogList = New List(Of Catalog.Catalog)

        For Each dr As DataRow In dt.Rows
            Dim rowCatalog = New Catalog.Catalog()
            rowCatalog.Id = dr(0).ToString()
            rowCatalog.Description = dr(1).ToString()
            rowCatalog.Data = dr.Table.Columns.Cast(Of DataColumn)().ToDictionary(Function(col) col.ColumnName, Function(col) dr(col))
            catalogList.Add(rowCatalog)
        Next
        Return catalogList.ToList()
    End Function
    Public Shared Function GetCatalogDataSet(ds As DataSet) As List(Of Catalog.Catalog)
        Dim catalogList = New List(Of Catalog.Catalog)
        For Each dr As DataRow In ds.Tables(0).Rows
            Dim rowCatalog = New Catalog.Catalog()
            rowCatalog.Data = dr.Table.Columns.Cast(Of DataColumn)().ToDictionary(Function(col) col.ColumnName, Function(col) dr(col))
            catalogList.Add(rowCatalog)
        Next
        Return catalogList.ToList()
    End Function
    Public Shared Function SetErrorToCatalog(ByVal pError As String) As List(Of Catalog.Catalog)
        Dim catalogList = New List(Of Catalog.Catalog)
        Dim rowCatalog = New Catalog.Catalog()
        rowCatalog.ErrorPrm = pError
        catalogList.Add(rowCatalog)
        Return catalogList.ToList()
    End Function


End Class
