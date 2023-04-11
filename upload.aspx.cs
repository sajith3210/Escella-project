using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void upload_btn_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string fileName = FileUpload1.FileName;
            string fileExtension = Path.GetExtension(fileName);
            if (fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                string savePath = Server.MapPath("~/ExcelFiles/") + fileName;
                FileUpload1.SaveAs(savePath);
                lblMessage.Visible = true;
                lblMessage.Text = "File uploaded successfully!";
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please upload a valid Excel file.";
            }
        }
        else
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Please select a file to upload.";
        }
    }
}