<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard.aspx.vb" Inherits="MedicalReimbursement.Dashboaed" %>

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
                <asp:Button ID="btnDashboard" runat="server" Text="Dashboard"  CssClass="bg-[#FFAA0A] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:cursor-pointer" />
                <asp:Button ID="btnMedicalReimbursement" runat="server" Text="Medical Reimbursement" CssClass="bg-[#145445] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:bg-[#FFAA0A] cursor-pointer" />
                <asp:Button ID="btnEmployees" runat="server" Text="Employees" CssClass="bg-[#145445] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:bg-[#FFAA0A] cursor-pointer" />
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

            <!-- Cards container -->
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
                                        <th>Approval</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="font-semibold text-sm"><%# Eval("Nama") %></td>
                                <td class="text-xs text-gray-400"><%# Eval("Tanggal") %></td>
                                <td class="text-xs font-semibold text-blue-400"><%# Eval("Kategori") %></td>
                                <td class="text-xs font-bold">
                                    <span class='<%# If(DirectCast(Eval("status_approval"), Boolean), "text-green-600", "text-red-600") %>'>
                                        <%# If(DirectCast(Eval("status_approval"), Boolean), "Approved", "Pending") %>
                                    </span>
                                </td>
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
        </main>
    </form>
</body>
</html>
