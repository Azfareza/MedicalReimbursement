<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard_EMP.aspx.vb" Inherits="MedicalReimbursement.Dashboard_EMP" MasterPageFile="~/Header.master" %>

<asp:Content ID="headContent" runat="server" ContentPlaceHolderID="head">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/2.3.0/js/dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/2.3.0/css/dataTables.dataTables.min.css">
    <script>
        $(document).ready(function () {
            $('#requestsTable').DataTable({
                "paging": true,
                "pageLength": 5,
                "searching": false,
                "info": false,
                "ordering": true,
                "order": [[1, "desc"]],
                "dom": 'rtip'
            });
        });
    </script>
</asp:Content>

<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="SubContent">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />
    <div class="max-w-screen-xl mx-auto px-4 sm:px-6 lg:px-8">
        <section aria-label="Summary sections" class="grid grid-cols-1 md:grid-cols-2 gap-8">
            <article>
                <h2 class="text-[#f7931e] font-semibold text-xl mb-4">Personal Summary</h2>
                <div class="bg-white rounded-xl p-6 grid grid-cols-1 sm:grid-cols-2 gap-y-6 gap-x-12 max-w-md">
                    <div>
                        <p class="text-[#145a52] font-semibold mb-1 text-md">
                            Full Name
                        </p>
                        <asp:Label ID="FullNameLabel" runat="server" Text="Budiono Siregar" CssClass="text-black"></asp:Label>
                    </div>
                    <div>
                        <p class="text-[#145a52] font-semibold mb-1 text-md">
                            NIP
                        </p>
                        <asp:Label ID="NIPLabel" runat="server" Text="65132879551" CssClass="text-black"></asp:Label>
                    </div>
                    <div>
                        <p class="text-[#145a52] font-semibold mb-1 text-md">
                            Departement
                        </p>
                        <asp:Label ID="DepartementLabel" runat="server" Text="IT" CssClass="text-black"></asp:Label>
                    </div>
                    <div>
                        <p class="text-[#145a52] font-semibold mb-1 text-md">
                            Level
                        </p>
                        <asp:Label ID="JabatanLabel" runat="server" Text="Junior" CssClass="text-black"></asp:Label>
                    </div>
                    <div>
                        <p class="text-[#145a52] font-semibold mb-1 text-md">
                            Status
                        </p>
                        <asp:Label ID="StatusLabel" runat="server" Text="PKWTT" CssClass="text-black"></asp:Label>
                    </div>
                    <div>
                        <p class="text-[#145a52] font-semibold mb-1 text-md">
                            No. HP
                        </p>
                        <asp:Label ID="selulerlabel" runat="server" Text="PKWTT" CssClass="text-black"></asp:Label>
                    </div>
                </div>
            </article>
            <article>
                <h2 class="text-[#f7931e] font-semibold text-xl mb-4">Last Request</h2>
                <div class="bg-white rounded-xl p-6"> <asp:Repeater ID="rptRequests" runat="server">
                        <HeaderTemplate>
                            <table id="requestsTable" class="display w-full">
                                <thead>
                                    <tr>
                                        <th>Kode Klaim</th>
                                        <th>Date</th>
                                        <th>Category</th>
                                        <th>Status Terakhir</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                                    <tr style="cursor:pointer" class="hover:bg-gray-100 transition duration-150" onclick='location.href="MR_EMP.aspx"'>
                                        <td class="font-semibold text-sm"><%# Eval("KdKlaim") %></td>
                                        <td class="text-xs text-gray-400"><%# Eval("Tanggalpengajuan") %></td>
                                        <td class="text-xs font-semibold text-blue-400"><%# Eval("Kategori") %></td>
                                        <td class="text-xs font-semibold text-blue-400"><%# Eval("Status_Terakhir") %></td>
                                    </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </article>
        </section>
    </div>
</asp:Content>