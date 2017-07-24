Public Class InputDataByDate
    Inherits System.Web.UI.Page

    Private Property teamID As String
        Get
            Return If(String.IsNullOrEmpty(txt_teamID.Text), 0, txt_teamID.Text)
        End Get
        Set(value As String)
            txt_teamID.Text = value
        End Set
    End Property

    Public DTM As New DataManager

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btn_import_Click(sender As Object, e As System.EventArgs) Handles btn_import.Click
        Dim alert As String = String.Empty
        Dim rowCount As Integer = 0
        If Not String.IsNullOrEmpty(file_importPartiData.FileName) Then
            Dim fileName As String = file_importPartiData.FileName
            Dim material As String() = fileName.Split(".")
            Dim extenstion As String = String.Empty

            If material.Length > 1 Then
                extenstion = material(1)
                If extenstion.ToLower = "xlsx" Then
                    alert = DTM.ImportMatchDetailAndTeamIDByXlsx(file_importPartiData.FileContent, rowCount)
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
        teamID = rowCount
        gv_matchDetail.DataBind()
    End Sub

    Private Sub btn_ExportXlsTemp_Click(sender As Object, e As System.EventArgs) Handles btn_ExportXlsTemp.Click
        Dim fName = "MatchDataByTeamTemp.xlsx"
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

    Public Function GetLocationName(ByVal locationID As Integer) As String
        Dim loName As String
        loName = DTM.GetLocationNameByID(locationID)
        Return loName
    End Function

    Private Sub gv_matchDetail_DataBinding(sender As Object, e As System.EventArgs) Handles gv_matchDetail.DataBinding
        gv_matchDetail.DataSource = DTM.GetLatestMatchDetailDataTable(teamID)
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