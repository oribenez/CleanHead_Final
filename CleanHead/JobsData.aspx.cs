using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class JobsData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void gvJobs_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Bind data to GridView
            DataSet dsJobs = ch_jobsSvc.GetJobs();
            GridViewSvc.GVBind(dsJobs, gvJobs);
        }
    }
    protected void gvJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Change page index
        gvJobs.PageIndex = e.NewPageIndex;

        //Bind data to GridView
        DataSet dsJobs = ch_jobsSvc.GetJobs();
        GridViewSvc.GVBind(dsJobs, gvJobs);
    }
    protected void btn_edit_job_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        gvJobs.EditIndex = gvr.RowIndex;
        //Bind data to GridView
        DataSet dsJobs = ch_jobsSvc.GetJobs();
        GridViewSvc.GVBind(dsJobs, gvJobs);
    }
    protected void btn_update_job_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int job_id = Convert.ToInt32(gvJobs.DataKeys[gvr.RowIndex].Value.ToString());
        TextBox txt_edit_job_name = (TextBox)gvr.FindControl("txt_edit_job_name");

        if (txt_edit_job_name.Text.Trim() != "")
        {
            if (Regex.IsMatch(txt_edit_job_name.Text.Trim(), @"^[א-תa-zA-Z''-'\s]{2,35}$")) {
                //all vars to one object
                ch_jobs job1 = new ch_jobs();
                job1.job_Name = txt_edit_job_name.Text.Trim();

                string err = ch_jobsSvc.UpdateJobById(job_id, job1);
                if (err == "")//אם העדכון התבצע
                {
                    lblErrGV.Text = string.Empty;
                    gvJobs.EditIndex = -1;

                    //Bind data to GridView
                    DataSet dsJobs = ch_jobsSvc.GetJobs();
                    GridViewSvc.GVBind(dsJobs, gvJobs);
                }
                else {
                    lblErrGV.Text = err;

                    //Bind data to GridView
                    DataSet dsJobs = ch_jobsSvc.GetJobs();
                    GridViewSvc.GVBind(dsJobs, gvJobs);
                }
            }
            else {
                lblErrGV.Text = "הכנס אותיות בין 2 ל 35 תווים";
            }
        }
        else
        {
            lblErrGV.Text = "הכנס תפקיד";
        }
    }
    protected void btn_cancel_update_job_Click(object sender, ImageClickEventArgs e)
    {
        gvJobs.EditIndex = -1;
        lblErrGV.Text = "";

        //Bind data to GridView
        DataSet dsJobs = ch_jobsSvc.GetJobs();
        GridViewSvc.GVBind(dsJobs, gvJobs);
    }
    protected void btn_cancel_insert_job_Click(object sender, ImageClickEventArgs e)
    {
        gvJobs.ShowFooter = false;
        btnInsert.Enabled = true;
        lblErrGV.Text = "";

        //Bind data to GridView
        DataSet dsJobs = ch_jobsSvc.GetJobs();
        GridViewSvc.GVBind(dsJobs, gvJobs);
    }
    protected void btn_insert_job_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        TextBox txt_insert_job_name = (TextBox)gvr.FindControl("txt_insert_job_name");
        if (txt_insert_job_name.Text.Trim() != "")
        {
            if (Regex.IsMatch(txt_insert_job_name.Text.Trim(), @"^[א-תa-zA-Z''-'\s]{2,35}$")) {
                //all vars to one object
                ch_jobs job1 = new ch_jobs();
                job1.job_Name = txt_insert_job_name.Text.Trim();

                string err = ch_jobsSvc.AddJob(job1);

                if (err == "") {//אם ההכנסה התבצע
                
                    lblErrGV.Text = "";
                    gvJobs.ShowFooter = false;
                    btnInsert.Enabled = true;

                    //Bind data to GridView
                    DataSet dsJobs = ch_jobsSvc.GetJobs();
                    GridViewSvc.GVBind(dsJobs, gvJobs);
                }
                else {
                    lblErrGV.Text = err;
                    txt_insert_job_name.Text = "";
                    //Bind data to GridView
                    DataSet dsJobs = ch_jobsSvc.GetJobs();
                    GridViewSvc.GVBind(dsJobs, gvJobs);
                }
            }
            else {
                lblErrGV.Text = "הכנס אותיות בין 2 ל 35 תווים";
            }
        }
        else
        {
            lblErrGV.Text = "הכנס תפקיד";
        }
    }
    protected void btn_delete_job_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        //Delete selected row
        int job_id = Convert.ToInt32(gvJobs.DataKeys[gvr.RowIndex].Value.ToString());
        ch_jobsSvc.DeleteJobById(job_id);

        //Bind data to GridView
        DataSet dsJobs = ch_jobsSvc.GetJobs();
        GridViewSvc.GVBind(dsJobs, gvJobs);
    }
    protected void gvJobs_RowUpdating(object sender, GridViewUpdateEventArgs e) {

    }
    protected void btnInsert_Click(object sender, EventArgs e) {
        gvJobs.ShowFooter = true;
        this.btnInsert.Enabled = false;

        //Bind data to GridView
        DataSet dsJobs = ch_jobsSvc.GetJobs();
        GridViewSvc.GVBind(dsJobs, gvJobs);
    }
}