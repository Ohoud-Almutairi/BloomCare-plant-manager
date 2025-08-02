<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="Ohoud10108WebApp.Demo.contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Contact Us</h2>
    <style>
        .contact-wrapper {
            background-color: white;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            max-width: 600px;
            margin: 40px auto;
            font-family: Arial, sans-serif;
        }

        .contact-wrapper h2 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 25px;
            font-size: 28px;
        }

        .contact-table {
            width: 100%;
            border-spacing: 10px;
        }

        .contact-table td:first-child {
            width: 30%;
            vertical-align: middle;
            font-weight: bold;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }

        .btn-send {
            text-align: center;
            background-color: #2E5C77;
            color: white;
            font-weight: bold;
            padding: 10px 20px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
        }

        .btn-send:hover {
            background-color: #1d3f56;
        }
    </style>

    <div class="contact-wrapper">
        

        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>

        <table class="contact-table">
            <tr>
                <td>From Email:</td>
                <td>
                    <asp:TextBox ID="txtSenderEmail" runat="server" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td>Subject:</td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" OnLoad="lblOutputClear_txtSubject" />
                </td>
            </tr>
            <tr>
                <td>Attachments:</td>
                <td>
                    <asp:FileUpload ID="fuAttachment" runat="server" AllowMultiple="true" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">Message:</td>
                <td>
                    <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Height="120px" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnSendMailViaMailMgr" runat="server" Text="Send Message" 
                        OnClick="btnSendMailViaMailMgr_Click" CssClass="btn-send" />

                    <asp:Button ID="btnSendViaCode" runat="server" Text="Send email via Code" 
                        OnClick="btnSendViaCode_Click" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
