<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppSecAssignmentV2.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <%--<script src="https://www.google.com/recaptcha/api.js?render=6Lc0qkgaAAAAACqWHeBG2J9MNznzGA5QgOQmmXSn"></script>--%>

    <style type="text/css">
        .auto-style1 {
            width: 18%;
        }
        .auto-style2 {
            width: 104px;
            height: 33px;
        }
        .auto-style3 {
            width: 97px;
            height: 33px;
        }
        .auto-style4 {
            width: 104px;
            height: 32px;
        }
        .auto-style5 {
            width: 97px;
            height: 32px;
        }
        .auto-style6 {
            height: 32px;
        }
        .auto-style7 {
            height: 34px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            Login<br />
            <br />
            <br />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">Email:</td>
                    <td class="auto-style3">
                        <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Password:</td>
                    <td class="auto-style5">
                        <asp:TextBox ID="tbPwd" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6" colspan="2">
                        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7" colspan="2">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
    </form>

    <%--<script>
        grecaptcha.ready(function () {
            grecaptcha.execute(' 6Lc0qkgaAAAAACqWHeBG2J9MNznzGA5QgOQmmXSn ', { action: 'Login' }).then(function (token) {
            document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>--%>

</body>
</html>
