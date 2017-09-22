Imports System.IO

Public Class Form1

    Dim region As String = ""
    Dim language As String = ""




    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("BR")
        ComboBox1.Items.Add("EUNE")
        ComboBox1.Items.Add("EUW")
        ComboBox1.Items.Add("JP")
        ComboBox1.Items.Add("LA1")
        ComboBox1.Items.Add("LA2")
        ComboBox1.Items.Add("NA")
        ComboBox1.Items.Add("OC1")
        ComboBox1.Items.Add("RU")
        ComboBox1.Items.Add("TR")
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList

        ComboBox2.Items.Add("en_US")
        ComboBox2.Items.Add("pt_BR")
        ComboBox2.Items.Add("tr_TR")
        ComboBox2.Items.Add("en_GB")
        ComboBox2.Items.Add("es_ES")
        ComboBox2.Items.Add("fr_FR")
        ComboBox2.Items.Add("it_IT")
        ComboBox2.Items.Add("cs_CZ")
        ComboBox2.Items.Add("el_GR")
        ComboBox2.Items.Add("hu_HU")
        ComboBox2.Items.Add("pl_PL")
        ComboBox2.Items.Add("ro_RO")
        ComboBox2.Items.Add("ru_RU")
        ComboBox2.Items.Add("es_MX")
        ComboBox2.Items.Add("en_AU")
        ComboBox2.Items.Add("ja_JP")
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList

        My.Settings.txtpath = ""
        TextBox1.Text = My.Settings.filepath
        st()
        check() ' if correct path 


    End Sub

    Sub check()
        Try

            For Each line As String In IO.File.ReadAllLines(My.Settings.txtpath)
                If line.Contains("region: ") Then
                    region = line.Split("""")(1)
                    ComboBox1.SelectedItem = region
                End If
                If line.Contains("locale: ") Then
                    language = line.Split("""")(1)
                    ComboBox2.SelectedItem = language
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        Try
            My.Computer.FileSystem.WriteAllText(My.Settings.txtpath, My.Computer.FileSystem.ReadAllText(My.Settings.txtpath).Replace(region, ComboBox1.SelectedItem), False)
            region = ComboBox1.SelectedItem
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        Try
            My.Computer.FileSystem.WriteAllText(My.Settings.txtpath, My.Computer.FileSystem.ReadAllText(My.Settings.txtpath).Replace(language, ComboBox2.SelectedItem), False)
            language = ComboBox2.SelectedItem
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Process.Start(TextBox1.Text)
            Me.Close()
        Catch ex As Exception
            MsgBox("Probably the file path is wrong!", MsgBoxStyle.Critical)
        End Try



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim openFileDialog1 As New OpenFileDialog()

        If openFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK AndAlso openFileDialog1.FileName <> "" Then
            My.Settings.filepath = System.IO.Path.GetFullPath(openFileDialog1.FileName)
            TextBox1.Text = My.Settings.filepath
        End If

        st()

        check()

    End Sub

    Sub st()
        Dim A As String() = Split(TextBox1.Text, "\")
        Dim pa As String = ""
        For Each k As String In A
            If k <> "LeagueClient.exe" Then
                pa = pa + k + "\"
            End If

        Next
        My.Settings.txtpath = pa + "Config\LeagueClientSettings.yaml"

    End Sub


End Class
