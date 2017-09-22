using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userPages_RecipientList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
            {
                string userID = Session["ID"].ToString();
                SqlCommand com = new SqlCommand("select name,ID from tblRecipient where userID='" + userID + "'", con);
                con.Open();
                cbl1.DataSource = com.ExecuteReader();
                cbl1.DataTextField = "categoryName";
                cbl1.DataValueField = "ID";
                cbl1.DataBind();
            }
        }
}