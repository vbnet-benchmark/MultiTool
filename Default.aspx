<%@ Page Title="Portfolio" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteLandingPage.Master" CodeBehind="Default.aspx.vb" Inherits="MultiTool._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    My Portfolio
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Hero Section -->
    <section class="text-center py-5 bg-light">
        <div class="container">
            <h1 class="display-4">Hi, I'm Stephan Rieber</h1>
            <p class="lead">Full-Stack ASP.NET Developer, Power BI, SSRS, SSIS, MSSQL DBA, Creator of Tools and Web Apps. </p>
        </div>
    </section>

    <!-- About Section -->
    <section class="py-5">
        <div class="container">
            <h2>About Me</h2>
            <p>
                I build web applications and small SaaS tools, focusing on .NET, ASP.NET, and practical productivity apps.
            </p>
        </div>
    </section>

    <!-- Skills & Resume Section -->
   <!-- Skills & Resume Section -->
<section class="py-5 bg-light">
    <div class="container">
        <h2>Skills & Expertise</h2>

        <!-- Databases -->
        <h5>Databases</h5>
        <span class="badge bg-primary me-1 mb-1">Microsoft SQL Server</span>
        <span class="badge bg-primary me-1 mb-1">T-SQL</span>
        <span class="badge bg-primary me-1 mb-1">SQL Database Administration</span>
        <span class="badge bg-primary me-1 mb-1">SQL DBA</span>
        <span class="badge bg-primary me-1 mb-1">SSRS</span>
        <span class="badge bg-primary me-1 mb-1">SSIS</span>

        <!-- Development -->
        <h5 class="mt-3">Development</h5>
        <span class="badge bg-info me-1 mb-1">VB.NET</span>
        <span class="badge bg-info me-1 mb-1">ASP.NET Web API</span>
        <span class="badge bg-info me-1 mb-1">ASPX</span>
        <span class="badge bg-info me-1 mb-1">Full-Stack Development</span>
        <span class="badge bg-info me-1 mb-1">.NET Framework</span>
        <span class="badge bg-info me-1 mb-1">Console Applications</span>

        <!-- Front-End -->
        <h5 class="mt-3">Front-End & UI</h5>
        <span class="badge bg-warning text-dark me-1 mb-1">HTML</span>
        <span class="badge bg-warning text-dark me-1 mb-1">CSS</span>
        <span class="badge bg-warning text-dark me-1 mb-1">JavaScript</span>
        <span class="badge bg-warning text-dark me-1 mb-1">DevExpress Controls</span>

        <!-- Data & Analysis -->
        <h5 class="mt-3">Data & Reporting</h5>
        <span class="badge bg-success me-1 mb-1">Power BI</span>
        <span class="badge bg-success me-1 mb-1">Data Analysis</span>
        <span class="badge bg-success me-1 mb-1">Query Optimization</span>
        <span class="badge bg-success me-1 mb-1">Dashboards</span>
        <span class="badge bg-success me-1 mb-1">Reporting Automation</span>

        <!-- Tools -->
        <h5 class="mt-3">Tools</h5>
        <span class="badge bg-secondary me-1 mb-1">SSMS</span>
        <span class="badge bg-secondary me-1 mb-1">Visual Studio</span>
        <span class="badge bg-secondary me-1 mb-1">IIS</span>
        <span class="badge bg-secondary me-1 mb-1">Office 365</span>

        <!-- Soft Skills -->
        <h5 class="mt-3">Soft Skills</h5>
        <span class="badge bg-dark me-1 mb-1">Leadership</span>
        <span class="badge bg-dark me-1 mb-1">Technical Documentation</span>
        <span class="badge bg-dark me-1 mb-1">Project Management</span>
        <span class="badge bg-dark me-1 mb-1">Mentoring</span>
        <span class="badge bg-dark me-1 mb-1">Application Testing</span>
        <span class="badge bg-dark me-1 mb-1">Data Entry</span>

        <!-- Optional Resume Download -->
        <div class="mt-3">
            <a href="/files/StephanRieber_Resume.pdf" class="btn btn-outline-primary">Download Resume</a>
        </div>
    </div>
</section>


    <!-- Projects / Work Section -->
    <section class="py-5">
        <div class="container">
            <h2>My Work</h2>
            <div class="row">
                <!-- Example Project Card -->
                <div class="col-md-4 mb-4">
                    <div class="card h-100 shadow-sm project-card">
                        <div class="card-body">
                            <h5 class="card-title">Calculator Suite</h5>
                            <p class="card-text">A web app featuring multiple calculators for health, finance, productivity, etc.</p>
                            <a href="../Applications/CalculatorSuite.aspx" class="btn btn-primary">View</a>
                        </div>
                    </div>
                </div>

                <!-- Placeholder for future projects -->
                <div class="col-md-4 mb-4">
                    <div class="card h-100 shadow-sm project-card">
                        <div class="card-body">
                            <h5 class="card-title">Resume & Portfolio Generator</h5>
                            <p class="card-text">Generator Resumes & Portfolios on the fly. We do not store your information for Privacy</p>
                            <a href="../Applications/ResumePortfolioGenerator.aspx" class="btn btn-primary">View</a>
                        </div>
                    </div>
                </div>

                <div class="col-md-4 mb-4">
                    <div class="card h-100 shadow-sm project-card">
                        <div class="card-body">
                            <h5 class="card-title">Project 3</h5>
                            <p class="card-text">Description of another project you want to showcase.</p>
                            <a href="#" class="btn btn-primary">View</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Contact Section -->
    <section class="py-5 bg-light">
        <div class="container">
            <h2>Contact Me</h2>
            <p>Interested in collaborating or hiring? Get in touch!</p>
            <asp:Panel ID="pnlContact" runat="server">
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control mb-2" Placeholder="Your Name"></asp:TextBox>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-2" Placeholder="Your Email"></asp:TextBox>
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control mb-2" Rows="4" Placeholder="Your Message"></asp:TextBox>
                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="btnSend_Click" />
            </asp:Panel>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-2"></asp:Label>
        </div>
    </section>

</asp:Content>
