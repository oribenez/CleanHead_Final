using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for HolidaysVacations
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class HolidaysVacations : System.Web.Services.WebService {

    public HolidaysVacations () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "Get DataSet of all religious")]
    public DataSet GetReligious() {
        string queryHldVac = "SELECT * FROM me_religious";
        return Connect.GetData(queryHldVac, "me_religious");
    }

    [WebMethod(Description = "Get DataSet of all holidays vacations")]
    public DataSet GetHolidaysVacations() {
        string queryHldVac = "SELECT * FROM me_holidays_vacations ORDER BY hld_vac_start_date";
        return Connect.GetData(queryHldVac, "me_holidays_vacations");
    }

    [WebMethod(Description = "Get DataSet of holidays vacations by religious")]
    public DataSet GetHolidaysVacationsByRlgId(int rlg_id) {
        string queryHldVac = "SELECT * FROM me_holidays_vacations AS `hld_vac` ";
        queryHldVac += "INNER JOIN me_religious AS `rlg` ON rlg.rlg_id = hld_vac.rlg_id ";
        queryHldVac += "WHERE rlg.rlg_id = " + rlg_id + " OR rlg.rlg_id = 5 ";
        queryHldVac += "ORDER BY hld_vac_start_date";
        return Connect.GetData(queryHldVac, "me_holidays_vacations");
    }

    [WebMethod(Description = "Get DataSet of holidays vacations of selected day")]
    public DataSet GetHolidaysVacationsByDate(DateTime dt) {
        string queryHldVac = "SELECT * FROM me_holidays_vacations ";
        queryHldVac += "WHERE hld_vac_start_date <= #" + dt.ToString("yyyy/MM/dd") + "# AND hld_vac_end_date >= #" + dt.ToString("yyyy/MM/dd") + "#;";
        return Connect.GetData(queryHldVac, "me_holidays_vacations");
    }

    [WebMethod(Description = "Get DataSet of holidays vacations of selected day By Religion")]
    public DataSet GetHolidaysVacationsByDateByRlgId(DateTime dt, int rlg_id) {
        string queryHldVac = "SELECT * FROM me_holidays_vacations ";
        queryHldVac += "INNER JOIN me_religious AS `rlg` ON rlg.rlg_id = hld_vac.rlg_id ";
        queryHldVac += "WHERE hld_vac_start_date <= #" + dt.ToString("yyyy/MM/dd") + "# AND hld_vac_end_date >= #" + dt.ToString("yyyy/MM/dd") + "# AND rlg.rlg_id = " + rlg_id + ";";
        return Connect.GetData(queryHldVac, "me_holidays_vacations");
    }
    
}
