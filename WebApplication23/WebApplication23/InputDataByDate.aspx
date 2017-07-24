<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InputDataByDate.aspx.vb" Inherits="WebApplication23.InputDataByDate" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset style="margin:0 0 0 0;"><legend>Upload Data By Excel</legend>
        <div style="float: left; ">
                <dx:ASPxTextBox ClientInstanceName="txt_teamID" runat="server" ID="txt_teamID" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxButton ID="btn_ExportXlsTemp" ClientInstanceName="btn_temp" runat="server" Text="Export Excel Temp" AutoPostBack="true" Width="90px" >
                    </dx:ASPxButton>
                    <dx:ASPxLabel ID="lbl_temp" runat="server" Text="Download ตัวอย่างไฟล์ Excel" style="margin-right:43px;"></dx:ASPxLabel>
                
        </div>
        <div style="clear: left; float:left; margin-top: 10px;">
                     <asp:FileUpload runat="server" ID="file_importPartiData"  />
                     <dx:ASPxButton ID="btn_import" ClientInstanceName="btn_import" runat="server" Text="Import">
                     </dx:ASPxButton>
        </div>
    </fieldset>
    <fieldset style="margin:0 0 0 0;"><legend>Latest Update</legend>
        <dx:ASPxGridView ClientInstanceName="gv_matchDetail" ID="gv_matchDetail" runat="server" SkinID="PlasticBlue"
            KeyFieldName="id">
            <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="true" />
            <SettingsEditing Mode="Inline" />
            <SettingsPager PageSize="15"></SettingsPager>
            <Columns>
                <dx:GridViewDataTextColumn Caption="Team Name" FieldName="team_id" Width="100" CellStyle-HorizontalAlign="Left">
                    <DataItemTemplate>
                        <dx:ASPxLabel ID="teamName" runat="server" Text='<%# GetTeamName(Eval("team_id")) %>' ></dx:ASPxLabel>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn caption="Date" FieldName="date" Width="100">
                    <PropertiesDateEdit DisplayFormatString="d/MM/yyyy"></PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn Caption="Program" FieldName="match_program" >
                     <DataItemTemplate>
                        <dx:ASPxLabel ID="mpName" runat="server" Text='<%# GetMPName(Eval("match_program")) %>' ></dx:ASPxLabel>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Location" FieldName="location">
                    <DataItemTemplate>
                        <dx:ASPxLabel ID="loName" runat="server" Text='<%# GetLocationName(Eval("location")) %>' ></dx:ASPxLabel>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Goal(+)" FieldName="goal_positive"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Goal(-)" FieldName="goal_negative"></dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
       <%--<asp:ObjectDataSource runat="server" ID="ods_matchDetail" TypeName="WebApplication23.DataManager" 
            SelectMethod="GetMatchDetailDataTableByTeamID" > <%--DataObjectTypeName = "WebApplication23.League_Master" UpdateMethod="UpdateLeagueMaster" DeleteMethod="DeleteLeagueMaster"
            InsertMethod="InsertLeague"--%>
        <%--</asp:ObjectDataSource>--%>
    </fieldset>
</asp:Content>
