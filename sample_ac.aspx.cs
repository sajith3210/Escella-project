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
using System.Text;
using System.Web.UI.HtmlControls;
public partial class sample_ac : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Create a connection to the database
        SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");

        // Open the connection
        con.Open();

        // Create a SQL command to retrieve the data
        SqlCommand command = new SqlCommand("SELECT * FROM regst", con);

        // Execute the command and retrieve the data
        SqlDataReader reader = command.ExecuteReader();

        // Create a new HTML table
        HtmlTable table = new HtmlTable();

        // Add the table header row
        HtmlTableRow headerRow = new HtmlTableRow();
        headerRow.Cells.Add(new HtmlTableCell("id"));
        headerRow.Cells.Add(new HtmlTableCell("Name"));
        headerRow.Cells.Add(new HtmlTableCell("Email"));
        headerRow.Cells.Add(new HtmlTableCell("Password"));
        table.Rows.Add(headerRow);

        // Iterate through the data and add the rows and cells to the table
        while (reader.Read())
        {
            HtmlTableRow dataRow = new HtmlTableRow();
            dataRow.Cells.Add(new HtmlTableCell(reader["id"].ToString()));
            dataRow.Cells.Add(new HtmlTableCell(reader["Name"].ToString()));
            dataRow.Cells.Add(new HtmlTableCell(reader["Email"].ToString()));
            dataRow.Cells.Add(new HtmlTableCell(reader["Password"].ToString()));
            table.Rows.Add(dataRow);
        }

        // Close the data reader and the database connection
        reader.Close();
        con.Close();
        HtmlGenericControl myTableContainer = (HtmlGenericControl)FindControl("myTableContainer");
        // Add the table to the web form
        myTableContainer.Controls.Add(table);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
}