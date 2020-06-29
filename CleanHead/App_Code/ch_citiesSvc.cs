using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_citiesSvc
/// </summary>
public class ch_citiesSvc
{
    /// <summary>
    /// Add a new ch_cities record to the database
    /// </summary>
    /// <param name="cty1">a new city you want to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddCity(ch_cities cty1)
    {
        string strSql1 = "SELECT COUNT(cty_id) FROM ch_cities WHERE cty_name = '" + cty1.cty_Name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_cities"));

        if (num > 0)
            return "העיר כבר קיימת במערכת";

        string strSql = "INSERT INTO ch_cities(cty_name)  VALUES('" + cty1.cty_Name + "')";
        Connect.DoAction(strSql, "ch_cities");
        return "";
    }

    /// <summary>
    /// Check if the "cityName" Exist in the database.
    /// </summary>
    /// <param name="cty1">The city you want to check</param>
    /// <returns>true if exists.
    /// false if not exists.</returns>
    public static bool IsCityNameExist(ch_cities cty1)
    {
        string strSql = "SELECT COUNT(cty_id) FROM ch_cities WHERE cty_name = '" + cty1.cty_Name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_cities"));
        if (num > 0)
            return true;
        return false;
    }

    /// <returns>DataSet of all cities records from database</returns>
    public static DataSet GetCities()
    {
        string strSql = "SELECT * FROM ch_cities";
        DataSet ds = Connect.GetData(strSql, "ch_cities");
        return ds;
    }

    /// <param name="strSearch">the string to search</param>
    /// <returns>DataSet of cities records from database. by search string (%string%)</returns>
    public static DataSet GetCities(string strSearch) {
        string strSql = "SELECT * FROM ch_cities ";
        strSql += "WHERE cty_name LIKE '%" + strSearch.Trim() + "%'";
        return Connect.GetData(strSql, "ch_cities");
    }

    /// <param name="id">cty_id</param>
    /// <returns>returns the number relationships of cty_id in other database tables.</returns>
    public static int Relationships(int id)
    {
        //is city exist in other tables
        string strSql1 = "SELECT COUNT(cty_id) FROM ch_schools WHERE cty_id = " + id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_schools"));

        strSql1 = "SELECT COUNT(cty_id) FROM ch_users WHERE cty_id = " + id;
        num += Convert.ToInt32(Connect.MathAction(strSql1, "ch_users"));
        return num;
    }

    /// <summary>
    /// Delete city record from database using city id 
    /// </summary>
    /// <param name="id">cty_id statement</param>
    public static void DeleteCityById(int id)
    {
        string strSql = "DELETE * FROM ch_cities WHERE cty_id=" + id;
        Connect.DoAction(strSql, "ch_cities");
    }

    /// <summary>
    /// Update city using its id
    /// </summary>
    /// <param name="id">cty_id statement</param>
    /// <param name="newCty1">new city to update</param>
    public static string UpdateCityById(int id, ch_cities newCty1)
    {
        string strSql1 = "SELECT COUNT(cty_id) FROM ch_cities WHERE cty_name = '" + newCty1.cty_Name + "' AND cty_id <>" + id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_cities"));

        if (num > 0)
            return "העיר כבר קיימת במערכת";

        string strSql = "UPDATE ch_cities SET cty_name='" + newCty1.cty_Name + "' WHERE cty_id=" + id;
        Connect.DoAction(strSql, "ch_cities");

        return "";
    }

    /// <summary>
    /// Get city id by city name
    /// </summary>
    /// <param name="name">city name</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static int GetIdByCtyName(string name)
    {
        string strSql = "SELECT COUNT(cty_id) FROM ch_cities WHERE cty_name = '" + name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_cities"));
        if (num > 0)
        {
            string strSql2 = "SELECT cty_id FROM ch_cities WHERE cty_name = '" + name + "'";
            DataSet ds = Connect.GetData(strSql2, "ch_cities");
            return Convert.ToInt32(ds.Tables["ch_cities"].Rows[0][0].ToString());
        }
        return -1;
    }
}