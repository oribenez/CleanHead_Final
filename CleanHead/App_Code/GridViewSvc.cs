using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for GridViewSvc
/// </summary>
public class GridViewSvc
{
    /// <summary>
    /// Update the data in the table using DataSet.
    /// </summary>
    /// <param name="ds">The DataSet of the GridView to bind</param>
    /// <param name="GV">The GridView you want to bind</param>
    public static void GVBind(DataSet ds, GridView GV)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            GV.DataSource = ds;
            GV.DataBind();
        }
        else
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            GV.DataSource = ds;
            GV.DataBind();
            int columncount = GV.Rows[0].Cells.Count;
            GV.Rows[0].Cells.Clear();
            GV.Rows[0].Cells.Add(new TableCell());
            GV.Rows[0].Cells[0].ColumnSpan = columncount;
            GV.Rows[0].Cells[0].Text = "לא נמצאו נתונים.";
        }

    }
}