<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="encrip_rijndael.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <p>
        <br />
    </p>
    <p>
        Prueba de encriptación con Rijndael</p>
    <form id="form1" runat="server">
        <p>
            Contraseña:
            <asp:TextBox ID="txtContrasena" runat="server"></asp:TextBox>
        </p>
        <p>
            La contraseña encriptada es:
            <asp:TextBox ID="txtEncriptada" runat="server" Enabled="False"></asp:TextBox>
        </p>
        <p>
            La contraseña desencriptada es:
            <asp:TextBox ID="txtDesencriptada" runat="server" Enabled="False"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnencriptar" runat="server" OnClick="btnencriptar_Click" Text="Encriptar con Rijndael" />
        </p>
        <p>
            <asp:Button ID="txtEncriptaciónSha" runat="server" OnClick="txtEncriptaciónSha_Click" Text="Encriptación con SHA1" />
        </p>
        <p>
            <asp:Label ID="lblValorPasswordSha" runat="server" Text="Aun no hace la prueba"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblVerificacionPassword" runat="server" Text="Aun no verifica la contraseña"></asp:Label>
        </p>
        <p>
            &nbsp;</p>
        <div>
        </div>
    </form>
</body>
</html>
