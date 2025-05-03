<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Master_Request.aspx.vb" Inherits="MedicalReimbursement.Master_Request" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="Nama" runat="server"></asp:TextBox>
                </div>
        <div>
        <asp:TextBox ID="Tanggal" runat="server"></asp:TextBox>
   </div>  
        <div>
        <asp:DropDownList runat="server" ID="Kategori">
            <asp:ListItem  text="" value=""/> 
            <asp:ListItem  text="Rawat Jalan" value="Rawat Jalan"/> 
            <asp:ListItem  text="Kacamata" value="kacamata"/> 
            <asp:ListItem  text="Persalinan" value="Persalinan"/> 
        </asp:DropDownList>
   </div>
        <div>
            <asp:TextBox ID="Status" runat="server"></asp:TextBox>
            </div>
        <div>
            
            <asp:Button ID="Button1" runat="server" Text="simpan" />
            
        </div>
    </form>
</body>
</html>
