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
        if (Session["Username"] != null)
        {
            
            Label1.Text += Session["Username"].ToString();

        }
        else Response.Redirect("login.aspx");

    }

    protected void logoutButton_Click(object sender, EventArgs e)
    {
        Session["Username"] = null;
        Session["ID"] = null;
        Response.Redirect("login.aspx");
    }

    protected void btnCategoryAdder_Click(object sender, EventArgs e)
    {
        string ID = Session["ID"].ToString();
        string CS = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            con.Open();
            SqlCommand com = new SqlCommand("select count(*) from tblCategory where categoryName='"+tbxRecipientCategory.Text+"' and UserId='"+ID+"'",con);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            if(temp==1)
            {
                SqlCommand getID = new SqlCommand("select ID from tblCategory where categoryName='" + tbxRecipientCategory.Text + "' and UserId='" + ID + "'", con);
                string CategoryId = getID.ExecuteScalar().ToString();
                SqlCommand categoryAdder = new SqlCommand("insert into tblRecipients(name,email,CategoryId) values(@name,@email,@CategoryId)",con);
                categoryAdder.Parameters.AddWithValue("@name",tbxRecipientName);
                categoryAdder.Parameters.AddWithValue("@email",tbxRecipientEmail);
                categoryAdder.Parameters.AddWithValue("@CategoryId",CategoryId);
                categoryAdder.ExecuteNonQuery();
            }
            else ClientScript.RegisterStartupScript(GetType(), "alert", "alert(category does not exist);", true);
        }
    }
}