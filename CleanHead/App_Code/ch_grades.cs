using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ch_grades
/// </summary>
public class ch_grades
{
    public int les_Id { get; set; } // מזהה השיעור שבו התקיים המבחן
    public string grd_Name { get; set; } // שם המבחן
    public string grd_Date { get; set; } // תאריך ביצוע המבחן

    /// <summary>
    /// Initializes a new instance of the ch_grades class
    /// </summary>
    public ch_grades() { }

    /// <summary>
    /// Initializes a new instance of the ch_grades class
    /// </summary>
    /// <param name="les_id">מזהה השיעור שבו התקיים המבחן</param>
    /// <param name="grd_name">שם המבחן</param>
    /// <param name="grd_date">תאריך ביצוע המבחן</param>
    public ch_grades(int les_id, string grd_name, string grd_date) {
        this.les_Id = les_id;
        this.grd_Name = grd_name;
        this.grd_Date = grd_date;
    }
}