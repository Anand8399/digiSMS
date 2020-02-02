<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="PresentationLayer.Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Print() {
            var dvReport = document.getElementById("dvReport");
            var frame1 = dvReport.getElementsByTagName("iframe")[0];
            if (navigator.appName.indexOf("Internet Explorer") != -1) {
                frame1.name = frame1.id;
                window.frames[frame1.id].focus();
                window.frames[frame1.id].print();
            }
            else {
                var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                frameDoc.print();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:RadioButtonList ID="rbFormat" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Word" Value="Word"  />
                <asp:ListItem Text="Excel" Value="Excel" />
                <asp:ListItem Text="PDF" Value="PDF" Selected="True" />
                <asp:ListItem Text="CSV" Value="CSV" />
            </asp:RadioButtonList>
            <asp:Button ID="btnExportToPDF" runat="server" OnClick="btnExportToPDF_Click" Text="Export To PDF" />
            <%--<asp:LinkButton runat="server" PostBackUrl="~/Home/Index"></asp:LinkButton>--%>
            <%--<asp:Button ID="btnHome" runat="server" Text="Go To Home Page" OnClientClick="../Home"  />--%>
        <%--<asp:Button ID="btnPrint" runat="server" Text="Print Directly" OnClientClick="Print()"></asp:Button>--%>
        <div id="dvReport">            
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" ToolPanelView="None" DisplayToolbar="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" />

        </div>
    </form>
</body>
</html>
