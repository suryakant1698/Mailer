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
        if (newActivationCode == null) Response.Redirect("appHomePage.aspx");
        else
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand checkActivationCode = new SqlCommand("select count(*) from tblUsers where activationCode='" + newActivationCode + "' and Verification=1", con);
                int temp = Convert.ToInt32(checkActivationCode.ExecuteScalar());
                if (temp == 1) lblMessage.Text = "Your account is already verified you do not need to click this link again ";
                else
                {
                    string checkValidSourece = "update tblUsers set Verification=1 where activationCode=@activationCode";
                    SqlCommand cmd = new SqlCommand(checkValidSourece);
                    SqlDataAdapter sda = new SqlDataAdapter();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@activationCode", newActivationCode);
                    cmd.Connection = con;

                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Account activation succesful";
                }
            }
        }
    }

protected void redirect(object sender, EventArgs e)
{
    Response.Redirect("Login.aspx");
}
}