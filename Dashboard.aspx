<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="MedicalReimbursement.Dashboard" MasterPageFile="~/Header.master" %>

<asp:Content ID="headContent" runat="server" ContentPlaceHolderID="head">
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
</asp:Content>
            <%--<div class="mx-auto mt-10 grid max-w-2xl grid-cols-1 gap-x-8 gap-y-16 lg:mx-0 lg:max-w-none lg:grid-cols-3">
                  <article class="flex max-w-xl flex-col items-start justify-between">
                    <div class="flex items-center gap-x-4 text-xs">
                      <time datetime="2020-03-16" class="text-gray-500">Mar 16, 2020</time>
                      <a href="#" class="relative z-10 rounded-full bg-gray-50 px-3 py-1.5 font-medium text-gray-600 hover:bg-gray-100">Marketing</a>
                    </div>
                    <div class="group relative">
                      <h3 class="mt-3 text-lg/6 font-semibold text-gray-900 group-hover:text-gray-600">
                        <a href="#">
                          <span class="absolute inset-0"></span>
                          Boost your conversion rate
                        </a>
                      </h3>
                      <p class="mt-5 line-clamp-3 text-sm/6 text-gray-600">Illo sint voluptas. Error voluptates culpa eligendi. Hic vel totam vitae illo. Non aliquid explicabo necessitatibus unde. Sed exercitationem placeat consectetur nulla deserunt vel. Iusto corrupti dicta.</p>
                    </div>
                    <div class="relative mt-8 flex items-center gap-x-4">
                      <img src="https://images.unsplash.com/photo-1519244703995-f4e0f30006d5?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80" alt="" class="size-10 rounded-full bg-gray-50">
                      <div class="text-sm/6">
                        <p class="font-semibold text-gray-900">
                          <a href="#">
                            <span class="absolute inset-0"></span>
                            Michael Foster
                          </a>
                        </p>
                        <p class="text-gray-600">Co-Founder / CTO</p>
                      </div>
                    </div>
                  </article>
                  <article class="flex max-w-xl flex-col items-start justify-between">
                    <div class="flex items-center gap-x-4 text-xs">
                      <time datetime="2020-03-16" class="text-gray-500">Mar 16, 2020</time>
                      <a href="#" class="relative z-10 rounded-full bg-gray-50 px-3 py-1.5 font-medium text-gray-600 hover:bg-gray-100">Marketing</a>
                    </div>
                    <div class="group relative">
                      <h3 class="mt-3 text-lg/6 font-semibold text-gray-900 group-hover:text-gray-600">
                        <a href="#">
                          <span class="absolute inset-0"></span>
                          Boost your conversion rate
                        </a>
                      </h3>
                      <p class="mt-5 line-clamp-3 text-sm/6 text-gray-600">Illo sint voluptas. Error voluptates culpa eligendi. Hic vel totam vitae illo. Non aliquid explicabo necessitatibus unde. Sed exercitationem placeat consectetur nulla deserunt vel. Iusto corrupti dicta.</p>
                    </div>
                    <div class="relative mt-8 flex items-center gap-x-4">
                      <img src="https://images.unsplash.com/photo-1519244703995-f4e0f30006d5?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80" alt="" class="size-10 rounded-full bg-gray-50">
                      <div class="text-sm/6">
                        <p class="font-semibold text-gray-900">
                          <a href="#">
                            <span class="absolute inset-0"></span>
                            Michael Foster
                          </a>
                        </p>
                        <p class="text-gray-600">Co-Founder / CTO</p>
                      </div>
                    </div>
                  </article>
                  <article class="flex max-w-xl flex-col items-start justify-between">
                    <div class="flex items-center gap-x-4 text-xs">
                      <time datetime="2020-03-16" class="text-gray-500">Mar 16, 2020</time>
                      <a href="#" class="relative z-10 rounded-full bg-gray-50 px-3 py-1.5 font-medium text-gray-600 hover:bg-gray-100">Marketing</a>
                    </div>
                    <div class="group relative">
                      <h3 class="mt-3 text-lg/6 font-semibold text-gray-900 group-hover:text-gray-600">
                        <a href="#">
                          <span class="absolute inset-0"></span>
                          Boost your conversion rate
                        </a>
                      </h3>
                      <p class="mt-5 line-clamp-3 text-sm/6 text-gray-600">Illo sint voluptas. Error voluptates culpa eligendi. Hic vel totam vitae illo. Non aliquid explicabo necessitatibus unde. Sed exercitationem placeat consectetur nulla deserunt vel. Iusto corrupti dicta.</p>
                    </div>
                    <div class="relative mt-8 flex items-center gap-x-4">
                      <img src="https://images.unsplash.com/photo-1519244703995-f4e0f30006d5?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80" alt="" class="size-10 rounded-full bg-gray-50">
                      <div class="text-sm/6">
                        <p class="font-semibold text-gray-900">
                          <a href="#">
                            <span class="absolute inset-0"></span>
                            Michael Foster
                          </a>
                        </p>
                        <p class="text-gray-600">Co-Founder / CTO</p>
                      </div>
                    </div>
                  </article>
            </div>--%>

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
        <section aria-label="Requests process donut chart" class="bg-white rounded-lg p-5 flex-1 max-w-xs">
            <h2 class="font-semibold text-sm mb-4 text-center">Requests Process</h2>
            <div class="flex justify-center mb-4">
                <svg aria-hidden="true" class="transform -rotate-90" height="120" viewBox="0 0 42 42" width="120">
                    <circle class="text-orange-400" cx="21" cy="21" fill="transparent" r="15.9" stroke="#FFAA0A" stroke-dasharray="50 50" stroke-dashoffset="25" stroke-width="8"></circle>
                    <circle class="text-teal-700" cx="21" cy="21" fill="transparent" r="15.9" stroke="#145445" stroke-dasharray="50 50" stroke-dashoffset="75" stroke-width="8"></circle>
                </svg>
            </div>
            <ul class="text-xs space-y-2">
                <li class="flex items-center gap-2">
                    <span aria-hidden="true" class="inline-block w-4 h-4 bg-[#145445] border border-[#145445]"></span>
                    <span>Processed (Approved, Reject)</span>
                </li>
                <li class="flex items-center gap-2">
                    <span aria-hidden="true" class="inline-block w-4 h-4 bg-[#FFAA0A] border border-[#FFAA0A]"></span>
                    <span>Unprocessed</span>
                </li>
            </ul>
        </section>
        <!-- Category card -->
        <section aria-label="Category donut chart" class="bg-white rounded-lg p-5 flex-1 max-w-xs">
            <h2 class="font-semibold text-sm mb-4 text-center">Category</h2>
            <div class="flex justify-center mb-4">
                <svg aria-hidden="true" class="transform -rotate-90" height="120" viewBox="0 0 42 42" width="120">
                    <circle cx="21" cy="21" fill="transparent" r="15.9" stroke="#EF2B2B" stroke-dasharray="80 20" stroke-dashoffset="25" stroke-width="8"></circle>
                    <circle cx="21" cy="21" fill="transparent" r="15.9" stroke="#0D8ACD" stroke-dasharray="15 85" stroke-dashoffset="105" stroke-width="8"></circle>
                    <circle cx="21" cy="21" fill="transparent" r="15.9" stroke="#F15CA4" stroke-dasharray="5 95" stroke-dashoffset="120" stroke-width="8"></circle>
                </svg>
            </div>
            <ul class="text-xs space-y-2">
                <li class="flex items-center gap-2">
                    <span aria-hidden="true" class="inline-block w-4 h-4 bg-[#EF2B2B] border border-[#EF2B2B]"></span>
                    <span>Rawat Jalan</span>
                </li>
                <li class="flex items-center gap-2">
                    <span aria-hidden="true" class="inline-block w-4 h-4 bg-[#0D8ACD] border border-[#0D8ACD]"></span>
                    <span>Kacamata</span>
                </li>
                <li class="flex items-center gap-2">
                    <span aria-hidden="true" class="inline-block w-4 h-4 bg-[#F15CA4] border border-[#F15CA4]"></span>
                    <span>Persalinan</span>
                </li>
            </ul>
        </section>
    </div>
</asp:Content>