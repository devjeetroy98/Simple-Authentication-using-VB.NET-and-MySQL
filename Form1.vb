Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Configuration
Imports System.Security.Cryptography
Public Class Form1
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub TabPage4_Click(sender As Object, e As EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Email As String = TextBox5.Text
        Dim Password As String = TextBox6.Text
        Dim Actualpassword As String = Encrypt(Password)
        TextBox5.Text = ""
        TextBox6.Text = ""
        Conn.Open()
        Try
            Dim query As String = "SELECT username, email from vbnet_auth WHERE email = '" + Email + "' AND password = '" + Encrypt(Password) + "'"
            Dim Mysc As New MySqlCommand(query, Conn)
            Dim data As MySqlDataReader
            data = Mysc.ExecuteReader()
            data.Read()
            Form2.Label2.Text = data.GetString(0)
            Form2.Label4.Text = data.GetString(1)
            Form2.Show()
            Me.Hide()

            Conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            Conn.Close()
        End Try

    End Sub
    Private Function Encrypt(clearText As String) As String
        Dim EncryptionKey As String = "3b241101-e2bb-4255-8caf-4136c566a962"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using
                clearText = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using
        Return clearText
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim User As String = TextBox1.Text
        Dim Email As String = TextBox2.Text
        Dim Password As String = TextBox3.Text
        Dim CPassword As String = TextBox4.Text
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Dim result As Boolean = String.Compare(Password, CPassword)
        Try
            Dim Str As String = "INSERT INTO vbnet_auth (username, email, password) VALUES ('" + User + "','" + Email + "','" + Encrypt(Password) + "')"
            Conn.Open()
            Dim Mysc As New MySqlCommand(Str, Conn)
            If (String.Compare(Password, CPassword)) Then
                MsgBox("Password and Confirm Password does not match!", MsgBoxStyle.Exclamation)
                Conn.Close()
            Else
                Dim Find As String = "SELECT email FROM vbnet_auth WHERE email = '" + Email + "'"
                Dim Mysc2 As New MySqlCommand(Find, Conn)
                Dim data As String
                data = Mysc2.ExecuteScalar()
                If (data = Email) Then
                    MsgBox("The user is already registered!", MsgBoxStyle.Exclamation)
                    Conn.Close()
                Else
                    Mysc.ExecuteNonQuery()
                    MsgBox("User registered successfully!", MsgBoxStyle.Information)
                    Conn.Close()
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Conn.Close()
        End Try
    End Sub
End Class
