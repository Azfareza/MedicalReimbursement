<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard_DIR.aspx.vb" Inherits="MedicalReimbursement.Dashboard_DIR" MasterPageFile="~/Header.master" %>

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

            const categoryData = {};

            $('#requestsTable tbody tr').each(function () {
                const category = $(this).find('td:eq(2)').text().trim();

                if (categoryData[category]) {
                    categoryData[category]++;
                } else {
                    categoryData[category] = 1;
                }
            });

            const categoryCtx = document.getElementById('categoryChart').getContext('2d');
            new Chart(categoryCtx, {
                type: 'doughnut',
                data: {
                    labels: Object.keys(categoryData),
                    datasets: [{
                        data: Object.values(categoryData),
                        backgroundColor: [
                            '#FFA916',
                            '#207B67',
                            '#FF5356',
                        ],
                        hoverOffset: 4
                    }]
                },
                options: {
                    responsive: false,
                    plugins: {
                        legend: {
                            position: 'bottom',
                        }
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="SubContent">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />

    <div class="container mx-auto px-4 flex flex-col md:flex-row gap-6">
        <div class="w-full flex-1/3">
            <h2 class="text-[#f9a01b] font-extrabold text-2xl mb-4">Requests</h2>
            <section aria-label="Requests list" class="bg-white rounded-lg p-5">
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
                                    <tr style="cursor:pointer" class="hover:bg-gray-100 transition duration-150" onclick='location.href="MR_DIREKSI.aspx"'>
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
        </div>
        <div class="flex-1 w-full">
            <h2 class="text-[#f9a01b] font-extrabold text-2xl mb-4">Category Distribution</h2>
            <section aria-label="Category donut chart" class="bg-white rounded-lg px-5">
                <div class="flex justify-center items-center">
                    <canvas id="categoryChart" class="h-[200px]"></canvas>
                </div>
            </section>
        </div>
    </div>
</asp:Content>