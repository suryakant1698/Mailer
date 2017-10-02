using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userPages_RecipientList : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["ID"] == null)
            Response.Redirect("../appHomePage.asspx");
        else
        {
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
                {
                    con.Open();
                    ArrayList categories = new ArrayList();
                    string userID = Session["ID"].ToString();
                    DataTable table = new DataTable();
                    table.Columns.Add("name");
                    table.Columns.Add("email");
                    table.Columns.Add("category");
                    SqlCommand getcategories = new SqlCommand("select ID from tblCategory where userID='" + userID + "' ", con);
                    using (SqlDataReader rdCategoryID = getcategories.ExecuteReader())
                    {
                        while (rdCategoryID.Read())
                        {
                            categories.Add(rdCategoryID["ID"]);
                        }
                    }
                    foreach (int categoyID in categories)
                    {
                        SqlCommand getCategoryName = new SqlCommand("select categoryName from tblCategory where ID='"+categoyID+"'",con);
                        string categoryName = getCategoryName.ExecuteScalar().ToString();
                        SqlCommand getRecipientsInfo = new SqlCommand("select name,email from tblRecipients where categoryID='" + categoyID + "'", con);
                        using (SqlDataReader rdrRecipients = getRecipientsInfo.ExecuteReader())
                        {
                            while (rdrRecipients.Read())
                            {
                                DataRow dataRow = table.NewRow();
                                dataRow["name"] = rdrRecipients["name"];
                                dataRow["email"] = rdrRecipients["email"];
                                dataRow["category"] = categoryName;
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