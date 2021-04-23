Imports System.Text
Imports System.Xml
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.IO

Public Class Users
    Public RecordFound As Boolean = False
    Public InsertData As Boolean = False
    Public UpdateData As Boolean = False
    Public DeleteData As Boolean = False
    Private _token As String
    Private _apilink As String
    Private _method As String
    Private _response As String
    Private _iduser As String
    Private _username As String
    Private _email As String
    Private _password As String
    Private _level As String

    Public Property Iduser()
        Get
            Return _iduser
        End Get
        Set(ByVal value)
            _iduser = value
        End Set
    End Property


    Public Property Username()
        Get
            Return _username
        End Get
        Set(ByVal value)
            _username = value
        End Set
    End Property


    Public Property Email()
        Get
            Return _email
        End Get
        Set(ByVal value)
            _email = value
        End Set
    End Property


    Public Property Password()
        Get
            Return _password
        End Get
        Set(ByVal value)
            _password = value
        End Set
    End Property


    Public Property Level()
        Get
            Return _level
        End Get
        Set(ByVal value)
            _level = value
        End Set
    End Property


    Public Property Token()
        Get
            Return _token
        End Get
        Set(ByVal value)
            _token = value
        End Set
    End Property
    Public Property APILink()
        Get
            Return _apilink
        End Get
        Set(ByVal value)
            _apilink = value
        End Set
    End Property
    Public Property Method()
        Get
            Return _method
        End Get
        Set(ByVal value)
            _method = value
        End Set
    End Property
    Public Property Response()
        Get
            Return _response
        End Get
        Set(ByVal value)
            _response = value
        End Set
    End Property

    Public Sub Simpan()
        Dim postdata As String
        Dim wstatus As String

        postdata = "token=" & _token & "&iduser=" & _iduser & "&username=" & _username & "&email=" & _email & "&password=" & _password & "&level=" & _level
        _response = WRequest(_apilink, _method, postdata)
        Dim jsonResulttodict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(_response)
        wstatus = jsonResulttodict.Item("status")
        If (wstatus = "success") Then
            InsertData = True
        Else
            InsertData = False
        End If
    End Sub


    Public Sub UpdateRecord(ByVal siduser As String)
        Dim postdata As String
        Dim wstatus As String

        postdata = "token=" & _token & "&iduser=" & _iduser & "&username=" & _username & "&email=" & _email & "&password=" & _password & "&level=" & _level
        _response = WRequest(_apilink, _method, postdata)
        Dim jsonResulttodict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(_response)
        wstatus = jsonResulttodict.Item("status")
        If (wstatus = "success") Then
            UpdateData = True
        Else
            UpdateData = False
        End If
    End Sub


    Public Sub CariUsers(ByVal siduser As String)
        Dim postdata As String
        Dim wstatus As String
        postdata = "token=" & _token & "&iduser=" & siduser

        _response = WRequest(_apilink, _method, postdata)
        Dim jsonResulttodict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(_response)
        wstatus = jsonResulttodict.Item("status")
        If (wstatus = "success") Then
            _username = jsonResulttodict.Item("username")
            _email = jsonResulttodict.Item("email")
            _password = jsonResulttodict.Item("password")
            _level = jsonResulttodict.Item("level")

            RecordFound = True
        Else
            RecordFound = False
        End If

    End Sub


    Public Sub DeleteRecord(ByVal siduser As String)
        Dim postdata As String
        Dim wstatus As String
        postdata = "token=" & _token & "&iduser=" & siduser

        _response = WRequest(_apilink, _method, postdata)
        Dim jsonResulttodict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(_response)
        wstatus = jsonResulttodict.Item("status")
        If (wstatus = "success") Then
            DeleteData = True

        Else
            DeleteData = False

        End If
    End Sub

    Public Sub getAllData(ByVal DS As DataSet, ByVal DG As DataGridView, ByVal TagRoot As String)
        Dim postdata As String
        postdata = "token=" & _token
        _response = WRequest(_apilink, _method, postdata)
        Dim srXmlData As New System.IO.StringReader(_response)
        DS.Clear()
        DS.ReadXml(srXmlData)

        DG.DataSource = DS
        DG.DataMember = TagRoot
        DG.Refresh()
    End Sub

    Public Sub Login()
        Dim postdata As String
        Dim wstatus As String
        postdata = "token=" & _token & "&username=" & _username & "&password=" & _password

        _response = WRequest(_apilink, _method, postdata)
        Dim jsonResulttodict = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(_response)
        wstatus = jsonResulttodict.Item("status")
        If (wstatus = "success") Then
            _username = jsonResulttodict.Item("username")
            _email = jsonResulttodict.Item("email")
            _password = jsonResulttodict.Item("password")
            _level = jsonResulttodict.Item("level")

            RecordFound = True
        Else
            RecordFound = False
        End If

    End Sub
End Class
