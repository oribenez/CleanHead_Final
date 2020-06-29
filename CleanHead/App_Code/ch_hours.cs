using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_crew
/// </summary>
public class ch_hours
{
    public string hr_Name { get; set; } // שם השעה
    public string hr_Start_Time { get; set; } // תחילת השעה
    public string hr_End_Time { get; set; } // סיום השעה
    public int sc_Id { get; set; } // מזהה בית הספר המשוייך לשעה זו
}