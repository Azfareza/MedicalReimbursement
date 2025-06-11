<%-- Dashboard_Admin.aspx --%>
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Header.master" CodeBehind="Dashboard_Admin.aspx.vb" Inherits="MedicalReimbursement.Dashboard_Admin" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/2.3.2/js/dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/2.3.2/css/dataTables.dataTables.min.css">
    <script>
        $(document).ready(function () {
            $('#tabelPegawai').DataTable({
                "paging": true,
                "pageLength": 12,
                "searching": true,
                "info": true,
                "ordering": true,
                "dom": '<"top"f>rt<"bottom"p><"clear">'
            });
        });
    </script>
    <style>
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
        }

        .modal-content {
            background-color: white;
            padding: 24px;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            width: 400px;
            max-width: 90%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    <span class="text-[#145445] font-extrabold text-2xl md:text-3xl ml-2">ADMIN PAGE</span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SubContent" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />

    <section class="bg-white rounded-lg p-5">
        <div class="flex justify-between items-center mb-4 px-1">
            <h2 class="text-[#f97316] font-semibold text-lg">Daftar Pegawai</h2>
        </div>

        <table id="tabelPegawai" class="w-full bg-white rounded-lg shadow-sm border border-gray-400 text-sm display">
            <thead class="text-left font-semibold border-b border-gray-400">
                <tr>
                    <th class="p-3">NIP</th>
                    <th class="p-3">Nama Lengkap</th>
                    <th class="p-3">Departemen</th>
                    <th class="p-3">Jabatan</th>
                    <th class="p-3">Status</th>
                    <th class="p-3">Role</th>
                    <th class="p-3"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPegawai" runat="server">
                    <ItemTemplate>
                        <tr class="border border-gray-400 rounded-md mt-2">
                            <td class="p-3"><%# Eval("NIP") %></td>
                            <td class="p-3"><%# Eval("NamaLengkap") %></td>
                            <td class="p-3"><%# Eval("NamaDepartemen") %></td>
                            <td class="p-3"><%# Eval("NamaJabatan") %></td>
                            <td class="p-3"><%# Eval("Status") %></td>
                            <td class="p-3">
                                <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role") %>'></asp:Label>
                            </td>
                            <td class="p-3">
                                <asp:Button Text="Edit" ID="btnEdit" runat="server" CssClass="bg-[#0052cc] p-2 rounded-sm text-white" CommandArgument='<%# Eval("NIP") %>' CommandName="Edit"></asp:Button>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </section>

    <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlEditModal" runat="server" Visible="false" CssClass="modal-overlay">
                <div class="modal-content">
                    <h3 class="text-xl font-bold mb-4">Update data pada <asp:Label ID="lblEditNIP" runat="server" CssClass="font-semibold"></asp:Label></h3>
                    
                    <div class="mt-4">
                        <label for="<%= ddlNewRole.ClientID %>" class="block text-gray-700 text-sm font-bold mb-2">Pilih Role Baru:</label>
                        <asp:DropDownList ID="ddlNewRole" runat="server" CssClass="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline">
                        </asp:DropDownList>
                    </div>

                    <div class="mt-6 flex justify-end gap-2">
                        <asp:Button ID="btnSaveEdit" runat="server" Text="Simpan" CssClass="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded" OnClick="btnSaveEdit_Click" />
                        <asp:Button ID="btnCloseModal" runat="server" Text="Tutup" CssClass="bg-gray-300 hover:bg-gray-400 text-gray-800 font-bold py-2 px-4 rounded" OnClick="btnCloseModal_Click" />
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rptPegawai" EventName="ItemCommand" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>