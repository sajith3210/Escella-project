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
        string Direction;
        string AssetName;
        int qty;
        float price;
        float amount;
        int Balance;
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
                Direction = dr[1].ToString();
                AssetName = dr[2].ToString();
                qty = Convert.ToInt32(dr[3].ToString());

                price = float.Parse(dr[4].ToString());

                string am = dr[5].ToString();
                am = am.Replace(",", ""); // remove comma
                amount = float.Parse(am);
                
                Balance = Convert.ToInt32(dr[6].ToString());
                savedata(date, Direction, AssetName, qty, price, amount, Balance, rowId);
        } //while end
        lblMessage.Visible = true;
        lblMessage.Text = "Data has been saved successfully";
        dr.Close();
        mycon.Close();

        // add report table 
        con.Open();  //rep tble con ope
        String prd_names = "SELECT  AssetName FROM stock_table WHERE regst_id=@regst_id";
        SqlCommand cmddd = new SqlCommand(prd_names, con);
        cmddd.Parameters.AddWithValue("@regst_id", rowId);

        SqlDataReader readerr = cmddd.ExecuteReader();
        List<string> item_nm = new List<string>();
       
        String pname;
        int index = 0;
        while (readerr.Read())
        {
            pname = (string) readerr[0];
            item_nm.Add(pname);
            index++;           
        }
        //remove duplicate value frm item_nm variable and save into remove_dup variable
        List<string> remove_dup_item_nm = item_nm.Distinct().ToList();
        //retrive each value from item_nm list
        string buy_itm_price;
        string sell_itm_price;
        int capital_gain;
        
        foreach (string pn in remove_dup_item_nm)
        {
            string product_name=pn;
           SqlConnection con_capi_gain = new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");
           con_capi_gain.Open();
           buy_itm_price = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName=@AssetName";
            SqlCommand cmd_buy = new SqlCommand(buy_itm_price, con_capi_gain);
            cmd_buy.Parameters.AddWithValue("@regst_id", rowId);
            cmd_buy.Parameters.AddWithValue("@AssetName", product_name);
            Int32 buy_result = (Int32)cmd_buy.ExecuteScalar();
            sell_itm_price = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'SELL' and regst_id=@regst_id and AssetName='ASIANPAINT'";
            SqlCommand cmd_sell = new SqlCommand(sell_itm_price, con_capi_gain);
            cmd_sell.Parameters.AddWithValue("@regst_id", rowId);
            cmd_sell.Parameters.AddWithValue("@AssetName", product_name);
            Int32 sell_result = (Int32)cmd_sell.ExecuteScalar();
            capital_gain = buy_result * (buy_result - sell_result);   
             save_to_report(product_name, capital_gain, rowId);
             con_capi_gain.Close();
        }
        con.Close();
         } // upload_btn_Click end


    //    , , ,
    private void savedata(String date, String Direction, String AssetName, int qty, float price, float amount, int Balance, int rowId)
    {

        // String mycon = "Data Source=HP-PC\\SQLEXPRESS; Initial Catalog=ExcelDatabase; Integrated Security=true";
        //SqlConnection con = new SqlConnection(mycon);
        //String query = "insert into stock_table(Date,Direction,AssetName,Quantity,price,Amount,Balance,regst_id) values(" + date + ",'" + Direction + "','" + AssetName + "','" + qty + "','" + price + "','" + amount + "','" + Balance + "','" + rowId + "')";

        string query = "INSERT INTO stock_table (Date, Direction, AssetName,Quantity,price,Amount,Balance,regst_id) VALUES (@Date, @Direction, @AssetName,@Quantity,@price,@Amount,@Balance,@regst_id)";

        con.Open();
        SqlCommand cmd = new SqlCommand(query, con);
        // Add parameter values to SqlCommand object
        cmd.Parameters.AddWithValue("@Date", date);

        cmd.Parameters.AddWithValue("@Direction", Direction);
        cmd.Parameters.AddWithValue("@AssetName", AssetName);
        cmd.Parameters.AddWithValue("@Quantity", qty);
        cmd.Parameters.AddWithValue("@price", price);
        cmd.Parameters.AddWithValue("@Amount", amount);
        cmd.Parameters.AddWithValue("@Balance", Balance);
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        //cmd.CommandText = query;
        //cmd.Connection = con;
        cmd.ExecuteNonQuery();
        
        con.Close();
    }//save data fun end

    //add to report table fun
    private void save_to_report(String product_name, int capital_gain, int rowId)
    {
        string query = "INSERT INTO report_tabl (product_name, capital_gain,regst_id) VALUES (@product_name, @capital_gain,@regst_id)";
        SqlConnection con_rep = new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");
        con_rep.Open();
        SqlCommand cmd_repo = new SqlCommand(query, con_rep);
  
        // Add parameter values to SqlCommand object
        cmd_repo.Parameters.AddWithValue("@product_name", product_name);
        cmd_repo.Parameters.AddWithValue("@capital_gain", capital_gain);
        cmd_repo.Parameters.AddWithValue("@regst_id", rowId);
        cmd_repo.ExecuteNonQuery();
        cmd_repo.Dispose();
        con_rep.Close();
        
    }// save_to_report end

        
    } //partial class end





