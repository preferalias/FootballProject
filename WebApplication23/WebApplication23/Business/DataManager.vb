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

#End Region

#Region "Match Detail"
    Public Function ImportMatchDetailByXlsx(ByVal x As IO.Stream) As Boolean
        Dim result As Boolean = False
        Dim a = 5
        Return result
    End Function
#End Region

End Class
