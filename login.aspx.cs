using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");
    protected void login_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text;
        string pwd = txtPassword.Text;
        SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM regst WHERE Email = @Email and password=@password ", con);
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@password", pwd);
        con.Open();
        int count = (int)cmd.ExecuteScalar();
        con.Close();
        if (count > 0)
        {
            Response.Redirect("~/index.aspx");
        }
        else
        {
            lblLoginCheck.Visible = true;
            lblLoginCheck.Text = "Email or password are incorrect please try again!!!";
        }
    }
}