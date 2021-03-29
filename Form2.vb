Imports System.Net.Mail
Imports System.Configuration
Imports System.IO
Imports System.Web

Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Receiver As String = Label4.Text
        Dim User As String = Label2.Text

        Dim reader As StreamReader = New StreamReader("../../MailBody.html")
        Dim body As String = reader.ReadToEnd()
        body = body.Replace(" {{ username }} ", User)


        Dim Mail As New MailMessage
        Dim SMTP As New SmtpClient("smtp.gmail.com")

        Mail.Subject = "Security Update"
        Mail.From = New MailAddress("xxx@gmail.com")
        SMTP.Credentials = New System.Net.NetworkCredential("xxx@gmail.com", "password")

        Mail.To.Add(Receiver)
        Mail.IsBodyHtml = True
        Mail.Body = body

        SMTP.EnableSsl = True
        SMTP.Port = "587"
        SMTP.Send(Mail)
    End Sub
End Class