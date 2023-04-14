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


public partial class upload : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] == null  )
        {
            Response.Redirect("login.aspx");
        }    
      
    }
    
    
   
    SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");
    protected void upload_btn_Click(object sender, EventArgs e)
    {
        int rowId;
        string email = (string)Session["email"];
       con.Open();
       //SqlCommand comm = new SqlCommand("select * from regst where Email=" + email, con);
       string query = "SELECT id FROM regst WHERE Email = @Email"; // SQL query to retrieve row id based on email
        //SqlDataReader sqldr = comm.ExecuteReader();
       SqlCommand cmd = new SqlCommand(query, con);
       cmd.Parameters.AddWithValue("@Email", email);
       SqlDataReader reader = cmd.ExecuteReader();
       if (reader.Read())
       {
            rowId = Convert.ToInt32(reader["id"]); // retrieve row id value from the specific column
           // save the row id value to your next page table
       }
       rowId = Convert.ToInt32(reader["id"]); // retrieve row id value from the specific column
       reader.Close();
        //int sq = sqldr.GetInt32(0);
        con.Close();
 
        
        string date;
        String type;
        String stock;
        int qty;
        int price;
        int amount;
        int total_qty;
        string path = Path.GetFileName(FileUpload1.FileName);
        path = path.Replace(" ", "");
        FileUpload1.SaveAs(Server.MapPath("~/ExcelFiles/") + path);
        String ExcelPath = Server.MapPath("~/ExcelFiles/") + path;
        OleDbConnection mycon = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + ExcelPath + "; Extended Properties=Excel 8.0; Persist Security Info = False");
        mycon.Open();
        OleDbCommand cmdd = new OleDbCommand("select * from [Transaction_with_corporate_acti$]", mycon);
        OleDbDataReader dr = cmdd.ExecuteReader();
       
        while (dr.Read())
        {
            // Response.Write("<br/>"+dr[0].ToString());

                date = dr[0].ToString();
                type = dr[1].ToString();
                stock = dr[2].ToString();
                qty = Convert.ToInt32(dr[3].ToString());

                price = Convert.ToInt32(dr[4].ToString());
                amount = Convert.ToInt32(dr[5].ToString());
                total_qty = Convert.ToInt32(dr[6].ToString());
                savedata(date, type, stock, qty, price, amount, total_qty, rowId);
       


        }
        lblMessage.Visible = true;
        lblMessage.Text = "Data has been saved successfully";
    }

   //    , , ,
    private void savedata(String date, String type, String stock, int qty, int price, int amount, int total_qty, int rowId)
    {
        String query = "insert into stock_table(Date,Type,Stock,Quantity,price,Amount,Total_quantity,regst_id) values(" + date + ",'" + type + "','" + stock + "','" + qty + "','" + price + "','" + amount + "','" + total_qty + "','" + rowId + "')";
        // String mycon = "Data Source=HP-PC\\SQLEXPRESS; Initial Catalog=ExcelDatabase; Integrated Security=true";
        //SqlConnection con = new SqlConnection(mycon);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Connection = con;
        cmd.ExecuteNonQuery();
        con.Close();
    }

    
}