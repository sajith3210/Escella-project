<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

   <title>Login</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<!-- Custom Theme files -->
<!-- //Custom Theme files -->
<!-- web font -->
<link href="http://localhost:50041/fonts.googleapis.com/css?family=Roboto:300,300i,400,400i,700,700i" rel="stylesheet" />
<!-- //web font -->
<link rel="stylesheet" href="dist/css/login.css" />
<script src="dist/js/login.js" ></script>
</head>
<body>
    
 <div class="login-page">
  <div class="form">
    <form class="login-form" runat="server">
      <asp:TextBox ID="txtEmail" type="email"   runat="server" placeholder="Email" required></asp:TextBox>
       <asp:TextBox ID="txtPassword" type="password"   runat="server" placeholder="Email" required></asp:TextBox>
     <asp:Button ID="login_btn" runat="server" BackColor="#FF9966" OnClick="login_Click" Text="Login" />
        <asp:Label id="lblLoginCheck" visible="False" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="Small"></asp:Label>
       <p class="message">Not registered? <a href="registration.aspx">Create an account</a></p>
    </form>
  </div>
</div>
</body>
</html>
