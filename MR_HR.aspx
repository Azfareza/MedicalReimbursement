<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MR_HR.aspx.vb" Inherits="MedicalReimbursement.MR_HR" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <title>Medical Reimbursement</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <%--<link href="https://fonts.bunny.net/css?family=figtree:400,500,600&display=swap" rel="stylesheet" />--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;700&amp;display=swap" rel="stylesheet" />
    <style>
        body {
            font-family: 'Inter', sans-serif;
        }
    </style>
    <script type="text/javascript">
        function showTab(tabId) {
            document.querySelectorAll('.tab-content').forEach(function (el) {
                el.classList.add('hidden');
            });

            // Reset all button styles
            document.querySelectorAll('.tab-button').forEach(function (el) {
                el.classList.remove('bg-[#FFAA0A]', 'text-white');
                el.classList.add('bg-gray-300', 'text-gray-700');
            });

            // Show selected tab and highlight button
            document.getElementById(tabId).classList.remove('hidden');
            document.getElementById(tabId + 'Btn').classList.add('bg-[#FFAA0A]', 'text-white');
            document.getElementById(tabId + 'Btn').classList.remove('bg-gray-300', 'text-gray-700');
        }

        // Set default tab
        document.addEventListener("DOMContentLoaded", function () {
            showTab('tab1');
            const approveBtn = document.getElementById('approveBtn');
            const approveForm = document.getElementById('approveForm');
            const approveSubmit = document.getElementById('approveSubmit');

            const rejectBtn = document.getElementById('rejectBtn');
            const rejectForm = document.getElementById('rejectForm');
            const rejectSubmit = document.getElementById('rejectSubmit');
            const rejectNote = document.getElementById('rejectNote');

            approveBtn.addEventListener('click', function () {
                approveForm.classList.remove('hidden');
                rejectForm.classList.add('hidden');
            });

            approveSubmit.addEventListener('click', function () {
                alert("Berhasil Diterima");
            });

            rejectBtn.addEventListener('click', function () {
                rejectForm.classList.remove('hidden');
                approveForm.classList.add('hidden');
            });

            rejectSubmit.addEventListener('click', function () {
                const note = rejectNote.value.trim();
                if (note === '') {
                    alert('Mohon isi catatan penolakan.');
                } else {
                    alert('Catatan Penolakan:\n' + note);
                }
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
                <asp:Button ID="btnDashboard" runat="server" Text="Dashboard" CssClass="bg-[#145445] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:bg-[#FFAA0A] cursor-pointer" />
                <asp:Button ID="btnMedicalReimbursement" runat="server" Text="Medical Reimbursement" CssClass="bg-[#FFAA0A] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:cursor-pointer" />
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
                        <span class="text-[#145445] font-extrabold text-2xl md:text-3xl ml-2">MEDICAL REIMBURSEMENT</span>
                    </div>
                </div>
            </div>

<%--MODAL--%>
    <asp:Panel ID="pnlModal" runat="server" CssClass="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 hidden">
        <div class="bg-gray-100 bg-opacity-90 rounded-2xl p-10 max-w-6xl w-full shadow-lg relative space-y-6"">
            <asp:Button ID="btnCloseModal" runat="server" Text="×" CssClass="absolute top-4 right-4 text-3xl font-bold text-gray-700 hover:text-gray-900 bg-transparent border-none cursor-pointer"  />
                    
                <div class="grid grid-cols-1 sm:grid-cols-3 gap-x-12 gap-y-6">
                    <!-- Left Column -->
                    <div class="space-y-4">
                          <div>
                            <asp:Label ID="lblNIPModal" runat="server" AssociatedControlID="txtNIPModal" Text="NIP" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                            <asp:TextBox ID="txtNIPModal" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
                          </div>
                          <div>
                            <asp:Label ID="lblNamaModal" runat="server" AssociatedControlID="txtNamaModal" Text="Nama" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                            <asp:TextBox ID="txtNamaModal" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs" TextMode="Email"></asp:TextBox>
                          </div>
                          <div>
                            <asp:Label ID="lblDepartemenModal" runat="server" AssociatedControlID="txtDepartemenModal" Text="Departemen" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                            <asp:TextBox ID="txtDepartemenModal" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
                          </div>
                        </div>

                    <!-- Middle Column -->
                    <div class="space-y-4">
                      <div>
                        <asp:Label ID="lblKategoriModal" runat="server" AssociatedControlID="txtKategoriModal" Text="Kategori" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                        <asp:TextBox ID="txtKategoriModal" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
                      </div>
                      <div>
                        <asp:Label ID="lblTanggalModal" runat="server" AssociatedControlID="txtTanggalModal" Text="Tanggal" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                        <asp:TextBox ID="txtTanggalModal" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
                      </div>
                    </div>

                    <!-- Right Column -->
                    <div class="space-y-4">
                        <div class="flex space-x-2 text-xs font-semibold mb-2">
                            <button type="button" onclick="showTab('tab1')" class="tab-button px-4 py-2 rounded bg-[#FFAA0A] text-white" id="tab1Btn">Kwitansi</button>
                            <button type="button" onclick="showTab('tab2')" class="tab-button px-4 py-2 rounded bg-gray-300 text-gray-700" id="tab2Btn">Resep</button>
                            <button type="button" onclick="showTab('tab3')" class="tab-button px-4 py-2 rounded bg-gray-300 text-gray-700" id="tab3Btn">Pendukung</button>
                        </div>
                        <div id="tab1" class="tab-content space-y-4">
                            <div>
                                <asp:Label ID="lblKwitansi" runat="server" Text="Kwitansi" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                                <asp:Image ID="imgKwitansi" runat="server" Width="300px" /><br />
                            </div>
                        </div>

                        <div id="tab2" class="tab-content hidden space-y-4">
                            <div>
                                <asp:Label ID="lblResep" runat="server" Text="Resep Obat" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                                <h2>Resep Obat</h2>
                                <asp:Image ID="imgResep" runat="server" Width="300px" /><br />
                            </div>
                        </div>

                        <div id="tab3" class="tab-content hidden space-y-4">
                            <div>
                                <asp:Label ID="lblPendukung" runat="server" Text="File Pendukung" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                                <h2>File Pendukung</h2>
                                <asp:Image ID="imgPendukung" runat="server" Width="300px" />
                            </div>
                        </div>
                    </div>
                </div>
                
                
                <div id="reviewOption" class="flex justify-center w-full">
                    <div class="flex flex-col items-center space-y-4">
                        <!-- Approve & Reject Buttons -->
                        <div class="flex space-x-4">
                            <button type="button" id="approveBtn" onclick="showApprove()" class="bg-green-400 hover:bg-green-500 text-white font-bold py-2 px-6 rounded-full text-lg">
                                Approve
                            </button>
                            <button type="button" id="rejectBtn" onclick="showReject()" class="bg-red-400 hover:bg-red-500 text-white font-bold py-2 px-6 rounded-full text-lg">
                                Reject
                            </button>
                        </div>
                    
                        <!-- Reject Form (Initially Hidden) -->
                        <div id="rejectForm" class="hidden flex flex-col items-center space-y-4">
                            <textarea id="rejectNote" placeholder="Catatan Penolakan" class="w-[300px] p-3 border rounded-lg text-sm" rows="5"></textarea>
                            <button id="rejectSubmit" type="button" class="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-6 rounded-full text-lg">
                                Submit Reject
                            </button>
                        </div>
                    
                        <div id="approveForm" class="hidden flex flex-col items-center space-y-4">
                            <button id="approveSubmit" type="button" class="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-6 rounded-full text-lg">
                                Submit Approve
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <%--END MODAL--%>


             <section class="max-w-5xl mx-auto space-y-10">
                <div>
                  <h2 class="text-[#f9a01b] font-extrabold text-2xl mb-6">
                    REQUEST LIST
                  </h2>
                  <asp:GridView ID="gvRequestList" runat="server" CssClass="w-full text-center text-sm" AutoGenerateColumns="False" GridLines="None" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="font-semibold text-black" RowStyle-CssClass="bg-white rounded-full">
                       <Columns>
                        <asp:BoundField DataField="NIP" ID="RtrNIP" HeaderText="NIP" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                        <asp:BoundField DataField="Nama" ID="RtrNama" HeaderText="Nama" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                        <asp:BoundField DataField="Departemen" ID="RtrDept" HeaderText="Departemen" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                        <asp:BoundField DataField="Kategori" ID="RtrKategori" HeaderText="Kategori" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                        <asp:BoundField DataField="Tanggal" ID="RtrTanggal" HeaderText="Tanggal" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton 
                                    ID="btnSelect" 
                                    runat="server" 
                                    CommandName="Select" 
                                    CssClass="text-blue-500 hover:text-blue-700"
                                    ToolTip="Lihat Detail">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-5 h-5 inline">
                                        <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                                        <path stroke-linecap="round" stroke-linejoin="round" d="M2.458 12C3.732 7.943 7.523 5.25 12 5.25s8.268 2.693 9.542 6.75c-1.274 4.057-5.065 6.75-9.542 6.75S3.732 16.057 2.458 12z" />
                                    </svg>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="py-4 px-6 text-center" />
                        </asp:TemplateField>
                       </Columns>
                    </asp:GridView>
                </div>
                <div>
                    <h2 class="text-[#f9a01b] font-extrabold text-2xl mb-6">
                        HISTORY
                    </h2>
                    <asp:GridView ID="gvHistory" runat="server" CssClass="w-full text-center text-sm" AutoGenerateColumns="False" GridLines="None" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="font-semibold text-black" RowStyle-CssClass="bg-white rounded-full" OnRowDataBound="gvHistory_RowDataBound" >
                        <Columns>
                            <asp:BoundField DataField="NIP" HeaderText="NIP" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                            <asp:BoundField DataField="Nama" HeaderText="Nama" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                            <asp:BoundField DataField="Departemen" HeaderText="Departemen" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                            <asp:BoundField DataField="Kategori" HeaderText="Kategori" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                            <asp:BoundField DataField="Tanggal" HeaderText="Tanggal" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                            <asp:BoundField DataField="status_approval" HeaderText="Status" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton 
                                        ID="btnSelect" 
                                        runat="server" 
                                        CommandName="Select" 
                                        CssClass="text-blue-500 hover:text-blue-700"
                                        ToolTip="Lihat Detail">
                                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-5 h-5 inline">
                                            <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                                            <path stroke-linecap="round" stroke-linejoin="round" d="M2.458 12C3.732 7.943 7.523 5.25 12 5.25s8.268 2.693 9.542 6.75c-1.274 4.057-5.065 6.75-9.542 6.75S3.732 16.057 2.458 12z" />
                                        </svg>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle CssClass="py-4 px-6 text-center" />
                            </asp:TemplateField>
                       </Columns>
                    </asp:GridView>
                </div>
            </section>
        </main>
    </form>
</body>
</html>
