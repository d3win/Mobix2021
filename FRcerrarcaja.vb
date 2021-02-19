Imports MySql.Data.MySqlClient
Public Class FRcerrarcaja


    Private Sub FRcerrarcaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbmensaje.Visible = False

        desglosecorte()
    End Sub
    Function cerrarconexion()
        If conexionMysql.State = ConnectionState.Open Then
            conexionMysql.Close()

        End If
    End Function
    Function desglosecorte()

        'Try

        'todos los datos son obtenidos con la fecha actual para evitar conflictos
        Dim dia, mes, año, fecha, horacaja, fechacaja As String
        Dim hora2, minuto, segundo, hora, compras, anticipo As String
        ' Dim fechacaja As Date
        hora2 = Now.Hour()
        minuto = Now.Minute()
        segundo = Now.Second()

        hora = hora2 & ":" & minuto & ":" & segundo

        dia = Date.Now.Day
        mes = Date.Now.Month
        año = Date.Now.Year
        fecha = año & "-" & mes & "-" & dia






        'CONSULTA PARA TODOS LOS PRODUCTOS EXTRAS, PAPELERIA Y SERVICIOS

        'consulto el id maximo mas actual y obtengo la fecha para saber que id se quedo con caja abierta
        '------------------------------------------------------------------
        Dim id As Integer
        cerrarconexion()
        conexionMysql.Open()
        Dim sql1 As String
        sql1 = "select max(idcaja) from caja where estado=0;"
        Dim cmd1 As New MySqlCommand(sql1, conexionMysql)
        reader = cmd1.ExecuteReader
        reader.Read()
        'id del registro abierto para cerrarlo
        id = reader.GetString(0).ToString()
        txtid.Text = id
        conexionMysql.Close()
        '------------------------------------------------------------------
        'consulto la hora inicial y la fecha en que se abrio la caja, esto significa que  desde ese momento vamos a comenzar a contar lo vendido
        '------------------------------------------------------------------
        cerrarconexion()
        conexionMysql.Open()
        Dim sql13 As String
        sql13 = "select DATE_FORMAT(fecha, '%Y-%m-%d')as fecha, hora_inicial,monto_inicial   from caja where idcaja=" & id & ";"
        Dim cmd13 As New MySqlCommand(sql13, conexionMysql)
        reader = cmd13.ExecuteReader
        reader.Read()
        'fecha que se abrio la caja
        fechacaja = reader.GetString(0).ToString()
        'hora en que se abrio la caja
        horacaja = reader.GetString(1).ToString()
        txtsaldoinicial.Text = reader.GetString(2).ToString()
        conexionMysql.Close()
        'MsgBox(fechacaja & " hora: " & horacaja)

        cerrarconexion()


        '---------------------------------------------------------
        '----------------------------------------------------'
        'comprobar si el cierre de caja se hizo en el mismo dia, o se hace ne otro dia diferente. 
        '---------------------------------------------------
        '-----------------------------------------------------





        Try

            conexionMysql.Open()
            Dim Sql2b As String
            '------PRIMERO SUMAMOS LAS VENTAS DE LOS SERVICIOS
            Sql2b = "select sum(deposito)as suma  from venta where fechaventa>='" & fechacaja & "' and hora>='" & horacaja & "';"
            'Sql2 = "select sum(total)as total from venta where fecha='" & fecha & "';"
            Dim cmd2b As New MySqlCommand(Sql2b, conexionMysql)
            reader = cmd2b.ExecuteReader
            reader.Read()

            txtanticipos.Text = reader.GetString(0).ToString()
            'MsgBox(anticipo)
            'MsgBox(reader.GetString(0).ToString())
            conexionMysql.Close()
            cerrarconexion()
        Catch ex As Exception
            txtanticipos.Text = 0
        End Try


        '.--------------------------------------------------------
        'COMPROBACION DE LAS COMPRAS REALIZADAS CUANDO SE ABRIO LA CAJA Y LA FECHA
        '----------------------------------------------------------
        Try
            cerrarconexion()

            conexionMysql.Open()
            Dim sql1a As String
            sql1a = "select sum(total)  from compra where fecha>='" & fechacaja & "' and hora>='" & horacaja & "';"
            Dim cmd1a As New MySqlCommand(sql1a, conexionMysql)
            reader = cmd1a.ExecuteReader
            reader.Read()
            txtcompras.Text = reader.GetString(0).ToString()
            conexionMysql.Close()
            cerrarconexion()
        Catch ex As Exception
            'MsgBox("Aun no hay compras realizadas", MsgBoxStyle.Information, "Sistema")
            txtcompras.Text = 0
            cerrarconexion()
        End Try

        '------------------------------------------------------------
        ' MsgBox(fechacaja)
        'MsgBox(horacaja)
        '.--------------------------------------------------------
        'COMPROBACION DE LAS COMPRAS de mercancia
        '----------------------------------------------------------
        Try
            cerrarconexion()

            conexionMysql.Open()
            Dim sql1ab As String
            sql1ab = "select sum(totalcompra)  from compramercancia where fecha>='" & fechacaja & "' and hora>='" & horacaja & "';"
            Dim cmd1ab As New MySqlCommand(sql1ab, conexionMysql)
            reader = cmd1ab.ExecuteReader
            reader.Read()
            txtcompramercancia.Text = reader.GetString(0).ToString()
            conexionMysql.Close()
            cerrarconexion()
        Catch ex As Exception
            'MsgBox("Aun no hay compras realizadas", MsgBoxStyle.Information, "Sistema")
            txtcompramercancia.Text = 0
            cerrarconexion()
        End Try

        '------------------------------------------------------------


        'verificamos que la fecha que se consulto sea la fecha actual, 
        'posiblemente la caja esta abierta desde el día de ayer, entonces cerramos la caja anterior


        '------------------------
        'If fecha = fechacaja Then
        'Else
        '    'en caso de que sean diferentes, vamos a cerrar la caja anterior.
        '    MsgBox("Existe un registro de caja abierta del dia " & fechacaja & ", para continuar, primero cierra la caja anterior")

        'End If


        '------------------------------------------------------------------
        Dim min, max As Integer
        '----------------------------------------------------------------
        'CORRESPONDE AL RANGO DE ID DE VENTA QUE SE HAN HECHO
        Try

            cerrarconexion()
            conexionMysql.Open()
            Dim sql22 As String
            sql22 = "select min(idventa)as minimo, max(idventa) as maximo from venta where fecha>='" & fechacaja & "' and hora >= '" & horacaja & "';"
            Dim cmd22 As New MySqlCommand(sql22, conexionMysql)
            reader = cmd22.ExecuteReader
            reader.Read()
            min = reader.GetString(0).ToString()
            max = reader.GetString(1).ToString()

            conexionMysql.Close()
            '-----------------------------------------------------------------


            'MsgBox("min:" & min & "max:" & max)

        Catch ex As Exception
            min = 0
            max = 0
            cerrarconexion()
        End Try


        '-----------------CONSULTAMOS SI EN EL CORTE SE HACE DE MAS DE 1 USUARIO, YA QUE UNO DE ELLOS
        '-----------------PUDO NO HABER HECHO SU CORTE, ENTONCES LE INDICAMOS AL USUARIO QUE SE HACE DE DOS
        Dim cantidad_total_productos As String


        If min = 0 And max = 0 Then

            'txtsaldogenerado.Text = 0
            txttotalproductos.Text = 0
        Else






            '--------------------------------------------------------------------
            'SUMA DE LAS VENTAS DE VENTAS RAPIDAS
            '-----------------------------------------------------------------------
            Try

                cerrarconexion()
                Dim Sqla1 As String
                'Dim totalcorteventa As String
                conexionMysql.Open()
                'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
                Sqla1 = "select sum(total)as suma from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and tipoventa=1;"
                'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
                Dim cmda1 As New MySqlCommand(Sqla1, conexionMysql)
                reader = cmda1.ExecuteReader()
                reader.Read()
                'Try
                txtventasproductos.Text = reader.GetString(0).ToString
                'txtventasefectivo.Text = reader.GetString(0).ToString

                'txttotalproductos.Text = reader.GetString(1).ToString
            Catch ex As Exception
                cerrarconexion()
                txtventasproductos.Text = 0
                'txtventasefectivo.Text = 0

            End Try



            '--------------------------------------------------------------------
            'SUMA DE LAS VENTAS DE VENTAS efectivo
            '-----------------------------------------------------------------------
            Try

                cerrarconexion()
                Dim Sqla12 As String
                Dim totalcorteventa As String
                conexionMysql.Open()
                'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
                Sqla12 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and idtipo_pago=1;"
                'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
                Dim cmda12 As New MySqlCommand(Sqla12, conexionMysql)
                reader = cmda12.ExecuteReader()
                reader.Read()
                'Try
                'txtventasproductos.Text = reader.GetString(0).ToString
                txtventasefectivo.Text = reader.GetString(0).ToString
                MsgBox("echo")
                'txttotalproductos.Text = reader.GetString(1).ToString
            Catch ex As Exception
                cerrarconexion()
                'txtventasproductos.Text = 0
                txtventasefectivo.Text = 0

            End Try


            '-------------------------------------------------------------------
            'SUMA De ventas por transferencias
            '---------------------------------------------------------------------------

            Try

                cerrarconexion()
                Dim Sqla13 As String
                'Dim totalcorteventa As String
                conexionMysql.Open()
                'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
                Sqla13 = "select sum(anticipo)as suma from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and idtipo_pago=2"
                'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
                Dim cmda13 As New MySqlCommand(Sqla13, conexionMysql)
                reader = cmda13.ExecuteReader()
                reader.Read()
                'Try
                txtventastransferencias.Text = reader.GetString(0).ToString
                'txttotalproductos.Text = reader.GetString(1).ToString
            Catch ex As Exception
                cerrarconexion()
                txtventastransferencias.Text = 0
            End Try

            '-------------------------------------------------------------------
            'SUMA De ventas por tarjeta
            '---------------------------------------------------------------------------

            Try

                cerrarconexion()
                Dim Sqla14 As String
                'Dim totalcorteventa As String
                conexionMysql.Open()
                'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
                Sqla14 = "select sum(total)as suma from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and idtipo_pago=3;"
                'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
                Dim cmda14 As New MySqlCommand(Sqla14, conexionMysql)
                reader = cmda14.ExecuteReader()
                reader.Read()
                'Try
                txtventastarjeta.Text = reader.GetString(0).ToString
                'txttotalproductos.Text = reader.GetString(1).ToString
            Catch ex As Exception
                cerrarconexion()
                txtventastarjeta.Text = 0
            End Try



            '-------------------------------------------------------------------
            'SUMA De ventas por vales
            '---------------------------------------------------------------------------

            Try

                cerrarconexion()
                Dim Sqla15 As String
                ' Dim totalcorteventa As String
                conexionMysql.Open()
                'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
                Sqla15 = "select sum(total)as suma from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and idtipo_pago=4;"
                'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
                Dim cmda15 As New MySqlCommand(Sqla15, conexionMysql)
                reader = cmda15.ExecuteReader()
                reader.Read()
                'Try
                txtventavales.Text = reader.GetString(0).ToString
                'txttotalproductos.Text = reader.GetString(1).ToString
            Catch ex As Exception
                cerrarconexion()
                txtventavales.Text = 0
            End Try
            '-------------------------------------------------------------------
            'SUMA DE LOS ANTICIPOS DADOS POR LOS SERVICIOS
            '---------------------------------------------------------------------------
            '--------------------------------------------------
            'Try
            ' MsgBox(fechacaja)

            'MsgBox(horacaja)


            ' Catch ex As Exception
            'MsgBox("error 1")
            'txtanticipos.Text = 0
            ' cerrarconexion()
            '  End Try


        End If

        '    Catch ex As Exception
        ' MsgBox("Aun no hay ventas.", MsgBoxStyle.Information, "Sistema")
        ' txtsaldogenerado.Text = 0
        'txttotalproductos.Text = 0
        conexionMysql.Close()


        cerrarconexion()






        '    End Try
        '-------------------------------------------------------------------------------------


        '----------------------------------------------------------
        'SE REALIZA LA SUMA DE TODAS LAS VENTAS QUE SE REALIZARON EL DIA
        'TOMANDO EN CUENTA LA FECHA DE APERTURA DE LA CAJA
        '----------------------------------------------------------


        'Dim sumaventas As Double
        'cerrarconexion()
        'conexionMysql.Open()
        'Dim Sql2 As String
        'Sql2 = "select sum(total)as total from venta where fecha='" & fecha & "';"
        'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
        'reader = cmd2.ExecuteReader
        'reader.Read()
        'txtsaldogenerado.Text = reader.GetString(0).ToString()
        'conexionMysql.Close()
        '    --------------------------------------------------------------------
        '    cerrarconexion()

        'conexionMysql.Open()
        'Dim Sql3 As String
        'Sql3 = "select sum(recargas_venta)as suma1, sum(ciber) as suma from corte where fecha_registro='" & fecha & "';"
        'Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
        'reader = cmd3.ExecuteReader
        'reader.Read()
        'conexionMysql.Close()

        'calculamos las operaciones para que de el final.
        ' cbttotales.Text = "$ " & CDbl(cbtnextras.Text) - CDbl(cbtncompras.Text)
        'hacemos la ultima oepracion matematica
        Try

            txttotalfinalventas.Text = CDbl(txtventasproductos.Text) + CDbl(txtanticipos.Text)

            txttotalventascompras.Text = CDbl(txttotalfinalventas.Text) - CDbl(txtcompras.Text)
            txtdeberialexistir.Text = CDbl(txtsaldoinicial.Text) + CDbl(txttotalventascompras.Text)
        Catch ex As Exception

        End Try

        '  Catch ex As Exception
        '    MsgBox("error")
        ' End Try



    End Function

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click


        Try

            'todos los datos son obtenidos con la fecha actual para evitar conflictos
            Dim dia, mes, año, fecha, horacaja, fechacaja As String
            Dim hora2, minuto, segundo, hora As String
            ' Dim fechacaja As Date
            hora2 = Now.Hour()
            minuto = Now.Minute()
            segundo = Now.Second()

            hora = hora2 & ":" & minuto & ":" & segundo

            dia = Date.Now.Day
            mes = Date.Now.Month
            año = Date.Now.Year
            fecha = año & "-" & mes & "-" & dia
            '-----------------------------------
            'guardamos el registro dentro de la BD CORTE
            '------------------------------------
            cerrarconexion()
            conexionMysql.Open()
            'actualizo el dato
            Dim Sql5 As String
            Sql5 = "UPDATE `caja` SET `monto_final` = '" & txttotalfinalventas.Text & "', `existencia_caja` = '" & txtsaldocaja.Text & "', `hora_final` = '" & hora & "', `estado` = '1', observaciones='" & txtobservaciones.Text & "' WHERE (`idcaja` = '" & txtid.Text & "');"
            Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
            cmd5.ExecuteNonQuery()
            conexionMysql.Close()
            MsgBox("Se ha cerrado la caja", MsgBoxStyle.Information, "CTRL+y")
            Me.Close()
        Catch ex As Exception
            MsgBox("Verifica la información", MsgBoxStyle.Information, "CTRL+y")
        End Try


    End Sub
    Function diferencia()
        Try

            txtdiferencia.Text = CDbl(txtsaldocaja.Text) - CDbl(txttotalventascompras.Text)
            If txtdiferencia.Text < 0 Then
                txtdiferencia.BackColor = Color.Coral
                lbmensaje.Visible = True



                'txtdiferencia.ForeColor = Color.White
            Else
                txtdiferencia.BackColor = Color.White
                'txtdiferencia.ForeColor = Color.Black
                lbmensaje.Visible = False

            End If
        Catch ex As Exception

        End Try

    End Function
    Private Sub Txtsaldocaja_TextChanged(sender As Object, e As EventArgs) Handles txtsaldocaja.TextChanged
        diferencia()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click


        Try

            'todos los datos son obtenidos con la fecha actual para evitar conflictos
            Dim dia, mes, año, fecha, horacaja, fechacaja As String
            Dim hora2, minuto, segundo, hora As String
            ' Dim fechacaja As Date
            hora2 = Now.Hour()
            minuto = Now.Minute()
            segundo = Now.Second()

            hora = hora2 & ":" & minuto & ":" & segundo

            dia = Date.Now.Day
            mes = Date.Now.Month
            año = Date.Now.Year
            fecha = año & "-" & mes & "-" & dia
            '-----------------------------------
            'guardamos el registro dentro de la BD CORTE
            '------------------------------------
            cerrarconexion()
            conexionMysql.Open()
            'actualizo el dato
            Dim Sql5 As String
            Sql5 = "UPDATE `caja` SET `monto_final` = '" & txttotalfinalventas.Text & "', `existencia_caja` = '" & txtsaldocaja.Text & "', `hora_final` = '" & hora & "', `estado` = '1', observaciones='" & txtobservaciones.Text & "', venta_rapida=" & txtventasproductos.Text & ", anticipos=" & txtanticipos.Text & ", compras=" & txtcompras.Text & ", total_ventas_compras=" & txttotalventascompras.Text & ", total_deberia_existir=" & txtdeberialexistir.Text & ", diferencia=" & txtdiferencia.Text & "   WHERE (`idcaja` = '" & txtid.Text & "');"
            Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
            cmd5.ExecuteNonQuery()
            conexionMysql.Close()
            MsgBox("Se ha cerrado la caja", MsgBoxStyle.Information, "CTRL+y")
            'MsgBox(txtid.Text)
            'omiyit la nota---------------------------------------------------------------------------------------------
            'FRNOTACERRARCAJA.ShowDialog()

            frmindex.btnabrircajamenu.Visible = True
            frmindex.btncerrarcajamenu.Visible = False
            Me.Close()
        Catch ex As Exception
            MsgBox("Verifica la información", MsgBoxStyle.Information, "CTRL+y")
        End Try


    End Sub

    Private Sub Button85_Click(sender As Object, e As EventArgs)

    End Sub

End Class