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
        if (Request.QueryString["les_id"] == null)
            Response.Redirect("LessonsData.aspx");
        else
        {
            if (Request.QueryString["les_id"].ToString() == "")
                Response.Redirect("LessonsData.aspx");
            else if (!ch_lessonsSvc.IsExist(Convert.ToInt32(Request.QueryString["les_id"].ToString())))
                Response.Redirect("LessonsData.aspx");
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

            //DDL layers
            string[] layers = ch_roomsSvc.GetClassesLayers(Convert.ToInt32(Session["sc_id"]));
            for (int i = 0; i < layers.Length; i++)
            {
                ddlLayers.Items.Add(layers[i]);
            }

            ddlLayers.Items.Add("-בחר שכבה-");

            ClearControls();

            //----reenter Choices----
            int les_id = Convert.ToInt32(Request.QueryString["les_id"].ToString());

            DataRow drLesson = ch_lessonsSvc.GetLesson(les_id);

            ddlProf.SelectedValue = drLesson["pro_id"].ToString();//מתמטיקה
            ddlLayers.SelectedItem.Text = drLesson["les_name"].ToString().Substring(drLesson["les_name"].ToString().LastIndexOf('-') + 2, drLesson["les_name"].ToString().Length - drLesson["les_name"].ToString().LastIndexOf('-') - 2);// יב
            txtLes_name.Text = drLesson["les_name"].ToString().Substring(0, drLesson["les_name"].ToString().LastIndexOf('-') - 1);// שם השיעור

            DataSet teachers = ch_teachers_lessonsSvc.GetTeachersLesson(les_id);
            lblEmpty_tch.Visible = (teachers.Tables[0].Rows.Count > 0) ? false : true;
            //show
            foreach (DataRow dr in teachers.Tables[0].Rows)
            {
                Panel pnlName = new Panel();
                pnlName.ID = "pnlName_" + dr["usr_id"].ToString();
                pnlName.CssClass = "pnlName";
                pnlTeachers.Controls.Add(pnlName);

                Label lblName = new Label();
                lblName.Text = dr["usr_fullname"].ToString();
                pnlName.Controls.Add(lblName);
            }

            //checkboxes
            DataSet dsTeachersProfessions = ch_teachers_professionsSvc.GetTeachersByPro(Convert.ToInt32(ddlProf.SelectedValue));
            for (int i = 0; i < dsTeachersProfessions.Tables[0].Rows.Count; i++)
            {
                int usr_id = Convert.ToInt32(dsTeachersProfessions.Tables[0].Rows[i]["tch_pro.usr_id"].ToString());
                ListItem li = new ListItem();
                li.Value = usr_id.ToString();
                li.Text = ch_usersSvc.GetUserFullname(usr_id);

                foreach (DataRow dr in teachers.Tables[0].Rows)
	            {
                    if (usr_id == Convert.ToInt32(dr["usr_id"].ToString()))
                    {
                        li.Selected = true;
                    }
	            }
                
                
                ckliTeachers.Items.Add(li);
            }

            

            

            DataSet students = ch_students_lessonsSvc.GetStudentsLesson(les_id);
            foreach (DataRow dr in students.Tables[0].Rows)
            {
                ((Hashtable)htAllSelectedStudents[dr["rm_id"].ToString()]).Add(dr["usr_id"].ToString(), dr["usr_fullname"].ToString());
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

            update_ckliStudentsByClass.Update();
            update_ckliTeachers.Update();
            update_pnlStudents.Update();
            update_pnlTeachers.Update();
        }
    }
    protected void ClearControls()
    {
        //clear students hashtable
        foreach (Hashtable htItem in htAllSelectedStudents.Values)
        {
            ((Hashtable)htItem).Clear();
        }
    }
    protected void ddlLayers_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    //DDL layers
        //    string[] layers = ch_roomsSvc.GetClassesLayers();
        //    for (int i = 0; i < layers.Length; i++)
        //    {
        //        ddlLayers.Items.Add(layers[i]);
        //    }

        //    ddlLayers.Items.Add("-בחר שכבה-");
        //    ddlLayers.SelectedIndex = ddlLayers.Items.Count - 1;
        //}
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

                update_ckliStudentsByClass.Update();
                update_ckliTeachers.Update();
                update_pnlStudents.Update();
                update_pnlTeachers.Update();

                ckliTeachers.Items.Clear();
                ddlClasses.SelectedIndex = ddlClasses.Items.Count - 1;
                ckliStudentsByClass.Items.Clear();

                DataSet dsTeachersProfessions = ch_teachers_professionsSvc.GetTeachersByPro(Convert.ToInt32(ddlProf.SelectedValue));
                for (int i = 0; i < dsTeachersProfessions.Tables[0].Rows.Count; i++)
                {
                    int usr_id = Convert.ToInt32(dsTeachersProfessions.Tables[0].Rows[i]["tch_pro.usr_id"].ToString());
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
                } 
                update_ckliStudentsByClass.Update();
                
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
    protected void btnUpdateLesson_Click(object sender, EventArgs e)
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

                ch_lessonsSvc.UpdateLesson(Convert.ToInt32(Request.QueryString["les_id"].ToString()), newLes);

                //update teachers
                ch_teachers_lessonsSvc.DeleteAllTeachersLesson(Convert.ToInt32(Request.QueryString["les_id"].ToString()));
                foreach (ListItem li in ckliTeachers.Items)
                {
                    if (li.Selected)
                    {
                        ch_teachers_lessons newTchLes = new ch_teachers_lessons();
                        newTchLes.les_Id = Convert.ToInt32(Request.QueryString["les_id"].ToString());
                        newTchLes.usr_Id = Convert.ToInt32(li.Value);

                        ch_teachers_lessonsSvc.AddTeachersLesson(newTchLes);
                    }
                }

                //update students
                ch_students_lessonsSvc.DeleteAllStudentsLesson(Convert.ToInt32(Request.QueryString["les_id"].ToString()));

                foreach (Hashtable htItem in htAllSelectedStudents.Values)
                {
                    foreach (string usr_id in htItem.Keys)
	                {
                        ch_students_lessons newStuLes = new ch_students_lessons();
                        newStuLes.les_Id = Convert.ToInt32(Request.QueryString["les_id"].ToString());
                        newStuLes.usr_Id = Convert.ToInt32(usr_id);

                        ch_students_lessonsSvc.AddStudentLesson(newStuLes);
	                }
                }
                Response.Redirect("LessonsData.aspx");
            }
        }
        //----reenter Choices----
        int les_id = Convert.ToInt32(Request.QueryString["les_id"].ToString());

        DataRow drLesson = ch_lessonsSvc.GetLesson(les_id);

        ddlProf.SelectedValue = drLesson["pro_id"].ToString();//מתמטיקה
        ddlLayers.SelectedItem.Text = drLesson["les_name"].ToString().Substring(drLesson["les_name"].ToString().LastIndexOf('-') + 2, drLesson["les_name"].ToString().Length - drLesson["les_name"].ToString().LastIndexOf('-') - 2);// יב
        txtLes_name.Text = drLesson["les_name"].ToString().Substring(0, drLesson["les_name"].ToString().LastIndexOf('-') - 1);// שם השיעור

        DataSet teachers = ch_teachers_lessonsSvc.GetTeachersLesson(les_id);
        lblEmpty_tch.Visible = (teachers.Tables[0].Rows.Count > 0) ? false : true;
        //show
        foreach (DataRow dr in teachers.Tables[0].Rows)
        {
            Panel pnlName = new Panel();
            pnlName.ID = "pnlName_" + dr["usr_id"].ToString();
            pnlName.CssClass = "pnlName";
            pnlTeachers.Controls.Add(pnlName);

            Label lblName = new Label();
            lblName.Text = dr["usr_fullname"].ToString();
            pnlName.Controls.Add(lblName);
        }

        //checkboxes
        ckliTeachers.Items.Clear();
        DataSet dsTeachersProfessions = ch_teachers_professionsSvc.GetTeachersByPro(Convert.ToInt32(ddlProf.SelectedValue));
        for (int i = 0; i < dsTeachersProfessions.Tables[0].Rows.Count; i++)
        {
            int usr_id = Convert.ToInt32(dsTeachersProfessions.Tables[0].Rows[i]["tch_pro.usr_id"].ToString());
            ListItem li = new ListItem();
            li.Value = usr_id.ToString();
            li.Text = ch_usersSvc.GetUserFullname(usr_id);

            foreach (DataRow dr in teachers.Tables[0].Rows)
            {
                if (usr_id == Convert.ToInt32(dr["usr_id"].ToString()))
                {
                    li.Selected = true;
                }
            }


            ckliTeachers.Items.Add(li);
        }

        foreach (Hashtable htItem in htAllSelectedStudents.Values)
        {
            ((Hashtable)htItem).Clear();
        }

        DataSet students = ch_students_lessonsSvc.GetStudentsLesson(les_id);
        foreach (DataRow dr in students.Tables[0].Rows)
        {
            ((Hashtable)htAllSelectedStudents[dr["rm_id"].ToString()]).Add(dr["usr_id"].ToString(), dr["usr_fullname"].ToString());
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

        update_ckliStudentsByClass.Update();
        update_ckliTeachers.Update();
        update_pnlStudents.Update();
        update_pnlTeachers.Update();
    }
    static bool ValidatePage()
    {
        //students check
        foreach (Hashtable htItem in htAllSelectedStudents.Values)
        {
            if (htItem.Count > 0)
            {
                return true;
            }
        }

        return false;
    }
}