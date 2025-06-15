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
                "searching": true,
                "info": true,
                "ordering": true,
                "dom": '<"top"f>rt<"bottom"p><"clear">'
            });
        });
    </script>
</asp:Content>

<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="SubContent">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />
    <!-- Main content -->
    <section aria-label="Summary sections" class="max-w-5xl mx-auto grid grid-cols-1 md:grid-cols-2 gap-8">
        <article>
            <h2 class="text-[#f7931e] font-semibold text-xl mb-4">Personal Summary
            </h2>
            <div class="bg-white rounded-xl p-6 grid grid-cols-1 sm:grid-cols-2 gap-y-6 gap-x-12 max-w-md">
                <div>
                    <p class="text-[#145a52] font-semibold mb-1">
                        Full Name
                    </p>
                    <asp:Label ID="FullNameLabel" runat="server" Text="Budiono Siregar" CssClass="text-black text-sm"></asp:Label>
                </div>
                <div>
                    <p class="text-[#145a52] font-semibold mb-1">
                        NIP
                    </p>
                    <asp:Label ID="NIPLabel" runat="server" Text="65132879551" CssClass="text-black text-sm"></asp:Label>
                </div>
                <div>
                    <p class="text-[#145a52] font-semibold mb-1">
                        Departement
                    </p>
                    <asp:Label ID="DepartementLabel" runat="server" Text="IT" CssClass="text-black text-sm"></asp:Label>
                </div>
                <div>
                    <p class="text-[#145a52] font-semibold mb-1">
                        Level
                    </p>
                    <asp:Label ID="JabatanLabel" runat="server" Text="Junior" CssClass="text-black text-sm"></asp:Label>
                </div>
                <div>
                    <p class="text-[#145a52] font-semibold mb-1">
                        Status
                    </p>
                    <asp:Label ID="StatusLabel" runat="server" Text="PKWTT" CssClass="text-black text-sm"></asp:Label>
                </div>
                <div>
                    <p class="text-[#145a52] font-semibold mb-1">
                        No. HP
                    </p>
                    <asp:Label ID="selulerlabel" runat="server" Text="PKWTT" CssClass="text-black text-sm"></asp:Label>
                </div>
            </div>
        </article>
        <article>
            <h2 class="text-[#f7931e] font-semibold text-xl mb-4">Medical Reimbursement Summary
            </h2>
            <asp:GridView ID="MedicalReimbursementGrid" runat="server" CssClass="bg-white rounded-xl w-full border-collapse border border-black/60" AutoGenerateColumns="False" GridLines="None" HeaderStyle-CssClass="border-b border-black/60" RowStyle-CssClass="border border-black/60 rounded-lg bg-[#eaeaea] mt-3" AlternatingRowStyle-CssClass="border border-black/60 rounded-lg bg-[#eaeaea] mt-3">
                <Columns>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="text-left font-semibold px-4 py-3" ItemStyle-CssClass="px-4 py-3 rounded-l-lg">
                        <ItemTemplate>
                            <asp:Label ID="StatusText" runat="server" Text='<%# Eval("Status") %>' CssClass='<%# Eval("StatusCss") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Category" HeaderText="Category" HeaderStyle-CssClass="text-left font-semibold px-4 py-3" ItemStyle-CssClass="px-4 py-3" />
                    <asp:BoundField DataField="RequestDate" HeaderText="Request Date" HeaderStyle-CssClass="text-left font-semibold px-4 py-3" ItemStyle-CssClass="px-4 py-3" />
                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="px-4 py-3 relative">
                        <ItemTemplate>
                            <i class="fas fa-edit text-blue-600 text-lg cursor-pointer" aria-hidden="true"></i>
                            <span class="absolute top-2 right-2 w-2.5 h-2.5 bg-red-600 rounded-full"></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </article>
    </section>
</asp:Content>