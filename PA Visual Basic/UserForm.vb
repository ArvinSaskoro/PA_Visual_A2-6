Imports MySql.Data.MySqlClient

Public Class UserForm

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub ChangeUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeUserToolStripMenuItem.Click
        Me.Hide()
        LoginForm.Show()
    End Sub

    Private Sub ProfilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfilToolStripMenuItem.Click
        Me.Hide()
        ProfilForm.Show()
    End Sub

    Private Sub TentangKamiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TentangKamiToolStripMenuItem.Click
        Me.Hide()
        AboutUsForm.Show()
    End Sub

    Private Sub TransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransaksiToolStripMenuItem.Click
        Me.Hide()
        TransaksiForm.Show()
    End Sub

    Private Sub UserForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblFullName2.Text = LoginForm.txtUsername.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

End Class