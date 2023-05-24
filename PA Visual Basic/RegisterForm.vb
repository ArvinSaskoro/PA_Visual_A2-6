Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports MySql.Data.MySqlClient

Public Class RegisterForm
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles txtusername.TextChanged

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CONN.Open()
        If txtusername.Text = "" Or txtpassword.Text = "" Or txtConfirmPassword.Text = "" Or txtemail.Text = "" Or txtfullname.Text = "" Then
            MessageBox.Show("Please fill all fields")
        ElseIf txtpassword.Text <> txtConfirmPassword.Text Then
            MessageBox.Show("Password and confirm password do not match")
        Else
            Dim query As String = "INSERT INTO akun (username, password, email, fullname, status) VALUES (@username, @password, @email, @fullname, @status)"
            Dim cmd As New MySqlCommand(query, CONN)
            cmd.Parameters.AddWithValue("@username", txtusername.Text)
            cmd.Parameters.AddWithValue("@password", txtpassword.Text)
            cmd.Parameters.AddWithValue("@email", txtemail.Text)
            cmd.Parameters.AddWithValue("@fullname", txtfullname.Text)
            cmd.Parameters.AddWithValue("@status", "customer")
            Try
                cmd.ExecuteNonQuery()
                MessageBox.Show("Registration successful!")
                txtusername.Clear()
                txtpassword.Clear()
                txtConfirmPassword.Clear()
                txtemail.Clear()
                txtfullname.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        CONN.Close()
    End Sub

    Private Sub RegisterForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        txtfullname.Focus()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        Dim LoginForm As New LoginForm()
        LoginForm.ShowDialog()
        Me.Close()
    End Sub

End Class