using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_professionsSvc
/// </summary>
public class ch_professionsSvc
{
    /// <summary>
    /// Add new ch_professions record 
    /// </summary>
    /// <param name="pro1">ch_professions row you want to add </param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddPro(ch_professions pro1)
    {
        string strSql1 = "SELECT COUNT(pro_id) FROM ch_professions WHERE pro_name = '" + pro1.pro_Name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_professions"));

        if (num > 0)
            return "המקצוע כבר קיים";

        string strSql = "INSERT INTO ch_professions(pro_name)  VALUES('" + pro1.pro_Name + "')";
        Connect.DoAction(strSql, "ch_professions");
        return "";
    }

    /// <returns>DataSet of all professions data</returns>
    public static DataSet GetProfessions()
    {
        string strSql = "SELECT * FROM ch_professions ORDER BY pro_name";
        DataSet ds = Connect.GetData(strSql, "ch_professions");
        return ds;
    }

    /// <param name="pro_id">profession id of the specific profession you want to get</param>
    /// <returns>DataRow of a specific profession</returns>
    public static DataRow GetProfession(int pro_id) {
        string strSql = "SELECT * FROM ch_professions WHERE pro_id = " + pro_id;
        DataSet ds = Connect.GetData(strSql, "ch_professions");
        return ds.Tables[0].Rows[0];
    }
    /// <summary>
    /// Get profession that teachers are teaching in a specific layer and school
    /// </summary>
    /// <param name="sc_id">school id of the specific school you want</param>
    /// <param name="layer">layer id of the specific layer you want</param>
    /// <returns>DataSet of professions</returns>
    public static DataSet GetLayerProfessions(int sc_id, string layer) {
        string strSql = "SELECT pro.pro_id, pro.pro_name, rm.rm_name "; // Mid(rm.rm_name, 1, Instr(rm.rm_name, ' ')) AS `lol`
        strSql += "FROM ((((ch_professions AS `pro` ";
        strSql += "INNER JOIN ch_lessons AS `les` ON pro.pro_id = les.pro_id) ";
        strSql += "INNER JOIN ch_students_lessons AS `stu_les` ON les.les_id = stu_les.les_id) ";
        strSql += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        strSql += "INNER JOIN ch_users AS `usr` ON usr.usr_id = stu.usr_id) ";
        strSql += "INNER JOIN ch_rooms AS `rm` ON stu.rm_id = rm.rm_id ";
        strSql += "WHERE usr.sc_id = " + sc_id + " ";
        strSql += "ORDER BY pro.pro_name;";
        DataSet ds = Connect.GetData(strSql, "ch_professions");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
            string dbLayer = ds.Tables[0].Rows[i]["rm_name"].ToString().Substring(0, ds.Tables[0].Rows[i]["rm_name"].ToString().IndexOf(' '));
            if (dbLayer != layer) {
                ds.Tables[0].Rows.RemoveAt(i);
                i = 0;
            }
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++) {
            if (ds.Tables[0].Rows[i]["pro_id"].ToString() == ds.Tables[0].Rows[i + 1]["pro_id"].ToString()) {
                ds.Tables[0].Rows.RemoveAt(i);
                i--;
            }
        }

        return ds;
    }
    /// <summary>
    /// Get profession that teachers are teaching in a specific layer
    /// </summary>
    /// <param name="layer">layer id of the specific layer you want</param>
    /// <returns>DataSet of professions</returns>
    public static DataSet GetLayerProfessions(string layer) {
        string strSql = "SELECT pro.pro_id, pro.pro_name, rm.rm_name "; // Mid(rm.rm_name, 1, Instr(rm.rm_name, ' ')) AS `lol`
        strSql += "FROM (((ch_professions AS `pro` ";
        strSql += "INNER JOIN ch_lessons AS `les` ON pro.pro_id = les.pro_id) ";
        strSql += "INNER JOIN ch_students_lessons AS `stu_les` ON les.les_id = stu_les.les_id) ";
        strSql += "INNER JOIN ch_students AS `stu` ON stu_les.usr_id = stu.usr_id) ";
        strSql += "INNER JOIN ch_rooms AS `rm` ON stu.rm_id = rm.rm_id ";
        strSql += "ORDER BY pro.pro_name;";
        DataSet ds = Connect.GetData(strSql, "ch_professions");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
            string dbLayer = ds.Tables[0].Rows[i]["rm_name"].ToString().Substring(0, ds.Tables[0].Rows[i]["rm_name"].ToString().IndexOf(' '));
            if (dbLayer != layer) {
                ds.Tables[0].Rows.RemoveAt(i);
                i = 0;
            }
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++) {
            if (ds.Tables[0].Rows[i]["pro_id"].ToString() == ds.Tables[0].Rows[i + 1]["pro_id"].ToString()) {
                ds.Tables[0].Rows.RemoveAt(i);
                i--;
            }
        }

        return ds;
    }

    /// <summary>
    /// Delete ch_profession record from database using profession id
    /// </summary>
    /// <param name="id">profession id of the profession you want to delete</param>
    public static void DeleteProById(int id)
    {
        string strSql = "DELETE * FROM ch_professions WHERE pro_id=" + id;
        Connect.DoAction(strSql, "ch_professions");
    }

    /// <summary>
    /// Update ch_profession record from database using profession id
    /// </summary>
    /// <param name="id">profession id of the profession you want to update</param>
    /// <param name="newPro1">the new profession you want to update</param>
    public static string UpdateProById(int id, ch_professions newPro1)
    {
        string strSql1 = "SELECT COUNT(pro_id) FROM ch_professions WHERE pro_name = '" + newPro1.pro_Name + "' AND pro_id <>" + id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_professions"));

        if (num > 0)
            return "המקצוע כבר קיים";

        string strSql = "UPDATE ch_professions SET pro_name='" + newPro1.pro_Name + "' WHERE pro_id=" + id;
        Connect.DoAction(strSql, "ch_professions");

        return "";
    }

    /// <summary>
    /// Get profession id by profession name
    /// </summary>
    /// <param name="name">profession name</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static int GetIdByProName(string name)
    {
        string strSql = "SELECT COUNT(pro_id) FROM ch_professions WHERE pro_name = '" + name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_professions"));
        if (num > 0)
        {
            string strSql2 = "SELECT pro_id FROM ch_professions WHERE pro_name = '" + name + "'";
            DataSet ds = Connect.GetData(strSql2, "ch_professions");
            return Convert.ToInt32(ds.Tables["ch_professions"].Rows[0][0].ToString());
        }
        return -1;
    }
}