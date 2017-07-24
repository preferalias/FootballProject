Imports System.Globalization
Imports System.Data.Common

Public Class DataManager

    Public Function CreateDataContext() As CoreDBDataContext
        Return New CoreDBDataContext(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnectionString").ConnectionString)
    End Function

#Region "League"
    Public Function GetAllLeagueDataTable() As IEnumerable
        Dim result As IEnumerable
        Using ctx = CreateDataContext()
            result = (From r In ctx.League_Masters).ToList
        End Using
        Return result
    End Function

    Public Sub InsertLeague(ByVal data As League_Master)
        Using ctx = CreateDataContext()
            Dim ID = (From r In ctx.League_Masters Select r.league_id).Max + 1
            data.league_id = ID
            ctx.League_Masters.InsertOnSubmit(data)
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Sub UpdateLeagueMaster(ByVal data As League_Master)
        Dim orig As League_Master
        Using ctx = CreateDataContext()
            orig = (From r In ctx.League_Masters Where r.league_id = data.league_id).SingleOrDefault
            With orig
                .league_id = data.league_id
                .league_name = data.league_name
            End With
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Sub DeleteLeagueMaster(ByVal data As League_Master)
        Using ctx = CreateDataContext()
            Dim orig As League_Master
            orig = (From r In ctx.League_Masters Where r.league_id = data.league_id).SingleOrDefault
            ctx.League_Masters.DeleteOnSubmit(orig)
            ctx.SubmitChanges()
        End Using
    End Sub
#End Region

#Region "Team"
    Public Function GetTeamByLeague(ByVal league_id As Integer) As IEnumerable
        Dim result As IEnumerable
        Dim ctx = CreateDataContext()
        result = (From r In ctx.Team_Masters Where r.league_id = league_id).ToList
        Return result
    End Function

    Public Sub InsertTeam(ByVal data As Team_Master)
        Using ctx = CreateDataContext()
            Dim ID = (From r In ctx.Team_Masters Select r.team_id).Max + 1
            data.team_id = ID
            ctx.Team_Masters.InsertOnSubmit(data)
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Sub InsertTeamList(ByVal dataList As List(Of Team_Master))
        Using ctx = CreateDataContext()
            For Each value In dataList
                Dim ID = (From r In ctx.Team_Masters Select r.team_id).Max + 1
                value.team_id = ID
                ctx.Team_Masters.InsertOnSubmit(value)
                ctx.SubmitChanges()
            Next
        End Using
    End Sub

    Public Sub UpdateTeamMaster(ByVal data As Team_Master)
        Dim orig As Team_Master
        Using ctx = CreateDataContext()
            orig = (From r In ctx.Team_Masters Where r.team_id = data.team_id).SingleOrDefault
            With orig
                .team_name = data.team_name
                .league_id = data.league_id
            End With
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Sub DeleteTeamMaster(ByVal data As Team_Master)
        Using ctx = CreateDataContext()
            Dim orig As Team_Master
            orig = (From r In ctx.Team_Masters Where r.team_id = data.team_id).SingleOrDefault
            ctx.Team_Masters.DeleteOnSubmit(orig)
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Function GetTeamNameByID(ByVal teamID As Integer) As String
        Dim result As String
        Using ctx = CreateDataContext()
            result = (From r In ctx.Team_Masters Where r.team_id = teamID Select r.team_name).SingleOrDefault
        End Using
        Return result
    End Function
#End Region

#Region "Match Program"
    Public Function GetAllMatchProgram() As List(Of Match_Program)
        Dim result As List(Of Match_Program)
        Using ctx = CreateDataContext()
            result = (From r In ctx.Match_Programs).ToList
        End Using
        Return result
    End Function

    Public Sub UpdateMatchProgram(ByVal data As Match_Program)
        Using ctx = CreateDataContext()
            Dim orig As Match_Program
            orig = (From r In ctx.Match_Programs Where r.id = data.id).SingleOrDefault
            orig.id = data.id
            orig.mp_name = data.mp_name
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Sub InsertMatchProgram(ByVal data As Match_Program)
        Using ctx = CreateDataContext()
            Dim IDorig = (From r In ctx.Match_Programs Select r.id).Max + 1
            data.id = IDorig
            ctx.Match_Programs.InsertOnSubmit(data)
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Sub DeleteMatchProgram(ByVal data As Match_Program)
        Using ctx = CreateDataContext()
            Dim orig As Match_Program
            orig = (From r In ctx.Match_Programs Where r.id = data.id).SingleOrDefault
            ctx.Match_Programs.DeleteOnSubmit(orig)
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Function GetMatchProgramNameByID(ByVal id As Integer)
        Dim result As String
        Using ctx = CreateDataContext()
            result = (From r In ctx.Match_Programs Where r.id = id Select r.mp_name).SingleOrDefault
        End Using
        Return result
    End Function
#End Region

#Region "Location"

    Public Function GetAllLocation() As List(Of Location)
        Dim result As List(Of Location)
        Using ctx = CreateDataContext()
            result = (From r In ctx.Locations).ToList
        End Using
        Return result
    End Function

    Public Sub UpdateLocation(ByVal data As Location)
        Using ctx = CreateDataContext()
            Dim orig As Location
            orig = (From r In ctx.Locations Where r.id = data.id).SingleOrDefault
            orig.id = data.id
            orig.location = data.location
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Sub InsertLocation(ByVal data As Location)
        Using ctx = CreateDataContext()
            Dim IDorig = (From r In ctx.Locations Select r.id).Max + 1
            data.id = IDorig
            ctx.Locations.InsertOnSubmit(data)
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Sub DeleteLocation(ByVal data As Location)
        Using ctx = CreateDataContext()
            Dim orig As Location
            orig = (From r In ctx.Locations Where r.id = data.id).SingleOrDefault
            ctx.Locations.DeleteOnSubmit(orig)
            ctx.SubmitChanges()
        End Using
    End Sub

    Public Function GetLocationNameByID(ByVal id As Integer)
        Dim result As String
        Using ctx = CreateDataContext()
            result = (From r In ctx.Locations Where r.id = id Select r.location).SingleOrDefault
        End Using
        Return result
    End Function
#End Region

#Region "Match Detail"
    Public Function ImportMatchDetailByXlsx(ByVal fileStream As IO.Stream, ByVal teamID As Integer) As String
        Dim reMsg As String = String.Empty
        Dim isLastRow As Boolean = False
        Dim dataList As New List(Of Match_Detail)
        Dim p As New OfficeOpenXml.ExcelPackage
        p.Load(fileStream)
        Dim sheet = p.Workbook.Worksheets.First
        'Define First Data Row
        Dim firstRow As Integer = 3
        Dim firstCol As Integer = 1
        Dim lastCol As Integer = 5
        Dim lastRow As Integer = 100
        For r = firstRow To lastRow
            Dim data As New Match_Detail
            data.team_id = teamID
            For c = firstCol To lastCol
                If sheet.Cells(r, c).Value IsNot Nothing Then
                    Try
                        Select Case c
                            Case 1
                                data.date = Date.Parse(sheet.Cells(r, c).Value, New CultureInfo("en-GB"))
                            Case 2
                                data.match_program = sheet.Cells(r, c).Value
                            Case 3
                                data.location = sheet.Cells(r, c).Value
                            Case 4
                                data.goal_positive = sheet.Cells(r, c).Value
                            Case 5
                                data.goal_negative = sheet.Cells(r, c).Value
                        End Select
                    Catch ex As Exception
                        Return "Error at row " & r & " column " & c
                    End Try
                Else
                    If c = firstCol Then
                        Dim checkLastRow As Boolean = True
                        For ct = firstCol + 1 To lastCol
                            If sheet.Cells(r, ct).Value IsNot Nothing Then
                                Return "Error at row " & r
                            End If
                        Next
                        isLastRow = checkLastRow
                        If isLastRow Then
                            Exit For
                        End If
                    Else
                        Return "Missing data at row " & r & " column " & c
                    End If
                End If
            Next
            If isLastRow Then
                Exit For
            End If
            dataList.Add(data)
        Next

        Using ctx = CreateDataContext()
            If ctx.Connection.State <> ConnectionState.Open Then ctx.Connection.Open()
            Dim tx As DbTransaction = ctx.Connection.BeginTransaction
            ctx.Transaction = tx
            Dim nextID As Integer = (From r In ctx.Match_Details Select r.id).Max + 1
            For i = 0 To dataList.Count - 1
                dataList(i).id = nextID
                ctx.Match_Details.InsertOnSubmit(dataList(i))
                Try
                    ctx.SubmitChanges()
                Catch ex As Exception
                    tx.Rollback()
                    Return "Error data foreign key value at row " & i + firstRow
                End Try
                nextID += 1
            Next
            tx.Commit()
            reMsg = "Input data success"
        End Using
        Return reMsg
    End Function

    Public Function GetMatchDetailDataTableByTeamID(ByVal teamID As Integer) As IEnumerable
        Dim result As IEnumerable
        Dim ctx = CreateDataContext()
        result = From r In ctx.Match_Details Where r.team_id = teamID Order By r.date Descending
        Return result
    End Function

    Public Function GetLatestMatchDetailDataTable(ByVal rowCount As Integer) As IEnumerable
        Dim result As IEnumerable
        Dim ctx = CreateDataContext()
        result = (From r In ctx.Match_Details Order By r.id Descending).Take(rowCount)
        Return result
    End Function

    Public Function ImportMatchDetailAndTeamIDByXlsx(ByVal fileStream As IO.Stream, ByRef affectedRow As Integer) As String
        Dim reMsg As String = String.Empty
        Dim isLastRow As Boolean = False
        Dim dataList As New List(Of Match_Detail)
        Dim p As New OfficeOpenXml.ExcelPackage
        p.Load(fileStream)
        Dim sheet = p.Workbook.Worksheets.First
        'Define First Data Row
        Dim firstRow As Integer = 3
        Dim firstCol As Integer = 1
        Dim lastCol As Integer = 6
        Dim lastRow As Integer = 100
        For r = firstRow To lastRow
            Dim data As New Match_Detail
            For c = firstCol To lastCol
                If sheet.Cells(r, c).Value IsNot Nothing Then
                    Try
                        Select Case c
                            Case 1
                                data.team_id = sheet.Cells(r, c).Value
                            Case 2
                                data.date = Date.Parse(sheet.Cells(r, c).Value, New CultureInfo("en-GB"))
                            Case 3
                                data.match_program = sheet.Cells(r, c).Value
                            Case 4
                                data.location = sheet.Cells(r, c).Value
                            Case 5
                                data.goal_positive = sheet.Cells(r, c).Value
                            Case 6
                                data.goal_negative = sheet.Cells(r, c).Value
                        End Select
                    Catch ex As Exception
                        Return "Error at row " & r & " column " & c
                    End Try
                Else
                    If c = firstCol Then
                        Dim checkLastRow As Boolean = True
                        For ct = firstCol + 1 To lastCol
                            If sheet.Cells(r, ct).Value IsNot Nothing Then
                                Return "Error at row " & r
                            End If
                        Next
                        isLastRow = checkLastRow
                        If isLastRow Then
                            Exit For
                        End If
                    Else
                        Return "Missing data at row " & r & " column " & c
                    End If
                End If
            Next
            If isLastRow Then
                Exit For
            End If
            dataList.Add(data)
        Next

        Using ctx = CreateDataContext()
            If ctx.Connection.State <> ConnectionState.Open Then ctx.Connection.Open()
            Dim tx As DbTransaction = ctx.Connection.BeginTransaction
            ctx.Transaction = tx
            Dim nextID As Integer = (From r In ctx.Match_Details Select r.id).Max + 1
            For i = 0 To dataList.Count - 1
                dataList(i).id = nextID
                ctx.Match_Details.InsertOnSubmit(dataList(i))
                Try
                    ctx.SubmitChanges()
                Catch ex As Exception
                    tx.Rollback()
                    Return "Error data foreign key value at row " & i + firstRow
                End Try
                nextID += 1
            Next
            affectedRow = dataList.Count
            tx.Commit()
            reMsg = "Input data success"
        End Using
        Return reMsg

    End Function
#End Region

#Region "Utility"
#End Region
End Class
