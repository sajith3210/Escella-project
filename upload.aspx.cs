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
       


        }
        lblMessage.Visible = true;
        lblMessage.Text = "Data has been saved successfully";
    }

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
        cmd.Parameters.AddWithValue("@Quantity",qty );
        cmd.Parameters.AddWithValue("@price", price);
        cmd.Parameters.AddWithValue("@Amount", amount);
        cmd.Parameters.AddWithValue("@Balance", Balance);
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        //cmd.CommandText = query;
        //cmd.Connection = con;
        cmd.ExecuteNonQuery();
        String prd_names = "SELECT DISTINCT AssetName FROM stock_table WHERE regst_id= @regst_id";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmdd = new SqlCommand(prd_names, con);  
        SqlDataReader reader = cmdd.ExecuteReader();
        String product_name;
        String amruth_anjan_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='AMRUTANJAN'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_amr_buy = new SqlCommand(amruth_anjan_buy, con);
        SqlDataReader amr_buy_reader = cmd_amr_buy.ExecuteReader();

        String amruth_anjan_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'SELL' and regst_id=@regst_id and AssetName='AMRUTANJAN'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_amr_sell = new SqlCommand(amruth_anjan_sell, con);
        SqlDataReader amr_sell_reader = cmd_amr_sell.ExecuteReader();


        String ASIANPAINT_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='ASIANPAINT'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_asian_buy = new SqlCommand(ASIANPAINT_buy, con);
        SqlDataReader asian_buy_reader = cmd_asian_buy.ExecuteReader();

        String ASIANPAINT_sell= "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'SELL' and regst_id=@regst_id and AssetName='ASIANPAINT'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_asian_sell = new SqlCommand(ASIANPAINT_sell, con);
        SqlDataReader asian_sell_reader = cmd_asian_sell.ExecuteReader();


        String BHARTIARTL_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='BHARTIART'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_BHARTIARTL_buy = new SqlCommand(BHARTIARTL_buy, con);
        SqlDataReader BHARTI_buy_reader = cmd_BHARTIARTL_buy.ExecuteReader();

        String BHARTIARTL_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'SELL' and regst_id=@regst_id and AssetName='BHARTIART'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_BHARTIARTL_sell = new SqlCommand(BHARTIARTL_sell, con);
        SqlDataReader BHARTI_sell_reader = cmd_BHARTIARTL_sell.ExecuteReader();

        String BLUESTARCO_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='BLUESTARCO'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_BLUESTARCO_buy = new SqlCommand(BLUESTARCO_buy, con);
        SqlDataReader BLUESTARCO_buy_reader = cmd_BLUESTARCO_buy.ExecuteReader();

        String BLUESTARCO_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='BLUESTARCO'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_BLUESTARCO_sell = new SqlCommand(BLUESTARCO_sell, con);
        SqlDataReader BLUESTARCO_sell_reader = cmd_BLUESTARCO_sell.ExecuteReader();


        String CDSL_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='CDSL'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_CDSL_buy = new SqlCommand(CDSL_buy, con);
        SqlDataReader CDSL_buy_reader = cmd_CDSL_buy.ExecuteReader();

        String CDSL_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='CDSL'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_CDSL_sell = new SqlCommand(CDSL_sell, con);
        SqlDataReader CDSL_sell_reader = cmd_CDSL_sell.ExecuteReader();

        String CHOLAFIN_buy = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='CHOLAFIN'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_CHOLAFIN_buy = new SqlCommand(CHOLAFIN_buy, con);
        SqlDataReader CHOLAFIN_buy_reader = cmd_CHOLAFIN_buy.ExecuteReader();

        String CHOLAFIN_sell = "SELECT SUM(Quantity) FROM stock_table WHERE Direction = 'BUY' and regst_id=@regst_id and AssetName='CHOLAFIN'";
        cmd.Parameters.AddWithValue("@regst_id", rowId);
        SqlCommand cmd_CHOLAFIN_sell = new SqlCommand(CHOLAFIN_sell, con);
        SqlDataReader CHOLAFIN_sell_reader = cmd_CHOLAFIN_sell.ExecuteReader();

        int int_amramruth_anjan_buy;
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
            int_amramruth_anjan_buy=Convert.ToInt32(amruth_anjan_buy);
            int_amruth_anjan_sell=Convert.ToInt32(amruth_anjan_sell);
            amruthan_capital_gain = int_amramruth_anjan_buy * (int_amramruth_anjan_buy - int_amruth_anjan_sell);
           
            int_asianpaint_buy = Convert.ToInt32(ASIANPAINT_buy);
            int_asianpaint_sell = Convert.ToInt32(ASIANPAINT_sell);
            asian_paint_capital_gain = int_asianpaint_buy * (int_asianpaint_buy - int_asianpaint_sell);

            int_bharati_airtel_buy = Convert.ToInt32(BHARTIARTL_buy);
            int_bharati_airtel_sell = Convert.ToInt32(BHARTIARTL_sell);
             bharati_airtel_capital_gain = int_bharati_airtel_buy * (int_bharati_airtel_buy - int_bharati_airtel_sell);

            int_BLUESTARCO_buy = Convert.ToInt32(BLUESTARCO_buy);
             int_BLUESTARCO_sell = Convert.ToInt32(BLUESTARCO_sell);
            bluestarco_capital_gain = int_BLUESTARCO_buy * (int_BLUESTARCO_buy - int_BLUESTARCO_sell);

            int_CDSL_buy = Convert.ToInt32(CDSL_buy);
            int_CDSL_sell = Convert.ToInt32(CDSL_sell);
            CDSL_capital_gain = int_CDSL_buy * (int_CDSL_buy - int_CDSL_sell);

            int_CHOLAFIN_buy = Convert.ToInt32(CHOLAFIN_buy);
            int_CHOLAFIN_sell = Convert.ToInt32(CHOLAFIN_sell);
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
        int num=1;
        while (reader.Read()) {
            if (num==1){
            product_name = (string) reader[0];  //product name amruthnjan
            amr_capi_gain = amruthan_capital_gain;
            }
            if (num == 2) {
                product_name = (string)reader[0];  //product name asian paint
                asian_pain_gain = asian_paint_capital_gain;
            }
            if (num == 3) {
                product_name = (string)reader[0];  //product name bharati airtel
                bharati_gain = bharati_airtel_capital_gain;
            }
            if (num == 4)
            {
                product_name = (string)reader[0];  //product name bluestarco
                bluestarco_gain = bluestarco_capital_gain;
            }
            if (num == 5)
            {
                product_name = (string)reader[0];  //product name cdsl_gain
                cdsl_gain = CDSL_capital_gain;
            }
            if (num == 6)
            {
                product_name = (string)reader[0];  //product name cholafin_gain
                cholafin_gain = CHOLAFIN_capital_gain;
            }
        }
        con.Close();
    }

    
}