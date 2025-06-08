<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Employee_HR.aspx.vb" Inherits="MedicalReimbursement.Employee_HR" MasterPageFile="~/Header.master" %>

<asp:Content ID="headContent" runat="server" ContentPlaceHolderID="head">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitlePlaceHolder" runat="server">
    <span class="text-[#145445] font-extrabold text-2xl md:text-3xl ml-2">EMPLOYEES</span>
</asp:Content>

<asp:Content ID="mainContent" runat="server" ContentPlaceHolderID="SubContent">
    <asp:ScriptManager ID="scriptmanager1" runat="server" />
    <div class="max-w-5xl mx-auto flex justify-end mb-8">
        <asp:Button ID="btnAddEmployee" runat="server" Text="Add Employee" CssClass="bg-[#0052cc] text-white font-semibold rounded-lg py-3 px-6 flex items-center space-x-2 hover:cursor-pointer"/> <%--OnClick="btnAddEmployee_Click"--%> 
        <asp:Panel ID="pnlModal" runat="server" CssClass="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 hidden">
        <div class="bg-gray-100 bg-opacity-90 rounded-2xl p-10 max-w-6xl w-full grid grid-cols-1 sm:grid-cols-3 gap-x-12 gap-y-6 shadow-lg relative">
        <asp:Button ID="btnCloseModal" runat="server" Text="×" CssClass="absolute top-4 right-4 text-3xl font-bold text-gray-700 hover:text-gray-900 bg-transparent border-none cursor-pointer"  />

        <!-- Left Column -->
        <div class="space-y-4">
          <div>
            <asp:Label ID="lblFullName" runat="server" AssociatedControlID="txtFullName" Text="Full Name" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtFullName" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="Email" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs" TextMode="Email"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblPhone" runat="server" AssociatedControlID="txtPhone" Text="Phone Number" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblPlaceOfBirth" runat="server" AssociatedControlID="txtPlaceOfBirth" Text="Place of Birth" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtPlaceOfBirth" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblDateOfBirth" runat="server" AssociatedControlID="txtDateOfBirth" Text="Date of Birth" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblAddress" runat="server" AssociatedControlID="txtAddress" Text="Address" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="4" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs resize-none"></asp:TextBox>
          </div>
        </div>

        <!-- Middle Column -->
        <div class="space-y-4">
          <div>
            <asp:Label ID="lblReligion" runat="server" AssociatedControlID="txtReligion" Text="Religion" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtReligion" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblNationalID" runat="server" AssociatedControlID="txtNationalID" Text="National ID" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtNationalID" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblGender" runat="server" Text="Gender" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <div class="flex items-center space-x-4 text-xs">
              <asp:RadioButton ID="rbMale" runat="server" GroupName="Gender" Text="Male" CssClass="accent-[#1d8ed1]" />
              <asp:RadioButton ID="rbFemale" runat="server" GroupName="Gender" Text="Female" CssClass="accent-[#1d8ed1]" />
            </div>
          </div>
          <div>
            <asp:Label ID="lblMarriageStatus" runat="server" Text="Marriage Status" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <div class="flex items-center space-x-4 text-xs">
              <asp:RadioButton ID="rbMarried" runat="server" GroupName="MarriageStatus" Text="Married" CssClass="accent-[#1d8ed1]"/>
              <asp:RadioButton ID="rbSingle" runat="server" GroupName="MarriageStatus" Text="Single" CssClass="accent-[#1d8ed1]" />
            </div>
          </div>
          <div>
            <asp:Label ID="lblBloodType" runat="server" Text="Blood Type" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <div class="flex items-center space-x-4 text-xs">
              <asp:RadioButton ID="rbBloodA" runat="server" GroupName="BloodType" Text="A" CssClass="accent-[#1d8ed1]" />
              <asp:RadioButton ID="rbBloodAB" runat="server" GroupName="BloodType" Text="AB" CssClass="accent-[#1d8ed1]" />
              <asp:RadioButton ID="rbBloodB" runat="server" GroupName="BloodType" Text="B" CssClass="accent-[#1d8ed1]" />
              <asp:RadioButton ID="rbBloodO" runat="server" GroupName="BloodType" Text="O" CssClass="accent-[#1d8ed1]" />
            </div>
          </div>
        </div>


        <!-- Right Column -->
        <div class="space-y-4">
          <div>
            <asp:Label ID="lblNIP" runat="server" AssociatedControlID="txtNIP" Text="NIP" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtNIP" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblStatus" runat="server" AssociatedControlID="txtStatus" Text="Status" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtStatus" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblJoinDate" runat="server" AssociatedControlID="txtJoinDate" Text="Join Date" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtJoinDate" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblBranch" runat="server" AssociatedControlID="txtBranch" Text="Branch" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtBranch" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblPosition" runat="server" AssociatedControlID="txtPosition" Text="Position" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtPosition" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
          <div>
            <asp:Label ID="lblLevel" runat="server" AssociatedControlID="txtLevel" Text="Level" CssClass="block text-[10px] font-semibold text-black mb-1"></asp:Label>
            <asp:TextBox ID="txtLevel" runat="server" CssClass="w-full rounded-md border border-gray-300 px-3 py-2 text-xs"></asp:TextBox>
          </div>
        </div>
            <asp:Button ID="Button1" runat="server" Text="Button" />
      </div>
    </asp:Panel>

    </div>
    <section class="max-w-5xl mx-auto">
        <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" CssClass="w-full text-center text-sm font-semibold text-black" HeaderStyle-CssClass="pb-3" RowStyle-CssClass="bg-white rounded-full" GridLines="None">
             <Columns>
                  <asp:BoundField DataField="NIP" HeaderText="NIP" />
                  <asp:BoundField DataField="Nama" HeaderText="Nama" />
                  <asp:BoundField DataField="Divisi" HeaderText="Divisi" />
                  <asp:BoundField DataField="Departemen" HeaderText="Departemen" />
                  <asp:BoundField DataField="Level" HeaderText="Level" />
                  <asp:BoundField DataField="Status_pegawai" HeaderText="Status" />
                  <asp:TemplateField>
                       <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" CssClass="bg-[#0052cc] p-2 rounded-sm text-white"  CommandArgument='<%# Eval("NIP") %>'></asp:Button>
                            <span aria-hidden="true" class="absolute top-0 right-0 block h-3 w-3 rounded-full bg-red-600 border-2 border-white" style="position: relative; top: -1.25rem; left: 0.75rem;"></span>
                       </ItemTemplate>
                  </asp:TemplateField>
             </Columns>
        </asp:GridView>
    </section>
</asp:Content>
