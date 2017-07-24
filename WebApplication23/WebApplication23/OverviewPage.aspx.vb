Public Class OverviewPage
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

    Private Property teamID As String
        Get
            Return If(String.IsNullOrEmpty(txt_teamID.Text), 0, txt_teamID.Text)
        End Get
        Set(value As String)
            txt_teamID.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ods_cbbteam_Filtering(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceFilteringEventArgs) Handles ods_cbbteam.Filtering
        e.ParameterValues("league_id") = LeagueID
    End Sub

    Private Sub ods_cbbteam_Selecting(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ods_cbbteam.Selecting
        e.InputParameters("league_id") = LeagueID
    End Sub

    Private Sub cbb_team_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbb_team.Callback
        cbb_team.DataBind()
    End Sub
    Public Function GetLocationName(ByVal locationID As Integer) As String
        Dim loName As String
        loName = DTM.GetLocationNameByID(locationID)
        Return loName
    End Function

    Private Sub gv_matchDetail_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles gv_matchDetail.CustomCallback
        gv_matchDetail.DataBind()
    End Sub

    Private Sub gv_matchDetail_DataBinding(sender As Object, e As System.EventArgs) Handles gv_matchDetail.DataBinding
        gv_matchDetail.DataSource = DTM.GetMatchDetailDataTableByTeamID(teamID)
    End Sub

    Public Function GetTeamName(ByVal teamID As Integer) As String
        Dim loName As String
        loName = DTM.GetTeamNameByID(teamID)
        Return loName
    End Function

    Public Function GetMPName(ByVal id As Integer) As String
        Dim loName As String
        loName = DTM.GetMatchProgramNameByID(id)
        Return loName
    End Function
End Class