<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OverviewPage.aspx.vb" Inherits="WebApplication23.OverviewPage" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="headerText">
        <h1>Overview Match Details</h1>
    </div>
    <fieldset style="margin:0 0 0 0;"><legend>Filter</legend>
      <div style="float:left; margin-bottom:20px;">
        
        <dx:ASPxTextBox ClientInstanceName="txt_leagueID" runat="server" ID="txt_leagueID" ClientVisible="false"></dx:ASPxTextBox>
        <dx:ASPxTextBox ClientInstanceName="txt_teamID" runat="server" ID="txt_teamID" ClientVisible="false"></dx:ASPxTextBox>
        <dx:ASPxLabel ID="lbl_league" runat="server" Text="Choose League" style="float:left; 
            margin-right:10px; margin-bottom:15px; margin-top:2px;"></dx:ASPxLabel>
        <dx:ASPxComboBox ClientInstanceName="cbb_league" ID="cbb_league" runat="server" DataSourceID="ods_cbbleague" style="margin-bottom:15px;">
            <ClientSideEvents SelectedIndexChanged="function(s,e){ var info = s.GetText().split(';');
                                                                    s.SetText(info[1]);
                                                                    txt_leagueID.SetText(info[0]);
                                                                    txt_teamID.SetText('');
                                                                    cbb_team.PerformCallback();
                                                                    }" />              
            <Columns> 
                <dx:ListBoxColumn Caption="ID" FieldName="league_id" Width="5px" />
                <dx:ListBoxColumn Caption="Name" FieldName="league_name" Width="70px" />
            </Columns>
        </dx:ASPxComboBox>
        <asp:ObjectDataSource runat="server" ID="ods_cbbleague" TypeName="WebApplication23.DataManager" 
                    SelectMethod="GetAllLeagueDataTable" ></asp:ObjectDataSource>
        <dx:ASPxLabel ID="lbl_team" runat="server" Text="Choose Team" style="float:left;margin-right:19px; margin-bottom:15px; margin-top:2px;"></dx:ASPxLabel>
        <dx:ASPxComboBox ClientInstanceName="cbb_team" ID="cbb_team" DataSourceID="ods_cbbteam" runat="server" Width="200px" style="margin-bottom:15px;">
             <ClientSideEvents SelectedIndexChanged="function(s,e){ var info = s.GetText().split(';');
                                                                    s.SetText(info[1]);
                                                                    txt_teamID.SetText(info[0]);
                                                                    gv_matchDetail.PerformCallback();
                                                                    }" />            
            <Columns> 
                <dx:ListBoxColumn Caption="ID" FieldName="team_id" Width="5px" />
                <dx:ListBoxColumn Caption="Name" FieldName="team_name" Width="70px" />
            </Columns>       
        </dx:ASPxComboBox>
        
        <asp:ObjectDataSource runat="server" ID="ods_cbbteam" TypeName="WebApplication23.DataManager" 
                    SelectMethod="GetTeamByLeague" >
        </asp:ObjectDataSource>
        </div>
    </fieldset>
    <fieldset><legend>Result</legend>
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
