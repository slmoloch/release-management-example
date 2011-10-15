<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPopupControl" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxCallbackPanel" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPanel" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxCallback" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <script type="text/javascript">
    // <![CDATA[
        var keyValue;
        function OnSendEmailClick(element, key) {
            callbackPanel.SetContentHtml("");
            popup.ShowAtElement(element);
            keyValue = key;
        }
        function popup_Shown(s, e) {
            callbackPanel.PerformCallback(keyValue);
        }

        function SendEmail() {
            SendMessageCallback.PerformCallback(keyValue + ";" + txtMessage.value);
        }

        function OnCallbackComplete(s, e) 
        {
            popup.Hide();
        }
        
    // ]]> 
    </script>

    <dx:ASPxPopupControl 
        ID="popup" 
        ClientInstanceName="popup" 
        runat="server" 
        AllowDragging="true"
        PopupHorizontalAlign="OutsideRight" 
        HeaderText="Send e-mail">

        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel 
                    ID="callbackPanel" 
                    ClientInstanceName="callbackPanel" 
                    runat="server"
                    Width="320px" 
                    Height="100px" 
                    OnCallback="callbackPanel_Callback" 
                    RenderMode="Table">
                    <PanelCollection>
                        <dx:PanelContent runat="server" >
                            <table>
                                <tr>
                                    <td>
                                        Message to: <asp:Literal ID="ltName" runat="server" Text=""/> (<asp:Literal ID="ltEmail" runat="server" Text=""/>)
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Message: <input type="text" id="txtMessage"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Send" EnableClientSideAPI="True" AutoPostBack="False">
                                            <ClientSideEvents Click="SendEmail" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ClientSideEvents Shown="popup_Shown" />
    </dx:ASPxPopupControl>

     <dx:ASPxCallback 
        ID="SendMessageCallback" 
        runat="server" 
        ClientInstanceName="SendMessageCallback"
        OnCallback="SendMessage">
        <ClientSideEvents CallbackComplete="OnCallbackComplete" />
    </dx:ASPxCallback>

    <dx:ASPxGridView 
        ID="grid" 
        ClientInstanceName="grid"
        runat="server"
        DataSourceID="masterDataSource"
        KeyFieldName="Id" 
        Width="100%">

        <Columns>
            <dx:GridViewDataColumn FieldName="Name" VisibleIndex="1" />
        </Columns>

        <Templates>
            <DetailRow>
                Region: <b><%# Eval("Name")%></b>
                <br />
                <br />
                <dx:ASPxGridView 
                    ID="detailGrid" 
                    runat="server" 
                    DataSourceID="detailDataSource" 
                    KeyFieldName="Id"
                    Width="100%" 
                    OnBeforePerformDataSelect="detailGrid_DataSelect">
                    <Columns>
                        <dx:GridViewCommandColumn VisibleIndex="0">
                            <EditButton Visible="True" />
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataColumn FieldName="Name" Caption="Name" VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="Email" Caption="E-Mail" VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="Actual" VisibleIndex="3" />
                        <dx:GridViewDataColumn FieldName="Estimated" VisibleIndex="4" />

                        <dx:GridViewDataColumn Caption="Actions" VisibleIndex="5" Width="15%" >
                            <DataItemTemplate>
                                <a href="javascript:void(0);" onclick="OnSendEmailClick(this, '<%# Container.KeyValue %>')">
                                   Send e-mail...</a>
                            </DataItemTemplate>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsEditing EditFormColumnCount="3"  Mode="PopupEditForm" PopupEditFormWidth="600px"  />
                    <Settings ShowTitlePanel="true" />
                    <SettingsText Title="Edit Person" />
                </dx:ASPxGridView>
            </DetailRow>
        </Templates>
        <SettingsDetail ShowDetailRow="true" />
    </dx:ASPxGridView>

    <asp:ObjectDataSource 
        runat="server"
        ID="masterDataSource"
        SelectMethod="GetAll"
        TypeName="RepresentativesDataAccess.RegionGateway" />

    <asp:ObjectDataSource 
        runat="server"
        ID="detailDataSource"  
        SelectMethod="GetForRegion"
        UpdateMethod="Update"
        TypeName="RepresentativesDataAccess.PersonGateway">

        <SelectParameters>
            <asp:SessionParameter Name="RegionId" SessionField="RegionId" Type="Int32" />
        </SelectParameters>

        <UpdateParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Actual" Type="Int32" />
            <asp:Parameter Name="Estimated" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>
