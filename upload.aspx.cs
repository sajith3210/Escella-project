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
        String prd_names = "SELECT DISTINCT AssetName FROM stock_table WHERE regst_id=@regst_id";
        SqlCommand cmddd = new SqlCommand(prd_names, con);
        cmddd.Parameters.AddWithValue("@regst_id", rowId);
        //SqlDataReader readerr = cmddd.ExecuteReader(); this code see up to line  
        
        String product_name;
        String amruth_anjan_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='AMRUTANJAN'";
        SqlCommand cmd_amr_buy = new SqlCommand(amruth_anjan_buy, con);
        cmd_amr_buy.Parameters.AddWithValue("@regst_id", rowId);
        Int32 amruth_anjan_buy_result = (Int32) cmd_amr_buy.ExecuteScalar();
       System.Diagnostics.Debug.WriteLine("bysdf result isdf kon ol", amruth_anjan_buy_result);
        //SqlDataReader amr_buy_reader = cmd_amr_buy.ExecuteReader();
        //amr_buy_reader.Close();
        
        String amruth_anjan_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'SELL' and regst_id=@regst_id and AssetName='AMRUTANJAN'";   
        SqlCommand cmd_amr_sell = new SqlCommand(amruth_anjan_sell, con);
        cmd_amr_sell.Parameters.AddWithValue("@regst_id", rowId);
        Int32 amruth_anjan_sell_result = (Int32)cmd_amr_sell.ExecuteScalar();
        

        String ASIANPAINT_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='ASIANPAINT'";   
        SqlCommand cmd_asian_buy = new SqlCommand(ASIANPAINT_buy, con);
        cmd_asian_buy.Parameters.AddWithValue("@regst_id", rowId);
        Int32 asian_paint_buy_result = (Int32)cmd_asian_buy.ExecuteScalar();
        

        String ASIANPAINT_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'SELL' and regst_id=@regst_id and AssetName='ASIANPAINT'";  
        SqlCommand cmd_asian_sell = new SqlCommand(ASIANPAINT_sell, con);
        cmd_asian_sell.Parameters.AddWithValue("@regst_id", rowId);
        Int32 asian_paint_sell_result = (Int32)cmd_asian_sell.ExecuteScalar();

        String BHARTIARTL_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='BHARTIARTL'";    
        SqlCommand cmd_BHARTIARTL_buy = new SqlCommand(BHARTIARTL_buy, con);
        cmd_BHARTIARTL_buy.Parameters.AddWithValue("@regst_id", rowId);
        Int32 bharatu_airtel_buy_result = (Int32)cmd_BHARTIARTL_buy.ExecuteScalar();

        String BHARTIARTL_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'SELL' and regst_id=@regst_id and AssetName='BHARTIARTL'";  
        SqlCommand cmd_BHARTIARTL_sell = new SqlCommand(BHARTIARTL_sell, con);
        cmd_BHARTIARTL_sell.Parameters.AddWithValue("@regst_id", rowId);
        Int32 bharatu_airtel_sell_result = (Int32)cmd_BHARTIARTL_sell.ExecuteScalar();

        String BLUESTARCO_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='BLUESTARCO'";   
        SqlCommand cmd_BLUESTARCO_buy = new SqlCommand(BLUESTARCO_buy, con);
        cmd_BLUESTARCO_buy.Parameters.AddWithValue("@regst_id", rowId);
        Int32 BLUESTARCO_buy_result = (Int32)cmd_BLUESTARCO_buy.ExecuteScalar();

        String BLUESTARCO_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='BLUESTARCO'";   
        SqlCommand cmd_BLUESTARCO_sell = new SqlCommand(BLUESTARCO_sell, con);
        cmd_BLUESTARCO_sell.Parameters.AddWithValue("@regst_id", rowId);
        Int32 BLUESTARCO_sell_result = (Int32)cmd_BLUESTARCO_sell.ExecuteScalar();

        String CDSL_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='CDSL'";       
        SqlCommand cmd_CDSL_buy = new SqlCommand(CDSL_buy, con);
        cmd_CDSL_buy.Parameters.AddWithValue("@regst_id", rowId);
        Int32 CDSL_buy_result = (Int32)cmd_CDSL_buy.ExecuteScalar();

        String CDSL_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='CDSL'";
        SqlCommand cmd_CDSL_sell = new SqlCommand(CDSL_sell, con);
        cmd_CDSL_sell.Parameters.AddWithValue("@regst_id", rowId);
        Int32 CDSL_sell_result = (Int32)cmd_CDSL_sell.ExecuteScalar();

        String CHOLAFIN_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='CHOLAFIN'";    
        SqlCommand cmd_CHOLAFIN_buy = new SqlCommand(CHOLAFIN_buy, con);
        cmd_CHOLAFIN_buy.Parameters.AddWithValue("@regst_id", rowId);
        Int32 CHOLAFIN_buy_result = (Int32)cmd_CHOLAFIN_buy.ExecuteScalar();

        String CHOLAFIN_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='CHOLAFIN'";     
        SqlCommand cmd_CHOLAFIN_sell = new SqlCommand(CHOLAFIN_sell, con);
        cmd_CHOLAFIN_sell.Parameters.AddWithValue("@regst_id", rowId);
        Int32 CHOLAFIN_sell_result = (Int32)cmd_CHOLAFIN_buy.ExecuteScalar();
        

        Int32 int_amramruth_anjan_buy;
        int int_amruth_anjan_sell;
        int amruthan_capital_gain;

        int int_asianpaint_buy;
        int int_asianpaint_sell;
        int asian_paint_capital_gain;

        int int_bharati_airtel_buy;
        int int_bharati_airtel_sell;
        int bharati_airtel_capital_gain;

        int int_BLUESTARCO_buy;
        int int_BLUESTARCO_sell;
        int bluestarco_capital_gain;

        int int_CDSL_buy;
        int int_CDSL_sell;
        int CDSL_capital_gain;

        int int_CHOLAFIN_buy;
        int int_CHOLAFIN_sell;
        int CHOLAFIN_capital_gain;
        try
        {
            int_amramruth_anjan_buy = amruth_anjan_buy_result;
            int_amruth_anjan_sell = amruth_anjan_sell_result;
            amruthan_capital_gain = int_amramruth_anjan_buy * (int_amramruth_anjan_buy - int_amruth_anjan_sell);

            int_asianpaint_buy = asian_paint_buy_result;
            int_asianpaint_sell = asian_paint_sell_result;
            asian_paint_capital_gain = int_asianpaint_buy * (int_asianpaint_buy - int_asianpaint_sell);

            int_bharati_airtel_buy = bharatu_airtel_buy_result;
            int_bharati_airtel_sell = bharatu_airtel_sell_result;
            bharati_airtel_capital_gain = int_bharati_airtel_buy * (int_bharati_airtel_buy - int_bharati_airtel_sell);

            int_BLUESTARCO_buy = BLUESTARCO_buy_result;
            int_BLUESTARCO_sell = BLUESTARCO_sell_result;
            bluestarco_capital_gain = int_BLUESTARCO_buy * (int_BLUESTARCO_buy - int_BLUESTARCO_sell);

            int_CDSL_buy = CDSL_buy_result;
            int_CDSL_sell = CDSL_sell_result;
            CDSL_capital_gain = int_CDSL_buy * (int_CDSL_buy - int_CDSL_sell);

            int_CHOLAFIN_buy = CHOLAFIN_buy_result;
            int_CHOLAFIN_sell = CHOLAFIN_sell_result;
            CHOLAFIN_capital_gain = int_CHOLAFIN_buy * (int_CHOLAFIN_buy - int_CHOLAFIN_sell);
        }
        catch (DivideByZeroException ex)
        {
            amruthan_capital_gain = 0;
            asian_paint_capital_gain = 0;
            bharati_airtel_capital_gain = 0;
            bluestarco_capital_gain = 0;
            CDSL_capital_gain = 0;
            CHOLAFIN_capital_gain = 0;
        }
        int amr_capi_gain;
        int asian_pain_gain;
        int bharati_gain;
        int bluestarco_gain;
        int cdsl_gain;
        int cholafin_gain;
        int num = 1;
        SqlDataReader readerr = cmddd.ExecuteReader(); 
        while (readerr.Read())
        {
            if (num == 1)
            {
                product_name = (string)readerr[0];  //product name amruthnjan
                amr_capi_gain = amruthan_capital_gain;
                save_to_report(product_name, amr_capi_gain, rowId);
            }
            if (num == 2)
            {
                product_name = (string)readerr[0];  //product name asian paint
                asian_pain_gain = asian_paint_capital_gain;
                save_to_report(product_name, asian_pain_gain, rowId);
            }
            if (num == 3)
            {
                product_name = (string)readerr[0];  //product name bharati airtel
                bharati_gain = bharati_airtel_capital_gain;
                save_to_report(product_name, bharati_gain, rowId);
            }
            if (num == 4)
            {
                product_name = (string)readerr[0];  //product name bluestarco
                bluestarco_gain = bluestarco_capital_gain;
                save_to_report(product_name, bluestarco_gain, rowId);
            }
            if (num == 5)
            {
                product_name = (string)readerr[0];  //product name cdsl_gain
                cdsl_gain = CDSL_capital_gain;
                save_to_report(product_name, cdsl_gain, rowId);
            }
            if (num == 6)
            {
                product_name = (string)readerr[0];  //product name cholafin_gain
                cholafin_gain = CHOLAFIN_capital_gain;
                save_to_report(product_name, cholafin_gain, rowId);
            }
            num++;
            
            
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





