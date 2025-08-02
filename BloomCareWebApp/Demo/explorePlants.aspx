<%@ Page Title="Explore Plants" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="explorePlants.aspx.cs" Inherits="Ohoud10108WebApp.Demo.explorePlants" %>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="text-align: center; font-size: 28px; margin-bottom: 30px;">Explore Plants</h2>

    <!-- Gallery container -->
    <div style="display: flex; flex-wrap: wrap; justify-content: center; gap: 20px;">

        <asp:Repeater ID="rptPlants" runat="server">
            <ItemTemplate>
                <div style="flex: 0 1 calc(33.33% - 40px); box-sizing: border-box;
                            border: 2px solid #cce5cc; border-radius: 12px; padding: 20px;
                            background-color: #f4faf4; box-shadow: 2px 2px 8px rgba(0, 0, 0, 0.1);
                            transition: transform 0.3s ease-in-out;">
                    <img src='<%# Eval("imageUrl") %>' alt='<%# Eval("plantName") %>'
                        style="width: 100%; height: 180px; object-fit: cover; border-radius: 8px; margin-bottom: 15px;" />
                    
                    <div style="text-align: left;">
                        <h3 style="margin: 5px 0; font-size: 22px; color: #336633;"><%# Eval("plantName") %></h3>
                        <p style="margin: 4px 0; font-size: 14px; color: #444;"><strong>Type:</strong> <%# Eval("typeName") %></p>
                        <p style="margin: 4px 0; font-size: 14px; color: #444;"><strong>Sunlight:</strong> <%# Eval("sunlightNeed") %></p>
                        <p style="margin: 4px 0; font-size: 14px; color: #444;"><strong>Water:</strong> <%# Eval("waterFreq") %></p>
                        <p style="margin: 4px 0; font-size: 14px; color: #444;"><strong>Indoor:</strong> <%# Convert.ToBoolean(Eval("isIndoor")) ? "Yes" : "No" %></p>
                        <p style="margin: 4px 0; font-size: 14px; color: #444;"><strong>Notes:</strong> <%# Eval("notes") %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>
</asp:Content>