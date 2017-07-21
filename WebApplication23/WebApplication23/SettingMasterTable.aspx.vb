Public Class SettingMasterTable
    Inherits System.Web.UI.Page

    Public DTM As New DataManager

    Private Property LeagueID As String
        Get
            Return If(String.IsNullOrEmpty(txt_leagueID.Text), 0, txt_leagueID.Text)
        End Get
        Set(value As String)
            txt_leagueID.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub gv_team_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles gv_team.CustomCallback
        If e.Parameters = "SaveTextBox" Then
            Dim team As String() = txt_addTeam.Text.Split(",")
            Dim teamList As New List(Of Team_Master)
            For Each v In team
                Dim team1 As New Team_Master
                team1.league_id = LeagueID
                team1.team_name = v
                teamList.Add(team1)
            Next
            DTM.InsertTeamList(teamList)
        End If
        gv_team.DataBind()
    End Sub

    Private Sub gv_team_CustomColumnDisplayText(sender As Object, e As DevExpress.Web.ASPxGridViewColumnDisplayTextEventArgs) Handles gv_team.CustomColumnDisplayText
        If e.Column.Caption = "No." Then
            e.DisplayText = (e.VisibleIndex + 1).ToString
        End If
    End Sub

    Private Sub cbb_league_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbb_league.Callback
        cbb_league.DataBind()
    End Sub

    Private Sub gv_league_CellEditorInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles gv_league.CellEditorInitialize, gv_team.CellEditorInitialize
        e.Editor.Visible = Not e.Editor.ReadOnly
    End Sub
End Class