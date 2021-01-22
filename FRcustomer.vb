Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Public Class FRcustomer
    Private Sub FRcustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

            Dim fechacalendarioentrega As String
            fechacalendarioentrega = frmindex.ssdeliverdate.Value.ToString("yyyy/MM/dd")
            '

            conexionMysql.Open()
            Dim Sql55 As String
            Dim ds As DataSet
            Sql55 = "select * from customer;"

            Dim cmd55 As New MySqlCommand(Sql55, conexionMysql)
            'reader = cmd55.ExecuteReader()
            'reader.Read()
            'Dim dt2 As New DataTable
            Dim da As New MySqlDataAdapter(cmd55)
            'cargamos el formulario  resumen
            'da.Fill(dt)
            ds = New DataSet()
            da.Fill(ds)
            'conexionMysql.Close()

            'idtipoproducto = reader.GetString(0).ToString

            conexionMysql.Close()
            ''-----------------------------------




            '-----------------------------------
            'DATOS DE LA EMPRESA
            conexionMysql.Open()
            Dim ds2 As DataSet
            Dim Sql2 As String
            Sql2 = "select nombre,calle_numero,colonia_ciudad,cp,estado,telefono,whatsapp,correo, fanpage,sitio_web,director,horario,giro,saludo_nota,rfc,saludo_ticket,saludo2,saludo3,saludo4,saludo5  from datos_empresa;"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            Dim dt2 As New DataTable
            Dim da2 As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            'da.Fill(dt)
            ds2 = New DataSet()
            da2.Fill(ds2)
            conexionMysql.Close()

            '.-----------------------------------
            conexionMysql.Open()
            Dim ds23 As DataSet
            Dim Sql23 As String
            Sql23 = "select logo from logo_empresa"
            Dim cmd23 As New MySqlCommand(Sql23, conexionMysql)
            Dim dt23 As New DataTable
            Dim da23 As New MySqlDataAdapter(cmd23)
            'cargamos el formulario  resumen
            'da.Fill(dt)
            ds23 = New DataSet()
            da23.Fill(ds23)
            conexionMysql.Close()

            '------------------------------------

            Dim rds As New ReportDataSource("dtcustomer", ds.Tables(0))
            Dim rds2 As New ReportDataSource("dtdatos_empresa", ds2.Tables(0))
            Dim rds3 As New ReportDataSource("dtlogo", ds23.Tables(0))

            ReportViewer1.LocalReport.DataSources.Clear()              'limpio la fuente de datos
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.DataSources.Add(rds3)


            Me.ReportViewer1.RefreshReport()
        Catch ex As Exception
            MsgBox("No haz seleccionado ningun valor", MsgBoxStyle.Information, "MOBI")
            Me.Close()
        End Try

        Me.ReportViewer1.RefreshReport()
    End Sub

End Class