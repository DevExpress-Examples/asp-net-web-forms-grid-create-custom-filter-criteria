<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128537118/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E353)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Grid View for ASP.NET Web Forms - How to apply custom filter criteria
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/128537118/)**
<!-- run online end -->
This example demonstrates how to allow users to set custom filter criteria in the Grid View control's filter row.

![Apply Custom Filter Criteria](result.png)

Handle the [AutoFilterCellEditorCreate](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxGridView.AutoFilterCellEditorCreate?p=netframework) event to display custom editors in filter row cells. The [AutoFilterCellEditorInitialize](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxGridView.AutoFilterCellEditorInitialize?p=netframework) event occurs before the filter row appears and allows you to initialize editors in filter row cells. The [ProcessColumnAutoFilter](https://docs.devexpress.com/AspNet/DevExpress.Web.ASPxGridView.ProcessColumnAutoFilter?p=netframework) event allows you to apply custom filter criteria when a user changes an editor value in the filter row.

## Files to Review

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))

## Documentation

- [Filter Row](https://docs.devexpress.com/AspNet/3753/components/grid-view/concepts/filter-data/filter-row)

## More Examples

- [How to implement a filter row template and use ASPxGridLookup as an editor](https://github.com/DevExpress-Examples/asp-net-web-forms-grid-implement-filter-row-template)
