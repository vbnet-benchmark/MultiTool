<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Test.aspx.vb" Inherits="MultiTool.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
	<label for="ccnum">Card Number</label>
	<input type="text" runat="server" id="tbccnum" name="tbccnum" pattern="[0-9]{13,19}" title="Please enter a valid credit card number"  placeholder="e.g. 4111111111111111">


        <label for="TextBox1">Card Number</label>
<asp:TextBox 
    ID="TextBox1" 
    runat="server" 
    TextMode="Password" 
    MaxLength="19" 
    placeholder="e.g. 4111111111111111">
</asp:TextBox>

	</div>
    </form>
</body>
</html>
