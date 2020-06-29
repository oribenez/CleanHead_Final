using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_hoursSvc
/// </summary>
public class ch_hoursSvc
{
    /// <summary>
    /// Adds a new record of hour to the database.
    /// </summary>
    /// <param name="hr1">the hour to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddHour(ch_hours hr1)
    {
        if (IsHourExist(hr1) > 0)
            return "החדר כבר קיים";

        string strSql = "INSERT INTO ch_hours(hr_name, hr_start_time, hr_end_time, sc_id)  VALUES('" + hr1.hr_Name + "', #" + hr1.hr_Start_Time + "#, #" + hr1.hr_End_Time + "#, " + hr1.sc_Id + ")";
        Connect.DoAction(strSql, "ch_hours");
        return "";
    }
    /// <summary>
    /// Check if hr1 is exist in homework
    /// </summary>
    /// <param name="hr1">homework identity</param>
    /// <returns>num of existing hr1 in DataSet</returns>
    public static int IsHourExist(ch_hours hr1)
    {
        string strSql = "SELECT COUNT(hr_id) FROM ch_hours WHERE sc_id = " + hr1.sc_Id + " AND (hr_start_time = #" + hr1.hr_Start_Time +"# OR hr_end_time = #" + hr1.hr_End_Time +"#)";
        return Convert.ToInt32(Connect.MathAction(strSql, "ch_hours"));
    }

    /// <param name="hr_id">the specific hour to show</param>
    /// <returns>DataRow of a GetHour from the database that has the same hr_id that the function gets.</returns>
    public static DataRow GetHour(int hr_id) {
        string strSql = "SELECT * FROM ch_hours WHERE hr_id = " + hr_id;
        DataSet ds = Connect.GetData(strSql, "ch_hours");
        return ds.Tables[0].Rows[0];
    }
    /// <param name="sc_id">sthe specific school to filter</param>
    /// <returns>DataSet of hours. filtered by school identity</returns>
    public static DataSet GetHours(int sc_id)
    {
        string strSql = "SELECT * FROM ch_hours WHERE sc_id = " + sc_id;
        DataSet ds = Connect.GetData(strSql, "ch_hours");
        return ds;
    }

    /// <summary>
    /// Delete hour using hour id
    /// </summary>
    /// <param name="id">hr_id statement</param>
    public static void DeleteHourById(int id)
    {
        string strSql = "DELETE * FROM ch_hours WHERE hr_id=" + id;
        Connect.DoAction(strSql, "ch_hours");
    }

    /// <summary>
    /// Update hour using its id
    /// </summary>
    /// <param name="id">hr_id statement</param>
    /// <param name="newHour1">ch_hours object</param>
    public static string UpdateHourById(int id, ch_hours newHour1)
    {
        if (IsHourExist(newHour1) > 1)
            return "החדר כבר קיים";

        string strSql = "UPDATE ch_hours SET hr_name='" + newHour1.hr_Name + "', hr_start_time=#" + newHour1.hr_Start_Time + "#, hr_end_time=#" + newHour1.hr_End_Time + "# WHERE hr_id=" + id;
        Connect.DoAction(strSql, "ch_hours");

        return "";
    }

    /// <summary>
    /// Get hour id by hour name
    /// </summary>
    /// <param name="name">hour name</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static int GetId(int sc_id, string name)
    {
        string strSql = "SELECT COUNT(hr_id) FROM ch_hours WHERE sc_id = " + sc_id + " AND hr_name = '" + name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_hours"));
        if (num > 0)
        {
            string strSql2 = "SELECT hr_id FROM ch_hours WHERE sc_id = " + sc_id + " AND hr_name = '" + name + "'";
            DataSet ds = Connect.GetData(strSql2, "ch_hours");
            return Convert.ToInt32(ds.Tables["ch_hours"].Rows[0][0].ToString());
        }
        return -1;
    }
}