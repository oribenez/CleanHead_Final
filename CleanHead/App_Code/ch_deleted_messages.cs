using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_deleted_messages
/// </summary>
public class ch_deleted_messages
{
    public int msg_Id { get; set; } // מזהה ההודעה
    public int usr_Id { get; set; } // מזהה המשתמש שמחק את ההודעה

    /// <summary>
    /// Initializes a new instance of the ch_deleted_messages class
    /// </summary>
	public ch_deleted_messages(){}
    /// <summary>
    /// Initializes a new instance of the ch_deleted_messages class
    /// </summary>
    /// <param name="msg_id">message identity</param>
    /// <param name="usr_id">user that delete the message</param>
    public ch_deleted_messages(int msg_id, int usr_id){
        msg_Id = msg_id;
        usr_Id = usr_id;
    }
}