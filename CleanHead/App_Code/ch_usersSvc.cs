using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_usersSvc
/// </summary>
public class ch_usersSvc
{
    /// <summary>
    /// Check if the user credentials are exist in database
    /// </summary>
    /// <param name="usr1">the user that want to login</param>
    /// <returns>true if the exist, false if not exist</returns>
    public static bool Login(ch_users usr1)
    {
        string strSql = "SELECT COUNT(usr_id) FROM ch_users WHERE usr_identity = '" + usr1.usr_Identity + "' AND usr_password='" + usr1.usr_Password + "'";
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_users"));
        if (num > 0)
            return true;
        return false;
    }

    /// <summary>
    /// Add a new user to database
    /// </summary>
    /// <param name="usr1">the new user to add</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string AddUser(ch_users usr1)
    {
        if (IsUserExists(usr1))
            return "User already Registered!";

        string strSql = "INSERT INTO ch_users(usr_identity, usr_first_name, usr_last_name, usr_birth_date, usr_gender, cty_id, usr_address, usr_home_phone, usr_cellphone, sc_id, usr_email, usr_password, lvl_id)  ";
        strSql += "VALUES('" + usr1.usr_Identity + "','" + usr1.usr_First_Name + "', '" + usr1.usr_Last_Name + "', '" + usr1.usr_Birth_Date + "', '" + usr1.usr_Gender + "', " + usr1.cty_Id + ", '" + usr1.usr_Address + "', '" + usr1.usr_Home_Phone + "', '" + usr1.usr_Cellphone + "', " + usr1.sc_Id + ", '" + usr1.usr_Email + "', '" + usr1.usr_Password + "', " + usr1.lvl_Id + ")";
        Connect.DoAction(strSql, "ch_users");
        return "";
    }

    /// <summary>
    /// Change the user password
    /// </summary>
    /// <param name="usr_id">thge user id of the user</param>
    /// <param name="oldPass">the old password</param>
    /// <param name="newPass">the new password</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string ChangePass(int usr_id, string oldPass, string newPass) {
        //check
        string queryGetOldPass = "SELECT usr_password FROM ch_users WHERE usr_id = " + usr_id;
        string dbOldPass = Connect.GetData(queryGetOldPass, "ch_users").Tables[0].Rows[0]["usr_password"].ToString();

        if (dbOldPass != oldPass) {
            return "סיסמה ישנה לא נכונה";
        }

        //update
        string queryUpdatePass = "UPDATE ch_users SET usr_password = '" + newPass + "' WHERE usr_id = " + usr_id;
        Connect.DoAction(queryUpdatePass, "ch_users");
        return "";
    }
    /// <summary>
    /// Delete a user by his id
    /// </summary>
    /// <param name="id">user id of the user to delete</param>
    public static void DeleteUserById(int id)
    {
        string strSql1 = "DELETE * FROM ch_users WHERE usr_id=" + id;
        Connect.DoAction(strSql1, "ch_users");
    }

    /// <summary>
    /// Update user by a new one
    /// </summary>
    /// <param name="usr_id">user id of the user to update</param>
    /// <param name="newUsr1">the new user to update</param>
    /// <returns>string of an error or a string.Empty if the action is completed</returns>
    public static string UpdateUserById(int usr_id, ch_users newUsr1)
    {
        if (IsEmailExistInOtherUser(usr_id, newUsr1.usr_Email))
            return "Email already exist";

        if (IsIdentityExistInOtherUser(usr_id, newUsr1.usr_Identity))
            return "Identity already exist";

        string strSql = "UPDATE ch_users SET usr_identity='" + newUsr1.usr_Identity + "', usr_first_name='" + newUsr1.usr_First_Name + "', usr_last_name='" + newUsr1.usr_Last_Name + "', usr_birth_date='" + newUsr1.usr_Birth_Date + "', usr_gender='" + newUsr1.usr_Gender + "', cty_id=" + newUsr1.cty_Id + ", usr_address='" + newUsr1.usr_Address + "', usr_home_phone='" + newUsr1.usr_Home_Phone + "', usr_cellphone='" + newUsr1.usr_Cellphone + "', sc_id=" + newUsr1.sc_Id + ", usr_email='" + newUsr1.usr_Email + "', lvl_id='" + newUsr1.lvl_Id + "' WHERE usr_id=" + usr_id;
        Connect.DoAction(strSql, "ch_users");

        return "";
    }

    /// <summary>
    /// Check if email is exist in other user
    /// </summary>
    /// <param name="usr_id">user id of the user to not check</param>
    /// <param name="email">the email to check if exist</param>
    /// <returns>true if exist, false if not.</returns>
    public static bool IsEmailExistInOtherUser(int usr_id, string email)
    {
        int num; string strSql;
        //check in students
        strSql = "SELECT COUNT(usr_email) FROM ch_users WHERE usr_id <> " + usr_id + " AND usr_email = '" + email + "'";
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_users"));
        if (num > 0)
            return true;

        return false;
    }

    /// <summary>
    /// Check if identity is exist in other user
    /// </summary>
    /// <param name="usr_id">user id of the user to not check</param>
    /// <param name="identity">the identity to check if exist</param>
    /// <returns>true if exist, false if not.</returns>
    public static bool IsIdentityExistInOtherUser(int usr_id, string identity)
    {
        int num; string strSql;
        //check in students
        strSql = "SELECT COUNT(usr_identity) FROM ch_users WHERE usr_id <> " + usr_id + " AND usr_identity = '" + identity + "'";
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_users"));
        if (num > 0)
            return true;

        return false;
    }

    /// <summary>
    /// check the user type
    /// </summary>
    /// <param name="id">user id of the user to check</param>
    /// <returns>"stu" if student, "tch" if teacher, "crw" if crew</returns>
    public static string GetUsrType(int id)
    {
        int num; string strSql;
        //check in students
        strSql = "SELECT COUNT(usr_id) FROM ch_students WHERE usr_id = " + id;
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_students"));
        if (num > 0)
            return "stu";

        //check in teachers
        strSql = "SELECT COUNT(usr_id) FROM ch_teachers WHERE usr_id = " + id;
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_teachers"));
        if (num > 0)
            return "tch";

        //check in crew
        strSql = "SELECT COUNT(usr_id) FROM ch_crew WHERE usr_id = " + id;
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_crew"));
        if (num > 0)
            return "crw";

        return "usr";
    }

    /// <param name="type">user type(stu/tch/crw)</param>
    /// <returns>DataSet of all user filtered by user type.</returns>
    public static DataSet GetUsersByType(string type)
    {
        if (type == "crw")
        {
            string strSql = "SELECT * ";
            strSql += "FROM ch_users ";
            strSql += "WHERE usr_id IN (SELECT usr_id FROM ch_crew);";
            return Connect.GetData(strSql, "ch_users");
        }
        else if (type == "tch")
        {
            string strSql = "SELECT * ";
            strSql += "FROM ch_users ";
            strSql += "WHERE usr_id IN (SELECT usr_id FROM ch_teachers);";
            return Connect.GetData(strSql, "ch_users");
        }
        else if (type == "stu")
        {
            string strSql = "SELECT * ";
            strSql += "FROM ch_users ";
            strSql += "WHERE usr_id IN (SELECT usr_id FROM ch_students);";
            return Connect.GetData(strSql, "ch_users");
        }
        else//all users
        {
            string strSql = "SELECT * ";
            strSql += "FROM ch_users ";
            return Connect.GetData(strSql, "ch_users");
        }
        
    }

    /// <summary>
    /// check if the user exist
    /// </summary>
    /// <param name="usr1">the user to check</param>
    /// <returns>true if exist false if not</returns>
    public static bool IsUserExists(ch_users usr1)
    {
        string strSql; int num;
        strSql = "SELECT COUNT(usr_email) FROM ch_users WHERE usr_email = '" + usr1.usr_Email + "'";
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_users"));
        if (num > 0)
            return true;

        strSql = "SELECT COUNT(usr_id) FROM ch_users WHERE usr_identity = '" + usr1.usr_Identity + "'";
        num = Convert.ToInt32(Connect.MathAction(strSql, "ch_users"));
        if (num > 0)
            return true;

        return false;
    }

    /// <returns>Maximum id of all usr_id records</returns>
    public static int GetMaxId()
    {
        //check in students
        string strSql = "SELECT MAX(usr_id) FROM ch_users";
        object obj = Connect.MathAction(strSql, "ch_users");

        return Convert.ToInt32(obj);
    }

    /// <param name="usr_id">user id of the user to get</param>
    /// <returns>DataRow of a specific user by his id</returns>
    public static DataRow GetUserById(int usr_id)
    {
        string strSql = "SELECT * ";
        strSql += "FROM ((ch_users AS `usr` ";
        strSql += "INNER JOIN ch_cities AS `cty` ON cty.cty_id = usr.cty_id) ";
        strSql += "INNER JOIN ch_schools AS `sc` ON sc.sc_id = usr.sc_id) ";
        strSql += "WHERE usr_id=" + usr_id;
        return Connect.GetData(strSql, "ch_users").Tables[0].Rows[0];
    }

    /// <param name="id">user id of the user</param>
    /// <returns>string of a user fullname like: "Amit Cohen"</returns>
    public static string GetUserFullname(int id)
    {
        string strSql = "SELECT * FROM ch_users WHERE usr_id=" + id;
        DataSet ds = Connect.GetData(strSql, "ch_users");

        return ds.Tables[0].Rows[0]["usr_first_name"] + " " + ds.Tables[0].Rows[0]["usr_last_name"];
    }

    /// <param name="identity">the identity of the user to get</param>
    /// <returns>DataSet of a specific user by his identity</returns>
    public static DataSet GetUserByIdentity(string identity)
    {
        string strSql = "SELECT * FROM ch_users WHERE usr_identity='" + identity + "'";
        return Connect.GetData(strSql, "ch_users");
    }

    /// <param name="id">level id of the level</param>
    /// <returns>DataSet of all users by their level</returns>
    public static DataSet GetUsersByLvl(int id)
    {
        string strSql = "SELECT * FROM ch_users WHERE lvl_id =" + id;
        return Connect.GetData(strSql, "ch_users");
    }
    /// <summary>
    /// check if the user id is exist in database
    /// </summary>
    /// <param name="usr_id">the user id of the user</param>
    /// <returns> true if exist, false if not exist.</returns>
    public static bool IsExist(int usr_id)
    {
        //check in students
        string strSql = "SELECT COUNT(usr_id) FROM ch_users WHERE usr_id=" + usr_id;
        int num = Convert.ToInt32(Connect.MathAction(strSql, "ch_users"));
        if (num > 0)
            return true;
        return false;
    }
}