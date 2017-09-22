using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string newActivationCode = Request.QueryString["activationCode"];
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            string checkValidSourece = "update tblUsers set Verification=1 where activationCode=@activationCode";
            using (SqlCommand cmd = new SqlCommand(checkValidSourece))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@activationCode", newActivationCode);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
    protected void redirect(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}