<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MR_EMP.aspx.vb" Inherits="MedicalReimbursement.MR_EMP" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <title>Dashboard</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;700&amp;display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Inter', sans-serif;
        }
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/2.3.0/js/dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/2.3.0/css/dataTables.dataTables.min.css">

    <script>
        $(document).ready(function () {
            $('#requestsTable').DataTable({
                "paging": true,
                "pageLength": 5,
                "searching": true,
                "info": true,
                "ordering": true,
                "dom": '<"top"f>rt<"bottom"p><"clear">'
            });
        });

    </script>
</head>
<body class="bg-gray-100">
    <form id="form1" runat="server" class="flex min-h-screen">
        <!-- Sidebar -->
        <aside class="bg-[#1B5E57] w-56 flex flex-col p-5">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="mb-6 h-10 rounded px-3 text-gray-900" />
            <div class="text-white mb-6">
                <p class="font-bold text-sm">Welcome,</p>
                <asp:Label ID="lblUserName" runat="server" Text="Nadia Setyaningrum" CssClass="text-xs font-light" />
            </div>
            <nav class="flex flex-col gap-3">
                <asp:Button ID="btnDashboard" runat="server" Text="Dashboard"  CssClass="bg-[#145445] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:bg-[#FFAA0A] cursor-pointer" /> 
                <asp:Button ID="btnMedicalReimbursement" runat="server" Text="Medical Reimbursement" CssClass="bg-[#FFAA0A] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:cursor-pointer" />
                <asp:Button ID="btnLogOut" runat="server" Text="Log Out" CssClass="border border-[#FF6B6B] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:bg-[#FF6B6B] cursor-pointer" />
            </nav>
        </aside>
        <!-- Main content -->
        <main class="flex-1 p-6">
            <!-- Header with logo and background image -->
            <div class="relative mb-8 rounded overflow-hidden">
                <asp:Image ID="imgHeaderBg" runat="server" CssClass="w-full h-28 object-cover opacity-30" 
                    ImageUrl="https://storage.googleapis.com/a1aa/image/dcc648eb-8145-4b6b-6446-fca61fbff108.jpg" 
                    AlternateText="Mining trucks and equipment in a mining site" />
                <div aria-label="Company logo and dashboard title" class="absolute inset-0 flex items-center gap-4 px-6">
                    <asp:Image ID="imgLogo" runat="server" CssClass="h-20 w-20 object-contain" 
                        ImageUrl="https://kutaibara.co.id/wp-content/uploads/2023/04/KBN-Logo-horizontal.png" 
                        AlternateText="Kutai Bara Nusantara company logo with flame and diamond shapes" />
                    <div class="text-[#145445] font-semibold text-xl md:text-2xl flex items-center gap-1">
                        <span class="text-lg md:text-xl font-normal block leading-none">
                            KUTAI BARA NUSANTARA<br />
                            <span class="text-xs font-light leading-tight block">A Leading Energy Producer</span>
                        </span>
                        <span class="text-[#145445] font-extrabold text-2xl md:text-3xl ml-2">DASHBOARD</span>
                    </div>
                </div>
            </div>
            
            <div class="flex flex-col md:flex-row gap-6">
                <section class="flex-1 w-1/2">
                    <div class="flex justify-between items-center mb-4 px-1">
                        <h2 class="text-[#f97316] font-semibold text-lg">
                            Log History
                        </h2>
                        <%--<asp:Button ID="btnAddNewRequest" runat="server" Text="Add New Request" CssClass="bg-[#0052cc] text-white font-semibold rounded-md py-2 px-5" />--%>
                    </div>
                    <asp:GridView ID="gvLogHistory" runat="server" AutoGenerateColumns="False" CssClass="w-full bg-white rounded-lg shadow-sm border border-gray-400 text-sm" HeaderStyle-CssClass="text-left font-semibold p-3 border-b border-gray-400" RowStyle-CssClass="border border-gray-400 rounded-md mt-2" AlternatingRowStyle-CssClass="border border-gray-400 rounded-md mt-2">
                        <Columns>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-CssClass="p-3" />
                            <asp:BoundField DataField="RequestDate" HeaderText="Request Date" ItemStyle-CssClass="p-3" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CssClass="bg-[#0052cc] p-2 rounded text-white relative" CommandName="EditRequest" CommandArgument='<%# Container.DataItemIndex %>'>
                                    <%--<i class="fas fa-edit"></i>--%>
                                    </asp:Button>
                                    <span class="absolute top-0 right-0 block h-2 w-2 rounded-full ring-2 ring-white bg-red-600" style="position:relative; top:-28px; left:-28px;"></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </section>
             <!-- Form -->
                <section class="flex-1 w-1/2">
                    <h2 class="text-[#f97316] font-semibold text-lg mb-4">
                        Add New Request
                    </h2>

                    <div class="flex flex-col md:flex-row gap-6 mb-4">
                        <div class="flex-1 w-1/2">
                            <asp:Label AssociatedControlID="ddlReimbursementCategory" runat="server" Text="Reimbursement Category" CssClass="block text-xs font-semibold mb-1" />
                            <asp:DropDownList ID="ddlReimbursementCategory" runat="server" CssClass="w-full rounded-md bg-gray-200 text-gray-900 py-2 px-2">
                                <asp:ListItem Value="null" Text="Select Category"></asp:ListItem>
                                <asp:ListItem>Rawat Jalan</asp:ListItem>
                                <asp:ListItem>Kacamata</asp:ListItem>
                                <asp:ListItem>Persalinan</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="flex-1 w-1/2">
                            <asp:Label ID="lblDate" runat="server" AssociatedControlID="txtDate" CssClass="block text-xs font-semibold mb-1" Text="Select Date" />
                            <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="w-full rounded-md border bg-gray-200 p-2 text-sm focus:border-blue-500 focus:ring focus:ring-blue-200" />
                        </div>
                    </div>
                    
                    <div class="mb-4">
                        <asp:Label AssociatedControlID="txtMedicalDetail" runat="server" Text="Medical Detail" CssClass="block text-xs font-semibold mb-1" />
                        <asp:TextBox ID="txtMedicalDetail" runat="server" TextMode="MultiLine" Rows="4" CssClass="w-full rounded-md bg-gray-200 text-xs p-3 resize-none"></asp:TextBox>
                    </div>
                    <div class="mb-4">
                        <asp:Label AssociatedControlID="txtTotalCost" runat="server" Text="Total Cost" CssClass="block text-xs font-semibold mb-1" />
                        <div class="flex flex-col md:flex-row gap-6">
                            <div class="w-5/6">
                                <asp:TextBox ID="txtTotalCost" runat="server" CssClass="w-full rounded-md bg-gray-200 font-semibold px-3 py-2 w-full" />            
                            </div>
                            <div class="w-1/6">
                                <asp:Button ID="btnCalculate" runat="server" Text="Calculate" CssClass="bg-[#0052cc] text-white font-semibold rounded-md py-2 px-5 w-full cursor-pointer" OnClick="btnCalculate_Click"  />
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblCalculation" runat="server" CssClass="block text-xs text-gray-600 mt-2 mb-4" />

                    <div class="mb-4">
                        <asp:Label AssociatedControlID="fileKwitansi" runat="server" Text="Kwitansi" CssClass="block text-xs font-semibold mb-1" />
                        <div class="flex items-center">
                            <asp:FileUpload ID="fileKwitansi" runat="server" CssClass="ftoKwt bg-gray-200 rounded-md px-4 py-2 border-gray-300 w-full" />
                        </div>
                    </div>
                    <div class="mb-4">
                        <asp:Label AssociatedControlID="fileResep" runat="server" Text="Resep Obat" CssClass="block text-xs font-semibold mb-1" />
                        <div class="flex items-center">
                            <asp:FileUpload ID="fileResep" runat="server" CssClass="bg-gray-200 rounded-md px-4 py-2 border-gray-300 w-full" />
                        </div>
                    </div>
                    <div class="mb-4">
                        <asp:Label AssociatedControlID="filePendukung" runat="server" Text="File Pendukung" CssClass="block text-xs font-semibold mb-1" />
                        <span class="font-normal text-[10px]">(Konsultasi, Lab)</span>
                        <div class="flex items-center bg-gray-200 rounded-md">
                            <asp:FileUpload ID="filePendukung" runat="server" CssClass="px-4 py-2 rounded-md border-gray-300 w-full" />
                        </div>
                    </div>

                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="bg-[#0052cc] text-white font-semibold rounded-md py-2 px-5 self-start cursor-pointer" OnClientClick="return confirm('Apakah kamu yakin ingin mengajukan klaim ini?');" OnClick="btnSubmit_Click"/>

                    <div class="text-center mt-4">
                        <asp:Label ID="lblStatus" runat="server" CssClass="text-blue-600 font-semibold"></asp:Label>
                    </div>
                </section>
            </div>
            <asp:Image ID="imgKwitansi" runat="server" Width="300px" /><br />
            <asp:Image ID="imgResep" runat="server" Width="300px" /><br />
            <asp:Image ID="imgPendukung" runat="server" Width="300px" />
        </main>
    </form>
</body>
</html>
