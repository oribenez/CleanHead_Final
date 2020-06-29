using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_teachersSvc
/// </summary>
public class ch_teachersSvc
{
    /// <summary>
    /// Add a teacher to the database
    /// </summary>
    /// <param name="tch1">the new teacher to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddTeacher(ch_teachers tch1)
    {
        if (ch_usersSvc.GetUsrType(tch1.usr_Id) != "usr")
            return "User already exists!";

        string strSql = "INSERT INTO ch_teachers(usr_id)  ";
        strSql += "VALUES(" + tch1.usr_Id + ")";
        Connect.DoAction(strSql, "ch_teachers");
        return "";
    }
    /// <summary>
    /// check if teacher is exist by his id
    /// </summary>
    /// <param name="id">the tch_id of the specific teacher to search</param>
    /// <returns>true if exists.
    /// false if not exists.</returns>
    public static string IsExist(int id)
    {
        int num; string strSql;
        //check in teachers
        strSql = "SELECT COUNT(usr_id) FROM ch_teachers WHERE usr_id = " + id;
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_teachers"));
        if (num > 0)
            return "tch";

        return "usr";
    }
    
    /// <returns>All teachers data</returns>
    public static DataSet GetTeachers()
    {
        string strSql = "SELECT * FROM ch_teachers";
        DataSet ds = Connect.GetData(strSql, "ch_teachers");
        return ds;
    }

    /// <returns>Get teacher data by id</returns>
    public static DataSet GetTeacherById(int id)
    {
        string strSql = "SELECT * FROM ch_teachers WHERE usr_id=" + id;
        return Connect.GetData(strSql, "ch_teachers");
    }
    /// <summary>
    /// Delete a teacher by his id
    /// </summary>
    /// <param name="usr_id">user id of the user you want to delete</param>
    public static void DeleteTeacherById(int usr_id)
    {
        string strSql1 = "DELETE * FROM ch_teachers WHERE usr_id=" + usr_id;
        Connect.DoAction(strSql1, "ch_teachers");
    }

    /// <returns>DataSet of all teachers</returns>
    public static DataSet GetTeachersGV()
    {
        string strSql = "SELECT ";
        strSql += "usr.usr_id AS `מזהה`, ";
        strSql += "usr.usr_identity AS `תעודת זהות`, ";
        strSql += "usr.usr_first_name AS `שם פרטי`, ";
        strSql += "usr.usr_last_name AS `שם משפחה`, ";
        strSql += "usr.usr_birth_date AS `תאריך לידה`, ";
        strSql += "usr.usr_gender AS `מין`, ";
        strSql += "cty.cty_name AS `עיר`, ";
        strSql += "usr.usr_address AS `כתובת`, ";
        strSql += "usr.usr_home_phone AS `טלפון בבית`, ";
        strSql += "usr.usr_cellphone AS `פלאפון`, ";
        strSql += "sc.sc_name AS `בית ספר`, ";
        strSql += "sc_cty.cty_name AS `עיר בית ספר`, ";
        strSql += "usr.usr_email AS `אימייל`, ";
        strSql += "lvl.lvl_name AS `רמה` ";
        strSql += "FROM ((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_teachers AS `tch` ON tch.usr_id = usr.usr_id ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }

    /// <param name="strSearch">the string to search</param>
    /// <returns>DataSet of all teachers. filtered by a search string</returns>
    public static DataSet GetTeachersGV(string strSearch) {
        string strSql = "SELECT ";
        strSql += "usr.usr_id AS `מזהה`, ";
        strSql += "usr.usr_identity AS `תעודת זהות`, ";
        strSql += "usr.usr_first_name AS `שם פרטי`, ";
        strSql += "usr.usr_last_name AS `שם משפחה`, ";
        strSql += "usr.usr_birth_date AS `תאריך לידה`, ";
        strSql += "usr.usr_gender AS `מין`, ";
        strSql += "cty.cty_name AS `עיר`, ";
        strSql += "usr.usr_address AS `כתובת`, ";
        strSql += "usr.usr_home_phone AS `טלפון בבית`, ";
        strSql += "usr.usr_cellphone AS `פלאפון`, ";
        strSql += "sc.sc_name AS `בית ספר`, ";
        strSql += "sc_cty.cty_name AS `עיר בית ספר`, ";
        strSql += "usr.usr_email AS `אימייל`, ";
        strSql += "lvl.lvl_name AS `רמה` ";
        strSql += "FROM ((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_teachers AS `tch` ON tch.usr_id = usr.usr_id ";
        strSql += "WHERE usr.usr_identity LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR usr.usr_first_name LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR usr.usr_last_name LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR usr.usr_gender LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR cty.cty_name LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR usr.usr_address LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR usr.usr_home_phone LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR usr.usr_cellphone LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR sc.sc_name LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR usr.usr_email LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }
    /// <param name="sc_id">school id of the filtered scool</param>
    /// <returns>DataSet of all records in ch_teachers filtered by a school</returns>
    public static DataSet GetTeachersGV(int sc_id) {
        string strSql = "SELECT ";
        strSql += "usr.usr_id AS `מזהה`, ";
        strSql += "usr.usr_identity AS `תעודת זהות`, ";
        strSql += "usr.usr_first_name AS `שם פרטי`, ";
        strSql += "usr.usr_last_name AS `שם משפחה`, ";
        strSql += "usr.usr_birth_date AS `תאריך לידה`, ";
        strSql += "usr.usr_gender AS `מין`, ";
        strSql += "cty.cty_name AS `עיר`, ";
        strSql += "usr.usr_address AS `כתובת`, ";
        strSql += "usr.usr_home_phone AS `טלפון בבית`, ";
        strSql += "usr.usr_cellphone AS `פלאפון`, ";
        strSql += "sc.sc_name AS `בית ספר`, ";
        strSql += "sc_cty.cty_name AS `עיר בית ספר`, ";
        strSql += "usr.usr_email AS `אימייל`, ";
        strSql += "lvl.lvl_name AS `רמה` ";
        strSql += "FROM ((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_teachers AS `tch` ON tch.usr_id = usr.usr_id ";
        strSql += "WHERE usr.sc_id = " + sc_id + " ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }
}