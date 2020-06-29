using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_crewSvc
/// </summary>
public class ch_crewSvc
{
    /// <summary>
    /// Adds a new crew record to the database.
    /// </summary>
    /// <param name="bhv1">The behavior to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddCrew(ch_crew crw1)
    {
        string strSql = "INSERT INTO ch_crew(usr_id, job_id) ";
        strSql += "VALUES(" + crw1.usr_Id + ", " + crw1.job_Id + ")";
        Connect.DoAction(strSql, "ch_crew");
        return "";
    }
    /// <summary>
    /// Check if the usr_id exists
    /// </summary>
    /// <param name="id">usr_id</param>
    /// <returns>true if exist, false if not exist.</returns>
    public static string IsExist(int id)
    {
        int num; string strSql;
        //check in crew
        strSql = "SELECT COUNT(usr_id) FROM ch_crew WHERE usr_id = " + id;
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_crew"));
        if (num > 0)
            return "crw";

        return "usr";
    }

    /// <param name="bhv_id">Identity of usr_id in database</param>
    /// <returns>
    /// DataRow of a crew from the database that has the same usr_id that the function gets.
    /// </returns>
    public static DataRow GetCrw(int usr_id)
    {
        string strSql = "SELECT job.job_id, job.job_name FROM ch_crew AS `crw` INNER JOIN ch_jobs AS `job` ON job.job_id = crw.job_id WHERE usr_id=" + usr_id;
        DataSet ds = Connect.GetData(strSql, "ch_crew");
        return ds.Tables[0].Rows[0];
    }

    /// <returns>
    /// DataSet of all crew from the database.
    /// </returns>
    public static DataSet GetCrew()
    {
        string strSql = "SELECT * FROM ch_crew";
        DataSet ds = Connect.GetData(strSql, "ch_crew");
        return ds;
    }

    /// <summary>
    /// Delete a database record by usr_id
    /// </summary>
    /// <param name="id">user id</param>
    public static void DeleteCrwById(int usr_id)
    {
        string strSql1 = "DELETE * FROM ch_crew WHERE usr_id=" + usr_id;
        Connect.DoAction(strSql1, "ch_crew");
    }

    /// <summary>
    /// Get DataSet of all crew users from database
    /// </summary>
    /// <returns>dataset full of crew users records</returns>
    public static DataSet GetCrewGV()
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
        strSql += "job.job_name AS `עבודה` ";
        strSql += "FROM (((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_crew AS `crw` ON crw.usr_id = usr.usr_id) ";
        strSql += "INNER JOIN ch_jobs AS `job` ON job.job_id = crw.job_id ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }

    /// <param name="strSearch">string to search</param>
    /// <returns>DataSet of crew users from database, filtered by a search string</returns>
    public static DataSet GetCrewGV(string strSearch) {
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
        strSql += "job.job_name AS `עבודה` ";
        strSql += "FROM (((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_crew AS `crw` ON crw.usr_id = usr.usr_id) ";
        strSql += "INNER JOIN ch_jobs AS `job` ON job.job_id = crw.job_id ";
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
        strSql += "OR job.job_name LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }

    /// <param name="sc_id">school id</param>
    /// <returns>DataSet of crew users from database, filtered by a school string</returns>
    public static DataSet GetCrewGV(int sc_id) {
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
        strSql += "job.job_name AS `עבודה` ";
        strSql += "FROM (((((ch_users AS `usr` INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "INNER JOIN ch_cities AS `sc_cty` ON sc.cty_id = sc_cty.cty_id) ";
        strSql += "INNER JOIN ch_levels AS `lvl` ON lvl.lvl_id = usr.lvl_id) ";
        strSql += "INNER JOIN ch_crew AS `crw` ON crw.usr_id = usr.usr_id) ";
        strSql += "INNER JOIN ch_jobs AS `job` ON job.job_id = crw.job_id ";
        strSql += "WHERE usr.sc_id = " + sc_id + " ";
        strSql += "ORDER BY usr.usr_id;";

        return Connect.GetData(strSql, "ch_users");
    }

    /// <summary>
    /// Update a crew record in the database
    /// </summary>
    /// <param name="newCrw1">new crew info to update</param>
    public static void UpdateCrwById(ch_crew newCrw1)
    {
        string strSql = "UPDATE ch_crew SET job_id=" + newCrw1.job_Id + " WHERE usr_id=" + newCrw1.usr_Id;
        Connect.DoAction(strSql, "ch_crew");
    }
}