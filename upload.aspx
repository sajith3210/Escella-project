<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <nav>
                <ul>
                    <li><a href="Default.aspx">Dashboard</a></li>
                    <li><a href="#">Calculate LTCG</a></li>
                    <li><a href="#">Capital Gain Report</a></li>
                </ul>
            </nav>
            <div class="content">
                <h1>Upload an Excel File</h1>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="upload_btn" runat="server" Text="Upload" OnClick="upload_btn_Click" /> <br /><br />
                <asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
