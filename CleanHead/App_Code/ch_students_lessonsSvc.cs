using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for ch_students_lessonsSvc
/// </summary>
public class ch_students_lessonsSvc
{
    /// <summary>
    /// Add a new ch_students_lessons record to the database
    /// </summary>
    /// <param name="stu_les1">a new ch_students_lessons you want to add</param>
    public static void AddStudentLesson(ch_students_lessons stu_les1)
    {
        string strSql = "INSERT INTO ch_students_lessons(les_id, usr_id) VALUES(" + stu_les1.les_Id + ", " + stu_les1.usr_Id + ")";
        Connect.DoAction(strSql, "ch_students_lessons");
    }
    /// <summary>
    /// Generate a Hashtable that is built from all classes in a school 
    /// </summary>
    /// <returns>the generated hashtable</returns>
    static Hashtable Selections()
    {
        Hashtable htAllSelectedStudents = new Hashtable();

        DataSet dsRooms = ch_roomsSvc.GetStuPrimaryClasses(Convert.ToInt32(HttpContext.Current.Session["sc_id"]));
        foreach (DataRow dr in dsRooms.Tables[0].Rows)
        {
            htAllSelectedStudents.Add(dr["rm_id"].ToString(), new Hashtable());
        }
        return htAllSelectedStudents;
    }

    /// <param name="les_id">the specific lesson you want to learn</param>
    /// <returns>DataSet of all students in a specific lesson</returns>
    public static DataSet GetStudentsLesson(int les_id)
    {
        string selectQuery = "SELECT stu_les.usr_id AS `usr_id`, (usr.usr_first_name + ' ' + usr.usr_last_name) AS `usr_fullname`, stu.rm_id AS `rm_id` FROM ((ch_students_lessons AS `stu_les` INNER JOIN ch_users AS `usr` ON stu_les.usr_id = usr.usr_id) INNER JOIN ch_students AS `stu` ON stu.usr_id = usr.usr_id) WHERE les_id=" + les_id;
        DataSet ds = Connect.GetData(selectQuery, "ch_students_lessons");
        return ds;
    }
    /// <summary>
    /// Delete a ch_students_lessons record
    /// </summary>
    /// <param name="stu_les1">the ch_students_lessons record to delete</param>
    public static void DeleteStudentLesson(ch_students_lessons stu_les1)
    {
        string strSql = "DELETE * FROM ch_students_lessons WHERE les_id=" + stu_les1.les_Id + " AND usr_id=" + stu_les1.usr_Id;
        Connect.DoAction(strSql, "ch_students_lessons");
    }
    /// <summary>
    /// delete all students in a specific lesson
    /// </summary>
    /// <param name="les_id">lesson id of the specific lesson.</param>
    public static void DeleteAllStudentsLesson(int les_id)
    {
        string strSql = "DELETE * FROM ch_students_lessons WHERE les_id=" + les_id;
        Connect.DoAction(strSql, "ch_students_lessons");
    }
}