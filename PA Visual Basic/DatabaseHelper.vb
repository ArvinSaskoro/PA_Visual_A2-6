Imports MySql.Data.MySqlClient

Module DatabaseHelper
    Public CONN As MySqlConnection
    Public CMD As MySqlCommand
    Public RD As MySqlDataReader
    Public DA As MySqlDataAdapter
    Public DS As DataSet
    Public STR As String

    Sub koneksi()
        STR = "server=localhost; userid=root; password=;database=db_pa_vb;Convert Zero Datetime=True"
        CONN = New MySqlConnection(STR)
    End Sub

    Function ExecuteNonQuery(query As String, parameters As List(Of MySqlParameter)) As Integer 'Fungsi ExecuteNonQuery menerima dua parameter yaitu query SQL dan daftar parameter. Fungsi ini akan mengembalikan nilai integer yang menunjukkan jumlah baris yang berhasil dimodifikasi.
        Dim result As Integer = 0
        CMD = New MySqlCommand(query, CONN)
        CMD.Parameters.AddRange(parameters.ToArray())

        Try
            If CONN.State = ConnectionState.Closed Then
                CONN.Open()
            End If

            result = CMD.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If CONN.State = ConnectionState.Open Then
                CONN.Close()
            End If
        End Try

        Return result
    End Function

    Function ExecuteReader(query As String, parameters As List(Of MySqlParameter)) As MySqlDataReader 'Fungsi ExecuteReader juga menerima dua parameter yaitu query SQL dan daftar parameter. Fungsi ini akan mengembalikan sebuah objek MySqlDataReader yang berisi hasil dari query yang dijalankan.
        CMD = New MySqlCommand(query, CONN)
        CMD.Parameters.AddRange(parameters.ToArray())

        Try
            If CONN.State = ConnectionState.Closed Then
                CONN.Open()
            End If

            RD = CMD.ExecuteReader()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return RD
    End Function
End Module' blok Try-Catch-Finally untuk menangani kesalahan koneksi atau kesalahan query. Kita juga menutup koneksi database pada blok Finally agar sumber daya yang digunakan dapat dibebaskan.