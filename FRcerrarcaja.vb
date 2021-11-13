Imports MySql.Data.MySqlClient
Imports System.IO.StreamWriter
Imports System.IO
Imports System.ComponentModel
Public Class FRcerrarcaja

    Public respaldar As New SaveFileDialog
    Public carpeta As New FolderBrowserDialog
    Private Sub FRcerrarcaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lbmensaje.Visible = False
        cargarlogoticket()
        desglosecorte()
    End Sub
    Function cerrarconexion()
        If conexionMysql.State = ConnectionState.Open Then
            conexionMysql.Close()

        End If
    End Function
    Function cargarlogoticket()
        Dim ruta As String
        'Dim mystreamreader As StreamReader = myProcess.StandardOutput

        Try
            '------------------- LEER INFORMACIÓN DE ARCHIVO ---------------------------

            Dim lineas As New ArrayList()
            Dim carpeta As String
            Dim rutaImagen As String

            carpeta = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

            Dim freader As New StreamReader(carpeta & "\rutaImagenNoBorrar.txt")

            rutaImagen = freader.ReadLine() 'leo primera linea
            'port = freader.ReadLine() 'leo primera linea

            'MsgBox(rutaImagen)
            ''verificamos que exista al menos 1 registro, en caso de que exista 0, indicamos que el valor es 0;
            'cerrarconexion()
            'conexionMysql.Open()
            'Dim sql22 As String
            'sql22 = "select ruta_logo from datos_empresa;"
            'Dim cmd22 As New MySqlCommand(sql22, conexionMysql)
            'reader = cmd22.ExecuteReader
            'reader.Read()
            'ruta = reader.GetString(0).ToString()
            'conexionMysql.Close()
            ''asignamos la ruta a la imagen
            pblogoticket.Image = Image.FromFile(rutaImagen)
            ' btventas.Image = Image.FromFile(rutaImagen)
            'btventas.BackgroundImageLayout = ImageLayout.Stretch
            'btventas.SizeMode = PictureBoxSizeMode.Zoom
            'pblogo.Image = Image.FromFile(rutaImagen)
            'btventas.BackgroundImageLayout = ImageLayout.Stretch
            'pblogo.SizeMode = PictureBoxSizeMode.Zoom

        Catch ex As Exception
            cerrarconexion()
        End Try



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
        lbfechainicial.Text = fecha

        'hora en que se abrio la caja
        horacaja = reader.GetString(1).ToString()
        lbhorainicial.Text = horacaja
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
        '''''''''''''Try
        '''''''''''''    cerrarconexion()

        '''''''''''''    conexionMysql.Open()
        '''''''''''''    Dim sql1ab As String
        '''''''''''''    sql1ab = "select sum(totalcompra)  from compramercancia where fecha>='" & fechacaja & "' and hora>='" & horacaja & "';"
        '''''''''''''    Dim cmd1ab As New MySqlCommand(sql1ab, conexionMysql)
        '''''''''''''    reader = cmd1ab.ExecuteReader
        '''''''''''''    reader.Read()
        '''''''''''''    txtcompramercancia.Text = reader.GetString(0).ToString()
        '''''''''''''    conexionMysql.Close()
        '''''''''''''    cerrarconexion()
        '''''''''''''Catch ex As Exception
        '''''''''''''    'MsgBox("Aun no hay compras realizadas", MsgBoxStyle.Information, "Sistema")
        '''''''''''''    txtcompramercancia.Text = 0
        '''''''''''''    cerrarconexion()
        '''''''''''''End Try

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
            sql22 = "select min(idventa)as minimo, max(idventa) as maximo from venta where fechaventa>='" & fechacaja & "' and hora >= '" & horacaja & "';"
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
                '  MsgBox(fechacaja)
                ' MsgBox(horacaja)
                cerrarconexion()
                Dim Sqla1 As String
                'Dim totalcorteventa As String
                conexionMysql.Open()
                'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
                Sqla1 = "select sum(total)as suma from venta where fechaventa>='" & fechacaja & "' and hora>='" & horacaja & "' and tipoventa=1;"
                'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
                Dim cmda1 As New MySqlCommand(Sqla1, conexionMysql)
                reader = cmda1.ExecuteReader()
                reader.Read()
                'Try
                MsgBox("ventas")
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
            ''''''''''Try

            ''''''''''    cerrarconexion()
            ''''''''''    Dim Sqla12 As String
            ''''''''''    Dim totalcorteventa As String
            ''''''''''    conexionMysql.Open()
            ''''''''''    'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
            ''''''''''    Sqla12 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and idtipo_pago=1;"
            ''''''''''    'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
            ''''''''''    Dim cmda12 As New MySqlCommand(Sqla12, conexionMysql)
            ''''''''''    reader = cmda12.ExecuteReader()
            ''''''''''    reader.Read()
            ''''''''''    'Try
            ''''''''''    'txtventasproductos.Text = reader.GetString(0).ToString
            ''''''''''    txtventasefectivo.Text = reader.GetString(0).ToString
            ''''''''''    'MsgBox("echo")
            ''''''''''    'txttotalproductos.Text = reader.GetString(1).ToString
            ''''''''''Catch ex As Exception
            ''''''''''    cerrarconexion()
            ''''''''''    'txtventasproductos.Text = 0
            ''''''''''    txtventasefectivo.Text = 0

            ''''''''''End Try


            '-------------------------------------------------------------------
            'SUMA De ventas por transferencias
            '---------------------------------------------------------------------------

            '''''''''''Try

            '''''''''''    cerrarconexion()
            '''''''''''    Dim Sqla13 As String
            '''''''''''    'Dim totalcorteventa As String
            '''''''''''    conexionMysql.Open()
            '''''''''''    'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
            '''''''''''    Sqla13 = "select sum(anticipo)as suma from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and idtipo_pago=2"
            '''''''''''    'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
            '''''''''''    Dim cmda13 As New MySqlCommand(Sqla13, conexionMysql)
            '''''''''''    reader = cmda13.ExecuteReader()
            '''''''''''    reader.Read()
            '''''''''''    'Try
            '''''''''''    txtventastransferencias.Text = reader.GetString(0).ToString
            '''''''''''    'txttotalproductos.Text = reader.GetString(1).ToString
            '''''''''''Catch ex As Exception
            '''''''''''    cerrarconexion()
            '''''''''''    txtventastransferencias.Text = 0
            '''''''''''End Try

            '-------------------------------------------------------------------
            'SUMA De ventas por tarjeta
            '---------------------------------------------------------------------------

            '''''''''''''Try

            '''''''''''''    cerrarconexion()
            '''''''''''''    Dim Sqla14 As String
            '''''''''''''    'Dim totalcorteventa As String
            '''''''''''''    conexionMysql.Open()
            '''''''''''''    'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
            '''''''''''''    Sqla14 = "select sum(total)as suma from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and idtipo_pago=3;"
            '''''''''''''    'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
            '''''''''''''    Dim cmda14 As New MySqlCommand(Sqla14, conexionMysql)
            '''''''''''''    reader = cmda14.ExecuteReader()
            '''''''''''''    reader.Read()
            '''''''''''''    'Try
            '''''''''''''    txtventastarjeta.Text = reader.GetString(0).ToString
            '''''''''''''    'txttotalproductos.Text = reader.GetString(1).ToString
            '''''''''''''Catch ex As Exception
            '''''''''''''    cerrarconexion()
            '''''''''''''    txtventastarjeta.Text = 0
            '''''''''''''End Try



            '-------------------------------------------------------------------
            'SUMA De ventas por vales
            '---------------------------------------------------------------------------

            '''''''''Try

            '''''''''    cerrarconexion()
            '''''''''    Dim Sqla15 As String
            '''''''''    ' Dim totalcorteventa As String
            '''''''''    conexionMysql.Open()
            '''''''''    'Sql = "select sum(total), fecha from venta where fecha='" & fecha & "';"
            '''''''''    Sqla15 = "select sum(total)as suma from venta where fecha>='" & fechacaja & "' and hora>='" & horacaja & "' and idtipo_pago=4;"
            '''''''''    'Sqla1 = "select sum(total)as suma, sum(cantidad)as cantidad  from venta where idventa between '" & min & "' and '" & max & "';"
            '''''''''    Dim cmda15 As New MySqlCommand(Sqla15, conexionMysql)
            '''''''''    reader = cmda15.ExecuteReader()
            '''''''''    reader.Read()
            '''''''''    'Try
            '''''''''    txtventavales.Text = reader.GetString(0).ToString
            '''''''''    'txttotalproductos.Text = reader.GetString(1).ToString
            '''''''''Catch ex As Exception
            '''''''''    cerrarconexion()
            '''''''''    txtventavales.Text = 0
            '''''''''End Try
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
            txtdeberialexistir.Text = CDbl(txtsaldoinicial.Text) + CDbl(txttotalfinalventas.Text)
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


            Dim total As Double
            total = CDbl(txttotalfinalventas.Text) + CDbl(txtsaldoinicial.Text)


            '            txtdiferencia.Text = CDbl(txtsaldocaja.Text) - CDbl(txttotalfinalventas.Text)
            txtdiferencia.Text = CDbl(txtsaldocaja.Text) - total
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


            lbfechafinal.Text = fecha
            lbhorafinal.Text = hora




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


            'se imprime el ticket de venta
            impresionticket()


            frmindex.btnabrircajamenu.Visible = True
            frmindex.btncerrarcajamenu.Visible = False
            Me.Close()
        Catch ex As Exception
            MsgBox("Verifica la información", MsgBoxStyle.Information, "CTRL+y")
        End Try


    End Sub
    Function impresionticket()

        'consultamos a la BD la impresora seleccionada por default
        Dim impresora As String
        Try
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql1 As String
            Sql1 = "select * from impresora;"
            Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
            reader = cmd1.ExecuteReader()
            reader.Read()
            impresora = reader.GetString(1).ToString()
            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("No hay una impresora seleccionada", MsgBoxStyle.Information, "Sistema")
            cerrarconexion()
        End Try



        ' txtetiqueta1 = " prueba de impresión"
        'txtetiqueta2 = " Nº : " & lbfolio.Text
        'txtetiqueta = " De : " & "$" & txttotalpagar.Text &
        'Chr(10) & " " & "12/12/!2"
        Try
            Dim PrintDialog1 As New PrintDialog
            PrintDialog1.Document = PrintDocument1
            PrintDialog1.PrinterSettings.PrinterName = impresora
            If PrintDocument1.PrinterSettings.IsValid Then
                PrintDocument1.Print() 'Imprime texto 
            Else
                MsgBox("Impresora invalida", MsgBoxStyle.Exclamation, "CTRL+y")
                'MessageBox.Show("La impresora no es valida")
            End If
            '--------------------------------------------------- 
        Catch ex As Exception
            MsgBox("Hay problemas con la impresion", MsgBoxStyle.Exclamation, "CTRL+y")

            'MessageBox.Show("Hay un problema de impresión",
            ex.ToString()
        End Try
    End Function
    Private Sub Button85_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim folio As Integer
        'If activarbusqueda = True Then
        '    folio = rtxtbusquedafolio.Text
        'Else
        '    folio = slbfolio.Text
        'End If

        'e.Graphics.DrawString(txtetiqueta1, New Font("verdana", 11, FontStyle.Bold), New SolidBrush(Color.Black), 1, 9)
        'e.Graphics.DrawString(txtetiqueta2, New Font("verdana", 9, FontStyle.Bold), New SolidBrush(Color.Black), 1, 28)
        'e.Graphics.DrawString(txtetiqueta, New Font("verdana", 13, FontStyle.Bold), New SolidBrush(Color.Black), 1, 57)
        'traemos la información del ticket, como encabezado, datos de la empresa etc.
        Dim saludo, ticketnombre, ticketcoloniaciudad, tickettelefono, ticketgiro, p1, p2, p3, p4, comentario, saludo2, saludo3, saludo4, saludo5 As String
        Dim callenumero, cp, estado, whatsapp, correo, rfc As String
        Dim x, y, tfuente, tfuente2, tfuente3 As Integer
        Try

            cerrarconexion()
            conexionMysql.Open()
            Dim Sql1 As String
            Sql1 = "select * from datos_empresa;"
            Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
            reader = cmd1.ExecuteReader()
            reader.Read()
            ticketnombre = reader.GetString(1).ToString()
            callenumero = reader.GetString(2).ToString()
            ticketcoloniaciudad = reader.GetString(3).ToString()
            cp = reader.GetString(4).ToString()
            estado = reader.GetString(5).ToString()
            tickettelefono = reader.GetString(6).ToString()
            whatsapp = reader.GetString(7).ToString()
            correo = reader.GetString(8).ToString()
            'ctxtfacebook.Text = reader.GetString(9).ToString()
            'ctxtsitio.Text = reader.GetString(10).ToString()
            'ctxtencargado.Text = reader.GetString(11).ToString()
            'ctxthorario.Text = reader.GetString(12).ToString()
            ticketgiro = reader.GetString(13).ToString()
            saludo = reader.GetString(24).ToString()
            saludo2 = reader.GetString(25).ToString()
            saludo3 = reader.GetString(26).ToString()
            saludo4 = reader.GetString(27).ToString()
            saludo5 = reader.GetString(28).ToString()
            'p1 = reader.GetString(14).ToString()
            'P2 = reader.GetString(15).ToString()
            'P3 = reader.GetString(16).ToString()
            'p4 = reader.GetString(17).ToString()
            rfc = reader.GetString(22).ToString()

            conexionMysql.Close()

        Catch ex As Exception
            cerrarconexion()
            MsgBox("Los datos de la empresa aun estan vacios", MsgBoxStyle.Information, "MOBI")
        End Try


        'OBTENEMOS LOS DATOS DEL CIERRE DE CAJA
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

        Try

            cerrarconexion()
            conexionMysql.Open()
            Dim Sql1 As String
            Sql1 = "select * from datos_empresa;"
            Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
            reader = cmd1.ExecuteReader()
            reader.Read()
            ticketnombre = reader.GetString(1).ToString()
            callenumero = reader.GetString(2).ToString()
            ticketcoloniaciudad = reader.GetString(3).ToString()
            cp = reader.GetString(4).ToString()
            estado = reader.GetString(5).ToString()
            tickettelefono = reader.GetString(6).ToString()
            whatsapp = reader.GetString(7).ToString()
            correo = reader.GetString(8).ToString()

            conexionMysql.Close()
        Catch ex As Exception

        End Try


        Try

            cerrarconexion()
            conexionMysql.Open()
            Dim Sql1 As String
            Sql1 = "select * from corte;"
            Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
            reader = cmd1.ExecuteReader()
            reader.Read()
            ticketnombre = reader.GetString(1).ToString()
            callenumero = reader.GetString(2).ToString()
            ticketcoloniaciudad = reader.GetString(3).ToString()
            cp = reader.GetString(4).ToString()
            estado = reader.GetString(5).ToString()
            tickettelefono = reader.GetString(6).ToString()
            whatsapp = reader.GetString(7).ToString()
            correo = reader.GetString(8).ToString()

            conexionMysql.Close()
        Catch ex As Exception

        End Try




        tfuente = 10 '7
        tfuente2 = 14
        tfuente3 = 16
        p1 = 10 'posicion de X
        x = 5
        y = 5
        Dim ii, incremento As Integer
        incremento = 16
        Dim yy(29) As Integer
        For ii = 0 To 29
            y = y + incremento
            yy(ii) = y
        Next



        Try
            ' La fuente a usar
            Dim prFont As New Font("Arial", 15, FontStyle.Bold)
            'POSICION DEL LOGO
            ' la posición superior
            e.Graphics.DrawImage(pblogoticket.Image, 50, 20, 120, 100)


            'imprimir el titutlo del ticket



            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString(ticketnombre, prFont, Brushes.Black, x, yy(7))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(8))
            'IMPRESION DE LOGOTIPO,
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(callenumero, prFont, Brushes.Black, x, yy(9))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(ticketcoloniaciudad, prFont, Brushes.Black, x, yy(10))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(cp, prFont, Brushes.Black, x, yy(11))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("TEL:" & tickettelefono, prFont, Brushes.Black, x, yy(12))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("LIC." & rfc, prFont, Brushes.Black, x, yy(13))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, yy(14))
            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString("FOLIO #" & txtid.Text, prFont, Brushes.Black, x, yy(15))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Fecha Ini: " & lbfechainicial.Text, prFont, Brushes.Black, x, yy(16))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Hora ini: " & lbhorainicial.Text, prFont, Brushes.Black, x, yy(17))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Fecha fin: " & lbfechafinal.Text, prFont, Brushes.Black, x, yy(18))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Hora fin: " & lbhorafinal.Text, prFont, Brushes.Black, x, yy(19))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Saldo inicial: $" & txtsaldoinicial.Text, prFont, Brushes.Black, x, yy(20))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Saldo Existente: $" & txtsaldocaja.Text, prFont, Brushes.Black, x, yy(21))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Saldo Esperado: $" & txtdeberialexistir.Text, prFont, Brushes.Black, x, yy(22))


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Diferencia : $" & txtdiferencia.Text, prFont, Brushes.Black, x, yy(23))


            '---------------------------------------------------------------------------------------------------------------------------------
            'consulto, cuantos dispositivos son, para obtener su informacion

            'Dim cantidaddispositivos, vueltas As Integer
            'cerrarconexion()
            'conexionMysql.Open()
            'Dim Sql12 As String
            ''temporalmente slbfolio.text por rtxtbusquedatemporal
            'Sql12 = "select count(*) from equipo where idventa='" & folio & "';"
            'Dim cmd12 As New MySqlCommand(Sql12, conexionMysql)
            'reader = cmd12.ExecuteReader()
            'reader.Read()
            'cantidaddispositivos = reader.GetString(0).ToString()
            ''callenumero = reader.GetString(2).ToString()
            ''ticketcoloniaciudad = reader.GetString(3).ToString()
            ''cp = reader.GetString(4).ToString()
            'conexionMysql.Close()
            'cerrarconexion()

            'conexionMysql.Open()
            'Dim Sql123, problema, equipo, modelo, imei, estadox, pass, notes As String
            'Sql123 = "select * from equipo where idventa='" & folio & "';"
            'Dim cmd123 As New MySqlCommand(Sql123, conexionMysql)
            'reader = cmd123.ExecuteReader()

            'Dim pp1, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14 As Integer

            'pp1 = yy(7)

            'p2 = yy(8)
            'p3 = yy(9)
            'p4 = yy(10)
            'p5 = yy(11)
            'p6 = yy(12)
            'p7 = yy(13)
            'p8 = yy(14)
            'p9 = yy(15)
            'p10 = yy(16)
            'p11 = yy(17)
            'p12 = yy(18)
            'p13 = yy(19)
            'p14 = yy(20)




            'For vueltas = 1 To cantidaddispositivos


            '    reader.Read()
            '    equipo = reader.GetString(1).ToString()
            '    modelo = reader.GetString(2).ToString()
            '    pass = reader.GetString(4).ToString()
            '    notes = reader.GetString(7).ToString()
            '    problema = reader.GetString(6).ToString()


            '    'imprimir el titutlo del ticket
            '    '----------------------------------------------------------------------------------------------------------------------------

            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, pp1)

            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString("--PROBLEM--", prFont, Brushes.Black, x, p2)

            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString(problema, prFont, Brushes.Black, x, p3)

            '    'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    'e.Graphics.DrawString("ID------PRECIO------CANTIDAD----TOTAL", prFont, Brushes.Black, x, yy(15))





            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p4)


            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString("--Device--", prFont, Brushes.Black, x, p5)
            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString("Item name:" & equipo, prFont, Brushes.Black, x, p6)

            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString("Model:" & modelo, prFont, Brushes.Black, x, p7)


            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString("Pass Code: " & pass, prFont, Brushes.Black, x, p8)


            '    prFont = New Font("Arial", tfuente, FontStyle.Bold)
            '    e.Graphics.DrawString("Notes:" & notes, prFont, Brushes.Black, x, p9)
            '    '----------------------------------------------------------------------------------------------------------------------

            '    pp1 = pp1 + (incremento * 9)
            '    p2 = p2 + (incremento * 9)
            '    p3 = p3 + (incremento * 9)
            '    p4 = p4 + (incremento * 9)
            '    p5 = p5 + (incremento * 9)
            '    p6 = p6 + (incremento * 9)
            '    p7 = p7 + (incremento * 9)
            '    p8 = p8 + (incremento * 9)
            '    p9 = p9 + (incremento * 9)
            '    'p10 = p10 + (incremento * 7)
            '    'p11 = p11 + (incremento * 7)
            '    'p12 = p12 + (incremento * 7)
            '    'p13 = p13 + (incremento * 7)
            '    'p14 = p14 + (incremento * 7)
            '    '                yy = yy + (incremento * 3.2)
            '    '               yy = yy + (incremento * 3.2)




            'Next
            'conexionMysql.Close()
            'cerrarconexion()

            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("==================================", prFont, Brushes.Black, 0, pp1)



            'Dim fechacalendarioentrega As String
            'fechacalendarioentrega = rcalendario.Value.ToString("MM/dd/yyyy")


            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("Delivery date:" & fechacalendarioentrega, prFont, Brushes.Black, x, p2)


            ''aqui es donde tenemos que leer todos los datos de la grilla llamada "grilla"

            'Dim i As Integer = sgrilla.RowCount
            ''MsgBox(i)
            'Dim t1, t2, t3, t4, t5 As Integer
            'Dim actividad As String
            'Dim cantidad, costo, idventa As Double
            'Dim idproducto As String
            'Dim jj As Integer
            '' t1 = yy(16)
            '' t2 = yy(17)
            '' t3 = yy(18)
            '' t4 = 40
            ''MsgBox(jj)
            ''MsgBox()
            ''suma de valores
            ''''''''For jj = 0 To i - 1



            ''   MsgBox("valosr de j:" & jj)
            ''a = venta.grillaventa.Item(j, 3).Value.ToString()
            ''''''''actividad = sgrilla.Rows(jj).Cells(1).Value 'descripcion
            ''''''''cantidad = sgrilla.Rows(jj).Cells(2).Value 'cantidad
            ''''''''costo = sgrilla.Rows(jj).Cells(3).Value 'costo
            ''''''''idproducto = sgrilla.Rows(jj).Cells(0).Value
            ''''''''comentario = sgrilla.Rows(jj).Cells(4).Value
            ''cerrarconexion()
            ''conexionMysql.Open()

            '' MsgBox("el producto es:" & actividad)

            ''Dim Sql2 As String
            ''Sql2 = "INSERT INTO ventaind (actividad, cantidad, costo, idventa,idproducto) VALUES ('" & actividad & "'," & cantidad & "," & costo & "," & lbfolio.Text & ",'" & idproducto & "');"
            ''Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            ''cmd2.ExecuteNonQuery()
            ''conexionMysql.Close()


            ''prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            ''    e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t2)


            ''    'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            ''    'e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t3)
            ''    '----------
            ''    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            ''    e.Graphics.DrawString("$" & costo, prFont, Brushes.Black, x, t3)
            ''    '----------
            ''    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            ''    e.Graphics.DrawString(cantidad, prFont, Brushes.Black, x + 80, t3)
            ''    '-----------

            ''    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            ''    e.Graphics.DrawString("$" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, x + 160, t3)
            '''-----------



            ''prFont = New Font("Arial", 10, FontStyle.Bold)
            ''e.Graphics.DrawString(cantidad & "-- $" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, 0, t3)

            ''''''''   t1 = t1 + (incremento * 3.2)
            ''''''''t2 = t2 + (incremento * 3.2)
            ''''''''t3 = t3 + (incremento * 3.2)

            '''''''' Next

            't1 = t1 - (incremento * 2)
            't2 = t2 - (incremento * 2)
            't3 = t3 - (incremento * 2)



            ''----------------AQUI SE IMPRIME EL TOTAL A PAGAR
            'cerrarconexion()
            'conexionMysql.Open()
            'Dim Sql14, total, deposito, resto As String
            'Sql14 = "select total,deposito,resto from venta where idventa='" & folio & "';"
            'Dim cmd14 As New MySqlCommand(Sql14, conexionMysql)
            'reader = cmd14.ExecuteReader()
            'reader.Read()
            'total = reader.GetString(0).ToString()
            'deposito = reader.GetString(1).ToString()
            'resto = reader.GetString(2).ToString()

            'conexionMysql.Close()

            'cerrarconexion()


            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("Total:", prFont, Brushes.Black, x, p3)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("$ " & total, prFont, Brushes.Black, x + 150, p4)

            ''prFont = New Font("Arial", tfuente, FontStyle.Bold)
            ''e.Graphics.DrawString("Price:", prFont, Brushes.Black, x, p5)
            ''prFont = New Font("Arial", tfuente, FontStyle.Bold)
            ''e.Graphics.DrawString("$ " & rtxtcostoreparacion.Text, prFont, Brushes.Black, x + 150, p6)


            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("Down payment:", prFont, Brushes.Black, x, p5)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("$ " & deposito, prFont, Brushes.Black, x + 150, p6)


            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("Balance:", prFont, Brushes.Black, x, p7)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("$ " & resto, prFont, Brushes.Black, x + 150, p8)



            ' prFont = New Font("Arial", tfuente, FontStyle.Bold)
            ' e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p9)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(saludo, prFont, Brushes.Black, x, p10)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(saludo2, prFont, Brushes.Black, x, p11)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(saludo3, prFont, Brushes.Black, x, p12)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(saludo4, prFont, Brushes.Black, x, p13)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(saludo5, prFont, Brushes.Black, x, p14)
            'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            'e.Graphics.DrawString("CTRL+y", prFont, Brushes.Black, 10, t2 + 60)

            ''imprimimos la fecha y hora
            'prFont = New Font("Arial", 10, FontStyle.Regular)
            'e.Graphics.DrawString(Date.Now.ToShortDateString.ToString & " " &
            '                Date.Now.ToShortTimeString.ToString, prFont, Brushes.Black, 15, 385)

            ''imprimimos el nombre del cliente
            'prFont = New Font("Arial", 25, FontStyle.Bold)
            'e.Graphics.DrawString("Nombre del Cliente" & txtcliente.Text, prFont, Brushes.Black, 50, 250)

            ''imprimimos la referencia del pedido
            'e.Graphics.DrawString("Referencia", prFont, Brushes.Black, 50, 520)
            'prFont = New Font("Arial", 18, FontStyle.Bold)
            'e.Graphics.DrawString("Nombre de la Referencia", prFont, Brushes.Black, 50, 555)

            ''imprimimos Pedido Ondupack y Pedido del cliente
            'prFont = New Font("Arial", 22, FontStyle.Regular)
            'e.Graphics.DrawString("Pedido", prFont, Brushes.Black, 50, 660)
            'e.Graphics.DrawString("Palets", prFont, Brushes.Black, 250, 660)

            'prFont = New Font("Arial", 24, FontStyle.Regular)
            'e.Graphics.DrawString("19875", prFont, Brushes.Black, 50, 700)
            'e.Graphics.DrawString("44", prFont, Brushes.Black, 250, 700)

            ''imprimimos Cajas X Palet y Cajas x Paquete
            'prFont = New Font("Arial", 22, FontStyle.Regular)
            'e.Graphics.DrawString("Cajas x Palet", prFont, Brushes.Black, 50, 760)
            'e.Graphics.DrawString("Cajas x Paquete", prFont, Brushes.Black, 250, 760)

            'prFont = New Font("Arial", 24, FontStyle.Regular)
            'e.Graphics.DrawString("500", prFont, Brushes.Black, 50, 800)
            'e.Graphics.DrawString("32", prFont, Brushes.Black, 250, 800)

            ''imprimimos el numero del Palet
            'prFont = New Font("Arial", 24, FontStyle.Regular)
            'e.Graphics.DrawString("Número del Palet     45", prFont, Brushes.Black, 50, 880)
            ''indicamos que hemos llegado al final de la pagina
            'e.HasMorePages = False

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message, "Administrador",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class