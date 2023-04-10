<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <title>Escella  SignUp Form</title>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
<!-- Custom Theme files -->
<!-- //Custom Theme files -->
<!-- web font -->
<link href="http://localhost:50041/fonts.googleapis.com/css?family=Roboto:300,300i,400,400i,700,700i" rel="stylesheet" />
<!-- //web font -->
<link rel="stylesheet" href="dist/css/Style.css">
</head>
<body>
   
	<!-- main -->
	<div class="main-w3layouts wrapper">
		
		<div class="main-agileinfo">
			<div class="agileits-top">
				<form action="#" method="post" runat="server" id="form1">
					
                    <asp:TextBox id="TextBox1" runat="server" placeholder="name" required ></asp:TextBox>
                    <asp:TextBox ID="TextBox2" type="email" CssClass="email"  runat="server" placeholder="Email" required></asp:TextBox>
					
					 <asp:TextBox ID="TextBox3" type="password"   runat="server" placeholder="Password" required></asp:TextBox> <br /><br />
			
                        
                     <asp:TextBox ID="TextBox4" type="password"    runat="server" placeholder="Confirm Password" required Font-Size="12pt"></asp:TextBox>

					<asp:Button ID="submit" runat="server" BackColor="#FF9966" OnClick="submit_Click" Text="Submit" />
                        <asp:Label id="lblPwdCheck" visible="False" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="Small"></asp:Label>
                    
				</form>
				<p>Already have an Account?<a href="login.aspx"> Login</a></p>
			</div>
		</div>
		<ul class="colorlib-bubbles">
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
		</ul>
	</div>
	<!-- //main -->
    
</body>
</html>
