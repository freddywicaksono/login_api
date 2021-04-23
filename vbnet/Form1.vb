Public Class Form1

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        LoginProcess()
    End Sub

    Private Sub LoginProcess()
        oUsers.Token = CreateToken()
        oUsers.Username = txtUsername.Text
        oUsers.Password = txtPassword.Text
        oUsers.APILink = "http://localhost/academic/api/users/login.php"
        oUsers.Method = "POST"
        oUsers.Login()
        If (oUsers.RecordFound = True) Then
            MessageBox.Show("Selamat Datang")
            If (oUsers.Level = "ADMIN") Then
                FormAdmin.Show()
                Me.Hide()
            ElseIf (oUsers.Level = "MAHASISWA") Then
                FormMahasiswa.Show()
                Me.Hide()
            ElseIf (oUsers.Level = "DOSEN") Then
                FormDosen.Show()
                Me.Hide()
            Else
                MessageBox.Show("Level tidak dikenal")
            End If
        Else
            MessageBox.Show("Login Not Valid")
        End If
    End Sub
End Class
