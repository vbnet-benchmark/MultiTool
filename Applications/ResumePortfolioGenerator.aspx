<%@ Page Title="Resume & Portfolio Generator" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="ResumePortfolioGenerator.aspx.vb" Inherits="MultiTool.ResumePortfolioGenerator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Resume & Portfolio Generator

   

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <section class="py-10">

    <div class="container-fluid">
        
            
        <h2 class="mb-4">Resume & Portfolio Generator</h2>
        <p class="lead">Fill out your details below and see a live preview. <strong>No information is stored.</strong></p>
        <div class="row">
                                <!-- buttons -->
                  <div class="d-flex justify-content-between align-items-center w-100 flex-wrap">
        <!-- Left side buttons -->
        <div class="d-flex gap-2 flex-wrap">
            <asp:Button ID="btnGenerateTestData" runat="server" Text="Generate Test Data" CssClass="btn btn-primary btn-lg"
                OnClick="btnGenerateTestData_Click" OnClientClick="return confirm('Are you sure you want to generate test data?');" />
            <asp:Button ID="btnClearForm" runat="server" Text="Clear Form" CssClass="btn btn-danger btn-lg"
                OnClick="btnClearForm_Click" OnClientClick="return confirm('Are you sure you want to clear the form?');" />
        </div>

        <!-- Right side buttons -->
        <div class="d-flex gap-2 flex-wrap">
            <asp:Button ID="btnGeneratePDF" runat="server" Text="Generate PDF" CssClass="btn btn-warning btn-lg" OnClick="btnGeneratePDF_Click"
                OnClientClick="return confirm('Save this document?');"
                />
            <asp:Button ID="btnGenerateDocx" runat="server" Text="Generate DOCX" CssClass="btn btn-success btn-lg" OnClick="btnGenerateDocx_Click" 
                OnClientClick="return confirm('Save this document?');"
                />
            <asp:Button ID="btnGenerateText" runat="server" Text="Generate TXT" CssClass="btn btn-info btn-lg" OnClick="btnGenerateText_Click" 
                OnClientClick="return confirm('Save this document?');"
                />
        </div>
    </div>

            <br /><br /><br /><br />
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-2"></asp:Label>
        </div>
        <div class="row">
            <!-- Form -->
            <div class="col-md-6">
                <asp:Panel ID="pnlForm" runat="server">
                    <!-- Personal Info -->
                    <div class="mb-3"><h5>Personal Information</h5></div>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <h6>Full Name</h6>
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" Placeholder="Full Name"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <h6>Email</h6>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <h6>Phone #</h6>
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" Placeholder="Phone Number"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <h6>Location</h6>
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" Placeholder="Location"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Experience, Education, Skills, Projects, Additional -->
                    <div class="mb-3"><h5>Work Experience</h5>
                        <asp:TextBox ID="txtExperience" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" Placeholder="Title, Company, Duration, Description (one per line)"></asp:TextBox>
                    </div>
                    <div class="mb-3"><h5>Education</h5>
                        <asp:TextBox ID="txtEducation" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" Placeholder="Degree, Institution, Duration (one per line)"></asp:TextBox>
                    </div>
                    <div class="mb-3"><h5>Skills</h5>
                        <asp:TextBox ID="txtSkills" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" Placeholder="Skill:Level (1-5), one per line"></asp:TextBox>
                    </div>
                    <div class="mb-3"><h5>Projects</h5>
                        <asp:TextBox ID="txtProjects" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" Placeholder="Project Name | Description | URL"></asp:TextBox>
                    </div>
                    <div class="mb-3"><h5>Additional Notes</h5>
                        <asp:TextBox ID="txtAdditional" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" Placeholder="Certifications, Awards, Hobbies"></asp:TextBox>
                    </div>

              


                </asp:Panel>
            </div>


                                <!-- Template -->
                    <div class="col-md-6">
                        <h5>Template</h5>
                         <div class="d-flex align-items-center gap-2">
                        <asp:DropDownList ID="ddlTemplate" runat="server" CssClass="form-select" AutoPostBack="True" 
                            OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged">
    <asp:ListItem Text="Classic" Value="Classic" Selected="True"></asp:ListItem>
    <asp:ListItem Text="Modern" Value="Modern"></asp:ListItem>
    <asp:ListItem Text="Portfolio" Value="Portfolio"></asp:ListItem>
                            <asp:ListItem Text="Creative" Value="Creative"></asp:ListItem>
                            <asp:ListItem Text="Executive" Value="Executive"></asp:ListItem>
                            <asp:ListItem Text="Tech Focus" Value="TechFocus"></asp:ListItem>
                            <asp:ListItem Text="Academic / Research" Value="AcademicResearch"></asp:ListItem>
                            <asp:ListItem Text="Startup / Entrepreneur" Value="StartupEntrepreneur"></asp:ListItem>
                            <asp:ListItem Text="Minimalist" Value="Minimalist"></asp:ListItem>
                            <asp:ListItem Text="Hybrid / Portfolio-CV" Value="HybridPortfolioCV"></asp:ListItem>
</asp:DropDownList>
                                                <asp:Button ID="btnRefreshTemplate" runat="server" Text="Refresh Template" CssClass="btn btn-info btn-lg" OnClick="btnRefreshTemplate_Click" />
                             </div>
<h5>Live Preview</h5>
                <div id="resumePreview" runat="server" class="border p-3" style="min-height:1000px; width:100%; background:#fff;">
                    Select Template Below To Preview
                </div>
                    </div>
            <!-- Live Preview -->

            <div class="col-md-6">
                
            </div>
        </div>
    </div>
</section>
            
        </ContentTemplate>
            <Triggers>
        <asp:PostBackTrigger ControlID="btnGeneratePDF" />
                        <asp:PostBackTrigger ControlID="btnGenerateDocx" />

                        <asp:PostBackTrigger ControlID="btnGenerateText" />

    </Triggers>
    </asp:UpdatePanel>
   
</asp:Content>
