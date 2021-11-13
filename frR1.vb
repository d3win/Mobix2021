Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WinForms
Public Class frR1
    Private Sub FrR1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '' MsgBox(frmindex.lbfolio.Text)
        '' MsgBox(frmindex.indexidusuario)
        ''-----------------------------------
        ' Try

        Dim fechacalendarioentrega As String
            fechacalendarioentrega = frmindex.ssdeliverdate.Value.ToString("yyyy/MM/dd")
        '
        cerrarconexion()
        conexionMysql.Open()
            Dim Sql55 As String
            Dim ds As DataSet
            ' Sql55 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.status='" & frmindex.scbstate.Text & "';"

            If frmindex.restatus = True Then


            'Sql55 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.status='" & frmindex.scbstate.Text & "';"
            'Sql55 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.status='" & frmindex.scbstate.Text & "';"
            Sql55 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.status='" & frmindex.scbstate.Text & "';"

        ElseIf frmindex.redeliverdate = True Then
            'Select Case venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes,equipo.idequipo
            'Sql2 = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.fechaentrega='" & fechacalendarioentrega & "';"
            'Sql55 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.fechaentrega='" & fechacalendarioentrega & "';"

            Sql55 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.datedelivery='" & fechacalendarioentrega & "' and equipo.status='Delivered';"


        ElseIf frmindex.rechekout = True Then

            'Sql55 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.fechaventa='" & fechacalendarioentrega & "';"
            Sql55 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.fechaventa='" & fechacalendarioentrega & "';"
            '            customer.name Like '%" & sscustomername.Text & "%';
        ElseIf frmindex.recustomer = True Then
            'en customer
            'Sql55 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.name Like '%" & frmindex.sscustomername.Text & "%';"
            Sql55 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.name like '%" & frmindex.sscustomername.Text & "%';"

        ElseIf frmindex.rephone = True Then
            'Sql55 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.telephone Like '%" & frmindex.stxtphonenumber.Text & "%';"
            Sql55 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.telephone like '%" & frmindex.stxtphonenumber.Text & "%';"

        ElseIf frmindex.remodelo = True Then
            'Sql55 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.modelo like '%" & frmindex.stxtmodel.Text & "%';"
            Sql55 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.modelo like '%" & frmindex.stxtmodel.Text & "%';"
        ElseIf frmindex.reorder = True Then
            'Sql55 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.idventa='" & frmindex.stxtorder.Text & "';"
            Sql55 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.idventa='" & frmindex.stxtorder.Text & "';"
        End If

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


        Dim rds2 As New ReportDataSource("dtdatos_empresa", ds2.Tables(0))
        Dim rds3 As New ReportDataSource("dtlogo", ds23.Tables(0))
        Dim rds As New ReportDataSource("dtstatus", ds.Tables(0))

        ReportViewer1.LocalReport.DataSources.Clear()              'limpio la fuente de datos
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.DataSources.Add(rds3)


            Me.ReportViewer1.RefreshReport()
        ' Catch ex As Exception
        ' MsgBox("No haz seleccionado ningun valor", MsgBoxStyle.Information, "MOBI")
        'Me.Close()
        '  End Try

    End Sub
End Class