using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using DevExpress.Data.Filtering;

public partial class Grid_Filter_CustomFilterCriteria : System.Web.UI.Page {
    const int ShowAllFilterId = -999;
    const int IntStep = 10;
    const int SymbolStep = 4;
    const int DecimalStep = 50;
    protected void Page_Load(object sender, EventArgs e) {

    }
    
    protected void grid_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e) {
        if(!IsCustomColumnFiltering(e.Column)) return;
        if(e.Kind != GridViewAutoFilterEventKind.CreateCriteria) return;
        int selectedIndex;
        if(!int.TryParse(e.Value, out selectedIndex)) return;
        if(selectedIndex == ShowAllFilterId) {
            Session[GetSessionFilterName(e.Column)] = null;
            e.Criteria = null;
        } else {
            Session[GetSessionFilterName(e.Column)] = selectedIndex;
            if(Equals(e.Column, grid.Columns["Quantity"])) {
                e.Criteria = new GroupOperator(GroupOperatorType.And,
                    new BinaryOperator("Quantity", selectedIndex * IntStep, BinaryOperatorType.GreaterOrEqual),
                    new BinaryOperator("Quantity", (selectedIndex + 1) * IntStep, BinaryOperatorType.Less));
            }
            if(Equals(e.Column, grid.Columns["UnitPrice"])) {
                e.Criteria = new BinaryOperator("UnitPrice", (selectedIndex + 1) * DecimalStep, BinaryOperatorType.Less);
            }
            if(Equals(e.Column, grid.Columns["CompanyName"])) {
                char[] values = GetSymbolValue(selectedIndex);
                e.Criteria = new GroupOperator(GroupOperatorType.And,
                    new BinaryOperator("CompanyName", values[0], BinaryOperatorType.GreaterOrEqual),
                    new BinaryOperator("CompanyName", values[1] + "zzz", BinaryOperatorType.Less));
            }
        }
    }
    protected void grid_AutoFilterCellEditorCreate(object sender, ASPxGridViewEditorCreateEventArgs e) {
        if(!IsCustomColumnFiltering(e.Column)) return;
        ComboBoxProperties combo = new ComboBoxProperties();
        combo.Items.Add("Show All", ShowAllFilterId); 
        if(Equals(e.Column, grid.Columns["Quantity"])) {
            for(int i = 0; i < 130 / IntStep; i++) {
                combo.Items.Add(string.Format("From {0} to {1}", i * IntStep, (i + 1) * IntStep - 1), i);
            }
        }
        if(Equals(e.Column, grid.Columns["UnitPrice"])) {
            for(int i = 0; i < 300 / DecimalStep; i++) {
                combo.Items.Add(string.Format("Less than {0:c}", (i + 1) * DecimalStep), i);
            }
        }
        if(Equals(e.Column, grid.Columns["CompanyName"])) {
            for(int i = 0; i <= 26 / SymbolStep; i ++) {
                char[] values = GetSymbolValue(i);
                combo.Items.Add(string.Format("From {0} to {1}", values[0], values[1]), i);
            }
        }
        e.EditorProperties = combo;
    }
    protected void grid_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e) {
        if(!IsCustomColumnFiltering(e.Column)) return; 
        if(e.Editor is ASPxComboBox) {
            if(Session[GetSessionFilterName(e.Column)] != null) {
                ((ASPxComboBox)e.Editor).SelectedIndex = (int)Session[GetSessionFilterName(e.Column)] + 1; // + 1 for "Show All"
            } else {
                ((ASPxComboBox)e.Editor).SelectedIndex = 0; //Show All, set the SelectedIndex to -1 to show the empty string for "Show all"
            }
        }
    }
    string GetSessionFilterName(GridViewDataColumn column) {
        return column != null ? column.FieldName + "FilterSelIndex" : string.Empty;
    }
    char[] GetSymbolValue(int index) {
        char[] res = new char[2];
        int value = (int)'A' + index * SymbolStep;
        res[0] =  (char)value;
        res[1] = Convert.ToChar(Math.Min((int)'Z', value + SymbolStep - 1));
        return res;
    }
    bool IsCustomColumnFiltering(GridViewDataColumn column) {
        return column != null && (Equals(column, grid.Columns["CompanyName"]) || Equals(column, grid.Columns["UnitPrice"]) || Equals(column, grid.Columns["Quantity"]));
    }
}
