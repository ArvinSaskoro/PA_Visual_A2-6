Imports MySql.Data.MySqlClient

Public Class AdminForm
    Sub showkamar()
        Try
            DA = New MySqlDataAdapter("SELECT * from rooms", CONN)
            DS = New DataSet
            DS.Clear()
            DA.Fill(DS, "rooms")
            dgv3.DataSource = DS.Tables("rooms")
            dgv3.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Sub Showreservasi()
        Try
            Dim query As String = "SELECT customer.customer_id, customer.room, customer.reservation, customer.confirmed, akun.fullname " &
                                  "FROM customer " &
                                  "JOIN akun ON customer.user_id = akun.user_id " &
                                  "WHERE customer.confirmed = 1"
            DA = New MySqlDataAdapter(query, CONN)
            DS = New DataSet
            DS.Clear()
            DA.Fill(DS, "customer")
            dgv2.DataSource = DS.Tables("customer")
            dgv2.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Sub Showconfirm()
        Try
            Dim query As String = "SELECT customer.customer_id, customer.room, customer.reservation, customer.confirmed, akun.fullname " &
                                  "FROM customer " &
                                  "JOIN akun ON customer.user_id = akun.user_id " &
                                  "WHERE customer.confirmed = 0"
            DA = New MySqlDataAdapter(query, CONN)
            DS = New DataSet
            DS.Clear()
            DA.Fill(DS, "customer")
            dgv1.DataSource = DS.Tables("customer")
            dgv1.Refresh()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub DataReservStripMenuItem_Click(sender As Object, e As EventArgs) Handles DataReservToolStripMenuItem.Click
        dgv1.Visible = False
        dgv2.Visible = True
        dgv3.Visible = False
        Showreservasi()

        txtID.Visible = True
        btnConfirm.Visible = False
        btnDel.Visible = True
    End Sub

    Private Sub TransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransaksiToolStripMenuItem.Click
        dgv1.Visible = False
        dgv2.Visible = False
        dgv3.Visible = True
        showkamar()
        txtID.Visible = False
        btnConfirm.Visible = False
        btnDel.Visible = False
    End Sub

    Private Sub AdminForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Koneksi ke database
        dgv1.Visible = False
        dgv2.Visible = False
        dgv3.Visible = False
        koneksi()
        txtID.Visible = False
        btnConfirm.Visible = False
        btnDel.Visible = False
        If txtID.Text = "" Then
            txtID.Text = "Input ID..."
            txtID.ForeColor = Color.DarkGray
        End If
    End Sub

    Private Sub GantiUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GantiUserToolStripMenuItem.Click
        Me.Close()
        LoginForm.Show()
    End Sub

    Private Sub KonfirmasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KonfirmasiToolStripMenuItem.Click
        dgv1.Visible = True
        dgv2.Visible = False
        dgv3.Visible = False
        Showconfirm()
        txtID.Visible = True
        btnConfirm.Visible = True
        btnDel.Visible = False
    End Sub

    Private Sub txtID_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtID.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If

        If txtID.Text = "Search" Then
            txtID.Text = ""
            txtID.ForeColor = Color.FromArgb(132, 123, 112)
        End If

        If e.KeyChar = Chr(13) Then
            Dim searchQuery As String = "SELECT * FROM customer WHERE customer_id LIKE @customer_id"
            CMD = New MySqlCommand(searchQuery, CONN)
            CMD.Parameters.AddWithValue("@customer_id", "%" & txtID.Text & "%")

            Try
                CONN.Open()
                RD = CMD.ExecuteReader()
                If RD.HasRows Then
                    Dim foundData As New DataTable()
                    foundData.Load(RD)

                    dgv2.DataSource = foundData
                    dgv2.ReadOnly = True

                    dgv3.DataSource = foundData
                    dgv3.ReadOnly = True
                Else
                    MsgBox("Data tidak ditemukan!", MsgBoxStyle.Information, "Attention")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                RD.Close()
                CONN.Close()
            End Try
        End If
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        If txtID.Text = "" Then
            MsgBox("Fill the Customer ID !!!")
            txtID.Focus()
        Else
            CONN.Open()
            If MessageBox.Show("Konfirmasi Customer " & txtID.Text & " ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                CMD = New MySqlCommand("UPDATE customer SET confirmed = '1' WHERE customer_id = '" & txtID.Text & "'", CONN)
                CMD.ExecuteNonQuery()

                Call Showconfirm()
            Else
            End If
            CONN.Close()
        End If
    End Sub

    Private Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        If txtID.Text = "" Then
            MsgBox("Fill the Customer ID !!!")
            txtID.Focus()
        Else
            CONN.Open()
            If MessageBox.Show("Delete data " & txtID.Text & " ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                CMD = New MySqlCommand("SELECT room FROM customer WHERE customer_id = '" & txtID.Text & "'", CONN)
                CMD.ExecuteNonQuery()

                Dim kamar As Integer = CMD.ExecuteScalar()

                CMD = New MySqlCommand("UPDATE rooms SET stat = '1' WHERE room = '" & kamar & "'", CONN)
                CMD.ExecuteNonQuery()

                CMD = New MySqlCommand("DELETE FROM customer WHERE customer_id = '" & txtID.Text & "'", CONN)
                CMD.ExecuteNonQuery()

                Call Showreservasi()
            Else
            End If
            CONN.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub txtID_LostFocus(sender As Object, e As EventArgs) Handles txtID.LostFocus
        If txtID.Text = "" Then
            txtID.Text = "Input ID..."
            txtID.ForeColor = Color.DarkGray
        End If
    End Sub

    Private Sub txtID_GotFocus(sender As Object, e As EventArgs) Handles txtID.GotFocus
        If txtID.Text = "Input ID..." Then
            txtID.Text = ""
            txtID.ForeColor = Color.Black
        End If
    End Sub
End Class