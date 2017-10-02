using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public object S { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
   
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {

        Session["ID"] = null;
        Response.Redirect("../appHomePage.aspx");
    }

   
}
