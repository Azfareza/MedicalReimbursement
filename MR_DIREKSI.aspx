<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MR_DIREKSI.aspx.vb" Inherits="MedicalReimbursement.MR_DIREKSI" MasterPageFile="~/Header.master" %>

<asp:Content ID="headContent" runat="server" ContentPlaceHolderID="head">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;700&amp;display=swap" rel="stylesheet" />
    <script src="https://cdn.tailwindcss.com"></script>
    <%--<link href="https://fonts.bunny.net/css?family=figtree:400,500,600&display=swap" rel="stylesheet" />--%>

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


       function openFullscreen(imageId) {
            const img = document.getElementById('<%= imgKwitansi.ClientID %>'.replace("imgKwitansi", imageId));
            if (img.requestFullscreen) {
                img.requestFullscreen();
            } else if (img.webkitRequestFullscreen) {
                img.webkitRequestFullscreen(); // Safari
            } else if (img.msRequestFullscreen) {
                img.msRequestFullscreen(); // IE11
            }
        }

        function showReject() {
            document.getElementById("rejectForm").classList.remove("hidden");
        }

        function submitReject() {
            const note = document.getElementById("rejectNote").value.trim();
            if (note === "") {
                alert("Catatan penolakan wajib diisi.");
                return;
            }
            __doPostBack('<%= btntidaksetuju.UniqueID %>', '');
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    <span class="text-[#145445] font-extrabold text-2xl md:text-3xl ml-2">REIMBURSEMENT</span>
</asp:Content>

<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="SubContent">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />
    <%--MODAL--%>
    <asp:Panel ID="pnlModal" runat="server" CssClass="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 hidden">
        <div class="bg-gray-100 bg-opacity-90 rounded-2xl p-10 max-w-6xl w-full shadow-lg relative space-y-6 overflow-y-auto max-h-[90vh]">
            <asp:Button ID="btnCloseModal" runat="server" Text="×" CssClass="absolute top-4 right-4 text-3xl font-bold text-gray-700 hover:text-gray-900 bg-transparent border-none cursor-pointer" />

            <div class="grid grid-cols-1 sm:grid-cols-3 gap-x-12 gap-y-6">
                <!-- Left Column -->
                <div class="space-y-4">
                    <div>
                        <asp:Label ID="lblClaim" runat="server" AssociatedControlID="txtClaim" Text="Kode Klaim" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                        <asp:TextBox ID="txtClaim" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
                    </div>
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
                    <div>
                        <asp:Label ID="lblBiayaModal" runat="server" AssociatedControlID="txtBiayaModal" Text="Biaya" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                        <asp:TextBox ID="txtbiayaModal" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
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
                            <asp:Image ID="imgKwitansi" runat="server" CssClass="max-w-full h-auto" />
                            <button type="button" onclick="openFullscreen('imgKwitansi')" class="text-xs text-blue-500 underline mt-1">Perbesar Gambar</button>
                        </div>
                    </div>

                    <div id="tab2" class="tab-content hidden space-y-4">
                        <div>
                            <asp:Label ID="lblResep" runat="server" Text="Resep Obat" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                            <h2>Resep Obat</h2>
                            <asp:Image ID="imgResep" runat="server" CssClass="max-w-full h-auto" />
                            <button type="button" onclick="openFullscreen('imgResep')" class="text-xs text-blue-500 underline mt-1">Perbesar Gambar</button>
                        </div>
                    </div>

                    <div id="tab3" class="tab-content hidden space-y-4">
                        <div>
                            <asp:Label ID="lblPendukung" runat="server" Text="File Pendukung" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
                            <h2>File Pendukung</h2>
                            <asp:Image ID="imgPendukung" runat="server" CssClass="max-w-full h-auto" />
                            <button type="button" onclick="openFullscreen('imgPendukung')" class="text-xs text-blue-500 underline mt-1">Perbesar Gambar</button>
                        </div>
                    </div>
                </div>
            </div>


            <div id="reviewOption" class="flex justify-center w-full">
                <div class="flex flex-col items-center space-y-4">
                    <!-- Approve & Reject Buttons -->
                    <div class="flex space-x-4">
                        <asp:Button runat="server" Text="Approve" ID="btnSetuju" class="bg-green-400 hover:bg-green-500 text-white font-bold py-2 px-6 rounded-full text-lg"></asp:Button>
                        <button type="button" onclick="showReject()" class="bg-red-400 hover:bg-red-500 text-white font-bold py-2 px-6 rounded-full text-lg">
                            Reject
                        </button>
                        <asp:Button ID="btntidaksetuju" runat="server" Text="Submit Reject" CssClass="hidden" OnClientClick="btntidaksetuju_Click" />

                    </div>
                    <!-- Reject Form (Initially Hidden) -->
                    <div id="rejectForm" class="hidden flex flex-col items-center space-y-4 mt-4">
                        <textarea id="rejectNote" name="rejectNote" placeholder="Catatan penolakan wajib diisi" class="w-[300px] p-3 border rounded-lg text-sm" rows="4"></textarea>
                        <button type="button" onclick="submitReject()" class="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-6 rounded-full text-lg">
                            Submit Reject
                        </button>
                    </div>
                    <div class="text-center mt-4">
                        <asp:Label ID="lblStatus" runat="server" CssClass="text-blue-600 font-semibold"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <%--END MODAL--%>
    <section class="max-w-5xl mx-auto space-y-10">
        <div>
            <h2 class="text-[#f9a01b] font-extrabold text-2xl mb-6">REQUEST LIST
            </h2>
            <asp:GridView ID="gvRequestList" runat="server" DataKeyNames="kdklaim" CssClass="w-full text-center text-sm" AutoGenerateColumns="False" GridLines="None" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="font-semibold text-black" RowStyle-CssClass="bg-white rounded-full">
                <Columns>
                    <asp:BoundField DataField="kdklaim" HeaderText="No. Klaim" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="NIP" HeaderText="NIP" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="NamaLengkap" HeaderText="Nama" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="NamaDepartemen" HeaderText="Departemen" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="Kategori" HeaderText="Kategori" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="TanggalPengajuan" HeaderText="Tanggal" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="Biaya" HeaderText="Biaya" DataFormatString="{0:#,##0.00} IDR" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
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
            <h2 class="text-[#f9a01b] font-extrabold text-2xl mb-6">HISTORY
            </h2>
            <asp:GridView ID="gvHistory" runat="server" CssClass="w-full text-center text-sm" AutoGenerateColumns="False" GridLines="None" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="font-semibold text-black" RowStyle-CssClass="bg-white rounded-full" OnRowDataBound="gvHistory_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="kdklaim" HeaderText="No. Klaim" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="NIP" HeaderText="NIP" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="Namalengkap" HeaderText="Nama" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="NamaDepartemen" HeaderText="Departemen" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="Kategori" HeaderText="Kategori" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="Tanggalpengajuan" HeaderText="Tanggal" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
                    <asp:BoundField DataField="status_terakhir" HeaderText="Status" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
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
</asp:Content>
