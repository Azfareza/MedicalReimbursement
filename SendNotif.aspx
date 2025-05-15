<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SendNotif.aspx.vb" Inherits="MedicalReimbursement.SendNotif" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Kirim WhatsApp</title>
    <script src="https://cdn.tailwindcss.com"></script>
</head>
<body class="bg-gray-100 font-sans">
    <form id="form1" runat="server">
        <div class="min-h-screen flex items-center justify-center">
            <div class="bg-white rounded-2xl shadow-lg p-10 w-full max-w-xl">
                <h2 class="text-2xl font-bold text-gray-800 mb-6 text-center">Form Kirim WhatsApp</h2>

                <div class="mb-4">
                    <asp:Label ID="lblNomor" runat="server" Text="Nomor Tujuan (cth: 6281234567890):"
                        CssClass="block text-sm font-medium text-gray-700"></asp:Label>
                    <asp:TextBox ID="txtNomor" runat="server" CssClass="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:ring-indigo-500 focus:border-indigo-500"></asp:TextBox>
                </div>

                <div class="mb-4">
                    <asp:Label ID="lblPesan" runat="server" Text="Pesan:"
                        CssClass="block text-sm font-medium text-gray-700"></asp:Label>
                    <asp:TextBox ID="txtPesan" runat="server" TextMode="MultiLine" Rows="5" Columns="50"
                        placeholder="Tulis pesan Anda di sini..."
                        CssClass="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:ring-indigo-500 focus:border-indigo-500"></asp:TextBox>
                </div>

                <div class="mb-4 text-center">
                    <asp:Button ID="btnKirim" runat="server" Text="Kirim"
                        CssClass="bg-emerald-800 text-white px-6 py-2 rounded-lg shadow hover:bg-emerald-900 transition duration-200 cursor-pointer"
                        OnClick="btnKirim_Click" />
                </div>

                <div class="text-center">
                    <asp:Label ID="lblStatus" runat="server" CssClass="text-blue-600 font-semibold"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
