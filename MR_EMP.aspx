<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MR_EMP.aspx.vb" Inherits="MedicalReimbursement.MR_EMP" MasterPageFile="~/Header.master"%>

<asp:Content ID="headContent" runat="server" ContentPlaceHolderID="head">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/2.3.2/js/dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/2.3.2/css/dataTables.dataTables.min.css">
    <script>
        $(document).ready(function () {
            $('#logHistoryTable').DataTable({
                "paging": true,
                "pageLength": 12,
                "searching": true,
                "info": true,
                "ordering": true,
                "dom": '<"top"f>rt<"bottom"p><"clear">'
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    <span class="text-[#145445] font-extrabold text-2xl md:text-3xl ml-2">REIMBURSEMENT</span>
</asp:Content>

<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="SubContent">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />
            
    <div class="flex flex-col md:flex-row gap-6 container mx-auto px-4">
        <section class="bg-white rounded-lg p-5 flex-1 w-1/2">
            <div class="flex justify-between items-center mb-4 px-1">
                <h2 class="text-[#f97316] font-semibold text-lg">Log History</h2>
            </div>

            <table id="logHistoryTable" class="w-full bg-white rounded-lg shadow-sm border border-gray-400 text-sm display">
                <thead class="text-left font-semibold border-b border-gray-400">
                    <tr>
                        <th class="p-3">Category</th>
                        <th class="p-3">Req. Date</th>
                        <th class="p-3">Status</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptLogHistory" runat="server">
                        <ItemTemplate>
                            <tr class="border border-gray-400 rounded-md mt-2">
                                <td class="p-3"><%# Eval("Kategori") %></td>
                                <td class="p-3"><%# Eval("TanggalPengajuan") %></td>
                                <td class="p-3"><%# Eval("Status_Terakhir") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
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
</asp:Content>