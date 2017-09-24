using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userPages_compose : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string message;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            con.Open();
            SqlCommand checkEmail = new SqlCommand("select count(*) from tblRecipients where email=@email", con);
            checkEmail.Parameters.AddWithValue("@email", tbxRecipientEmail.Text);
            int temp = Convert.ToInt32(checkEmail.ExecuteScalar().ToString());
            if (temp == 1)
            {
                SqlCommand selectName = new SqlCommand("select name from tblRecipients where email=@email", con);
                selectName.Parameters.AddWithValue("@email",tbxRecipientEmail.Text);
                string RecipientName = selectName.ExecuteScalar().ToString();
                
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/htmlTemplates/template1.html")))
                {
                    body = reader.ReadToEnd();
                    body = body.Replace("{UserName}", RecipientName);
                    body = body.Replace("{body}",tbxMailBody.Text);
                }
                SqlCommand getSenderEmail = new SqlCommand("select email from tblUsers where ID=@ID",con);
                getSenderEmail.Parameters.AddWithValue("@ID",Convert.ToInt32(Session["ID"].ToString()));
                string senderEmail = getSenderEmail.ExecuteScalar().ToString();
                sendEmail(senderEmail,body);
                message = "mail  was sent succesfuly";
            }
            else message = "Recipient's email Invalid check again or add a new one";
        }

    }
    protected void sendEmail(string senderEmail, string body)
    {
        using (MailMessage mail = new MailMessage(senderEmail, tbxRecipientEmail.Text))
        {
            mail.Subject = tbxSubject.Text;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(senderEmail, tbxPassword.Text);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mail);

        }
    }
}