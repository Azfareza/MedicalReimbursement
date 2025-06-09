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
                            <td class="p-3"><%# Eval("Role") %></td>
                            <td class="p-3">
                                <asp:TemplateField>
                                     <ItemTemplate>
                                          <asp:Button Text="Edit" ID="btnEdit" runat="server" CssClass="bg-[#0052cc] p-2 rounded-sm text-white" CommandArgument='<%# Eval("NIP") %>'></asp:Button>
                                     </ItemTemplate>
                                </asp:TemplateField>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </section>

</asp:Content>