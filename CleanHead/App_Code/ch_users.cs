using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_users
/// </summary>
public class ch_users
{
    public string usr_Identity { get; set; } // ת.ז. של המשתמש
    public string usr_First_Name { get; set; } // שם פרטי של המשתמש
    public string usr_Last_Name { get; set; } // שם משפחה של המשתמש
    public string usr_Birth_Date { get; set; } // תאריך לידה של המשתמש
    public string usr_Gender { get; set; } // מגדר של המשתמש
    public int cty_Id { get; set; } // מזהה עיר
    public string usr_Address { get; set; } // כתובת מגורים של המשתמש
    public string usr_Home_Phone { get; set; } // מספר טלפון בבית של המשתמש
    public string usr_Cellphone { get; set; } // מספר טלפון נייד של המשתמש
    public int sc_Id { get; set; } // מזהה בית ספר
    public string usr_Email { get; set; } // אימייל של המשתמש
    public string usr_Password { get; set; } // סיסמה של המשתמש
    public int lvl_Id { get; set; } // מזהה רמת גישה של המשתמש באתר
}