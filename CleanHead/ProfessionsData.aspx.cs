using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class ProfessionsData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void GVProfessions_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Bind data to GridView
            DataSet dsProfessions = ch_professionsSvc.GetProfessions();
            GridViewSvc.GVBind(dsProfessions, GVProfessions);
        }
    }
    protected void GVProfessions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        GVProfessions.PageIndex = e.NewPageIndex;

        //Bind data to GridView
        DataSet dsProfessions = ch_professionsSvc.GetProfessions();
        GridViewSvc.GVBind(dsProfessions, GVProfessions);
    }

    protected void btn_edit_pro_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        GVProfessions.EditIndex = gvr.RowIndex;
        //Bind data to GridView
        DataSet dsProfessions = ch_professionsSvc.GetProfessions();
        GridViewSvc.GVBind(dsProfessions, GVProfessions);
    }
    protected void btn_update_pro_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int pro_id = Convert.ToInt32(GVProfessions.DataKeys[gvr.RowIndex].Value.ToString());
        TextBox txt_edit_pro_name = (TextBox)gvr.FindControl("txt_edit_pro_name");

        if (txt_edit_pro_name.Text.Trim() != "")
        {
            if (Regex.IsMatch(txt_edit_pro_name.Text.Trim(), @"^[א-תa-zA-Z''-'\s]{2,35}$")) {
                //all vars to one object
                ch_professions pro1 = new ch_professions();
                pro1.pro_Name = txt_edit_pro_name.Text.Trim();


                string err = ch_professionsSvc.UpdateProById(pro_id, pro1);
                if (err == "")//אם העדכון התבצע
                {
                    lblErrGV.Text = string.Empty;
                    GVProfessions.EditIndex = -1;

                    //Bind data to GridView
                    DataSet dsProfessions = ch_professionsSvc.GetProfessions();
                    GridViewSvc.GVBind(dsProfessions, GVProfessions);
                }
                else
                {
                    lblErrGV.Text = err;

                    //Bind data to GridView
                    DataSet dsProfessions = ch_professionsSvc.GetProfessions();
                    GridViewSvc.GVBind(dsProfessions, GVProfessions);
                }
            }
            else {
                lblErrGV.Text = "הכנס אותיות בין 2 ל 35 תווים";
            }
        }
        else
        {
            lblErrGV.Text = "הכנס מקצוע";
        }
    }
    protected void btn_cancel_update_pro_Click(object sender, ImageClickEventArgs e)
    {
        GVProfessions.EditIndex = -1;
        lblErrGV.Text = "";

        //Bind data to GridView
        DataSet dsProfessions = ch_professionsSvc.GetProfessions();
        GridViewSvc.GVBind(dsProfessions, GVProfessions);
    }
    protected void btn_cancel_insert_pro_Click(object sender, ImageClickEventArgs e)
    {
        GVProfessions.ShowFooter = false;
        btnInsert.Enabled = true;
        lblErrGV.Text = "";

        //Bind data to GridView
        DataSet dsProfessions = ch_professionsSvc.GetProfessions();
        GridViewSvc.GVBind(dsProfessions, GVProfessions);
    }
    protected void btn_insert_pro_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        TextBox txt_insert_pro_name = (TextBox)gvr.FindControl("txt_insert_pro_name");

        if (txt_insert_pro_name.Text.Trim() != "")
        {
            if (Regex.IsMatch(txt_insert_pro_name.Text.Trim(), @"^[א-תa-zA-Z''-'\s]{2,35}$")) {
                //all vars to one object
                ch_professions pro1 = new ch_professions();
                pro1.pro_Name = txt_insert_pro_name.Text.Trim();

                string err = ch_professionsSvc.AddPro(pro1);

                if (err == "")//אם ההכנסה התבצע
                {
                    lblErrGV.Text = "";
                    GVProfessions.ShowFooter = false;
                    btnInsert.Enabled = true;

                    //Bind data to GridView
                    DataSet dsProfessions = ch_professionsSvc.GetProfessions();
                    GridViewSvc.GVBind(dsProfessions, GVProfessions);
                }
                else
                {
                    lblErrGV.Text = err;
                    txt_insert_pro_name.Text = "";
                    //Bind data to GridView
                    DataSet dsProfessions = ch_professionsSvc.GetProfessions();
                    GridViewSvc.GVBind(dsProfessions, GVProfessions);
                }
            }
            else {
                lblErrGV.Text = "הכנס אותיות בין 2 ל 35 תווים";
            }
            
        }
        else
        {
            lblErrGV.Text = "הכנס מקצוע";
        }
    }
    protected void btn_delete_pro_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        int pro_id = Convert.ToInt32(GVProfessions.DataKeys[gvr.RowIndex].Value.ToString());
        ch_professionsSvc.DeleteProById(pro_id);

        //Bind data to GridView
        DataSet dsProfessions = ch_professionsSvc.GetProfessions();
        GridViewSvc.GVBind(dsProfessions, GVProfessions);
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        GVProfessions.ShowFooter = true;
        this.btnInsert.Enabled = false;

        //Bind data to GridView
        DataSet dsProfessions = ch_professionsSvc.GetProfessions();
        GridViewSvc.GVBind(dsProfessions, GVProfessions);
    }
}