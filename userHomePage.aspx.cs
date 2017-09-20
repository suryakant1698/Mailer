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
        //all the checks have been done for the corresponding user only i.e. two users can enter the same recipient's info twice
        if (Session["username"] != null)
        {
            //header portion for displaying user's name
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
            {
                string username = Session["username"].ToString();
                con.Open();
                SqlCommand com = new SqlCommand("select fullname from tblUsers where username='" + username + "' or email='" + username + "'", con);
                string fullname = com.ExecuteScalar().ToString();
                Label1.Text = "wlecome " + fullname;
            }
        }
        else Response.Redirect("Login.aspx");//in case someone enters the address of the page

    }


    protected void btnCategoryAdder_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            //procedure returns -1 if category is already added otherwise inserts the provided values in corresponding columns
            string message;
            con.Open();
            string ID = Session["ID"].ToString();
            SqlCommand cmd = new SqlCommand("spInsertCategory", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", ID);
            cmd.Parameters.AddWithValue("@categoryName", tbxCategoryName.Text);
            int categoryID = Convert.ToInt32(cmd.ExecuteScalar());
            switch (categoryID)
            {
                case -1:
                    message = "Category already registered" + ID;
                    break;
                default:
                    message = "Category AddedSuccesfuly";
                    break;

            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
        }


    }

    protected void RecipientAdder_Click(object sender, EventArgs e)
    {

        string message;
        string ID = Session["ID"].ToString();
        string CS = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            con.Open();
            SqlCommand com = new SqlCommand("select count(*) from tblCategory where categoryName='" + tbxRecipientCategory.Text + "' and UserID='" + ID + "'", con);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            if (temp == 1)
            {
                SqlCommand getID = new SqlCommand("select ID from tblCategory where categoryName='" + tbxRecipientCategory.Text + "' and userID='" + ID + "'", con);
                string CategoryID = getID.ExecuteScalar().ToString();
  s              SqlCommand checkRecipientEmail = new SqlCommand("select count(*) from tblRecipients where email='" + tbxRecipientEmail.Text + "' and categoryID='" + CategoryID + "' ", con);
                int unvalidEmail = Convert.ToInt32(checkRecipientEmail.ExecuteScalar().ToString());
                if (unvalidEmail == 1) message = " you already registered a user with this email names can repeat but emails cant";
                else
                {
                    SqlCommand categoryAdder = new SqlCommand("insert into tblRecipients(name,email,categoryID) values(@name,@email,@categoryID)", con);
                    categoryAdder.Parameters.AddWithValue("@name", tbxRecipientName.Text);
                    categoryAdder.Parameters.AddWithValue("@email", tbxRecipientEmail.Text);
                    categoryAdder.Parameters.AddWithValue("@categoryID", CategoryID);
                    categoryAdder.ExecuteNonQuery();
                    message = "recipient added succesfuly";
                }
            }
            else message = "category does not exist ensure you entered correctly or add a new one";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
        }
    }


    protected void logoutButton_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        Session["ID"] = null;
        Response.Redirect("Login.aspx");
    }
}


