using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_messagesSvc
/// </summary>
public class ch_messagesSvc
{
    /// <summary>
    /// Get message id by message name
    /// </summary>
    /// <param name="name">message name</param>
    /// <returns>-1 if not exist or return the id if name exist</returns>
    public static DataSet GetMessageById(int id)
    {
        string strSql = "SELECT * FROM ch_messages WHERE msg_id =" + id;

        return Connect.GetData(strSql, "ch_messages");
    }
    /// <returns>DataSet of all messages records from database</returns>
    public static DataSet GetMessages()
    {
        string strSql = "SELECT * FROM ch_messages";

        return Connect.GetData(strSql, "ch_messages");
    }
    /// <summary>
    /// Add new message to the database
    /// </summary>
    /// <param name="msg1">ch_messages object</param>
    public static void NewUsrMsg(ch_messages msg1)
    {
        string strSql1 = "INSERT INTO ch_messages(msg_date, msg_title, msg_content, msg_checked)  ";
        strSql1 += "VALUES('" + msg1.msg_Date + "', '" + msg1.msg_Title + "', '" + msg1.msg_Content + "', " + Convert.ToInt32(msg1.msg_Checked) + ")";
        Connect.DoAction(strSql1, "ch_messages");

        string strSql2 = "SELECT LAST(msg_id) FROM ch_messages";
        int maxId = Convert.ToInt32(Connect.MathAction(strSql2, "ch_messages"));

        string strSql3 = "INSERT INTO ch_users_messages(msg_id, msg_sender_id, msg_reciver_id)  ";
        strSql3 += "VALUES(" + maxId + ", '" + msg1.msg_Sender_Id + "', '" + msg1.msg_Reciver_Id + "')";
        Connect.DoAction(strSql3, "ch_users_messages");
    }
    /// <param name="msg_id">message id of a specific message you want to get</param>
    /// <returns>A specific message by msg_id</returns>
    public static DataRow GetMessage(int msg_id) {
        string strSql = "SELECT ";
        strSql += "msg.msg_id AS `קוד הודעה`, ";
        strSql += "msg.msg_date AS `תאריך`, ";
        strSql += "msg.msg_title AS `כותרת`, ";
        strSql += "msg.msg_content AS `הודעה`, ";
        strSql += "msg.msg_checked AS `נקראה` ";
        strSql += "FROM ch_messages AS `msg` ";
        strSql += "WHERE msg.msg_id = " + msg_id + " ";

        return Connect.GetData(strSql, "ch_messages").Tables[0].Rows[0];
    }
    /// <param name="id">user id of user you want to specify</param>
    /// <returns>All recived messages of a specific user</returns>
    public static DataSet GetMessagesRecivedGV(int id)
    {
        string strSql = "SELECT ";
        strSql += "msg.msg_id AS `קוד הודעה`, ";
        strSql += "msg.msg_date AS `תאריך`, ";
        strSql += "msg.msg_title AS `כותרת`, ";
        strSql += "(usr_sender.usr_first_name & ' ' & usr_sender.usr_last_name) AS `שולח`, ";
        strSql += "msg.msg_content AS `הודעה`, ";
        strSql += "msg.msg_checked AS `נקראה` ";
        strSql += "FROM (ch_users_messages AS `ref` ";
        strSql += "INNER JOIN ch_messages AS `msg` ON ref.msg_id = msg.msg_id) ";
        strSql += "INNER JOIN ch_users AS `usr_sender` ON ref.msg_sender_id = usr_sender.usr_id ";
        strSql += "WHERE ref.msg_reciver_id = " + id + " ";
        strSql += "AND NOT EXISTS(SELECT * FROM ch_deleted_messages AS `del_msg` WHERE msg.msg_id = del_msg.msg_id AND ref.msg_reciver_id = del_msg.usr_id) ";
        strSql += "ORDER BY msg.msg_checked DESC, msg.msg_date DESC";

        return Connect.GetData(strSql, "ch_users_messages");
    }
    /// <param name="id">user id of user you want to specify</param>
    /// <returns>All sent messages of a specific user</returns>
    public static DataSet GetMessagesSentGV(int id) {
        string strSql = "SELECT ";
        strSql += "msg.msg_id AS `קוד הודעה`, ";
        strSql += "msg.msg_date AS `תאריך`, ";
        strSql += "msg.msg_title AS `כותרת`, ";
        strSql += "(usr_reciver.usr_first_name & ' ' & usr_reciver.usr_last_name) AS `מקבל`, ";
        strSql += "msg.msg_content AS `הודעה`, ";
        strSql += "msg.msg_checked AS `נקראה` ";
        strSql += "FROM (ch_users_messages AS `ref` ";
        strSql += "INNER JOIN ch_messages AS `msg` ON ref.msg_id = msg.msg_id) ";
        strSql += "INNER JOIN ch_users AS `usr_reciver` ON ref.msg_reciver_id = usr_reciver.usr_id ";
        strSql += "WHERE ref.msg_sender_id = " + id + " ";
        strSql += "AND NOT EXISTS(SELECT * FROM ch_deleted_messages AS `del_msg` WHERE msg.msg_id = del_msg.msg_id AND ref.msg_sender_id = del_msg.usr_id) ";
        strSql += "ORDER BY msg.msg_checked DESC, msg.msg_date DESC";

        return Connect.GetData(strSql, "ch_users_messages");
    }
    /// <summary>
    /// Update the message status to read 
    /// </summary>
    /// <param name="id">the message id</param>
    public static void ChangeMsgStatus(int id)
    {
        string str = "UPDATE ch_messages SET msg_checked=1 WHERE msg_id=" + id;
        Connect.DoAction(str, "ch_messages");
    }
}