using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

/// <summary>
/// Summary description for ADO
/// </summary>
public class Connect
{
    static string GetConnection() {
        return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\ministry_education.accdb;Persist Security Info=True";
    }
    public static DataSet GetData(string strSql, string tblName) {
        OleDbConnection con = new OleDbConnection(GetConnection());
        OleDbCommand cmd = new OleDbCommand(strSql, con);
        DataSet ds = new DataSet();
        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
        adapter.Fill(ds, tblName);
        return ds;

    }
    public static int DoAction(string strSql, string tblName)
    {
        OleDbConnection con = new OleDbConnection(GetConnection());
        OleDbCommand cmd = new OleDbCommand(strSql, con);

        con.Open();
        int row = cmd.ExecuteNonQuery();
        con.Close();

        return row;
    }
    public static object MathAction(string strSql, string tblName)
    {
        OleDbConnection con = new OleDbConnection(GetConnection());
        OleDbCommand cmd = new OleDbCommand(strSql, con);

        con.Open();
        object result = cmd.ExecuteScalar();
        con.Close();

        return result;
    }
}