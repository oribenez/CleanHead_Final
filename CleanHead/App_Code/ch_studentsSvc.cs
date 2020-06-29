using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_studentsSvc
/// </summary>
public class ch_studentsSvc
{
    /// <summary>
    /// Add a student to the database
    /// </summary>
    /// <param name="stu1">user object</param>
    public static string AddStudent(ch_students stu1)
    {
        if (ch_usersSvc.GetUsrType(stu1.usr_Id) != "usr")
            return "User already exists!";

        string strSql = "INSERT INTO ch_students(usr_id, rm_id, stu_mom_identity, stu_mom_first_name, stu_mom_cellphone, stu_dad_identity, stu_dad_first_name, stu_dad_cellphone)  ";
        strSql += "VALUES(" + stu1.usr_Id + ", " + stu1.rm_Id + ", '" + stu1.stu_Mom_Identity + "', '" + stu1.stu_Mom_First_Name + "', '" + stu1.stu_Mom_Cellphone + "', '" + stu1.stu_Dad_Identity + "', '" + stu1.stu_Dad_First_Name + "', '" + stu1.stu_Dad_Cellphone + "')";
        Connect.DoAction(strSql, "ch_students");
        return "";
    }
    /// <summary>
    /// Delete a student by his id
    /// </summary>
    /// <param name="id">the specific student to delete</param>
    public static void DeleteStudentById(int id)
    {
        string strSql1 = "DELETE * FROM ch_students WHERE usr_id=" + id;
        Connect.DoAction(strSql1, "ch_students");
    }

    /// <returns>All students data</returns>
    public static DataSet GetStudents()
    {
        string strSql = "SELECT * FROM ch_students";
        DataSet ds = Connect.GetData(strSql, "ch_students");
        return ds;
    }
    /// <returns>All students data by class</returns>
    public static DataSet GetStudents(int rm_id) {
        string strSql = "SELECT * FROM ch_students AS `stu` ";
        strSql += "INNER JOIN ch_rooms AS `rm` ON stu.rm_id = rm.rm_id ";
        strSql += "WHERE rm.rm_id=" + rm_id;
        return Connect.GetData(strSql, "ch_students");
    }
    /// <returns>All students data by class and school</returns>
    public static DataSet GetStudents(int sc_id, int rm_id) {
        string strSql = "SELECT * FROM (ch_students AS `stu` ";
        strSql += "INNER JOIN ch_rooms AS `rm` ON stu.rm_id = rm.rm_id) ";
        strSql += "INNER JOIN ch_users AS `usr` ON usr.usr_id = stu.usr_id ";
        strSql += "WHERE usr.sc_id =" + sc_id + " AND rm.rm_id=" + rm_id;
        return Connect.GetData(strSql, "ch_students");
    }

    /// <returns>All students data</returns>
    public static DataSet GetStudentsGV()
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
        strSql += "lvl.lvl_name AS `רמה`, ";
        strSql += "rm.rm_name AS `כיתה`, ";
        strSql += "stu.stu_mom_identity AS `מספר זהות - אמא`, ";
        strSql += "stu.stu_mom_first_name AS `שם פרטי - אמא`, ";
        strSql += "stu.stu_mom_cellphone AS `פלאפון - אמא`, ";
        strSql += "stu.stu_dad_identity AS `מספר זהות - אבא`, ";
        strSql += "stu.stu_dad_first_name AS `שם פרטי - אבא`, ";
        strSql += "stu.stu_dad_cellphone AS `פלאפון - אבא` ";
        strSql += "FROM (((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_students AS `stu` ON stu.usr_id = usr.usr_id) ";
        strSql += "INNER JOIN ch_rooms AS `rm` ON rm.rm_id = stu.rm_id ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }
    /// <param name="strSearch">the string to search in all students users</param>
    /// <returns>DataSet of students data filtered by a search string</returns>
    public static DataSet GetStudentsGV(string strSearch) {
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
        strSql += "lvl.lvl_name AS `רמה`, ";
        strSql += "rm.rm_name AS `כיתה`, ";
        strSql += "stu.stu_mom_identity AS `מספר זהות - אמא`, ";
        strSql += "stu.stu_mom_first_name AS `שם פרטי - אמא`, ";
        strSql += "stu.stu_mom_cellphone AS `פלאפון - אמא`, ";
        strSql += "stu.stu_dad_identity AS `מספר זהות - אבא`, ";
        strSql += "stu.stu_dad_first_name AS `שם פרטי - אבא`, ";
        strSql += "stu.stu_dad_cellphone AS `פלאפון - אבא` ";
        strSql += "FROM (((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_students AS `stu` ON stu.usr_id = usr.usr_id) ";
        strSql += "INNER JOIN ch_rooms AS `rm` ON rm.rm_id = stu.rm_id ";
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
        strSql += "OR rm.rm_name LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }
    /// <param name="sc_id">school id of the school you want to filter by</param>
    /// <returns>DataSet of students filtered by a school</returns>
    public static DataSet GetStudentsGV(int sc_id) {
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
        strSql += "lvl.lvl_name AS `רמה`, ";
        strSql += "rm.rm_name AS `כיתה`, ";
        strSql += "stu.stu_mom_identity AS `מספר זהות - אמא`, ";
        strSql += "stu.stu_mom_first_name AS `שם פרטי - אמא`, ";
        strSql += "stu.stu_mom_cellphone AS `פלאפון - אמא`, ";
        strSql += "stu.stu_dad_identity AS `מספר זהות - אבא`, ";
        strSql += "stu.stu_dad_first_name AS `שם פרטי - אבא`, ";
        strSql += "stu.stu_dad_cellphone AS `פלאפון - אבא` ";
        strSql += "FROM (((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_students AS `stu` ON stu.usr_id = usr.usr_id) ";
        strSql += "INNER JOIN ch_rooms AS `rm` ON rm.rm_id = stu.rm_id ";
        strSql += "WHERE usr.sc_id = " + sc_id + " ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }

    /// <returns>DataRow student data by id</returns>
    public static DataRow GetStudentById(int id)
    {
        string strSql = "SELECT * FROM ch_students AS `stu` INNER JOIN ch_rooms AS `rm` ON rm.rm_id = stu.rm_id WHERE usr_id=" + id;
        return Connect.GetData(strSql, "ch_students").Tables[0].Rows[0];
    }
    /// <summary>
    /// Update a student by his id
    /// </summary>
    /// <param name="usr_id">the user id of the student</param>
    /// <param name="newStu1">the new student info</param>
    public static void UpdateStuById(int usr_id, ch_students newStu1)
    {
        string strSql = "UPDATE ch_students SET rm_id=" + newStu1.rm_Id + ", stu_mom_identity='" + newStu1.stu_Mom_Identity + "', stu_mom_first_name='" + newStu1.stu_Mom_First_Name + "', stu_mom_cellphone='" + newStu1.stu_Mom_Cellphone + "', stu_dad_identity='" + newStu1.stu_Dad_Identity + "', stu_dad_first_name='" + newStu1.stu_Dad_First_Name + "', stu_dad_cellphone='" + newStu1.stu_Dad_Cellphone + "' WHERE usr_id=" + usr_id;
        Connect.DoAction(strSql, "ch_students");
    }

    /// <summary>
    /// Check if student is exist by his usr_id
    /// </summary>
    /// <param name="id">user id of the student ti search</param>
    /// <returns>a string of the user type "stu" if student and "usr" if not</returns>
    public static string IsExist(int id)
    {
        int num; string strSql;
        //check in students
        strSql = "SELECT COUNT(usr_id) FROM ch_students WHERE usr_id = " + id;
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_students"));
        if (num > 0)
            return "stu";

        return "usr";
    }

    
}