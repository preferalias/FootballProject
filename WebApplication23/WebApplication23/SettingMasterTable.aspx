<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SettingMasterTable.aspx.vb" Inherits="WebApplication23.SettingMasterTable" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="headerText">
        <h1>Setting Master Table</h1>
    </div>
    <fieldset><legend>League and Team</legend>
    <div style="float:left; width:250px; margin-right:50px; margin-bottom:20px;">
        <dx:ASPxLabel ID="lbl_leagueTable" runat="server" text="League Table" style="margin-bottom:7px;"></dx:ASPxLabel>
        <dx:ASPxGridView ClientInstanceName="gv_league" ID="gv_league" runat="server" DataSourceID="ods_leagueMaster" SkinID="PlasticBlue"
        KeyFieldName="league_id">
        <ClientSideEvents EndCallback="function(s,e) { cbb_league.PerformCallback();}" />
        <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="true" />
        <SettingsEditing Mode="Inline" />
        <SettingsPager PageSize="15"></SettingsPager>
        <Columns>
            <dx:GridViewDataColumn EditFormSettings-Visible="False" Caption="ID" ReadOnly="true" FieldName="league_id" ></dx:GridViewDataColumn>
            <dx:GridViewDataTextColumn Caption="League Name" FieldName="league_name" Width="200">
                
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn ShowEditButton="true"
                            ShowUpdateButton="true" ShowCancelButton="true" ShowDeleteButton="true">
                            <HeaderCaptionTemplate>
                            <dx:ASPxButton ID="btn_New" Text="Add" runat="server" Cursor="pointer" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { gv_league.AddNewRow(); }" />
                            </dx:ASPxButton>
                        </HeaderCaptionTemplate>
            </dx:GridViewCommandColumn>
        </Columns>
        </dx:ASPxGridView>
        <asp:ObjectDataSource runat="server" ID="ods_leagueMaster" TypeName="WebApplication23.DataManager" InsertMethod="InsertLeague" DataObjectTypeName = "WebApplication23.League_Master"
                    SelectMethod="GetAllLeagueDataTable" UpdateMethod="UpdateLeagueMaster" DeleteMethod="DeleteLeagueMaster"></asp:ObjectDataSource>
    </div>
    <div style="float:left; margin-bottom:20px;">
        <dx:ASPxTextBox ClientInstanceName="txt_leagueID" runat="server" ID="txt_leagueID" ClientVisible="false"></dx:ASPxTextBox>
        
        <dx:ASPxLabel ID="lbl_league" runat="server" Text="Choose League" style="float:left; 
            margin-right:10px; margin-bottom:15px; margin-top:2px;"></dx:ASPxLabel>
        <dx:ASPxComboBox ClientInstanceName="cbb_league" ID="cbb_league" runat="server" DataSourceID="ods_cbbleague" style="margin-bottom:15px;">
            <ClientSideEvents SelectedIndexChanged="function(s,e){ var info = s.GetText().split(';');
                                                                    s.SetText(info[1]);
                                                                    txt_leagueID.SetText(info[0]);
                                                                    gv_team.PerformCallback();
                                                                    }" />              
            <Columns> 
                <dx:ListBoxColumn Caption="ID" FieldName="league_id" Width="5px" />
                <dx:ListBoxColumn Caption="Name" FieldName="league_name" Width="70px" />
            </Columns>
        </dx:ASPxComboBox>
        <asp:ObjectDataSource runat="server" ID="ods_cbbleague" TypeName="WebApplication23.DataManager" 
                    SelectMethod="GetAllLeagueDataTable" ></asp:ObjectDataSource>
        <dx:ASPxLabel ID="lbl_teamtable" runat="server" text="Team Table" style="clear:both; margin-bottom:7px;"></dx:ASPxLabel>
        <dx:ASPxGridView ID="gv_team" KeyFieldName="team_id" DataSourceID="ods_team" ClientInstanceName="gv_team" runat="server">
        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
        <SettingsBehavior ConfirmDelete="false" EnableRowHotTrack="true" />
        <SettingsEditing Mode="Inline" />
            <Columns> 
                <dx:GridViewDataColumn ReadOnly="true" Caption="No." ></dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="ID" FieldName="team_id" ReadOnly="true"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="League ID" FieldName="league_id"></dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Team" FieldName="team_name" Width="200px"></dx:GridViewDataColumn>
                <dx:GridViewCommandColumn ShowEditButton="true"
                            ShowUpdateButton="true" ShowCancelButton="true" ShowDeleteButton="true">
                            <HeaderCaptionTemplate>
                            <dx:ASPxButton ID="btn_New" Text="Add" runat="server" Cursor="pointer" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { gv_team.AddNewRow(); }" />
                            </dx:ASPxButton>
                        </HeaderCaptionTemplate>
                </dx:GridViewCommandColumn>
            </Columns>
        </dx:ASPxGridView>
        <div style="margin-top:10px; margin-bottom:5px;">
            <dx:ASPxLabel ID="lbl_addTeam" runat="server" Text="Add Multiple Data(use , to seperate): ex. Liverpool,Everton,Arsenal" ></dx:ASPxLabel>
        </div>
        <dx:ASPxTextBox ID="txt_addTeam" ClientInstanceName="txt_addTeam" runat="server" Width="440px"></dx:ASPxTextBox>
        <dx:ASPxButton ID="btn_addTeam" runat="server" Text="Insert Data" AutoPostBack="false">
            <ClientSideEvents Click="function(s,e){ if (txt_addTeam.GetText()) {
                                                      if (txt_leagueID.GetText()) {
                                                            gv_team.PerformCallback('SaveTextBox');
                                                        }
                                                        else {
                                                            alert('Choose League');
                                                        }
                                                    }
                                                    else {
                                                        alert('Insert Team name');
                                                    }}" />
        </dx:ASPxButton>
        <asp:ObjectDataSource runat="server" ID="ods_team" TypeName="WebApplication23.DataManager" 
            DataObjectTypeName = "WebApplication23.Team_Master" SelectMethod="GetTeamByLeague"
            InsertMethod="InsertTeam" UpdateMethod="UpdateTeamMaster" DeleteMethod="DeleteTeamMaster" >
                <SelectParameters>
                    <asp:ControlParameter ControlID="txt_leagueID"  Name="league_id" PropertyName="Text" Type="String" />
                </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </fieldset>
    <fieldset><legend>Other</legend>
        <div style="float:left; width:250px; margin-right:50px;">
            <dx:ASPxLabel ID="lbl_matchpro" runat="server" text="Match Program Table" style="margin-bottom:7px;"></dx:ASPxLabel>
            <dx:ASPxGridView ID="gv_matchpro" KeyFieldName="id" DataSourceID="ods_matchpro" ClientInstanceName="gv_matchpro" runat="server">
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="true" />
            <SettingsEditing Mode="Inline" />
                <Columns> 
                    <dx:GridViewDataColumn Caption="ID" FieldName="id"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Match Program" FieldName="mp_name" ></dx:GridViewDataColumn>
                    <dx:GridViewCommandColumn ShowEditButton="true"
                                ShowUpdateButton="true" ShowCancelButton="true" ShowDeleteButton="true">
                                <HeaderCaptionTemplate>
                                <dx:ASPxButton ID="btn_New" Text="Add" runat="server" Cursor="pointer" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) { gv_matchpro.AddNewRow(); }" />
                                </dx:ASPxButton>
                            </HeaderCaptionTemplate>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:ObjectDataSource runat="server" ID="ods_matchpro" TypeName="WebApplication23.DataManager" 
                DataObjectTypeName = "WebApplication23.Match_Program" SelectMethod="GetAllMatchProgram"
                InsertMethod="InsertMatchProgram" UpdateMethod="UpdateMatchProgram" DeleteMethod="DeleteMatchProgram" >
            </asp:ObjectDataSource>
        </div>
        <div style="float:left;">
            <dx:ASPxLabel ID="lbl_location" runat="server" text="Location Table" style="margin-bottom:7px;"></dx:ASPxLabel>
            <dx:ASPxGridView ID="gv_location" KeyFieldName="id" DataSourceID="ods_location" ClientInstanceName="gv_location" runat="server">
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <SettingsBehavior ConfirmDelete="true" EnableRowHotTrack="true" />
            <SettingsEditing Mode="Inline" />
                <Columns> 
                    <dx:GridViewDataColumn Caption="ID" FieldName="id"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Location" FieldName="location" ></dx:GridViewDataColumn>
                    <dx:GridViewCommandColumn ShowEditButton="true"
                                ShowUpdateButton="true" ShowCancelButton="true" ShowDeleteButton="true">
                                <HeaderCaptionTemplate>
                                <dx:ASPxButton ID="btn_New" Text="Add" runat="server" Cursor="pointer" AutoPostBack="False">
                                    <ClientSideEvents Click="function(s, e) { gv_location.AddNewRow(); }" />
                                </dx:ASPxButton>
                            </HeaderCaptionTemplate>
                    </dx:GridViewCommandColumn>
                </Columns>
            </dx:ASPxGridView>
            <asp:ObjectDataSource runat="server" ID="ods_location" TypeName="WebApplication23.DataManager" 
                DataObjectTypeName = "WebApplication23.Location" SelectMethod="GetAllLocation"
                InsertMethod="InsertLocation" UpdateMethod="UpdateLocation" DeleteMethod="DeleteLocation" >
            </asp:ObjectDataSource>
        </div>
    </fieldset>
</asp:Content>
