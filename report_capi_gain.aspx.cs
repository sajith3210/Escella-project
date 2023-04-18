using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;

public partial class dummy_form : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] == null)
        {
            Response.Redirect("login.aspx");
        }  

        SqlConnection con= new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");
        SqlCommand comm = new SqlCommand("select report_id ,product_name,capital_gain from report_tabl", con);
        SqlDataAdapter d = new SqlDataAdapter(comm);
        DataTable dt = new DataTable();
        d.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();


        con.Close();





        // Define the SQL query to retrieve data
        string query = "select product_name ,capital_gain from report_tabl";

        // Create a connection to the database
        SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");

        // Create a command object with the query and connection
        SqlCommand command = new SqlCommand(query, connection);

        // Open the database connection
        connection.Open();

        // Execute the query and get the data reader
        SqlDataReader reader = command.ExecuteReader();

        // Bind the chart to the data reader
        Chart1.DataSource = reader;
        Chart1.Series[0].XValueMember = "product_name";
        Chart1.Series[0].YValueMembers = "capital_gain";
        Chart1.DataBind();
        
        // Close the data reader and database connection
        reader.Close();
        connection.Close();

    }
}