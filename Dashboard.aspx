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
            <!-- Cards container -->
            <div class="flex flex-col md:flex-row gap-6">
                <!-- Requests card -->
                <section aria-label="Requests list" class="bg-white rounded-lg p-5 flex-1 max-w-md">
                    <h2 class="font-semibold text-sm mb-4 text-center">Requests</h2>
                    <asp:Repeater ID="rptRequests" runat="server">
                        <ItemTemplate>
                            <li class="bg-[#F9F9F9] rounded-lg flex justify-between items-center px-4 py-3">
                                <span class="font-semibold text-sm"><%# Eval("Nama") %></span>
                                <span class="text-xs text-gray-400"><%# Eval("Tanggal") %></span>
                                <span class="text-xs font-semibold text-blue-400"><%# Eval("Kategori") %></span>
                                <span class="text-xs font-bold text-red-600 ml-4"><%# Eval("Status") %></span>
                            </li>
                        </ItemTemplate>
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
