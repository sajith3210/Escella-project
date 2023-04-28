using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class repo_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
          
        SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=escelladb;Integrated Security=True");
        DataTable table = new DataTable();

        DataColumn scriptcolumn = new DataColumn("Script_Name", typeof(String));
        table.Columns.Add(scriptcolumn);

        DataColumn quantity_sold_column = new DataColumn("quantity_sold", typeof(int));
        table.Columns.Add(quantity_sold_column);

        DataColumn sell_date_column = new DataColumn("sell_date", typeof(String));
        table.Columns.Add(sell_date_column);


        DataColumn purchase_date_column = new DataColumn("purchase_date", typeof(String));
        table.Columns.Add(purchase_date_column);



        DataColumn holding_days_colums = new DataColumn("Holding_days", typeof(int));
        table.Columns.Add(holding_days_colums);

        DataColumn purchase_price_columns = new DataColumn("purchase_price", typeof(int));
        table.Columns.Add(purchase_price_columns);

        DataColumn sale_price_columns = new DataColumn("sale_price", typeof(int));
        table.Columns.Add(sale_price_columns);


        DataColumn value_at_coast_columns = new DataColumn("value_at_coast", typeof(int));
        table.Columns.Add(value_at_coast_columns);

        DataColumn value_at_sale_columns = new DataColumn("value_at_sale", typeof(int));
        table.Columns.Add(value_at_sale_columns);

        DataColumn stcg_column_columns = new DataColumn("stcg", typeof(int));
        table.Columns.Add(stcg_column_columns);
        con.Open();
        string query = "SELECT Distinct  AssetName FROM stock_table WHERE Direction='SELL'";
        SqlCommand cmd = new SqlCommand(query, con);
       
        // Create a new SqlDataAdapter object and pass the SQL query and connection string to its constructor
        SqlDataAdapter adapter = new SqlDataAdapter(query, con);

        // Create a new DataSet object to hold the retrieved data
        DataSet dataSet = new DataSet();

        adapter.Fill(dataSet);

        // Access the retrieved data by iterating through the DataSet object's Tables property
        foreach (DataRow row in dataSet.Tables[0].Rows)
        {
            // Access individual fields in the row using the column name or index
            string asset_name = row["AssetName"].ToString();
            string buy_qur = "SELECT * from stock_table where AssetName='"+asset_name+"' and direction='BUY' order by Date";
         
             SqlDataAdapter adapter_buy = new SqlDataAdapter(buy_qur, con);
            DataSet ds_buy = new DataSet();
            adapter_buy.Fill(ds_buy);

            string sell_qur = "SELECT * from stock_table where AssetName='" + asset_name + "' and direction='SELL' order by Date";
            SqlDataAdapter adapter_sell = new SqlDataAdapter(sell_qur, con);
            DataSet ds_sell = new DataSet();
            adapter_sell.Fill(ds_sell);

            DataTable buy_table = new DataTable();
            buy_table = ds_buy.Tables[0];
            DataTable sell_table = new DataTable();
            sell_table = ds_sell.Tables[0];

            int rowIndex = 0;
            string scriptname;
            string sell_qu ;
            string sell_date;
            string sale_price;
            int value_at_sal;

            string buy_qu;
            string buy_date;
            
            string pur_price;
            int value_at_co;

            int sell_qu_int;
            int sale_price_int;
            int buy_qu_int;
            int pur_price_int;
            int STCG=0;
            DataRow row_tb = table.NewRow();
            
            foreach (DataRow se in ds_sell.Tables[0].Rows) {
                 scriptname = se["AssetName"].ToString();
                 sell_qu = se["Quantity"].ToString();
                 sell_qu_int = int.Parse(sell_qu);
                 sell_date = se["Date"].ToString();
                 sale_price = se["price"].ToString();
                 sale_price_int = int.Parse(sale_price);
                int K=0;
                for(int i=0;i<buy_table.Rows.Count;i++)
                //foreach (DataRow be in ds_buy.Tables[0].Rows)
                {
                    buy_qu = buy_table.Rows[i]["Quantity"].ToString();
                    buy_qu_int = int.Parse(buy_qu);
                    buy_date = buy_table.Rows[i]["Date"].ToString();
                    pur_price = buy_table.Rows[i]["price"].ToString(); 
                    pur_price_int = int.Parse(pur_price);

                    //value at coast calculation
                    value_at_co = sell_qu_int * pur_price_int;
                    //value at sale calculation
                    value_at_sal = sell_qu_int * sale_price_int;

                    DateTime sellDate = DateTime.Parse(sell_date);
                    DateTime buyDate = DateTime.Parse(buy_date);

                    TimeSpan difference = sellDate - buyDate;
                    //holding days  save into daydiffrence variable
                    int daysDifference = difference.Days;

                    if (daysDifference < 365) {
                        STCG=value_at_sal - value_at_co;
                    }


                    table.Rows[K][0] = scriptname;
                    table.Rows[K][0] = sell_qu;
                    table.Rows[K][0] = sell_date;
                    table.Rows[K][0] = buy_date;
                    table.Rows[K][0] = daysDifference;
                    table.Rows[K][0] = pur_price;
                    table.Rows[K][0] = sale_price;
                    table.Rows[K][0] = value_at_co;
                    table.Rows[K][0] = value_at_sal;
                    
                     buy_table.Rows[i].Delete();
                    buy_table.AcceptChanges();
                    K++;
                    break;
                    //be--;
                }

                rowIndex++;
            }

          //  DataColumn quantity_sold_columnn = new DataColumn("quantity_sold", typeof(int));
            //table.Columns.Add(quantity_sold_column);

            //sell
            //string sell_quer = "SELECT AssetName from stock_table where AssetName=@asset_name and direction=@sell order by Date";
            //cmd.Parameters.AddWithValue("@asset_name", asset_name);
            //cmd.Parameters.AddWithValue("@buy", "SELL");
            //SqlDataReader readerr = cmd.ExecuteReader();
          

            // ...
        }

        GridView1.DataSource = table;
        GridView1.DataBind();
        con.Close();

    }//page load end
    


}//partial class end