<%@ Page Language="C#" MasterPageFile="~/MasterPages/ListView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="WH100000.aspx.cs" Inherits="Page_WH100000" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/ListView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="CloudianGlobal.Withholdingtax"
        PrimaryView="withholdingTax"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phL" runat="Server">
	<px:PXGrid ID="grid" runat="server" DataSourceID="ds" Width="100%" Height="150px" SkinID="Primary" AllowAutoHide="false">
		<Levels>
			<px:PXGridLevel DataMember="withholdingTax">
			    <Columns>
				<px:PXGridColumn DataField="Idnbr" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Atc" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Type" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Description" Width="70" ></px:PXGridColumn>
				<px:PXGridColumn DataField="TaxRate" Width="180" ></px:PXGridColumn>
				<px:PXGridColumn DataField="Bir_form" Width="180" ></px:PXGridColumn></Columns>
			</px:PXGridLevel>
		</Levels>
		<AutoSize Container="Window" Enabled="True" MinHeight="150" ></AutoSize>
		<ActionBar >
		</ActionBar>
	
		<Mode AllowUpload="True" /></px:PXGrid>
</asp:Content>
