using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_behaviorSvc
/// </summary>
public class ch_behaviorsSvc
{
    /// <summary>
    /// Adds a new record of behavior to the database.
    /// </summary>
    /// <param name="bhv1">The behavior to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddBehavior(ch_behaviors bhv1) {
        if (NumBhvExist(bhv1) > 0)
            return "שם ההתנהגות כבר קיים במערכת";

        string strSql = "INSERT INTO ch_behaviors(bhv_name, bhv_value)  VALUES('" + bhv1.bhv_name + "'," + bhv1.bhv_value + ")";
        Connect.DoAction(strSql, "ch_behaviors");
        return "";
    }

    /// <summary>
    /// Check if the behavior that the function get is already exist in database.
    /// </summary>
    /// <param name="bhv1">The behavior to check if it's equal to record in the Database</param>
    /// <returns>Integer number of behaviors in database that are equal to bhv1.</returns>
    public static int NumBhvExist(ch_behaviors bhv1) {
        string strSql1 = "SELECT COUNT(bhv_id) FROM ch_behaviors WHERE bhv_name = '" + bhv1.bhv_name + "'";
        return Convert.ToInt32(Connect.MathAction(strSql1, "ch_behaviors"));
    }


    /// <param name="bhv_id">Identity of behavior in database</param>
    /// <returns>
    /// DataRow of a behavior from the database that has the same bhv_id that the function gets.
    /// </returns>
    public static DataRow GetBehavior(int bhv_id) {
        string strSql = "SELECT * FROM ch_behaviors WHERE bhv_id = " + bhv_id;
        return Connect.GetData(strSql, "ch_behaviors").Tables[0].Rows[0];
    }

    /// <returns>
    /// DataSet of all behaviors from the database.
    /// </returns>
    public static DataSet GetBehaviors() {
        string strSql = "SELECT * FROM ch_behaviors";
        return Connect.GetData(strSql, "ch_behaviors");
    }

    /// <summary>
    /// Delete a database record by bhv_id
    /// </summary>
    /// <param name="id">behavior id</param>
    public static void DeleteBehaviorById(int id) {
        string strSql = "DELETE * FROM ch_behaviors WHERE bhv_id=" + id;
        Connect.DoAction(strSql, "ch_behaviors");
    }
    /// <summary>
    /// Update a behavior record in the database by bhv_id
    /// </summary>
    /// <param name="id">behavior id</param>
    /// <param name="newBhv1">new behavior to update</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string UpdateBehaviorById(int id, ch_behaviors newBhv1) {
        string strSql1 = "SELECT COUNT(bhv_id) FROM ch_behaviors WHERE bhv_name = '" + newBhv1.bhv_name + "' AND bhv_id <>" + id;
        int num = Convert.ToInt32(Connect.MathAction(strSql1, "ch_behaviors"));

        if (num > 0)
            return "שם ההתנהגות כבר קיים במערכת";

        string strSql = "UPDATE ch_behaviors SET bhv_name='" + newBhv1.bhv_name + "', bhv_value=" + newBhv1.bhv_value + " WHERE bhv_id=" + id;
        Connect.DoAction(strSql, "ch_behaviors");

        return "";
    }

    /// <param name="name">Name of behavior(bhv_name)</param>
    /// <returns>The id By bhv_name or -1 if the name is not found</returns>
    public static int GetIdByBhvName(string name) {
        string strSql = "SELECT COUNT(bhv_id) FROM ch_behaviors WHERE bhv_name = '" + name + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_behaviors"));
        if (num > 0) {
            string strSql2 = "SELECT bhv_id FROM ch_behaviors WHERE bhv_name = '" + name + "'";
            DataSet ds = Connect.GetData(strSql2, "ch_behaviors");
            return Convert.ToInt32(ds.Tables["ch_behaviors"].Rows[0][0].ToString());
        }
        return -1;
    }
}