using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class HoursData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void gvHours_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToInt32(Session["lvl_id"]) < 4) {
                //Bind data to GridView
                DataSet dsHours = ch_hoursSvc.GetHours(Convert.ToInt32(Session["sc_id"]));
                GridViewSvc.GVBind(dsHours, gvHours);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                gvHours.Visible = false;
                btnInsert.Visible = false;
            }
        }
    }
    protected void gvHours_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        gvHours.PageIndex = e.NewPageIndex;

        //Bind data to GridView
        int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);

        if (Convert.ToInt32(Session["lvl_id"]) < 4) {
                //Bind data to GridView
                DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
                GridViewSvc.GVBind(dsHours, gvHours);
            }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            if (ddlSchools.SelectedValue == "-1") {
                gvHours.Visible = false;
                btnInsert.Visible = false;
            }
            else {
                //Bind data to GridView
                DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
                GridViewSvc.GVBind(dsHours, gvHours);
                gvHours.Visible = true;
                btnInsert.Visible = true;
            }
        }
    }
    protected void btn_edit_hr_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        gvHours.EditIndex = gvr.RowIndex;
        //Bind data to GridView
        int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
        DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
        GridViewSvc.GVBind(dsHours, gvHours);
    }

    protected bool ValidateEditPage(GridViewRow gvr) {
        TextBox txt_edit_hr_name = (TextBox)gvr.FindControl("txt_edit_hr_name");
        TextBox txt_edit_hr_start_time = (TextBox)gvr.FindControl("txt_edit_hr_start_time");
        TextBox txt_edit_hr_end_time = (TextBox)gvr.FindControl("txt_edit_hr_end_time");

        if (txt_edit_hr_name.Text == "") {
            lblErrGV.Text = "הכנס את שם השעה";
            return false;
        }
        if (txt_edit_hr_name.Text.Length >= 20) {
            lblErrGV.Text = "שם השעה צריך להיות בעל מספר תווים קטן מ 20";
            return false;
        }

        if (txt_edit_hr_start_time.Text == "") {
            lblErrGV.Text = "הכנס את זמן תחילת השעה";
            return false;
        }

        DateTime dt;
        string[] formats = { "HH:mm", "H:mm" };
        if (!DateTime.TryParseExact(txt_edit_hr_start_time.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
            lblErrGV.Text = "זמן תחילת השעה לא תקין";
            return false;
        }

        if (txt_edit_hr_end_time.Text == "") {
            lblErrGV.Text = "הכנס את זמן סוף השעה";
            return false;
        }
        if (!DateTime.TryParseExact(txt_edit_hr_end_time.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
            lblErrGV.Text = "זמן סוף השעה לא תקין";
            return false;
        }

        lblErrGV.Text = "";
        return true;
    }
    protected void btn_update_hr_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int hr_id = Convert.ToInt32(gvHours.DataKeys[gvr.RowIndex].Value.ToString());
        TextBox txt_edit_hr_name = (TextBox)gvr.FindControl("txt_edit_hr_name");
        TextBox txt_edit_hr_start_time = (TextBox)gvr.FindControl("txt_edit_hr_start_time");
        TextBox txt_edit_hr_end_time = (TextBox)gvr.FindControl("txt_edit_hr_end_time");

        if (!ValidateEditPage(gvr)){ return; }
        //all vars to one object
        ch_hours hr1 = new ch_hours();
        hr1.hr_Name = txt_edit_hr_name.Text.Trim();
        hr1.hr_Start_Time = txt_edit_hr_start_time.Text.Trim();
        hr1.hr_End_Time = txt_edit_hr_end_time.Text.Trim();

        if (Convert.ToInt32(Session["lvl_id"]) < 4) {
            hr1.sc_Id = Convert.ToInt32(Session["sc_id"]);
        }
        else {
            hr1.sc_Id = Convert.ToInt32(ddlSchools.SelectedValue);
        }
            
            
        string err = ch_hoursSvc.UpdateHourById(hr_id, hr1);
        if (err == "")//אם העדכון התבצע
        {
            lblErrGV.Text = string.Empty;
            gvHours.EditIndex = -1;

            //Bind data to GridView
            int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
            DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
            GridViewSvc.GVBind(dsHours, gvHours);
        }
        else {
            lblErrGV.Text = err;

            //Bind data to GridView
            int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
            DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
            GridViewSvc.GVBind(dsHours, gvHours);
        }
    }
    protected void btn_cancel_update_hr_Click(object sender, ImageClickEventArgs e)
    {
        gvHours.EditIndex = -1;
        lblErrGV.Text = "";

        //Bind data to GridView
        int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
        DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
        GridViewSvc.GVBind(dsHours, gvHours);
    }
    protected void btn_cancel_insert_hr_Click(object sender, ImageClickEventArgs e)
    {
        gvHours.ShowFooter = false;
        btnInsert.Enabled = true;
        lblErrGV.Text = "";

        //Bind data to GridView
        int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
        DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
        GridViewSvc.GVBind(dsHours, gvHours);
    }
    protected bool ValidateInsertPage(GridViewRow gvr) {
        TextBox txt_insert_hr_name = (TextBox)gvr.FindControl("txt_insert_hr_name");
        TextBox txt_insert_hr_start_time = (TextBox)gvr.FindControl("txt_insert_hr_start_time");
        TextBox txt_insert_hr_end_time = (TextBox)gvr.FindControl("txt_insert_hr_end_time");
        if (txt_insert_hr_name.Text == "") {
            lblErrGV.Text = "הכנס את שם השעה";
            return false;
        }
        if (txt_insert_hr_name.Text.Length >= 20) {
            lblErrGV.Text = "שם השעה צריך להיות בעל מספר תווים קטן מ 20";
            return false;
        }

        if (txt_insert_hr_start_time.Text == "") {
            lblErrGV.Text = "הכנס את זמן תחילת השעה";
            return false;
        }

        DateTime dt;
        string[] formats = { "HH:mm", "H:mm" };
        if (!DateTime.TryParseExact(txt_insert_hr_start_time.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
            lblErrGV.Text = "זמן תחילת השעה לא תקין";
            return false;
        }

        if (txt_insert_hr_end_time.Text == "") {
            lblErrGV.Text = "הכנס את זמן סוף השעה";
            return false;
        }
        if (!DateTime.TryParseExact(txt_insert_hr_start_time.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) {
            lblErrGV.Text = "זמן סוף השעה לא תקין";
            return false;
        }

        lblErrGV.Text = "";
        return true;
    }
    protected void btn_insert_hr_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        TextBox txt_insert_hr_name = (TextBox)gvr.FindControl("txt_insert_hr_name");
        TextBox txt_insert_hr_start_time = (TextBox)gvr.FindControl("txt_insert_hr_start_time");
        TextBox txt_insert_hr_end_time = (TextBox)gvr.FindControl("txt_insert_hr_end_time");

        if (!ValidateInsertPage(gvr)) { return; }

        //all vars to one object
        ch_hours hr1 = new ch_hours();
        hr1.hr_Name = txt_insert_hr_name.Text.Trim();
        hr1.hr_Start_Time = txt_insert_hr_start_time.Text.Trim();
        hr1.hr_End_Time = txt_insert_hr_end_time.Text.Trim();
        hr1.sc_Id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);

        string err = ch_hoursSvc.AddHour(hr1);

        if (err == "")//אם ההכנסה התבצעה
        {
            lblErrGV.Text = "";
            gvHours.ShowFooter = false;
            btnInsert.Enabled = true;

            //Bind data to GridView
            int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
            DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
            GridViewSvc.GVBind(dsHours, gvHours);
        }
        else
        {
            lblErrGV.Text = err;
            txt_insert_hr_name.Text = "";
            //Bind data to GridView
            int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
            DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
            GridViewSvc.GVBind(dsHours, gvHours);
        }
    }
    protected void btn_delete_hr_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        int hr_id = Convert.ToInt32(gvHours.DataKeys[gvr.RowIndex].Value.ToString());
        ch_hoursSvc.DeleteHourById(hr_id);

        //Bind data to GridView
        int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
        DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
        GridViewSvc.GVBind(dsHours, gvHours);
    }
    protected void ddlSchools_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            //DDL Schools
            DataSet dsSchools = ch_schoolsSvc.GetSchools();

            ddlSchools.DataSource = dsSchools;
            ddlSchools.DataValueField = "sc_id";
            dsSchools.Tables[0].Columns.Add("school", typeof(string), "sc_name + ' - ' + cty_name");
            ddlSchools.DataTextField = "school";
            ddlSchools.DataBind();

            ddlSchools.Items.Add(new ListItem("-בחר בית ספר-", "-1"));
            ddlSchools.SelectedIndex = ddlSchools.Items.Count - 1;
        }
    }
    protected void ddlSchools_SelectedIndexChanged(object sender, EventArgs e) {
        if (ddlSchools.SelectedValue == "-1") {
            gvHours.Visible = false;
            btnInsert.Visible = false;
        }
        else {
            //Bind data to GridView
            DataSet dsHours = ch_hoursSvc.GetHours(Convert.ToInt32(ddlSchools.SelectedValue));
            GridViewSvc.GVBind(dsHours, gvHours);
            gvHours.Visible = true;
            btnInsert.Visible = true;
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        gvHours.ShowFooter = true;
        this.btnInsert.Enabled = false;

        //Bind data to GridView
        int sc_id = Convert.ToInt32(Session["lvl_id"]) < 4 ? Convert.ToInt32(Session["sc_id"]) : Convert.ToInt32(ddlSchools.SelectedValue);
        DataSet dsHours = ch_hoursSvc.GetHours(sc_id);
        GridViewSvc.GVBind(dsHours, gvHours);
    }
}