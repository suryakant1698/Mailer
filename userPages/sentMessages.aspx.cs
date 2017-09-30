using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userPages_sentMessages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
            Response.Redirect("../Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
                {
                    con.Open();
                    string recipientIDs;
                    int templateID;
                    string recipientNames;
                    DataTable table = new DataTable();
                    table.Columns.Add("body");
                    table.Columns.Add("TemplateName");
                    table.Columns.Add("Recipients");
                    SqlCommand getSentMailsInfo = new SqlCommand("select * from tblSentMails", con);
                    using (SqlDataReader rdSentMails = getSentMailsInfo.ExecuteReader())
                    {
                        while (rdSentMails.Read())
                        {
                            recipientNames = "";
                            using (SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
                            {
                                con3.Open();
                                recipientIDs = rdSentMails["recipientID"].ToString();
                                templateID = Convert.ToInt32(rdSentMails["templateID"]);
                                if (Convert.ToInt32(Session["ID"]) == getUserIDbyrecipientID(recipientIDs[0]))//checking if a particular mail is sent by the current user or not
                                {
                                    SqlCommand getTemplateName = new SqlCommand("select name from tblTemplates where ID='" + templateID + "' ", con3);
                                    string templateName = getTemplateName.ExecuteScalar().ToString();
                                    foreach (char ID in recipientIDs)
                                    {
                                        if (ID != ',')
                                        {
                                            SqlCommand getRecipientName = new SqlCommand("select name from tblRecipients where ID='" + ID + "'", con3);
                                            string recipientName = getRecipientName.ExecuteScalar().ToString();
                                            recipientNames += recipientName;
                                            recipientNames += ",";
                                        }
                                    }
                                    DataRow dataRow = table.NewRow();
                                    dataRow["body"] = rdSentMails["body"];
                                    dataRow["TemplateName"] = templateName;
                                    dataRow["Recipients"] = recipientNames;
                                    table.Rows.Add(dataRow);
                                }
                            }
                        }
                        grdView.DataSource = table;
                        grdView.DataBind();
                    }

                }

            }
        }
    }
    public int getUserIDbyrecipientID(char recipientID)
    {
        using (SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            con2.Open();
            SqlCommand getCategoryID = new SqlCommand("select categoryID from tblRecipients where ID='" + recipientID + "'", con2);
            int categoryID = Convert.ToInt32(getCategoryID.ExecuteScalar());
            SqlCommand getUserID = new SqlCommand("select userID from tblCategory where ID='" + categoryID + "'", con2);
            int userID = Convert.ToInt32(getUserID.ExecuteScalar());
            return userID;

        }
    }

}