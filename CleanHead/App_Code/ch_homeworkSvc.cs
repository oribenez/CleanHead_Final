using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml;
using System.IO;
using System.Web.Hosting;

/// <summary>
/// Summary description for ch_homeworkSvc
/// </summary>
public class ch_homeworkSvc
{
    /// <returns>
    /// The max id hw_id from DataSet
    /// </returns>
    public static int GetMaxId() {
        DataSet ds_hw_xml = new DataSet();
        ds_hw_xml.ReadXml(HttpContext.Current.Server.MapPath("App_Data/homework.xml"));

        int max = 0;
        foreach (DataRow dr in ds_hw_xml.Tables[0].Rows) {
            int hw_id = Convert.ToInt32(dr["hw_id"]);
            if (hw_id > max) {
                max = hw_id;
            }
        }
        return max;
    }
    /// <summary>
    /// Check if hw_id is exist in homework
    /// </summary>
    /// <param name="hw_id">homework identity</param>
    /// <returns>num of existing hw_id in DataSet</returns>
    public static int IsExist(int hw_id) {
        DataSet ds_hw_xml = new DataSet();
        ds_hw_xml.ReadXml(HttpContext.Current.Server.MapPath("App_Data/homework.xml"));

        int count = 0;

        foreach (DataRow dr_hw_xml in ds_hw_xml.Tables[0].Rows) {
            if (Convert.ToInt32(dr_hw_xml["hw_id"]) == hw_id) {
                count++;
            }
        }

        return count;
    }
    /// <summary>
    /// Add homework to the database
    /// </summary>
    /// <param name="hw">the new homework to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddHomework(ch_homework hw) {
        try {
            //file name
            string filename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"App_Data\homework.xml");

            //create new instance of XmlDocument
            XmlDocument doc = new XmlDocument();

            //load from file
            doc.Load(filename);

            //create node and add value
            XmlNode node = doc.CreateNode(XmlNodeType.Element, "homework_item", null);

            //create hw_id node
            XmlNode node_hw_id = doc.CreateElement("hw_id");
            //add value for it
            node_hw_id.InnerText = GetMaxId() + 1 + "";

            //create les_id node
            XmlNode node_les_id = doc.CreateElement("les_id");
            node_les_id.InnerText = hw.les_Id.ToString();

            //create hw_pubdate node
            XmlNode node_hw_pubdate = doc.CreateElement("hw_pubdate");
            node_hw_pubdate.InnerText = DateTime.Now.ToShortDateString();

            //create hw_deadlinedate node
            XmlNode node_hw_deadlinedate = doc.CreateElement("hw_deadlinedate");
            node_hw_deadlinedate.InnerText = hw.hw_Deadlinedate;

            //create hr_id node
            XmlNode node_hr_id = doc.CreateElement("hr_id");
            node_hr_id.InnerText = hw.hr_Id.ToString();

            //create hw_txt node
            XmlNode node_hw_txt = doc.CreateElement("hw_txt");
            node_hw_txt.InnerText = hw.hw_Txt;

            //create hw_editdate node
            XmlNode node_hw_editdate = doc.CreateElement("hw_editdate");
            node_hw_editdate.InnerText = "";

            //add to parent node
            node.AppendChild(node_hw_id);
            node.AppendChild(node_les_id);
            node.AppendChild(node_hw_pubdate);
            node.AppendChild(node_hw_deadlinedate);
            node.AppendChild(node_hr_id);
            node.AppendChild(node_hw_txt);
            node.AppendChild(node_hw_editdate);

            //add to elements collection
            doc.DocumentElement.AppendChild(node);

            //save back
            doc.Save(filename);
            return "";
        }
        catch (Exception) {
            return "אירעה תקלה בעת הכנסת הנתונים לשרת.\n נסה שוב מאוחר יותר";
        }
    }
    /// <returns>DataSet of all homeworks.</returns>
    public static DataSet GetHomeworks() {
        DataSet ds_hw_xml = new DataSet();
        ds_hw_xml.ReadXml(HttpContext.Current.Server.MapPath("App_Data/homework.xml"));

        //add new columns
        ds_hw_xml.Tables[0].Columns.Add("pro_name");
        ds_hw_xml.Tables[0].Columns.Add("les_name");
        ds_hw_xml.Tables[0].Columns.Add("hr_name");
        ds_hw_xml.Tables[0].Columns.Add("hw_timeleft");

        foreach (DataRow dr_hw_xml in ds_hw_xml.Tables[0].Rows) {
            DataRow dr_les = ch_lessonsSvc.GetLesson(Convert.ToInt32(dr_hw_xml["les_id"]));

            //pro_name
            DataRow dr_pro = ch_professionsSvc.GetProfession(Convert.ToInt32(dr_les["pro_id"]));
            dr_hw_xml["pro_name"] = dr_pro["pro_name"];

            //les_name
            dr_hw_xml["les_name"] = dr_les["les_name"];

            //hr_name
            DataRow dr_hr = ch_hoursSvc.GetHour(Convert.ToInt32(dr_hw_xml["hr_id"]));
            dr_hw_xml["hr_name"] = dr_hr["hr_name"];

            //timeleft
            DateTime dtDeadline = DateTime.ParseExact(dr_hw_xml["hw_deadlinedate"].ToString() + " " + Convert.ToDateTime(dr_hr["hr_start_time"].ToString()).ToString("HH:mm"), "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            TimeSpan calcTimeLeft = dtDeadline - DateTime.Now;
            if (calcTimeLeft.TotalMinutes < 0) {
                dr_hw_xml["hw_timeleft"] = "נגמר הזמן!";
            }
            else {
                dr_hw_xml["hw_timeleft"] = "להגשה בעוד " + calcTimeLeft.ToString(@"%d") + " ימים ו-" + calcTimeLeft.ToString(@"%h") + " שעות";
            }

        }

        return ds_hw_xml;
    }
    /// <param name="sc_id">school id</param>
    /// <returns>DataSet of homeworks. filtered by school id.</returns>
    public static DataSet GetHomeworks(int sc_id) {
        DataSet ds_hw_xml = new DataSet();
        ds_hw_xml.ReadXml(HttpContext.Current.Server.MapPath("App_Data/homework.xml"));

        //add new columns
        ds_hw_xml.Tables[0].Columns.Add("pro_name");
        ds_hw_xml.Tables[0].Columns.Add("les_name");
        ds_hw_xml.Tables[0].Columns.Add("hr_name");
        ds_hw_xml.Tables[0].Columns.Add("hw_timeleft");

        DataSet ds_lessons = ch_lessonsSvc.GetLessons(sc_id);
        
        for (int i = 0; i < ds_hw_xml.Tables[0].Rows.Count; i++) {
            bool flag = false;
            foreach (DataRow dr in ds_lessons.Tables[0].Rows) {
                if (ds_hw_xml.Tables[0].Rows[i]["les_id"].ToString() == dr["les_id"].ToString()) {
                    flag = true;
                    DataRow dr_hw_xml = ds_hw_xml.Tables[0].Rows[i];
                    DataRow dr_les = ch_lessonsSvc.GetLesson(Convert.ToInt32(dr_hw_xml["les_id"]));

                    //pro_name
                    DataRow dr_pro = ch_professionsSvc.GetProfession(Convert.ToInt32(dr_les["pro_id"]));
                    dr_hw_xml["pro_name"] = dr_pro["pro_name"];

                    //les_name
                    dr_hw_xml["les_name"] = dr_les["les_name"];

                    //hr_name
                    DataRow dr_hr = ch_hoursSvc.GetHour(Convert.ToInt32(dr_hw_xml["hr_id"]));
                    dr_hw_xml["hr_name"] = dr_hr["hr_name"];

                    //timeleft
                    DateTime dtDeadline = DateTime.ParseExact(dr_hw_xml["hw_deadlinedate"].ToString() + " " + Convert.ToDateTime(dr_hr["hr_start_time"].ToString()).ToString("HH:mm"), "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                    TimeSpan calcTimeLeft = dtDeadline - DateTime.Now;
                    if (calcTimeLeft.TotalMinutes < 0) {
                        dr_hw_xml["hw_timeleft"] = "נגמר הזמן!";
                    }
                    else {
                        dr_hw_xml["hw_timeleft"] = "להגשה בעוד " + calcTimeLeft.ToString(@"%d") + " ימים ו-" + calcTimeLeft.ToString(@"%h") + " שעות";
                    }
                }
            }
            if (!flag) {
                ds_hw_xml.Tables[0].Rows.RemoveAt(i);
                i = -1;
            }
        }

        return ds_hw_xml;
    }
    /// <param name="hw_id"> homework identity</param>
    /// <returns>DataSet of homeworks . filtered by homework identity.</returns>
    public static DataRow GetHomework(int hw_id) {
        DataSet ds_hw_xml = new DataSet();
        ds_hw_xml.ReadXml(HttpContext.Current.Server.MapPath("App_Data/homework.xml"));

        //add new columns
        ds_hw_xml.Tables[0].Columns.Add("pro_name");
        ds_hw_xml.Tables[0].Columns.Add("les_name");
        ds_hw_xml.Tables[0].Columns.Add("hr_name");
        ds_hw_xml.Tables[0].Columns.Add("hw_timeleft");

        foreach (DataRow dr_hw_xml in ds_hw_xml.Tables[0].Rows) {
            DataRow dr_les = ch_lessonsSvc.GetLesson(Convert.ToInt32(dr_hw_xml["les_id"]));

            //pro_name
            DataRow dr_pro = ch_professionsSvc.GetProfession(Convert.ToInt32(dr_les["pro_id"]));
            dr_hw_xml["pro_name"] = dr_pro["pro_name"];

            //les_name
            dr_hw_xml["les_name"] = dr_les["les_name"];

            //hr_name
            DataRow dr_hr = ch_hoursSvc.GetHour(Convert.ToInt32(dr_hw_xml["hr_id"]));
            dr_hw_xml["hr_name"] = dr_hr["hr_name"];

            //timeleft
            DateTime dtDeadline = DateTime.ParseExact(dr_hw_xml["hw_deadlinedate"].ToString() + " " + Convert.ToDateTime(dr_hr["hr_start_time"].ToString()).ToString("HH:mm"), "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
            TimeSpan calcTimeLeft = dtDeadline - DateTime.Now;
            if (calcTimeLeft.TotalMinutes < 0) {
                dr_hw_xml["hw_timeleft"] = "נגמר הזמן!";
            }
            else {
                dr_hw_xml["hw_timeleft"] = "להגשה בעוד " + calcTimeLeft.ToString(@"%d") + " ימים ו-" + calcTimeLeft.ToString(@"%h") + " שעות";
            }


            if (Convert.ToInt32(dr_hw_xml["hw_id"]) == hw_id) {
                return dr_hw_xml;
            }

        }

        return ds_hw_xml.Tables[0].Rows[0];
    }

    /// <param name="les_id">all homeworks in the specific les_id</param>
    /// <returns>DataSet of homeworks that teachers gave in a specific lesson</returns>
    public static DataSet GetLessonHomeworks(int les_id) {
        DataSet ds_hw = GetHomeworks();

        for (int i = 0; i < ds_hw.Tables[0].Rows.Count; i++) {
            if (Convert.ToInt32(ds_hw.Tables[0].Rows[i]["les_id"].ToString()) != les_id) {
                ds_hw.Tables[0].Rows.RemoveAt(i);
                i = -1;
            }
        }

        return ds_hw;
    }

    /// <param name="usr_id">the specific homeworks of the scpecific student usr_id.</param>
    /// <returns>DataSet of homeworks. filtered by a specific user</returns>
    public static DataSet GetStuHomeworks(int usr_id) {
        DataSet ds_hw = GetHomeworks();
        DataSet ds_stu_les = ch_lessonsSvc.GetStudentLessons(usr_id);
        
        for (int i = 0; i < ds_hw.Tables[0].Rows.Count; i++) {
            DataRow dr_hw = ds_hw.Tables[0].Rows[i];
            bool flag = false;

            foreach (DataRow dr_stu_les in ds_stu_les.Tables[0].Rows) {
                if (dr_hw["les_id"].ToString() == dr_stu_les["מזהה השיעור"].ToString()) {
                    flag = true;
                }
            }
            
            if (!flag) { // אם זה לא שיעור של התלמיד אז תמחק אותו
                ds_hw.Tables[0].Rows.RemoveAt(i);
                i = -1;
            }
        }

        return ds_hw;
    }
    /// <summary>
    /// count the number of homeworks to submit. filtered by a specific user
    /// </summary>
    /// <param name="usr_id">the specific user that have that homework to submit</param>
    /// <returns>integer number of homeworks to submit</returns>
    public static int GetNumStuHomeworksToSubmit(int usr_id) {
        DataSet ds = GetStuHomeworks(usr_id);
        int count = 0;
        foreach (DataRow dr in ds.Tables[0].Rows) {
            DateTime dt = Convert.ToDateTime(dr["hw_deadlinedate"]);
            if (dt >= DateTime.Now) {
                count++;
            }
        }

        return count;
    }
    /// <summary>
    /// Update a homework record in the database by hw_id
    /// </summary>
    /// <param name="hw_id">the specific homework to update</param>
    /// <param name="hw">the new homework to update</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string UpdateHomework(int hw_id, ch_homework hw) {
        try {
            DataSet ds_hw = GetHomeworks();
            ds_hw.Tables[0].Columns.Remove("pro_name");
            ds_hw.Tables[0].Columns.Remove("les_name");
            ds_hw.Tables[0].Columns.Remove("hr_name");
            ds_hw.Tables[0].Columns.Remove("hw_timeleft");

            foreach (DataRow dr_hw in ds_hw.Tables[0].Rows) {
                int current_hw_id = Convert.ToInt32(dr_hw["hw_id"]);
                if (current_hw_id == hw_id) {
                    dr_hw["les_id"] = hw.les_Id;
                    dr_hw["hr_id"] = hw.hr_Id;
                    dr_hw["hw_txt"] = hw.hw_Txt;
                    dr_hw["hw_deadlinedate"] = hw.hw_Deadlinedate;
                    dr_hw["hw_editdate"] = DateTime.Now.ToShortDateString();
                }
            }

            ds_hw.WriteXml(HttpContext.Current.Server.MapPath("App_Data/homework.xml"));
            return "";
        }
        catch (Exception) {
            return "אירעה תקלה בעת עדכון הנתונים.\n נסה שוב מאוחר יותר";
        }
    }
    /// <summary>
    /// Delete a record from homeworks XML by homework identity
    /// </summary>
    /// <param name="hw_id">homework to delete</param>
    public static void DeleteHomework(int hw_id) {
        DataSet ds_hw = GetHomeworks();
        ds_hw.Tables[0].Columns.Remove("pro_name");
        ds_hw.Tables[0].Columns.Remove("les_name");
        ds_hw.Tables[0].Columns.Remove("hr_name");
        ds_hw.Tables[0].Columns.Remove("hw_timeleft");

        for (int i = 0; i < ds_hw.Tables[0].Rows.Count; i++) {
            DataRow dr_hw = ds_hw.Tables[0].Rows[i];

            int current_hw_id = Convert.ToInt32(dr_hw["hw_id"]);
            if (current_hw_id == hw_id) {
                ds_hw.Tables[0].Rows.RemoveAt(i);
            }
        }
        ds_hw.WriteXml(HttpContext.Current.Server.MapPath("App_Data/homework.xml"));
    }
}