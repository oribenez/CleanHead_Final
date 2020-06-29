using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class RoomsData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void gvRooms_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                //Bind data to GridView
                DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
                GridViewSvc.GVBind(dsRooms, gvRooms);

            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                gvRooms.Visible = false;
            }
            
        }
    }
    protected void gvRooms_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        gvRooms.PageIndex = e.NewPageIndex;

        if (Convert.ToInt32(Session["lvl_id"]) == 3) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
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
        if (ddlSchools.SelectedValue != "-1") {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
            GridViewSvc.GVBind(dsRooms, gvRooms);
            gvRooms.Visible = true;
        }
        else {
            gvRooms.Visible = false;
        }
    }

    protected void btn_edit_rm_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        gvRooms.EditIndex = gvr.RowIndex;

        if (Convert.ToInt32(Session["lvl_id"]) == 3) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
    }
    protected void btn_update_rm_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rm_id = Convert.ToInt32(gvRooms.DataKeys[gvr.RowIndex].Value.ToString());
        TextBox txt_edit_rm_name = (TextBox)gvr.FindControl("txt_edit_rm_name");

        if (txt_edit_rm_name.Text.Trim() != "")
        {
            //all vars to one object
            ch_rooms rm1 = new ch_rooms();
            rm1.rm_Name = txt_edit_rm_name.Text.Trim();

            string err = ch_roomsSvc.UpdateRoomById(rm_id, rm1);
            if (err == "")//אם העדכון התבצע
            {
                lblErrGV.Text = string.Empty;
                gvRooms.EditIndex = -1;

                if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                    //Bind data to GridView
                    DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
                    GridViewSvc.GVBind(dsRooms, gvRooms);
                }
                else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                    //Bind data to GridView
                    DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
                    GridViewSvc.GVBind(dsRooms, gvRooms);
                }
            }
            else
            {
                lblErrGV.Text = err;

                if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                    //Bind data to GridView
                    DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
                    GridViewSvc.GVBind(dsRooms, gvRooms);
                }
                else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                    //Bind data to GridView
                    DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
                    GridViewSvc.GVBind(dsRooms, gvRooms);
                }
            }
        }
        else
        {
            lblErrGV.Text = "הכנס חדר";
        }
    }
    protected void btn_cancel_update_rm_Click(object sender, ImageClickEventArgs e)
    {
        gvRooms.EditIndex = -1;
        lblErrGV.Text = "";

        if (Convert.ToInt32(Session["lvl_id"]) == 3) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
    }
    protected void btn_cancel_insert_rm_Click(object sender, ImageClickEventArgs e)
    {
        gvRooms.ShowFooter = false;
        btnInsert.Enabled = true;
        lblErrGV.Text = "";

        if (Convert.ToInt32(Session["lvl_id"]) == 3) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
    }
    protected void btn_insert_rm_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        TextBox txt_insert_rm_name = (TextBox)gvr.FindControl("txt_insert_rm_name");

        if (txt_insert_rm_name.Text.Trim() != "")
        {
            //all vars to one object
            ch_rooms rm1 = new ch_rooms();
            rm1.rm_Name = txt_insert_rm_name.Text.Trim();

            if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                rm1.sc_Id = Convert.ToInt32(Session["sc_id"]);
            }
            else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                rm1.sc_Id = Convert.ToInt32(ddlSchools.SelectedValue);
            }

            string err = ch_roomsSvc.AddRoom(rm1);

            if (err == "")//אם ההכנסה התבצע
            {
                lblErrGV.Text = "";
                gvRooms.ShowFooter = false;
                btnInsert.Enabled = true;

                if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                    //Bind data to GridView
                    DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
                    GridViewSvc.GVBind(dsRooms, gvRooms);
                }
                else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                    //Bind data to GridView
                    DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
                    GridViewSvc.GVBind(dsRooms, gvRooms);
                }
            }
            else
            {
                lblErrGV.Text = err;
                txt_insert_rm_name.Text = "";

                if (Convert.ToInt32(Session["lvl_id"]) == 3) {
                    //Bind data to GridView
                    DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
                    GridViewSvc.GVBind(dsRooms, gvRooms);
                }
                else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
                    //Bind data to GridView
                    DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
                    GridViewSvc.GVBind(dsRooms, gvRooms);
                }
            }

        }
        else
        {
            lblErrGV.Text = "הכנס חדר";
        }
    }
    protected void btn_delete_rm_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        int rm_id = Convert.ToInt32(gvRooms.DataKeys[gvr.RowIndex].Value.ToString());
        ch_roomsSvc.DeleteRoomById(rm_id);

        if (Convert.ToInt32(Session["lvl_id"]) == 3) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
    }

    protected void btnInsert_Click(object sender, EventArgs e) {
        gvRooms.ShowFooter = true;
        this.btnInsert.Enabled = false;


        if (Convert.ToInt32(Session["lvl_id"]) == 3) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(Session["sc_id"]));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
        else if (Convert.ToInt32(Session["lvl_id"]) >= 4) {
            //Bind data to GridView
            DataSet dsRooms = ch_roomsSvc.GetRooms(Convert.ToInt32(ddlSchools.SelectedValue));
            GridViewSvc.GVBind(dsRooms, gvRooms);
        }
    }
}