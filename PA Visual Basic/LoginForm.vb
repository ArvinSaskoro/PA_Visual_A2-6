Imports System.Data.SqlClient
Imports System.Windows
Imports MySql.Data.MySqlClient

Public Class LoginForm
    Public Shared ActiveUsername As String
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        'Koneksi ke database
        koneksi()
        CONN.Open()

        'Membuat perintah SQL untuk mengambil data pengguna berdasarkan username dan password
        STR = "SELECT * FROM akun WHERE username = @username AND password = @password"
        CMD = New MySqlCommand(STR, CONN)
        CMD.Parameters.AddWithValue("@username", txtUsername.Text)
        CMD.Parameters.AddWithValue("@password", txtPassword.Text)

        Try
            'Menjalankan perintah SQL
            RD = CMD.ExecuteReader()

            If RD.HasRows Then
                'Jika data ditemukan, cek apakah pengguna merupakan admin atau user
                RD.Read()
                If RD.Item("status") = "admin" Then
                    'Jika pengguna merupakan admin, tampilkan form admin
                    AdminForm.Show()
                    AdminForm.ToolStripStatusLabel1.Text = RD.GetString(4)
                    AdminForm.ToolStripStatusLabel2.Text = RD.GetString(1)
                    AdminForm.ToolStripStatusLabel3.Text = RD.GetString(3)
                Else
                    'Jika pengguna merupakan user, tampilkan form user
                    UserForm.Show()
                    ActiveUsername = txtUsername.Text
                    Dim profileForm As New ProfilForm()
                End If

                'Sembunyikan form login
                Me.Hide()
            Else
                'Jika data tidak ditemukan, tampilkan pesan kesalahan
                MessageBox.Show("Username atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            RD.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        'Menutup koneksi ke database
        CONN.Close()
    End Sub

    Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        koneksi()
        txtUsername.Text = "Username"
        txtUsername.ForeColor = Color.DarkGray

        txtPassword.Text = "Password"
        txtPassword.ForeColor = Color.DarkGray
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Hide()
        Dim registerForm As New RegisterForm()
        registerForm.ShowDialog()
        Me.Close()
    End Sub

    Private Sub txtUsername_GotFocus(sender As Object, e As EventArgs) Handles txtUsername.GotFocus
        If txtUsername.Text = "Username" Then
            txtUsername.Text = ""
            txtUsername.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtUsername_LostFocus(sender As Object, e As EventArgs) Handles txtUsername.LostFocus
        If txtUsername.Text = "" Then
            txtUsername.Text = "Username"
            txtUsername.ForeColor = Color.DarkGray
        End If
    End Sub

    Private Sub txtPassword_GotFocus(sender As Object, e As EventArgs) Handles txtPassword.GotFocus
        If txtPassword.Text = "Password" Then
            txtPassword.Text = ""
            txtPassword.PasswordChar = "•"
            txtPassword.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtPassword_LostFocus(sender As Object, e As EventArgs) Handles txtPassword.LostFocus
        If txtPassword.Text = "" Then
            txtPassword.Text = "Password"
            txtPassword.PasswordChar = "•"
            txtPassword.ForeColor = Color.DarkGray
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If txtPassword.PasswordChar = "•" Then
            txtPassword.PasswordChar = ""
        Else
            txtPassword.PasswordChar = "•"
        End If
    End Sub


End Class


