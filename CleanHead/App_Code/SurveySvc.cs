using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for SurveySvc
/// </summary>
public class SurveySvc
{
    /// <returns>DataSet of all Surveys</returns>
    public static DataSet GetSurveys() {
        DataSet xml_ds = new DataSet();
        xml_ds.ReadXml(HttpContext.Current.Server.MapPath("App_Data/Survey.xml"));
        return xml_ds;
    }
    /// <param name="srv_id">int survey id of the specific survey</param>
    /// <returns>DataRow of a specific survey</returns>
    public static DataRow GetSurvey(int srv_id) {
        DataSet xml_ds = new DataSet();
        xml_ds.ReadXml(HttpContext.Current.Server.MapPath("App_Data/Survey.xml"));

        foreach (DataRow dr in xml_ds.Tables[0].Rows) {
            int xml_srv_id = Convert.ToInt32(dr["srv_id"]);
            if (xml_srv_id == srv_id) {
                return dr;
            }
        }
        return null;
    }
    /// <summary>
    /// Update a survey by a new one
    /// </summary>
    /// <param name="srv1">the new survey</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string UpdateSurvey(Survey srv1)
    {
        try {
            DataSet xml_ds = GetSurveys();
            int srv_id = 1; // תמיד זה 1 כי ניתן לעשות רק סקר אחד
            foreach (DataRow dr in xml_ds.Tables[0].Rows) {
                if (Convert.ToInt32(dr["srv_id"]) == srv_id) {
                    dr["srv_question"] = srv1.srv_question;
                    dr["srv_ans1"] = srv1.srv_ans1;
                    dr["srv_ans2"] = srv1.srv_ans2;
                    dr["srv_ans3"] = srv1.srv_ans3;
                    dr["srv_ans4"] = srv1.srv_ans4;
                    break;
                }
            }

            xml_ds.WriteXml(HttpContext.Current.Server.MapPath("App_Data/Survey.xml"));
            return "";
        }
        catch (Exception) {
            return "אירעה תקלה בעת עדכון הנתונים.\n נסה שוב מאוחר יותר";
        }
    }
    /// <summary>
    /// save score of all surveys
    /// </summary>
    /// <param name="ans1Score">score of answer 1</param>
    /// <param name="ans2Score">score of answer 2</param>
    /// <param name="ans3Score">score of answer 3</param>
    /// <param name="ans4Score">score of answer 4</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string SurveySaveScore(string ans1Score, string ans2Score, string ans3Score, string ans4Score)
    {
        try {
            DataSet xml_ds = GetSurveys();
            int srv_id = 1; // תמיד זה 1 כי ניתן לעשות רק סקר אחד
            foreach (DataRow dr in xml_ds.Tables[0].Rows) {
                if (Convert.ToInt32(dr["srv_id"]) == srv_id) {
                    dr["srv_ans1_score"] = ans1Score;
                    dr["srv_ans2_score"] = ans2Score;
                    dr["srv_ans3_score"] = ans3Score;
                    dr["srv_ans4_score"] = ans4Score;
                    break;
                }
            }

            xml_ds.WriteXml(HttpContext.Current.Server.MapPath("App_Data/Survey.xml"));
            return "";
        }
        catch (Exception) {
            return "אירעה תקלה בעת עדכון הנתונים.\n נסה שוב מאוחר יותר";
        }
    }
    /// <summary>
    /// Check if the user did the survey
    /// </summary>
    /// <param name="usr_id">the user id of user</param>
    /// <returns>true if exists.
    /// false if not exists.</returns>
    public static bool IsUsrDidSurvey(int usr_id) {
        DataSet xml_ds = GetSurveys();
        for (int i = 1; i <= 4; i++) {
            if (HttpContext.Current.Application["srv_ans" + i + "_score"].ToString() == "") { continue; }
            int[] usr_idArrInt = Array.ConvertAll(HttpContext.Current.Application["srv_ans" + i + "_score"].ToString().Split(','), Convert.ToInt32);

            foreach (int item in usr_idArrInt) {
                if (usr_id == item) {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Reset the survey
    /// </summary>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string ResetSurvey() {
        try {
            for (int i = 1; i <= 4; i++) {
                HttpContext.Current.Application["srv_ans" + i + "_score"] = "";
            }

            DataSet xml_ds = GetSurveys();
            int srv_id = 1; // תמיד זה 1 כי ניתן לעשות רק סקר אחד
            foreach (DataRow dr in xml_ds.Tables[0].Rows) {
                if (Convert.ToInt32(dr["srv_id"]) == srv_id) {
                    dr["srv_question"] = "";
                    dr["srv_ans1"] = "";
                    dr["srv_ans2"] = "";
                    dr["srv_ans3"] = "";
                    dr["srv_ans4"] = "";
                    dr["srv_ans1_score"] = "";
                    dr["srv_ans2_score"] = "";
                    dr["srv_ans3_score"] = "";
                    dr["srv_ans4_score"] = "";
                    break;
                }
            }

            xml_ds.WriteXml(HttpContext.Current.Server.MapPath("App_Data/Survey.xml"));
            return "";
        }
        catch (Exception) {
            return "אירעה תקלה בעת עדכון הנתונים.\n נסה שוב מאוחר יותר";
        }
    }
}