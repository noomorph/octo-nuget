<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs"
    Inherits="OctoNuget.WebForms.Configuration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        thead tr { background-color: silver }
        th {text-align: left}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <asp:Repeater ID="rptSettings" runat="server">
            <HeaderTemplate>
                <thead>
                    <tr>
                        <th>Key</th>
                        <th>Value</th>
                    </tr>
                </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <th style="width:300px;"><%# Eval("Key") %></th>
                    <td style="min-width: 300px;"><%# Eval("Value") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
