<%@ Page Title="Manage Plants" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="managePlants.aspx.cs" Inherits="Ohoud10108WebApp.Demo.managePlants" %>



    <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Manage Your Plants 🌿</h2>

    <!-- Notification -->
    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
    
    <!-- FORM -->
    <asp:ValidationSummary ID="validationSummary" runat="server" ErrorMessage="* Make sure to fill in the required fields" ForeColor="Red"  />

    <div style="margin-bottom: 20px;">
        <asp:Label AssociatedControlID="txtPlantName" runat="server" Text="Plant Name: " />
        <asp:TextBox ID="txtPlantName" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ControlToValidate="txtPlantName" ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" runat="server" />

        <br />

        <asp:Label AssociatedControlID="ddlPlantType" runat="server" Text="Plant Type: " />
        <asp:DropDownList ID="ddlPlantType" runat="server" CssClass="form-control" />
        <asp:RequiredFieldValidator ControlToValidate="ddlPlantType" InitialValue="-1" ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" runat="server" />

        <br />

        <asp:Label runat="server" Text="Sunlight Need: " />
        <asp:RadioButtonList ID="rblSunlight" runat="server" RepeatDirection="Horizontal" class ="radio-list-horizontal">
            <asp:ListItem Text="Low" Value="Low" /> 
            <asp:ListItem Text="Medium" Value="Medium" />
            <asp:ListItem Text="High" Value="High" />
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ControlToValidate="rblSunlight" InitialValue="" ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" runat="server" />

        <br />

        <asp:Label runat="server" Text="Water Frequency: " />
        <asp:RadioButtonList ID="rblWaterFreq" runat="server" RepeatDirection="Horizontal" class ="radio-list-horizontal">
            <asp:ListItem Text="Daily" Value="Daily" />
            <asp:ListItem Text="Every 3 days" Value="Every 3 days" />
            <asp:ListItem Text="Weekly" Value="Weekly" />
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ControlToValidate="rblWaterFreq" InitialValue="" ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" runat="server" />

        <br />

        <asp:Label runat="server" Text="Is Indoor? " />
        <asp:RadioButtonList ID="rblIsIndoor" runat="server" RepeatDirection="Horizontal" class ="radio-list-horizontal">
            <asp:ListItem Text="Yes" Value="1" />
            <asp:ListItem Text="No" Value="0" />
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ControlToValidate="rblIsIndoor" InitialValue="" ErrorMessage="* Required" ForeColor="Red" Display="Dynamic" runat="server" />

        <br />

        <asp:Label AssociatedControlID="txtNotes" runat="server" Text="Notes: " />
        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="3" Columns="50" CssClass="form-control" />

        <br />

        <asp:Label AssociatedControlID="txtImageUrl" runat="server" Text="Image URL: " />
        <asp:TextBox ID="txtImageUrl" runat="server" CssClass="form-control" />

        <br />

        <!-- Hidden Field for Edit -->
        <asp:HiddenField ID="hiddenPlantId" runat="server" />

        <!-- Buttons -->
        <asp:Button ID="btnAdd" runat="server" Text="Add Plant" OnClick="btnAdd_Click" />
        <asp:Button ID="btnGetData" runat="server" Text="Get Data" OnClick="btnGetData_Click" CausesValidation="false" />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update Plant" OnClick="btnUpdate_Click" Visible="false" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CausesValidation="false"  />
         <!-- Export -->
    <asp:Button ID="Button1" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" CausesValidation="false" />

    </div>

    <!-- Export -->
   <!-- <asp:Button ID="btnExportExcel" runat="server" Text="Export to Excel" OnClick="btnExportExcel_Click" CausesValidation="false" />

    <br /><br /> -->

    <!-- GridView -->
    <asp:Panel ID="pnlGrid" runat="server" Visible="false">
    <asp:GridView ID="gvPlants" runat="server" AutoGenerateColumns="False" 
    OnRowCommand="gvPlants_RowCommand" CssClass="table table-bordered" 
    DataKeyNames="plantId,typeId">
        <Columns>
            <asp:BoundField DataField="plantName" HeaderText="Name" />
            <asp:BoundField DataField="typeName" HeaderText="Type" />
            <asp:BoundField DataField="sunlightNeed" HeaderText="Sunlight" />
            <asp:BoundField DataField="waterFreq" HeaderText="Water" />
            <asp:BoundField DataField="isIndoor" HeaderText="Indoor?" />
            <asp:BoundField DataField="notes" HeaderText="Notes" />
            <asp:BoundField DataField="imageUrl" HeaderText="Image" />
            <asp:TemplateField>
                <ItemTemplate>
                  <!--  <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditPlant" CommandArgument='<%# Eval("plantId") %>' Text="Edit" OnClientClick="return confirm('Edit this plant?');" />
                    &nbsp;|&nbsp;
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeletePlant" CommandArgument='<%# Eval("plantId") %>' Text="Delete" OnClientClick="return confirm('Are you sure to delete?');" /> -->

                    <div style="display: flex; gap: 6px;">
                        <asp:Button ID="btnEdit" runat="server" Text="Edit ✏️"
                            CommandName="EditPlant"
                            CommandArgument='<%# Container.DataItemIndex %>'
                            CausesValidation="false"
                            Style="background-color: #4CAF50; color: white; border: none; padding: 6px 12px; border-radius: 5px; cursor: pointer; font-size: 14px;" />

                        <asp:Button ID="btnDelete" runat="server" Text="Delete🗑️"
                            CommandName="DeletePlant"
                            CommandArgument='<%# Container.DataItemIndex %>'
                            CausesValidation="false"
                            OnClientClick="return confirm('Are you sure you want to delete this plant?');"
                            Style="background-color: #f44336; color: white; border: none; padding: 6px 12px; border-radius: 5px; cursor: pointer; font-size: 14px;" />
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>

</asp:Content>
