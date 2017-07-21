Public Class InputDataPage
    Inherits System.Web.UI.Page

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

    Private Sub ods_cbbteam_Filtering(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceFilteringEventArgs) Handles ods_cbbteam.Filtering
        e.ParameterValues("league_id") = LeagueID
    End Sub

    Private Sub ods_cbbteam_Selecting(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ods_cbbteam.Selecting
        e.InputParameters("league_id") = LeagueID
    End Sub

    Private Sub cbb_team_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbb_team.Callback
        cbb_team.DataBind()
    End Sub

    Private Sub btn_ExportXlsTemp_Click(sender As Object, e As System.EventArgs) Handles btn_ExportXlsTemp.Click
        Dim fName = "MatchDataTemp.xlsx"
        Dim templateFile = Server.MapPath("~/Templates/" & fName)

        If Not IO.File.Exists(templateFile) Then
            ' btn_ExportXlsTemp.JSProperties("cpError") = "Unable to load " & IO.Path.GetFileName(templateFile)
            ClientScript.RegisterStartupScript(Me.GetType(), "myalert", "alert('" & "Unable to load " & IO.Path.GetFileName(templateFile) & "');", True)
            '(Page, "Unable to load " & IO.Path.GetFileName(templateFile))
        Else
            Dim bytes = IO.File.ReadAllBytes(templateFile)
            Response.ContentType = "application/vnd.ms-excel"
            Response.ContentEncoding = Encoding.UTF8
            Response.AddHeader("Content-Disposition", "attachment;filename=" & fName)
            Response.BinaryWrite(bytes)
            Response.Flush()
            Response.End()
            bytes = Nothing
        End If
    End Sub

    Private Sub btn_import_Click(sender As Object, e As System.EventArgs) Handles btn_import.Click
        Dim alert As String = String.Empty
        If Not String.IsNullOrEmpty(file_importPartiData.FileName) Then
            Dim fileName As String = file_importPartiData.FileName
            Dim material As String() = fileName.Split(".")
            Dim extenstion As String = String.Empty
            If material.Length > 1 Then
                extenstion = material(1)
                If extenstion.ToLower = "xlsx" Then

                Else
                    alert = "Please only upload file  .xlsx"
                End If
            Else
                alert = "Please only upload file  .xlsx"
            End If 
        Else
            alert = "File upload is empty"
        End If
        If Not String.IsNullOrEmpty(alert) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "importAlert", "alert('" & alert & "');", True)
        End If
    End Sub
End Class