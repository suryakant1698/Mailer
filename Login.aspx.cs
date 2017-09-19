using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;


public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // string password = "1234";
       // Response.Write(encryption(password));
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
         
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString); //verifying username or email whatever he enters
        con.Open();
        string checkuser = "select count(*) from tblUsers where username='" + tbxUsername.Text + "' or email='" + tbxUsername.Text + "' and Verification=1";
        SqlCommand com = new SqlCommand(checkuser, con);
        int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
        con.Close();
        if (temp == 1)
        {
            con.Open();
            string checkPasswordQuerry = "select upassword from tblUsers where username='" + tbxUsername.Text + "' or email='" + tbxUsername.Text + "'"; //verifying password

            SqlCommand passcom = new SqlCommand(checkPasswordQuerry, con);
            string Password = passcom.ExecuteScalar().ToString();
            if (Password ==encryption(tbxPassword.Text.Trim()))
            {

                Session["UserLoggedIn"] = tbxUsername.Text;
                Response.Redirect("userHomePage.aspx");
            }
            else Response.Write("incorrect username or password");


        }
        else Response.Write("incorrect username or password or account is not verified yet ");



    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("register.aspx");
    }
    public string encryption(String password)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] encrypt;
        UTF8Encoding encode = new UTF8Encoding();
        //encrypt the given password string into Encrypted data  
        encrypt = md5.ComputeHash(encode.GetBytes(password));
        StringBuilder encryptdata = new StringBuilder();
        //Create a new string by using the encrypted data  
        for (int i = 0; i < encrypt.Length; i++)
        {
            encryptdata.Append(encrypt[i].ToString());
        }
        return encryptdata.ToString();
    }


}