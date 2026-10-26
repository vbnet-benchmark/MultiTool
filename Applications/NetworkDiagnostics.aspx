<%@ Page Title="Network Diagnostics" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="NetworkDiagnostics.aspx.vb" Inherits="MultiTool.NetworkDiagnostics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Network Diagnostics
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <h2 class="mb-4 text-primary">Network Diagnostics</h2>
        <p class="lead">Test raw TCP connectivity to backend services this installation integrates with (legacy order-processing systems, print/label servers, licensing servers, partner relays).</p>

        <!-- ================== Quick Connectivity Test ================== -->
        <hr class="my-4" />
        <h4>Quick Connectivity Test</h4>
        <div class="row g-2 align-items-end mb-2">
            <div class="col-md-4">
                <label class="form-label">Host</label>
                <asp:TextBox ID="txtDirectHost" runat="server" CssClass="form-control" placeholder="e.g. legacy-orders.example.com" />
            </div>
            <div class="col-md-2">
                <label class="form-label">Port</label>
                <asp:TextBox ID="txtDirectPort" runat="server" CssClass="form-control" placeholder="9100" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnDirectTest" runat="server" Text="Test Connection" CssClass="btn btn-primary" OnClick="btnDirectTest_Click" />
            </div>
        </div>
        <asp:Label ID="lblDirectResult" runat="server" CssClass="text-muted" />

        <h6 class="mt-4">Verified Partner Connectivity Test</h6>
        <div class="row g-2 align-items-end mb-2">
            <div class="col-md-4">
                <label class="form-label">Host</label>
                <asp:TextBox ID="txtDirectSafeHost" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <label class="form-label">Port</label>
                <asp:TextBox ID="txtDirectSafePort" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnDirectSafeTest" runat="server" Text="Test Approved Connection" CssClass="btn btn-outline-primary" OnClick="btnDirectSafeTest_Click" />
            </div>
        </div>
        <asp:Label ID="lblDirectSafeResult" runat="server" CssClass="text-muted" />

        <!-- ================== Service Health Ping ================== -->
        <hr class="my-4" />
        <h4>Service Health Ping</h4>
        <div class="row g-2 align-items-end mb-2">
            <div class="col-md-4">
                <label class="form-label">Host</label>
                <asp:TextBox ID="txtPingHost" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <label class="form-label">Port</label>
                <asp:TextBox ID="txtPingPort" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnPing" runat="server" Text="Ping Service" CssClass="btn btn-primary" OnClick="btnPing_Click" />
            </div>
        </div>
        <asp:Label ID="lblPingResult" runat="server" CssClass="text-muted" />

        <h6 class="mt-4">Verified Service Ping</h6>
        <div class="row g-2 align-items-end mb-2">
            <div class="col-md-4">
                <label class="form-label">Host</label>
                <asp:TextBox ID="txtPingSafeHost" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <label class="form-label">Port</label>
                <asp:TextBox ID="txtPingSafePort" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnPingSafe" runat="server" Text="Ping Approved Service" CssClass="btn btn-outline-primary" OnClick="btnPingSafe_Click" />
            </div>
        </div>
        <asp:Label ID="lblPingSafeResult" runat="server" CssClass="text-muted" />

        <!-- ================== Register External Relay Partner ================== -->
        <hr class="my-4" />
        <h4>Register External Relay Partner</h4>
        <div class="row g-2 align-items-end mb-2">
            <div class="col-md-4">
                <label class="form-label">Relay Host</label>
                <asp:TextBox ID="txtRelayHost" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <label class="form-label">Relay Port</label>
                <asp:TextBox ID="txtRelayPort" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnRegisterRelay" runat="server" Text="Register Relay Partner" CssClass="btn btn-primary" OnClick="btnRegisterRelay_Click" />
            </div>
        </div>
        <asp:Label ID="lblRelayResult" runat="server" CssClass="text-muted" />

        <h6 class="mt-4">Register Approved Relay Partner</h6>
        <div class="row g-2 align-items-end mb-2">
            <div class="col-md-4">
                <label class="form-label">Relay Host</label>
                <asp:TextBox ID="txtRelaySafeHost" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <label class="form-label">Relay Port</label>
                <asp:TextBox ID="txtRelaySafePort" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnRegisterRelaySafe" runat="server" Text="Register Approved Partner" CssClass="btn btn-outline-primary" OnClick="btnRegisterRelaySafe_Click" />
            </div>
        </div>
        <asp:Label ID="lblRelaySafeResult" runat="server" CssClass="text-muted" />

        <!-- ================== Connector Profile Test ================== -->
        <hr class="my-4" />
        <h4>Connector Profile Test</h4>
        <p class="text-muted">Tests connectivity using this deployment's currently configured connector profile.</p>
        <div class="row g-2 align-items-end mb-2">
            <div class="col-md-4">
                <label class="form-label">Host</label>
                <asp:TextBox ID="txtConnectorHost" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <label class="form-label">Port</label>
                <asp:TextBox ID="txtConnectorPort" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnConnectorTest" runat="server" Text="Test Connector Profile" CssClass="btn btn-primary" OnClick="btnConnectorTest_Click" />
            </div>
        </div>
        <asp:Label ID="lblConnectorResult" runat="server" CssClass="text-muted" />

        <!-- ================== Batch Relay Connectivity Test ================== -->
        <hr class="my-4" />
        <h4>Batch Relay Connectivity Test</h4>
        <p class="text-muted">Enter one <code>host:port</code> pair per line to test multiple branch-office relay servers at once.</p>
        <div class="row g-2 mb-2">
            <div class="col-md-6">
                <asp:TextBox ID="txtBatchTargets" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="relay-branch1.example.com:9100&#10;relay-branch2.example.com:9100" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnBatchTest" runat="server" Text="Test All" CssClass="btn btn-primary" OnClick="btnBatchTest_Click" />
            </div>
        </div>
        <asp:Label ID="lblBatchResult" runat="server" CssClass="text-muted" />

        <h6 class="mt-4">Batch Verified Relay Test</h6>
        <div class="row g-2 mb-2">
            <div class="col-md-6">
                <asp:TextBox ID="txtBatchSafeTargets" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnBatchSafeTest" runat="server" Text="Test All Approved" CssClass="btn btn-outline-primary" OnClick="btnBatchSafeTest_Click" />
            </div>
        </div>
        <asp:Label ID="lblBatchSafeResult" runat="server" CssClass="text-muted" />
    </div>
</asp:Content>
