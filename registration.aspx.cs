using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
    }
    SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");


    protected void submit_Click(object sender, EventArgs e)
    {
    string email = TextBox2.Text;
    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM regst WHERE Email = @Email", con);
    cmd.Parameters.AddWithValue("@Email", email);
    con.Open();
    int count = (int)cmd.ExecuteScalar();
    con.Close();
    if (count > 0)
    {
        lblPwdCheck.Visible = true;
        lblPwdCheck.Text = "Email adress is already please try another one";
    }
    else
    {
        if (TextBox3.Text == TextBox4.Text)
        {
            con.Open();
         

            SqlCommand comm = new SqlCommand("insert into regst values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "')", con);
          
      
            comm.ExecuteNonQuery();
            con.Close();
            Response.Redirect("~/login.aspx");
        }

        else
        {
            lblPwdCheck.Visible = true;
            lblPwdCheck.Text = "Password are does not match plese enter password again";
        }
    }

      
    }
}