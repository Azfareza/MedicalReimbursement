<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Employee_HR.aspx.vb" Inherits="MedicalReimbursement.Employee_HR" MasterPageFile="~/Header.master" %>

<asp:Content ID="headContent" runat="server" ContentPlaceHolderID="head">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/2.3.2/js/dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/2.3.2/css/dataTables.dataTables.min.css">
    <script>
        function initializeDataTable() {
            // Destroy existing DataTable instance if it exists
            if ($.fn.DataTable.isDataTable('#tabelPegawai')) {
                $('#tabelPegawai').DataTable().destroy();
            }
            $('#tabelPegawai').DataTable({
                "paging": true,
                "pageLength": 12,
                "searching": true,
                "info": true,
                "ordering": true,
                "dom": '<"top"f>rt<"bottom"p><"clear">'
            });
        }

        $(document).ready(function () {
            initializeDataTable(); // Initialize on first page load

            // Re-initialize DataTable after every ASP.NET AJAX partial postback
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
                initializeDataTable();
                // Check if the modal panel should be shown based on ViewState
                var pnlModal = $('#<%= pnlModal.ClientID %>');
                var hdnShowModal = $('#<%= hdnShowModal.ClientID %>');

                if (hdnShowModal.val() === 'true') {
                    pnlModal.removeClass('hidden');
                    hdnShowModal.val('false'); // Reset the hidden field after showing
                } else {
                    pnlModal.addClass('hidden');
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    <span class="text-[#145445] font-extrabold text-2xl md:text-3xl ml-2">EMPLOYEES</span>
</asp:Content>

<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="SubContent">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />

    <section class="max-w-5xl mx-auto">
        <div class="flex justify-between items-center mb-4 px-1">
            <h2 class="text-[#f97316] font-semibold text-lg">Daftar Pegawai</h2>
            <asp:Button ID="btnTambahPegawai" runat="server"
                Text="➕ Tambah Pegawai"
                CssClass="bg-[#16a34a] hover:bg-[#15803d] text-white font-semibold px-4 py-2 rounded-md shadow-sm transition"
                OnClick="btnTambahPegawai_Click"
             />
        </div>

        <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlModal" runat="server" CssClass="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center hidden z-50">
                    <div class="bg-white p-6 rounded-xl w-full max-w-3xl shadow-2xl relative max-h-[90vh] overflow-y-auto scrollbar-thin scrollbar-thumb-gray-400 scrollbar-track-gray-100">
                        <h3 class="text-xl font-bold text-[#145445] mb-6 border-b pb-2">📋 Detail Informasi Pegawai</h3>

                        <div class="grid grid-cols-3 gap-4 text-sm">
                            <div>
                                <span class="font-semibold text-gray-700">Nama Lengkap:</span>
                                <asp:Label ID="lblNamaLengkap" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Tempat Lahir:</span>
                                <asp:Label ID="lblTempatLahir" runat="server" CssClass="block mb-2 text-gray-800" />
                                <span class="font-semibold text-gray-700">Tanggal Lahir:</span>
                                <asp:Label ID="lblTanggalLahir" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Jenis Kelamin:</span>
                                <asp:Label ID="lblJenisKelamin" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Kebangsaan:</span>
                                <asp:Label ID="lblKebangsaan" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Agama:</span>
                                <asp:Label ID="lblAgama" runat="server" CssClass="block mb-2 text-gray-800" />
                            </div>

                            <div>
                                <span class="font-semibold text-gray-700">NIK:</span>
                                <asp:Label ID="lblNIK" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">NIP:</span>
                                <asp:Label ID="lblNIP" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">NPWP:</span>
                                <asp:Label ID="lblNPWP" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Seluler:</span>
                                <asp:Label ID="lblSeluler" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Email:</span>
                                <asp:Label ID="lblEmail" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Departemen:</span>
                                <asp:Label ID="lblDepartemen" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Jabatan:</span>
                                <asp:Label ID="lblJabatan" runat="server" CssClass="block mb-2 text-gray-800" />
                            </div>
                            <div>
                                <span class="font-semibold text-gray-700">Alamat:</span>
                                <asp:Label ID="lblAlamat" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Kecamatan:</span>
                                <asp:Label ID="lblKecamatan" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Kelurahan:</span>
                                <asp:Label ID="lblKelurahan" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Kota:</span>
                                <asp:Label ID="lblKota" runat="server" CssClass="block mb-2 text-gray-800" />

                                <span class="font-semibold text-gray-700">Provinsi:</span>
                                <asp:Label ID="lblProvinsi" runat="server" CssClass="block mb-2 text-gray-800" />
                            </div>
                        </div>

                        <h3 class="text-xl font-bold text-[#145445] mt-6 mb-6 border-b pb-2">👨‍👩‍👧‍👦 Tanggungan Pegawai</h3>
                        <asp:Repeater ID="rptTanggungan" runat="server">
                            <HeaderTemplate>
                                <table class="table-auto w-full text-sm border mb-4">
                                    <thead class="bg-gray-100">
                                        <tr>
                                            <th class="border px-2 py-1">Nama</th>
                                            <th class="border px-2 py-1">Tempat Lahir</th>
                                            <th class="border px-2 py-1">Tanggal Lahir</th>
                                            <th class="border px-2 py-1">Jenis Kelamin</th>
                                            <th class="border px-2 py-1">Pekerjaan</th>
                                            <th class="border px-2 py-1">Hubungan</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="border px-2 py-1"><%# Eval("NamaLengkap") %></td>
                                    <td class="border px-2 py-1"><%# Eval("TempatLahir") %></td>
                                    <td class="border px-2 py-1"><%# Eval("TanggalLahir", "{0:yyyy-MM-dd}") %></td>
                                    <td class="border px-2 py-1"><%# Eval("JenisKelamin") %></td>
                                    <td class="border px-2 py-1"><%# Eval("Pekerjaan") %></td>
                                    <td class="border px-2 py-1"><%# Eval("Hubungan") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                    </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>

                        <div class="mt-6 text-right">
                            <asp:Button ID="btnCloseModal" runat="server" Text="Tutup" CssClass="bg-red-600 hover:bg-red-700 text-white font-semibold px-5 py-2 rounded shadow-sm transition" OnClick="btnCloseModal_Click" />
                        </div>

                        <asp:HiddenField ID="hdnShowModal" runat="server" Value="false" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="upPegawaiTable" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table id="tabelPegawai" class="w-full bg-white rounded-lg shadow-sm border border-gray-400 text-sm display">
                    <thead class="text-left font-semibold border-b border-gray-400">
                        <tr>
                            <th class="p-3">NIP</th>
                            <th class="p-3">Nama Lengkap</th>
                            <th class="p-3">Departemen</th>
                            <th class="p-3">Jabatan</th>
                            <th class="p-3">Status</th>
                            <th class="p-3">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptPegawai" runat="server" OnItemCommand="rptPegawai_ItemCommand">
                            <ItemTemplate>
                                <tr class="border border-gray-400 rounded-md mt-2">
                                    <td class="p-3"><%# Eval("NIP") %></td>
                                    <td class="p-3"><%# Eval("NamaLengkap") %></td>
                                    <td class="p-3"><%# Eval("NamaDepartemen") %></td>
                                    <td class="p-3"><%# Eval("NamaJabatan") %></td>
                                    <td class="p-3"><%# Eval("Status") %></td>
                                    <td class="p-3">
                                        <asp:Button Text="Detail" ID="btnEdit" runat="server" CssClass="bg-[#0052cc] p-2 rounded-sm text-white" CommandArgument='<%# Eval("NIP") %>' CommandName="View" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="upModalAdd" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlModalAdd" runat="server" CssClass="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center hidden z-50">
                    <div class="bg-white p-6 rounded-xl w-full max-w-7xl shadow-2xl relative max-h-[90vh] overflow-y-auto scrollbar-thin scrollbar-thumb-gray-400 scrollbar-track-gray-100">
                        <h3 class="text-xl font-bold text-[#145445] mb-4 border-b pb-2">➕ Tambah Pegawai Baru</h3>

                        <div class="grid grid-cols-3 gap-4 text-sm">
                            <div>
                                <h4 class="text-xl font-bold text-[#145445] mb-4 border-b pb-2">Data Diri</h4>
                                <label class="block font-semibold text-gray-700">NIP</label>
                                <asp:TextBox ID="txtNIP" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2" />

                                <label class="block font-semibold text-gray-700">NIK</label>
                                <asp:TextBox ID="txtNIK" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2" />
                                
                                <label class="block font-semibold text-gray-700">NPWP</label>
                                <asp:TextBox ID="txtNPWP" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2" />

                                <label class="block font-semibold text-gray-700">Nama Lengkap</label>
                                <asp:TextBox ID="txtNamaLengkap" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2" />

                                <label class="block font-semibold text-gray-700">Tempat Lahir</label>
                                <asp:TextBox ID="txtTempatLahir" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2" />

                                <label class="block font-semibold text-gray-700">Tanggal Lahir</label>
                                <asp:TextBox ID="txtTanggalLahir" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2" TextMode="Date" />
                                
                                <label class="block font-semibold text-gray-700">Jenis Kelamin</label>
                                <asp:DropDownList ID="ddlJenisKelamin" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2">
                                    <asp:ListItem Text="Laki-Laki" Value="Laki-Laki" />
                                    <asp:ListItem Text="Perempuan" Value="Perempuan" />
                                </asp:DropDownList>

                                <label class="block font-semibold text-gray-700">Kebangsaan</label>
                                <asp:TextBox ID="txtKebangsaan" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>
                                
                                <label class="block font-semibold text-gray-700">Agama</label>
                                <asp:TextBox ID="txtAgama" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>
                            </div>
                            <div>

                                <h4 class="text-xl font-bold text-[#145445] mb-4 border-b pb-2">Alamat</h4>
                                <label class="block font-semibold text-gray-700">Provinsi</label>
                                <asp:TextBox ID="txtProvinsi" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <label class="block font-semibold text-gray-700">Kota</label>
                                <asp:TextBox ID="txtKota" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <label class="block font-semibold text-gray-700">Kecamatan</label>
                                <asp:TextBox ID="txtKecamatan" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <label class="block font-semibold text-gray-700">Kelurahan</label>
                                <asp:TextBox ID="txtKelurahan" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <label class="block font-semibold text-gray-700">Detail Alamat</label>
                                <asp:TextBox ID="txtDetilAlamat" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <label class="block font-semibold text-gray-700">Kode Pos</label>
                                <asp:TextBox ID="txtKodePos" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <%-- Aktif = 1 --%>

                                <h4 class="text-xl font-bold text-[#145445] mb-4 mt-4 border-b pb-2">Kontak</h4>
                                <label class="block font-semibold text-gray-700">E-Mail</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <label class="block font-semibold text-gray-700">Seluler</label>
                                <asp:TextBox ID="txtSeluler" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>
                            </div>

                            <div>
                                <h4 class="text-xl font-bold text-[#145445] mb-4 border-b pb-2">Data Kepegawaian</h4>
                                <label class="block font-semibold text-gray-700">Kode Departemen</label>
                                <asp:TextBox ID="txtKdDept" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <label class="block font-semibold text-gray-700">Kode Jabatan</label>
                                <asp:TextBox ID="txtKdJabatan" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>
                                
                                <label class="block font-semibold text-gray-700">Tanggal Mulai</label>
                                <asp:TextBox ID="txtTanggalMulai" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2" TextMode="Date" />
                                
                                <label class="block font-semibold text-gray-700">Status</label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2">
                                    <asp:ListItem Text="PKWT" Value="PKWT" />
                                    <asp:ListItem Text="PKWTT" Value="PKWTT" />
                                </asp:DropDownList>

                                <label class="block font-semibold text-gray-700">Catatan</label>
                                <asp:TextBox ID="txtCatatan" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <%-- Foto = Null --%>

                                <label class="block font-semibold text-gray-700">Password</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="w-full border px-3 py-2 rounded-md mb-2"/>

                                <%-- Role = 4 --%>
                            </div>

                        </div>

                        <div class="mt-4 text-right">
                            <asp:Button ID="btnSimpanPegawai" runat="server" Text="Simpan"
                                CssClass="bg-green-600 hover:bg-green-700 text-white font-semibold px-5 py-2 rounded shadow-sm transition" />

                            <asp:Button ID="btnBatalTambah" runat="server" Text="Batal"
                                CssClass="ml-2 bg-gray-400 hover:bg-gray-500 text-white font-semibold px-5 py-2 rounded shadow-sm transition"
                                OnClick="btnBatalTambah_Click" />
                        </div>

                        <asp:HiddenField ID="hdnShowAddModal" runat="server" Value="false" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>