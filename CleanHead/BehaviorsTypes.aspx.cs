using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class BehaviorsTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) {
        if (Convert.ToInt32(Session["lvl_id"]) < 4) {
            Response.Redirect("~/Errors/403Error.aspx");
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        gvBehaviorsTypes.ShowFooter = true;
        this.btnInsert.Enabled = false;

        //Bind data to GridView
        DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
        GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
    }
    protected void gvBehaviorsTypes_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            //Bind data to GridView
            DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
            GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
        }
    }
    protected void gvBehaviorsTypes_PageIndexChanging(object sender, GridViewPageEventArgs e) {
        //Change page index
        gvBehaviorsTypes.PageIndex = e.NewPageIndex;

        //Bind data to GridView
        DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
        GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
    }
    protected bool ValidateEditPage(GridViewRow gvr){
        TextBox txt_edit_bhv_name = (TextBox)gvr.FindControl("txt_edit_bhv_name");
        TextBox txt_edit_bhv_value = (TextBox)gvr.FindControl("txt_edit_bhv_value");

        if (txt_edit_bhv_name.Text.Trim() == ""){
            lblErr.Text = "הכנס את סוג ההתנהגות";
            return false;
        }
        if (!Regex.IsMatch(txt_edit_bhv_name.Text.Trim(), @"^[א-תa-zA-Z''-'\s]{2,35}$")) {
            lblErr.Text = "הכנס אותיות בין 2 ל 35 תווים בסוג ההתנהגות";
            return false;
        }

        if (txt_edit_bhv_value.Text == "") {
            lblErr.Text = "הכנס את ערך ההתנהגות";
            return false;
        }
        if (!Regex.IsMatch(txt_edit_bhv_value.Text.Trim(), @"^[0-9]{1,2}$")) {
            lblErr.Text = "הכנס מספרים בלבד בין 0 ל-99 בערך ההתנהגות";
            return false;
        }
        if (Convert.ToInt32(txt_edit_bhv_value.Text.Trim()) < 0 || Convert.ToInt32(txt_edit_bhv_value.Text.Trim()) > 99) {
            lblErr.Text = "הכנס מספרים בלבד בין 0 ל-99 בערך ההתנהגות";
            return false;
        }

        lblErr.Text = "";
        return true;
    }
    protected void btn_update_bhv_Click(object sender, ImageClickEventArgs e) {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int bhv_id = Convert.ToInt32(gvBehaviorsTypes.DataKeys[gvr.RowIndex].Value.ToString());
        TextBox txt_edit_bhv_name = (TextBox)gvr.FindControl("txt_edit_bhv_name");
        TextBox txt_edit_bhv_value = (TextBox)gvr.FindControl("txt_edit_bhv_value");

        if (!ValidateEditPage(gvr)) { return; }

        //all vars to one object
        ch_behaviors bhv1 = new ch_behaviors();
        bhv1.bhv_name = txt_edit_bhv_name.Text.Trim();
        bhv1.bhv_value = Convert.ToInt32(txt_edit_bhv_value.Text.Trim());

        string err = ch_behaviorsSvc.UpdateBehaviorById(bhv_id, bhv1);
        if (err == "")//אם העדכון התבצע
        {
            lblErr.Text = string.Empty;
            gvBehaviorsTypes.EditIndex = -1;

            //Bind data to GridView
            DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
            GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
        }
        else {
            lblErr.Text = err;

            //Bind data to GridView
            DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
            GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
        }
    }
    protected void btn_cancel_update_bhv_Click(object sender, ImageClickEventArgs e) {
        gvBehaviorsTypes.EditIndex = -1;
        lblErr.Text = "";

        //Bind data to GridView
        DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
        GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
    }
    protected void btn_edit_bhv_Click(object sender, ImageClickEventArgs e) {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        gvBehaviorsTypes.EditIndex = gvr.RowIndex;

        //Bind data to GridView
        DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
        GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
    }
    protected bool ValidateInsertPage(GridViewRow gvr) {
        TextBox txt_insert_bhv_name = (TextBox)gvr.FindControl("txt_insert_bhv_name");
        TextBox txt_insert_bhv_value = (TextBox)gvr.FindControl("txt_insert_bhv_value");

        if (txt_insert_bhv_name.Text.Trim() == "") {
            lblErr.Text = "הכנס את סוג ההתנהגות";
            return false;
        }
        if (!Regex.IsMatch(txt_insert_bhv_name.Text.Trim(), @"^[א-תa-zA-Z''-'\s]{2,35}$")) {
            lblErr.Text = "הכנס אותיות בין 2 ל 35 תווים בסוג ההתנהגות";
            return false;
        }

        if (txt_insert_bhv_value.Text == "") {
            lblErr.Text = "הכנס את ערך ההתנהגות";
            return false;
        }
        if (!Regex.IsMatch(txt_insert_bhv_value.Text.Trim(), @"^[0-9]{1,2}$")) {
            lblErr.Text = "הכנס מספרים בלבד בין 0 ל-99 בערך ההתנהגות";
            return false;
        }
        if (Convert.ToInt32(txt_insert_bhv_value.Text.Trim()) < 0 || Convert.ToInt32(txt_insert_bhv_value.Text.Trim()) > 99) {
            lblErr.Text = "הכנס מספרים בלבד בין 0 ל-99 בערך ההתנהגות";
            return false;
        }

        lblErr.Text = "";
        return true;
    }
    protected void btn_insert_bhv_Click(object sender, ImageClickEventArgs e) {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        TextBox txt_insert_bhv_name = (TextBox)gvr.FindControl("txt_insert_bhv_name");
        TextBox txt_insert_bhv_value = (TextBox)gvr.FindControl("txt_insert_bhv_value");

        if (!ValidateInsertPage(gvr)) { return; }

        //all vars to one object
        ch_behaviors bhv1 = new ch_behaviors();
        bhv1.bhv_name = txt_insert_bhv_name.Text.Trim();
        bhv1.bhv_value = Convert.ToInt32(txt_insert_bhv_value.Text.Trim());

        string err = ch_behaviorsSvc.AddBehavior(bhv1);

        if (err == "")//אם ההכנסה התבצע
        {
            lblErr.Text = "";
            gvBehaviorsTypes.ShowFooter = false;
            btnInsert.Enabled = true;

            //Bind data to GridView
            DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
            GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
        }
        else {
            lblErr.Text = err;
            txt_insert_bhv_name.Text = "";

            //Bind data to GridView
            DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
            GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
        }
    }
    protected void btn_delete_bhv_Click(object sender, ImageClickEventArgs e) {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        int bhv_id = Convert.ToInt32(gvBehaviorsTypes.DataKeys[gvr.RowIndex].Value.ToString());
        ch_behaviorsSvc.DeleteBehaviorById(bhv_id);

        //Bind data to GridView
        DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
        GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
    }
    protected void btn_cancel_insert_bhv_Click(object sender, ImageClickEventArgs e) {
        gvBehaviorsTypes.ShowFooter = false;
        btnInsert.Enabled = true;
        lblErr.Text = "";

        //Bind data to GridView
        DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
        GridViewSvc.GVBind(dsBehaviors, gvBehaviorsTypes);
    }
}