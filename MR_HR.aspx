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
                <asp:Button ID="btnDashboard" runat="server" Text="Dashboard" CssClass="bg-[#145445] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:cursor-pointer" />
                <asp:Button ID="btnMedicalReimbursement" runat="server" Text="Medical Reimbursement" CssClass="bg-[#FFAA0A] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:cursor-pointer" />
                <asp:Button ID="btnEmployees" runat="server" Text="Employees" CssClass="bg-[#145445] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:cursor-pointer" />
                <asp:Button ID="btnLogOut" runat="server" Text="Log Out" CssClass="border border-[#FF6B6B] text-white text-xs font-semibold rounded px-4 py-2 text-left hover:cursor-pointer" />
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
             <section class="max-w-5xl mx-auto space-y-10">
     <div>
      <h2 class="text-[#f9a01b] font-extrabold text-2xl mb-6">
       REQUEST LIST
      </h2>
      <asp:GridView ID="gvRequestList" runat="server" CssClass="w-full text-center text-sm" AutoGenerateColumns="False" GridLines="None" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="font-semibold text-black" RowStyle-CssClass="bg-white rounded-full" >
       <Columns>
        <asp:BoundField DataField="NIP" HeaderText="NIP" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:BoundField DataField="Departement" HeaderText="Departement" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:TemplateField HeaderText="" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3">
         <ItemTemplate>
          <asp:Button ID="btnEditRequest" runat="server" CssClass="relative text-white bg-blue-600 p-2 rounded">
          <%-- <i class="fas fa-edit"></i>
           <span class="absolute top-0 right-0 block h-2 w-2 rounded-full ring-2 ring-white bg-red-600"></span>--%>
          </asp:Button>
         </ItemTemplate>
        </asp:TemplateField>
       </Columns>
      </asp:GridView>
     </div>
     <div>
      <h2 class="text-[#f9a01b] font-extrabold text-2xl mb-6">
       HISTORY
      </h2>
      <asp:GridView ID="gvHistory" runat="server" CssClass="w-full text-center text-sm" AutoGenerateColumns="False" GridLines="None" HeaderStyle-Font-Bold="True" HeaderStyle-CssClass="font-semibold text-black" RowStyle-CssClass="bg-white rounded-full" >
       <Columns>
        <asp:BoundField DataField="NIP" HeaderText="NIP" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:BoundField DataField="Departement" HeaderText="Departement" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:BoundField DataField="Category" HeaderText="Category" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3" />
        <asp:TemplateField HeaderText="" ItemStyle-CssClass="py-4 px-6" HeaderStyle-CssClass="pb-3">
         <ItemTemplate>
          <asp:Button ID="btnEditHistory" runat="server" CssClass="relative text-white bg-blue-600 p-2 rounded">
           <%--<i class="fas fa-edit"></i>
           <span class="absolute top-0 right-0 block h-2 w-2 rounded-full ring-2 ring-white bg-red-600"></span>--%>
          </asp:Button>
         </ItemTemplate>
        </asp:TemplateField>
       </Columns>
      </asp:GridView>
     </div>
    </section>
        </main>
    </form>
</body>
</html>
