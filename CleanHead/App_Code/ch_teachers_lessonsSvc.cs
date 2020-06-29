using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_teachers_lessonsSvc
/// </summary>
public class ch_teachers_lessonsSvc
{
    /// <summary>
    /// Add a new ch_teachers_lessons record to the database
    /// </summary>
    /// <param name="newTchLes">a new record you want to add</param>
    public static void AddTeachersLesson(ch_teachers_lessons newTchLes)
    {
        string strSql = "INSERT INTO ch_teachers_lessons(les_id, usr_id) VALUES(" + newTchLes.les_Id + ", " + newTchLes.usr_Id + ")";
        Connect.DoAction(strSql, "ch_teachers_lessons");
    }

    /// <param name="les_id">lesson id of the specific lesson</param>
    /// <returns>DataSet of all teachers that are teaching in a specific lesson</returns>
    public static DataSet GetTeachersLesson(int les_id)
    {
        string selectQuery = "SELECT tch_les.usr_id AS `usr_id`, (usr.usr_first_name + ' ' + usr.usr_last_name) AS `usr_fullname` FROM ch_teachers_lessons AS `tch_les` INNER JOIN ch_users AS `usr` ON tch_les.usr_id = usr.usr_id WHERE les_id=" + les_id;
        DataSet ds = Connect.GetData(selectQuery, "ch_teachers_lessons");
        return ds;
    }

    /// <returns>DataSet teachers who teach in lessons</returns>
    public static DataSet GetTeachersLessons() {
        string lessosnQuery = "SELECT * FROM ((ch_teachers_lessons AS `tch_les` ";
        lessosnQuery += "INNER JOIN ch_users AS `usr` ON tch_les.usr_id = usr.usr_id) ";
        lessosnQuery += "INNER JOIN ch_lessons AS `les` ON tch_les.les_id = les.les_id) ";
        lessosnQuery += "ORDER BY usr.usr_first_name, les.les_name;";

        return Connect.GetData(lessosnQuery, "ch_teachers_lessons");
    }

    /// <summary>
    /// from:
    /// ליאורה, ליאורה, ליאורה, אבי ,עמית, עמית
    /// 
    /// to:
    /// ליאורה, אבי, עמית
    /// </summary>
    /// <param name="sc_id">school id of the specific school</param>
    /// <returns>DataSet of all teachers lessons, filtered by a specific school</returns>
    public static DataSet GetTeachersLessonsGroupByUsr_id(int sc_id) {
        string lessosnQuery = "SELECT tch_les.usr_id, MIN(tch_les.les_id), usr.usr_first_name, usr.usr_last_name ";
        lessosnQuery += "FROM (ch_teachers_lessons AS `tch_les` ";
        lessosnQuery += "INNER JOIN ch_users AS `usr` ON tch_les.usr_id = usr.usr_id) ";
        lessosnQuery += "WHERE usr.sc_id = " + sc_id + " ";
        lessosnQuery += "GROUP BY tch_les.usr_id, usr.usr_first_name, usr.usr_last_name ";
        lessosnQuery += "ORDER BY usr.usr_first_name;";

        return Connect.GetData(lessosnQuery, "ch_teachers_lessons");
    }
    /// <summary>
    /// Delete all teachers in a specific lesson
    /// </summary>
    /// <param name="les_id">lesson id of the specific lesson to delete</param>
    public static void DeleteAllTeachersLesson(int les_id)
    {
        string strSql = "DELETE * FROM ch_teachers_lessons WHERE les_id=" + les_id;
        Connect.DoAction(strSql, "ch_teachers_lessons");
    }
}