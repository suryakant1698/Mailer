using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate("register");
        if (!Page.IsValid)
            return;

        int ID;
        string CS = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            //supplying values to the stored procedure which will return -1 or -2 if the username or email has already been used respectively else will store the data in table and retun scope_ID 
            SqlCommand cmd = new SqlCommand("spRegistration", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", tbxUsername.Text);
            cmd.Parameters.AddWithValue("email", tbxEmail.Text);
            cmd.Parameters.AddWithValue("@fullname", tbxFullName.Text);
            cmd.Parameters.AddWithValue("@upassword", getHash(tbxPassword.Text));
            con.Open();
            ID = Convert.ToInt32(cmd.ExecuteScalar());
            string message;
            switch (ID)
            {
                case -1:
                    message = "Username already exists.\\nPlease choose a different username.";
                    break;
                case -2:
                    message = "Supplied email address has already been used.";
                    break;
                default:
                    message = "Registration successful";
                    SendActivationEmail(ID);
                    tbxEmail.Text = ""; tbxFullName.Text = ""; tbxPassword.Text = ""; tbxPassword.Text = ""; tbxUsername.Text = ""; tbxComfrimPassword.Text = "";
                    break;
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
        }
    }
    public void SendActivationEmail(int ID)
    {
        string constr = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        string activationCode = Guid.NewGuid().ToString();
        using (SqlConnection con = new SqlConnection(constr))//creating a unique activation code  and storing it in db for corresponding user
        {
            using (SqlCommand cmd = new SqlCommand("update tblUsers set activationCode=@ActivationCode  where username='" + tbxUsername.Text + "'"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ActivationCode", activationCode);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        using (MailMessage mm = new MailMessage("suryakant.rocky@gmail.com", tbxEmail.Text))
        {
            //sending the mail portion
            mm.Subject = "Account Activation";
            string body = "Hello " + tbxFullName.Text + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = http://localhost:54709/accountActivation.aspx?activationCode=" + activationCode + ">Click here to activate your account.</a>";
            body += "<br /><br />Thanks";
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("suryakant.rocky@gmail.com", "suryasharma");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
    private static string getHash(string text)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Page.Validate("login");
        if (!Page.IsValid)
            return;

        string message;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString)) //verifying username or email whatever he enters
        {
            con.Open();
            string checkuser = "select count(*) from tblUsers where (username='" + tbxLoginUsername.Text + "' or email='" + tbxLoginUsername.Text + "') and Verification=1";
            SqlCommand com = new SqlCommand(checkuser, con);
            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            if (temp == 1)
            {
                message = "";
                string checkPasswordQuerry = "select upassword from tblUsers where username='" + tbxLoginUsername.Text + "' or email='" + tbxLoginUsername.Text + "'"; //verifying password
                SqlCommand passcom = new SqlCommand(checkPasswordQuerry, con);
                string Password = passcom.ExecuteScalar().ToString();
                if (Password == getHash(tbxLoginPassword.Text.Trim()))
                {

                    string selectID = "select ID from tblUsers where username='" + tbxLoginUsername.Text + "' or email='" + tbxLoginUsername.Text + "'"; //verifying password

                    SqlCommand IDcom = new SqlCommand(selectID, con);
                    string ID = IDcom.ExecuteScalar().ToString();
                    Session["ID"] = ID;
                    Response.Redirect("userPages/userHomePage.aspx");
                }
                else message = "incorrect username or password";
            }
            else message = "incorrect username or password or account is not verified yet ";
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);

        }
    }
}