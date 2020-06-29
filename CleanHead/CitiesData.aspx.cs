using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class CitiesData : System.Web.UI.Page
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
            DataSet dsCities = ch_citiesSvc.GetCities();
            GridViewSvc.GVBind(dsCities, gvCities);
        }
        else {
            if (Request.QueryString["search"] == "") {
                //Bind data to GridView
                DataSet dsCities = ch_citiesSvc.GetCities();
                GridViewSvc.GVBind(dsCities, gvCities);
            }
            else {
                txtSearch.Text = Request.QueryString["search"].ToString();

                //Bind data to GridView
                DataSet dsCities = ch_citiesSvc.GetCities(Request.QueryString["search"].ToString());
                GridViewSvc.GVBind(dsCities, gvCities);
            }
        }
    }
    protected void gvCities_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvBind();
        }
    }
    protected void gvCities_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        gvCities.PageIndex = e.NewPageIndex;

        gvBind();
    }
    protected void btn_edit_cty_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        gvCities.EditIndex = gvr.RowIndex;
        gvBind();
    }
    protected void btn_update_cty_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int cty_id = Convert.ToInt32(gvCities.DataKeys[gvr.RowIndex].Value.ToString());
        TextBox txt_edit_cty_name = (TextBox)gvr.FindControl("txt_edit_cty_name");

        if (txt_edit_cty_name.Text.Trim() != "")
        {
            if (Regex.IsMatch(txt_edit_cty_name.Text.Trim(), @"^[א-תa-zA-Z''-'\s]{2,35}$")) {
                //all vars to one object
                ch_cities cty1 = new ch_cities();
                cty1.cty_Name = txt_edit_cty_name.Text.Trim();


                string err = ch_citiesSvc.UpdateCityById(cty_id, cty1);
                if (err == "")//אם העדכון התבצע
                {
                    lblErrGV.Text = string.Empty;
                    gvCities.EditIndex = -1;

                    gvBind();
                }
                else
                {
                    lblErrGV.Text = err;

                    gvBind();
                }
            }
            else {
                lblErrGV.Text = "הכנס אותיות בין 2 ל 35 תווים";
            }
        }
        else
        {
            lblErrGV.Text = "הכנס עיר";
        }
    }
    protected void btn_cancel_update_cty_Click(object sender, ImageClickEventArgs e)
    {
        gvCities.EditIndex = -1;
        lblErrGV.Text = "";

        gvBind();
    }
    protected void btn_cancel_insert_cty_Click(object sender, ImageClickEventArgs e)
    {
        gvCities.ShowFooter = false;
        btnInsert.Enabled = true;
        lblErrGV.Text = "";

        gvBind();
    }
    protected void btn_insert_cty_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        TextBox txt_insert_cty_name = (TextBox)gvr.FindControl("txt_insert_cty_name");

        if (txt_insert_cty_name.Text.Trim() != "")
        {
            if (Regex.IsMatch(txt_insert_cty_name.Text.Trim(), @"^[א-תa-zA-Z''-'\s]{2,35}$")) {
                //all vars to one object
                ch_cities cty1 = new ch_cities();
                cty1.cty_Name = txt_insert_cty_name.Text.Trim();

                string err = ch_citiesSvc.AddCity(cty1);

                if (err == "")//אם ההכנסה התבצע
                {
                    lblErrGV.Text = "";
                    gvCities.ShowFooter = false;
                    btnInsert.Enabled = true;

                    gvBind();
                }
                else
                {
                    lblErrGV.Text = err;
                    txt_insert_cty_name.Text = "";
                    gvBind();
                }
            }
            else {
                lblErrGV.Text = "הכנס אותיות בין 2 ל 35 תווים";
            }
        }
        else
        {
            lblErrGV.Text = "הכנס עיר";
        }
    }
    protected void btn_delete_cty_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        int cty_id = Convert.ToInt32(gvCities.DataKeys[gvr.RowIndex].Value.ToString());

        int numRel = ch_citiesSvc.Relationships(cty_id);
        if (numRel > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(),"CounterScript", "Relationship();", true);
            //Response.Write("<script>Relationship();</script>");
        }
        else
        {
            ch_citiesSvc.DeleteCityById(cty_id);
            gvBind();
        }
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        gvCities.ShowFooter = true;
        this.btnInsert.Enabled = false;

        gvBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e) {
        Button btn = (Button)sender;
        string currentUrl = Request.Url.AbsolutePath; // get current url eg.  /TESTERS/Default6.aspx
        Response.Redirect(currentUrl + "?search=" + txtSearch.Text);
    }
}