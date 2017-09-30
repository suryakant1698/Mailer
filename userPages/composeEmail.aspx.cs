using System;
using System.Collections;
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
        if (Session["ID"] == null) Response.Redirect("../Login.aspx");
        else
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


                string tooltip = string.Empty;
                foreach (RepeaterItem i in rptrCategory.Items)
                {
                    CheckBoxList cb = (CheckBoxList)i.FindControl("cblRecipients");
                    foreach (ListItem li in cb.Items)
                    {
                        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
                        {
                            con.Open();
                            SqlCommand addToolTip = new SqlCommand("select email from tblRecipients where ID='" + li.Value + "'", con);
                            SqlDataReader rd = addToolTip.ExecuteReader();
                            while (rd.Read())
                                tooltip=rd.ToString();
                            // li.Attributes.Add("title",tooltip);
                            li.Attributes["title"] = tooltip;
                        }
                    }

                }
            }

        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        Page.Validate("mailCredentials");
        if (!Page.IsValid)
            return;
        string recipientIDs = "";
        int recipientCount = 1;
        foreach (RepeaterItem i in rptrCategory.Items)
        {
            CheckBoxList cb = (CheckBoxList)i.FindControl("cblRecipients");
            foreach (ListItem li in cb.Items)
            {
                if (li.Selected)
                {
                    sendEmail(Convert.ToInt32(li.Value));
                    recipientIDs += li.Value;
                    recipientIDs += ",";
                }
            }

        }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            con.Open();
            SqlCommand getTemplateID = new SqlCommand("select ID from tblTemplates where filePath='" + ddlTemplateSelector.SelectedItem.Value + "'", con);
            int templateID = Convert.ToInt32(getTemplateID.ExecuteScalar());
            SqlCommand saveEmail = new SqlCommand("insert into tblSentMails(body,recipientId,templateID) values(@body,@recipientID,@templateID)", con);
            saveEmail.Parameters.AddWithValue("@body", tbxMailBody.Text);
            saveEmail.Parameters.AddWithValue("@recipientID", recipientIDs);
            saveEmail.Parameters.AddWithValue("templateID", templateID);
            saveEmail.ExecuteNonQuery();
            tbxMailBody.Text = "";
            tbxSubject.Text = "";
        }
        string message = "all the mails were sent succesfuly";
        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
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