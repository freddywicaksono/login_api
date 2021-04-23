Imports System.Text
Imports System.Xml
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.IO

Module MyMod
    Public oUsers As New Users

    Public Function CreateToken()
        Dim x As String
        Dim dateAsString = DateTime.Now.ToString("yyyy-MM-dd")
        x = getMD5Hash("UMC.AC.ID" & dateAsString)
        Return x
    End Function

    Public Function WRequest(ByVal URL As String, ByVal method As String, ByVal POSTdata As String) As String
        Dim responseData As String = ""
        Try
            Dim cookieJar As New Net.CookieContainer()
            Dim hwrequest As Net.HttpWebRequest = Net.WebRequest.Create(URL)
            hwrequest.CookieContainer = cookieJar
            hwrequest.Accept = "*/*"
            hwrequest.AllowAutoRedirect = True
            hwrequest.UserAgent = "http_requester/0.1"
            hwrequest.Timeout = 100000
            hwrequest.Method = method
            If hwrequest.Method = "POST" Then
                hwrequest.ContentType = "application/x-www-form-urlencoded"
                Dim encoding As New ASCIIEncoding() 'Use UTF8Encoding for XML requests
                Dim postByteArray() As Byte = encoding.GetBytes(POSTdata)
                hwrequest.ContentLength = postByteArray.Length
                Dim postStream As IO.Stream = hwrequest.GetRequestStream()
                postStream.Write(postByteArray, 0, postByteArray.Length)
                postStream.Close()
            End If
            Dim hwresponse As Net.HttpWebResponse = hwrequest.GetResponse()
            If hwresponse.StatusCode = Net.HttpStatusCode.OK Then
                Dim responseStream As IO.StreamReader = _
                  New IO.StreamReader(hwresponse.GetResponseStream())
                responseData = responseStream.ReadToEnd()
            End If
            hwresponse.Close()
        Catch e As Exception
            responseData = "An error occurred: " & e.Message
        End Try
        Return responseData
    End Function

    Public Function getMD5Hash(ByVal strToHash As String) As String

        Dim md5Obj As New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)
        bytesToHash = md5Obj.ComputeHash(bytesToHash)
        Dim strResult As String = ""
        Dim b As Byte
        For Each b In bytesToHash
            strResult += b.ToString("x2")
        Next
        Return strResult
    End Function
End Module
