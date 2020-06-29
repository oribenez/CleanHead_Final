using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for Cities
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Cities : System.Web.Services.WebService {

    public Cities () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //Taken from: http://www.piba.gov.il/AuthorityUnits/Documents/yeshuv_150331.xml
    [WebMethod(Description = "Get DataSet of all cities in Israel")]
    public DataSet GetCities() {
        DataSet ds_xml = new DataSet();
        ds_xml.ReadXml(Server.MapPath("App_Data/yeshuv_150331.xml"));
        return ds_xml;
    }

    [WebMethod(Description = "Check if city exist")]
    public bool IsExist(string cty_name) {
        DataSet ds_xml = new DataSet();
        ds_xml.ReadXml(Server.MapPath("App_Data/yeshuv_150331.xml"));

        foreach (DataRow dr in ds_xml.Tables[0].Rows) {
            if (dr["שם_ישוב"].ToString() == cty_name) {
                return true;
            }
        }
        return false;
    }
}
