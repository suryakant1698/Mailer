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

public partial class practiceRegister : System.Web.UI.Page
{
    private object nager;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int ID;
        string CS = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            //supplying values to the stored procedure which will return -1 or -2 if the username or email has already been used respectively else will store the data in table and retun scope_ID 
            SqlCommand cmd = new SqlCommand("spCheckEmailUsername1", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", tbxUsername.Text);
            cmd.Parameters.AddWithValue("email", tbxEmail.Text);
            cmd.Parameters.AddWithValue("@fullname", tbxFullName.Text);
            cmd.Parameters.AddWithValue("@upassword", encryption(tbxPassword.Text));
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
                    Response.Redirect("register.aspx");
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
    public string encryption(String password)
    {
        //encypting the given password before storing into database
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] encrypt;
        UTF8Encoding encode = new UTF8Encoding();
        encrypt = md5.ComputeHash(encode.GetBytes(password));
        StringBuilder encryptdata = new StringBuilder();
        for (int i = 0; i < encrypt.Length; i++)
        {
            encryptdata.Append(encrypt[i].ToString());
        }
        return encryptdata.ToString();
    }
}




