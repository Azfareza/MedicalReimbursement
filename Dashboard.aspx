<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="MedicalReimbursement.Dashboard" MasterPageFile="~/Header.master" %>

<asp:Content ID="headContent" runat="server" ContentPlaceHolderID="head">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/2.3.0/js/dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/2.3.0/css/dataTables.dataTables.min.css">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
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

        function renderCharts(statusArr, kategoriArr) {
            new Chart(document.getElementById('statusChart'), {
                type: 'doughnut',
                data: {
                    labels: ['Processed', 'Unprocessed'],
                    datasets: [{
                        data: statusArr,
                        backgroundColor: ['#145445', '#FFAA0A']
                    }]
                },
                options: {
                    cutout: '70%',
                    responsive: false,
                    plugins: { legend: { display: false } }
                }
            });

            new Chart(document.getElementById('categoryChart'), {
                type: 'doughnut',
                data: {
                    labels: ['Rawat Jalan', 'Kacamata', 'Persalinan'],
                    datasets: [{
                        data: kategoriArr,
                        backgroundColor: ['#EF2B2B', '#0D8ACD', '#F15CA4']
                    }]
                },
                options: {
                    cutout: '70%',
                    responsive: false,
                    plugins: { legend: { display: false } }
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="SubContent">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />

    <div class="flex flex-col md:flex-row gap-6">
        <!-- Requests card -->
        <section aria-label="Requests list" class="bg-white rounded-lg p-5 flex-1 w-1/3">
            <h2 class="font-semibold text-sm mb-4 text-center">Requests</h2>
            <asp:Repeater ID="rptRequests" runat="server">
                <HeaderTemplate>
                    <table id="requestsTable" class="display w-full">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Date</th>
                                <th>Category</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr style="cursor:pointer" class="hover:bg-gray-100 transition duration-150" onclick='location.href="MR_HR.aspx"'>
                        <td class="font-semibold text-sm"><%# Eval("Namalengkap") %></td>
                        <td class="text-xs text-gray-400"><%# Eval("Tanggalpengajuan") %></td>
                        <td class="text-xs font-semibold text-blue-400"><%# Eval("Kategori") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </section>

        <!-- Requests Process card -->
        <section aria-label="Requests process chart" class="bg-white rounded-lg p-5 flex-1 max-w-xs">
            <h2 class="font-semibold text-sm mb-4 text-center">Requests Process</h2>
            <div class="flex justify-center mb-4">
                <canvas id="statusChart" width="120" height="120"></canvas>
            </div>
            <ul class="text-xs space-y-2">
                <li class="flex items-center gap-2">
                    <span class="inline-block w-4 h-4 bg-[#145445] border border-[#145445]"></span>
                    <span>Processed (Approved, Reject)</span>
                </li>
                <li class="flex items-center gap-2">
                    <span class="inline-block w-4 h-4 bg-[#FFAA0A] border border-[#FFAA0A]"></span>
                    <span>Unprocessed</span>
                </li>
            </ul>
        </section>

        <!-- Category card -->
        <section aria-label="Category chart" class="bg-white rounded-lg p-5 flex-1 max-w-xs">
            <h2 class="font-semibold text-sm mb-4 text-center">Category</h2>
            <div class="flex justify-center mb-4">
                <canvas id="categoryChart" width="120" height="120"></canvas>
            </div>
            <ul class="text-xs space-y-2">
                <li class="flex items-center gap-2">
                    <span class="inline-block w-4 h-4 bg-[#EF2B2B] border border-[#EF2B2B]"></span>
                    <span>Rawat Jalan</span>
                </li>
                <li class="flex items-center gap-2">
                    <span class="inline-block w-4 h-4 bg-[#0D8ACD] border border-[#0D8ACD]"></span>
                    <span>Kacamata</span>
                </li>
                <li class="flex items-center gap-2">
                    <span class="inline-block w-4 h-4 bg-[#F15CA4] border border-[#F15CA4]"></span>
                    <span>Persalinan</span>
                </li>
            </ul>
        </section>
    </div>
</asp:Content>