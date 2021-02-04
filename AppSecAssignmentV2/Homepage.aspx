<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="AppSecAssignmentV2.Homepage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Homepage<br />
            <p>Email: <asp:Label ID="lblUserID" runat="server"></asp:Label></p>
            <p>Credit Card Information: <asp:Label ID="lblCreditCardInfo" runat="server"></asp:Label></p>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="Logout" />
        </div>
    </form>
</body>
</html>
