using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_users_gradesSvc
/// </summary>
public class ch_users_gradesSvc
{
    /// <summary>
    /// Add a grade to a user
    /// </summary>
    /// <param name="newUsrGrade">the user grade</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddUsrGrade(ch_users_grades newUsrGrade)
    {
        string queryIsExist = "SELECT COUNT(grd_id) FROM ch_users_grades ";
        queryIsExist += "WHERE usr_id = " + newUsrGrade.usr_Id + " AND grd_id = " + newUsrGrade.grd_Id;
        int num = Convert.ToInt32(Connect.MathAction(queryIsExist, "ch_users_grades"));
        if (num > 0)
        {
            return "לתלמיד כבר יש ציון במערכת";
        }

        string queryInsert = "INSERT INTO ch_users_grades";
        queryInsert += "(usr_id, grd_id, grd_num) ";
        queryInsert += "VALUES(" + newUsrGrade.usr_Id + ", " + newUsrGrade.grd_Id + ", " + newUsrGrade.grd_Num + ")";

        Connect.DoAction(queryInsert, "ch_users_grades");
        return "";
    }

    /// <param name="usrGrade">the user grade to get</param>
    /// <returns>DataRow of a user grade</returns>
    public static DataRow GetUserGrade(ch_users_grades usrGrade) {
        string queryGetUserGrade = "SELECT * FROM ch_users_grades WHERE usr_id = " + usrGrade.usr_Id + " AND grd_id = " + usrGrade.grd_Id;
        return Connect.GetData(queryGetUserGrade, "ch_users_grades").Tables[0].Rows[0];
    }

    /// <summary>
    /// Delete a user grade
    /// </summary>
    /// <param name="usrGrade">the user grade to delete</param>
    public static void DeleteUserGrade(ch_users_grades usrGrade) {
        string queryDeleteUserGrade = "DELETE * FROM ch_users_grades WHERE usr_id = " + usrGrade.usr_Id + " AND grd_id = " + usrGrade.grd_Id;
        Connect.DoAction(queryDeleteUserGrade, "ch_users_grades");
    }

    /// <summary>
    /// Delete a users grades filtered by test
    /// </summary>
    /// <param name="grd_id">the test id</param>
    public static void DeleteUsersGradesByTest(int grd_id) {
        string queryDeleteUsersGrades = "DELETE * FROM ch_users_grades WHERE grd_id = " + grd_id;
        Connect.DoAction(queryDeleteUsersGrades, "ch_users_grades");
    }

    /// <param name="grd_id">the test id of the test</param>
    /// <returns>DataSet of all users grades by a test</returns>
    public static DataSet GetUsersGrades(int grd_id) {
        string queryGetUserGrade = "SELECT * FROM ch_users_grades WHERE grd_id = " + grd_id;
        return Connect.GetData(queryGetUserGrade, "ch_users_grades");
    }
}