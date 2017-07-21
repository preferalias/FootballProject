<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InputDataPage.aspx.vb" Inherits="WebApplication23.InputDataPage" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="headerText">
        <h1>Input Data</h1>
    </div>
    <fieldset style="margin:0 0 0 0;"><legend>Upload Data By Excel</legend>
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
    
        <div style="float: right; ">
                        <dx:ASPxButton ID="btn_ExportXlsTemp" ClientInstanceName="btn_temp" runat="server" Text="Export Excel Temp" AutoPostBack="true" Width="90px" >
                    </dx:ASPxButton>
                    <dx:ASPxLabel ID="lbl_temp" runat="server" Text="Download ตัวอย่างไฟล์ Excel" style="margin-right:43px;"></dx:ASPxLabel>
                
        </div>
        <div style="clear: right; float:right; margin-top: 10px;">
                     <asp:FileUpload runat="server" ID="file_importPartiData"  />
                     <dx:ASPxButton ID="btn_import" ClientInstanceName="btn_import" runat="server" Text="Import"></dx:ASPxButton>
        </div>
    </fieldset>
</asp:Content>
