Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Private Sub LoginButton_Click(sender As Object, e As System.EventArgs) Handles LoginButton.Click
        If UserName.Text = "aa" And Password.Text = "123" Then
            FormsAuthentication.RedirectFromLoginPage(UserName.Text, False)
        Else
            FailureText.Text = "Invalid Username or Password"
        End If
    End Sub
End Class