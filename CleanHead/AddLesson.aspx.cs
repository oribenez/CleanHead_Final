using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class AddLesson : System.Web.UI.Page
{
    static Hashtable htAllSelectedStudents = Selections();

    /// <summary>
    /// מהווה מערך של כיתות שבתוכם יש תלמידים
    /// </summary>
    static Hashtable Selections()
    {
        Hashtable htAllSelectedStudents = new Hashtable();

        DataSet dsRooms = ch_roomsSvc.GetStuPrimaryClasses();
        foreach (DataRow dr in dsRooms.Tables[0].Rows)
	    {
            htAllSelectedStudents.Add(dr["rm_id"].ToString(), new Hashtable());
	    }
        return htAllSelectedStudents;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["lvl_id"]) != 3) {
            Response.Redirect("~/Errors/403Error.aspx");
        }
        if (!IsPostBack)
        {
            //DDL cities
            DataSet dsProf = ch_professionsSvc.GetProfessions();

            ddlProf.DataSource = dsProf;
            ddlProf.DataValueField = "pro_id";
            ddlProf.DataTextField = "pro_name";
            ddlProf.DataBind();

            ddlProf.Items.Add("-בחר מקצוע-");
            ddlProf.SelectedIndex = ddlProf.Items.Count - 1;

            //DDL rooms
            DataSet dsRooms = ch_roomsSvc.GetStuPrimaryClasses(Convert.ToInt32(Session["sc_id"]));

            ddlClasses.DataSource = dsRooms;
            ddlClasses.DataValueField = "rm_id";
            ddlClasses.DataTextField = "rm_name";
            ddlClasses.DataBind();

            ddlClasses.Items.Add("-בחר כיתת תלמיד-");
            ddlClasses.SelectedIndex = ddlClasses.Items.Count - 1;

            ckliTeachers.Items.Clear();
            ckliStudentsByClass.Items.Clear();
            ddlClasses.SelectedIndex = ddlClasses.Items.Count - 1;

            pnlStudents.Visible = false;
            pnlTeachers.Visible = false;
            ckliTeachers.Visible = false;
            ddlClasses.Visible = false;
            ckliStudentsByClass.Visible = false;
            update_ckliStudentsByClass.Update();
            update_ckliTeachers.Update();
            update_pnlStudents.Update();
            update_pnlTeachers.Update();
        }
    }
    protected void ddlLayers_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //DDL layers
            string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(Session["sc_id"]));
            for (int i = 0; i < layers.Length; i++)
            {
                ddlLayers.Items.Add(layers[i]);
            }

            ddlLayers.Items.Add(new ListItem("-בחר שכבה-", "-1"));
            ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;
        }
    }
    protected void ddlProf_SelectedIndexChanged(object sender, EventArgs e)
    {
        //check if there is a selected item
        if (ddlProf != null)
        {
            if (ddlProf.SelectedIndex != ddlProf.Items.Count - 1)
            {
                pnlStudents.Visible = true;
                pnlTeachers.Visible = true;
                ckliTeachers.Visible = true;
                ddlClasses.Visible = true;
                ckliStudentsByClass.Visible = true;

                ckliTeachers.Items.Clear();

                update_ckliStudentsByClass.Update();
                update_ckliTeachers.Update();
                update_pnlStudents.Update();
                update_pnlTeachers.Update();

                int sc_id = Convert.ToInt32(Session["sc_id"]);
                int pro_id = Convert.ToInt32(ddlProf.SelectedValue);
                DataSet dsTeachersProfessions = ch_teachers_professionsSvc.GetTeachersByProAndSchool(sc_id, pro_id); // לא נכנס ללולאה באג

                for (int i = 0; i < dsTeachersProfessions.Tables[0].Rows.Count; i++)
                {
                    int usr_id = Convert.ToInt32(dsTeachersProfessions.Tables[0].Rows[i]["usr_id"].ToString());
                    ListItem li = new ListItem();
                    li.Value = usr_id.ToString();
                    li.Text = ch_usersSvc.GetUserFullname(usr_id);

                    ckliTeachers.Items.Add(li);
                }
            }
            else
            {
                pnlStudents.Visible = false;
                pnlTeachers.Visible = false;
                ckliTeachers.Visible = false;
                ddlClasses.Visible = false;
                ckliStudentsByClass.Visible = false;

                update_ckliStudentsByClass.Update();
                update_ckliTeachers.Update();
                update_pnlStudents.Update();
                update_pnlTeachers.Update();
            }
        }
        
    }
    protected void ckliTeachers_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblEmpty_tch.Visible = (ckliTeachers.SelectedIndex != -1) ? false : true;

        foreach (ListItem li in ckliTeachers.Items)
        {
            if (li.Selected)
            {
                Panel pnlName = new Panel();
                pnlName.ID = "pnlName_" + li.Value;
                pnlName.CssClass = "pnlName";
                pnlTeachers.Controls.Add(pnlName);

                Label lblName = new Label();
                lblName.Text = li.Text;
                pnlName.Controls.Add(lblName);
            }
        }
    }
    protected void ddlClasses_SelectedIndexChanged(object sender, EventArgs e)
    {
        //check if there is a selected item
        if (ddlClasses != null)
        {
            if (ddlClasses.SelectedIndex != ddlClasses.Items.Count - 1)
            {
                ckliStudentsByClass.Items.Clear();

                DataSet dsStudentsByClass = ch_studentsSvc.GetStudents(Convert.ToInt32(ddlClasses.SelectedValue));
                for (int i = 0; i < dsStudentsByClass.Tables[0].Rows.Count; i++)
                {
                    int usr_id = Convert.ToInt32(dsStudentsByClass.Tables[0].Rows[i]["usr_id"].ToString());
                    ListItem li = new ListItem();
                    li.Value = usr_id.ToString();
                    li.Text = ch_usersSvc.GetUserFullname(usr_id);

                    ckliStudentsByClass.Items.Add(li);
                } update_ckliStudentsByClass.Update();
                
                //reselect the selected students
                foreach (ListItem li in ckliStudentsByClass.Items)
                {
                    if (((Hashtable)htAllSelectedStudents[ddlClasses.SelectedValue]).ContainsKey(li.Value))
                    {
                        li.Selected = true;
                    }
                }
                
            }
            
        }
    }
    protected void ckliStudentsByClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblEmpty_stu.Visible = (ckliStudentsByClass.SelectedIndex != -1) ? false : true;

        ((Hashtable)htAllSelectedStudents[ddlClasses.SelectedValue]).Clear();
        //insert to hashtable
        foreach (ListItem li in ckliStudentsByClass.Items)
        {

            if (li.Selected)
            {
                ((Hashtable)htAllSelectedStudents[ddlClasses.SelectedValue]).Add(li.Value, li.Text);
            }
        }

        //show
        foreach (Hashtable htItem in htAllSelectedStudents.Values)
        {
            foreach (string student in htItem.Values)
	        {
                Panel pnlName = new Panel();
                pnlName.CssClass = "pnlName";
                pnlStudents.Controls.Add(pnlName);

                Label lblName = new Label();
                lblName.Text = student;
                pnlName.Controls.Add(lblName);
	        }
            
        }
        update_pnlStudents.Update();
    }
    protected void btnAddLesson_Click(object sender, EventArgs e)
    {
        
        if (ValidatePage())
        {
            //if teacher is checked
            bool selected = false;
            foreach (ListItem li in ckliTeachers.Items)
	        {
		        if (li.Selected)
	            {
		            selected = true;
	            }
	        }

            if (selected)
            {
                int selected_prof = Convert.ToInt32(ddlProf.SelectedValue);
                string selected_layer = ddlLayers.SelectedItem.Text;
                string les_name = txtLes_name.Text.Trim();

                ch_lessons newLes = new ch_lessons();
                newLes.pro_Id = selected_prof;
                newLes.les_Name = les_name + " - " + selected_layer;

                ch_lessonsSvc.AddLesson(newLes);

                int lastLesson_id = ch_lessonsSvc.GetLastInsertId();

                //add teachers
                foreach (ListItem li in ckliTeachers.Items)
                {
                    if (li.Selected)
                    {
                        ch_teachers_lessons newTchLes = new ch_teachers_lessons();
                        newTchLes.les_Id = lastLesson_id;
                        newTchLes.usr_Id = Convert.ToInt32(li.Value);

                        ch_teachers_lessonsSvc.AddTeachersLesson(newTchLes);
                    }
                }

                //add students
                foreach (Hashtable htItem in htAllSelectedStudents.Values)
                {
                    foreach (string usr_id in htItem.Keys)
	                {
                        ch_students_lessons newStuLes = new ch_students_lessons();
                        newStuLes.les_Id = lastLesson_id;
                        newStuLes.usr_Id = Convert.ToInt32(usr_id);

                        ch_students_lessonsSvc.AddStudentLesson(newStuLes);
	                }
                }
                Response.Redirect("LessonsData.aspx");
            }
            else
            {
                lblError.Text = "לא נבחרו מורים";
            }
        }

        foreach (ListItem li in ckliTeachers.Items) {
            if (li.Selected) {
                Panel pnlName = new Panel();
                pnlName.ID = "pnlName_" + li.Value;
                pnlName.CssClass = "pnlName";
                pnlTeachers.Controls.Add(pnlName);

                Label lblName = new Label();
                lblName.Text = li.Text;
                pnlName.Controls.Add(lblName);
            }
        }

        //show
        foreach (Hashtable htItem in htAllSelectedStudents.Values) {
            foreach (string student in htItem.Values) {
                Panel pnlName = new Panel();
                pnlName.CssClass = "pnlName";
                pnlStudents.Controls.Add(pnlName);

                Label lblName = new Label();
                lblName.Text = student;
                pnlName.Controls.Add(lblName);
            }

        }
        update_pnlStudents.Update();
    }
    protected bool ValidatePage()
    {
        //students check
        foreach (Hashtable htItem in htAllSelectedStudents.Values)
        {
            if (htItem.Count != 0)
            {
                return true;
            }
        }

        lblError.Text = "לא נבחרו תלמידים";
        return false;
    }
}