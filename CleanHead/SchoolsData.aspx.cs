using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class SchoolsData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["lvl_id"]) < 4) {
            Response.Redirect("~/Errors/403Error.aspx");
        }
    }
    protected void gvBind() {
        if (Request.QueryString["search"] == null) {
            //Bind data to GridView
            DataSet dsSchools = ch_schoolsSvc.GetSchools();
            GridViewSvc.GVBind(dsSchools, gvSchools);
        }
        else {
            if (Request.QueryString["search"] != "") {
                txtSearch.Text = Request.QueryString["search"].ToString();

                //Bind data to GridView
                DataSet dsSchools = ch_schoolsSvc.GetSchools(Request.QueryString["search"].ToString());
                GridViewSvc.GVBind(dsSchools, gvSchools);
            }
        }
    }
    protected void gvSchools_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvBind();
        }
    }
    protected void gvSchools_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        gvSchools.PageIndex = e.NewPageIndex;

        //Bind data to GridView
        gvBind();
    }
    protected void btn_edit_sc_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        gvSchools.EditIndex = gvr.RowIndex;

        //Bind data to GridView
        gvBind();
    }
    protected void btn_update_sc_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int sc_id = Convert.ToInt32(gvSchools.DataKeys[gvr.RowIndex].Value.ToString());
        TextBox txt_edit_sc_symbol = (TextBox)gvr.FindControl("txt_edit_sc_symbol");
        TextBox txt_edit_sc_name = (TextBox)gvr.FindControl("txt_edit_sc_name");
        DropDownList txt_edit_ddlCities = (DropDownList)gvr.FindControl("txt_edit_ddlCities");
        TextBox txt_edit_sc_address = (TextBox)gvr.FindControl("txt_edit_sc_address");
        TextBox txt_edit_sc_telephone = (TextBox)gvr.FindControl("txt_edit_sc_telephone");

        //all vars to one object
        //add cty to db
        ch_cities cty = new ch_cities();
        cty.cty_Name = txt_edit_ddlCities.SelectedItem.Text;

        //בדיקה אם קיים בWS
        Cities.Cities ctyWs = new Cities.Cities();
        if (!ctyWs.IsExist(cty.cty_Name)) {
            lblErrGV.Text = "העיר כבר לא קיימת במאגר הנתונים הארצי";
            return;
        }
        ch_citiesSvc.AddCity(cty);

        //all vars to one object
        ch_schools sc1 = new ch_schools();
        sc1.sc_Symbol = txt_edit_sc_symbol.Text.Trim();
        sc1.sc_Name = txt_edit_sc_name.Text.Trim();
        sc1.cty_Id = ch_citiesSvc.GetIdByCtyName(cty.cty_Name);
        sc1.sc_Address = txt_edit_sc_address.Text.Trim();
        sc1.sc_Telephone = txt_edit_sc_telephone.Text.Trim();

        if ((lblErrGV.Text = Validation(sc1)) == "") {
            string err = ch_schoolsSvc.UpdateSchoolById(sc_id, sc1);
            if (err == "")//אם העדכון התבצע
            {
                lblErrGV.Text = string.Empty;
                gvSchools.EditIndex = -1;

                //Bind data to GridView
                gvBind();
            }
            else
            {
                lblErrGV.Text = err;

                //Bind data to GridView
                gvBind();
            }
        }
    }
    protected void btn_cancel_update_sc_Click(object sender, ImageClickEventArgs e)
    {
        gvSchools.EditIndex = -1;
        lblErrGV.Text = "";

        //Bind data to GridView
        gvBind();
    }
    protected void btn_cancel_insert_sc_Click(object sender, ImageClickEventArgs e)
    {
        gvSchools.ShowFooter = false;
        btnInsert.Enabled = true;
        lblErrGV.Text = "";

        //Bind data to GridView
        gvBind();
    }
    protected void btn_insert_sc_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        TextBox txt_insert_sc_symbol = (TextBox)gvr.FindControl("txt_insert_sc_symbol");
        TextBox txt_insert_sc_name = (TextBox)gvr.FindControl("txt_insert_sc_name");
        DropDownList txt_insert_ddlCities = (DropDownList)gvr.FindControl("txt_insert_ddlCities");
        TextBox txt_insert_sc_address = (TextBox)gvr.FindControl("txt_insert_sc_address");
        TextBox txt_insert_sc_telephone = (TextBox)gvr.FindControl("txt_insert_sc_telephone");

        //all vars to one object
        //add cty to db
        ch_cities cty = new ch_cities();
        cty.cty_Name = txt_insert_ddlCities.SelectedItem.Text;

        //בדיקה אם קיים בWS
        Cities.Cities ctyWs = new Cities.Cities();
        if (!ctyWs.IsExist(cty.cty_Name)) {
            lblErrGV.Text = "העיר כבר לא קיימת במאגר הנתונים הארצי";
            return;
        }
        ch_citiesSvc.AddCity(cty);

        //all vars to one object
        ch_schools sc1 = new ch_schools();
        sc1.sc_Symbol = txt_insert_sc_symbol.Text.Trim();
        sc1.sc_Name = txt_insert_sc_name.Text.Trim();
        sc1.cty_Id = ch_citiesSvc.GetIdByCtyName(cty.cty_Name);
        sc1.sc_Address = txt_insert_sc_address.Text.Trim();
        sc1.sc_Telephone = txt_insert_sc_telephone.Text.Trim();

        if ((lblErrGV.Text = Validation(sc1)) != "") { return; }
        string err = ch_schoolsSvc.AddSchool(sc1);

        if (err == "")//אם ההכנסה התבצע
        {
            lblErrGV.Text = "";
            gvSchools.ShowFooter = false;
            btnInsert.Enabled = true;

            //Bind data to GridView
            gvBind();
        }
        else
        {
            lblErrGV.Text = err;
            txt_insert_sc_name.Text = "";
            //Bind data to GridView
            gvBind();
        }
 
    }
    public string Validation(ch_schools sc1) {
        if (sc1.sc_Symbol == "")
            return "הכנס סמל מוסד";
        if (!Regex.IsMatch(sc1.sc_Symbol, "^[0-9]{6}$")) {
            return "סמל מוסד לא תקין";
        }

        if (sc1.sc_Name == "")
            return "הכנס שם בית ספר";
        if (!Regex.IsMatch(sc1.sc_Name, @"^[א-תa-zA-z\s-]{2,35}$")) {
            return "שם בית ספר לא תקין";
        }

        if (sc1.cty_Id == -1)
            return "הכנס עיר";

        if (sc1.sc_Address == "")
            return "הכנס כתובת";
        if (!Regex.IsMatch(sc1.sc_Address, @"^[א-תa-zA-z0-9\s-,]{2,35}$")) {
            return "כתובת לא תקינה";
        }

        if (sc1.sc_Telephone == "")
            return "הכנס טלפון";
        if (!Regex.IsMatch(sc1.sc_Telephone, @"^0\d([\d]{0,1})([-]{0,1})\d{7}$")) {
            return "טלפון לא תקין";
        }
        return "";
    }
    protected void btn_delete_sc_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        int sc_id = Convert.ToInt32(gvSchools.DataKeys[gvr.RowIndex].Value.ToString());
        ch_schoolsSvc.DeleteSchoolById(sc_id);

        //Bind data to GridView
        gvBind();
    }
    protected void gvSchools_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //check if is in edit mode
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList txt_edit_ddlCities = (DropDownList)e.Row.FindControl("txt_edit_ddlCities");
                Cities.Cities cty = new Cities.Cities();
                txt_edit_ddlCities.DataSource = cty.GetCities();

                //Bind subcategories data to dropdownlist
                txt_edit_ddlCities.DataTextField = "שם_ישוב";
                txt_edit_ddlCities.DataBind();
                GridViewRow gvr = (GridViewRow)txt_edit_ddlCities.NamingContainer;

                DataRowView dr = e.Row.DataItem as DataRowView;
                txt_edit_ddlCities.SelectedItem.Text = dr["cty_name"].ToString();
            }
        }
    }
    protected void gvSchools_RowUpdating(object sender, GridViewUpdateEventArgs e) {

    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        gvSchools.ShowFooter = true;
        this.btnInsert.Enabled = false;

        //Bind data to GridView
        gvBind();

        //DDL cities
        Cities.Cities cty = new Cities.Cities();
        DropDownList txt_insert_ddlCities = (DropDownList)gvSchools.FooterRow.FindControl("txt_insert_ddlCities");
        txt_insert_ddlCities.DataSource = cty.GetCities();
        txt_insert_ddlCities.DataTextField = "שם_ישוב";
        txt_insert_ddlCities.DataBind();

        ListItem li = new ListItem();
        li.Text = "-בחר עיר-";
        li.Value = "-1";
        txt_insert_ddlCities.Items.Add(li);
        txt_insert_ddlCities.SelectedIndex = txt_insert_ddlCities.Items.Count - 1;
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        Button btn = (Button)sender;
        string currentUrl = Request.Url.AbsolutePath; // get current url eg.  /TESTERS/Default6.aspx
        Response.Redirect(currentUrl + "?search=" + txtSearch.Text);
    }
}