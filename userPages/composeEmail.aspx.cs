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
                    //adding the data to the dropdown list of the templates
                    SqlCommand com = new SqlCommand("select filePath,name from tblTemplates", con);
                    con.Open();
                    ddlTemplateSelector.DataSource = com.ExecuteReader();
                    ddlTemplateSelector.DataTextField = "name";
                    ddlTemplateSelector.DataValueField = "filePath";
                    ddlTemplateSelector.DataBind();
                }
                using (SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
                {
                    //adding data to the repeater
                    using (SqlDataAdapter da1 = new SqlDataAdapter("select categoryName,ID from tblCategory where userID='" + Session["ID"].ToString() + "'", con3))
                    {
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        rptrCategory.DataSource = ds1;
                        rptrCategory.DataBind();
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
       
        foreach (RepeaterItem i in rptrCategory.Items)
        {
            CheckBoxList cblRecipientNames = (CheckBoxList)i.FindControl("cblRecipients");
            foreach (ListItem recipientID in cblRecipientNames.Items)
            {
                if (recipientID.Selected)
                {
                    sendEmail(Convert.ToInt32(recipientID.Value));//passing the recipient's ID to the mailSending function
                    recipientIDs += recipientID.Value;
                    recipientIDs += ",";
                }
            }
           
        }
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            //saving the mails's body in the database along with template and recipient IDs
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
            //fetching the recipient's information by the ID and user's information
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

    protected void btnTemplatePreview_Click(object sender, EventArgs e)
    {
        previewImage.ImageUrl="../images/avatara.png";
    }



    protected void cbSelectAll_CheckedChanged(object sender, EventArgs e)
    {
       
        foreach (RepeaterItem i in rptrCategory.Items)
        {
            CheckBoxList cblRecipientNames = (CheckBoxList)i.FindControl("cblRecipients");
            foreach (ListItem recipientID in cblRecipientNames.Items)
            {
                recipientID.Selected = cbSelectAll.Checked;
             

            }

        }
    }

    protected void cbCategory_CheckedChanged(object sender, EventArgs e)
    {

        foreach (RepeaterItem i in rptrCategory.Items)
        {
            CheckBox cbCategory = (CheckBox)i.FindControl("cbCategory");
            CheckBoxList cblRecipientNames = (CheckBoxList)i.FindControl("cblRecipients");
            foreach (ListItem recipientID in cblRecipientNames.Items)
            {
                recipientID.Selected = cbCategory.Checked;

                
            }

        }
    }
}