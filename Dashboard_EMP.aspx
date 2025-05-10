<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Dashboard_EMP.aspx.vb" Inherits="MedicalReimbursement.Dashboard_EMP" %>

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
             <section aria-label="Summary sections" class="max-w-5xl mx-auto grid grid-cols-1 md:grid-cols-2 gap-8">
     <article>
      <h2 class="text-[#f7931e] font-semibold text-xl mb-4">
       Personal Summary
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
         Division
        </p>
        <asp:Label ID="DivisionLabel" runat="server" Text="Networking" CssClass="text-black text-sm"></asp:Label>
       </div>
       <div>
        <p class="text-[#145a52] font-semibold mb-1">
         Level
        </p>
        <asp:Label ID="LevelLabel" runat="server" Text="Junior" CssClass="text-black text-sm"></asp:Label>
       </div>
       <div>
        <p class="text-[#145a52] font-semibold mb-1">
         Status
        </p>
        <asp:Label ID="StatusLabel" runat="server" Text="PKWTT" CssClass="text-black text-sm"></asp:Label>
       </div>
      </div>
     </article>
     <article>
      <h2 class="text-[#f7931e] font-semibold text-xl mb-4">
       Medical Reimbursement Summary
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
            </main>
    </form>
</body>
</html>
