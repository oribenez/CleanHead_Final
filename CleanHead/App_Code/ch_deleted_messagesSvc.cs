using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_deleted_messagesSvc
/// </summary>
public class ch_deleted_messagesSvc
{
    /// <summary>
    /// Adds a new record of deleted message to the database.
    /// </summary>
    /// <param name="del_msg">the message to delete</param>
    /// <returns>true if the message deleted now. false if already deleted in the past.</returns>
    public static bool Add(ch_deleted_messages del_msg){
        if (IsExist(del_msg)) {
            return false;
        }

        string strSql = "INSERT INTO ch_deleted_messages(msg_id, usr_id) ";
        strSql += "VALUES(" + del_msg.msg_Id + ", " + del_msg.usr_Id + ")";
        Connect.DoAction(strSql, "ch_deleted_messages");

        return true;
    }
    /// <summary>
    /// Checks if ch_deleted_messages already exist in database
    /// </summary>
    /// <param name="del_msg"></param>
    /// <returns>return true if ch_deleted_messages is exist in database. false if not exist.</returns>
    public static bool IsExist(ch_deleted_messages del_msg){
        string queryIsExist = "SELECT COUNT(msg_id) FROM ch_deleted_messages WHERE msg_id=" + del_msg.msg_Id + " AND usr_id=" + del_msg.usr_Id;
        int k = Convert.ToInt32(Connect.MathAction(queryIsExist, "ch_deleted_messages"));
        if (k > 0)
            return true;

        return false;
    }
}