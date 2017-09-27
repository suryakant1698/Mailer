﻿using System;
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
        if (Session["ID"] == null)
            Response.Redirect("../Login.aspx");
        if (!IsPostBack)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand("select ID,name from tblTemplates", con);
                con.Open();
                ddlTemplateSelector.DataSource = com.ExecuteReader();
                ddlTemplateSelector.DataTextField = "name";
                ddlTemplateSelector.DataValueField = "ID";
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
                    sendEmail(li.Value);
            }
        }
    }
    protected void sendEmail(string recipientID)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            con.Open();
            SqlCommand getTemplatePath = new SqlCommand("select filePath from tblTemplates where Id='" + ddlTemplateSelector.SelectedItem.Value + "'", con);//fetching template path of the selected item
            string templatePath = getTemplatePath.ExecuteScalar().ToString();
            SqlCommand getRecipientName = new SqlCommand("select name from tblRecipients where ID='" + recipientID + "'", con);
            string recipientName = getRecipientName.ExecuteScalar().ToString();
            SqlCommand getRecipientEmail = new SqlCommand("select email from tblRecipients where ID='" + recipientID + "'", con);
            string recipientEmail = getRecipientName.ExecuteScalar().ToString();
            SqlCommand getUserEmail = new SqlCommand("select email from tblUsers where ID='" + Session["ID"].ToString() + "'", con);
            string userEmail = getUserEmail.ExecuteScalar().ToString();
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath(templatePath)))
            {
                //changing the value of the placeholders in template as per recipient's information
                body = reader.ReadToEnd();
                body = body.Replace("{UserName}", recipientName);
                body = body.Replace("{body}", tbxMailBody.Text);
            }
            using (MailMessage mail = new MailMessage())
            {

                //mail.From = userEmail;
                //mail.To=recipientEmail;
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

//PREVIOUS CODE HAS BEEN PUT INTO COMMENTS


//    protected void btnSend_Click(object sender, EventArgs e)
//    {
//        Page.Validate("mailCredentials");
//        if (!Page.IsValid)
//            return;
//        string message;
//        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
//        {

//            con.Open();
//            string templateID = ddlTemplateSelector.SelectedItem.Value;
//            SqlCommand getTemplatePath = new SqlCommand("select filePath from tblTemplates where ID='" + templateID + "'", con);//fetching template path of the selected item
//            string templatePath = getTemplatePath.ExecuteScalar().ToString();
//            SqlCommand getRecpientID = new SqlCommand("select ID from tblRecipients where email='" + tbxRecipientEmail.Text + "'", con);
//            int recipientID = Convert.ToInt32(getRecpientID.ExecuteScalar().ToString());
//            SqlCommand checkEmail = new SqlCommand("select count(*) from tblRecipients where email='" + tbxRecipientEmail.Text + "'", con);//ensuring email of the recipient is already added
//            int temp = Convert.ToInt32(checkEmail.ExecuteScalar().ToString());

//            if (temp == 1)
//            {
//                SqlCommand selectName = new SqlCommand("select name from tblRecipients where email='" + tbxRecipientEmail.Text + "'", con);//fetching name from te
//                string RecipientName = selectName.ExecuteScalar().ToString();

//                string body = string.Empty;
//                using (StreamReader reader = new StreamReader(Server.MapPath(templatePath)))
//                {
//                    //changing the value of the placeholders in template as per recipient's information
//                    body = reader.ReadToEnd();
//                    body = body.Replace("{UserName}", RecipientName);
//                    body = body.Replace("{body}", tbxMailBody.Text);
//                }
//                SqlCommand getSenderEmail = new SqlCommand("select email from tblUsers where ID='" + Session["ID"].ToString() + "'", con);
//                string senderEmail = getSenderEmail.ExecuteScalar().ToString();
//                sendEmail(senderEmail, body);
//                SqlCommand insertMail = new SqlCommand("insert into tblSentMails(body,RecipientID,templateID) values(@body,@recipientID,@templateID)", con);
//                insertMail.Parameters.AddWithValue("@body", tbxMailBody.Text);//storing the sent mail in database
//                insertMail.Parameters.AddWithValue("@recipientID", recipientID);
//                insertMail.Parameters.AddWithValue("@templateID", Convert.ToInt32(templateID));
//                insertMail.ExecuteNonQuery();
//                message = "mail  was sent succesfuly";

//            }
//            else message = "Recipient's email Invalid check again or add a new one";
//            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
//        }

//    }
//    protected void sendEmail(string senderEmail, string body)
//    {
//        using (MailMessage mail = new MailMessage(senderEmail, tbxRecipientEmail.Text))
//        {
//            mail.Subject = tbxSubject.Text;
//            mail.Body = body;
//            mail.IsBodyHtml = true;
//            SmtpClient smtp = new SmtpClient();
//            smtp.Host = "smtp.gmail.com";
//            smtp.EnableSsl = true;
//            NetworkCredential NetworkCred = new NetworkCredential(senderEmail, tbxPassword.Text);
//            smtp.UseDefaultCredentials = true;
//            smtp.Credentials = NetworkCred;
//            smtp.Port = 587;
//            smtp.Send(mail);

//        }
//    }
//}

