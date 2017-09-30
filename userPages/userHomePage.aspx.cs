using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
public partial class users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //all the checks have been done for the corresponding user only i.e.two users can enter the same recipient's info twice
        if (Session["ID"] == null) Response.Redirect("../Login.aspx");
        else
        {
            if (!IsPostBack)
            {
                //adding the data to the drop down list
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
                {
                    string userID = Session["ID"].ToString();
                    SqlCommand com = new SqlCommand("select categoryName,ID from tblCategory where userID='" + userID + "'", con);
                    con.Open();
                    ddlCategoryName.DataSource = com.ExecuteReader();
                    ddlCategoryName.DataTextField = "categoryName";
                    ddlCategoryName.DataValueField = "ID";
                    ddlCategoryName.DataBind();
                }
            }
        }
    }
    protected void btnCategoryAdder_Click(object sender, EventArgs e)
    {
        Page.Validate("Category");
        if (!Page.IsValid)
            return;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString))
        {
            //procedure returns -1 if category is already added otherwise inserts the provided values in corresponding columns
            string message;
            con.Open();
            string ID = Session["ID"].ToString();
            SqlCommand cmd = new SqlCommand("spInsertCategory", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", ID);
            cmd.Parameters.AddWithValue("@categoryName", tbxCategoryName.Text);
            int categoryID = Convert.ToInt32(cmd.ExecuteScalar());
            switch (categoryID)
            {
                case -1:
                    message = "Category already registered";
                    break;
                default:
                    message = "Category AddedSuccesfuly";
                    tbxCategoryName.Text = "";
                    break;
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
        }
    }
    protected void RecipientAdder_Click(object sender, EventArgs e)
    {
        //adding recipient's information
        Page.Validate("Recipient");
        if (!Page.IsValid)
            return;
        string message;
        string ID = Session["ID"].ToString();
        string CS = ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            con.Open();
            string CategoryID = ddlCategoryName.SelectedItem.Value;
            SqlCommand checkRecipientEmail = new SqlCommand("select count(*) from tblRecipients where email='" + tbxRecipientEmail.Text + "' and categoryID='" + CategoryID + "' ", con);
            int unvalidEmail = Convert.ToInt32(checkRecipientEmail.ExecuteScalar().ToString());//ensuring new email of recipient 
            if (unvalidEmail == 1) message = " you already registered a user with this email names can repeat but emails cant";
            else
            {
                SqlCommand categoryAdder = new SqlCommand("insert into tblRecipients(name,email,categoryID) values(@name,@email,@categoryID)", con);
                categoryAdder.Parameters.AddWithValue("@name", tbxRecipientName.Text);
                categoryAdder.Parameters.AddWithValue("@email", tbxRecipientEmail.Text);
                categoryAdder.Parameters.AddWithValue("@categoryID", CategoryID);
                categoryAdder.ExecuteNonQuery();
                message = "recipient added succesfuly";
                tbxRecipientEmail.Text = "";tbxRecipientName.Text = "";
            }
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
        }
    }
}


