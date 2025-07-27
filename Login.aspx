<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="MedicalReimbursement.Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <title>Login - Kutai Bara Nusantara</title>
    <link href="https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css" rel="stylesheet" />
</head>
<body class="bg-green-700 flex items-center justify-center min-h-screen">
    <form id="form1" runat="server">
        <div class="bg-white rounded-xl shadow-2xl p-10 w-full max-w-sm">
            <div class="flex flex-col items-center mb-6">
                <asp:Image ID="imgLogo" runat="server" CssClass="h-100 w-100 object-contain"
                ImageUrl="https://kutaibara.co.id/wp-content/uploads/2023/04/KBN-Logo-horizontal.png"
                AlternateText="Kutai Bara Nusantara company logo with flame and diamond shapes" />
            </div>

            <div class="mb-4">
                <asp:Label ID="UsernameLabel" CssClass="block font-semibold mb-1" Text="NIP" AssociatedControlID="txtUsername" runat="server"></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="w-full px-4 py-2 rounded-lg bg-gray-100 focus:outline-none" />
            </div>
            <div class="mb-6">
                <asp:Label ID="PasswordLabel" CssClass="block font-semibold mb-1" Text="Password" AssociatedControlID="txtPassword" runat="server"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="w-full px-4 py-2 rounded-lg bg-gray-100 focus:outline-none" />
            </div>
            <div class="text-center">
                <asp:Button ID="btnLogin" runat="server" Text="Masuk" CssClass="bg-green-700 text-white px-6 py-2 rounded-full hover:bg-green-800 transition" />
            </div>
        </div>
    </form>
</body>
</html>
