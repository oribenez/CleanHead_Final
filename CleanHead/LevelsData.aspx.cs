using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class LevelsData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["lvl_id"]) < 5) {
            Response.Redirect("~/Errors/403Error.aspx");
        }
    }
    protected void gvLevels_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Bind data to GridView
            DataSet dsLevels = ch_levelsSvc.GetLevels();
            GridViewSvc.GVBind(dsLevels, gvLevels);
        }
    }
    protected void gvLevels_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        gvLevels.PageIndex = e.NewPageIndex;

        //Bind data to GridView
        DataSet dsLevels = ch_levelsSvc.GetLevels();
        GridViewSvc.GVBind(dsLevels, gvLevels);
    }
    protected void btn_edit_lvl_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        gvLevels.EditIndex = gvr.RowIndex;
        //Bind data to GridView
        DataSet dsLevels = ch_levelsSvc.GetLevels();
        GridViewSvc.GVBind(dsLevels, gvLevels);
    }
    protected void btn_update_lvl_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int lvl_id = Convert.ToInt32(gvLevels.DataKeys[gvr.RowIndex].Values["lvl_id"].ToString());
        string lvl_name = gvLevels.DataKeys[gvr.RowIndex].Values["lvl_name"].ToString();
        TextBox txt_edit_lvl_id = (TextBox)gvr.FindControl("txt_edit_lvl_id");
        TextBox txt_edit_lvl_name = (TextBox)gvr.FindControl("txt_edit_lvl_name");
        TextBox txt_edit_lvl_desc = (TextBox)gvr.FindControl("txt_edit_lvl_desc");

        if (lvl_id.ToString() != txt_edit_lvl_id.Text)
        {
            
        }
        if (txt_edit_lvl_id.Text.Trim() != "" && txt_edit_lvl_name.Text.Trim() != "")
        {
            //all vars to one object
            ch_levels lvl1 = new ch_levels();
            lvl1.lvl_Id = Convert.ToInt32(txt_edit_lvl_id.Text.Trim());
            lvl1.lvl_Name = txt_edit_lvl_name.Text.Trim();
            lvl1.lvl_Desc = txt_edit_lvl_desc.Text.Trim();

            string err = ch_levelsSvc.UpdateLevelById(lvl_id, lvl_name, lvl1);
            if (err == "")//אם העדכון התבצע
            {
                lblErrGV.Text = string.Empty;
                gvLevels.EditIndex = -1;

                //Bind data to GridView
                DataSet dsLevels = ch_levelsSvc.GetLevels();
                GridViewSvc.GVBind(dsLevels, gvLevels);
            }
            else
            {
                lblErrGV.Text = err;

                //Bind data to GridView
                DataSet dsLevels = ch_levelsSvc.GetLevels();
                GridViewSvc.GVBind(dsLevels, gvLevels);
            }
        }
        else
        {
            lblErrGV.Text = "הכנס דרגה";
        }
    }
    protected void btn_cancel_update_lvl_Click(object sender, ImageClickEventArgs e)
    {
        gvLevels.EditIndex = -1;
        lblErrGV.Text = "";

        //Bind data to GridView
        DataSet dsLevels = ch_levelsSvc.GetLevels();
        GridViewSvc.GVBind(dsLevels, gvLevels);
    }
    protected void btn_cancel_insert_lvl_Click(object sender, ImageClickEventArgs e)
    {
        gvLevels.ShowFooter = false;
        btnInsert.Enabled = true;
        lblErrGV.Text = "";

        //Bind data to GridView
        DataSet dsLevels = ch_levelsSvc.GetLevels();
        GridViewSvc.GVBind(dsLevels, gvLevels);
    }
    protected void btn_insert_lvl_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        TextBox txt_insert_lvl_id = (TextBox)gvr.FindControl("txt_insert_lvl_id");
        TextBox txt_insert_lvl_name = (TextBox)gvr.FindControl("txt_insert_lvl_name");
        TextBox txt_insert_lvl_desc = (TextBox)gvr.FindControl("txt_insert_lvl_desc");

        if (txt_insert_lvl_id.Text.Trim() != "" && txt_insert_lvl_name.Text.Trim() != "")
        {
            //all vars to one object
            ch_levels lvl1 = new ch_levels();
            lvl1.lvl_Id = Convert.ToInt32(txt_insert_lvl_id.Text.Trim());
            lvl1.lvl_Name = txt_insert_lvl_name.Text.Trim();
            lvl1.lvl_Desc = txt_insert_lvl_desc.Text.Trim();

            string err = ch_levelsSvc.AddLevel(lvl1);

            if (err == "")//אם ההכנסה התבצע
            {
                lblErrGV.Text = "";
                gvLevels.ShowFooter = false;
                btnInsert.Enabled = true;

                //Bind data to GridView
                DataSet dsLevels = ch_levelsSvc.GetLevels();
                GridViewSvc.GVBind(dsLevels, gvLevels);
            }
            else
            {
                lblErrGV.Text = err;
                txt_insert_lvl_id.Text = "";
                txt_insert_lvl_name.Text = "";
                txt_insert_lvl_desc.Text = "";
                //Bind data to GridView
                DataSet dsLevels = ch_levelsSvc.GetLevels();
                GridViewSvc.GVBind(dsLevels, gvLevels);
            }

        }
        else
        {
            lblErrGV.Text = "הכנס דרגה";
        }
    }
    protected void btn_delete_lvl_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        int lvl_id = Convert.ToInt32(gvLevels.DataKeys[gvr.RowIndex].Value.ToString());
        ch_levelsSvc.DeleteLevelById(lvl_id);

        //Bind data to GridView
        DataSet dsLevels = ch_levelsSvc.GetLevels();
        GridViewSvc.GVBind(dsLevels, gvLevels);
    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        gvLevels.ShowFooter = true;
        this.btnInsert.Enabled = false;

        //Bind data to GridView
        DataSet dsLevels = ch_levelsSvc.GetLevels();
        GridViewSvc.GVBind(dsLevels, gvLevels);
    }
}