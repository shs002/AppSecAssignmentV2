<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AppSecAssignmentV2.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tbPassword.ClientID %>').value;

            if (str.length < 8) {
                document.getElementById("lblPwdCheck").innerHTML = "Password length must be at least 8 characters";
                document.getElementById("lblPwdCheck").style.color = "Red";
                return ("too_short")
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lblPwdCheck").innerHTML = "Password require at least 1 number";
                document.getElementById("lblPwdCheck").style.color = "Red";
                return ("no_number");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lblPwdCheck").innerHTML = "Password require at least 1 upper case letter";
                document.getElementById("lblPwdCheck").style.color = "Red";
                return ("no_upper_case")
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lblPwdCheck").innerHTML = "Password require at least 1 lower case letter";
                document.getElementById("lblPwdCheck").style.color = "Red";
                return ("no_lower_case")
            }
            else if (str.search(/[^a-zA-Z0-9]/) == -1) {
                document.getElementById("lblPwdCheck").innerHTML = "Password require at least 1 special character";
                document.getElementById("lblPwdCheck").style.color = "Red";
                return ("no_special_char")
            }

            document.getElementById("lblPwdCheck").innerHTML = "Password is strong!";
            document.getElementById("lblPwdCheck").style.color = "Blue";
        }
        function match() {
            var str = document.getElementById('<%=tbPassword.ClientID %>').value;
            var cpwd = document.getElementById('<%=tbCPwd.ClientID %>').value;

            if (str != cpwd) {
                document.getElementById("lblPwdMatch").innerHTML = "Password does not match";
                document.getElementById("lblPwdMatch").style.color = "Red";
            }
            document.getElementById("lblPwdMatch").innerHTML = "";
            document.getElementById("lblPwdMatch").style.color = "Blue";
        }
    </script>

    <style type="text/css">
        .auto-style2 {
            width: 700px;
        }
        .auto-style3 {
            height: 26px;
            width: 177px;
        }
        .auto-style7 {
            height: 26px;
            width: 599px;
        }
        .auto-style8 {
            width: 177px;
        }
        .auto-style9 {
            width: 599px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            Registration<br />
            <br />
            <br />
            <table class="auto-style2">
                <tr>
                    <td class="auto-style3">First Name:</td>
                    <td class="auto-style7">
                        <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">Last Name:</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">Credit Card Info:</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="tbCreditCardInfo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">Email Address:</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="tbEmail" runat="server" TextMode="Email"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">Password:</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="tbPassword" runat="server" onkeyup="javascript:validate()" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lblPwdCheck" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">Confirm Password:</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="tbCPwd" runat="server" onkeyup="javascript:match()" TextMode="Password"></asp:TextBox>
                        <asp:Label ID="lblPwdMatch" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style8">Date of Birth:</td>
                    <td class="auto-style9">
                        <asp:TextBox ID="tbDateOfBirth" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" Width="330px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
