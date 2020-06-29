using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Schedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void cldr1_DayRender(object sender, DayRenderEventArgs e) {
        CalendarDay currentDay = ((DayRenderEventArgs)e).Day;
        TableCell currentCell = ((DayRenderEventArgs)e).Cell;

        if (ddlReligious.SelectedValue == "-1") {
            HolidaysVacations.HolidaysVacations hv = new HolidaysVacations.HolidaysVacations();
            DataSet ds = hv.GetHolidaysVacations();
            foreach (DataRow dr in ds.Tables[0].Rows) {
                if (!currentDay.IsOtherMonth
                            && currentDay.Date >= Convert.ToDateTime(dr["hld_vac_start_date"]).Date
                            && currentDay.Date <= Convert.ToDateTime(dr["hld_vac_end_date"]).Date) {
                    Label lblName = new Label();
                    lblName.CssClass = "lblName";
                    // Show the Event Text
                    lblName.Text = dr["hld_vac_name"].ToString();
                    currentCell.Controls.Add(lblName);
                }
            }
        }
        else {
            HolidaysVacations.HolidaysVacations hv = new HolidaysVacations.HolidaysVacations();
            int rlg_id = Convert.ToInt32(ddlReligious.SelectedValue);
            DataSet ds = hv.GetHolidaysVacationsByRlgId(rlg_id);
            foreach (DataRow dr in ds.Tables[0].Rows) {
                if (!currentDay.IsOtherMonth
                            && currentDay.Date >= Convert.ToDateTime(dr["hld_vac_start_date"]).Date
                            && currentDay.Date <= Convert.ToDateTime(dr["hld_vac_end_date"]).Date) {
                    Label lblName = new Label();
                    lblName.CssClass = "lblName";
                    // Show the Event Text
                    lblName.Text = dr["hld_vac_name"].ToString();
                    currentCell.Controls.Add(lblName);
                }
            }
        }
    }
    protected void cldr1_SelectionChanged(object sender, EventArgs e) {
        HolidaysVacations.HolidaysVacations hv = new HolidaysVacations.HolidaysVacations();
        DataSet ds = null;
        if (ddlReligious.SelectedValue == "-1") {
            ds = hv.GetHolidaysVacationsByDate(cldr1.SelectedDate);
        }
        else {
            ds = hv.GetHolidaysVacationsByDateByRlgId(cldr1.SelectedDate, Convert.ToInt32(ddlReligious.SelectedValue));
        }


        foreach (DataRow dr in ds.Tables[0].Rows) {
            Label lblInfo = new Label();
            lblInfo.Text = "<br /><b><u>" + dr["hld_vac_name"].ToString() + "</b></u><br />" + dr["hld_vac_info"].ToString() + "<br /><br />";
            pnlclndrInfo.Controls.Add(lblInfo);
        }
    }
    protected void ddlReligious_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            HolidaysVacations.HolidaysVacations hv = new HolidaysVacations.HolidaysVacations();
            DataSet ds = hv.GetReligious();
            ddlReligious.DataSource = ds;
            ddlReligious.DataValueField = "rlg_id";
            ddlReligious.DataTextField = "rlg_name";
            ddlReligious.DataBind();

            ddlReligious.Items.Add(new ListItem("-כל הדתות-", "-1"));
            ddlReligious.SelectedValue = "-1";
        }
    }
}