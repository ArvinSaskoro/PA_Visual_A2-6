Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class ProfilForm

    Private Sub ProfilForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim username As String = LoginForm.ActiveUsername
        Dim query As String = "SELECT * FROM akun WHERE username = @username"

        ' Menggunakan parameterized query untuk mencegah serangan SQL Injection
        Dim parameters As New List(Of MySqlParameter)()
        parameters.Add(New MySqlParameter("@username", username))

        ' Mengeksekusi query menggunakan fungsi ExecuteReader dari modul DatabaseHelper
        Dim reader As MySqlDataReader = ExecuteReader(query, parameters)

        If reader.Read() Then
            Dim fullName As String = reader.GetString("fullname")
            Dim email As String = reader.GetString("email")
            Dim status As String = reader.GetString("status")

            lblFullName.Text = fullName
            lblEmail.Text = email
            lblStatus.Text = status
        End If
        lblFullName2.Text = LoginForm.txtUsername.Text
        reader.Close()
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub ChangeUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeUserToolStripMenuItem.Click
        Me.Hide()
        LoginForm.Show()
    End Sub

    Private Sub TentangKamiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TentangKamiToolStripMenuItem.Click
        Me.Hide()
        AboutUsForm.Show()
    End Sub

    Private Sub TransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransaksiToolStripMenuItem.Click
        Me.Hide()
        TransaksiForm.Show()
    End Sub

    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        Me.Hide()
        UserForm.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub
End Class