using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;

public partial class users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLoggedIn"] != null)
        {

            Label1.Text += Session["UserLoggedIn"].ToString();

        }
        else Response.Redirect("login.aspx");

    }

    protected void logoutButton_Click(object sender, EventArgs e)
    {
        Session["UserLoggedIn"] = null;
        Response.Redirect("login.aspx");
    }

    protected void btnCategoryAdder_Click(object sender, EventArgs e)
    {
        string username = Session["UserLoggedIn"].ToString();
        string CS = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            SqlCommand cmd=
        }
    }
}