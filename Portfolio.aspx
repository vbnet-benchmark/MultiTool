<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SiteLandingPage.Master" CodeBehind="Portfolio.aspx.vb" Inherits="MultiTool.Portfolio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Portfolio/Projects
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Projects / Work Section -->
<section class="py-5">
    <div class="container">
        <h2 class="mb-4">My Work</h2>

        <!-- Search Box -->
        <input type="text" id="projectSearch" class="form-control mb-4" placeholder="Search projects..." onkeyup="filterProjects()">

        <div class="row" id="projectsContainer">
            <!-- Example Project Card -->
            <div class="col-md-4 mb-4 project-item">
                <div class="card h-100 shadow-sm project-card">
                    <div class="card-body">
                        <h5 class="card-title">Calculator Suite</h5>
                        <p class="card-text">A web app featuring multiple calculators for health, finance, productivity, etc.</p>
                        <a href="../Applications/CalculatorSuite.aspx" class="btn btn-primary">View</a>
                    </div>
                </div>
            </div>

            <div class="col-md-4 mb-4 project-item">
                <div class="card h-100 shadow-sm project-card">
                    <div class="card-body">
                        <h5 class="card-title">Resume & Portfolio Generator</h5>
                        <p class="card-text">Generate Resumes & Portfolios on the fly. We do not store your information for privacy.</p>
                        <a href="../Applications/ResumePortfolioGenerator.aspx" class="btn btn-primary">View</a>
                    </div>
                </div>
            </div>

            <div class="col-md-4 mb-4 project-item">
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

<script>
    // Filter projects dynamically
    function filterProjects() {
        var input = document.getElementById('projectSearch');
        var filter = input.value.toLowerCase();
        var projects = document.getElementsByClassName('project-item');

        for (var i = 0; i < projects.length; i++) {
            var title = projects[i].getElementsByClassName('card-title')[0].innerText.toLowerCase();
            var desc = projects[i].getElementsByClassName('card-text')[0].innerText.toLowerCase();
            if (title.includes(filter) || desc.includes(filter)) {
                projects[i].style.display = "";
            } else {
                projects[i].style.display = "none";
            }
        }
    }
</script>


</asp:Content>
