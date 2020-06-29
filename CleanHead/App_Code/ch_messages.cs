using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_messages
/// </summary>
public class ch_messages
{
    public string msg_Date { get; set; } // תאריך שליחת ההודעה
    public string msg_Sender_Id { get; set; } // מזהה משתמש של שולח ההודעה
    public string msg_Reciver_Id { get; set; } // מזהה משתמש של מקבל ההודעה
    public string msg_Title { get; set; } // כותרת ההודעה
    public string msg_Content { get; set; } // תוכן ההודעה
    public bool msg_Checked { get; set; } // האם ההודעה נקראה
}