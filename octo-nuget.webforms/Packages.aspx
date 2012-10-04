<%@ Page Language="C#" Inherits="OctoNuget.WebForms.Packages" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
    <head runat="server">
        <title>Packages</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <ul>
                <asp:Repeater id="rptPackages" runat="server">
                    <HeaderTemplate>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <%# Eval("Id") %>.<%# Eval("Version") %>.nupkg
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                    </FooterTemplate>
                </asp:Repeater>
            </ul>
        </form>
    </body>
</html>