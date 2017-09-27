using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        if (!IsPostBack)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand("select filePath,name from tblTemplates", con);
                con.Open();
                ddlTemplateSelector.DataSource = com.ExecuteReader();
                ddlTemplateSelector.DataTextField = "name";
                ddlTemplateSelector.DataValueField = "filePath";
                ddlTemplateSelector.DataBind();
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
            {
                SqlDataAdapter da1 = new SqlDataAdapter("select categoryName,ID from tblCategory where userID='" + Session["ID"].ToString() + "'", con);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                rptrCategory.DataSource = ds1;
                rptrCategory.DataBind();
            }
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem i in rptrCategory.Items)
        {
            CheckBoxList cb = (CheckBoxList)i.FindControl("cblRecipients");
            foreach (ListItem li in cb.Items)
            {
                if (li.Selected)
                {
                    sendEmail(Convert.ToInt32(li.Value));
                }

            }

        }
    }

    protected void sendEmail(int recipientID)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            con.Open();
            SqlCommand getRecipientName = new SqlCommand("select name from tblRecipients where ID='" + recipientID + "'", con);
            string recipientName = getRecipientName.ExecuteScalar().ToString();
            SqlCommand getRecipientEmail = new SqlCommand("select email from tblRecipients where ID='" + recipientID + "'", con);
            string recipientEmail = getRecipientEmail.ExecuteScalar().ToString();
            SqlCommand getUserEmail = new SqlCommand("select email from tblUsers where ID='" + Convert.ToInt32(Session["ID"]) + "'", con);
            con.Close();
            con.Open();
            string userEmail = getUserEmail.ExecuteScalar().ToString();
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(ddlTemplateSelector.SelectedItem.Value)))
            {
                //changing the value of the placeholders in template as per recipient's information
                body = reader.ReadToEnd();
                body = body.Replace("{UserName}", recipientName);
                body = body.Replace("{body}", tbxMailBody.Text);
            }
            using (MailMessage mail = new MailMessage(userEmail, recipientEmail))
            {
                mail.Subject = tbxSubject.Text;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(userEmail, tbxPassword.Text);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mail);
            }
        }
    }
}