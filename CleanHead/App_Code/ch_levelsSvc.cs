using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_levelsSvc
/// </summary>
public class ch_levelsSvc
{
    /// <summary>
    /// Add new record of ch_levels in Database
    /// </summary>
    /// <param name="lvl1">the level you want to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddLevel(ch_levels lvl1)
    {
        if (IsLevelExist(lvl1))
            return "הדרגה כבר קיימת";

        string strSql = "INSERT INTO ch_levels(lvl_id, lvl_name, lvl_desc)  VALUES(" + lvl1.lvl_Id + ",'" + lvl1.lvl_Name + "','" + lvl1.lvl_Desc + "')";
        Connect.DoAction(strSql, "ch_levels");
        return "";
    }
    /// <summary>
    /// Check if level is exist
    /// </summary>
    /// <param name="lvl1">level you want to check</param>
    /// <returns>true if exist, false if not</returns>
    public static bool IsLevelExist(ch_levels lvl1)//use while insert
    {
        string strSql = "SELECT COUNT(lvl_id) FROM ch_levels WHERE lvl_id = " + lvl1.lvl_Id + " OR lvl_name = '" + lvl1.lvl_Name + "' OR lvl_desc = '" + lvl1.lvl_Desc + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_levels"));
        if (num > 0)
            return true;
        return false;
    }
    public static bool IsLevelExist(ch_levels lvl1, int old_lvl_id, string old_lvl_name)//use while update
    {
        if (old_lvl_id == lvl1.lvl_Id || old_lvl_name == lvl1.lvl_Name)
        {
            string strSql = "SELECT COUNT(lvl_id) FROM ch_levels WHERE lvl_id = " + lvl1.lvl_Id + " OR lvl_name = '" + lvl1.lvl_Name + "' OR lvl_desc = '" + lvl1.lvl_Desc + "'";
            int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_levels"));
            if (num > 1)
                return true;
            return false;
        }
        else
        {
            string strSql = "SELECT COUNT(lvl_id) FROM ch_levels WHERE lvl_id = " + lvl1.lvl_Id + " OR lvl_name = '" + lvl1.lvl_Name + "' OR lvl_desc = '" + lvl1.lvl_Desc + "'";
            int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_levels"));
            if (num > 0)
                return true;
            return false;
        }
    }

    /// <param name="lvl_id">the specific level by his lvl_id</param>
    /// <returns>DataRow of ch_levels by his lvl_id</returns>
    public static DataRow GetLevel(int lvl_id) {
        string strSql = "SELECT * FROM ch_levels WHERE lvl_id = " + lvl_id;
        return Connect.GetData(strSql, "ch_levels").Tables[0].Rows[0];
    }
    /// <returns>DataSet of all ch_levels records</returns>
    public static DataSet GetLevels()
    {
        string strSql = "SELECT * FROM ch_levels";
        DataSet ds = Connect.GetData(strSql, "ch_levels");
        return ds;
    }

    /// <summary>
    /// Delete lvl using lvl id
    /// </summary>
    /// <param name="id">lvl_id statement</param>
    public static void DeleteLevelById(int id)
    {
        string strSql = "DELETE * FROM ch_levels WHERE lvl_id=" + id;
        Connect.DoAction(strSql, "ch_levels");
    }

    /// <summary>
    /// Update lvl using its id
    /// </summary>
    /// <param name="id">lvl_id statement</param>
    /// <param name="newLevel1">ch_levels object</param>
    public static string UpdateLevelById(int id, string name, ch_levels newLevel1)
    {
        if (IsLevelExist(newLevel1,id, name))
            return "הדרגה כבר קיימת";

        string strSql = "UPDATE ch_levels SET lvl_id = " + newLevel1.lvl_Id + ", lvl_name = '" + newLevel1.lvl_Name + "', lvl_desc = '" + newLevel1.lvl_Desc + "' WHERE lvl_id=" + id;
        Connect.DoAction(strSql, "ch_levels");

        return "";
    }

    /// <summary>
    /// Get lvl id by lvl name
    /// </summary>
    /// <param name="name">lvl name</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static int GetIdByLevelName(string name)
    {
        string strSql = "SELECT COUNT(lvl_id) FROM ch_levels WHERE lvl_name = '" + name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_levels"));
        if (num > 0)
        {
            string strSql2 = "SELECT lvl_id FROM ch_levels WHERE lvl_name = '" + name + "'";
            DataSet ds = Connect.GetData(strSql2, "ch_levels");
            return Convert.ToInt32(ds.Tables["ch_levels"].Rows[0][0].ToString());
        }
        return -1;
    }
}