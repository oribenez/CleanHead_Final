using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_teachers_professionsSvc
/// </summary>
public class ch_teachers_professionsSvc
{
    /// <summary>
    /// Add a new ch_teachers_professions record to the database
    /// </summary>
    /// <param name="tch_pro">a new taecher profession you want to add</param>
    public static void AddTeacherProfessions(ch_teachers_professions tch_pro) {
        string insertQuery = "INSERT INTO ch_teachers_professions(pro_id, usr_id) VALUES(" + tch_pro.pro_Id + ", " + tch_pro.usr_Id + ")";
        Connect.DoAction(insertQuery, "ch_teachers_professions");
    }
    /// <summary>
    /// Update a specific teacher profession
    /// </summary>
    /// <param name="tch_pro">the teacher profession to update</param>
    public static void UpdateTeacherProfessions(ch_teachers_professions tch_pro) {
        
        string updateQuery = "UPDATE ch_teachers_professions SET pro_id = " + tch_pro.pro_Id + " WHERE usr_id = " + tch_pro.usr_Id;
        Connect.DoAction(updateQuery, "ch_teachers_professions");
    }
    /// <summary>
    /// Delete a specific teacher profession
    /// </summary>
    /// <param name="tch_pro">the teacher profession to delete</param>
    public static void DeleteTeacherProfessions(ch_teachers_professions tch_pro) {
        string deleteQuery = "DELETE * FROM ch_teachers_professions WHERE usr_id=" + tch_pro.usr_Id + " AND pro_id = " + tch_pro.pro_Id;
        Connect.DoAction(deleteQuery, "ch_teachers_professions");
    }
    /// <summary>
    /// check if the ch_teachers_professions is exist in the database records
    /// </summary>
    /// <param name="tch_pro">the teacher profession you want to delete</param>
    /// <returns>true if exists.
    /// false if not exists.</returns>
    public static bool IsExist(ch_teachers_professions tch_pro) {
        //check in crew
        string strSql = "SELECT COUNT(usr_id) FROM ch_teachers_professions WHERE usr_id = " + tch_pro.usr_Id + " AND pro_id = " + tch_pro.pro_Id;
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_teachers_professions"));
        if (num > 0)
            return true;
        return false;
    }
    /// <param name="usr_id">the specific teacher</param>
    /// <returns>DataSet of all profession of a teacher</returns>
    public static DataSet GetProfessionsByTch(int usr_id) {
        string queryProfessions = "SELECT pro.pro_id, pro.pro_name FROM ch_teachers_professions AS `tch_pro` INNER JOIN ch_professions AS `pro` ON pro.pro_id = tch_pro.pro_id WHERE usr_id = " + usr_id;
        return Connect.GetData(queryProfessions, "ch_teachers_professions");
    }
    /// <param name="tch_id">teacher id of the specific teacher </param>
    /// <param name="layer">the specific layer to filter</param>
    /// <returns>DataSet of all professions filtered by teacher and layer</returns>
    public static DataSet GetProfessionsByTch(int tch_id, string layer) {//MID(rm.rm_name,1,INSTR(1,rm.rm_name,' '))
        string queryProfessions = "SELECT ";
        queryProfessions += "MIN(pro.pro_id) AS `מזהה_מקצוע`, ";
        queryProfessions += "pro.pro_name ";
        queryProfessions += "FROM ((((ch_teachers_professions AS `tch_pro` ";
        queryProfessions += "INNER JOIN ch_professions AS `pro` ON pro.pro_id = tch_pro.pro_id) ";
        queryProfessions += "INNER JOIN ch_lessons AS `les` ON les.pro_id = pro.pro_id) ";
        queryProfessions += "INNER JOIN ch_students_lessons AS `stu_les` ON stu_les.les_id = les.les_id) ";
        queryProfessions += "INNER JOIN ch_students AS `stu` ON stu.usr_id = stu_les.usr_id) ";
        queryProfessions += "INNER JOIN ch_rooms AS `rm` ON rm.rm_id = stu.rm_id ";
        queryProfessions += "WHERE tch_pro.usr_id = " + tch_id + " AND MID(rm.rm_name,1,INSTR(1,rm.rm_name,' ')) = '" + layer + "' ";
        queryProfessions += "GROUP BY pro.pro_name";
        return Connect.GetData(queryProfessions, "ch_teachers_professions");
    }

    /// <param name="pro_id">profession id of the specific profession</param>
    /// <returns>DataSet of all teachers that teach a specific profession</returns>
    public static DataSet GetTeachersByPro(int pro_id)
    {
        string queryTeachers = "SELECT * ";
        queryTeachers += "FROM (ch_teachers_professions AS `tch_pro` ";
        queryTeachers += "INNER JOIN ch_teachers AS `tch` ON tch_pro.usr_id = tch.usr_id) ";
        queryTeachers += "INNER JOIN ch_users AS `usr` ON tch.usr_id = usr.usr_id ";
        queryTeachers += "WHERE tch_pro.pro_id = " + pro_id;
        return Connect.GetData(queryTeachers, "ch_teachers_professions");
    }
    /// <param name="sc_id">school id of the specific school</param>
    /// <param name="layer">the specific layer to filter</param>
    /// <param name="pro_id">profession id of the profession to filter</param>
    /// <returns>DataSet of all teachers filtered by a school, layer and a profession</returns>
    public static DataSet GetTeachersByProAndSchool(int sc_id, string layer, int pro_id) {
        string queryTeachers = "SELECT usr.usr_id, (MIN(usr.usr_first_name) + ' ' + MIN(usr.usr_last_name)) AS `usr_fullname` ";
        queryTeachers += "FROM ((((((ch_teachers_professions AS `tch_pro` ";
        queryTeachers += "INNER JOIN ch_teachers AS `tch` ON tch_pro.usr_id = tch.usr_id) ";
        queryTeachers += "INNER JOIN ch_teachers_lessons AS `tch_les` ON tch_les.usr_id = tch.usr_id) ";
        queryTeachers += "INNER JOIN ch_lessons AS `les` ON les.les_id = tch_les.les_id) ";
        queryTeachers += "INNER JOIN ch_students_lessons AS `stu_les` ON stu_les.les_id = les.les_id) ";
        queryTeachers += "INNER JOIN ch_students AS `stu` ON stu.usr_id = stu_les.usr_id) ";
        queryTeachers += "INNER JOIN ch_rooms AS `rm` ON rm.rm_id = stu.rm_id) ";
        queryTeachers += "INNER JOIN ch_users AS `usr` ON usr.usr_id = tch.usr_id ";
        queryTeachers += "WHERE tch_pro.pro_id = " + pro_id + " AND usr.sc_id = " + sc_id + " AND MID(rm.rm_name,1,INSTR(1,rm.rm_name,' ')) = '" + layer + "' ";
        queryTeachers += "GROUP BY usr.usr_id;";

        return Connect.GetData(queryTeachers, "ch_teachers_professions");
    }

    public static DataSet GetTeachersByProAndSchool(int sc_id, int pro_id) {
        string queryTeachers = "SELECT usr.usr_id, (MIN(usr.usr_first_name) + ' ' + MIN(usr.usr_last_name)) AS `usr_fullname` ";
        queryTeachers += "FROM (ch_teachers_professions AS `tch_pro` ";
        queryTeachers += "INNER JOIN ch_teachers AS `tch` ON tch_pro.usr_id = tch.usr_id) ";
        queryTeachers += "INNER JOIN ch_users AS `usr` ON usr.usr_id = tch.usr_id ";
        queryTeachers += "WHERE tch_pro.pro_id = " + pro_id + " AND usr.sc_id = " + sc_id + " ";
        queryTeachers += "GROUP BY usr.usr_id;";

        return Connect.GetData(queryTeachers, "ch_teachers_professions");
    }
}