using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_schoolsSvc
/// </summary>
public class ch_schoolsSvc
{
    /// <summary>
    /// Add a new ch_schools record to the database
    /// </summary>
    /// <param name="sc1">a new school you want to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddSchool(ch_schools sc1) {
        string strSql1 = "SELECT COUNT(sc_id) FROM ch_schools WHERE sc_symbol = '" + sc1.sc_Symbol + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_schools"));
        if (num > 0)
            return "בית ספר כבר קיים";

        string strSql = "INSERT INTO ch_schools(sc_name, sc_symbol, cty_id, sc_address, sc_telephone)  VALUES('" + sc1.sc_Name + "', '" + sc1.sc_Symbol + "', " + sc1.cty_Id + ", '" + sc1.sc_Address + "', '" + sc1.sc_Telephone + "')";
        Connect.DoAction(strSql, "ch_schools");
        return "";
    }
    /// <param name="sc_id">specific id of the specific school</param>
    /// <returns>DataSet of schools filtered by school</returns>
    public static DataSet GetSchool(int sc_id) {
        string strSql = "SELECT sc_id, sc_symbol, sc_name, cty_name, sc_address, sc_telephone ";
        strSql += "FROM ch_schools AS `sc` ";
        strSql += "INNER JOIN ch_cities AS `cty` ON sc.cty_id = cty.cty_id ";
        strSql += "WHERE sc_id =" + sc_id;
        DataSet ds = Connect.GetData(strSql, "ch_schools");
        return ds;
    }
    /// <returns>DataSet of schools</returns>
    public static DataSet GetSchools()
    {
        string strSql = "SELECT sc_id, sc_symbol, sc_name, cty_name, sc_address, sc_telephone FROM ch_schools AS `sc` INNER JOIN ch_cities AS `cty` ON sc.cty_id = cty.cty_id";
        DataSet ds = Connect.GetData(strSql, "ch_schools");
        return ds;
    }
    /// <param name="strSearch">the string to search in database</param>
    /// <returns>dataset of all school that match the string search</returns>
    public static DataSet GetSchools(string strSearch) {
        string strSql = "SELECT sc_id, sc_symbol, sc_name, cty_name, sc_address, sc_telephone FROM ch_schools AS `sc` INNER JOIN ch_cities AS `cty` ON sc.cty_id = cty.cty_id ";
        strSql += "WHERE sc_symbol LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR sc_name LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR cty_name LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR sc_address LIKE '%" + strSearch.Trim() + "%' ";
        strSql += "OR sc_telephone LIKE '%" + strSearch.Trim() + "%';";
        DataSet ds = Connect.GetData(strSql, "ch_schools");
        return ds;
    }
    /// <summary>
    /// generate date of the middle between semester A and B of all schools
    /// </summary>
    /// <returns>string of the date</returns>
    public static string GetSchoolSemester() {
        return "01/02/" + DateTime.Now.Year; // תאריך אמצע סמסטר קבוע לכל בתי הספר
    }
    /// <summary>
    /// Delete a record of ch_schools by his id
    /// </summary>
    /// <param name="id">school id of the school you want to delete</param>
    public static void DeleteSchoolById(int id)
    {
        string strSql = "DELETE * FROM ch_schools WHERE sc_id=" + id;
        Connect.DoAction(strSql, "ch_schools");
    }

    /// <summary>
    /// Update a record of ch_schools using its id
    /// </summary>
    /// <param name="id">sc_id statement</param>
    /// <param name="newSchool1">ch_schools object</param>
    public static string UpdateSchoolById(int id, ch_schools newSchool1)
    {
        string strSql1 = "SELECT COUNT(sc_id) FROM ch_schools WHERE sc_symbol = '" + newSchool1.sc_Symbol + "' AND sc_id <> " + id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_schools"));
        if (num > 0)
            return "בית ספר כבר קיים";

        string strSql = "UPDATE ch_schools SET sc_name='" + newSchool1.sc_Name + "', sc_symbol='" + newSchool1.sc_Symbol + "', cty_id=" + newSchool1.cty_Id + ", sc_address='" + newSchool1.sc_Address + "', sc_telephone='" + newSchool1.sc_Telephone + "' WHERE sc_id=" + id;
        Connect.DoAction(strSql, "ch_schools");

        return "";
    }

    

    /// <summary>
    /// Get school id by its symbol
    /// </summary>
    /// <param name="symbol">school symbol</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static int GetId(string symbol)
    {
        string strSql = "SELECT COUNT(sc_id) FROM ch_schools WHERE sc_symbol = '" + symbol + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_schools"));
        if (num > 0)
        {
            string strSql2 = "SELECT sc_id FROM ch_schools WHERE sc_symbol = '" + symbol + "'";
            DataSet ds = Connect.GetData(strSql2, "ch_schools");
            return Convert.ToInt32(ds.Tables["ch_schools"].Rows[0][0].ToString());
        }
        return -1;
    }
    /// <summary>
    /// Get school id by its name and city
    /// </summary>
    /// <param name="sc_name">school name</param>
    /// <param name="cty_id">school city id</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static int GetId(string sc_name, int cty_id)
    {
        string strSql = "SELECT COUNT(sc_id) FROM ch_schools WHERE sc_name = '" + sc_name + "' AND cty_id = " + cty_id;
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_schools"));
        if (num > 0)
        {
            string strSql2 = "SELECT sc_id FROM ch_schools WHERE sc_name = '" + sc_name + "' AND cty_id = " + cty_id;
            DataSet ds = Connect.GetData(strSql2, "ch_schools");
            return Convert.ToInt32(ds.Tables["ch_schools"].Rows[0][0].ToString());
        }
        return -1;
    }
}