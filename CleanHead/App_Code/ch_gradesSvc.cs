using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_gradesSvc
/// </summary>
public class ch_gradesSvc
{
    /// <summary>
    /// Adds a new record of grade to the database.
    /// </summary>
    /// <param name="newGrades">The grade to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
	public static string AddGrade(ch_grades newGrades){
        string queryIsExist = "SELECT COUNT(grd_id) FROM ch_grades ";
        queryIsExist += "WHERE les_id = " + newGrades.les_Id + " AND grd_name = '" + newGrades.grd_Name + "'";
        int num = Convert.ToInt32(Connect.MathAction(queryIsExist, "ch_grades"));
        if (num > 0) {
            return "כבר קיים מבחן בעל שם זהה באותו שיעור";
        }

        string queryInsert = "INSERT INTO ch_grades(les_id, grd_name, grd_date) VALUES(" + newGrades.les_Id + ", '" + newGrades.grd_Name + "', '" + newGrades.grd_Date + "')";
        Connect.DoAction(queryInsert, "ch_grades");

        return "";
    }

    /// <summary>
    /// Update a grade record in the database by grd_id
    /// </summary>
    /// <param name="grd_id">grade id</param>
    /// <param name="newGrades">new grade to update</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string UpdateGrade(int grd_id, ch_grades newGrades) {
        string queryIsExist = "SELECT COUNT(grd_id) FROM ch_grades ";
        queryIsExist += "WHERE les_id = " + newGrades.les_Id + " AND grd_name = '" + newGrades.grd_Name + "'";
        int num = Convert.ToInt32(Connect.MathAction(queryIsExist, "ch_grades"));
        if (num > 1) {
            return "כבר קיים מבחן בעל שם זהה באותו שיעור";
        }

        string queryUpdate = "UPDATE ch_grades SET les_id = " + newGrades.les_Id + ", grd_name = '" + newGrades.grd_Name + "', grd_date = '" + newGrades.grd_Date + "' WHERE grd_id = " + grd_id;
        Connect.DoAction(queryUpdate, "ch_grades");

        return "";
    }
    /// <summary>
    /// Delete a database record by grd_id
    /// </summary>
    /// <param name="grd_id">grade id</param>
    public static void DeleteGrade(int grd_id) {
        string delTest = "DELETE * FROM ch_grades WHERE grd_id = " + grd_id;
        Connect.DoAction(delTest, "ch_grades");
    }

    /// <param name="grd_id">grade id</param>
    /// <returns>
    /// DataRow of a Test from the database that has the same grd_id that the function gets.
    /// </returns>
    public static DataRow GetTest(int grd_id) {
        string test = "SELECT * FROM ch_grades WHERE grd_id = " + grd_id;
        return Connect.GetData(test, "ch_grades").Tables[0].Rows[0];
    }
    /// <summary>
    /// Update test Percents
    /// </summary>
    /// <param name="grd_id">grade id</param>
    /// <param name="percents">num percents</param>
    public static void PercentsUpdate(int grd_id, int percents) {
        string updateQuery = "UPDATE ch_grades SET grd_percents = " + percents + " WHERE grd_id = " + grd_id;
        Connect.DoAction(updateQuery, "ch_grades");
    }

    /// <param name="les_id">lesson id</param>
    /// <returns>
    /// DataSet of all tests from the database.
    /// </returns>
    public static DataSet GetTests(int les_id) {
        string tests = "SELECT grd_id, grd_name, grd_date, grd_percents FROM ch_grades WHERE les_id = " + les_id + " ORDER BY grd_date";
        return Connect.GetData(tests, "ch_grades");
    }

    /// <param name="usr_id">for who is the report specified</param>
    /// <returns>
    /// DataSet of all tests from the database specified for a student.
    /// filtered by user
    /// </returns>
    public static DataSet GradesReportForStu(int usr_id) {
        string queryGradesReport = "SELECT ";
        queryGradesReport += "FORMAT(grd.grd_date,'dd/MM/yyyy') AS `תאריך`, ";
        queryGradesReport += "les.les_name AS `שיעור`, ";
        queryGradesReport += "grd.grd_name AS `שם המבחן`, ";
        queryGradesReport += "usr_grd.grd_num AS `ציון` ";
        queryGradesReport += "FROM (ch_grades AS `grd` ";
        queryGradesReport += "INNER JOIN ch_users_grades AS `usr_grd` ON usr_grd.grd_id = grd.grd_id) ";
        queryGradesReport += "INNER JOIN ch_lessons AS `les` ON les.les_id = grd.les_id ";
        queryGradesReport += "WHERE usr_grd.usr_id = " + usr_id + " ";
        queryGradesReport += "ORDER BY grd.grd_date DESC, les.les_name;";

        return Connect.GetData(queryGradesReport, "ch_grades");
    }

    /// <param name="usr_id">for who is the report specified</param>
    /// <param name="les_id">the only lesson you want to see in this report</param>
    /// <returns>
    /// DataSet of all tests from the database specified for a student.
    /// filtered by user and lesson
    /// </returns>
    public static DataSet GradesReportForStu(int usr_id, int les_id) {
        string queryGradesReport = "SELECT ";
        queryGradesReport += "FORMAT(grd.grd_date,'dd/MM/yyyy') AS `תאריך`, ";
        queryGradesReport += "les.les_name AS `שיעור`, ";
        queryGradesReport += "grd.grd_name AS `שם המבחן`, ";
        queryGradesReport += "usr_grd.grd_num AS `ציון` ";
        queryGradesReport += "FROM (ch_grades AS `grd` ";
        queryGradesReport += "INNER JOIN ch_users_grades AS `usr_grd` ON usr_grd.grd_id = grd.grd_id) ";
        queryGradesReport += "INNER JOIN ch_lessons AS `les` ON les.les_id = grd.les_id ";
        queryGradesReport += "WHERE usr_grd.usr_id = " + usr_id + " AND grd.les_id = " + les_id + " ";
        queryGradesReport += "ORDER BY grd.grd_date, les.les_name;";

        return Connect.GetData(queryGradesReport, "ch_grades");
    }

    /// <param name="sc_id">the only school you want to see in this report</param>
    /// <param name="les_id">the only lesson you want to see in this report</param>
    /// <returns>
    /// DataSet of all tests and students from the database
    /// filtered by school and lesson
    /// </returns>
    public static DataSet GradesReport(int sc_id, int les_id) {
        DataSet studentsInLesson = ch_students_lessonsSvc.GetStudentsLesson(les_id);
        studentsInLesson.Tables[0].Columns["usr_fullname"].ColumnName = "תלמידים";

        DataSet dsTests = ch_gradesSvc.GetTests(les_id);
        if (studentsInLesson.Tables[0].Rows.Count == 0 || dsTests.Tables[0].Rows.Count == 0) {
            studentsInLesson.Clear();
            return studentsInLesson;
        }

        int i = 0;
        foreach (DataRow drTest in dsTests.Tables[0].Rows) {
            int grd_id = Convert.ToInt32(drTest["grd_id"]);

            string grades = "SELECT usr_id, grd_num FROM ch_users_grades WHERE grd_id = " + grd_id;
            DataSet dsGrades = Connect.GetData(grades, "ch_users_grades");

            //calculate semster
            DateTime test_date = Convert.ToDateTime(drTest["grd_date"].ToString());
            DateTime sc_semester_date = Convert.ToDateTime(ch_schoolsSvc.GetSchoolSemester());
            string semesterName = "מחצית א'";
            if (test_date.Month >= sc_semester_date.Month && test_date.Month <= 7) {
                semesterName = "מחצית ב'";
            }

            studentsInLesson.Tables[0].Columns.Add(drTest["grd_name"].ToString() + " (" + test_date.ToString("dd/MM/yyyy") + ") - " + semesterName);

            int j = 0;
            foreach (DataRow dr in studentsInLesson.Tables[0].Rows) {
                foreach (DataRow drGrade in dsGrades.Tables[0].Rows) {
                    if (dr["usr_id"].ToString() == drGrade["usr_id"].ToString()) {
                        dr[i + 3] = drGrade["grd_num"];
                    }
                }
                j++;
            }
            i++;
        }

        //calculate final grade
        studentsInLesson.Tables[0].Columns.Add("ציון סופי");


        dsTests = ch_gradesSvc.GetTests(les_id);
        
        double avg = 0;
        foreach (DataRow dr_students in studentsInLesson.Tables[0].Rows) {
            int usr_id = Convert.ToInt32(dr_students["usr_id"]);

            int sumPercents = 0;

            foreach (DataRow dr_tests in dsTests.Tables[0].Rows) {
                sumPercents += Convert.ToInt32(dr_tests["grd_percents"].ToString());
            }

            double result = 0;
            foreach (DataRow dr_tests in dsTests.Tables[0].Rows) {
                int grd_id = Convert.ToInt32(dr_tests["grd_id"].ToString());
                int percents = Convert.ToInt32(dr_tests["grd_percents"].ToString());

                string gradeQuery = "SELECT grd_num FROM ch_users_grades WHERE grd_id = " + grd_id + " AND usr_id = " + usr_id;
                DataSet ds = Connect.GetData(gradeQuery, "ch_users_grades");
                if (ds.Tables[0].Rows.Count == 0) {
                    int grade = 0;
                    result += ((double)percents / (double)sumPercents) * (double)grade;
                }
                else {
                    int grade = Convert.ToInt32(Connect.GetData(gradeQuery, "ch_users_grades").Tables[0].Rows[0][0].ToString());
                    result += ((double)percents / (double)sumPercents) * (double)grade;
                }
            }
            //Mathematical calculation with one decimal digit
            dr_students["ציון סופי"] = Math.Round(result, 1, MidpointRounding.AwayFromZero);
            avg += (double)Math.Round(result, 1, MidpointRounding.AwayFromZero);
        }

        avg /= (double)studentsInLesson.Tables[0].Rows.Count;
        avg = Math.Round(avg, 2, MidpointRounding.AwayFromZero);

        DataRow footer_dr = studentsInLesson.Tables[0].NewRow();
        footer_dr["ציון סופי"] = "ממוצע כולל: " + avg;
        studentsInLesson.Tables[0].Rows.Add(footer_dr);

        return studentsInLesson;
    }

    /// <returns>Max grade id</returns>
    public static int GetMaxId() {
        //check in ch_grades
        string strSql = "SELECT MAX(grd_id) FROM ch_grades";
        object obj = Connect.MathAction(strSql, "ch_grades");

        return Convert.ToInt32(obj);
    }
}