using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack) {
            int usr_id = Convert.ToInt32(Session["usr_id"]);
            lblBehavior.Text = ch_students_behaviorsSvc.GetStuBehaviorsPointsByMonth(usr_id).ToString();//Behavior
            lblHomeworks.Text = ch_homeworkSvc.GetNumStuHomeworksToSubmit(usr_id).ToString();//Homewwork
        }


        //SURVEY
        DataRow dr_srv_item = SurveySvc.GetSurvey(1); // srv_id = 1 cause it's the default survey
        if (!IsPostBack) {
            if (SurveySvc.IsUsrDidSurvey(Convert.ToInt32(Session["usr_id"]))) {
                pnlSurveyFill.Visible = false;
                pnlUpdateSurvey.Visible = false;
                pnlNewSurvey.Visible = false;
                pnlSurveyAfterFill.Visible = true;

                
                if (dr_srv_item["srv_question"].ToString() == "") {
                }

                lblQuestionAfter.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

                BindStatistics();
            }
            else
	        {
                if (dr_srv_item["srv_question"].ToString() == "") {
                }
                else {
                    //FILL MODE
                    lblQuestion.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

                    for (int i = 1; i <= 4; i++) { // all four answers
                        if (dr_srv_item["srv_ans" + i].ToString() != "") {
                            ListItem li = new ListItem();
                            li.Text = dr_srv_item["srv_ans" + i].ToString();
                            li.Value = i.ToString();
                            rblAnswers.Items.Add(li);
                        }
                    }
                }
            }

            
        }
        lblQuestionAfter.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

        //bind survey
        //EDIT MODE
        txtQuestion.Text = dr_srv_item["srv_question"].ToString(); // bind txtQuestion

        for (int i = 1; i <= 4; i++) { // all four answers
            if (dr_srv_item["srv_ans" + i].ToString() != "") {
                Panel pnl = new Panel();
                pnl.CssClass = "form-group";
                pnl.ID = "formgroupUpt" + i;

                TextBox txt = new TextBox();
                txt.Text = dr_srv_item["srv_ans" + i].ToString();
                txt.ID = "txtAns" + i.ToString();
                txt.CssClass = "material";
                txt.Attributes.Add("required", "required");
                pnl.Controls.Add(txt);

                LiteralControl lc = new LiteralControl();
                lc.Text += "<span class='form-highlight'></span><span class='form-bar'></span>";
                pnl.Controls.Add(lc);

                Label lbl = new Label();
                lbl.AssociatedControlID = "txtAns" + i;
                lbl.CssClass = "form-label";
                lbl.Text = "תשובה " + i;
                pnl.Controls.Add(lbl);

                pnlTxtAnswers.Controls.Add(pnl);
            }
        }



        //bind survey
        //NEW SURVEY MODE

        for (int i = 1; i <= 4; i++) { // all four answers
            if (dr_srv_item["srv_ans" + i].ToString() != "") {
                Panel pnl = new Panel();
                pnl.CssClass = "form-group";
                pnl.ID = "formgroupNew" + i;

                TextBox txt = new TextBox();
                txt.ID = "txtNewAns" + i.ToString();
                txt.CssClass = "material";
                txt.Attributes.Add("required", "required");
                pnl.Controls.Add(txt);

                LiteralControl lc = new LiteralControl();
                lc.Text += "<span class='form-highlight'></span><span class='form-bar'></span>";
                pnl.Controls.Add(lc);

                Label lbl = new Label();
                lbl.AssociatedControlID = "txtNewAns" + i;
                lbl.CssClass = "form-label";
                lbl.Text = "תשובה " + i;
                pnl.Controls.Add(lbl);

                pnlTxtNewAnswers.Controls.Add(pnl);
            }
        }
    }

    protected void BindStatistics() {
        int numVotes = 0;
        int[] arrVotes = new int[5];
        string[] arrColors = { "", "#ef5350", "#FFCA28", "#26A69A", "#AB47BC" };

        for (int i = 1; i <= 4; i++) {
            if (Application["srv_ans" + i + "_score"].ToString() == "") {
                continue;
            }
            string[] ansScore = Application["srv_ans" + i + "_score"].ToString().Split(',');
            numVotes += ansScore.Length;
            arrVotes[i] = ansScore.Length;
        }
        
        DataRow dr_srv_item = SurveySvc.GetSurvey(1); // srv_id = 1 cause it's the default survey
        lblQuestion.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion
        for (int i = 1; i <= 4; i++) {
            if (dr_srv_item["srv_ans" + i].ToString() != "") {
                Label lbl = new Label();
                lbl.Text = dr_srv_item["srv_ans" + i].ToString();
                lbl.Attributes.Add("style", "margin: 0 5px 0 0;");
                pnlStatsAnswers.Controls.Add(lbl);

                Panel pnl = new Panel();
                //pnl.Height = 10;
                double percents = ((double)arrVotes[i] / (double)numVotes) * 100;
                percents = Math.Round(percents,2);
                pnl.Attributes.Add("style", "width:" + percents + "%; background: " + arrColors[i] + "; margin: 0 0 10px 0;font-size:13px;font-weight: bold;color: #fff;text-align: left;padding: 0 0 0 5px;");
                pnl.Controls.Add(new LiteralControl(percents.ToString() + "%"));
                pnlStatsAnswers.Controls.Add(pnl);
            }
        }
        
    }

    
    protected void btnSendAns_Click(object sender, EventArgs e) {
        pnlUpdateSurvey.Visible = false;
        pnlSurveyFill.Visible = false;
        pnlSurveyAfterFill.Visible = true;
        pnlNewSurvey.Visible = false;

        if (Application["srv_ans" + rblAnswers.SelectedValue + "_score"].ToString() == "") {
            Application.Lock();
            Application["srv_ans" + rblAnswers.SelectedValue + "_score"] = Session["usr_id"];
            Application.UnLock();
        }
        else {
            Application.Lock();
            Application["srv_ans" + rblAnswers.SelectedValue + "_score"] = "," + Session["usr_id"];
            Application.UnLock();
        }

        BindStatistics();
    }
    protected void btnUpdateSrv_Click(object sender, EventArgs e) {
        Panel formgroupUpt1 = (Panel)pnlTxtAnswers.FindControl("formgroupUpt1");
        Panel formgroupUpt2 = (Panel)pnlTxtAnswers.FindControl("formgroupUpt2");
        Panel formgroupUpt3 = (Panel)pnlTxtAnswers.FindControl("formgroupUpt3");
        Panel formgroupUpt4 = (Panel)pnlTxtAnswers.FindControl("formgroupUpt4");
        TextBox txt1 = (TextBox)formgroupUpt1.FindControl("txtAns1");
        TextBox txt2 = (TextBox)formgroupUpt2.FindControl("txtAns2");
        TextBox txt3 = (TextBox)formgroupUpt3.FindControl("txtAns3");
        TextBox txt4 = (TextBox)formgroupUpt4.FindControl("txtAns4");
        Survey srv1 = new Survey(txtQuestion.Text, txt1.Text, txt2.Text, txt3.Text, txt4.Text);
        SurveySvc.UpdateSurvey(srv1);

        //bind
        if (SurveySvc.IsUsrDidSurvey(Convert.ToInt32(Session["usr_id"]))) {
            pnlUpdateSurvey.Visible = false;
            pnlSurveyFill.Visible = false;
            pnlSurveyAfterFill.Visible = true;

            DataRow dr_srv_item = SurveySvc.GetSurvey(1); // srv_id = 1 cause it's the default survey
            if (dr_srv_item["srv_question"].ToString() == "") {
                pnlSurvey.Visible = false;
            }

            lblQuestionAfter.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

            BindStatistics();
        }
        else {
            DataRow dr_srv_item = SurveySvc.GetSurvey(1); // srv_id = 1 cause it's the default survey
            if (dr_srv_item["srv_question"].ToString() == "") {
                pnlSurvey.Visible = false;
            }
            else {
                //FILL MODE
                lblQuestion.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

                for (int i = 1; i <= 4; i++) { // all four answers
                    if (dr_srv_item["srv_ans" + i].ToString() != "") {
                        ListItem li = new ListItem();
                        li.Text = dr_srv_item["srv_ans" + i].ToString();
                        li.Value = i.ToString();
                        rblAnswers.Items.Add(li);
                    }
                }
            }
        }
    }
    protected void imgbtnEditSurvey_Click(object sender, ImageClickEventArgs e) {
        pnlSurveyFill.Visible = false;
        pnlUpdateSurvey.Visible = true;
        pnlNewSurvey.Visible = false;
        pnlSurveyAfterFill.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e) {
        if (SurveySvc.IsUsrDidSurvey(Convert.ToInt32(Session["usr_id"]))) {
            pnlSurveyFill.Visible = false;
            pnlUpdateSurvey.Visible = false;
            pnlNewSurvey.Visible = false;
            pnlSurveyAfterFill.Visible = true;

            BindStatistics();
        }
        else {
            pnlSurveyFill.Visible = true;
            pnlUpdateSurvey.Visible = false;
            pnlNewSurvey.Visible = false;
            pnlSurveyAfterFill.Visible = false;

            //FILL MODE
            DataRow dr_srv_item = SurveySvc.GetSurvey(1); // srv_id = 1 cause it's the default survey
            lblQuestion.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

            for (int i = 1; i <= 4; i++) { // all four answers
                if (dr_srv_item["srv_ans" + i].ToString() != "") {
                    ListItem li = new ListItem();
                    li.Text = dr_srv_item["srv_ans" + i].ToString();
                    li.Value = i.ToString();
                    rblAnswers.Items.Add(li);
                }
            }
        }
    }
    protected void imgbtnNewSurvey_Click(object sender, ImageClickEventArgs e) {
        pnlSurveyFill.Visible = false;
        pnlUpdateSurvey.Visible = false;
        pnlNewSurvey.Visible = true;
        pnlSurveyAfterFill.Visible = false;
    }
    protected void btnCancelNewSurvey_Click(object sender, EventArgs e) {
        if (SurveySvc.IsUsrDidSurvey(Convert.ToInt32(Session["usr_id"]))) {
            pnlSurveyFill.Visible = false;
            pnlUpdateSurvey.Visible = false;
            pnlNewSurvey.Visible = false;
            pnlSurveyAfterFill.Visible = true;

            BindStatistics();
        }
        else {
            pnlSurveyFill.Visible = true;
            pnlUpdateSurvey.Visible = false;
            pnlNewSurvey.Visible = false;
            pnlSurveyAfterFill.Visible = false;

            //FILL MODE
            DataRow dr_srv_item = SurveySvc.GetSurvey(1); // srv_id = 1 cause it's the default survey
            lblQuestion.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

            for (int i = 1; i <= 4; i++) { // all four answers
                if (dr_srv_item["srv_ans" + i].ToString() != "") {
                    ListItem li = new ListItem();
                    li.Text = dr_srv_item["srv_ans" + i].ToString();
                    li.Value = i.ToString();
                    rblAnswers.Items.Add(li);
                }
            }
        }
    }
    protected void btnMakeSrv_Click(object sender, EventArgs e) {
        Panel formgroupNew1 = (Panel)pnlTxtAnswers.FindControl("formgroupNew1");
        Panel formgroupNew2 = (Panel)pnlTxtAnswers.FindControl("formgroupNew2");
        Panel formgroupNew3 = (Panel)pnlTxtAnswers.FindControl("formgroupNew3");
        Panel formgroupNew4 = (Panel)pnlTxtAnswers.FindControl("formgroupNew4");
        TextBox txt1 = (TextBox)formgroupNew1.FindControl("txtNewAns1");
        TextBox txt2 = (TextBox)formgroupNew2.FindControl("txtNewAns2");
        TextBox txt3 = (TextBox)formgroupNew3.FindControl("txtNewAns3");
        TextBox txt4 = (TextBox)formgroupNew4.FindControl("txtNewAns4");

        if (txtNewQuestion.Text == "") {
            return;
        }
        if (txt1.Text == "") {
            lblErr_new.Text = "תשובה 1 ריק";
            return;
        }
        if (txt2.Text == "") {
            lblErr_new.Text = "תשובה 2 ריק";
            return;
        }
        

        SurveySvc.ResetSurvey();
        Survey srv1 = new Survey(txtNewQuestion.Text, txt1.Text, txt2.Text, txt3.Text, txt4.Text);
        SurveySvc.UpdateSurvey(srv1);
        lblErr_new.Text = "";


        //bind
        rblAnswers.Items.Clear();
        if (SurveySvc.IsUsrDidSurvey(Convert.ToInt32(Session["usr_id"]))) {
            pnlUpdateSurvey.Visible = false;
            pnlSurveyFill.Visible = false;
            pnlSurveyAfterFill.Visible = true;

            DataRow dr_srv_item = SurveySvc.GetSurvey(1); // srv_id = 1 cause it's the default survey
            if (dr_srv_item["srv_question"].ToString() == "") {
                pnlSurvey.Visible = false;
            }

            lblQuestionAfter.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

            BindStatistics();
        }
        else {
            DataRow dr_srv_item = SurveySvc.GetSurvey(1); // srv_id = 1 cause it's the default survey
            if (dr_srv_item["srv_question"].ToString() == "") {
                pnlSurvey.Visible = false;
            }
            else {
                //FILL MODE
                lblQuestion.Text = dr_srv_item["srv_question"].ToString(); // bind lblQuestion

                for (int i = 1; i <= 4; i++) { // all four answers
                    if (dr_srv_item["srv_ans" + i].ToString() != "") {
                        ListItem li = new ListItem();
                        li.Text = dr_srv_item["srv_ans" + i].ToString();
                        li.Value = i.ToString();
                        rblAnswers.Items.Add(li);
                    }
                }
            }
        }


        pnlSurveyFill.Visible = true;
        pnlUpdateSurvey.Visible = false;
        pnlNewSurvey.Visible = false;
        pnlSurveyAfterFill.Visible = false;
        BindStatistics();
    }
}