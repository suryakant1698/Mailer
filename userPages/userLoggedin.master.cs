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
        if (Session["username"] != null)
        {
            //header portion for displaying user's name
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
            {
                string username = Session["username"].ToString();
                con.Open();
                SqlCommand com = new SqlCommand("select fullname from tblUsers where username='" + username + "' or email='" + username + "'", con);
                string fullname = com.ExecuteScalar().ToString();
               lblusername.Text = "welcome " + fullname;
            }
          
        }
        else Response.Redirect("../Login.aspx");
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        Session["ID"] = null;
        Response.Redirect("../Login.aspx");
    }




}
