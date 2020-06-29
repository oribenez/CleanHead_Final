using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ch_behavior
/// </summary>
public class ch_behaviors
{
    public int bhv_id { get; set; } // מזהה התנהגות
    public string bhv_name { get; set; } // שם/סוג ההתנהגות
    public int bhv_value { get; set; } // שווי ההתנהגות

    /// <summary>
    /// Initializes a new instance of the ch_behaviors class
    /// </summary>
    public ch_behaviors() { }
    /// <summary>
    /// Initializes a new instance of the ch_behaviors class
    /// </summary>
    /// <param name="bhv_id">behavior id</param>
    public ch_behaviors(int bhv_id) {
        DataRow drBhv = ch_behaviorsSvc.GetBehavior(bhv_id);

        this.bhv_name = drBhv["bhv_name"].ToString();
        this.bhv_value = Convert.ToInt32(drBhv["bhv_value"]);
    }
    /// <summary>
    /// Initializes a new instance of the ch_behaviors class
    /// </summary>
    /// <param name="bhv_id">מזהה התנהגות</param>
    /// <param name="bhv_name">שם/סוג ההתנהגות</param>
    /// <param name="bhv_value">שווי ההתנהגות</param>
    public ch_behaviors(int bhv_id, string bhv_name, int bhv_value)
	{
        this.bhv_id = bhv_id;
        this.bhv_name = bhv_name;
        this.bhv_value = bhv_value;
	}
}