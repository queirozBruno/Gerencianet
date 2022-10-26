<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Gerencianet._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <div class="form-group">
        <input type="text" id="txtCustomerName" runat="server" placeholder="Nome do Cliente" class="form-control" /><br />
        <input type="text" id="txtProductName" runat="server" placeholder="Nome do Produto" class="form-control" />
    </div>

    <br />
    <asp:Button ID="btnBuy" runat="server" OnClick="btnBuy_Click" CssClass="btn btn-success" Text="Comprar" />
    <a id="lnkBillet" runat="server" href="#" target="_blank" visible="false" class="btn btn-primary">Abrir Boleto</a>

</asp:Content>
