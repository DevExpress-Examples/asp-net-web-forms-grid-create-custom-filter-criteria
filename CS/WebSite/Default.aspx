<%-- BeginRegion --%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Grid_Filter_CustomFilterCriteria" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v14.1" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%-- EndRegion --%>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Create the Customer Filter Criteria</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="adInvoices" Width="100%" AutoGenerateColumns="False" OnProcessColumnAutoFilter="grid_ProcessColumnAutoFilter" OnAutoFilterCellEditorCreate="grid_AutoFilterCellEditorCreate" OnAutoFilterCellEditorInitialize="grid_AutoFilterCellEditorInitialize">
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn FieldName="Country"  VisibleIndex="0">
                <Settings AllowAutoFilter="False" />
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataColumn FieldName="CompanyName" VisibleIndex="1">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Quantity" VisibleIndex="2">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="3">
                <PropertiesTextEdit DisplayFormatString="c">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <Settings ShowGroupPanel="True" ShowFilterRow="true" />
    </dxwgv:ASPxGridView>
    <%-- BeginRegion DataSource --%>
    <asp:AccessDataSource ID="adInvoices" runat="server" DataFile="~/App_Data/nwind.mdb"
        SelectCommand="SELECT [Customers.CompanyName] AS CompanyName, [CustomerID], [City], [Region], [Country], [Salesperson], [OrderID], [OrderDate], [ProductID], [ProductName], [UnitPrice], [Quantity], [Discount], [ExtendedPrice], [Freight] FROM [Invoices]">
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="adCountries" runat="server" DataFile="~/App_Data/nwind.mdb"
        SelectCommand="SELECT distinct [Country] FROM [Invoices]">
    </asp:AccessDataSource>
    <asp:AccessDataSource ID="adCities" runat="server" DataFile="~/App_Data/nwind.mdb"
        SelectCommand="SELECT distinct [City] FROM [Invoices] Where [Country]=?">
        <SelectParameters>
            <asp:SessionParameter SessionField="CountryFilter" Name="Country" DefaultValue="" />
        </SelectParameters>
    </asp:AccessDataSource>
    <%-- EndRegion --%>
    </form>
</body>
</html>
