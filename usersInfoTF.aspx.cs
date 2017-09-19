using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*using (SqlConnection con = new SqlConnection("data source=.; database=[C:\Users\dell\Documents\Visual Studio 2015\WebSites\WebSite2\App_Data\Database2.mdf]; integrated security=SSPI"))
        {
            SqlDataAdapter sde = new SqlDataAdapter("Select * from tblUsers", con);
            DataSet ds = new DataSet();
            sde.Fill(ds);
            DataGrid1.DataSource = ds;
            DataGrid1.DataBind();
        }*/
    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}