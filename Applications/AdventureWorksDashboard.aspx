<%@ Page Title="AdventureWorks Dashboard" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AdventureWorksDashboard.aspx.vb" Inherits="MultiTool.AdventureWorksDashboard" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdventureWorks Dashboard
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <h2 class="mb-4 text-primary">Select Report Platform - AdventureWorks2022 Database</h2>

        <!-- Dropdown to select report type -->
        <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="True" CssClass="form-select" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
            <asp:ListItem Text="-- Select Report Type --" Value=""></asp:ListItem>
            <asp:ListItem Text="SSRS" Value="SSRS"></asp:ListItem>
            <asp:ListItem Text="Power BI" Value="PowerBI"></asp:ListItem>
        </asp:DropDownList>

        <hr class="my-4" />

        <!-- Placeholder for report -->
        <asp:Panel ID="pnlReportContainer" runat="server" Visible="False">
            <asp:Label ID="lblReportMessage" runat="server" Text="" CssClass="text-muted mb-3"></asp:Label>
            <!-- Dropdown to select specific report -->
        <asp:DropDownList ID="ddlReportList" runat="server" AutoPostBack="True" CssClass="form-select mb-4" OnSelectedIndexChanged="ddlReportList_SelectedIndexChanged">
</asp:DropDownList>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="600px" ProcessingMode="Local">
</rsweb:ReportViewer>


        </asp:Panel>
    </div>
</asp:Content>
