using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_homework
/// </summary>
public class ch_homework
{
    public int les_Id { get; set; } // מזהה השיעור בו ניתן שיעורי הבית
    public string hw_Txt { get; set; } // תוכן שיעורי הבית
    public string hw_Deadlinedate { get; set; }  // תאריך הגשת שיעורי הבית
    public int hr_Id { get; set; } // שעת הגשת שיעורי הבית

    /// <summary>
    /// Initializes a new instance of the ch_homework class
    /// </summary>
    /// <param name="les_Id">מזהה השיעור בו ניתן שיעורי הבית</param>
    /// <param name="hw_Txt">תוכן שיעורי הבית</param>
    /// <param name="hw_Deadlinedate">תאריך הגשת שיעורי הבית</param>
    /// <param name="hr_Id">שעת הגשת שיעורי הבית</param>
    public ch_homework(int les_Id, string hw_Txt, string hw_Deadlinedate, int hr_Id)
	{
        this.les_Id = les_Id;
        this.hw_Txt = hw_Txt;
        this.hw_Deadlinedate = hw_Deadlinedate;
        this.hr_Id = hr_Id;
	}
}