Imports System.Windows
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class TransaksiForm
    Private Sub TransaksiForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtp1.MinDate = DateTime.Now.Date
        UpdateComboKamar()
        Pict1.SizeMode = PictureBoxSizeMode.StretchImage
        lblFullName2.Text = LoginForm.txtUsername.Text
    End Sub

    Private Sub UpdateComboKamar()
        CONN.Close()
        cbKamar.Items.Clear()

        ' Query untuk mengambil daftar kamar yang tersedia
        Dim query As String = "SELECT * FROM rooms WHERE stat = '1'"
        Dim table As New DataTable()

        DA = New MySqlDataAdapter(query, CONN)
        DA.Fill(table)

        For Each row As DataRow In table.Rows
            Dim roomnumber As String = row.Item("room").ToString()
            Dim roomtype As String = row.Item("type").ToString()
            cbKamar.Items.Add($"{roomnumber} - {roomtype}")
        Next
    End Sub

    Private Sub btnPesan_Click(sender As Object, e As EventArgs) Handles btnPesan.Click
        Dim user_id As Integer

        CONN.Open()
        user_id = Convert.ToInt32(CMD.ExecuteScalar())
        If cbKamar.Text = "" Or dtp1.Text = "" Then
            MessageBox.Show("Please fill all fields")
        Else
            Dim tanggal As String = dtp1.Text
            Dim query As String = "INSERT INTO customer (user_id, room, reservation, confirmed) VALUES (@user_id, @room, @reservation, @confirmed)"

            Dim command As New MySqlCommand(query, CONN)
            command.Parameters.AddWithValue("@user_id", user_id)
            command.Parameters.AddWithValue("@room", cbKamar.Text)
            command.Parameters.AddWithValue("@reservation", tanggal)
            command.Parameters.AddWithValue("@confirmed", 0)
            command.ExecuteNonQuery()



            Dim kamar = cbKamar.Text.Split(" - ")(0)
            Dim update As String = "UPDATE rooms SET stat = '0' WHERE room = '" & kamar & "'"
            CMD = New MySqlCommand(update, CONN)
            CMD.ExecuteNonQuery()

            ' Menampilkan pesan sukses
            MessageBox.Show("Registration successful!")

            ' Mengosongkan combo box dan memperbarui isinya
            cbKamar.Text = ""
            UpdateComboKamar()
        End If
        CONN.Close()
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) 
        Me.Hide()
        LoginForm.Show()
    End Sub

    Private Sub cbKamar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKamar.SelectedIndexChanged

        Dim selectedRoom As String = cbKamar.SelectedItem.ToString()
        Dim roomType As String = selectedRoom.Split("-"c)(1).Trim()

        If roomType = "Suite" Then
            Pict1.Image = Image.FromFile("D:\Akuliah\SEMESTER4\praktikum_PV\AAA Project Akhir\PA Visual Basic Errorhandling\PA Visual Basic\images\suite.jpg")
        ElseIf roomType = "Exclusive" Then
            Pict1.Image = Image.FromFile("D:\Akuliah\SEMESTER4\praktikum_PV\AAA Project Akhir\PA Visual Basic Errorhandling\PA Visual Basic\images\exclusive.jpg")
        Else
            Pict1.Image = Nothing
        End If
    End Sub

    Private Sub KeluarToolStripMenuItem_Click(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub ChangeUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeUserToolStripMenuItem.Click
        Me.Hide()
        LoginForm.Show()
    End Sub


    Private Sub HomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HomeToolStripMenuItem.Click
        Me.Hide()
        UserForm.Show()
    End Sub

    Private Sub ProfilToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfilToolStripMenuItem.Click
        Me.Hide()
        ProfilForm.Show()
    End Sub

    Private Sub TentangKamiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TentangKamiToolStripMenuItem.Click
        Me.Hide()
        AboutUsForm.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles Pict1.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub
End Class