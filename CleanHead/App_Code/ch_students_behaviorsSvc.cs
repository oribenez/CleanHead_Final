using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_students_behaviorSvc
/// </summary>
public class ch_students_behaviorsSvc
{
    /// <summary>
    /// Add a new ch_students_behaviors record to the database
    /// </summary>
    /// <param name="stubhv1">a new student behavior you want to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddStuBehavior(ch_students_behaviors stubhv1) {
        if (IsStuBehaviorExist(stubhv1) > 0)
            return "לא יכולה להירשם התנהגות כפולה באותו יום ושעה לאותו תלמיד";

        string strSql = "INSERT INTO ch_students_behaviors(bhv_id, stu_id, les_id, stu_bhv_date, hr_id)  VALUES(" + stubhv1.bhv_id + "," + stubhv1.stu_id + "," + stubhv1.les_id + ",'" + stubhv1.stu_bhv_date.ToString("yyyy/MM/dd") + "'," + stubhv1.hr_id + ")";
        Connect.DoAction(strSql, "ch_students_behaviors");
        return "";
    }

    /// <param name="stubhv1">the object to check if exist in database</param>
    /// <returns>the number of instances of stubhv1 existing in database</returns>
    public static int IsStuBehaviorExist(ch_students_behaviors stubhv1) {
        string strSql1 = "SELECT COUNT(bhv_id) FROM ch_students_behaviors WHERE bhv_id = " + stubhv1.bhv_id + " AND stu_id = " + stubhv1.stu_id + " AND les_id = " + stubhv1.les_id + " AND Format(stu_bhv_date, 'yyyy/MM/dd') = #" + stubhv1.stu_bhv_date.ToString("yyyy/MM/dd") + "# AND hr_id = " + stubhv1.hr_id;
        return Convert.ToInt32(Connect.MathAction(strSql1, "ch_students_behaviors"));
    }

    /// <returns>DataRow of a specific student behavior</returns>
    public static DataRow GetStuBehavior(ch_students_behaviors stubhv1) {
        string strSql = "SELECT * FROM ch_students_behaviors WHERE bhv_id = " + stubhv1.bhv_id + " AND stu_id = " + stubhv1.stu_id + " AND les_id = " + stubhv1.les_id + " AND Format(stu_bhv_date, 'yyyy/MM/dd') = #" + stubhv1.stu_bhv_date.ToString("yyyy/MM/dd") + "# AND hr_id = " + stubhv1.hr_id;
        return Connect.GetData(strSql, "ch_students_behaviors").Tables[0].Rows[0];
    }

    /// <returns>DataSet of all students behaviors</returns>
    public static DataSet GetStudentsBehaviors() {
        string strSql = "SELECT * FROM ch_students_behaviors";
        return Connect.GetData(strSql, "ch_students_behaviors");
    }
    /// <summary>
    /// | שם/סוג | הפרעה | היעדרות | איחור | שוטטות
    ///     ck       ck        ck         ck      אורי
    ///     ck       ck        ck         ck      עמית
    /// </summary>
    /// <param name="les_id">lesson id</param>
    /// <returns>students behaviors DataTable (as the e.g. in summary) by a lesson</returns>
    public static DataTable GetStudentsBehaviorsToInsert(int les_id) { 
        DataSet dsBehaviors = ch_behaviorsSvc.GetBehaviors();
        DataTable dtStuBhvInsert = new DataTable();

        dtStuBhvInsert.Columns.Add("usr_id");
        dtStuBhvInsert.Columns.Add("תלמידים/ סוג");

        // add: איחור | שוטטות | הפרעה
        for (int i = 0; i < dsBehaviors.Tables[0].Rows.Count; i++) {
            string bhv_name = dsBehaviors.Tables[0].Rows[i]["bhv_name"].ToString();
            dtStuBhvInsert.Columns.Add(bhv_name);
        }

        if (les_id == -1) {
            return dtStuBhvInsert;
        }
        DataSet dsStudentsInLesson = ch_students_lessonsSvc.GetStudentsLesson(les_id);

        foreach (DataRow drStudentsInLesson in dsStudentsInLesson.Tables[0].Rows) {
            DataRow newdr = dtStuBhvInsert.NewRow();
            newdr["usr_id"] = drStudentsInLesson["usr_id"].ToString();
            newdr["תלמידים/ סוג"] = drStudentsInLesson["usr_fullname"].ToString();
            dtStuBhvInsert.Rows.Add(newdr);
        }

        return dtStuBhvInsert;
    }

    /// <param name="stu_id">student id of a specific student to get all of his behaviors</param>
    /// <returns>DataSet of all behaviors of a specific student</returns>
    public static DataSet GetStuBehaviors(int stu_id) {
        string strSql = "SELECT * ";
        strSql += "FROM (((((ch_students_behaviors AS `stu_bhv`";
        strSql += "INNER JOIN ch_behaviors AS `bhv` ON bhv.bhv_id = stu_bhv.bhv_id) ";
        strSql += "INNER JOIN ch_lessons AS `les` ON les.les_id = stu_bhv.les_id) ";
        strSql += "INNER JOIN ch_professions AS `pro` ON pro.pro_id = les.pro_id) ";
        strSql += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.les_id = les.les_id) ";
        strSql += "INNER JOIN ch_users AS `usr` ON usr.usr_id = tch_les.usr_id) ";
        strSql += "INNER JOIN ch_hours AS `hr` ON hr.hr_id = stu_bhv.hr_id ";
        strSql += "WHERE stu_id = " + stu_id + " ";
        strSql += "ORDER BY stu_bhv.stu_bhv_date DESC ";
        return Connect.GetData(strSql, "ch_students_behaviors");
    }

    /// <param name="stu_id">student id of the student to get his behavior points.</param>
    /// <returns>student behavior point</returns>
    public static int GetStuBehaviorsPointsByMonth(int stu_id) {
        DataSet dsBhv = GetStuBehaviors(stu_id);

        //calculate points
        int sumPoints = 100;
        foreach (DataRow dr in dsBhv.Tables[0].Rows) {
            DateTime dt = Convert.ToDateTime(dr["stu_bhv_date"]);
            if (dt.Month == DateTime.Now.Month) {
                sumPoints -= Convert.ToInt32(dr["bhv_value"]);
            }
        }
        return sumPoints;
    }
    /// <summary>
    /// Delete a student behavior
    /// </summary>
    /// <param name="stubhv1">student behavior to delete</param>
    public static void DeleteStuBehavior(ch_students_behaviors stubhv1) {
        string strSql = "DELETE * FROM ch_students_behaviors WHERE bhv_id = " + stubhv1.bhv_id + " AND stu_id = " + stubhv1.stu_id + " AND les_id = " + stubhv1.les_id + " AND Format(stu_bhv_date, 'yyyy/MM/dd') = '" + stubhv1.stu_bhv_date.ToString("yyyy/MM/dd") + "' AND hr_id = " + stubhv1.hr_id;
        Connect.DoAction(strSql, "ch_students_behaviors");
    }
    /// <summary>
    /// Update an old stu_bhv by a new one.
    /// </summary>
    /// <param name="old_stubhv1">the old stu_bhv to change</param>
    /// <param name="new_stubhv1">the new stu_bhv</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string UpdateStuBehavior(ch_students_behaviors old_stubhv1, ch_students_behaviors new_stubhv1) {
        string strSql1 = "SELECT COUNT(bhv_id) FROM ch_students_behaviors WHERE bhv_id = " + new_stubhv1.bhv_id + " AND stu_id = " + new_stubhv1.stu_id + " AND les_id = " + new_stubhv1.les_id + " AND Format(stu_bhv_date, 'yyyy/MM/dd') = #" + new_stubhv1.stu_bhv_date.ToString("yyyy/MM/dd") + "# AND hr_id = " + new_stubhv1.hr_id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_students_behaviors"));

        if (num > 0)
            return "לא יכולה להירשם התנהגות כפולה באותו יום ושעה לאותו תלמיד";

        string strSql = "UPDATE ch_students_behaviors SET bhv_id = " + new_stubhv1.bhv_id + " AND stu_id = " + new_stubhv1.stu_id + " AND les_id = " + new_stubhv1.les_id + " AND Format(stu_bhv_date, 'yyyy/MM/dd') = #" + new_stubhv1.stu_bhv_date.ToString("yyyy/MM/dd") + "# AND hr_id = " + new_stubhv1.hr_id + " WHERE bhv_id = " + old_stubhv1.bhv_id + " AND stu_id = " + old_stubhv1.stu_id + " AND les_id = " + old_stubhv1.les_id + " AND Format(stu_bhv_date, 'yyyy/MM/dd') = #" + old_stubhv1.stu_bhv_date.ToString("yyyy/MM/dd") + "# AND hr_id = " + old_stubhv1.hr_id;
        Connect.DoAction(strSql, "ch_students_behaviors");

        return "";
    }
}