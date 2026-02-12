Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web
Imports DevExpress.Data.Filtering

Partial Public Class Grid_Filter_CustomFilterCriteria
	Inherits System.Web.UI.Page

	Private Const ShowAllFilterId As Integer = -999
	Private Const IntStep As Integer = 10
	Private Const SymbolStep As Integer = 4
	Private Const DecimalStep As Integer = 50
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

	End Sub

	Protected Sub grid_ProcessColumnAutoFilter(ByVal sender As Object, ByVal e As ASPxGridViewAutoFilterEventArgs)
		If Not IsCustomColumnFiltering(e.Column) Then
			Return
		End If
		If e.Kind <> GridViewAutoFilterEventKind.CreateCriteria Then
			Return
		End If
		Dim selectedIndex As Integer = Nothing
		If Not Integer.TryParse(e.Value, selectedIndex) Then
			Return
		End If
		If selectedIndex = ShowAllFilterId Then
			Session(GetSessionFilterName(e.Column)) = Nothing
			e.Criteria = Nothing
		Else
			Session(GetSessionFilterName(e.Column)) = selectedIndex
			If Equals(e.Column, grid.Columns("Quantity")) Then
				e.Criteria = New GroupOperator(GroupOperatorType.And, New BinaryOperator("Quantity", selectedIndex * IntStep, BinaryOperatorType.GreaterOrEqual), New BinaryOperator("Quantity", (selectedIndex + 1) * IntStep, BinaryOperatorType.Less))
			End If
			If Equals(e.Column, grid.Columns("UnitPrice")) Then
				e.Criteria = New BinaryOperator("UnitPrice", (selectedIndex + 1) * DecimalStep, BinaryOperatorType.Less)
			End If
			If Equals(e.Column, grid.Columns("CompanyName")) Then
				Dim values() As Char = GetSymbolValue(selectedIndex)
				e.Criteria = New GroupOperator(GroupOperatorType.And, New BinaryOperator("CompanyName", values(0), BinaryOperatorType.GreaterOrEqual), New BinaryOperator("CompanyName", values(1) & "zzz", BinaryOperatorType.Less))
			End If
		End If
	End Sub
	Protected Sub grid_AutoFilterCellEditorCreate(ByVal sender As Object, ByVal e As ASPxGridViewEditorCreateEventArgs)
		If Not IsCustomColumnFiltering(e.Column) Then
			Return
		End If
		Dim combo As New ComboBoxProperties()
		combo.Items.Add("Show All", ShowAllFilterId)
		If Equals(e.Column, grid.Columns("Quantity")) Then
			Dim i As Integer = 0
			Do While i < 130 \ IntStep
				combo.Items.Add(String.Format("From {0} to {1}", i * IntStep, (i + 1) * IntStep - 1), i)
				i += 1
			Loop
		End If
		If Equals(e.Column, grid.Columns("UnitPrice")) Then
			Dim i As Integer = 0
			Do While i < 300 \ DecimalStep
				combo.Items.Add(String.Format("Less than {0:c}", (i + 1) * DecimalStep), i)
				i += 1
			Loop
		End If
		If Equals(e.Column, grid.Columns("CompanyName")) Then
			Dim i As Integer = 0
			Do While i <= 26 \ SymbolStep
				Dim values() As Char = GetSymbolValue(i)
				combo.Items.Add(String.Format("From {0} to {1}", values(0), values(1)), i)
				i += 1
			Loop
		End If
		e.EditorProperties = combo
	End Sub
	Protected Sub grid_AutoFilterCellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
		If Not IsCustomColumnFiltering(e.Column) Then
			Return
		End If
		If TypeOf e.Editor Is ASPxComboBox Then
			If Session(GetSessionFilterName(e.Column)) IsNot Nothing Then
				CType(e.Editor, ASPxComboBox).SelectedIndex = DirectCast(Session(GetSessionFilterName(e.Column)), Integer) + 1 ' + 1 for "Show All"
			Else
				CType(e.Editor, ASPxComboBox).SelectedIndex = 0 'Show All, set the SelectedIndex to -1 to show the empty string for "Show all"
			End If
		End If
	End Sub
	Private Function GetSessionFilterName(ByVal column As GridViewDataColumn) As String
		Return If(column IsNot Nothing, column.FieldName & "FilterSelIndex", String.Empty)
	End Function
	Private Function GetSymbolValue(ByVal index As Integer) As Char()
		Dim res(1) As Char
		Dim value As Integer = CInt(Math.Truncate(AscW("A"c))) + index * SymbolStep
		res(0) = ChrW(value)
		res(1) = Convert.ToChar(Math.Min(AscW("Z"c), value + SymbolStep - 1))
		Return res
	End Function
	Private Function IsCustomColumnFiltering(ByVal column As GridViewDataColumn) As Boolean
		Return column IsNot Nothing AndAlso (Equals(column, grid.Columns("CompanyName")) OrElse Equals(column, grid.Columns("UnitPrice")) OrElse Equals(column, grid.Columns("Quantity")))
	End Function
End Class
