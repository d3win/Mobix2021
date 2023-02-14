Imports MySql.Data.MySqlClient
Imports System.IO.StreamWriter
Imports System.IO
Imports System.ComponentModel

Public Class frmindex
    Public respaldar As New SaveFileDialog
    Public carpeta As New FolderBrowserDialog
    Public hora, hora2, minuto, segundo, usuarioactual As String
    Public idcustomer, idequipo, idseller, idcustomerbus As String
    Public activarbusqueda, activarreparacion As Boolean
    Public restatus, redeliverdate, rechekout, recustomer, rephone, remodelo, reorder, statusbusquedaventa As Boolean

    Private Sub Button75_Click(sender As Object, e As EventArgs) Handles Button75.Click

        cerrarconexion()
        'cargamos la ventana de frdatosorganizacion

        Dim formulario As New frdatosorganizacion


        formulario.ShowDialog()
    End Sub
    Function cerrarconexion()
        If conexionMysql.State = ConnectionState.Open Then
            conexionMysql.Close()

        End If
    End Function
    Function cargarlogook()
        'Dim cmdx1 As New MySqlCommand(Sqlx1, conexionMysql)
        'Dim dt As New DataTable
        ' Try

        'pblogo.Visible = True


        ' Establecemos a Nothing el valor de la propiedad Image
        ' del control PictureBox.
        'pblogo.Image = Nothing
        pblogo.Image = Nothing
        ' Catch ex As Exception

        'End Try

        Try
            Dim sql As String
            Dim dt As New DataTable
            sql = "select * from logo_empresa;"
            Dim cmdx1 As New MySqlCommand(sql, conexionMysql)
            Dim da As New MySqlDataAdapter(cmdx1)

            '            da = New SqlDataAdapter(sql, conexionMysql)
            conexionMysql.Open()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim ms As New System.IO.MemoryStream() 'Creamos el MemoryStream y poder cargar la imagen.
                Dim imageBuffer() As Byte = CType(dt.Rows(0).Item("LOGO"), Byte()) 'Conbertimos la imagen cargada en el datatable a Bytes.
                ms = New System.IO.MemoryStream(imageBuffer) 'Cargamos el MemoryStream con la imagen ya convertida en Bytes.
                pblogo.BackgroundImage = Nothing 'Si existe una imagen cargada en el PictureBox la borramos.
                pblogo.BackgroundImage = (Image.FromStream(ms)) 'Cargamos la imagen al PictureBox, Nota: Lo hacemos como .BackgroundImage pero igual lo podemos hacer como .Image.
                pblogo.BackgroundImageLayout = ImageLayout.Zoom 'Ajustamos la imagen al todo el PictureBox.
                conexionMysql.Close()
                dt.Clear()
                dt.Reset()
                ms.Dispose()
                ms.Close()
                sql = ""
            End If
            conexionMysql.Close()


            comprobarexistenciadelogo()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function
    Function comprobarexistenciadelogo()
        cerrarconexion()
        Dim id As String
        Try

            conexionMysql.Open()


            Dim Sql111 As String
            Sql111 = "select idlogo_empresa from logo_empresa;"
            Dim cmd111 As New MySqlCommand(Sql111, conexionMysql)
            reader = cmd111.ExecuteReader()
            reader.Read()

            id = reader.GetString(0).ToString
        Catch ex As Exception
            cerrarconexion()
            id = 0
        End Try



        'MsgBox(id)

        If id = 1 Then

            pblogo.Visible = True
            ' MsgBox("visible")
        Else
            pblogo.Visible = False
            '       MsgBox("Nooooooooo visible")

        End If


        conexionMysql.Close()

    End Function
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        cargarseller()
        TabControl1.SelectedIndex = 4
        cerrarconexion()
        'TabControl1.SelectedIndex = 3
        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.DimGray
        Button67.BackColor = Color.FromArgb(27, 38, 44)
        Button14.BackColor = Color.FromArgb(27, 38, 44)
        Button38.BackColor = Color.FromArgb(27, 38, 44)

        'cargarlogook()
        cargarlogoticket()
        'comprobarexistenciadelogo()

        'Dim tipo As Integer

        'tipo = tipoingreso()


        'If tipo = 2 Then

        '    TabControl1.SelectedIndex = 1
        '    Button1.BackColor = Color.FromArgb(27, 38, 44)
        '    Button2.BackColor = Color.FromArgb(27, 38, 44)
        '    ' Button3.BackColor = Color.FromArgb(47, 56, 72)
        '    ' Button4.BackColor = Color.FromArgb(47, 56, 72)
        '    ' Button5.BackColor = Color.FromArgb(47, 56, 72)
        '    ' Button6.BackColor = Color.FromArgb(47, 56, 72)
        '    Button8.BackColor = Color.FromArgb(27, 38, 44)
        '    'Button10.BackColor = Color.FromArgb(47, 56, 72)
        '    Button12.BackColor = Color.FromArgb(27, 38, 44)
        '    ' Button13.BackColor = Color.FromArgb(47, 56, 72)
        '    Button67.BackColor = Color.DimGray


        '    ''''''''''''''''''' comprobartipoingreso()
        'Else

        '    ' TabControl1.SelectedIndex = 9

        '    Button1.BackColor = Color.FromArgb(27, 38, 44)
        '    Button2.BackColor = Color.FromArgb(27, 38, 44)
        '    'Button3.BackColor = Color.FromArgb(47, 56, 72)
        '    'Button4.BackColor = Color.FromArgb(47, 56, 72)
        '    'Button5.BackColor = Color.FromArgb(47, 56, 72)
        '    'Button6.BackColor = Color.FromArgb(47, 56, 72)
        '    Button8.BackColor = Color.FromArgb(27, 38, 44)
        '    '  Button10.BackColor = Color.FromArgb(47, 56, 72)
        '    Button12.BackColor = Color.DimGray
        '    ' Button13.BackColor = Color.FromArgb(47, 56, 72)
        '    Button67.BackColor = Color.FromArgb(27, 38, 44)





        'End If

        ' Try
        asignar_impresora()
        'cargamos los datos de la empresa para que se muestren, en caso de que exista un valor.
        cargardatos_empresa()
        'cargamos el tipo de corte que se ha seleccionado en su momento.
        'en caso de que no exista ningun corte lo insertamos, solo existen 2 tipo, por usuario y por tiempo
        '''''''''''''''''''tipo_corte_configuracion()

        ''''''''''''''' cargardatos_ticket_cambio()



        'cargarlogook()
        'Catch ex As Exception
        'cerrarconexion()
        'End Try
    End Sub
    Function cargardatos_empresa()
        Try

            conexionMysql.Open()
            Dim Sql1 As String
            Sql1 = "select * from datos_empresa;"
            Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
            reader = cmd1.ExecuteReader()
            reader.Read()
            orgrfc.Text = reader.GetString(22).ToString()

            '  MsgBox(reader.GetString(22).ToString())

            orgnombre.Text = reader.GetString(1).ToString()
            orgcallenumero.Text = reader.GetString(2).ToString()
            orgcoloniaciudad.Text = reader.GetString(3).ToString()
            orgcp.Text = reader.GetString(4).ToString()
            orgestado.Text = reader.GetString(5).ToString()
            orgtelefono.Text = reader.GetString(6).ToString()
            orgwhatsapp.Text = reader.GetString(7).ToString()
            orgcorreo.Text = reader.GetString(8).ToString()
            orgfacebook.Text = reader.GetString(9).ToString()
            orgsitioweb.Text = reader.GetString(10).ToString()
            orgencargado.Text = reader.GetString(11).ToString()
            orghorario.Text = reader.GetString(12).ToString()
            orggiro.Text = reader.GetString(13).ToString()
            orgsaludo.Text = reader.GetString(24).ToString()
            ors2.Text = reader.GetString(25).ToString()
            ors3.Text = reader.GetString(26).ToString()
            ors4.Text = reader.GetString(27).ToString()
            ors5.Text = reader.GetString(28).ToString()

            'ctxtminimoarticulos.Text = reader.GetString(21).ToString()


            conexionMysql.Close()
            ' MsgBox("datos cargados")
        Catch ex As Exception
            ' MsgBox("error")
        End Try

    End Function
    Function asignar_impresora()

        Try


            Dim respuesta_nombre_impresora As String


            respuesta_nombre_impresora = obtener_nombre_impresora()
            'MsgBox(respuesta_nombre_impresora)

            If respuesta_nombre_impresora = "" Then
                'en caso de que no exista impresora almacenada, entonces buscamos la que esta por default y la guardamos



                Dim instance As New Printing.PrinterSettings
                Dim impresosaPredt As String = instance.PrinterName
                'MsgBox("LA IMPRESORA A GUARDAR ES:" & impresosaPredt)

                txtnombreimpresora.Text = impresosaPredt

                cerrarconexion()
                conexionMysql.Open()
                Dim Sql2 As String
                Sql2 = "INSERT INTO `impresora` (`idimpresora`, `nombre_impresora`) VALUES ('1', '" & impresosaPredt & "');"
                Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                cmd2.ExecuteNonQuery()
                conexionMysql.Close()
                MsgBox("Se ha asignado por default la impresora que esta predeterminada en el sistema", MsgBoxStyle.Information, "CTRL+y")

                '            MsgBox("Hola esto es una prueba" & Chr(13) &
                '" de un textbox multilinea" 



            Else
                'en caso contrario se supone que ya hay impresora, solo la asignamos al label de configuracion de impresora

                txtnombreimpresora.Text = respuesta_nombre_impresora


            End If

        Catch ex As Exception

        End Try




    End Function

    Function obtener_nombre_impresora()
        Try

            Dim nombre_impresora
            conexionMysql.Open()
            Dim Sql111 As String
            Sql111 = "select * from impresora;"
            Dim cmd111 As New MySqlCommand(Sql111, conexionMysql)
            reader = cmd111.ExecuteReader()
            reader.Read()

            nombre_impresora = reader.GetString(1).ToString
            conexionMysql.Close()





            If nombre_impresora = "" Then
                MsgBox("No hay ninguna impresora seleccionada")
            End If
            Return nombre_impresora
        Catch ex As Exception
            cerrarconexion()
        End Try


    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lbcalendario.Text = Date.Now
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        'Dim respuesta As String
        'If activarreparacion = True Then
        '    respuesta = MsgBox("Hay una venta en curso, ¿desea cancelarla?", MsgBoxStyle.YesNo, "MOBI")

        '    If respuesta = vbYes Then

        '    End If


        'Else
        '    End
        'End If

        End
    End Sub

    Function eliminarreparacionencurso()
        cerrarconexion()
        conexionMysql.Open()
        Dim Sql2 As String
        Sql2 = "delete from venta_ind where idventa=" & slbfolio.Text & ";"
        Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
        cmd2.ExecuteNonQuery()
        cerrarconexion()
        conexionMysql.Close()

        cerrarconexion()
        conexionMysql.Open()
        Dim Sql23 As String
        Sql23 = "delete from venta where idventa=" & slbfolio.Text & ";"
        Dim cmd23 As New MySqlCommand(Sql23, conexionMysql)
        cmd23.ExecuteNonQuery()
        cerrarconexion()
        conexionMysql.Close()


        cerrarconexion()
        conexionMysql.Open()
        Dim Sql234 As String
        Sql234 = "delete from equipo where idventa=" & slbfolio.Text & ";"
        Dim cmd234 As New MySqlCommand(Sql234, conexionMysql)
        cmd234.ExecuteNonQuery()
        cerrarconexion()
        conexionMysql.Close()

    End Function

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        Me.Size = New System.Drawing.Size(1400, 900)


        '----------------------------------------------------------
        '1285,711 'esta es la medida antetior...
        '-------------------------------------------------------

        Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2

        'Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.StartPosition = FormStartPosition.CenterScreen
        'Me.Top = 0
        'Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        'maximizar una venta
        'Me.WindowState = FormWindowState.Maximized
        Me.Size = My.Computer.Screen.WorkingArea.Size
        Me.Location = Screen.PrimaryScreen.WorkingArea.Location
        'Me.Location = New System.Drawing.Point(0, 0)
    End Sub

    Private Sub Frmindex_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        actualizar2021()


        Dim id, valor As Integer
        Try
            conexionMysql.Open()
            Dim Sql111 As String
            Sql111 = "select traduccion from datos_empresa;"
            Dim cmd111 As New MySqlCommand(Sql111, conexionMysql)
            reader = cmd111.ExecuteReader()
            reader.Read()

            id = reader.GetString(0).ToString
            conexionMysql.Close()

        Catch ex As Exception
            cerrarconexion()
            id = 0
        End Try

        cerrarconexion()

        If id = 1 Then
            traduccion()
            conexionMysql.Open()
            Dim Sql22 As String
            Sql22 = "UPDATE  datos_empresa SET traduccion = '" & id & "';"
            Dim cmd22 As New MySqlCommand(Sql22, conexionMysql)
            cmd22.ExecuteNonQuery()
            conexionMysql.Close()
            ' MsgBox("Cambio realizado, se cerrara el sistema", MsgBoxStyle.Information, "MOBI")
            conexionMysql.Close()
            'End
            cerrarconexion()
        End If






        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.FromArgb(27, 38, 44)

        'chagenda.CheckState = False
        ' agendagrilla.Visible = False
        'agendacalendario.Visible = False


        '''''''''''''''''''' listaclientes.Visible = False
        'inicio la variable en 0, COMPRAS mercancia
        ''''''''''''''''''''' idsumado = 0

        'Button1.BackColor = Color.DimGray

        btninconsistencia.Visible = False
        'Button1.BackColor = Color.DimGray
        'Button2.BackColor = Color.FromArgb(47, 56, 72)
        'Button8.BackColor = Color.FromArgb(47, 56, 72)
        'Button12.BackColor = Color.FromArgb(47, 56, 72)
        'Button67.BackColor = Color.FromArgb(47, 56, 72)


        'ccgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        'cgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        'grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        'grillap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        'grilla2p.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        'grilla2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        'ugrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        'grillacliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        'cgrillacorte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        'protxtgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill



        'grilla2.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue


        Timer1.Enabled = True


        'ocultamos la grilla2, donde se realizan las busquedas de nombres
        'grilla2.Visible = False


        'rbclave.Checked = True


        'txtnombre.Visible = False
        'lbnombre.Visible = False
        'btnbuscarnombre.Visible = False


        'cargamos la estadistica cada vez que se carge el formulario o en su caso, se de clic al botón inicio.

        'estadistica()

        'SE CARGAN LOS DATOS INICIALES DEL SISTEMA

        '''''''''''''''''''''''''''''''''''''''''''''''''inserciondedatosiniciales()

        'COMPROBAR SI HAY CAJAS ABIERTAS


        comprobarcajaabierta()


        cargarlogoticket()


        ComprobarActualizacionBD

    End Sub
    Function ComprobarActualizacionBD()
        Try
            'actualizacion dodne se crea la caja para poder manejar el efectivo de ventas un resumen de ventas.
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "CREATE TABLE `caja` (
  `idcaja` int(11) NOT NULL AUTO_INCREMENT,
  `monto_inicial` double DEFAULT NULL,
  `monto_final` double DEFAULT NULL,
  `existencia_caja` double DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  `hora_inicial` time DEFAULT NULL,
  `hora_final` time DEFAULT NULL,
  `estado` int(11) DEFAULT NULL,
  `observaciones` text,
  `venta_rapida` double DEFAULT NULL,
  `anticipos` double DEFAULT NULL,
  `compras` double DEFAULT NULL,
  `total_ventas_compras` double DEFAULT NULL,
  `total_deberia_existir` double DEFAULT NULL,
  `diferencia` double DEFAULT NULL,
  PRIMARY KEY (`idcaja`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            cmd2.ExecuteNonQuery()
            cerrarconexion()
            conexionMysql.Close()
            'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
            'MsgBox("USUARIO CREADO CORRECTAMENTE", MsgBoxStyle.Information, "Sistema")
            conexionMysql.Close()
            cerrarconexion()

        Catch ex As Exception

        End Try

        Try
            'actualizacion dodne se crea la caja para poder manejar el efectivo de ventas un resumen de ventas.
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql23 As String
            Sql23 = "ALTER TABLE `mobi`.`venta` 
ADD COLUMN `hora` TIME NULL AFTER `tipoventa`;"
            Dim cmd23 As New MySqlCommand(Sql23, conexionMysql)
            cmd23.ExecuteNonQuery()
            cerrarconexion()
            conexionMysql.Close()
            'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
            'MsgBox("USUARIO CREADO CORRECTAMENTE", MsgBoxStyle.Information, "Sistema")
            conexionMysql.Close()
            cerrarconexion()

        Catch ex As Exception

        End Try








    End Function
    Function cargarlogoticket()
        Dim ruta As String
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
            btventas.Image = Image.FromFile(rutaImagen)
            'btventas.BackgroundImageLayout = ImageLayout.Stretch
            btventas.SizeMode = PictureBoxSizeMode.Zoom
            pblogo.Image = Image.FromFile(rutaImagen)
            'btventas.BackgroundImageLayout = ImageLayout.Stretch
            pblogo.SizeMode = PictureBoxSizeMode.Zoom

        Catch ex As Exception
            cerrarconexion()
        End Try



    End Function
    Function comprobarcajaabierta()
        'primero comprobamos si existe caja abierta.
        '------------------------------------------------
        Dim cantidad As Integer

        Try
            'verificamos que exista al menos 1 registro, en caso de que exista 0, indicamos que el valor es 0;
            conexionMysql.Open()
            Dim sql22 As String
            sql22 = "select count(idcaja) from caja;"
            Dim cmd22 As New MySqlCommand(sql22, conexionMysql)
            reader = cmd22.ExecuteReader
            reader.Read()
            cantidad = reader.GetString(0).ToString()
            conexionMysql.Close()
        Catch ex As Exception
            cantidad = 0
        End Try

        'si la cantidad es cero, entonces, significa que si puede abrir una caja, porque no hay nada aun.
        If cantidad = 0 Then


        Else


            Try

                conexionMysql.Open()
                Dim sql2 As String
                sql2 = "select count(idcaja) from caja where estado=0;"
                Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                reader = cmd2.ExecuteReader
                reader.Read()
                cantidad = reader.GetString(0).ToString()
                conexionMysql.Close()
            Catch ex As Exception
                cantidad = 1
            End Try
        End If

        'MsgBox(cantidad)
        If cantidad = 0 Then




            btnabrircajamenu.Visible = True
            btncerrarcajamenu.Visible = False
        Else
            btnabrircajamenu.Visible = False
            btncerrarcajamenu.Visible = True
        End If

        ''.-----------------------------------------------
        '  btnabrircajamenu.Visible = False

        '-actualizacion diciembre 2020, desactivamos la caja

        'Dim formulario As New FRabrircaja

        'formulario.ShowDialog()
        'Else
        'Dim respuesta As String
        'respuesta = MsgBox("Existen cajas abiertas sin cerrar, ¿deseas forzas cierre?, todo se pondrá en Ceros", MsgBoxStyle.YesNo, "CTRL+y")


        'If respuesta = vbYes Then

        '    cerrarconexion()

        '    conexionMysql.Open()
        '    'actualizo el dato
        '    Dim Sql5 As String
        '    Sql5 = "UPDATE `dwin`.`caja` SET `estado` = '1';"
        '    Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
        '    cmd5.ExecuteNonQuery()
        '    conexionMysql.Close()
        '    MsgBox("Cajas cerradas", MsgBoxStyle.Information, "CTR+y")
        '    Dim formulario As New FRabrircaja
        '    formulario.ShowDialog()
        '    '---------------------------------
        ' End If




        '  End If
    End Function
    Function inserciondedatosiniciales()
        Try


            Dim claveroot As String
            conexionMysql.Open()
            Dim Sqlx As String
            Sqlx = "select usuario from usuario where usuario= 'root';"
            Dim cmdx As New MySqlCommand(Sqlx, conexionMysql)
            reader = cmdx.ExecuteReader()
            reader.Read()
            Try
                claveroot = reader.GetString(0).ToString
            Catch ex2 As Exception
                claveroot = ""
            End Try

            cerrarconexion()
            conexionMysql.Close()


            If claveroot = "" Then

                Try
                    'si es su primer venta, entonces vamos a agregar automaticamente a un usuario...
                    cerrarconexion()
                    conexionMysql.Open()
                    Dim Sql2 As String
                    Sql2 = "INSERT INTO `tipo_usuario` (`tipo_usuario`, `tipo`) VALUES (1, 'ADMINISTRADOR');"
                    Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                    cmd2.ExecuteNonQuery()
                    cerrarconexion()
                    conexionMysql.Close()
                    'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
                    'MsgBox("USUARIO CREADO CORRECTAMENTE", MsgBoxStyle.Information, "Sistema")
                    conexionMysql.Close()
                    cerrarconexion()

                    cerrarconexion()
                    conexionMysql.Open()
                    Dim Sql3 As String
                    Sql3 = "INSERT INTO `tipo_usuario` (`tipo_usuario`, `tipo`) VALUES (2, 'USUARIO');"
                    Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
                    cmd3.ExecuteNonQuery()
                    conexionMysql.Close()

                    conexionMysql.Open()
                    Dim Sql4 As String
                    Sql4 = "INSERT INTO `impresora` (`idimpresora`, `nombre_impresora`) VALUES ('1', 'impresora');"
                    Dim cmd4 As New MySqlCommand(Sql4, conexionMysql)
                    cmd4.ExecuteNonQuery()
                    conexionMysql.Close()

                    'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
                    '  MsgBox("Registro guardado correctamente", MsgBoxStyle.Information, "Sistema")
                    conexionMysql.Close()

                    conexionMysql.Open()
                    Dim Sql As String
                    Sql = "INSERT INTO usuario (usuario, nombre, ap, am, telefono, correo, direccion, tipo_usuario) VALUES ('root', 'root', 'root', 'root', 'root', 'root', 'root', 1);"
                    Dim cmd As New MySqlCommand(Sql, conexionMysql)
                    cmd.ExecuteNonQuery()
                    conexionMysql.Close()
                    'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
                    'MsgBox("Registro guardado correctamente", MsgBoxStyle.Information, "Sistema")
                    conexionMysql.Close()


                    cerrarconexion()
                    conexionMysql.Open()
                    Dim Sql44 As String
                    Sql44 = "INSERT INTO `CLIENTE` (`idcliente`, `nombre`, `ap`, `am`, `rfc`, `direccion`, `telefono`, `correo`) VALUES ('1', 'USUARIO', 'USUARIO', 'USUARIO', '000', '000', '000', '000');"
                    Dim cmd44 As New MySqlCommand(Sql44, conexionMysql)
                    cmd44.ExecuteNonQuery()
                    conexionMysql.Close()

                    cerrarconexion()
                    conexionMysql.Open()
                    Dim Sql33 As String
                    Sql33 = "INSERT INTO `proveedor` (`nombre_empresa`, `nombre_encargado`, `ap_encargado`, `am_encargado`, `ciudad`, `estado`, `telefono`, `correo`) VALUES ('general', 'general', 'general', 'general', 'general', 'general', '00', '00');"
                    Dim cmd33 As New MySqlCommand(Sql33, conexionMysql)
                    cmd33.ExecuteNonQuery()
                    conexionMysql.Close()

                    cerrarconexion()
                    conexionMysql.Open()
                    Dim Sql34 As String
                    Sql34 = "INSERT INTO `tipoproducto` (`tipo`) VALUES ('general');"
                    Dim cmd34 As New MySqlCommand(Sql34, conexionMysql)
                    cmd34.ExecuteNonQuery()
                    conexionMysql.Close()



                    'AQUI INSERTAMOS LOS DATOS INICIALES DEL SERVICIO, QUE SON 3, 

                    conexionMysql.Open()
                    Dim Sql35 As String
                    Sql35 = "INSERT INTO `tipoventa` (`idtipoventa`,`tipoventa`) VALUES ('1','venta');"
                    Dim cmd35 As New MySqlCommand(Sql35, conexionMysql)
                    cmd35.ExecuteNonQuery()
                    conexionMysql.Close()
                    conexionMysql.Open()
                    Dim Sql36 As String
                    Sql36 = "INSERT INTO `tipoventa` (`idtipoventa`,`tipoventa`) VALUES ('2','servicios');"
                    Dim cmd36 As New MySqlCommand(Sql36, conexionMysql)
                    cmd36.ExecuteNonQuery()
                    conexionMysql.Close()
                    'MsgBox("se creo tipo de venta")
                    conexionMysql.Open()
                    Dim Sql37 As String
                    Sql37 = "INSERT INTO `tipoventa` (`idtipoventa`,`tipo`) VALUES ('3','vinil');"
                    Dim cmd37 As New MySqlCommand(Sql37, conexionMysql)
                    cmd37.ExecuteNonQuery()
                    conexionMysql.Close()


                    Try
                        conexionMysql.Open()

                        Dim Sql40 As String
                        Sql40 = "INSERT INTO `tipo_pago` (`idtipo_pago`, `tipo`) VALUES ('1', 'EFECTIVO');
INSERT INTO `dwin`.`tipo_pago` (`idtipo_pago`, `tipo`) VALUES ('2', 'DEPOSITO');
INSERT INTO `dwin`.`tipo_pago` (`idtipo_pago`, `tipo`) VALUES ('3', 'TRANSFERENCIA');
;"
                        Dim cmd40 As New MySqlCommand(Sql40, conexionMysql)
                        cmd40.ExecuteNonQuery()
                        conexionMysql.Close()

                    Catch ex As Exception
                        cerrarconexion()
                    End Try





                    MsgBox("EL SISTEMA CREO LOS REGISTROS INICIALES EXITOSAMENTE", MsgBoxStyle.Information, "Sistema")
                Catch ex3 As Exception
                    MsgBox("SISTEMA NO CREO LOS DATOS CORRECTAMENTE", MsgBoxStyle.Exclamation, "Sistema")

                End Try

            End If


        Catch ex As Exception
            MsgBox("UPSSS.... algo no esta bien!!!", MsgBoxStyle.Exclamation, "CTRL+y")
        End Try

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TabControl1.SelectedIndex = 0

        Button1.BackColor = Color.DimGray
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.FromArgb(27, 38, 44)
        Button14.BackColor = Color.FromArgb(27, 38, 44)
        Button38.BackColor = Color.FromArgb(27, 38, 44)
        'btnabrircajamenu.Visible = true
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        comprobarintegridadreparacion()
        lblistaproducto.Visible = False
        cargarlogoticket()
        slbcustomer.Visible = False
        cargarseller()
        TabControl1.SelectedIndex = 1
        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.DimGray
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.FromArgb(27, 38, 44)
        Button14.BackColor = Color.FromArgb(27, 38, 44)
        sgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue



        Button38.BackColor = Color.FromArgb(27, 38, 44)
        sgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        sgrilla.RowHeadersVisible = False
        'ampliar columna 
        'grillap.Columns(2).Width = 120


        sgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        sgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        obtenerfolio()
    End Sub

    Private Sub Button67_Click(sender As Object, e As EventArgs) Handles Button67.Click
        rgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill



        rgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        rcbitems.Visible = False
        rcbitemtemporal.Visible = True

        cargarlogoticket()
        If activarbusqueda = False Then
            cbstate.SelectedIndex = 0
            rtxtbusquedafolio.Visible = False
            cargarseller()
            obtenerfolio()

        Else
        End If
        'rcbseller.SelectedIndex = 0

        rlbcustomer.Visible = False
        lblistarespuestos.Visible = False

        TabControl1.SelectedIndex = 2
        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.DimGray
        Button14.BackColor = Color.FromArgb(27, 38, 44)
        Button38.BackColor = Color.FromArgb(27, 38, 44)

        lblistarespuestos.Visible = False
        comprobarintegridadreparacion()

    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'cargarseller()
        'reporteorderinicial()
        TabControl1.SelectedIndex = 3
        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.DimGray
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.FromArgb(27, 38, 44)
        Button14.BackColor = Color.FromArgb(27, 38, 44)
        Button38.BackColor = Color.FromArgb(27, 38, 44)
        ssgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        segrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        segrilla.Rows.Clear()

    End Sub

    Private Sub Button84_Click(sender As Object, e As EventArgs) Handles Button84.Click
        '  Private Sub abrir_inventario_existente()
        Dim filename As String
        Dim openfiler As New OpenFileDialog
        'Definiendo propiedades al openfiledialog
        With openfiler
            'directorio inicial
            .InitialDirectory = "C:\"
            'archivos que se pueden abrir
            .Filter = "Archivos de imágen(*.jpg)|*.png|All Files (*.*)|*.*"
            'indice del archivo de lectura por defecto
            .FilterIndex = 1
            'restaurar el directorio al cerrar el dialogo
            .RestoreDirectory = True
        End With
        '
        If openfiler.ShowDialog = Windows.Forms.DialogResult.OK Then  'Evalua si el usuario hace click en el boton abrir
            'Obteniendo la ruta completa del archivo xml
            filename = openfiler.FileName
            Me.pblogo.Image = Image.FromFile(filename)
            pblogo.SizeMode = PictureBoxSizeMode.Zoom
            pblogo.Visible = True

            txtrutaimagen.Text = pblogo.ImageLocation
            'filename = File.FileName
            'txtrutaimagen.Text = File.FileName
            '------------------------------
            filename = openfiler.FileName
            txtrutaimagen.Text = openfiler.FileName
            '-------------------------------

            If pblogo.Image Is Nothing Then
                ' el PB no tiene una imagen cargada
                pblogo.Visible = False
            Else
                ' SI tiene una imagen cargada
                pblogo.Visible = True

            End If



        End If
        'End Sub


        'Dim nuevaruta As String
        'nuevaruta = Replace(txtrutaimagen.Text, "\", "\\")


        'conexionMysql.Open()

        'Dim Sql36 As String
        'Sql36 = "UPDATE `dwin`.`datos_empresa` SET `ruta_logo` = '" & nuevaruta & "' WHERE (`iddatos_empresa` = '1');"
        'Dim cmd36 As New MySqlCommand(Sql36, conexionMysql)
        'cmd36.ExecuteNonQuery()
        'conexionMysql.Close()

        'MsgBox(txtrutaimagen.Text)

    End Sub
    Private Function Bytes_Imagen(ByVal Imagen As Byte()) As Image
        Try
            'si hay imagen
            If Not Imagen Is Nothing Then
                'caturar array con memorystream hacia Bin
                Dim Bin As New MemoryStream(Imagen)
                'con el método FroStream de Image obtenemos imagen
                Dim Resultado As Image = Image.FromStream(Bin)
                'y la retornamos
                Return Resultado
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Function Imagen_Bytes(ByVal Imagen As Image) As Byte()
        'si hay imagen
        If Not Imagen Is Nothing Then
            'variable de datos binarios en stream(flujo)
            Dim Bin As New MemoryStream
            'convertir a bytes
            Imagen.Save(Bin, Imaging.ImageFormat.Jpeg)
            'retorna binario
            Return Bin.GetBuffer
        Else
            Return Nothing
        End If
    End Function
    Private Sub Button83_Click(sender As Object, e As EventArgs) Handles Button83.Click

        cerrarconexion()

        'Try
        Dim res As Integer

        Try

            conexionMysql.Open()
            Dim sql As String = "insert into logo_empresa(idlogo_empresa,logo)values(1,?imagen)"
            'conexionMysql = New MySqlConnection(StrConexion)
            Dim Comando As New MySqlCommand(sql, conexionMysql)
            Dim Imag As Byte()
            Imag = Imagen_Bytes(Me.pblogo.Image)

            Comando.Parameters.AddWithValue("?imagen", Imag)

            'conexionMysql.Open()
            'If conexionMysql.State = ConnectionState.Open Then
            Comando.ExecuteNonQuery()
            'End If
            conexionMysql.Close()

            res = 0

        Catch ex As Exception
            res = 1
        End Try



        If res = 1 Then

            ' Try
            MsgBox("Ya existe una imagen en el sistema, será remplazada", MsgBoxStyle.Information, "CTRL+y")
            cerrarconexion()
            conexionMysql.Open()

            Dim sql2 As String
            sql2 = "delete from  logo_empresa"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()

            conexionMysql.Open()
            Dim sql As String = "insert into logo_empresa(idlogo_empresa,logo)values(1,?imagen)"
            'conexionMysql = New MySqlConnection(StrConexion)
            Dim Comando As New MySqlCommand(sql, conexionMysql)

            Dim Imag As Byte()
            Imag = Imagen_Bytes(Me.pblogo.Image)

            Comando.Parameters.AddWithValue("?imagen", Imag)

            'conexionMysql.Open()
            'If conexionMysql.State = ConnectionState.Open Then
            Comando.ExecuteNonQuery()
            'End If
            conexionMysql.Close()



            Dim nuevaruta As String
            nuevaruta = Replace(txtrutaimagen.Text, "\", "\\")
            ' MsgBox("nuevaruta" & nuevaruta)

            cerrarconexion()
            conexionMysql.Open()
            'Try

            Dim Sql36 As String
            Sql36 = "UPDATE `datos_empresa` SET `ruta_logo` = '" & nuevaruta & "' WHERE (`iddatos_empresa` = '1');"
            Dim cmd36 As New MySqlCommand(Sql36, conexionMysql)
            cmd36.ExecuteNonQuery()
            conexionMysql.Close()

            'MsgBox(txtrutaimagen.Text)
            '-------------------------------------------------------------------------------------------------------

            '--------ALMACENAMOS LA RUTA EN EL ARCHIVO DE TEXTO

            Try


                Dim rutaimagen As String

                'IP = InputBox("Ingresa la ip del servidor de la BD")
                rutaimagen = nuevaruta


                Dim lines() As String = {rutaimagen}
                ' Dim lines2() As String = {port}

                ' Set a variable to the My Documents path.
                Dim mydocpath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                ' Write the string array to a new file named "WriteLines.txt".
                Using outputFile As New StreamWriter(mydocpath & Convert.ToString("\rutaImagenNoBorrar.txt"))
                    For Each line As String In lines
                        outputFile.WriteLine(line)
                    Next
                End Using
            Catch ex As Exception
                MsgBox("Error al generar el archivo .txt", MsgBoxStyle.Information, "CTRL+y")
            End Try


            '-----------------------------------------------




            ' MsgBox("Logotipo Guardado", MsgBoxStyle.Information, "CTRL+y")




            '----------------------------------------------------------------------------------------------------------
            MsgBox("Logotipo Guardado", MsgBoxStyle.Information, "CTRL+y")
            cargarlogoticket()


        End If



        'Dim file As New OpenFileDialog()
        'Dim imag As Byte

        'file.Filter = "Archivo JPG|*.jpg"
        'If file.ShowDialog() = DialogResult.OK Then
        '    pblogo.Image = Image.FromFile(file.FileName)

        '    Imag = Imagen_Bytes(Me.pblogo.Image)

        '    ' lbnombreimagen.Text = Path.GetFileName(pblogo.Tag.ToString())
        'End If
    End Sub

    Private Sub Button87_Click(sender As Object, e As EventArgs) Handles Button87.Click
        cerrarconexion()

        Try

            conexionMysql.Open()

            Dim sql2 As String
            sql2 = "delete from  logo_empresa"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()

            ' Liberamos los recursos utilizados por el objeto Image
            'Try



            'pblogo.Image.Dispose()
            'pblogo.Visible = False

            'Catch ex As Exception

            'End Try

            ' Establecemos a Nothing el valor de la propiedad Image
            ' del control PictureBox.






            cargarlogook()
            MsgBox("Logotipo Eliminado", MsgBoxStyle.Information, "Sistema")
            'comprobarexistenciadelogo()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub
    Function obtenerfolio()
        Dim folio As Integer

        Try

            cerrarconexion()


            'grilla.Rows.Clear()
            conexionMysql.Open()
            Dim sql2 As String
            sql2 = "select max(idventa) from venta;"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            reader = cmd2.ExecuteReader()
            reader.Read()
            folio = reader.GetString(0).ToString
            lbfolio.Text = folio + 1
            slbfolio.Text = folio + 1


            conexionMysql.Close()
            'MsgBox("si hay folio")

        Catch ex As Exception
            lbfolio.Text = folio + 1
            slbfolio.Text = folio + 1
            MsgBox("Al parecer será tu primer venta, Esperemos todo marche bien", MsgBoxStyle.Information, "Sistema")
            'se insertan los datos iniciales para que comienze a trabajar el sistema
            ' inserciondedatosiniciales()

            cerrarconexion()


            'Dim claveroot As String
            'conexionMysql.Open()
            'Dim Sqlx As String
            'Sqlx = "select usuario from usuario where usuario= 'root';"
            'Dim cmdx As New MySqlCommand(Sqlx, conexionMysql)
            'reader = cmdx.ExecuteReader()
            'reader.Read()
            'Try
            '    claveroot = reader.GetString(0).ToString
            'Catch ex2 As Exception
            '    claveroot = ""
            'End Try

            'cerrarconexion()
            'conexionMysql.Close()


            'If claveroot = "" Then
            '    conexionMysql.Open()
            '    Dim Sql As String
            '    Sql = "INSERT INTO usuario (usuario, nombre, ap, am, telefono, correo, direccion, tipo_usuario) VALUES ('root', 'root', 'root', 'root', 'root', 'root', 'root', 1);"
            '    Dim cmd As New MySqlCommand(Sql, conexionMysql)
            '    cmd.ExecuteNonQuery()
            '    conexionMysql.Close()
            '    'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
            '    MsgBox("Registro guardado correctamente", MsgBoxStyle.Information, "Sistema")
            '    conexionMysql.Close()


            '    cerrarconexion()
            '    conexionMysql.Open()
            '    Dim Sql4 As String
            '    Sql4 = "INSERT INTO `dwin`.`CLIENTE` (`idcliente`, `nombre`, `ap`, `am`, `edad`, `direccion`, `telefono`, `correo`) VALUES ('1', 'USUARIO', 'USUARIO', 'USUARIO', '000', '000', '000', '000');"
            '    Dim cmd4 As New MySqlCommand(Sql4, conexionMysql)
            '    cmd4.ExecuteNonQuery()
            '    conexionMysql.Close()
            '    MsgBox("Registro guardado correctamente", MsgBoxStyle.Information, "Sistema")

            'End If


        End Try

    End Function
    Function limpiarventa()
        txtname.Text = ""
        txtcity.Text = ""
        txtaddress.Text = ""
        txtstate.Text = ""
        txttelephone.Text = ""
        txtclaveproducto.Text = ""
        txtprice.Text = ""
        txtpiece.Text = ""
        txtnombreproducto.Text = ""
        stxttotalfinal.Text = ""

    End Function
    Function sventa()
        Dim ii As Integer = sgrilla.RowCount

        'Try

        If stxttotalfinal.Text = "0" Or ii <= 0 Or cbseller.Text = "" Then
            MsgBox("No hay ventas que realizar", MsgBoxStyle.Information, "Sistema")
            stxttotalfinal.Text = ""
        Else
            'obtener fecha y hora

            Dim dia, mes, año, fecha, fechaclave As String
            hora2 = Now.Hour()
            minuto = Now.Minute()
            segundo = Now.Second()

            hora = hora2 & ":" & minuto & ":" & segundo

            dia = Date.Now.Day
            mes = Date.Now.Month
            año = Date.Now.Year
            fecha = año & "-" & mes & "-" & dia

            fechaclave = año & mes & dia & hora2 & minuto & segundo

            Dim fechacalendarioentrega As String
            fechacalendarioentrega = rcalendario.Value.ToString("yyyy/MM/dd")
            '------------------ insertar reginstro en tabla venta ---------------------------------------

            cerrarconexion()
            Dim idcliente As Integer

            idcliente = 1

            Try
                cerrarconexion()
                conexionMysql.Open()
                Dim Sql31 As String
                'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                Sql31 = "select idcustomer from customer where name like '%" & txtname.Text & "%';"
                Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
                reader = cmd31.ExecuteReader()
                reader.Read()
                idcliente = reader.GetString(0).ToString()
                conexionMysql.Close()
                'en caso de que lo obtenga, obtenemos el ID, en caso contrario guardamos el dato
                'MsgBox(idcliente)
            Catch ex As Exception
                ' idcliente = 1
                cerrarconexion()

                'en caso de que no este el cliente, lo guardamos
                conexionMysql.Open()
                Dim Sqlx1 As String
                Sqlx1 = "INSERT INTO `customer` (`name`, `address`, `state`, `city`, `telephone`,`email`) VALUES ( '" & txtname.Text & "', '" & txtaddress.Text & "', '" & txtstate.Text & "', '" & txtcity.Text & "', '" & txttelephone.Text & "','" & stxtemail.Text & "');"
                Dim cmdx1 As New MySqlCommand(Sqlx1, conexionMysql)
                cmdx1.ExecuteNonQuery()
                conexionMysql.Close()
                'txttotalpagar.Text = ""
                conexionMysql.Close()

                '------------------------obtenemos el id del cliente
                cerrarconexion()
                conexionMysql.Open()
                Dim Sqlx2 As String
                'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                Sqlx2 = "select idcustomer from customer where name like '%" & rtxtcustomername.Text & "%';"
                Dim cmdx2 As New MySqlCommand(Sqlx2, conexionMysql)
                reader = cmdx2.ExecuteReader()
                reader.Read()
                idcliente = reader.GetString(0).ToString()
                conexionMysql.Close()

                MsgBox("Customer insertado", MsgBoxStyle.Information, "MOBI")


                '-MsgBox(idcliente)
            End Try


            '----------------insertamos los valores del equipo y del ------------------
            'cerrarconexion()
            'conexionMysql.Open()
            'Dim Sql2 As String
            'Sql2 = "INSERT INTO `mobi`.`equipo` (`equipo`, `modelo`, `imei`, `password`, `status`, `problema`, `nota`, `idcustomer`) VALUES ('" & rtxtequipo.Text & "', '" & rtxtmodelo.Text & "', '" & rtxtimei.Text & "', '" & rtxtpassword.Text & "', '" & cbstate.Text & "', '" & rtxtproblem.Text & "', '" & rtxtnote.Text & "', '" & idcliente & "');"
            'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            'cmd2.ExecuteNonQuery()
            'conexionMysql.Close()
            ''txttotalpagar.Text = ""
            'conexionMysql.Close()
            ''--------------
            Dim idequipo, idseller As Integer
            ''--------------
            'cerrarconexion()
            'conexionMysql.Open()
            'Dim Sqlx3 As String
            ''consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            'Sqlx3 = "select idequipo from equipo where equipo like '%" & rtxtequipo.Text & "%';"
            'Dim cmdx3 As New MySqlCommand(Sqlx3, conexionMysql)
            'reader = cmdx3.ExecuteReader()
            'reader.Read()
            'idequipo = reader.GetString(0).ToString()
            'conexionMysql.Close()
            ''-----------------------------------------------------------------------------------

            cerrarconexion()
            conexionMysql.Open()
            Dim Sqlx4 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sqlx4 = "select idseller from seller where name_seller='" & cbseller.Text & "';"
            Dim cmdx4 As New MySqlCommand(Sqlx4, conexionMysql)
            reader = cmdx4.ExecuteReader()
            reader.Read()
            idseller = reader.GetString(0).ToString()
            conexionMysql.Close()

            ' idequipo = 0

            '            Dim ii As Integer = rgrilla.RowCount
            '           ii = rgrilla.RowCount

            'If ii = 0 Then
            'MsgBox("No hay nada que vender")

            'Else


            cerrarconexion()
            conexionMysql.Open()
            Dim Sql As String
            'Sql = "INSERT INTO `mobi`.`venta` (`idventa`, `idcustomer`,  `idseller`, `total`, `fechaventa`, `fechaentrega`, `costo_reparacion`, `deposito`, `resto`,`tipoventa`) VALUES ('" & lbfolio.Text & "', '" & idcliente & "', '" & idseller & "', '" & stxttotalfinal.Text & "', '" & fecha & "', '" & fecha & "', '0', '0', '0',1);"
            Sql = "INSERT INTO `venta` (`idventa`, `idcustomer`,  `idseller`, `total`, `fechaventa`, `fechaentrega`, `deposito`, `resto`,`tipoventa`,`hora`) VALUES ('" & lbfolio.Text & "', '" & idcliente & "', '" & idseller & "', '" & stxttotalfinal.Text & "', '" & fecha & "', '" & fecha & "', '0', '0',1,'" & hora & "');"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            cmd.ExecuteNonQuery()
            conexionMysql.Close()
            'txttotalpagar.Text = ""
            conexionMysql.Close()


            '------------------ FIN insertar reginstro en tabla venta ---------------------------------------


            '------------------ insertar reginstro en tabla ventaIND ---------------------------------------
            Dim i As Integer = sgrilla.RowCount
            ' MsgBox(i)
            Dim clave As String
            Dim actividad As String
            Dim cantidad, precio, total As Double
            Dim producto, observaciones As String
            Dim j As Integer
            conexionMysql.Open()

            'suma de valores
            For j = 0 To i - 1
                'MsgBox("valosr de j:" & j)
                'a = venta.grillaventa.Item(j, 3).Value.ToString()
                clave = sgrilla.Rows(j).Cells(0).Value 'descripcion
                producto = sgrilla.Rows(j).Cells(1).Value 'cantidad
                precio = sgrilla.Rows(j).Cells(2).Value 'costo
                cantidad = sgrilla.Rows(j).Cells(3).Value
                total = sgrilla.Rows(j).Cells(4).Value
                cerrarconexion()
                '  MsgBox("insertar")

                '                If clave = "00" Then
                '               clave = fechaclave
                '      End If

                'MsgBox("el producto es:" & actividad)
                conexionMysql.Open()
                Dim Sql25 As String
                Sql25 = "INSERT INTO venta_ind (actividad, cantidad, costo, idventa,idproducto) VALUES ('" & producto & "'," & cantidad & "," & precio & "," & lbfolio.Text & ",'" & clave & "');"
                Dim cmd25 As New MySqlCommand(Sql25, conexionMysql)
                cmd25.ExecuteNonQuery()
                conexionMysql.Close()

                'conexionMysql.Open()
                'Dim Sql25 As String
                'Sql25 = "INSERT INTO venta_ind (actividad, cantidad, costo, idventa,idproducto,name_item) VALUES ('" & rtxtnombre.Text & "'," & rtxtcantidad.Text & "," & rtxtprecio.Text & "," & folio & ",'" & claveproducto & "','" & rtxtequipo.Text & "');"
                'Dim cmd25 As New MySqlCommand(Sql25, conexionMysql)
                'cmd25.ExecuteNonQuery()
                'conexionMysql.Close()




            Next

            '----------------------------- se hace actualización a la tabla de productos--------------
            Dim totalactualizar, m, n As Integer
            'cerrarconexion()

            'conexionMysql.Open()
            'Dim Sql3 As String
            ''consultamos cuantos registros se insertaros para posteriormente actualizarlos en su registro original
            'Sql3 = "select count(distinct idproducto) from ventaind where idventa=" & lbfolio.Text & " and idproducto <> '';"
            'Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            'reader = cmd3.ExecuteReader()
            'reader.Read()
            'totalactualizar = reader.GetString(0).ToString()
            'conexionMysql.Close()

            'for para dar las vueltas necesarias para poder actualizar
            '---------------DECLARACION DE VARIABLES------------------
            Dim claveproducto, cantidadpro As Integer
            Dim matriz(totalactualizar, 3) As String
            '-------------------------------------------
            cantidadpro = 0
            '----------------------------- se hace actualización a la tabla de productos--------------
            'cerrarconexion()
            'conexionMysql.Open()
            'Dim Sql4 As String
            ''consultamos cuantos registros se insertaros para posteriormente actualizarlos en su registro original
            'Sql4 = "select idproducto, sum(cantidad) from ventaind where idventa=" & lbfolio.Text & "  group by idproducto;"
            'Dim cmd4 As New MySqlCommand(Sql4, conexionMysql)
            'reader = cmd4.ExecuteReader()




            'For m = 1 To totalactualizar
            '    reader.Read()


            '    'guardamos los valores en una matriz
            '    matriz(m, 0) = reader.GetString(0).ToString()
            '    matriz(m, 1) = reader.GetString(1).ToString()
            '    'MsgBox("el registro de la matriz(idproducto):" & matriz(m, 0))
            '    'MsgBox("el registro de la matriz(cantidad):" & matriz(m, 1))
            '    'MsgBox(matriz(m, 0))
            '    'MsgBox(matriz(m, 1))

            'Next
            'conexionMysql.Close()

            '-----------------------consultamos cuantos valores existes de tal idproducto

            Dim x, xcantidad As Integer

            'For x = 1 To totalactualizar
            '    cerrarconexion()

            '    conexionMysql.Open()
            '    xcantidad = 0

            '    'MsgBox("el producto es:" & matriz(x, 0))
            '    Dim Sql6 As String
            '    'consultamos cuantos registros se insertaros para posteriormente actualizarlos en su registro original
            '    Sql6 = "select cantidad,idproducto from producto where idproducto='" & matriz(x, 0) & "';"
            '    Dim cmd6 As New MySqlCommand(Sql6, conexionMysql)
            '    reader = cmd6.ExecuteReader()

            '    reader.Read()
            '    'guardamos los valores en una matriz
            '    xcantidad = reader.GetString(0).ToString()
            '    matriz(x, 1) = xcantidad - matriz(x, 1)
            '    conexionMysql.Close()
            'Next
            '---------------------------------------------------------------------------------------
            'For n = 1 To totalactualizar
            '    '-----------------------ahora sacamos los valores de la matriz para poder actualizarlos
            '    '--------------------------------------------------------------------------------------------
            '    cerrarconexion()

            '    conexionMysql.Open()
            '    'actualizo el dato
            '    Dim Sql5 As String
            '    Sql5 = "UPDATE producto SET cantidad=" & matriz(n, 1) & " WHERE idproducto='" & matriz(n, 0) & "';"
            '    Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
            '    cmd5.ExecuteNonQuery()
            '    conexionMysql.Close()
            '    ' MsgBox("registro actualizado")
            '    '-------------------------------------------------------------------------------------------------
            'Next
            '-------------------------------------------------------------------------------------------
            '--------------------------------------- se manda a imprimir el reporte



            'If chcalcularcambio.Checked = True Then

            '    '-------------------NUEVO SISTEMA

            '    frcambio.ShowDialog()
            '    frcambio.txtpaga.Focus()


            'Else

            imprimir()

            MsgBox("Venta realizada", MsgBoxStyle.Information, "Sistema")


            slimpiartodo()


            ' End If


            'txtcliente.Text = "USUARIO"
            'listaclientes.Visible = False
            'If chimpresionticket.Checked = True Then
            'impresionticket()

            'vamos a trabajar con el ticket

            'imprimir()






            'End If
            '   If chimpresion.Checked = True Then
            '//////////////////////////////////////////////////////
            ' frx.Show()
            'cargamos el formulario 2, temporalmente para ver si funcionan los reportes.
            ''    Form2.TextBox1.Text = lbfolio.Text
            'Dim newformulario As New frnotaventa
            'aqui le cambien por el formulario de prueba, para coregir
            'el problema de los reportes.
            'newformulario.Show()
            'cagamos los datos a la nota de venta primeroa



            'informe1.Show()





            'este codigo si funcionaba
            '  cargardatosnotaventa()
            ' FRNOTAVENTA.ShowDialog()
            'imprimirnotaventa()
            'End If

        End If

        'una vez guardada la venta, se obtiene un nuevo folio para la proxima venta
        obtenerfolio()





        '------------------------------------------------------------------------
        'al finalizar se limpia la grilla y las cajas de texto
        ' txttotalpagar.Text = ""
        'txtunidades.Text = ""
        'grilla.Rows.Clear()
        'txttotalpagar.Text = ""

        'llamamos para obtener un nuevo folio


        'End If

        '------------------ FIN insertar reginstro en tabla ventaIND ---------------------------------------
        'Catch ex As Exception
        '    comprobartipoingreso()
        '    MsgBox(ex.Message)
        'End Try

    End Function
    Function slimpiartodo()
        txtname.Text = ""
        txtstate.Text = ""
        txtcity.Text = ""
        txtaddress.Text = ""
        txttelephone.Text = ""
        txtclaveproducto.Text = ""
        txtprice.Text = ""
        txtpiece.Text = ""
        stxttotal.Text = ""
        stxttotalfinal.Text = ""

        cbseller.SelectedIndex = 0

        sgrilla.Rows.Clear()

    End Function
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click


        If statusbusquedaventa = True Then
            cancelartodoventa()
        Else


            'comentamos la caja,

            Dim estadocaja As Integer


            estadocaja = comprobarcaja()

            If estadocaja = 0 Then

                'obtenerfolio()
                ' venta()
                ' RPT1.Show()
                sventa()
            Else
                MsgBox("No existe ninguna caja abierta", MsgBoxStyle.Information, "CTRL+y")
            End If








        End If
        'aqui vamos a guardar el registro



        'If stxttotalfinal.Text = "" Or txtclaveproducto.Text = "" Or txtname.Text = "" Or cbseller.Text = "" Then
        '    MsgBox("Hace falta información", MsgBoxStyle.Information, "MOBICOMPU")
        'Else



        '    Dim fecha, dia, mes, año As String
        '    Dim valor As Double

        '    dia = Date.Now.Day
        '    mes = Date.Now.Month
        '    año = Date.Now.Year
        '    fecha = año & "-" & mes & "-" & dia
        '    Dim idseller As Integer


        '    Try

        '        '---------------------------------
        '        '--------OBTENEMOS EL ID DEL TIPO DE PAGO DE LA VENTA
        '        conexionMysql.Open()
        '        Dim Sql322 As String
        '        'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        '        Sql322 = "SELECT idseller from seller where seller='" & cbseller.Text & "';"
        '        Dim cmd322 As New MySqlCommand(Sql322, conexionMysql)
        '        reader = cmd322.ExecuteReader()
        '        reader.Read()
        '        idseller = reader.GetString(0).ToString()
        '        '-------------------------------------------------
        '        '----------datos que se van al reporte

        '        conexionMysql.Close()


        '    Catch ex As Exception
        '        cerrarconexion()
        '    End Try




        '    Try

        '        cerrarconexion()
        '        conexionMysql.Open()
        '        Dim Sql2 As String
        '        Sql2 = "INSERT INTO `mobi`.`venta` (`idventa`, `customer_name`, `address`, `estate`, `city`, `telephone`, `product`, `piece`, `description`, `price`, `total`, `idseller`, `fechaventa`) VALUES ('" & lbfolio.Text & "', '" & txtname.Text & "', '" & txtaddress.Text & "', '" & txtstate.Text & "', '" & txtcity.Text & "', '" & txttelephone.Text & "', '" & txtclaveproducto.Text & "', '" & txtpiece.Text & "', '" & txtnombreproducto.Text & "', '" & txtprice.Text & "', '" & stxttotalfinal.Text & "', '" & idseller & "', '" & fecha & "');"
        '        Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
        '        cmd2.ExecuteNonQuery()
        '        conexionMysql.Close()
        '        MsgBox("Venta realizada", MsgBoxStyle.Information, "CTRL+y")
        '        limpiarventa()
        '        obtenerfolio()
        '    Catch ex As Exception
        '        cerrarconexion()
        '        '    MsgBox("error")
        '    End Try

        'End If


    End Sub
    Function comprobarcaja()
        Dim registrar As Boolean
        Dim idestado, idmaximo, cantidad As Integer

        '---------------------------
        'PRIMERO COMPROBAMOS QUE EXISTE UNA CAJA, PRIMER REGISTRO
        '------------------------------
        Try
            cerrarconexion()

            conexionMysql.Open()
            Dim sql22 As String
            sql22 = "select count(idcaja) from caja;"
            Dim cmd22 As New MySqlCommand(sql22, conexionMysql)
            reader = cmd22.ExecuteReader
            reader.Read()
            cantidad = reader.GetString(0).ToString()
            conexionMysql.Close()

            '---------------------------
        Catch ex As Exception
            'cantidad = 0
        End Try


        If cantidad <= 0 Then
            registrar = False
            idestado = 1
            'MsgBox("primer")
            Return idestado
        Else

            Try
                'todos los datos son obtenidos con la fecha actual para evitar conflictos
                Dim dia, mes, año, fecha, fechacaja, horacaja As String
                Dim hora2, minuto, segundo, hora As String
                hora2 = Now.Hour()
                minuto = Now.Minute()
                segundo = Now.Second()

                hora = hora2 & ":" & minuto & ":" & segundo

                dia = Date.Now.Day
                mes = Date.Now.Month
                año = Date.Now.Year
                fecha = año & "-" & mes & "-" & dia
                Dim fechahoy As String

                '---------------------------
                'PRIMERO OBTENERMOS EL MAYOR ID
                '------------------------------
                conexionMysql.Open()
                Dim sql2 As String
                sql2 = "select max(idcaja)as maximo from caja;"
                Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                reader = cmd2.ExecuteReader
                reader.Read()
                idmaximo = reader.GetString(0).ToString()
                conexionMysql.Close()
                '---------------------------

                conexionMysql.Open()
                Dim sql25 As String
                sql25 = "select estado from caja where idcaja=" & idmaximo & ";"
                Dim cmd25 As New MySqlCommand(sql25, conexionMysql)
                reader = cmd25.ExecuteReader
                reader.Read()
                idestado = reader.GetString(0).ToString()
                conexionMysql.Close()
                registrar = False
                'MsgBox("No existe caja abierta", MsgBoxStyle.Exclamation, "CTRL+y")

                ' MsgBox("segundo")
                Return idestado

            Catch ex As Exception
                registrar = True
                ' MsgBox("error")
            End Try


        End If

        'If idestado = 0 Then
        '    'en caso de que exista una caja abierta, entonces abrimos la ventana para cerrar la caja
        '    Dim formulario As New FRcerrarcaja
        '    formulario.ShowDialog()

        'End If



    End Function
    Private Sub imprimir()

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
            'ex.ToString()
        End Try

    End Sub
    Private Sub imprimirreparacion()

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
            '  MsgBox(impresora)
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
            PrintDialog1.Document = PrintDocument2
            PrintDialog1.PrinterSettings.PrinterName = impresora
            If PrintDocument2.PrinterSettings.IsValid Then
                PrintDocument2.Print() 'Imprime texto 
            Else
                MsgBox("Impresora invalida", MsgBoxStyle.Exclamation, "CTRL+y")
                'MessageBox.Show("La impresora no es valida")
            End If
            '--------------------------------------------------- 
        Catch ex As Exception
            MsgBox("Impresora fuera de linea", MsgBoxStyle.Exclamation, "CTRL+y")
            cerrarconexion()
            'MessageBox.Show("Hay un problema de impresión",
            ex.ToString()
        End Try

    End Sub
    Private Sub imprimirreparacionseller()

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
            PrintDialog1.Document = PrintDocument4
            PrintDialog1.PrinterSettings.PrinterName = impresora
            If PrintDocument4.PrinterSettings.IsValid Then
                PrintDocument4.Print() 'Imprime texto 
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

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim nameseller As String
        Try

            cerrarconexion()

            conexionMysql.Open()
            Dim Sql As String
            Sql = "select name_seller  from seller where name_seller='" & conftxtseller.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            nameseller = reader.GetString(0).ToString()

            conexionMysql.Close()
            'MsgBox(nameseller)
            'MsgBox(conftxtseller.Text)
        Catch ex As Exception
            nameseller = ""
            cerrarconexion()
            '   MsgBox("    ERROR")
        End Try



        If nameseller = conftxtseller.Text Then
            MsgBox("the selles ready exist", MsgBoxStyle.Information, "MOBICOMPU")


        Else


            Try

                cerrarconexion()
                conexionMysql.Open()
                Dim Sql2 As String
                Sql2 = "INSERT INTO `seller` (`name_seller`) VALUES ('" & conftxtseller.Text & "');"
                Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                cmd2.ExecuteNonQuery()
                conexionMysql.Close()
                MsgBox("Registro guardado", MsgBoxStyle.Information, "CTRL+y")
                cargarseller()
                conftxtseller.Text = ""
                'obtenerfolio()
            Catch ex As Exception

            End Try
        End If

    End Sub
    Function cargarseller()


        'limpiar el combo para que no se duplique
        confcbseller.Items.Clear()
        cbseller.Items.Clear()
        rcbseller.Items.Clear()
        scbseller.Items.Clear()

        Try


            Dim cantidadproveedor, i As Integer
            cerrarconexion()

            conexionMysql.Open()
            Dim Sql As String
            Sql = "select count(*)as contador from seller;"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            cantidadproveedor = reader.GetString(0).ToString()

            conexionMysql.Close()


            cerrarconexion()

            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "select * from seller;"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            reader = cmd2.ExecuteReader()

            For i = 1 To cantidadproveedor

                reader.Read()

                confcbseller.Items.Add(reader.GetString(1).ToString())
                cbseller.Items.Add(reader.GetString(1).ToString())
                rcbseller.Items.Add(reader.GetString(1).ToString())
                scbseller.Items.Add(reader.GetString(1).ToString())

            Next



            confcbseller.SelectedIndex = 0
            cbseller.SelectedIndex = 0
            rcbseller.SelectedIndex = 0
            scbseller.SelectedIndex = 0

            reader.Close()

            conexionMysql.Close()
            confcbseller.Text = ""

        Catch ex As Exception
            cerrarconexion()
        End Try



    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try

            conexionMysql.Open()

            Dim sql2 As String
            sql2 = "delete from seller where name_seller='" & confcbseller.Text & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()
            cargarseller()
        Catch ex As Exception
            cerrarconexion()
        End Try

    End Sub
    Function calculartotalventa()
        Try
            stxttotal.Text = CDbl(txtprice.Text) * CDbl(txtpiece.Text)
        Catch ex As Exception
            stxttotal.Text = ""
        End Try

    End Function
    Private Sub Txtpiece_TextChanged(sender As Object, e As EventArgs) Handles txtpiece.TextChanged
        calculartotalventa()
    End Sub

    Private Sub Txtprice_TextChanged(sender As Object, e As EventArgs) Handles txtprice.TextChanged
        calculartotalventa()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub txtpiece_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpiece.KeyPress

        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
        'If Not IsNumeric(e.KeyChar) Then
        '    e.Handled = True

        'End If
    End Sub

    Private Sub txtprice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtprice.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim nameseller As String
        Try

            cerrarconexion()

            conexionMysql.Open()
            Dim Sql As String
            Sql = "select name_seller  from state_device where state='" & conftxtseller.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            nameseller = reader.GetString(0).ToString()

            conexionMysql.Close()
            'MsgBox(nameseller)
            'MsgBox(conftxtseller.Text)
        Catch ex As Exception
            nameseller = ""
            cerrarconexion()
            '   MsgBox("    ERROR")
        End Try



        If nameseller = conftxtseller.Text Then
            MsgBox("the selles ready exist", MsgBoxStyle.Information, "MOBICOMPU")


        Else


            Try

                cerrarconexion()
                conexionMysql.Open()
                Dim Sql2 As String
                Sql2 = "INSERT INTO `seller` (`name_seller`) VALUES ('" & conftxtseller.Text & "');"
                Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                cmd2.ExecuteNonQuery()
                conexionMysql.Close()
                MsgBox("Registro guardado", MsgBoxStyle.Information, "CTRL+y")
                cargarseller()
                conftxtseller.Text = ""
                'obtenerfolio()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles rtxtcustomername.TextChanged
        'Dim idcliente As Integer

        'Try
        '    cerrarconexion()
        '    conexionMysql.Open()
        '    Dim Sql31 As String
        '    'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        '    Sql31 = "select name from customer where name like '%" & rtxtcustomername.Text & "%';"
        '    Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
        '    reader = cmd31.ExecuteReader()
        '    reader.Read()
        '    idcliente = reader.GetString(0).ToString()
        '    conexionMysql.Close()
        '    'MsgBox(idcliente)
        'Catch ex As Exception
        '    idcliente = 1
        '    '-MsgBox(idcliente)
        'End Try


        rtxtcustomername.BackColor = Color.White
        If rtxtcustomername.Text = "" Then
            rlbcustomer.Visible = False
            rlbcustomer.Items.Clear()
        Else


            rlbcustomer.Visible = True
            rlbcustomer.Items.Clear()


            'cerramos la conexion
            cerrarconexion()

            Dim cantidad, i As Integer
            cantidad = 0
            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "select count(*) from customer where name like '%" & rtxtcustomername.Text & "%';"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            reader = cmd2.ExecuteReader
            reader.Read()
            cantidad = reader.GetString(0).ToString

            'cargamos el formulario  resumen
            conexionMysql.Close()

            'MsgBox("hay tantos resultados:" & cantidad)

            cerrarconexion()

            If cantidad = 0 Then
                rlbcustomer.Visible = False
            Else



                conexionMysql.Open()
                Dim Sql As String
                Sql = "select name as nombre from customer where name like '%" & rtxtcustomername.Text & "%';"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader
                For i = 1 To cantidad
                    reader.Read()

                    rlbcustomer.Items.Add(reader.GetString(0).ToString)
                Next


                conexionMysql.Close()
                'Catch ex As Exception
            End If

            'End Try
        End If

    End Sub
    Function rcargarcliente()
        Dim idcliente As Integer
        Try
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31 = "select name from customer where name like '%" & rtxtcustomername.Text & "%';"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            idcliente = reader.GetString(0).ToString()
            conexionMysql.Close()
            'MsgBox(idcliente)
        Catch ex As Exception
            idcliente = 1
            '-MsgBox(idcliente)
        End Try

    End Function

    Private Sub Rtxtcostorepuesto_TextChanged(sender As Object, e As EventArgs) Handles rtxtnombre.TextChanged


        If rtxtnombre.Text = "" Then
            lblistarespuestos.Visible = False
            lblistarespuestos.Items.Clear()
        Else


            lblistarespuestos.Visible = True
            lblistarespuestos.Items.Clear()


            'cerramos la conexion
            cerrarconexion()

            Dim cantidad, i As Integer
            cantidad = 0
            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "select count(*) from producto where name like '%" & rtxtnombre.Text & "%';"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            reader = cmd2.ExecuteReader
            reader.Read()
            cantidad = reader.GetString(0).ToString

            'cargamos el formulario  resumen
            conexionMysql.Close()

            'MsgBox("hay tantos resultados:" & cantidad)

            cerrarconexion()

            If cantidad = 0 Then
                lblistarespuestos.Visible = False
                rtxtclaveproducto.Text = ""
                'rtxtnombre.Text = ""
                rtxtprecio.Text = ""
                rtxtcantidad.Text = ""

            Else



                conexionMysql.Open()
                Dim Sql As String
                Sql = "select name as nombre from producto where name like '%" & rtxtnombre.Text & "%';"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader
                For i = 1 To cantidad
                    reader.Read()

                    lblistarespuestos.Items.Add(reader.GetString(0).ToString)
                Next


                conexionMysql.Close()
                'Catch ex As Exception
            End If

            'End Try
        End If

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim res As Integer
        res = 1
        If ptxtclaveproducto.Text = "" Or ptxtnombreproducto.Text = "" Or ptxtcantidad.Text = "" Or ptxtcosto.Text = "" Or ptxtprecio.Text = "" Then
            MsgBox("Hace falta información", MsgBoxStyle.Exclamation, "Sistema")
        Else
            'Try
            Dim clave As String
            cerrarconexion()
            'consulto que el ID no exista para poder ingresar uno nuevo
            conexionMysql.Open()
            Dim Sql As String
            Sql = "select idproducto from producto where idproducto='" & ptxtclaveproducto.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            Try
                clave = reader.GetString(0).ToString
            Catch ex As Exception
                clave = ""
            End Try
            cerrarconexion()

            conexionMysql.Close()
            'comprobar si devolvio null o un valor real
            If clave = ptxtclaveproducto.Text Then
                MsgBox("La clave del producto ya existe, asigna un nuevo valor", MsgBoxStyle.Exclamation, "Sistema")
                'Catch ex As Exception
                res = 0
            End If


            'End Try

            '----------------------------------------- obtener id de proveedor
            Dim idproveedor As Integer
            cerrarconexion()

            'conexionMysql.Open()
            'Dim Sql5 As String
            'Sql5 = "select idproveedor from proveedor where nombre_empresa='" & txtproveedor.Text & "';"
            'Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
            'reader = cmd5.ExecuteReader()
            'reader.Read()

            'idproveedor = reader.GetString(0).ToString

            'conexionMysql.Close()

            ''----------------------------------------- obtener id de tipoproducto
            'Dim idtipoproducto As Integer
            'cerrarconexion()

            'conexionMysql.Open()
            'Dim Sql55 As String
            'Sql55 = "select idtipoproducto from tipoproducto where tipo='" & txttipoproducto.Text & "';"
            'Dim cmd55 As New MySqlCommand(Sql55, conexionMysql)
            'reader = cmd55.ExecuteReader()
            'reader.Read()

            'idtipoproducto = reader.GetString(0).ToString

            'conexionMysql.Close()


            If res <> 0 Then


                Try

                    cerrarconexion()

                    conexionMysql.Open()

                    Dim sql2 As String
                    sql2 = "INSERT INTO producto (idproducto,name, cantidad, costo, precio) VALUES ('" & ptxtclaveproducto.Text & "', '" & ptxtnombreproducto.Text & "', " & ptxtcantidad.Text & ", " & ptxtcosto.Text & ", " & ptxtprecio.Text & ");"
                    Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                    cmd2.ExecuteNonQuery()

                    conexionMysql.Close()



                    '---------------------------------------------------------

                    MsgBox("Producto guardado", MsgBoxStyle.Information, "Sistema")
                    'se llena la grilla, tomando en cuenta ninguna elemento.
                    'txtnombrep.Text = ""
                    Call pllenadogrilla()

                Catch ex As Exception
                    MsgBox("Existe un problema al guardar al registro", MsgBoxStyle.Information, "Sistema")
                    cerrarconexion()
                End Try

                Call plimpiar()

            End If

        End If
    End Sub
    Function plimpiar()
        ptxtclaveproducto.Text = ""
        ptxtnombreproducto.Text = ""
        ptxtcantidad.Text = ""
        ptxtcosto.Text = ""
        ptxtprecio.Text = ""
    End Function
    Function pllenadogrilla()

        pgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        pgrilla.RowHeadersVisible = False
        'ampliar columna 
        'grillap.Columns(2).Width = 120


        pgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue

        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql As String
            sql = "select * from producto"
            Dim cmd As New MySqlCommand(sql, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            'cargamos el formulario  resumen
            da.Fill(dt)
            pgrilla.DataSource = dt
            pgrilla.Columns(1).Width = 500
            pgrilla.Columns(0).Width = 90
            pgrilla.Columns(2).Width = 90
            pgrilla.Columns(3).Width = 90
            pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try
    End Function
    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles ptxtprecio.TextChanged

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        TabControl1.SelectedIndex = 5
        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.FromArgb(27, 38, 44)
        Button14.BackColor = Color.DimGray
        Button38.BackColor = Color.FromArgb(27, 38, 44)
        pgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        pllenadogrilla()
    End Sub

    Private Sub Lblistarespuestos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lblistarespuestos.SelectedIndexChanged

    End Sub

    Private Sub lblistarespuestos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lblistarespuestos.KeyPress
        If e.KeyChar = Chr(13) Then
            rseleccionpieza()
        End If
    End Sub
    Function rseleccionpieza()
        rtxtnombre.Text = lblistarespuestos.Text
        lblistarespuestos.Visible = False
        'rtxtclaveproducto.Enabled = False


        'cargamos los datos del producto
        rcargardatosproducto()
    End Function
    Function rcargardatosproducto()
        conexionMysql.Open()
        Dim Sql As String
        Sql = "select * from producto where name='" & rtxtnombre.Text & "';"
        Dim cmd As New MySqlCommand(Sql, conexionMysql)
        reader = cmd.ExecuteReader()
        reader.Read()
        rtxtprecio.Text = reader.GetString(4).ToString()
        rtxtnombre.Text = reader.GetString(1).ToString()
        ' rtxtcantidad.Text = reader.GetString(2).ToString()
        rtxtclaveproducto.Text = reader.GetString(0).ToString()
        'ptxtcosto.Text = reader.GetString(3).ToString()

        'txtpreciomayoreop.Text = reader.GetString(5).ToString()

        reader.Close()
    End Function
    Private Sub lblistarespuestos_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lblistarespuestos.MouseDoubleClick
        rseleccionpieza()
        ' MsgBox("doble")
    End Sub

    Private Sub Ptxtclaveproducto_TextChanged(sender As Object, e As EventArgs) Handles ptxtclaveproducto.TextChanged
        'grillap.Visible = True
        'grilla2p.Visible = False
        'grillahistorialesproductos.Visible = False

        If ptxtclaveproducto.Text = "" Then
            plimpiar()

        Else
            pbuscarid()

        End If
    End Sub
    Function pbuscarid()
        If ptxtclaveproducto.Text = "" Then
            'MsgBox("aqui no paso nada")
        Else
            'MsgBox("comenzamos a buscar")
            Try
                Dim cantidad As Integer

                ' respuesta = vbYes

                cerrarconexion()

                If reader.HasRows Then
                    reader.Close()

                End If

                Dim claveproveedor, clavetipoproducto As Integer

                cerrarconexion()

                'Try
                'MsgBox(txtclavep.Text)

                Try
                    'MsgBox(txtclavep.Text)
                    conexionMysql.Open()
                    Dim Sql As String
                    Sql = "select * from producto where idproducto='" & ptxtclaveproducto.Text & "';"
                    Dim cmd As New MySqlCommand(Sql, conexionMysql)
                    reader = cmd.ExecuteReader()
                    reader.Read()
                    ptxtnombreproducto.Text = reader.GetString(1).ToString()
                    ptxtcantidad.Text = reader.GetString(2).ToString()
                    ptxtcosto.Text = reader.GetString(3).ToString()
                    ptxtprecio.Text = reader.GetString(4).ToString()
                    'txtpreciomayoreop.Text = reader.GetString(5).ToString()

                    reader.Close()





                    'MsgBox("actividad:" & txtactividad.Text)

                Catch ex As Exception
                    'MsgBox("hay problemas", MsgBoxStyle.Exclamation)
                    btninconsistencia.Visible = True
                    cerrarconexion()
                    ptxtnombreproducto.Text = ""
                    ptxtprecio.Text = ""
                    ptxtcosto.Text = ""
                    ptxtcantidad.Text = ""
                End Try


                conexionMysql.Close()




                '   MsgBox(clavetipoproducto)

                If reader.HasRows Then
                    reader.Close()

                End If



                '-------------cargamos el proveedor del producto.
                'Try

                '    Dim valor As String
                '    cerrarconexion()

                '    conexionMysql.Open()
                '    Dim Sql3 As String
                '    Sql3 = "select * from proveedor where idproveedor=" & claveproveedor & ";"
                '    Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
                '    reader = cmd3.ExecuteReader()
                '    reader.Read()
                '    valor = reader.GetString(1).ToString()

                '    conexionMysql.Close()

                '    txtproveedor.Text = valor



                '    '-------------cargamos el tipodeproducto del producto.



                '    cerrarconexion()

                'Catch ex As Exception
                '    txtproveedor.Text = "INDEFINIDO"
                '    cerrarconexion()
                '    btninconsistencia.Visible = True
                'End Try


                'Dim valortipopro As String



                ''MsgBox("hahahahaha" & clavetipoproducto)
                'Try


                '    conexionMysql.Open()
                '    Dim Sql33 As String
                '    Sql33 = "select tipo from tipoproducto where idtipoproducto=" & clavetipoproducto & ";"
                '    Dim cmd33 As New MySqlCommand(Sql33, conexionMysql)
                '    reader = cmd33.ExecuteReader()
                '    reader.Read()
                '    valortipopro = reader.GetString(0).ToString()

                '    conexionMysql.Close()


                '    'MsgBox("hoooooooooooooo" & valortipopro)


                '    txttipoproducto.Text = valortipopro

                'Catch ex As Exception
                '    txttipoproducto.Text = "INDEFINIDO"
                '    cerrarconexion()
                '    btninconsistencia.Visible = True
                'End Try


                '----------------------------------------------

                '  obtenemos los dos precios de venta
                'conexionMysql.Open()
                'Dim Sql2 As String
                'Sql2 = "select * from tipo_costo where idproducto=" & txtclavep.Text & ";"
                'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                'reader = cmd2.ExecuteReader()
                'reader.Read()
                'txtprecioindividualp.Text = reader.GetString(1).ToString()
                'txtpreciomayoreop.Text = reader.GetString(2).ToString()
                'reader.Close()

                'conexionMysql.Close()



                'If cantidad = 1 Then
                '    MsgBox("Este es el ultimo producto existente, recuerda adquirir más", MsgBoxStyle.Information, "Sistema")
                'ElseIf cantidad = 0 Then
                '    respuesta = MsgBox("Ya no hay productos por vender." & vbCrLf & "Para mantener la integridad de la información, primero agrega mas productos", MsgBoxStyle.Exclamation, "Sistema")
                '    borrar()

                'End If

                ''en caso de que quede 1, si se vende, pero si queda 0, depende de la respuesta.

                'If respuesta = vbYes Then
                '    txtcantidad.Text = 1

                'Else
                '    txtactividad.Text = ""
                '    txtcosto.Text = ""
                '    txtcantidad.Text = ""


                'End If



                ''realizar operacion para obtener el total 

                'txttotal.Text = CDbl(txtcantidad.Text) * CDbl(txtcosto.Text)






            Catch ex As Exception
                'MsgBox("El producto no existe o no se a podido procesar", MsgBoxStyle.Exclamation, "Sistema")

                'tipoingreso()
                cerrarconexion()


                Call plimpiar()


                'MsgBox("Hay detalles con el proceso", MsgBoxStyle.Information, "CTRL+y")

            End Try
        End If
    End Function

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If ptxtclaveproducto.Text = "" Or ptxtnombreproducto.Text = "" Or ptxtcantidad.Text = "" Or ptxtcosto.Text = "" Or ptxtprecio.Text = "" Then
            MsgBox("There are empty boxes, check the information", MsgBoxStyle.Information, "MOBI")
        Else




            Try
                cerrarconexion()

                conexionMysql.Open()

                Dim sql2 As String
                sql2 = "UPDATE producto SET name='" & ptxtnombreproducto.Text & "', cantidad=" & ptxtcantidad.Text & ", costo=" & ptxtcosto.Text & ", precio=" & ptxtprecio.Text & "   WHERE idproducto='" & ptxtclaveproducto.Text & "';"
                Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                cmd2.ExecuteNonQuery()

                conexionMysql.Close()
                MsgBox("Updated product", MsgBoxStyle.Information, "Sistema")


                Call plimpiar()


                Call pllenadogrilla()

            Catch ex As Exception
                cerrarconexion()

            End Try



        End If

        'End If

    End Sub
    Function actualizarcalculototal()

        Dim folio As String
        If activarbusqueda = True Then
            folio = rtxtbusquedafolio.Text
        Else
            folio = slbfolio.Text
        End If


        cerrarconexion()
        Dim sumatotal As String
        conexionMysql.Open()
        Dim Sql1 As String
        Sql1 = "select sum(reparacion + partes) as suma from equipo where idventa='" & folio & "';"
        Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
        reader = cmd1.ExecuteReader()
        reader.Read()
        sumatotal = reader.GetString(0).ToString()
        reader.Close()
        conexionMysql.Close()
        ' MsgBox(sumatotal)
        'Dim sumatotal As Double

        'sumatotal = CDbl(reparacion) + CDbl(piezas)
        'rtxttotal.Text = sumatotal

        cerrarconexion()


        '---------------------------------------
        'aqui va a calcular cuanto falta para pagar, en caso de que halla aumentado el valot total



        'calculamos total - deposito




        Dim depositox, totalx, faltante As Double


        'resto = CDbl(rtxttotal.Text) - CDbl(rtxtdeposito.Text)



        'rtxtresto.Text = resto










        'Try

        conexionMysql.Open()

        rtxttotal.Text = sumatotal
        ' rrsumatorio()

        Dim sql23 As String
        sql23 = "update venta set total='" & rtxttotal.Text & "', resto='" & faltante & "' where idventa='" & folio & "';"
        Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
        cmd23.ExecuteNonQuery()

        conexionMysql.Close()

        'Catch ex As Exception
        cerrarconexion()
        'End Try


        'Try


        cerrarconexion()
        conexionMysql.Open()
        Dim Sql22 As String
        Sql22 = "select total,deposito from venta where idventa='" & folio & "';"
        Dim cmd22 As New MySqlCommand(Sql22, conexionMysql)
        reader = cmd22.ExecuteReader()
        reader.Read()
        totalx = reader.GetString(0).ToString()
        depositox = reader.GetString(1).ToString()

        'cbformadepagoservicios.Items.Add(reader.GetString(1).ToString())
        '---------------------------------
        cerrarconexion()

        'Catch ex As Exception
        cerrarconexion()
        ' End Try



        faltante = CDbl(totalx) - CDbl(depositox)


        ' MsgBox(totalx)
        'MsgBox(depositox)

        'MsgBox(faltante)


        '------------------------------------------------------------------ACTUALIZAR RESTO------------------------------------------------------------------------
        conexionMysql.Open()

        rtxttotal.Text = sumatotal
        ' rrsumatorio()

        Dim sql233 As String
        sql233 = "update venta set resto='" & faltante & "' where idventa='" & folio & "';"
        Dim cmd233 As New MySqlCommand(sql233, conexionMysql)
        cmd233.ExecuteNonQuery()

        conexionMysql.Close()

        '------------------------------------------------------------------ACTUALIZAR RESTO------------------------------------------------------------------------



        cerrarconexion()
        conexionMysql.Open()
        Dim Sql2 As String
        Sql2 = "select total,resto from venta where idventa='" & folio & "';"
        Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
        reader = cmd2.ExecuteReader()
        reader.Read()
        rtxttotal.Text = reader.GetString(0).ToString()
        rtxtresto.Text = reader.GetString(1).ToString()

        'cbformadepagoservicios.Items.Add(reader.GetString(1).ToString())
        '---------------------------------
        cerrarconexion()






        ' MsgBox(rtxttotal.Text)

    End Function

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click

        If rtxtnombre.Text = "" Or rtxtcantidad.Text = "" Or rtxtprecio.Text = "" Then
            MsgBox("Falta información por agregar", MsgBoxStyle.Information, "MOBI")


            If rtxtnombre.Text = "" Then
                rtxtnombre.BackColor = Color.FromArgb(247, 220, 111)
            End If
            If rtxtcantidad.Text = "" Then
                rtxtcantidad.BackColor = Color.FromArgb(247, 220, 111)
            End If

            If rtxtprecio.Text = "" Then
                rtxtprecio.BackColor = Color.FromArgb(247, 220, 111)
            End If


        Else

            ' rtxtclaveproducto.Enabled = True
            'verificamos si existe o no el producto, en caso contrario, lo agregamos.

            'Dim idproducto As Integer
            'Dim respuesta As String
            'Try
            Dim idequipomaximo As Integer

            Try

                conexionMysql.Open()
                Dim Sqlxx As String
                Sqlxx = "select max(idequipo) from equipo;"
                Dim cmdxx As New MySqlCommand(Sqlxx, conexionMysql)
                reader = cmdxx.ExecuteReader()
                reader.Read()
                idequipomaximo = reader.GetString(0).ToString()
                reader.Close()
                conexionMysql.Close()
            Catch ex As Exception
                idequipomaximo = 0
                cerrarconexion()

            End Try

            'Catch ex As Exception
            '    cerrarconexion()
            '    idproducto = 0
            'End Try



            'If idproducto = 0 Then


            '    respuesta = MsgBox("El producto es nuevo,¿Deseas agregarlo?", MsgBoxStyle.YesNo, "MOBI")

            '    If respuesta = vbYes Then
            '        conexionMysql.Open()
            '        Dim Sqlx1 As String
            '        Sqlx1 = "INSERT INTO `mobi`.`producto` (`idproducto`, `name`, `cantidad`, `costo`, `precio`) VALUES ( '" & rtxtnombre.Text & "', '" & rtxtaddress.Text & "', '" & rtxtstate.Text & "', '" & rtxtcity.Text & "', '" & rtxttelephone.Text & "');"
            '        Dim cmdx1 As New MySqlCommand(Sqlx1, conexionMysql)
            '        cmdx1.ExecuteNonQuery()
            '        conexionMysql.Close()
            '        'txttotalpagar.Text = ""
            '        conexionMysql.Close()
            '    End If



            'End If
            Dim dia, mes, año, fecha, fechaclave, claveproducto As String
            hora2 = Now.Hour()
            minuto = Now.Minute()
            segundo = Now.Second()

            hora = hora2 & ":" & minuto & ":" & segundo

            dia = Date.Now.Day
            mes = Date.Now.Month
            año = Date.Now.Year
            fecha = año & "-" & mes & "-" & dia

            fechaclave = año & mes & dia & hora2 & minuto & segundo

            If rtxtclaveproducto.Text = "" Then
                claveproducto = fechaclave
            Else
                claveproducto = rtxtclaveproducto.Text
            End If

            Dim i As Integer = rgrilla.RowCount
            Dim gananciatotal As Double
            'txtcostoconproducto.Text = Math.Round(f, 2)
            gananciatotal = CDbl(rtxtcantidad.Text) * CDbl(rtxtprecio.Text)
            gananciatotal = Math.Round(gananciatotal, 2)

            Dim folio As String
            If activarbusqueda = True Then
                folio = rtxtbusquedafolio.Text

                'en caso de que ya exista la busqueda, solo agregamos los nuevos producto, al folio ya existente del equipo
                cerrarconexion()
                conexionMysql.Open()
                Dim Sql25 As String
                Sql25 = "INSERT INTO venta_ind (actividad, cantidad, costo, idventa,idproducto,name_item,idequipo) VALUES ('" & rtxtnombre.Text & "'," & rtxtcantidad.Text & "," & rtxtprecio.Text & "," & folio & ",'" & claveproducto & "','" & rtxtequipo.Text & "','" & cb1.Text & "');"
                Dim cmd25 As New MySqlCommand(Sql25, conexionMysql)
                cmd25.ExecuteNonQuery()
                conexionMysql.Close()


            Else


                'MsgBox("maximo antes" & idequipomaximo)
                folio = slbfolio.Text
                idequipomaximo = idequipomaximo + 1
                'MsgBox("maximo despues" & idequipomaximo)


                cerrarconexion()
                conexionMysql.Open()
                Dim Sql25 As String
                Sql25 = "INSERT INTO venta_ind (actividad, cantidad, costo, idventa,idproducto,name_item,idequipo) VALUES ('" & rtxtnombre.Text & "'," & rtxtcantidad.Text & "," & rtxtprecio.Text & "," & folio & ",'" & claveproducto & "','" & rtxtequipo.Text & "','" & idequipomaximo & "');"
                Dim cmd25 As New MySqlCommand(Sql25, conexionMysql)
                cmd25.ExecuteNonQuery()
                conexionMysql.Close()


                rgrilla.Rows.Add(claveproducto, rtxtnombre.Text, rtxtprecio.Text, rtxtcantidad.Text, gananciatotal)

            End If
            'MsgBox(folio)





            'MsgBox(claveproducto)
            '---------------------------------------------------------------------


            If rtxtclaveproducto.Text = "" Then


                conexionMysql.Open()
                Dim Sql255 As String
                Sql255 = "insert into producto (idproducto,name, cantidad,costo,precio) values('" & claveproducto & "','" & rtxtnombre.Text & "', '" & rtxtcantidad.Text & "',0,'" & rtxtprecio.Text & "');"
                Dim cmd255 As New MySqlCommand(Sql255, conexionMysql)
                cmd255.ExecuteNonQuery()
                conexionMysql.Close()

            End If

            'activamos la variable para indicar que hay una reparacion en curso
            'activarreparacion = True



            If activarbusqueda = True Then
                'en caso de que sea una busqueda en curso, solo vamos a agregar una suma nueva, tomando en cuenta, los totales de cada equipo y venta individual
                Dim reparacion, piezas As String
                conexionMysql.Open()
                Dim Sql As String
                Sql = "select sum(reparacion) from equipo where idventa='" & rtxtbusquedafolio.Text & "';"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader()
                reader.Read()
                reparacion = reader.GetString(0).ToString()
                reader.Close()
                conexionMysql.Close()


                conexionMysql.Open()
                Dim Sql1 As String
                Sql1 = "select sum(costo) from venta_ind where idventa='" & rtxtbusquedafolio.Text & "';"
                Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
                reader = cmd1.ExecuteReader()
                reader.Read()
                piezas = reader.GetString(0).ToString()
                reader.Close()
                conexionMysql.Close()

                Dim sumatotal As Double

                sumatotal = CDbl(reparacion) + CDbl(piezas)
                rtxttotal.Text = sumatotal

                cerrarconexion()
                conexionMysql.Open()


                ' rrsumatorio()

                'Dim sql23 As String
                'sql23 = "update venta set total='" & rtxttotal.Text & "' where idventa='" & rtxtbusquedafolio.Text & "';"
                'Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
                'cmd23.ExecuteNonQuery()

                'conexionMysql.Close()
                rsumatorioactualizar()
                '-------------------------------------------------------------------------
                '--------------------------------------------------------------------------
                'llamo a la funcion actualizarcalculo total para que actualize el total tomando en cuenta los valores existentes en la BD
                actualizarcalculototal()
                '---------------------------------------------------------------------------


                'consultamos los datos de partes, actualizamos
                cerrarconexion()
                conexionMysql.Open()
                Dim Sql312 As String
                'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                Sql312 = "SELECT * FROM equipo where equipo='" & rcbitemtemporal.Text & "' and idventa='" & folio & "';"
                Dim cmd312 As New MySqlCommand(Sql312, conexionMysql)
                reader = cmd312.ExecuteReader()
                reader.Read()
                rtxtpriceparts.Text = reader.GetString(10).ToString()
                conexionMysql.Close()

                'MsgBox("se actualizo el total")

            Else
                rsumatorio()

            End If


            If activarbusqueda = True Then


                cerrarconexion()
                'CARGAMOS LOS VALORES NUEVAMENTE
                '-----------------------------------------------------------------------------------------
                'cargamos las piezas de reparacion en caso de que tenga
                conexionMysql.Open()
                Dim Sql311 As String
                'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                Sql311 = "select count(*)as contador from venta_ind where idventa=" & folio & " and name_item='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "';"
                Dim cmd311 As New MySqlCommand(Sql311, conexionMysql)
                reader = cmd311.ExecuteReader()
                reader.Read()
                Dim cantidad As Integer = reader.GetString(0).ToString()
                conexionMysql.Close()

                Dim j As Integer

                '-----------------------------------------------------------------------
                'seleccionaritem()
                '-----------------------------------------------------------------------

                'cerrarconexion()
                'conexionMysql.Open()
                'Dim costo, idproducto, cantidadpro, actividad, idventa_ind As String
                'Dim Sql3 As String
                'Sql3 = "select * from venta_ind where idventa=" & folio & " and name_item='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "';"
                'Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
                'reader = cmd3.ExecuteReader()

                'For i = 0 To cantidad - 1

                '    reader.Read()
                '    'MsgBox(i)
                '    idventa_ind = reader.GetString(0).ToString()
                '    actividad = reader.GetString(1).ToString()
                '    cantidadpro = reader.GetString(2).ToString()
                '    costo = reader.GetString(3).ToString()
                '    idproducto = reader.GetString(5).ToString()


                '    rgrilla.Rows.Add(idventa_ind, idproducto, actividad, costo, cantidadpro, CDbl(cantidadpro) * CDbl(costo))


                'Next
                '-------------------------------------------------------------------------------------------
            End If

            rlimpiar()
            rtxtclaveproducto.Focus()
        End If

    End Sub
    Function cargarProductosEnVenta()

        'cerrarconexion()
        'conexionMysql.Open()
        'Dim costo, idproducto, cantidadpro, actividad, idventa_ind As String
        'Dim Sql3 As String
        'Sql3 = "select * from venta_ind where idventa=" & folio & " and name_item='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "';"
        'Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
        'reader = cmd3.ExecuteReader()

        'For i = 0 To cantidad - 1

        '    reader.Read()
        '    'MsgBox(i)
        '    idventa_ind = reader.GetString(0).ToString()
        '    actividad = reader.GetString(1).ToString()
        '    cantidadpro = reader.GetString(2).ToString()
        '    costo = reader.GetString(3).ToString()
        '    idproducto = reader.GetString(5).ToString()


        '    rgrilla.Rows.Add(idventa_ind, idproducto, actividad, costo, cantidadpro, CDbl(cantidadpro) * CDbl(costo))


        'Next

    End Function
    Function rsumatorioactualizar()

        Dim sumaporequipo As String

        ' MsgBox(rcbitemtemporal.Text)
        Try

            cerrarconexion()
            'consultar primero suma de cada equipo de la venta que se ha realizado
            conexionMysql.Open()
            Dim Sql As String

            Sql = "select sum(cantidad*costo)as total from venta_ind where idventa='" & rtxtbusquedafolio.Text & "' and name_item='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            sumaporequipo = reader.GetString(0).ToString()
            reader.Close()
            conexionMysql.Close()
            cerrarconexion()
            rtxtpriceparts.Text = sumaporequipo
        Catch ex As Exception
            cerrarconexion()
            sumaporequipo = "0"
            rtxtpriceparts.Text = "0"
        End Try

        '-------------------------------------
        'obtengo la suma que ya hay insertada y solo actualizo la nueva suma
        conexionMysql.Open()
        Dim sql23 As String
        sql23 = "update equipo set partes='" & sumaporequipo & "' where idventa='" & rtxtbusquedafolio.Text & "' and equipo='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "';"
        Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
        cmd23.ExecuteNonQuery()

        conexionMysql.Close()





    End Function
    Public Sub rsumatorio()
        Dim i As Integer = rgrilla.RowCount

        Dim j As Integer
        Dim sumacon, sumasin, cantidad_productos, sumagananciatotal As Double
        Dim a, b, c, d As String
        'suma de valores
        For j = 0 To i - 2
            'MsgBox("valosr de j:" & j)
            'a = venta.grillaventa.Item(j, 3).Value.ToString()
            a = rgrilla.Rows(j).Cells(4).Value 'con cotizador
            'c = sgrilla.Rows(j).Cells(9).Value 'sin cotizador
            'd = sgrilla.Rows(j).Cells(7).Value 'totalganancia

            'b = sgrilla.Rows(j).Cells(3).Value
            'cantidad_productos = cantidad_productos + b
            ' sumacon = sumacon + a
            'sumasin = sumasin + c
            sumagananciatotal = sumagananciatotal + a

            'MsgBox(a)
        Next


        Try
            If rtxtcostoreparacion.Text = "" Then
                'rtxtcostoreparacion.Text = "0"

            End If


            rtxtpriceparts.Text = sumagananciatotal
            rtxttotaltemporal.Text = CDbl(sumagananciatotal) + CDbl(rtxtcostoreparacion.Text)
            rtxttotal.Text = CDbl(sumagananciatotal) + CDbl(rtxtcostoreparacion.Text)



        Catch ex As Exception

        End Try



        If activarbusqueda = True Then
            'en caso de que se este buscando, entonces que actualice automaticamente


            'cerrarconexion()
            'conexionMysql.Open()


            '' rrsumatorio()

            'Dim sql23 As String
            'sql23 = "update venta set total='" & rtxttotal.Text & "' where idventa='" & rtxtbusquedafolio.Text & "';"
            'Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
            'cmd23.ExecuteNonQuery()

            'conexionMysql.Close()


            'conexionMysql.Open()
            'Dim sql234 As String
            'sql234 = "update equipo set partes='" & rtxtpriceparts.Text & "' where idventa='" & rtxtbusquedafolio.Text & "';"
            'Dim cmd234 As New MySqlCommand(sql234, conexionMysql)
            'cmd234.ExecuteNonQuery()

            'conexionMysql.Close()



        End If

        'stxtpagarsin.Text = sumasin
        'stxttotal.Text = sumasin
        'stxtgananciatotalproductos.Text = sumagananciatotal

        ''el valor siguiente es como funcionaba anteriormente
        ''        stxttotal.Text = suma
        'stxttotalproductos.Text = cantidad_productos
    End Sub
    Private Sub Rtxtclaveproducto_TextChanged(sender As Object, e As EventArgs) Handles rtxtclaveproducto.TextChanged
        'grillap.Visible = True
        'grilla2p.Visible = False
        'grillahistorialesproductos.Visible = False

        If rtxtclaveproducto.Text = "" Then
            rlimpiar()

        Else
            rbuscarid()
            lblistarespuestos.Visible = False
        End If
    End Sub
    Function rbuscarid()

        If rtxtclaveproducto.Text = "" Then
            'MsgBox("aqui no paso nada")
        Else
            ' 'MsgBox("comenzamos a buscar")
            Try
                Dim cantidad As Integer

                ' respuesta = vbYes

                cerrarconexion()

                'If reader.HasRows Then
                '    reader.Close()

                'End If

                Dim claveproveedor, clavetipoproducto As Integer

                cerrarconexion()

                Try

                    conexionMysql.Open()
                    Dim Sql As String
                    Sql = "select * from producto where idproducto='" & rtxtclaveproducto.Text & "';"
                    Dim cmd As New MySqlCommand(Sql, conexionMysql)
                    reader = cmd.ExecuteReader()
                    reader.Read()
                    rtxtprecio.Text = reader.GetString(4).ToString()
                    rtxtnombre.Text = reader.GetString(1).ToString()
                    ' rtxtcantidad.Text = reader.GetString(2).ToString()
                    ' rtxtcos.Text = reader.GetString(3).ToString()

                    'txtpreciomayoreop.Text = reader.GetString(5).ToString()

                    reader.Close()

                    conexionMysql.Close()

                    'MsgBox("entro")



                Catch ex As Exception
                    btninconsistencia.Visible = True
                    cerrarconexion()
                    'rlimpiar()
                    rtxtnombre.Text = ""
                    rtxtcantidad.Text = ""
                    rtxtprecio.Text = ""
                End Try


                conexionMysql.Close()




                '   MsgBox(clavetipoproducto)

                If reader.HasRows Then
                    reader.Close()

                End If





            Catch ex As Exception

                cerrarconexion()


                Call rlimpiar()



            End Try
        End If
    End Function

    Function rlimpiar()
        rtxtclaveproducto.Text = ""
        rtxtnombre.Text = ""
        rtxtcantidad.Text = ""
        rtxtprecio.Text = ""

    End Function

    Private Sub rgrilla_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles rgrilla.CellContentDoubleClick
        'Try

        'eliminar datos al dar doble clic.
        'grilla.CurrentRow.Index


        'Dim Variable As String = rgrilla.Item(0, rgrilla.CurrentRow.Index).Value
        ''MsgBox(Variable)




        'MsgBox(folio)
        'conexionMysql.Open()

        '    Dim sql2 As String
        'sql2 = "delete from venta_ind where idventa='" & folio & "' and idproducto='" & Variable & "'"
        'Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
        '    cmd2.ExecuteNonQuery()

        '    conexionMysql.Close()





        'rgrilla.Rows.RemoveAt(rgrilla.CurrentRow.Index)
        '    'eliminado el registro, sumamos el total de valores. 
        '    rsumatorio()
        ''Catch ex As Exception

        'End Try
    End Sub

    Private Sub Rtxtcostoreparacion_TextChanged(sender As Object, e As EventArgs) Handles rtxtcostoreparacion.TextChanged
        If activarbusqueda = False Then

            rsumatorio()

        Else
            'en caso de que exista una busqueda en curso, hay que actualizar el costo de la reparacion

            'consultar primero suma de cada equipo de la venta que se ha realizado
            'conexionMysql.Open()
            'Dim Sql As String
            'Dim sumaporequipo As String
            'Sql = "select sum(cantidad*costo)as total from venta_ind where idventa='" & rtxtbusquedafolio.Text & "' and name_item='" & rcbitemtemporal.Text & "';"
            'Dim cmd As New MySqlCommand(Sql, conexionMysql)
            'reader = cmd.ExecuteReader()
            'reader.Read()
            'sumaporequipo = reader.GetString(4).ToString()
            'reader.Close()
            'conexionMysql.Close()
            Try


                If rtxtcostoreparacion.Text = "" Then

                Else


                    cerrarconexion()
                    '-------------------------------------
                    'obtengo la suma que ya hay insertada y solo actualizo la nueva suma
                    conexionMysql.Open()
                    Dim sql23 As String
                    sql23 = "update equipo set reparacion='" & rtxtcostoreparacion.Text & "' where idventa='" & rtxtbusquedafolio.Text & "' and equipo='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "';"
                    Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
                    cmd23.ExecuteNonQuery()
                    conexionMysql.Close()











                    actualizarcalculototal()
                End If
            Catch ex As Exception

            End Try


        End If

    End Sub
    Function actualizarreparacion()


    End Function
    Private Sub Rgrilla_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles rgrilla.CellContentClick

    End Sub

    Private Sub Txtname_TextChanged(sender As Object, e As EventArgs) Handles txtname.TextChanged

        If txtname.Text = "" Then
            slbcustomer.Visible = False
            slbcustomer.Items.Clear()
        Else


            slbcustomer.Visible = True
            slbcustomer.Items.Clear()


            'cerramos la conexion
            cerrarconexion()

            Dim cantidad, i As Integer
            cantidad = 0
            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "select count(*) from customer where name like '%" & txtname.Text & "%';"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            reader = cmd2.ExecuteReader
            reader.Read()
            cantidad = reader.GetString(0).ToString

            'cargamos el formulario  resumen
            conexionMysql.Close()

            'MsgBox("hay tantos resultados:" & cantidad)

            cerrarconexion()

            If cantidad = 0 Then
                slbcustomer.Visible = False
            Else



                conexionMysql.Open()
                Dim Sql As String
                Sql = "select name as nombre from customer where name like '%" & txtname.Text & "%';"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader
                For i = 1 To cantidad
                    reader.Read()

                    slbcustomer.Items.Add(reader.GetString(0).ToString)
                Next


                conexionMysql.Close()
                'Catch ex As Exception
            End If

            'End Try
        End If
    End Sub


    Private Sub rgrilla_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles rgrilla.CellDoubleClick
        'Try




        Dim Variable As String = rgrilla.Item(0, rgrilla.CurrentRow.Index).Value
        ' MsgBox(Variable)


        If Variable = "" Then


        Else

            Dim folio As String
            If activarbusqueda = True Then
                folio = rtxtbusquedafolio.Text
            Else
                folio = slbfolio.Text
            End If


            cerrarconexion()
            '---------------------------------------------------
            '------------------------------------------------------------
            'actualizarcalculototal()
            Dim idequipo As String
            conexionMysql.Open()
            Dim Sql33 As String
            Sql33 = "select idequipo from venta_ind where idventa='" & folio & "' and idventa_ind='" & Variable & "';"
            Dim cmd33 As New MySqlCommand(Sql33, conexionMysql)
            reader = cmd33.ExecuteReader
            reader.Read()
            'slbcustomer.Items.Add(reader.GetString(0).ToString)

            idequipo = reader.GetString(0).ToString

            conexionMysql.Close()
            cerrarconexion()
            '------------------------------------------------------------
            '----------------------------------------------------

            cerrarconexion()
            conexionMysql.Open()

            Dim sql2 As String
            sql2 = "delete from venta_ind where idventa='" & folio & "' and idventa_ind='" & Variable & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()

            cerrarconexion()
            'actualizar el total

            'conexionMysql.Open()

            rsumatorioactualizar()

            '------------------------------------------------------------
            'actualizarcalculototal()
            Dim sumre, sumpa, sumde, sumtotal As Double
            conexionMysql.Open()
            Dim Sql As String
            Sql = "select sum(reparacion), sum(partes), deposito from equipo where idventa='" & folio & "' and idequipo='" & idequipo & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader
            reader.Read()
            'slbcustomer.Items.Add(reader.GetString(0).ToString)

            sumre = reader.GetString(0).ToString
            sumpa = reader.GetString(1).ToString
            sumde = reader.GetString(2).ToString

            conexionMysql.Close()
            cerrarconexion()
            '------------------------------------------------------------
            'actualizo el resto, 
            sumtotal = (sumre + sumpa) - sumde


            MsgBox(sumtotal)

            conexionMysql.Open()
            Dim sql23 As String
            sql23 = "update equipo set resto='" & sumtotal & "' where idventa='" & folio & "' and idequipo='" & idequipo & "';;"
            Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
            cmd23.ExecuteNonQuery()

            conexionMysql.Close()
            cerrarconexion()


            '-------------------------------------------
            'rrsumatorio()

            cerrarconexion()
            conexionMysql.Open()
            Dim Sql31234 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31234 = "SELECT * FROM equipo where equipo='" & rcbitemtemporal.Text & "' and idventa='" & folio & "'  and idequipo='" & cb1.Text & "';"
            Dim cmd31234 As New MySqlCommand(Sql31234, conexionMysql)
            reader = cmd31234.ExecuteReader()
            reader.Read()
            rtxtresto.Text = reader.GetString(14).ToString()

            conexionMysql.Close()
            cerrarconexion()
            '---------------------------------------------------

            conexionMysql.Open()
            Dim Sql2s As String
            Sql2s = "select * from venta where idventa='" & folio & "' ;"
            Dim cmd2s As New MySqlCommand(Sql2s, conexionMysql)
            reader = cmd2s.ExecuteReader()
            reader.Read()
            rtxttotal.Text = reader.GetString(3).ToString()
            rtxtresto.Text = reader.GetString(7).ToString()

            'cbformadepagoservicios.Items.Add(reader.GetString(1).ToString())
            '---------------------------------
            cerrarconexion()

            'Dim sql23 As String
            'sql23 = "update venta set total='" & rtxttotal.Text & "' where idventa='" & folio & "';"
            'Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
            'cmd23.ExecuteNonQuery()

            'conexionMysql.Close()


            Try


                rgrilla.Rows.RemoveAt(rgrilla.CurrentRow.Index)
                'eliminado el registro, sumamos el total de valores. 
                'rsumatorio()
                'Catch ex As Exception
            Catch ex As Exception

            End Try


        End If

        'eliminar datos al dar doble clic.
        'grilla.CurrentRow.Index
        ' rgrilla.Rows.RemoveAt(rgrilla.CurrentRow.Index)
        'eliminado el registro, sumamos el total de valores. 

        ' Catch ex As Exception

        ' End Try
    End Sub
    Function actualizarresto()



    End Function
    Private Sub Rtxtdeposito_TextChanged(sender As Object, e As EventArgs) Handles rtxtdeposito.TextChanged
        rtxtdeposito.BackColor = Color.White

        'If activarbusqueda = False Then
        'Try


        Try

            If CDbl(rtxtdeposito.Text) > CDbl(rtxttotal.Text) Then
                rtxtdeposito.Text = ""
                rtxtresto.Text = ""
                MsgBox("Valor fuera de rango", MsgBoxStyle.Information, "MOBI")
            Else
                rtxtresto.Text = CDbl(rtxttotal.Text) - CDbl(rtxtdeposito.Text)


                'actualizo el dato del equipo-----------------------------------------



                Dim folio As Integer
                If activarbusqueda = True Then
                    folio = rtxtbusquedafolio.Text
                Else
                    folio = slbfolio.Text
                End If
                cerrarconexion()
                conexionMysql.Open()

                Dim sql23 As String
                sql23 = "update venta set deposito='" & rtxtdeposito.Text & "', resto='" & rtxtresto.Text & "' where idventa='" & folio & "';"
                Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
                cmd23.ExecuteNonQuery()
                conexionMysql.Close()

                ' MsgBox("actualizado")
                'seleccionaritem()



            End If
            ' Catch ex As Exception

            'End Try
            'Else



            '    If activarbusqueda = True Then


            '        Try

            '            'en caso de que la busqueda sea verdad, osea, que se este llevando una busqueda, solo actualizamos el la información de anticipos
            '            If CDbl(rtxtdeposito.Text) > CDbl(rtxtresto.Text) Then
            '                rtxtdeposito.Text = ""
            '                rtxtresto.Text = ""
            '                MsgBox("Valor fuera de rango", MsgBoxStyle.Information, "MOBI")
            '            Else
            '                rtxtresto.Text = CDbl(rtxttotal.Text) - CDbl(rtxtdeposito.Text)

            '            End If

            '        Catch ex As Exception

            '        End Try


        Catch ex As Exception

        End Try




        'End If
        'End If

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click


        'BOTON SOLD, VENTA DE REPARACION

        If activarbusqueda = False Then

            Dim estadocaja As Integer


            estadocaja = comprobarcaja()



            ''MsgBox(estadocaja)
            If estadocaja = 0 Then

                'obtenerfolio()
                ' venta()
                ' RPT1.Show()
                'sventa()

                venta()

                activarreparacion = False
            Else
                '    MsgBox("No existe ninguna caja abierta", MsgBoxStyle.Information, "CTRL+y")
            End If






        Else
            MsgBox("Hay una busqueda en curso", MsgBoxStyle.Information, "MOBI")
        End If
        '  rcbitemtemporal.Items.Clear()
    End Sub
    Function rlimpiartodo()

        Try
            rtxtcustomername.Text = ""
            rtxtstate.Text = ""
            rtxtnombre.Text = ""
            rtxtequipo.Text = ""
            rtxtdeposito.Text = ""
            rtxtdeposito.Text = ""
            rtxtimei.Text = ""
            rtxtnote.Text = ""
            rtxtprecio.Text = ""
            rtxtproblem.Text = ""
            rtxtcity.Text = ""
            rtxttelephone.Text = ""
            rtxtaddress.Text = ""
            rtxtmodelo.Text = ""
            rtxtpassword.Text = ""
            rtxttotal.Text = ""
            rtxtcostoreparacion.Text = ""
            rtxtresto.Text = ""
            cbstate.SelectedIndex = 0
            rcbseller.SelectedIndex = 0
            rtxtemail.Text = ""
            rgrilla.Rows.Clear()
            rgrilla2.Rows.Clear()
            rtxttotaltemporal.Text = ""
            rtxtpriceparts.Text = ""
            cb1.Items.Clear()

        Catch ex As Exception
        End Try



        'eliminar lo que exista en el folio que esta por hacerse
        'eliminarventa_indtemporal()
    End Function

    Function eliminarventa_indtemporal()
        Try

            conexionMysql.Open()
            Dim Sql26 As String
            Sql26 = "delete from venta_ind where idventa=" & slbfolio.Text & ";"
            Dim cmd26 As New MySqlCommand(Sql26, conexionMysql)
            cmd26.ExecuteNonQuery()
            conexionMysql.Close()
        Catch ex As Exception

        End Try

    End Function

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim formulario As New frmproducto
        formulario.ShowDialog()

    End Sub

    Private Sub Rtxtcantidad_TextChanged(sender As Object, e As EventArgs) Handles rtxtcantidad.TextChanged
        rtxtcantidad.BackColor = Color.White

    End Sub

    Function venta()
        obtenerfolio()

        'Try
        Dim cantidaddedispositivos As Integer
        cantidaddedispositivos = rcbitemtemporal.Items.Count
        'MsgBox(cantidaddedispositivos)

        If cantidaddedispositivos = 0 Then
            MsgBox("Agrega al menos un  dispositivo a la lista", MsgBoxStyle.Information, "MOBI")
        Else



            If rtxttotal.Text = "0" Or rtxttotal.Text = "" Or rtxtresto.Text = "" Or rtxtdeposito.Text = "" Or rtxtcustomername.Text = "" Then
                MsgBox("No hay ventas que realizar", MsgBoxStyle.Information, "Sistema")





                'comprobar que caja esta vacia
                If rtxttotal.Text = "" Then
                    rtxttotal.BackColor = Color.FromArgb(247, 220, 111)
                    rtxttotal.Focus()
                End If

                If rtxtresto.Text = "" Then
                    rtxtresto.BackColor = Color.FromArgb(247, 220, 111)
                    rtxtresto.Focus()
                End If



                If rtxtdeposito.Text = "" Then
                    rtxtdeposito.BackColor = Color.FromArgb(247, 220, 111)
                    rtxtdeposito.Focus()

                End If

                If rtxtcustomername.Text = "" Then
                    rtxtcustomername.BackColor = Color.FromArgb(247, 220, 111)
                    rtxtcustomername.Focus()
                End If


                'rtxttotal.Text = ""
            Else
                'obtener fecha y hora
                Dim hora, minuto, segundo, hora2 As String
                Dim dia, mes, año, fecha As String
                hora2 = Now.Hour()
                minuto = Now.Minute()
                segundo = Now.Second()

                hora = hora2 & ":" & minuto & ":" & segundo

                dia = Date.Now.Day
                mes = Date.Now.Month
                año = Date.Now.Year
                fecha = año & "-" & mes & "-" & dia

                Dim fechacalendarioentrega As String
                fechacalendarioentrega = rcalendario.Value.ToString("yyyy/MM/dd")
                '------------------ insertar reginstro en tabla venta ---------------------------------------

                cerrarconexion()
                Dim idcliente As Integer

                idcliente = 1

                Try
                    cerrarconexion()
                    conexionMysql.Open()
                    Dim Sql31 As String
                    'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                    Sql31 = "select idcustomer from customer where name like '%" & rtxtcustomername.Text & "%';"
                    Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
                    reader = cmd31.ExecuteReader()
                    reader.Read()
                    idcliente = reader.GetString(0).ToString()
                    conexionMysql.Close()
                    'en caso de que lo obtenga, obtenemos el ID, en caso contrario guardamos el dato
                    'MsgBox(idcliente)
                Catch ex As Exception
                    ' idcliente = 1
                    cerrarconexion()

                    'en caso de que no este el cliente, lo guardamos
                    conexionMysql.Open()
                    Dim Sqlx1 As String
                    Sqlx1 = "INSERT INTO `customer` (`name`, `address`, `state`, `city`, `telephone`,`email`) VALUES ( '" & rtxtcustomername.Text & "', '" & rtxtaddress.Text & "', '" & rtxtstate.Text & "', '" & rtxtcity.Text & "', '" & rtxttelephone.Text & "','" & rtxtemail.Text & "');"
                    Dim cmdx1 As New MySqlCommand(Sqlx1, conexionMysql)
                    'cmdx1.ExecuteNonQuery()
                    conexionMysql.Close()
                    'txttotalpagar.Text = ""
                    conexionMysql.Close()

                    '------------------------obtenemos el id del cliente
                    cerrarconexion()
                    conexionMysql.Open()
                    Dim Sqlx2 As String
                    'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                    Sqlx2 = "select idcustomer from customer where name like '%" & rtxtcustomername.Text & "%';"
                    Dim cmdx2 As New MySqlCommand(Sqlx2, conexionMysql)
                    reader = cmdx2.ExecuteReader()
                    reader.Read()
                    idcliente = reader.GetString(0).ToString()
                    conexionMysql.Close()

                    MsgBox("Customer insertado", MsgBoxStyle.Information, "MOBI")


                    '-MsgBox(idcliente)
                End Try


                '----------------insertamos los valores del equipo y del ------------------
                'cerrarconexion()
                'conexionMysql.Open()
                'Dim Sql2 As String
                'Sql2 = "INSERT INTO `mobi`.`equipo` (`idequipo`,`equipo`, `modelo`, `imei`, `password`, `status`, `problema`, `nota`, `idcustomer`) VALUES ('" & slbfolio.Text & "','" & rtxtequipo.Text & "', '" & rtxtmodelo.Text & "', '" & rtxtimei.Text & "', '" & rtxtpassword.Text & "', '" & cbstate.Text & "', '" & rtxtproblem.Text & "', '" & rtxtnote.Text & "', '" & idcliente & "');"
                'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                'cmd2.ExecuteNonQuery()
                'conexionMysql.Close()
                ''txttotalpagar.Text = ""
                'conexionMysql.Close()
                '--------------
                Dim idequipo, idseller As Integer
                '--------------
                cerrarconexion()
                'conexionMysql.Open()
                'Dim Sqlx3 As String
                ''consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                'Sqlx3 = "select idequipo from equipo where equipo like '%" & rtxtequipo.Text & "%';"
                'Dim cmdx3 As New MySqlCommand(Sqlx3, conexionMysql)
                'reader = cmdx3.ExecuteReader()
                'reader.Read()
                'idequipo = reader.GetString(0).ToString()
                'conexionMysql.Close()
                '-----------------------------------------------------------------------------------
                idequipo = slbfolio.Text
                cerrarconexion()
                conexionMysql.Open()
                Dim Sqlx4 As String
                'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                Sqlx4 = "select idseller from seller where name_seller='" & rcbseller.Text & "';"
                Dim cmdx4 As New MySqlCommand(Sqlx4, conexionMysql)
                reader = cmdx4.ExecuteReader()
                reader.Read()
                idseller = reader.GetString(0).ToString()
                conexionMysql.Close()



                '            Dim ii As Integer = rgrilla.RowCount
                '           ii = rgrilla.RowCount

                'If ii = 0 Then
                'MsgBox("No hay nada que vender")

                'Else
                '-----------------------------------------------------------------------------
                ''------SUMAMOS LAS VENTAS TOTALES Y ANTICIPOS
                'Dim sumdeposito, sumresto As String
                'cerrarconexion()
                'conexionMysql.Open()
                'Dim Sqlx45 As String
                ''consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
                'Sqlx45 = "select sum(deposito), sum(resto) from equipo where idventa='" & slbfolio.Text & "';"
                'Dim cmdx45 As New MySqlCommand(Sqlx45, conexionMysql)
                'reader = cmdx45.ExecuteReader()
                'reader.Read()
                'sumdeposito = reader.GetString(0).ToString()
                'sumresto = reader.GetString(1).ToString()
                'conexionMysql.Close()
                '-------------------------------------------------------------------------------
                'Dim hora2, minuto, segundo, hora As String
                ' Dim fechacaja As Date
                hora2 = Now.Hour()
                minuto = Now.Minute()
                segundo = Now.Second()

                hora = hora2 & ":" & minuto & ":" & segundo


                cerrarconexion()
                conexionMysql.Open()
                Dim Sql As String
                '                Sql = "INSERT INTO `mobi`.`venta` (`idventa`, `idcustomer`,  `idseller`, `total`, `fechaventa`, `fechaentrega`, `deposito`, `resto`,`tipoventa`) VALUES ('" & slbfolio.Text & "', '" & idcliente & "', '" & idseller & "', '" & rtxttotal.Text & "', '" & fecha & "', '" & fechacalendarioentrega & "', '" & sumdeposito & "', '" & sumresto & "',2);"
                Sql = "INSERT INTO `venta` (`idventa`, `idcustomer`,  `idseller`, `total`, `fechaventa`, `fechaentrega`, `deposito`,`resto`,`tipoventa`,`hora`) VALUES ('" & slbfolio.Text & "', '" & idcliente & "', '" & idseller & "', '" & rtxttotal.Text & "', '" & fecha & "', '" & fecha & "','" & rtxtdeposito.Text & "','" & rtxtresto.Text & "',2,'" & hora & "');"
                'Sql = "INSERT INTO `venta` (`idventa`, `idcustomer`,  `idseller`, `total`, `fechaventa`,'', `deposito`,`resto`,`tipoventa`,`hora`) VALUES ('" & slbfolio.Text & "', '" & idcliente & "', '" & idseller & "', '" & rtxttotal.Text & "', '" & fecha & "', '" & rtxtdeposito.Text & "','" & rtxtresto.Text & "',2,'" & hora & "');"

                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                cmd.ExecuteNonQuery()
                conexionMysql.Close()
                'txttotalpagar.Text = ""
                conexionMysql.Close()


                '------------------ FIN insertar reginstro en tabla venta ---------------------------------------


                '------------------ insertar reginstro en tabla ventaIND ---------------------------------------
                Dim i As Integer = rgrilla.RowCount
                Dim j As Integer
                Dim actividad As String
                Dim cantidad, precio, total As Double
                Dim producto, observaciones, clave As String

                conexionMysql.Open()
                '-------------------------------------------------------------
                'suma de valores
                'For j = 0 To i - 2
                '    'MsgBox("valosr de j:" & j)
                '    'a = venta.grillaventa.Item(j, 3).Value.ToString()
                '    clave = rgrilla.Rows(j).Cells(0).Value 'descripcion
                '    producto = rgrilla.Rows(j).Cells(1).Value 'cantidad
                '    precio = rgrilla.Rows(j).Cells(2).Value 'costo
                '    cantidad = rgrilla.Rows(j).Cells(3).Value
                '    total = rgrilla.Rows(j).Cells(4).Value
                '    cerrarconexion()

                '    'MsgBox("el producto es:" & producto)
                '    conexionMysql.Open()
                '    Dim Sql25 As String
                '    '   Sql25 = "INSERT INTO venta_ind (actividad, cantidad, costo, idventa,idproducto) VALUES ('" & producto & "'," & cantidad & "," & precio & "," & lbfolio.Text & ",'" & clave & "');"
                '    '  Dim cmd25 As New MySqlCommand(Sql25, conexionMysql)
                '    '    cmd25.ExecuteNonQuery()
                '    conexionMysql.Close()
                'Next

                '----------------------------- se hace actualización a la tabla de productos--------------
                Dim totalactualizar, m, n As Integer
                'cerrarconexion()

                'conexionMysql.Open()
                'Dim Sql3 As String
                ''consultamos cuantos registros se insertaros para posteriormente actualizarlos en su registro original
                'Sql3 = "select count(distinct idproducto) from ventaind where idventa=" & lbfolio.Text & " and idproducto <> '';"
                'Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
                'reader = cmd3.ExecuteReader()
                'reader.Read()
                'totalactualizar = reader.GetString(0).ToString()
                'conexionMysql.Close()

                'for para dar las vueltas necesarias para poder actualizar
                '---------------DECLARACION DE VARIABLES------------------
                Dim claveproducto, cantidadpro As Integer
                Dim matriz(totalactualizar, 3) As String
                '-------------------------------------------
                cantidadpro = 0
                '----------------------------- se hace actualización a la tabla de productos--------------
                'cerrarconexion()
                'conexionMysql.Open()
                'Dim Sql4 As String
                ''consultamos cuantos registros se insertaros para posteriormente actualizarlos en su registro original
                'Sql4 = "select idproducto, sum(cantidad) from ventaind where idventa=" & lbfolio.Text & "  group by idproducto;"
                'Dim cmd4 As New MySqlCommand(Sql4, conexionMysql)
                'reader = cmd4.ExecuteReader()




                'For m = 1 To totalactualizar
                '    reader.Read()


                '    'guardamos los valores en una matriz
                '    matriz(m, 0) = reader.GetString(0).ToString()
                '    matriz(m, 1) = reader.GetString(1).ToString()
                '    'MsgBox("el registro de la matriz(idproducto):" & matriz(m, 0))
                '    'MsgBox("el registro de la matriz(cantidad):" & matriz(m, 1))
                '    'MsgBox(matriz(m, 0))
                '    'MsgBox(matriz(m, 1))

                'Next
                'conexionMysql.Close()

                '-----------------------consultamos cuantos valores existes de tal idproducto

                Dim x, xcantidad As Integer

                'For x = 1 To totalactualizar
                '    cerrarconexion()

                '    conexionMysql.Open()
                '    xcantidad = 0

                '    'MsgBox("el producto es:" & matriz(x, 0))
                '    Dim Sql6 As String
                '    'consultamos cuantos registros se insertaros para posteriormente actualizarlos en su registro original
                '    Sql6 = "select cantidad,idproducto from producto where idproducto='" & matriz(x, 0) & "';"
                '    Dim cmd6 As New MySqlCommand(Sql6, conexionMysql)
                '    reader = cmd6.ExecuteReader()

                '    reader.Read()
                '    'guardamos los valores en una matriz
                '    xcantidad = reader.GetString(0).ToString()
                '    matriz(x, 1) = xcantidad - matriz(x, 1)
                '    conexionMysql.Close()
                'Next
                '---------------------------------------------------------------------------------------
                'For n = 1 To totalactualizar
                '    '-----------------------ahora sacamos los valores de la matriz para poder actualizarlos
                '    '--------------------------------------------------------------------------------------------
                '    cerrarconexion()

                '    conexionMysql.Open()
                '    'actualizo el dato
                '    Dim Sql5 As String
                '    Sql5 = "UPDATE producto SET cantidad=" & matriz(n, 1) & " WHERE idproducto='" & matriz(n, 0) & "';"
                '    Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
                '    cmd5.ExecuteNonQuery()
                '    conexionMysql.Close()
                '    ' MsgBox("registro actualizado")
                '    '-------------------------------------------------------------------------------------------------
                'Next
                '-------------------------------------------------------------------------------------------
                '--------------------------------------- se manda a imprimir el reporte



                'If chcalcularcambio.Checked = True Then

                '    '-------------------NUEVO SISTEMA

                '    frcambio.ShowDialog()
                '    frcambio.txtpaga.Focus()


                'Else



                MsgBox("Venta realizada", MsgBoxStyle.Information, "Sistema")

                imprimirreparacion()
                MsgBox("Continuar", MsgBoxStyle.Information, "Ctrl+y")
                imprimirreparacionseller()


                rlimpiartodo()

                rcbitemtemporal.Items.Clear()
                cb1.Items.Clear()

                ' End If


                'txtcliente.Text = "USUARIO"
                'listaclientes.Visible = False
                'If chimpresionticket.Checked = True Then
                'impresionticket()

                'vamos a trabajar con el ticket

                'imprimir()






                'End If
                '   If chimpresion.Checked = True Then
                '//////////////////////////////////////////////////////
                ' frx.Show()
                'cargamos el formulario 2, temporalmente para ver si funcionan los reportes.
                ''    Form2.TextBox1.Text = lbfolio.Text
                'Dim newformulario As New frnotaventa
                'aqui le cambien por el formulario de prueba, para coregir
                'el problema de los reportes.
                'newformulario.Show()
                'cagamos los datos a la nota de venta primeroa



                'informe1.Show()





                'este codigo si funcionaba
                '  cargardatosnotaventa()
                ' FRNOTAVENTA.ShowDialog()
                'imprimirnotaventa()
                'End If

            End If

            'una vez guardada la venta, se obtiene un nuevo folio para la proxima venta
            obtenerfolio()





            '------------------------------------------------------------------------
            'al finalizar se limpia la grilla y las cajas de texto
            ' txttotalpagar.Text = ""
            'txtunidades.Text = ""
            'grilla.Rows.Clear()
            'txttotalpagar.Text = ""

            'llamamos para obtener un nuevo folio


            'End If

            '------------------ FIN insertar reginstro en tabla ventaIND ---------------------------------------
            'Catch ex As Exception
            '    comprobartipoingreso()
            '    MsgBox(ex.Message)
            'End 
            'rcbitems.Items.Clear()


        End If


    End Function

    Private Sub rlbcustomer_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles rlbcustomer.MouseDoubleClick
        rseleccioncliente()
    End Sub
    Function rseleccioncliente()
        rtxtcustomername.Text = rlbcustomer.Text
        rlbcustomer.Visible = False
        'MsgBox(rlbcustomer.Text)
        'cargamos los datos del producto
        rcargardatoscliente()
    End Function

    Private Sub Rtxtmodelo_TextChanged(sender As Object, e As EventArgs) Handles rtxtmodelo.TextChanged

    End Sub

    Private Sub Rtxttelephone_TextChanged(sender As Object, e As EventArgs) Handles rtxttelephone.TextChanged

    End Sub

    Function rcargardatoscliente()
        cerrarconexion()

        conexionMysql.Open()
        Dim Sql As String
        Sql = "select * from customer where name='" & rtxtcustomername.Text & "';"
        Dim cmd As New MySqlCommand(Sql, conexionMysql)
        reader = cmd.ExecuteReader()
        reader.Read()
        Try

            rtxtaddress.Text = reader.GetString(2).ToString()
        Catch ex As Exception

        End Try
        Try

            rtxtstate.Text = reader.GetString(3).ToString()
        Catch ex As Exception

        End Try
        Try

            rtxtcity.Text = reader.GetString(4).ToString()
        Catch ex As Exception

        End Try
        Try

            rtxttelephone.Text = reader.GetString(5).ToString()
        Catch ex As Exception

        End Try
        Try

            rtxtemail.Text = reader.GetString(6).ToString()
        Catch ex As Exception

        End Try

        reader.Close()
    End Function

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If txtnombreproducto.Text = "" Or txtpiece.Text = "" Or txtprice.Text = "" Then
            MsgBox("Falta información por agregar", MsgBoxStyle.Information, "MOBI")
        Else


            If statusbusquedaventa = True Then
                MsgBox("Hay una busqueda en curso, cancelaremos la busqueda u vuelta a agregar el producto")
                cancelartodoventa()
                statusbusquedaventa = False

            Else



                Dim dia, mes, año, fecha, fechaclave, claveproducto As String
                hora2 = Now.Hour()
                minuto = Now.Minute()
                segundo = Now.Second()

                hora = hora2 & ":" & minuto & ":" & segundo

                dia = Date.Now.Day
                mes = Date.Now.Month
                año = Date.Now.Year
                fecha = año & "-" & mes & "-" & dia

                fechaclave = año & mes & dia & hora2 & minuto & segundo

                If txtclaveproducto.Text = "" Then
                    claveproducto = fechaclave
                Else
                    claveproducto = txtclaveproducto.Text
                End If
                'consultamos

                Dim i As Integer = sgrilla.RowCount
                Dim gananciatotal As Double
                'txtcostoconproducto.Text = Math.Round(f, 2)
                'MsgBox(txtprice.Text)
                'MsgBox(txtpiece.Text)

                gananciatotal = CDbl(txtpiece.Text) * CDbl(txtprice.Text)
                gananciatotal = Math.Round(gananciatotal, 2)
                sgrilla.Rows.Add(claveproducto, txtnombreproducto.Text, txtprice.Text, txtpiece.Text, gananciatotal)
                ' sgrilla.Columns(1).Width = 350


                srsumatorio()

                slimpiar()


                txtclaveproducto.Focus()
            End If
        End If

    End Sub
    Function slimpiar()
        txtclaveproducto.Text = ""
        txtnombreproducto.Text = ""
        txtprice.Text = ""
        txtpiece.Text = ""

    End Function
    Public Sub srsumatorio()
        Dim i As Integer = sgrilla.RowCount

        Dim j As Integer
        Dim sumacon, sumasin, cantidad_productos, ssumagananciatotal As Double
        Dim a, b, c, d As String
        'suma de valores
        For j = 0 To i - 1
            'MsgBox("valosr de j:" & j)
            'a = venta.grillaventa.Item(j, 3).Value.ToString()
            a = sgrilla.Rows(j).Cells(4).Value 'con cotizador
            'c = sgrilla.Rows(j).Cells(9).Value 'sin cotizador
            'd = sgrilla.Rows(j).Cells(7).Value 'totalganancia

            'b = sgrilla.Rows(j).Cells(3).Value
            'cantidad_productos = cantidad_productos + b
            ' sumacon = sumacon + a
            'sumasin = sumasin + c
            ssumagananciatotal = ssumagananciatotal + a


            'MsgBox(a)
        Next
        ' Try
        'If rtxtcostoreparacion.Text = "" Then
        '    'rtxtcostoreparacion.Text = "0"

        'End If

        stxttotalfinal.Text = CDbl(ssumagananciatotal) '+ CDbl(rtxtcostoreparacion.Text)
        ' Catch ex As Exception

        'End Try

        'stxtpagarsin.Text = sumasin
        'stxttotal.Text = sumasin
        'stxtgananciatotalproductos.Text = sumagananciatotal

        ''el valor siguiente es como funcionaba anteriormente
        ''        stxttotal.Text = suma
        'stxttotalproductos.Text = cantidad_productos
    End Sub

    Private Sub Slbcustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles slbcustomer.SelectedIndexChanged

    End Sub

    Private Sub rtxtcantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtxtcantidad.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub rtxtcostoreparacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtxtcostoreparacion.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub rtxtdeposito_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtxtdeposito.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub rtxttelephone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtxttelephone.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Dim formulario As New frmproducto
        formulario.ShowDialog()

    End Sub

    Private Sub Txtclaveproducto_TextChanged(sender As Object, e As EventArgs) Handles txtclaveproducto.TextChanged
        'grillap.Visible = True
        'grilla2p.Visible = False
        'grillahistorialesproductos.Visible = False

        If txtclaveproducto.Text = "" Then
            slimpiar()

        Else
            sbuscarid()
            lblistaproducto.Visible = False
        End If
    End Sub
    Function sbuscarid()

        If txtclaveproducto.Text = "" Then
            'MsgBox("aqui no paso nada")
        Else
            ' 'MsgBox("comenzamos a buscar")
            Try
                Dim cantidad As Integer

                ' respuesta = vbYes

                cerrarconexion()

                'If reader.HasRows Then
                '    reader.Close()

                'End If

                Dim claveproveedor, clavetipoproducto As Integer

                cerrarconexion()

                Try

                    conexionMysql.Open()
                    Dim Sql As String
                    Sql = "select * from producto where idproducto='" & txtclaveproducto.Text & "';"
                    Dim cmd As New MySqlCommand(Sql, conexionMysql)
                    reader = cmd.ExecuteReader()
                    reader.Read()
                    txtprice.Text = reader.GetString(4).ToString()
                    txtnombreproducto.Text = reader.GetString(1).ToString()
                    ' rtxtcantidad.Text = reader.GetString(2).ToString()
                    ' rtxtcos.Text = reader.GetString(3).ToString()

                    'txtpreciomayoreop.Text = reader.GetString(5).ToString()

                    reader.Close()

                    conexionMysql.Close()

                    'MsgBox("entro")



                Catch ex As Exception
                    btninconsistencia.Visible = True
                    cerrarconexion()
                    'rlimpiar()
                    txtnombreproducto.Text = ""
                    txtpiece.Text = ""
                    txtprice.Text = ""
                End Try


                conexionMysql.Close()




                '   MsgBox(clavetipoproducto)

                If reader.HasRows Then
                    reader.Close()

                End If





            Catch ex As Exception

                cerrarconexion()


                Call slimpiar()



            End Try
        End If
    End Function

    Private Sub Lblistaproducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lblistaproducto.SelectedIndexChanged

    End Sub

    Private Sub slbcustomer_DoubleClick(sender As Object, e As EventArgs) Handles slbcustomer.DoubleClick

    End Sub
    Function sseleccioncliente()
        txtname.Text = slbcustomer.Text
        slbcustomer.Visible = False

        'cargamos los datos del producto
        scargardatoscliente()
    End Function

    Private Sub slbcustomer_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles slbcustomer.MouseDoubleClick
        sseleccioncliente()
    End Sub

    Private Sub Txtnombreproducto_TextChanged(sender As Object, e As EventArgs) Handles txtnombreproducto.TextChanged

        If txtnombreproducto.Text = "" Then
            'lblistaproducto.Visible = False
            'lblistaproducto.Items.Clear()
        Else


            lblistaproducto.Visible = True
            lblistaproducto.Items.Clear()


            'cerramos la conexion
            cerrarconexion()

            Dim cantidad, i As Integer
            cantidad = 0
            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "select count(*) from producto where name like '%" & txtnombreproducto.Text & "%';"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            reader = cmd2.ExecuteReader
            reader.Read()
            cantidad = reader.GetString(0).ToString

            'cargamos el formulario  resumen
            conexionMysql.Close()

            'MsgBox("hay tantos resultados:" & cantidad)

            cerrarconexion()

            If cantidad = 0 Then
                lblistaproducto.Visible = False
                txtclaveproducto.Text = ""
                txtpiece.Text = ""
                txtprice.Text = ""
                stxttotal.Text = ""
            Else



                conexionMysql.Open()
                Dim Sql As String
                Sql = "select name as nombre from producto where name like '%" & txtnombreproducto.Text & "%';"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader
                For i = 1 To cantidad
                    reader.Read()

                    lblistaproducto.Items.Add(reader.GetString(0).ToString)
                Next


                conexionMysql.Close()
                'Catch ex As Exception
            End If

            'End Try
        End If

    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        realizarbusqueda()
    End Sub
    Function realizarbusqueda()


        If rtxtbusquedafolio.Visible = False Then
            rtxtbusquedafolio.Visible = True
            rtxtbusquedafolio.Focus()
        ElseIf rtxtbusquedafolio.Visible = True Then
            'MsgBox("Ahora si buscamos", MsgBoxStyle.Information, "MOBI")
            activarbusqueda = True
            activarreparacion = False

            rcbitemtemporal.Items.Clear()
            rlimpiartodo()
            'cancelartodo()
            'limpiarventa()




            busquedareparacion()

            rchpay.Checked = False


        End If
    End Function
    Function busquedareparacion()
        If rtxtbusquedafolio.Text = "" Then
            ' MsgBox("No hay folios que buscar", MsgBoxStyle.Information, "MOBI")
            cancelartodo()
        Else

            rcbitems.Items.Clear()
            rgrilla.Rows.Clear()
            Dim tipoventa As Integer
            'primero verifico si se trata de un servicio de reparación
            Try
                conexionMysql.Open()
                ' Dim idcustomer, idequipo, idseller As Integer
                Dim Sqlaa As String
                Sqlaa = "select tipoventa from venta where idventa='" & rtxtbusquedafolio.Text & "';"
                Dim cmdaa As New MySqlCommand(Sqlaa, conexionMysql)
                reader = cmdaa.ExecuteReader()
                reader.Read()
                tipoventa = reader.GetString(0).ToString()
                'reader.Close()
                cerrarconexion()
            Catch ex As Exception
                tipoventa = 0
                cerrarconexion()
            End Try


            If tipoventa <= 1 Then
                ' MsgBox("El folio corresponde a una venta directa", MsgBoxStyle.Information, "MOBI")

                cancelartodo()

                'a
            Else


                conexionMysql.Open()
                Dim idequipobus, idcus, idsellerbus, fechaactualizar As String
                Dim Sql As String
                Sql = "select * from venta where idventa='" & rtxtbusquedafolio.Text & "';"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader()
                reader.Read()
                idcus = reader.GetString(1).ToString()
                idsellerbus = reader.GetString(2).ToString()
                rtxttotal.Text = reader.GetString(3).ToString()
                rcalendario.Text = reader.GetString(5).ToString()
                fechaactualizar = reader.GetString(5).ToString()

                'rtxtbalancetotal.Text = 
                rtxtdeposito.Text = reader.GetString(6).ToString()
                'rtxtresto.Text = reader.GetString(7).ToString()

                'rtxttotal.Text = reader.GetString(4).ToString()
                'rcalendario.Text = reader.GetString(6).ToString()
                'reader.Close()
                ' MsgBox(idcustomer)
                'MsgBox(idequipo)
                'MsgBox(idseller)

                cerrarconexion()


                '----------------------comprobar si no tienen fecha, actualizamos la fecha----------------
                Dim fecha As String
                Try

                    conexionMysql.Open()
                    Dim Sqla2 As String
                    Sqla2 = "select datedelivery from equipo where idventa='" & rtxtbusquedafolio.Text & "';"
                    Dim cmda2 As New MySqlCommand(Sqla2, conexionMysql)
                    reader = cmda2.ExecuteReader()
                    reader.Read()
                    fecha = reader.GetString(0).ToString()
                    cerrarconexion()
                Catch ex As Exception
                    cerrarconexion()
                    fecha = ""

                End Try



                'eb caso de que se haga una busqueda con un folio que no tenga el campo deliverydate, entonces lo actualizamos

                If fecha = "" Then

                    Dim fechaactualizarfinal As String
                    fechaactualizarfinal = rcalendario.Value.ToString("yyyy/MM/dd")

                    'si la fecha esta vacia, actualizo el dato, en caso contrario, no hago nada.
                    cerrarconexion()
                    conexionMysql.Open()
                    Dim Sql2 As String
                    Sql2 = "update equipo set datedelivery='" & fechaactualizarfinal & "' where idventa='" & rtxtbusquedafolio.Text & "';"
                    'Sql2 = "UPDATE `mobi`.`customer` SET `name` = '" & rtxtcustomername.Text & "', `address` = '" & rtxtaddress.Text & "', `state` = '" & rtxtstate.Text & "', `city` = '" & rtxtcity.Text & "', `telephone` = '" & rtxttelephone.Text & "', `email` = '" & rtxtemail.Text & "' WHERE (`idcustomer` = '" & customeract & "');"
                    'Sql2 = "update customer set name='" & rtxtcustomername.Text & "', address='" & rtxtaddress.Text & "', state='" & rtxtcity.Text & "', city='" & rtxtstate.Text & "', telephone='" & rtxttelephone.Text & "', email='" & rtxtemail.Text & "' where idcustomer='" & idcustomerbus & "';"
                    Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                    cmd2.ExecuteNonQuery()
                    conexionMysql.Close()
                    MsgBox("Date update", MsgBoxStyle.Information, "MOBI")
                End If

                '------------------------------------------------------------------------------------------




                Try

                    conexionMysql.Open()
                    'Dim idequipobus, idcus, idsellerbus As String
                    Dim Sqla As String
                    Sqla = "select * from venta where idventa='" & rtxtbusquedafolio.Text & "';"
                    Dim cmda As New MySqlCommand(Sqla, conexionMysql)
                    reader = cmda.ExecuteReader()
                    reader.Read()
                    'idcus = reader.GetString(1).ToString()
                    'idsellerbus = reader.GetString(2).ToString()
                    'rtxttotal.Text = reader.GetString(3).ToString()
                    'rcalendario.Text = reader.GetString(5).ToString()
                    ''rtxtbalancetotal.Text = 
                    'rtxtdeposito.Text = reader.GetString(6).ToString()
                    rtxtresto.Text = reader.GetString(7).ToString()

                    'rtxttotal.Text = reader.GetString(4).ToString()
                    'rcalendario.Text = reader.GetString(6).ToString()
                    'reader.Close()
                    ' MsgBox(idcustomer)
                    'MsgBox(idequipo)
                    'MsgBox(idseller)
                    cerrarconexion()
                Catch ex As Exception

                End Try

                '                MsgBox(rtxttotal.Text)

                '----------------------------------
                cerrarconexion()

                ' MsgBox(idcus)
                '----------------------------------------------------------------------------------------------------------------------

                conexionMysql.Open()
                'Dim idcustomer, idequipo, idseller As Integer
                Dim Sql22x As String
                Sql22x = "SELECT * FROM customer where idcustomer=" & idcus & ";"
                Dim cmd22x As New MySqlCommand(Sql22x, conexionMysql)
                reader = cmd22x.ExecuteReader()
                reader.Read()


                Try

                    rtxtemail.Text = reader.GetString(6).ToString()
                Catch ex As Exception

                End Try
                Try

                    rtxtstate.Text = reader.GetString(3).ToString()
                Catch ex As Exception

                End Try
                Try

                    rtxtcity.Text = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                Try

                    rtxttelephone.Text = reader.GetString(5).ToString()
                Catch ex As Exception

                End Try
                Try

                    rtxtcustomername.Text = reader.GetString(1).ToString()
                Catch ex As Exception

                End Try

                cerrarconexion()

                ' Try
                ' MsgBox(rtxttotal.Text)
                'cargamos los datos necesarios
                conexionMysql.Open()
                'Dim idcustomer, idequipo, idseller As Integer
                Dim Sql22 As String
                Sql22 = "select * from customer where idcustomer=" & idcus & ";"
                Dim cmd22 As New MySqlCommand(Sql22, conexionMysql)
                reader = cmd22.ExecuteReader()
                reader.Read()

                Try

                    rtxtaddress.Text = reader.GetString(2).ToString()
                Catch ex As Exception

                End Try
                Try

                    rtxtstate.Text = reader.GetString(3).ToString()
                Catch ex As Exception

                End Try
                Try

                    rtxtcity.Text = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                Try

                    rtxttelephone.Text = reader.GetString(5).ToString()
                Catch ex As Exception

                End Try
                Try

                    rtxtcustomername.Text = reader.GetString(1).ToString()
                Catch ex As Exception

                End Try


                '----------------------------------------------------------------------------------------------------
                'reader.Close()
                cerrarconexion()

                ' MsgBox("1")

                rlbcustomer.Visible = False
                Dim seller As String
                Try
                    '-------------------------
                    conexionMysql.Open()
                    ' Dim idcustomer, idequipo, idseller As Integer
                    Dim Sqlx2 As String
                    Sqlx2 = "select name_seller from seller where idseller='" & idsellerbus & "';"
                    Dim cmdx2 As New MySqlCommand(Sqlx2, conexionMysql)
                    reader = cmdx2.ExecuteReader()
                    reader.Read()
                    seller = reader.GetString(0).ToString()
                    'reader.Close()
                    cerrarconexion()
                    'MsgBox("2")
                    'MsgBox(seller)


                Catch ex As Exception
                    seller = "seller"
                    cerrarconexion()

                End Try

                rcbseller.Text = seller

                '----------------------

                conexionMysql.Open()
                ' Dim idcustomer, idequipo, idseller As Integer
                Dim Sqlx3 As String
                Sqlx3 = "select count(*) as contador from equipo where idventa=" & rtxtbusquedafolio.Text & ";"
                Dim cmdx3 As New MySqlCommand(Sqlx3, conexionMysql)
                reader = cmdx3.ExecuteReader()
                reader.Read()
                Dim cantidad As Integer = reader.GetString(0).ToString()
                ' reader.Close()
                cerrarconexion()
                'MsgBox("2")
                'MsgBox(cantidad)


                '------------------------

                Dim idequipo, equipo, modelo, imei, password, status, problema, nota, idcustomer2, reparacion, partes, idventa As String
                Dim ii As Integer
                '-------------------------------------- buscamos los valores de la grilla y lo mandamos a eso
                'reader.Close()

                cerrarconexion()
                conexionMysql.Open()
                Dim clave, costo, actividad, posicion, fechastatus As String
                Dim Sql3 As String
                Sql3 = "select * from equipo where idventa='" & rtxtbusquedafolio.Text & "';"
                Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
                reader = cmd3.ExecuteReader()

                For ii = 0 To cantidad - 1
                    'MsgBox(ii)
                    reader.Read()


                    idequipo = reader.GetString(0).ToString()
                    equipo = reader.GetString(1).ToString()

                    modelo = reader.GetString(2).ToString()
                    imei = reader.GetString(3).ToString()
                    password = reader.GetString(4).ToString()
                    status = reader.GetString(5).ToString()
                    problema = reader.GetString(6).ToString()
                    nota = reader.GetString(7).ToString()
                    idcustomer2 = reader.GetString(8).ToString()
                    reparacion = reader.GetString(9).ToString()
                    partes = reader.GetString(10).ToString()
                    idventa = reader.GetString(11).ToString()

                    posicion = reader.GetString(12).ToString()



                    fechastatus = reader.GetString(13).ToString()
                    rtxtdatestatus.Text = fechastatus




                    rgrilla2.Rows.Add(idequipo, equipo, modelo, imei, password, status, problema, nota, idcustomer2, reparacion, partes, idventa)


                    'agregamos la fecha del status de la actualizacion
                    'calendariostatus.Value = New Date(1000, 12, 23)


                    rcbitemtemporal.Items.Add(equipo)
                    cb1.Items.Add(idequipo)

                    ''ocultamos el combotemporal ymostramos el combo de busqueda
                    'rcbitems.Visible = True
                    'rcbitemtemporal.Visible = False
                Next



                Try

                    rcbitemtemporal.SelectedIndex = 0
                    'seleccionaritem()
                Catch ex As Exception

                End Try

                ''----------------------------------
                'conexionMysql.Open()
                'Dim equipoultimo As String
                'Dim Sql34 As String
                'Sql34 = "select * from equipo where idventa='" & rtxtbusquedafolio.Text & "';"
                'Dim cmd34 As New MySqlCommand(Sql34, conexionMysql)
                'reader = cmd34.ExecuteReader()

                'For ii = 0 To cantidad - 1
                '    'MsgBox(ii)
                '    reader.Read()

                '    rcbitemtemporal.Items.Add(equipo)
                '    'rcbitemtemporal.SelectedIndex = 0

                '    ''ocultamos el combotemporal ymostramos el combo de busqueda
                '    'rcbitems.Visible = True
                '    'rcbitemtemporal.Visible = False
                'Next




                '----------------------------------
                'cerrarconexion()

                'conexionMysql.Open()
                'Dim Sql2 As String
                'Sql2 = "select * from tipo_pago;"
                'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                'reader = cmd2.ExecuteReader()

                'For i = 1 To cantidadproveedor

                '    reader.Read()

                '    cbformadepago.Items.Add(reader.GetString(1).ToString())
                '    cbformadepagoservicios.Items.Add(reader.GetString(1).ToString())
                'Next
                '---------------------------------




                reader.Close()
                conexionMysql.Close()
                cerrarconexion()
                '----------------

                'conexionMysql.Open()
                ''Dim idcustomer, idequipo, idseller As Integer
                'Dim Sql23 As String
                'Sql23 = "select * from equipo where idequipo='" & rtxtbusquedafolio.Text & "';"
                'Dim cmd23 As New MySqlCommand(Sql23, conexionMysql)
                'reader = cmd23.ExecuteReader()
                'reader.Read()
                'rtxtequipo.Text = reader.GetString(1).ToString()
                'rtxtmodelo.Text = reader.GetString(2).ToString()
                'rtxtimei.Text = reader.GetString(3).ToString()
                'rtxtpassword.Text = reader.GetString(4).ToString()
                'cbstate.Text = reader.GetString(5).ToString()
                'rtxtproblem.Text = reader.GetString(6).ToString()
                'rtxtnote.Text = reader.GetString(7).ToString()

                'reader.Close()
                'cerrarconexion()

                'conexionMysql.Open()
                ''Dim idcustomer, idequipo, idseller As Integer
                'Dim Sql24 As String
                'Sql24 = "select name_seller from seller  where idseller='" & idseller & "';"
                'Dim cmd24 As New MySqlCommand(Sql24, conexionMysql)
                'reader = cmd24.ExecuteReader()
                'reader.Read()

                'Dim seller As String = reader.GetString(0).ToString()
                'MsgBox(seller)

                'reader.Close()
                'cerrarconexion()
                ' Catch ex As Exception

                'End Try

                '-------------------------------

                '-------------------------------------- buscamos los valores de la grilla y lo mandamos a eso
                'cerrarconexion()
                'conexionMysql.Open()
                'Dim contador As Integer
                'Dim Sql2 As String
                'Sql2 = "select count(*) as contador from venta_ind where idventa='" & rtxtbusquedafolio.Text & "';"
                'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                'reader = cmd2.ExecuteReader()
                'reader.Read()
                'contador = reader.GetString(0).ToString()
                'reader.Close()
                'conexionMysql.Close()
                'cerrarconexion()
                ''---------------------------------------------
                'If contador > 0 Then


                '    '-------------------------------------- buscamos los valores de la grilla y lo mandamos a eso
                '    cerrarconexion()
                '    conexionMysql.Open()
                '    'Dim clave, costo, cantidad, actividad As String
                '    Dim Sql33 As String
                '    Sql33 = "select * from venta_ind where idventa='" & rtxtbusquedafolio.Text & "';"
                '    Dim cmd33 As New MySqlCommand(Sql33, conexionMysql)
                '    reader = cmd33.ExecuteReader()

                '    For i = 0 To contador - 1

                '        reader.Read()


                '        clave = reader.GetString(5).ToString()
                '        actividad = reader.GetString(1).ToString()
                '        cantidad = reader.GetString(3).ToString()
                '        costo = reader.GetString(2).ToString()


                '        rgrilla.Rows.Add(clave, actividad, cantidad, costo, CDbl(cantidad) * CDbl(costo))


                '    Next




                '    reader.Close()
                '    conexionMysql.Close()
                '    cerrarconexion()
                '    '---------------------------------------------

                'End If


            End If
        End If



    End Function

    Private Sub Rtxtbusquedafolio_TextChanged(sender As Object, e As EventArgs) Handles rtxtbusquedafolio.TextChanged
        'realizarbusqueda()



    End Sub

    Function scargardatoscliente()
        Try

            conexionMysql.Open()
            Dim Sql As String
            Sql = "select * from customer where name='" & txtname.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            txtaddress.Text = reader.GetString(2).ToString()
            txtstate.Text = reader.GetString(3).ToString()
            txtcity.Text = reader.GetString(4).ToString()
            txttelephone.Text = reader.GetString(5).ToString()
            stxtemail.Text = reader.GetString(6).ToString()


            reader.Close()
        Catch ex As Exception

        End Try

    End Function

    Private Sub lblistaproducto_DoubleClick(sender As Object, e As EventArgs) Handles lblistaproducto.DoubleClick
        'sseleccionpieza()
    End Sub

    Private Sub Rtxtaddress_TextChanged(sender As Object, e As EventArgs) Handles rtxtaddress.TextChanged
        rlbcustomer.Visible = False

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'e.Graphics.DrawString(txtetiqueta1, New Font("verdana", 11, FontStyle.Bold), New SolidBrush(Color.Black), 1, 9)
        'e.Graphics.DrawString(txtetiqueta2, New Font("verdana", 9, FontStyle.Bold), New SolidBrush(Color.Black), 1, 28)
        'e.Graphics.DrawString(txtetiqueta, New Font("verdana", 13, FontStyle.Bold), New SolidBrush(Color.Black), 1, 57)
        'traemos la información del ticket, como encabezado, datos de la empresa etc.
        Dim saludo, ticketnombre, ticketcoloniaciudad, tickettelefono, ticketgiro, p1, p2, p3, p4, comentario As String
        Dim callenumero, cp, estado, whatsapp, correo, rfc, s2, s4, s3, s5 As String
        Dim x, y, tfuente, tfuente2, tfuente3 As Integer
        'Try

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
        's2 = reader.GetString(14).ToString()
        s2 = reader.GetString(25).ToString()
        s3 = reader.GetString(26).ToString()
        s4 = reader.GetString(27).ToString()
        s5 = reader.GetString(28).ToString()

        rfc = reader.GetString(22).ToString()

        conexionMysql.Close()

        ' Catch ex As Exception
        cerrarconexion()
        'End Try

        tfuente = 10 '7
        tfuente2 = 14
        tfuente3 = 10
        p1 = 10 'posicion de X
        x = 5
        y = 125
        Dim ii, incremento As Integer
        incremento = 14
        Dim yy(20) As Integer
        For ii = 0 To 20
            y = y + incremento
            yy(ii) = y
        Next



        Try
            ' La fuente a usar
            Dim prFont As New Font("Arial", 15, FontStyle.Bold)
            'POSICION DEL LOGO
            ' la posición superior
            e.Graphics.DrawImage(pblogoticket.Image, 50, 20, 110, 110)


            'imprimir el titutlo del ticket



            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString(ticketnombre, prFont, Brushes.Black, x, yy(0))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))
            'IMPRESION DE LOGOTIPO,
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(callenumero, prFont, Brushes.Black, x, yy(3))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(ticketcoloniaciudad, prFont, Brushes.Black, x, yy(4))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(cp, prFont, Brushes.Black, x, yy(5))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("TEL:" & tickettelefono, prFont, Brushes.Black, x, yy(6))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("LIC." & rfc, prFont, Brushes.Black, x, yy(7))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, yy(8))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Técnico" & cbseller.Text, prFont, Brushes.Black, x, yy(9))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Cliente:" & txtname.Text, prFont, Brushes.Black, x, yy(10))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Fecha:" & Date.Now, prFont, Brushes.Black, x, yy(11))
            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString("Orden #" & lbfolio.Text, prFont, Brushes.Black, x, yy(12))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, yy(13))

            'imprimir el titutlo del ticket

            prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            e.Graphics.DrawString("Producto", prFont, Brushes.Black, x, yy(14))
            prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            e.Graphics.DrawString("ID------Precio------Cant----TOTAL", prFont, Brushes.Black, x, yy(15))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, yy(16))



            'aqui es donde tenemos que leer todos los datos de la grilla llamada "grilla"

            Dim i As Integer = sgrilla.RowCount
            'MsgBox(i)
            Dim t1, t2, t3, t4, t5 As Integer
            Dim actividad As String
            Dim cantidad, costo, idventa As Double
            Dim idproducto As String
            Dim jj As Integer
            t1 = yy(17)
            t2 = yy(18)
            t3 = yy(19)
            t4 = 40
            'MsgBox(jj)m
            'suma de valores
            For jj = 0 To i - 1



                '   MsgBox("valosr de j:" & jj)
                'a = venta.grillaventa.Item(j, 3).Value.ToString()
                actividad = sgrilla.Rows(jj).Cells(1).Value 'descripcion
                cantidad = sgrilla.Rows(jj).Cells(2).Value 'cantidad
                costo = sgrilla.Rows(jj).Cells(3).Value 'costo
                idproducto = sgrilla.Rows(jj).Cells(0).Value
                comentario = sgrilla.Rows(jj).Cells(4).Value
                'cerrarconexion()
                'conexionMysql.Open()

                ' MsgBox("el producto es:" & actividad)

                'Dim Sql2 As String
                'Sql2 = "INSERT INTO ventaind (actividad, cantidad, costo, idventa,idproducto) VALUES ('" & actividad & "'," & cantidad & "," & costo & "," & lbfolio.Text & ",'" & idproducto & "');"
                'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                'cmd2.ExecuteNonQuery()
                'conexionMysql.Close()

                prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                e.Graphics.DrawString(actividad, prFont, Brushes.Black, x, t1)

                prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t2)


                'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                'e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t3)
                '----------
                prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                e.Graphics.DrawString("$" & costo, prFont, Brushes.Black, x, t3)
                '----------
                prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                e.Graphics.DrawString(cantidad, prFont, Brushes.Black, x + 80, t3)
                '-----------

                prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                e.Graphics.DrawString("$" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, x + 160, t3)
                '-----------



                'prFont = New Font("Arial", 10, FontStyle.Bold)
                'e.Graphics.DrawString(cantidad & "-- $" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, 0, t3)

                t1 = t1 + (incremento * 3.2)
                t2 = t2 + (incremento * 3.2)
                t3 = t3 + (incremento * 3.2)

            Next

            t1 = t1 - (incremento * 2)
            t2 = t2 - (incremento * 2)
            t3 = t3 - (incremento * 2)


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, 0, t3)

            '----------------AQUI SE IMPRIME EL TOTAL A PAGAR

            prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            e.Graphics.DrawString("   Total $" & stxttotalfinal.Text, prFont, Brushes.Black, x, t3 + incremento)
            'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            'e.Graphics.DrawString("Efectivo---->$" & lbpagacon.Text, prFont, Brushes.Black, x, t3 + (incremento * 2))
            'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            'e.Graphics.DrawString("  Cambio---->$" & lbcambio.Text, prFont, Brushes.Black, x, t3 + (incremento * 3))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, t3 + (incremento * 4))
            prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            e.Graphics.DrawString(saludo, prFont, Brushes.Black, x, t3 + (incremento * 5))
            prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            e.Graphics.DrawString(s2, prFont, Brushes.Black, x, t3 + (incremento * 6))
            prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            e.Graphics.DrawString(s3, prFont, Brushes.Black, x, t3 + (incremento * 7))
            prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            e.Graphics.DrawString(s4, prFont, Brushes.Black, x, t3 + (incremento * 8))
            prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            e.Graphics.DrawString(s5, prFont, Brushes.Black, x, t3 + (incremento * 9))
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

    Private Sub PrintDocument2_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage


        Dim folio As Integer
        If activarbusqueda = True Then
            folio = rtxtbusquedafolio.Text
        Else
            folio = slbfolio.Text
        End If

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

        tfuente = 10 '7
        tfuente2 = 14
        tfuente3 = 16
        p1 = 10 'posicion de X
        x = 5
        y = 125
        Dim ii, incremento As Integer
        incremento = 14
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
            e.Graphics.DrawImage(pblogoticket.Image, 50, 20, 120, 120)


            'imprimir el titutlo del ticket



            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString(ticketnombre, prFont, Brushes.Black, x, yy(0))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))
            'IMPRESION DE LOGOTIPO,
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(callenumero, prFont, Brushes.Black, x, yy(3))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(ticketcoloniaciudad, prFont, Brushes.Black, x, yy(4))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(cp, prFont, Brushes.Black, x, yy(5))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("TEL:" & tickettelefono, prFont, Brushes.Black, x, yy(6))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("RFC:" & rfc, prFont, Brushes.Black, x, yy(7))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, yy(8))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Técnico" & rcbseller.Text, prFont, Brushes.Black, x, yy(9))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Cliente:" & rtxtcustomername.Text, prFont, Brushes.Black, x, yy(10))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Fecha:" & Date.Now, prFont, Brushes.Black, x, yy(11))
            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString("ORDEN #" & folio, prFont, Brushes.Black, x, yy(12))


            '---------------------------------------------------------------------------------------------------------------------------------
            'consulto, cuantos dispositivos son, para obtener su informacion

            Dim cantidaddispositivos, vueltas As Integer
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql12 As String
            'temporalmente slbfolio.text por rtxtbusquedatemporal
            Sql12 = "select count(*) from equipo where idventa='" & folio & "';"
            Dim cmd12 As New MySqlCommand(Sql12, conexionMysql)
            reader = cmd12.ExecuteReader()
            reader.Read()
            cantidaddispositivos = reader.GetString(0).ToString()
            'callenumero = reader.GetString(2).ToString()
            'ticketcoloniaciudad = reader.GetString(3).ToString()
            'cp = reader.GetString(4).ToString()
            conexionMysql.Close()
            cerrarconexion()

            conexionMysql.Open()
            Dim Sql123, problema, equipo, modelo, imei, estadox As String
            Sql123 = "select * from equipo where idventa='" & folio & "';"
            Dim cmd123 As New MySqlCommand(Sql123, conexionMysql)
            reader = cmd123.ExecuteReader()

            Dim pp1, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14 As Integer

            pp1 = yy(13)

            p2 = yy(14)
            p3 = yy(15)
            p4 = yy(16)
            p5 = yy(17)
            p6 = yy(18)
            p7 = yy(19)
            p8 = yy(20)
            p9 = yy(21)
            p10 = yy(22)
            p11 = yy(23)
            p12 = yy(24)
            p13 = yy(25)
            p14 = yy(26)




            For vueltas = 1 To cantidaddispositivos


                reader.Read()
                equipo = reader.GetString(1).ToString()
                modelo = reader.GetString(2).ToString()
                imei = reader.GetString(3).ToString()
                estadox = reader.GetString(5).ToString()
                problema = reader.GetString(6).ToString()


                'imprimir el titutlo del ticket
                '----------------------------------------------------------------------------------------------------------------------------

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, pp1)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("--PROBLEMA--", prFont, Brushes.Black, x, p2)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString(problema, prFont, Brushes.Black, x, p3)

                'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                'e.Graphics.DrawString("ID------PRECIO------CANTIDAD----TOTAL", prFont, Brushes.Black, x, yy(15))





                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p4)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("--Dispositivo--", prFont, Brushes.Black, x, p5)
                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Nombre Dispo.:" & equipo, prFont, Brushes.Black, x, p6)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Modelo:" & modelo, prFont, Brushes.Black, x, p7)


                'prFont = New Font("Arial", tfuente, FontStyle.Bold)
                'e.Graphics.DrawString("IMEI:" & imei, prFont, Brushes.Black, x, p8)


                'prFont = New Font("Arial", tfuente, FontStyle.Bold)
                'e.Graphics.DrawString("Status:" & estadox, prFont, Brushes.Black, x, p9)
                '----------------------------------------------------------------------------------------------------------------------

                pp1 = pp1 + (incremento * 7)
                p2 = p2 + (incremento * 7)
                p3 = p3 + (incremento * 7)
                p4 = p4 + (incremento * 7)
                p5 = p5 + (incremento * 7)
                p6 = p6 + (incremento * 7)
                p7 = p7 + (incremento * 7)
                p8 = p8 + (incremento * 7)
                p9 = p9 + (incremento * 7)
                p10 = p10 + (incremento * 7)
                p11 = p11 + (incremento * 7)
                p12 = p12 + (incremento * 7)
                p13 = p13 + (incremento * 7)
                p14 = p14 + (incremento * 7)
                '                yy = yy + (incremento * 3.2)
                '               yy = yy + (incremento * 3.2)




            Next
            conexionMysql.Close()
            cerrarconexion()

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, 0, pp1)



            Dim fechacalendarioentrega As String
            fechacalendarioentrega = rcalendario.Value.ToString("MM/dd/yyyy")


            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("Delivery date:" & fechacalendarioentrega, prFont, Brushes.Black, x, p2)


            'aqui es donde tenemos que leer todos los datos de la grilla llamada "grilla"

            Dim i As Integer = sgrilla.RowCount
            'MsgBox(i)
            Dim t1, t2, t3, t4, t5 As Integer
            Dim actividad As String
            Dim cantidad, costo, idventa As Double
            Dim idproducto As String
            Dim jj As Integer
            ' t1 = yy(16)
            ' t2 = yy(17)
            ' t3 = yy(18)
            ' t4 = 40
            'MsgBox(jj)
            'MsgBox()
            'suma de valores
            '''''''For jj = 0 To i - 1



            '   MsgBox("valosr de j:" & jj)
            'a = venta.grillaventa.Item(j, 3).Value.ToString()
            '''''''actividad = sgrilla.Rows(jj).Cells(1).Value 'descripcion
            '''''''cantidad = sgrilla.Rows(jj).Cells(2).Value 'cantidad
            '''''''costo = sgrilla.Rows(jj).Cells(3).Value 'costo
            '''''''idproducto = sgrilla.Rows(jj).Cells(0).Value
            '''''''comentario = sgrilla.Rows(jj).Cells(4).Value
            'cerrarconexion()
            'conexionMysql.Open()

            ' MsgBox("el producto es:" & actividad)

            'Dim Sql2 As String
            'Sql2 = "INSERT INTO ventaind (actividad, cantidad, costo, idventa,idproducto) VALUES ('" & actividad & "'," & cantidad & "," & costo & "," & lbfolio.Text & ",'" & idproducto & "');"
            'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            'cmd2.ExecuteNonQuery()
            'conexionMysql.Close()


            'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t2)


            '    'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    'e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t3)
            '    '----------
            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString("$" & costo, prFont, Brushes.Black, x, t3)
            '    '----------
            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString(cantidad, prFont, Brushes.Black, x + 80, t3)
            '    '-----------

            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString("$" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, x + 160, t3)
            ''-----------



            'prFont = New Font("Arial", 10, FontStyle.Bold)
            'e.Graphics.DrawString(cantidad & "-- $" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, 0, t3)

            '''''''   t1 = t1 + (incremento * 3.2)
            '''''''t2 = t2 + (incremento * 3.2)
            '''''''t3 = t3 + (incremento * 3.2)

            ''''''' Next

            t1 = t1 - (incremento * 2)
            t2 = t2 - (incremento * 2)
            t3 = t3 - (incremento * 2)



            '----------------AQUI SE IMPRIME EL TOTAL A PAGAR
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql14, total, deposito, resto As String
            Sql14 = "select total,deposito,resto from venta where idventa='" & folio & "';"
            Dim cmd14 As New MySqlCommand(Sql14, conexionMysql)
            reader = cmd14.ExecuteReader()
            reader.Read()
            total = reader.GetString(0).ToString()
            deposito = reader.GetString(1).ToString()
            resto = reader.GetString(2).ToString()

            conexionMysql.Close()

            cerrarconexion()


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Total:", prFont, Brushes.Black, x, p3)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & total, prFont, Brushes.Black, x + 150, p4)

            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("Price:", prFont, Brushes.Black, x, p5)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("$ " & rtxtcostoreparacion.Text, prFont, Brushes.Black, x + 150, p6)


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Deposito:", prFont, Brushes.Black, x, p5)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & deposito, prFont, Brushes.Black, x + 150, p6)


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Resto:", prFont, Brushes.Black, x, p7)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & resto, prFont, Brushes.Black, x + 150, p8)



            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p9)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo, prFont, Brushes.Black, x, p10)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo2, prFont, Brushes.Black, x, p11)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo3, prFont, Brushes.Black, x, p12)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo4, prFont, Brushes.Black, x, p13)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo5, prFont, Brushes.Black, x, p14)
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

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        cancelartodo()

        obtenerfolio()
        '        eliminarregistrostemporalesequipo()

    End Sub
    Function cancelartodo()

        Dim respuesta As String
        If activarreparacion = True Then

            respuesta = MsgBox("Hay una venta en curso, ¿desea cancelarla?", MsgBoxStyle.YesNo, "MOBI")

            If respuesta = vbYes Then
                eliminarregistrostemporalesequipo()
                activarbusqueda = False
                rtxtbusquedafolio.Visible = False
                rlimpiartodo()
                habilitarcajasdevice()
                comprobarventarealizada()
                rcbitems.Visible = False
                rcbitemtemporal.Visible = True
                rcbitemtemporal.Items.Clear()
                rcbitems.Items.Clear()
                activarreparacion = False
                activarbusqueda = False
                rtxtclaveproducto.Text = ""
                cb1.Items.Clear()
                'rtxtbalancetotal.Text = ""
                rchpay.Checked = False
                rtxtdatestatus.Text = ""
            End If
        Else

            activarbusqueda = False
            rtxtbusquedafolio.Visible = False
            rlimpiartodo()
            habilitarcajasdevice()
            comprobarventarealizada()
            rcbitems.Visible = False
            rcbitemtemporal.Visible = True
            rcbitemtemporal.Items.Clear()
            rcbitems.Items.Clear()
            activarreparacion = False
            activarbusqueda = False
            rtxtclaveproducto.Text = ""
            cb1.Items.Clear()
            'rtxtbalancetotal.Text = ""
            rchpay.Checked = False
            rtxtdatestatus.Text = ""
            lblistarespuestos.Visible = False

        End If

    End Function
    Function comprobarventarealizada()
        'comprobamos que ya este la venta realizada
        cerrarconexion()
        Dim folioventa, folioventa2 As String
        Try

            conexionMysql.Open()
            Dim Sql As String
            Sql = "select * from venta where idventa='" & slbfolio.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            folioventa = reader.GetString(0).ToString()
            reader.Close()
        Catch ex As Exception
            folioventa = 0
        End Try

        '---------------------------------------------

        Try

            conexionMysql.Open()
            Dim Sql1 As String
            Sql1 = "SELECT * FROM equipo where idventa='" & slbfolio.Text & "';"
            Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
            reader = cmd1.ExecuteReader()
            reader.Read()
            folioventa2 = reader.GetString(0).ToString()
            reader.Close()
            '---------------------------------------------
        Catch ex As Exception
            folioventa2 = 1
        End Try



        If folioventa = folioventa2 Then
        Else
            eliminarregistrostemporalesequipo()
        End If

    End Function
    Function eliminarregistrostemporalesequipo()
        Try


            cerrarconexion()
            conexionMysql.Open()
            Dim sql2 As String
            sql2 = "delete from equipo where idventa='" & slbfolio.Text & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()
            cerrarconexion()
            conexionMysql.Open()
            Dim sql23 As String
            sql23 = "delete from venta_ind where idventa='" & slbfolio.Text & "';"
            Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
            cmd23.ExecuteNonQuery()

            conexionMysql.Close()
        Catch ex As Exception

        End Try


    End Function
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        'Try

        If activarbusqueda = False Then
            MsgBox("No hay servicios por actualizar", MsgBoxStyle.Information, "MOBI")


        Else


            'consultamos quien es el customer para actualizar sus datos



            cerrarconexion()
            Dim customeract As Integer
            conexionMysql.Open()
            Dim Sql As String
            Sql = "select idcustomer from customer where name='" & rtxtcustomername.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            customeract = reader.GetString(0).ToString()
            reader.Close()
            '---------------------------------------------

            ' MsgBox(rtxtbusquedafolio.Text)
            ' MsgBox(rtxtequipo.Text)

            'actualizamos el registro
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "UPDATE `customer` SET `name` = '" & rtxtcustomername.Text & "', `address` = '" & rtxtaddress.Text & "', `state` = '" & rtxtstate.Text & "', `city` = '" & rtxtcity.Text & "', `telephone` = '" & rtxttelephone.Text & "', `email` = '" & rtxtemail.Text & "' WHERE (`idcustomer` = '" & customeract & "');"
            'Sql2 = "update customer set name='" & rtxtcustomername.Text & "', address='" & rtxtaddress.Text & "', state='" & rtxtcity.Text & "', city='" & rtxtstate.Text & "', telephone='" & rtxttelephone.Text & "', email='" & rtxtemail.Text & "' where idcustomer='" & idcustomerbus & "';"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            cmd2.ExecuteNonQuery()
            conexionMysql.Close()
            'MsgBox("Se ha asignado por default la impresora que esta predeterminada en el sistema", MsgBoxStyle.Information, "CTRL+y")

            '            MsgBox("Hola esto es una prueba" & Chr(13) &
            '" de un textbox multilinea" 
            'eliminamos todos los registros de la grilla o de ventaindividual, 



            cerrarconexion()
            conexionMysql.Open()
            Dim Sql23 As String
            Sql23 = "UPDATE `equipo` SET  `modelo` = '" & rtxtmodelo.Text & "', `imei` = '" & rtxtimei.Text & "', `password` = '" & rtxtpassword.Text & "', `status` = '" & cbstate.Text & "', `problema` = '" & rtxtproblem.Text & "', `nota` = '" & rtxtnote.Text & "' WHERE (equipo='" & rtxtequipo.Text & "' and idventa='" & rtxtbusquedafolio.Text & "' and idequipo='" & cb1.Text & "');"
            'Sql23 = "update equipo set modelo='" & rtxtmodelo.Text & "', imei='" & rtxtimei.Text & "', password='" & rtxtpassword.Text & "', status='" & cbstate.Text & "', problema='" & rtxtproblem.Text & "', nota='" & rtxtnote.Text & "' where idequipo='" & rtxtequipo.Text & "' and idventa='" & rtxtbusquedafolio.Text & "';"
            Dim cmd23 As New MySqlCommand(Sql23, conexionMysql)
            cmd23.ExecuteNonQuery()
            conexionMysql.Close()
            MsgBox("registro actualizado", MsgBoxStyle.Information, "MOBI")
        End If


        '    Dim fechacalendarioentrega As String
        '    fechacalendarioentrega = rcalendario.Value.ToString("yyyy/MM/dd")


        '    cerrarconexion()
        '    conexionMysql.Open()
        '    Dim Sql24 As String
        '    Sql24 = "update venta set total='" & rtxttotal.Text & "', fechaentrega='" & fechacalendarioentrega & "', costo_reparacion='" & rtxtcostoreparacion.Text & "', deposito='" & rtxtdeposito.Text & "', resto='" & rtxtresto.Text & "' where idventa='" & rtxtbusquedafolio.Text & "';"
        '    Dim cmd24 As New MySqlCommand(Sql24, conexionMysql)
        '    cmd24.ExecuteNonQuery()
        '    conexionMysql.Close()
        '    '--------------------------------
        '    'eliminamos los registros que existan 
        '    '--------------------------------
        '    cerrarconexion()
        '    conexionMysql.Open()
        '    Dim Sql26 As String
        '    Sql26 = "delete from venta_ind where idventa=" & rtxtbusquedafolio.Text & ";"
        '    Dim cmd26 As New MySqlCommand(Sql26, conexionMysql)
        '    cmd26.ExecuteNonQuery()
        '    conexionMysql.Close()

        '    '------------------ insertar reginstro en tabla ventaIND ---------------------------------------
        '    Dim j As Integer
        '    Dim i As Integer = rgrilla.RowCount
        '    Dim clave As String
        '    Dim actividad As String
        '    Dim cantidad, precio, total As Double
        '    Dim producto, observaciones As String

        '    conexionMysql.Open()

        '    'suma de valores
        '    For j = 0 To i - 2
        '        'MsgBox("valosr de j:" & j)
        '        'a = venta.grillaventa.Item(j, 3).Value.ToString()
        '        clave = rgrilla.Rows(j).Cells(0).Value 'descripcion
        '        producto = rgrilla.Rows(j).Cells(1).Value 'cantidad
        '        precio = rgrilla.Rows(j).Cells(2).Value 'costo
        '        cantidad = rgrilla.Rows(j).Cells(3).Value
        '        total = rgrilla.Rows(j).Cells(4).Value
        '        cerrarconexion()

        '        'MsgBox()
        '        conexionMysql.Open()
        '        Dim Sql27 As String
        '        Sql27 = "INSERT INTO venta_ind (actividad, cantidad, costo, idventa,idproducto) VALUES ('" & producto & "'," & cantidad & "," & precio & "," & rtxtbusquedafolio.Text & ",'" & clave & "');"
        '        Dim cmd27 As New MySqlCommand(Sql27, conexionMysql)
        '        cmd27.ExecuteNonQuery()
        '        conexionMysql.Close()

        '    Next

        '    '----------------------------- se hace actualización a la tabla de productos--------------
        '    cerrarconexion()
        '    Dim idseller As Integer
        '    conexionMysql.Open()
        '    Dim Sql As String
        '    Sql = "select idseller from seller where name_seller='" & rcbseller.Text & "';"
        '    Dim cmd As New MySqlCommand(Sql, conexionMysql)
        '    reader = cmd.ExecuteReader()
        '    reader.Read()
        '    idseller = reader.GetString(0).ToString()
        '    reader.Close()
        '    '---------------------------------------------
        '    'MsgBox(idseller)


        '    cerrarconexion()

        '    conexionMysql.Open()
        '    Dim Sql25 As String
        '    Sql25 = "update venta set idseller ='" & idseller & "', total='" & rtxttotal.Text & "', fechaentrega='" & fechacalendarioentrega & "', costo_reparacion='" & rtxtcostoreparacion.Text & "', deposito='" & rtxtdeposito.Text & "', resto='" & rtxtresto.Text & "' where idventa='" & rtxtbusquedafolio.Text & "';"
        '    Dim cmd25 As New MySqlCommand(Sql25, conexionMysql)
        '    cmd25.ExecuteNonQuery()
        '    conexionMysql.Close()


        '    MsgBox("Datos actualizados", MsgBoxStyle.Information, "MOBI")
        'End If

        ' Catch ex As Exception

        'End Try


    End Sub

    Function sseleccionpieza()
        txtnombreproducto.Text = lblistaproducto.Text
        lblistaproducto.Visible = False

        'cargamos los datos del producto
        scargardatosproducto()
    End Function

    Private Sub Scbstate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles scbstate.SelectedIndexChanged
        segrilla.Rows.Clear()

        reportestatus()
        restatus = True
        redeliverdate = False
        rechekout = False
        recustomer = False
        rephone = False
        remodelo = False
        reorder = False

        'limpiamos la grilla



    End Sub
    Function reportestatus()
        ssgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        segrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        modificargrilla()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 12)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        'ssgrilla.Columns(1).Width = 10





        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue

        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql As String
            sql = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.status='" & scbstate.Text & "';"
            ' sql = "select * from equipo where status='" & scbstate.Text & "';"
            Dim cmd As New MySqlCommand(sql, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            Dim a, b As Integer
            a = 90
            b = 40
            ssgrilla.Columns(0).Width = 30
            ssgrilla.Columns(1).Width = a + 30
            ssgrilla.Columns(2).Width = a + 20

            ssgrilla.Columns(3).Width = a
            ssgrilla.Columns(4).Width = a
            ssgrilla.Columns(5).Width = a
            ssgrilla.Columns(6).Width = a + 40
            ssgrilla.Columns(7).Width = a + 40
            ssgrilla.Columns(8).Width = a + 40
            ssgrilla.Columns(9).Width = a + 40
            ssgrilla.Columns(10).Width = b
            ssgrilla.Columns(11).Width = b
            ssgrilla.Columns(12).Width = b
            ssgrilla.Columns(13).Width = b
            ssgrilla.Columns(14).Width = b





            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try

        Try
            '-----------------------------------------------------------------------------------
            '----------------------------- se hace actualización a la tabla de productos--------------
            cerrarconexion()
            Dim idseller As Integer
            conexionMysql.Open()
            Dim Sql3 As String
            Sql3 = "select count(*) from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.status='" & scbstate.Text & "';"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            reader = cmd3.ExecuteReader()
            reader.Read()
            Dim contador As Integer = reader.GetString(0).ToString()
            reader.Close()

            'MsgBox(contador)
            '------------------------------------------------------------------

            cerrarconexion()

            Dim i As Integer
            conexionMysql.Open()
            Dim sql4 As String
            Dim a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17 As String
            sql4 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.status='" & scbstate.Text & "';"
            Dim cmd4 As New MySqlCommand(sql4, conexionMysql)
            reader = cmd4.ExecuteReader()
            For i = 0 To contador - 1
                reader.Read()
                '  MsgBox(i)
                a1 = reader.GetString(0).ToString()
                a2 = reader.GetString(1).ToString()
                a3 = reader.GetString(2).ToString()
                a4 = reader.GetString(3).ToString()
                Try

                    a5 = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try

                a6 = reader.GetString(5).ToString()
                a7 = reader.GetString(6).ToString()
                a8 = reader.GetString(7).ToString()
                a9 = reader.GetString(8).ToString()
                a10 = reader.GetString(9).ToString()
                a11 = reader.GetString(10).ToString()
                a12 = reader.GetString(11).ToString()
                a13 = reader.GetString(12).ToString()
                a14 = reader.GetString(13).ToString()
                a15 = reader.GetString(14).ToString()
                a16 = reader.GetString(15).ToString()
                'a17 = reader.GetString(16).ToString()


                segrilla.Rows.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16)


            Next
        Catch ex As Exception
            'MsgBox("Existen problemas con la consulta", MsgBoxStyle.Information, "MOBI")
            cerrarconexion()
        End Try



    End Function
    Function reportetechnical()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120
        modificargrilla()

        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue


        cerrarconexion()
        Dim idseller As Integer
        conexionMysql.Open()
        Dim Sql As String
        Sql = "select idseller from seller where name_seller='" & scbseller.Text & "';"
        Dim cmd As New MySqlCommand(Sql, conexionMysql)
        reader = cmd.ExecuteReader()
        reader.Read()
        idseller = reader.GetString(0).ToString()
        reader.Close()



        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql2 As String
            sql2 = "select * from venta where idseller=" & idseller & ";"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try






        '-----------------------------------------------------------------------------------
        '----------------------------- se hace actualización a la tabla de productos--------------
        cerrarconexion()
        ' Dim idseller As Integer
        conexionMysql.Open()
        Dim Sql3 As String
        Sql3 = "select count(*) from venta where idseller=" & idseller & ";"
        Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
        reader = cmd3.ExecuteReader()
        reader.Read()
        Dim contador As Integer = reader.GetString(0).ToString()
        reader.Close()

        'MsgBox(contador)
        '------------------------------------------------------------------
        '--------agregamos los datos a la grilla 2
        If conexionMysql.State = ConnectionState.Open Then
            conexionMysql.Close()

        End If
        cerrarconexion()

        conexionMysql.Open()
        Dim sql4 As String
        Dim a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17 As String
        sql4 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.idcustomer=" & idseller & ";"
        Dim cmd4 As New MySqlCommand(sql4, conexionMysql)
        reader = cmd4.ExecuteReader()
        For i = 0 To contador - 1
            reader.Read()
            'MsgBox(i)
            a1 = reader.GetString(0).ToString()
            a2 = reader.GetString(1).ToString()
            a3 = reader.GetString(2).ToString()
            a4 = reader.GetString(3).ToString()
            a5 = reader.GetString(4).ToString()
            a6 = reader.GetString(5).ToString()
            a7 = reader.GetString(6).ToString()
            a8 = reader.GetString(7).ToString()
            a9 = reader.GetString(8).ToString()
            a10 = reader.GetString(9).ToString()
            a11 = reader.GetString(10).ToString()
            a12 = reader.GetString(11).ToString()
            a13 = reader.GetString(12).ToString()
            a14 = reader.GetString(13).ToString()
            a15 = reader.GetString(14).ToString()
            a16 = reader.GetString(15).ToString()
            'a17 = reader.GetString(16).ToString()


            segrilla.Rows.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16)


        Next





    End Function

    Private Sub Scbseller_SelectedIndexChanged(sender As Object, e As EventArgs) Handles scbseller.SelectedIndexChanged
        reportetechnical()
    End Sub

    Private Sub Ssdeliverdate_ValueChanged(sender As Object, e As EventArgs) Handles ssdeliverdate.ValueChanged
        reportedeliverdate()
        restatus = False
        redeliverdate = True
        rechekout = False
        recustomer = False
        rephone = False
        remodelo = False
        reorder = False

    End Sub
    Function reportedeliverdate()
        modificargrilla()
        Dim fechacalendarioentrega As String
        fechacalendarioentrega = ssdeliverdate.Value.ToString("yyyy/MM/dd")
        Try
            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()
            conexionMysql.Open()
            Dim sql2 As String
            sql2 = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.fechaentrega='" & fechacalendarioentrega & "';"
            ' sql2 = "select * from venta where fechaentrega='" & fechacalendarioentrega & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            Dim a, b As Integer
            a = 90
            b = 40
            ssgrilla.Columns(0).Width = 30
            ssgrilla.Columns(1).Width = a + 30
            ssgrilla.Columns(2).Width = a + 20

            ssgrilla.Columns(3).Width = a
            ssgrilla.Columns(4).Width = a
            ssgrilla.Columns(5).Width = a
            ssgrilla.Columns(6).Width = a + 40
            ssgrilla.Columns(7).Width = a + 40
            ssgrilla.Columns(8).Width = a + 40
            ssgrilla.Columns(9).Width = a + 40
            ssgrilla.Columns(10).Width = b
            ssgrilla.Columns(11).Width = b
            ssgrilla.Columns(12).Width = b
            ssgrilla.Columns(13).Width = b
            ssgrilla.Columns(14).Width = b
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try





        Try




            '-----------------------------------------------------------------------------------
            '----------------------------- se hace actualización a la tabla de productos--------------
            cerrarconexion()
            Dim idseller As Integer
            conexionMysql.Open()
            Dim Sql3 As String
            Sql3 = "select count(*) from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.datedelivery='" & fechacalendarioentrega & "' and equipo.status='Delivered';"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            reader = cmd3.ExecuteReader()
            reader.Read()
            Dim contador As Integer = reader.GetString(0).ToString()
            reader.Close()

            'MsgBox(contador)
            '------------------------------------------------------------------
            '--------agregamos los datos a la grilla 2
            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()
            '  MsgBox("fechacalendario")
            conexionMysql.Open()
            Dim sql4 As String
            Dim a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17 As String
            sql4 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.datedelivery='" & fechacalendarioentrega & "' and equipo.status='Delivered';"
            Dim cmd4 As New MySqlCommand(sql4, conexionMysql)
            reader = cmd4.ExecuteReader()
            For i = 0 To contador - 1
                reader.Read()
                'MsgBox(i)
                a1 = reader.GetString(0).ToString()
                a2 = reader.GetString(1).ToString()
                a3 = reader.GetString(2).ToString()
                a4 = reader.GetString(3).ToString()
                Try

                    a5 = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                a6 = reader.GetString(5).ToString()
                a7 = reader.GetString(6).ToString()
                a8 = reader.GetString(7).ToString()
                a9 = reader.GetString(8).ToString()
                a10 = reader.GetString(9).ToString()
                a11 = reader.GetString(10).ToString()
                a12 = reader.GetString(11).ToString()
                a13 = reader.GetString(12).ToString()
                a14 = reader.GetString(13).ToString()
                a15 = reader.GetString(14).ToString()
                a16 = reader.GetString(15).ToString()
                'a17 = reader.GetString(16).ToString()


                segrilla.Rows.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16)


            Next


        Catch ex As Exception
            ' MsgBox("Existen problemas con la consulta", MsgBoxStyle.Information, "MOBI")
            cerrarconexion()
        End Try
    End Function
    Function reportedatecheckout()
        modificargrilla()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120


        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        Dim fechacalendarioentrega As String
        fechacalendarioentrega = ssdatecheckout.Value.ToString("yyyy/MM/dd")
        '

        'MsgBox(fechacalendarioentrega)

        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql2 As String
            sql2 = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.fechaventa='" & fechacalendarioentrega & "';"

            'sql2 = "select * from venta where fechaventa='" & fechacalendarioentrega & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            Dim a, b As Integer
            a = 90
            b = 40
            ssgrilla.Columns(0).Width = 30
            ssgrilla.Columns(1).Width = a + 30
            ssgrilla.Columns(2).Width = a + 20

            ssgrilla.Columns(3).Width = a
            ssgrilla.Columns(4).Width = a
            ssgrilla.Columns(5).Width = a
            ssgrilla.Columns(6).Width = a + 40
            ssgrilla.Columns(7).Width = a + 40
            ssgrilla.Columns(8).Width = a + 40
            ssgrilla.Columns(9).Width = a + 40
            ssgrilla.Columns(10).Width = b
            ssgrilla.Columns(11).Width = b
            ssgrilla.Columns(12).Width = b
            ssgrilla.Columns(13).Width = b
            ssgrilla.Columns(14).Width = b
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try






        Try

            '-----------------------------------------------------------------------------------
            '----------------------------- se hace actualización a la tabla de productos--------------
            cerrarconexion()
            Dim idseller As Integer
            conexionMysql.Open()
            Dim Sql3 As String
            Sql3 = "select count(*) from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.fechaventa='" & fechacalendarioentrega & "'"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            reader = cmd3.ExecuteReader()
            reader.Read()
            Dim contador As Integer = reader.GetString(0).ToString()
            reader.Close()

            'MsgBox(contador)
            '------------------------------------------------------------------
            '--------agregamos los datos a la grilla 2
            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql4 As String
            Dim a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17 As String
            sql4 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.fechaventa='" & fechacalendarioentrega & "';"
            Dim cmd4 As New MySqlCommand(sql4, conexionMysql)
            reader = cmd4.ExecuteReader()
            For i = 0 To contador - 1
                reader.Read()
                'MsgBox(i)
                a1 = reader.GetString(0).ToString()
                a2 = reader.GetString(1).ToString()
                a3 = reader.GetString(2).ToString()
                a4 = reader.GetString(3).ToString()
                Try

                    a5 = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                a6 = reader.GetString(5).ToString()
                a7 = reader.GetString(6).ToString()
                a8 = reader.GetString(7).ToString()
                a9 = reader.GetString(8).ToString()
                a10 = reader.GetString(9).ToString()
                a11 = reader.GetString(10).ToString()
                a12 = reader.GetString(11).ToString()
                a13 = reader.GetString(12).ToString()
                a14 = reader.GetString(13).ToString()
                a15 = reader.GetString(14).ToString()
                a16 = reader.GetString(15).ToString()
                'a17 = reader.GetString(16).ToString()


                segrilla.Rows.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16)


            Next



        Catch ex As Exception
            '   MsgBox("Existen problemas con la consulta", MsgBoxStyle.Information, "MOBI")
            cerrarconexion()
        End Try

    End Function
    Function modificargrilla()
        'ssgrilla.Rows.Clear()
        ssgrilla.DefaultCellStyle.Font = New Font("Arial", 12)
        ssgrilla.RowHeadersVisible = False
        segrilla.DefaultCellStyle.Font = New Font("Arial", 12)
        segrilla.RowHeadersVisible = False
        'ampliar columna 
        'ssgrilla.Columns(0).Width = 20
        'ssgrilla.Columns(2).Width = 50

        'ssgrilla.Columns(3).Width = 50
        'ssgrilla.Columns(4).Width = 50
        'ssgrilla.Columns(5).Width = 50
        'ssgrilla.Columns(6).Width = 50
        'ssgrilla.Columns(7).Width = 50
        'ssgrilla.Columns(7).Width = 50
        'ssgrilla.Columns(8).Width = 50

        'ssgrilla.Columns(10).Width = 50
        'ssgrilla.Columns(11).Width = 50

        'ssgrilla.Columns(3).Width = 500


        ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        segrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        segrilla.Rows.Clear()

    End Function
    Function reportecustomername()
        modificargrilla()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120


        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        Dim fechacalendarioentrega As String
        fechacalendarioentrega = ssdatecheckout.Value.ToString("yyyy/MM/dd")
        '
        Dim idcliente As Integer
        Try

            'MsgBox(fechacalendarioentrega)
            cerrarconexion()
            conexionMysql.Open()

            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31 = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.name like '%" & sscustomername.Text & "%';"
            '            Sql31 = "select idcustomer from customer where name like '%" & sscustomername.Text & "%';"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            idcliente = reader.GetString(0).ToString()
            conexionMysql.Close()

        Catch ex As Exception

        End Try


        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql2 As String
            'sql2 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.name like '%" & sscustomername.Text & "%';"
            sql2 = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.name like '%" & sscustomername.Text & "%';"

            'sql2 = "select * from venta,customer where venta.idcustomer=customer.idcustomer and venta.idcustomer='" & idcliente & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            Dim a, b As Integer
            a = 90
            b = 40
            ssgrilla.Columns(0).Width = 30
            ssgrilla.Columns(1).Width = a + 30
            ssgrilla.Columns(2).Width = a + 20

            ssgrilla.Columns(3).Width = a
            ssgrilla.Columns(4).Width = a
            ssgrilla.Columns(5).Width = a
            ssgrilla.Columns(6).Width = a + 40
            ssgrilla.Columns(7).Width = a + 40
            ssgrilla.Columns(8).Width = a + 40
            ssgrilla.Columns(9).Width = a + 40
            ssgrilla.Columns(10).Width = b
            ssgrilla.Columns(11).Width = b
            ssgrilla.Columns(12).Width = b
            ssgrilla.Columns(13).Width = b
            ssgrilla.Columns(14).Width = b
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try



        Try

            '-----------------------------------------------------------------------------------
            '----------------------------- se hace actualización a la tabla de productos--------------
            cerrarconexion()
            Dim idseller As Integer
            conexionMysql.Open()
            Dim Sql3 As String
            Sql3 = "select count(*) from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.name like '%" & sscustomername.Text & "%';"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            reader = cmd3.ExecuteReader()
            reader.Read()
            Dim contador As Integer = reader.GetString(0).ToString()
            reader.Close()

            'MsgBox(contador)
            '------------------------------------------------------------------
            '--------agregamos los datos a la grilla 2
            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()
            '  MsgBox("fechacalendario")
            conexionMysql.Open()
            Dim sql4 As String
            Dim a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17 As String
            sql4 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.name like '%" & sscustomername.Text & "%';"
            Dim cmd4 As New MySqlCommand(sql4, conexionMysql)
            reader = cmd4.ExecuteReader()
            For i = 0 To contador - 1
                reader.Read()
                'MsgBox(i)
                a1 = reader.GetString(0).ToString()
                a2 = reader.GetString(1).ToString()
                a3 = reader.GetString(2).ToString()
                a4 = reader.GetString(3).ToString()
                Try

                    a5 = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                a6 = reader.GetString(5).ToString()
                a7 = reader.GetString(6).ToString()
                a8 = reader.GetString(7).ToString()
                a9 = reader.GetString(8).ToString()
                a10 = reader.GetString(9).ToString()
                a11 = reader.GetString(10).ToString()
                a12 = reader.GetString(11).ToString()
                a13 = reader.GetString(12).ToString()
                a14 = reader.GetString(13).ToString()
                a15 = reader.GetString(14).ToString()
                a16 = reader.GetString(15).ToString()
                'a17 = reader.GetString(16).ToString()


                segrilla.Rows.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16)


            Next


        Catch ex As Exception
            cerrarconexion()
        End Try




    End Function
    Private Sub Ssdatecheckout_ValueChanged(sender As Object, e As EventArgs) Handles ssdatecheckout.ValueChanged
        reportedatecheckout()
        restatus = False
        redeliverdate = False
        rechekout = True
        recustomer = False
        rephone = False
        remodelo = False
        reorder = False

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles sscustomername.TextChanged
        reportecustomername()
        restatus = False
        redeliverdate = False
        rechekout = False
        recustomer = True
        rephone = False
        remodelo = False
        reorder = False

    End Sub

    Function scargardatosproducto()
        conexionMysql.Open()
        Dim Sql As String
        Sql = "select * from producto where name='" & txtnombreproducto.Text & "';"
        Dim cmd As New MySqlCommand(Sql, conexionMysql)
        reader = cmd.ExecuteReader()
        reader.Read()
        txtprice.Text = reader.GetString(4).ToString()
        txtnombreproducto.Text = reader.GetString(1).ToString()
        ' rtxtcantidad.Text = reader.GetString(2).ToString()
        txtclaveproducto.Text = reader.GetString(0).ToString()
        'ptxtcosto.Text = reader.GetString(3).ToString()

        'txtpreciomayoreop.Text = reader.GetString(5).ToString()

        reader.Close()
    End Function

    Private Sub Ssitemname_TextChanged(sender As Object, e As EventArgs) Handles stxtphonenumber.TextChanged
        'reporteitemname()
        reportephonenumber()
        restatus = False
        redeliverdate = False
        rechekout = False
        recustomer = False
        rephone = True
        remodelo = False
        reorder = False

    End Sub
    Function reportephonenumber()
        modificargrilla()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120


        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        Dim fechacalendarioentrega As String
        fechacalendarioentrega = ssdatecheckout.Value.ToString("yyyy/MM/dd")
        '
        Dim idcliente As Integer
        Try

            'MsgBox(fechacalendarioentrega)
            cerrarconexion()
            conexionMysql.Open()

            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            'Sql31 = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.telephone like '%" & stxtphonenumber.Text & "%';"
            '            Sql31 = "select idcustomer from customer where name like '%" & sscustomername.Text & "%';"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            idcliente = reader.GetString(0).ToString()
            conexionMysql.Close()

        Catch ex As Exception

        End Try


        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql2 As String
            'sql2 = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.telephone like '%" & stxtphonenumber.Text & "%';"
            sql2 = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.telephone like '%" & stxtphonenumber.Text & "%';"

            'sql2 = "select * from venta,customer where venta.idcustomer=customer.idcustomer and venta.idcustomer='" & idcliente & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            Dim a, b As Integer
            a = 90
            b = 40
            ssgrilla.Columns(0).Width = 30
            ssgrilla.Columns(1).Width = a + 30
            ssgrilla.Columns(2).Width = a + 20

            ssgrilla.Columns(3).Width = a
            ssgrilla.Columns(4).Width = a
            ssgrilla.Columns(5).Width = a
            ssgrilla.Columns(6).Width = a + 40
            ssgrilla.Columns(7).Width = a + 40
            ssgrilla.Columns(8).Width = a + 40
            ssgrilla.Columns(9).Width = a + 40
            ssgrilla.Columns(10).Width = b
            ssgrilla.Columns(11).Width = b
            ssgrilla.Columns(12).Width = b
            ssgrilla.Columns(13).Width = b
            ssgrilla.Columns(14).Width = b
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try


        Try


            '-----------------------------------------------------------------------------------
            '----------------------------- se hace actualización a la tabla de productos--------------
            cerrarconexion()
            Dim idseller As Integer
            conexionMysql.Open()
            Dim Sql3 As String
            Sql3 = "select count(*) from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.telephone like '%" & stxtphonenumber.Text & "%';"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            reader = cmd3.ExecuteReader()
            reader.Read()
            Dim contador As Integer = reader.GetString(0).ToString()
            reader.Close()

            'MsgBox(contador)
            '------------------------------------------------------------------
            '--------agregamos los datos a la grilla 2
            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()
            '  MsgBox("fechacalendario")
            conexionMysql.Open()
            Dim sql4 As String
            Dim a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17 As String
            sql4 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and customer.telephone like '%" & stxtphonenumber.Text & "%';"
            Dim cmd4 As New MySqlCommand(sql4, conexionMysql)
            reader = cmd4.ExecuteReader()
            For i = 0 To contador - 1
                reader.Read()
                'MsgBox(i)
                a1 = reader.GetString(0).ToString()
                a2 = reader.GetString(1).ToString()
                a3 = reader.GetString(2).ToString()
                a4 = reader.GetString(3).ToString()
                Try

                    a5 = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                a6 = reader.GetString(5).ToString()
                a7 = reader.GetString(6).ToString()
                a8 = reader.GetString(7).ToString()
                a9 = reader.GetString(8).ToString()
                a10 = reader.GetString(9).ToString()
                a11 = reader.GetString(10).ToString()
                a12 = reader.GetString(11).ToString()
                a13 = reader.GetString(12).ToString()
                a14 = reader.GetString(13).ToString()
                a15 = reader.GetString(14).ToString()
                a16 = reader.GetString(15).ToString()
                'a17 = reader.GetString(16).ToString()


                segrilla.Rows.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16)


            Next






        Catch ex As Exception
            ' MsgBox("Existen problemas con la consulta", MsgBoxStyle.Information, "MOBI")
            cerrarconexion()
        End Try


    End Function

    Private Sub rtxtbusquedafolio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtxtbusquedafolio.KeyPress



    End Sub

    Private Sub TextBox3_TextChanged_1(sender As Object, e As EventArgs) Handles stxtmodel.TextChanged
        'reportemodel()
        reportemodelactualizado()
        restatus = False
        redeliverdate = False
        rechekout = False
        recustomer = False
        rephone = False
        remodelo = True
        reorder = False

    End Sub
    Function reportemodelactualizado()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120
        modificargrilla()

        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue

        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql As String
            'sql = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.modelo like '%" & stxtmodel.Text & "%';"
            sql = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.modelo like '%" & stxtmodel.Text & "%';"

            ' sql = "select * from equipo where status='" & scbstate.Text & "';"
            Dim cmd As New MySqlCommand(sql, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            Dim a, b As Integer
            a = 90
            b = 40
            ssgrilla.Columns(0).Width = 30
            ssgrilla.Columns(1).Width = a + 30
            ssgrilla.Columns(2).Width = a + 20

            ssgrilla.Columns(3).Width = a
            ssgrilla.Columns(4).Width = a
            ssgrilla.Columns(5).Width = a
            ssgrilla.Columns(6).Width = a + 40
            ssgrilla.Columns(7).Width = a + 40
            ssgrilla.Columns(8).Width = a + 40
            ssgrilla.Columns(9).Width = a + 40
            ssgrilla.Columns(10).Width = b
            ssgrilla.Columns(11).Width = b
            ssgrilla.Columns(12).Width = b
            ssgrilla.Columns(13).Width = b
            ssgrilla.Columns(14).Width = b
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try



        Try

            '-----------------------------------------------------------------------------------
            '----------------------------- se hace actualización a la tabla de productos--------------
            cerrarconexion()
            Dim idseller As Integer
            conexionMysql.Open()
            Dim Sql3 As String
            Sql3 = "select count(*) from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.modelo like '%" & stxtmodel.Text & "%';"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            reader = cmd3.ExecuteReader()
            reader.Read()
            Dim contador As Integer = reader.GetString(0).ToString()
            reader.Close()

            'MsgBox(contador)
            '------------------------------------------------------------------
            '--------agregamos los datos a la grilla 2
            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()
            '  MsgBox("fechacalendario")
            conexionMysql.Open()
            Dim sql4 As String
            Dim a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17 As String
            sql4 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and equipo.modelo like '%" & stxtmodel.Text & "%';"
            Dim cmd4 As New MySqlCommand(sql4, conexionMysql)
            reader = cmd4.ExecuteReader()
            For i = 0 To contador - 1
                reader.Read()
                'MsgBox(i)
                a1 = reader.GetString(0).ToString()
                a2 = reader.GetString(1).ToString()
                a3 = reader.GetString(2).ToString()
                a4 = reader.GetString(3).ToString()
                Try

                    a5 = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                a6 = reader.GetString(5).ToString()
                a7 = reader.GetString(6).ToString()
                a8 = reader.GetString(7).ToString()
                a9 = reader.GetString(8).ToString()
                a10 = reader.GetString(9).ToString()
                a11 = reader.GetString(10).ToString()
                a12 = reader.GetString(11).ToString()
                a13 = reader.GetString(12).ToString()
                a14 = reader.GetString(13).ToString()
                a15 = reader.GetString(14).ToString()
                a16 = reader.GetString(15).ToString()
                'a17 = reader.GetString(16).ToString()


                segrilla.Rows.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16)


            Next

        Catch ex As Exception
            'MsgBox("Existen problemas con la consulta", MsgBoxStyle.Information, "MOBI")
            cerrarconexion()
        End Try


    End Function
    Private Sub rtxtbusquedafolio_KeyDown(sender As Object, e As KeyEventArgs) Handles rtxtbusquedafolio.KeyDown
        If e.KeyCode = Keys.Escape Then
            rtxtbusquedafolio.Visible = False
            rcbitems.Visible = False
            rcbitemtemporal.Visible = True
            rcbitemtemporal.Items.Clear()
            rcbitemtemporal.Items.Clear()



        End If
        If e.KeyCode = Keys.Enter Then
            realizarbusqueda()
        End If
    End Sub

    Private Sub TextBox4_TextChanged_1(sender As Object, e As EventArgs) Handles stxtorder.TextChanged
        'reporteimei()
        reporteorder()
        restatus = False
        redeliverdate = False
        rechekout = False
        recustomer = False
        rephone = False
        remodelo = False
        reorder = True

    End Sub
    Function reporteorder()
        modificargrilla()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120


        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue

        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql As String
            'sql = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.idventa='" & stxtorder.Text & "';"
            sql = "select venta.idventa,equipo.equipo,equipo.modelo,venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.idventa='" & stxtorder.Text & "';"

            ' sql = "select * from equipo where status='" & scbstate.Text & "';"
            Dim cmd As New MySqlCommand(sql, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            Dim a, b As Integer
            a = 90
            b = 40
            ssgrilla.Columns(0).Width = 30
            ssgrilla.Columns(1).Width = a + 30
            ssgrilla.Columns(2).Width = a + 20

            ssgrilla.Columns(3).Width = a
            ssgrilla.Columns(4).Width = a
            ssgrilla.Columns(5).Width = a
            ssgrilla.Columns(6).Width = a + 40
            ssgrilla.Columns(7).Width = a + 40
            ssgrilla.Columns(8).Width = a + 40
            ssgrilla.Columns(9).Width = a + 40
            ssgrilla.Columns(10).Width = b
            ssgrilla.Columns(11).Width = b
            ssgrilla.Columns(12).Width = b
            ssgrilla.Columns(13).Width = b
            ssgrilla.Columns(14).Width = b
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try





        Try


            '-----------------------------------------------------------------------------------
            '----------------------------- se hace actualización a la tabla de productos--------------
            cerrarconexion()
            Dim idseller As Integer
            conexionMysql.Open()
            Dim Sql3 As String
            Sql3 = "select count(*) from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.idventa='" & stxtorder.Text & "';"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            reader = cmd3.ExecuteReader()
            reader.Read()
            Dim contador As Integer = reader.GetString(0).ToString()
            reader.Close()

            'MsgBox(contador)
            '------------------------------------------------------------------
            '--------agregamos los datos a la grilla 2
            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()
            '  MsgBox("fechacalendario")
            conexionMysql.Open()
            Dim sql4 As String
            Dim a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17 As String
            sql4 = "select venta.idventa,equipo.idequipo,equipo.equipo,equipo.modelo,equipo.datedelivery,venta.fechaventa,equipo.status,customer.name,customer.telephone,equipo.problema,equipo.nota,equipo.reparacion,equipo.partes, venta.deposito,venta.resto,venta.total from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer and venta.idventa='" & stxtorder.Text & "';"
            Dim cmd4 As New MySqlCommand(sql4, conexionMysql)
            reader = cmd4.ExecuteReader()
            For i = 0 To contador - 1
                reader.Read()
                'MsgBox(i)
                a1 = reader.GetString(0).ToString()
                a2 = reader.GetString(1).ToString()
                a3 = reader.GetString(2).ToString()
                a4 = reader.GetString(3).ToString()
                Try

                    a5 = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                a6 = reader.GetString(5).ToString()
                a7 = reader.GetString(6).ToString()
                a8 = reader.GetString(7).ToString()
                a9 = reader.GetString(8).ToString()
                a10 = reader.GetString(9).ToString()
                a11 = reader.GetString(10).ToString()
                a12 = reader.GetString(11).ToString()
                a13 = reader.GetString(12).ToString()
                a14 = reader.GetString(13).ToString()
                a15 = reader.GetString(14).ToString()
                a16 = reader.GetString(15).ToString()
                'a17 = reader.GetString(16).ToString()


                segrilla.Rows.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16)


            Next




        Catch ex As Exception
            '  MsgBox("Existen problemas con la consulta", MsgBoxStyle.Information, "MOBI")
            cerrarconexion()
        End Try


    End Function
    Function reporteorderinicial()

        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120


        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue

        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql As String
            sql = "select venta.fechaentrega,venta.fechaventa,equipo.status,customer.name,customer.telephone,venta.idventa,equipo.equipo,equipo.modelo,equipo.problema,equipo.nota,venta.deposito,venta.resto,venta.total,equipo.reparacion,equipo.partes from venta,equipo,customer  where venta.idventa=equipo.idventa and venta.idcustomer=customer.idcustomer;"
            ' sql = "select * from equipo where status='" & scbstate.Text & "';"
            Dim cmd As New MySqlCommand(sql, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60
            modificargrilla()
            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try
    End Function

    Public Sub rrsumatorio()
        'Try

        Dim i As Integer = rgrilla2.RowCount


        'MsgBox(i)
        Dim j As Integer
        Dim sumacon, sumasin, cantidad_productos, sumagananciatotal As Double
        Dim a, b, c, d As String
        'suma de valores
        For j = 0 To i - 2
            'MsgBox("valosr de j:" & j)
            'a = venta.grillaventa.Item(j, 3).Value.ToString()
            a = rgrilla2.Rows(j).Cells(9).Value 'con cotizador
            'c = sgrilla.Rows(j).Cells(9).Value 'sin cotizador
            'd = sgrilla.Rows(j).Cells(7).Value 'totalganancia

            'b = sgrilla.Rows(j).Cells(3).Value
            'cantidad_productos = cantidad_productos + b
            ' sumacon = sumacon + a
            'sumasin = sumasin + c
            Try

                sumagananciatotal = sumagananciatotal + a
            Catch ex As Exception

            End Try

            'MsgBox(a)
        Next
        Try
            If rtxtcostoreparacion.Text = "" Then
                'rtxtcostoreparacion.Text = "0"

            End If
            rtxttotal.Text = CDbl(sumagananciatotal)

            'rtxttotal.Text = CDbl(sumagananciatotal) + CDbl(rtxtcostoreparacion.Text)
        Catch ex As Exception

        End Try

        'stxtpagarsin.Text = sumasin
        'stxttotal.Text = sumasin
        'stxtgananciatotalproductos.Text = sumagananciatotal

        ''el valor siguiente es como funcionaba anteriormente
        ''        stxttotal.Text = suma
        'stxttotalproductos.Text = cantidad_productos
    End Sub
    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click


        If rtxtequipo.Text = "" Or rtxtmodelo.Text = "" Or rtxtcostoreparacion.Text = "" Then
            MsgBox("Falta información por agregar", MsgBoxStyle.Information, "MOBI")

            If rtxtequipo.Text = "" Then
                rtxtequipo.BackColor = Color.FromArgb(247, 220, 111)
            End If
            If rtxtmodelo.Text = "" Then
                rtxtmodelo.BackColor = Color.FromArgb(247, 220, 111)
            End If

            If rtxtcostoreparacion.Text = "" Then
                rtxtcostoreparacion.BackColor = Color.FromArgb(247, 220, 111)
            End If

        Else
            rchpay.Checked = False
            '----------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------
            'OBTENEMOS UN NUEVO FOLIO PARA LA BUSQUEDA EN CASO DE QUE SE NECESITE
            obtenerfolio()
            '----------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------
            '----------------------------------------------------------------------------------------------------------------------

            'buscamos dentro de rgrilla2 si ya existe un item con el mismo nombre
            Dim respuestarepetido As Boolean
            respuestarepetido = True
            Dim itemnombre As String
            Dim ii As Integer
            Dim j As Integer = rgrilla2.RowCount
            ' MsgBox(j)

            'If j <= 1 Then

            'Else

            '    For ii = 0 To j - 1
            '        itemnombre = rgrilla2.Rows(ii).Cells(1).Value 'descripcion

            '        ' MsgBox(itemnombre)
            '        'MsgBox(rtxtequipo.Text)

            '        If itemnombre = rtxtequipo.Text Then
            '            MsgBox("Ya existe un equipo con el mismo nombre", MsgBoxStyle.Exclamation, "MOBI")
            '            ii = j + 1
            'respuestarepetido = False
            '        Else

            '        End If

            '    Next

            'End If

            If respuestarepetido = True Then
                Dim idequipo As String
                almacenarequipo()

                'consulto el equipo maximo insertado en este momento, que se supone debe de ser el que continua
                '------------------------------------------
                conexionMysql.Open()
                Dim Sql As String
                Sql = "select max(idequipo) from equipo"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader()
                reader.Read()
                idequipo = reader.GetString(0).ToString()

                reader.Close()
                conexionMysql.Close()
                cerrarconexion()

                '--------------------------------------------

                'rcbitems.Items.Add(rtxtequipo.Text)

                Dim dia, mes, año, fecha, fechaclave, claveproducto As String
                hora2 = Now.Hour()
                minuto = Now.Minute()
                segundo = Now.Second()

                hora = hora2 & ":" & minuto & ":" & segundo
                dia = Date.Now.Day
                mes = Date.Now.Month
                año = Date.Now.Year
                fecha = año & "-" & mes & "-" & dia
                fechaclave = año & mes & dia & hora2 & minuto & segundo
                Dim i As Integer = rgrilla2.RowCount
                Dim gananciatotal As Double
                i = i
                'txtcostoconproducto.Text = Math.Round(f, 2)
                'gananciatotal = CDbl(rtxtcantidad.Text) * CDbl(rtxtprecio.Text)
                'gananciatotal = Math.Round(gananciatotal, 2)
                rgrilla2.Rows.Add(idequipo, rtxtequipo.Text, rtxtmodelo.Text, rtxtimei.Text, rtxtpassword.Text, rtxtstate.Text, rcbseller.Text, rtxtproblem.Text, rtxtnote.Text, rtxttotaltemporal.Text)
                ' sgrilla.Columns(1).Width = 350

                'MsgBox("agregado")

                'rsumatorio()
                '        rrguardarproductostemporal()
                rcbitemtemporal.Items.Add(rtxtequipo.Text)
                '-------------------------------------------
                'obtenemos el folio

                Dim indicecomboitem As Integer
                indicecomboitem = rcbitemtemporal.Items.Count

                'MsgBox("hay en el combo:" & indicecomboitem)
                indicecomboitem = indicecomboitem
                '-------------------------------------------
                cb1.Items.Add(idequipo)
                activarreparacion = True


                rrlimpiar()

                rrsumatorio()





                'rtxtclaveproducto.Focus()

                'AGREGAMOS TODOS LSO VALORES QUE TENEMOS EN LA GRILLA A LA BD
            End If


        End If

    End Sub
    Function almacenarequipo()

        Dim idcliente As Integer

        idcliente = 1

        Try
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31 = "select idcustomer from customer where name like '%" & rtxtcustomername.Text & "%';"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            idcliente = reader.GetString(0).ToString()
            conexionMysql.Close()
            'en caso de que lo obtenga, obtenemos el ID, en caso contrario guardamos el dato
            'MsgBox(idcliente)
        Catch ex As Exception
            ' idcliente = 1
            cerrarconexion()
            'Try

            ' MsgBox(rtxtemail.Text)
            'en caso de que no este el cliente, lo guardamos
            conexionMysql.Open()
            Dim Sqlx1 As String
            Sqlx1 = "INSERT INTO `customer` (`name`, `address`, `state`, `city`, `telephone`,`email`) VALUES ( '" & rtxtcustomername.Text & "', '" & rtxtaddress.Text & "', '" & rtxtstate.Text & "', '" & rtxtcity.Text & "', '" & rtxttelephone.Text & "','" & rtxtemail.Text & "');"
            Dim cmdx1 As New MySqlCommand(Sqlx1, conexionMysql)
            cmdx1.ExecuteNonQuery()
            conexionMysql.Close()
            'txttotalpagar.Text = ""
            conexionMysql.Close()

            '------------------------obtenemos el id del cliente
            cerrarconexion()
            conexionMysql.Open()
            Dim Sqlx2 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sqlx2 = "select idcustomer from customer where name like '%" & rtxtcustomername.Text & "%';"
            Dim cmdx2 As New MySqlCommand(Sqlx2, conexionMysql)
            reader = cmdx2.ExecuteReader()
            reader.Read()
            idcliente = reader.GetString(0).ToString()
            conexionMysql.Close()

            MsgBox("Customer insertado", MsgBoxStyle.Information, "MOBI")

            'Catch ex As Exception
            'cerrarconexion()

            'End Try

            '-MsgBox(idcliente)
        End Try

        Dim folio As Integer
        If activarbusqueda = True Then
            folio = rtxtbusquedafolio.Text
        Else
            folio = slbfolio.Text
        End If

        '-------------------------------------------
        'obtenemos el folio

        Dim indicecomboitem As Integer
        indicecomboitem = rcbitemtemporal.Items.Count

        'MsgBox("hay en el combo:" & indicecomboitem)
        indicecomboitem = indicecomboitem
        '-------------------------------------------
        If rtxtpriceparts.Text = "" Then
            rtxtpriceparts.Text = "0"
        End If

        '-------cargamos los calculos para sacar el deposito y resto
        If rtxtdeposito.Text = "" Then
            'rtxtcostoreparacion.Text = "0"
            rtxtresto.Text = rtxtcostoreparacion.Text
            rtxtdeposito.Text = "0"
        End If


        '----------------------------------------------




        cerrarconexion()
        conexionMysql.Open()
        Dim Sql2 As String
        Sql2 = "INSERT INTO `equipo` (`equipo`, `modelo`, `imei`, `password`, `status`, `problema`, `nota`, `idcustomer`,`reparacion`,`partes`,`idventa`,`posicion`) VALUES ('" & rtxtequipo.Text & "', '" & rtxtmodelo.Text & "', '" & rtxtimei.Text & "', '" & rtxtpassword.Text & "', '" & cbstate.Text & "', '" & rtxtproblem.Text & "', '" & rtxtnote.Text & "', '" & idcliente & "','" & rtxtcostoreparacion.Text & "','" & rtxtpriceparts.Text & "','" & folio & "','" & indicecomboitem & "');"
        Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
        cmd2.ExecuteNonQuery()
        conexionMysql.Close()
        'txttotalpagar.Text = ""
        conexionMysql.Close()

    End Function
    Function rrguardarproductostemporal()
        '------------------ insertar reginstro en tabla ventaIND ---------------------------------------
        Dim i As Integer = rgrilla.RowCount
        Dim j As Integer
        Dim actividad As String
        Dim cantidad, precio, total As Double
        Dim producto, observaciones, clave As String

        ' conexionMysql.Open()

        'suma de valores
        For j = 0 To i - 2
            'MsgBox("valosr de j:" & j)
            'a = venta.grillaventa.Item(j, 3).Value.ToString()
            clave = rgrilla.Rows(j).Cells(0).Value 'descripcion
            producto = rgrilla.Rows(j).Cells(1).Value 'cantidad
            precio = rgrilla.Rows(j).Cells(2).Value 'costo
            cantidad = rgrilla.Rows(j).Cells(3).Value
            total = rgrilla.Rows(j).Cells(4).Value
            cerrarconexion()

            'MsgBox("el producto es:" & producto)
            conexionMysql.Open()
            Dim Sql25 As String
            Sql25 = "INSERT INTO venta_ind (actividad, cantidad, costo, idventa,idproducto,name_item) VALUES ('" & producto & "'," & cantidad & "," & precio & "," & lbfolio.Text & ",'" & clave & "','" & rtxtequipo.Text & "');"
            Dim cmd25 As New MySqlCommand(Sql25, conexionMysql)
            cmd25.ExecuteNonQuery()
            conexionMysql.Close()
        Next

        rgrilla.Rows.Clear()

    End Function

    Function rrlimpiar()
        rtxtequipo.Text = ""
        rtxtmodelo.Text = ""
        rtxtimei.Text = ""
        rtxtpassword.Text = ""
        rtxtstate.Text = ""
        rtxtproblem.Text = ""
        rtxtnote.Text = ""
        rtxtcostoreparacion.Text = ""
        rgrilla.Rows.Clear()
        rtxtpriceparts.Text = ""
        rtxtdeposito.Text = ""

    End Function
    Function habilitarcajasdevice()
        rtxtequipo.Enabled = True
        rtxtmodelo.Enabled = True
        rtxtimei.Enabled = True
        rtxtpassword.Enabled = True
        rtxtstate.Enabled = True
        rtxtproblem.Enabled = True
        rtxtnote.Enabled = True
        rtxtclaveproducto.Enabled = True
        rtxtprecio.Enabled = True
        rtxtcantidad.Enabled = True
        Button17.Enabled = True
        rtxtnombre.Enabled = True
        rtxtcostoreparacion.Enabled = True
        Button29.Enabled = True


    End Function
    Function deshabilitarcajasdevice()
        rtxtequipo.Enabled = False
        rtxtmodelo.Enabled = False
        rtxtimei.Enabled = False
        rtxtpassword.Enabled = False
        rtxtstate.Enabled = False
        rtxtproblem.Enabled = False
        rtxtnote.Enabled = False
        rtxtclaveproducto.Enabled = False
        rtxtprecio.Enabled = False
        rtxtcantidad.Enabled = False
        Button17.Enabled = False
        rtxtnombre.Enabled = False
        rtxtcostoreparacion.Enabled = False
        Button29.Enabled = False


    End Function
    Function cargardatosgrilla2()
        Dim Variable As String = rgrilla2.Item(1, rgrilla2.CurrentRow.Index).Value
        'MsgBox(Variable)




        'LIMPIAMOS LA GRILLA
        rgrilla.Rows.Clear()
        deshabilitarcajasdevice()
        'rtxtequipo.Enabled = False


        cerrarconexion()
        conexionMysql.Open()
        Dim contador As Integer
        Dim Sql2 As String
        Sql2 = "select count(*) as contador from venta_ind where idventa='" & slbfolio.Text & "' and name_item='" & Variable & "';"
        Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
        reader = cmd2.ExecuteReader()
        reader.Read()
        contador = reader.GetString(0).ToString()
        reader.Close()
        conexionMysql.Close()
        cerrarconexion()
        '---------------------------------------------
        If contador > 0 Then


            '-------------------------------------- buscamos los valores de la grilla y lo mandamos a eso


            cerrarconexion()
            conexionMysql.Open()
            Dim clave, costo, cantidad, actividad As String
            Dim Sql3 As String
            Sql3 = "select * from venta_ind where idventa='" & slbfolio.Text & "' and name_item='" & Variable & "';"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            reader = cmd3.ExecuteReader()





            For i = 0 To contador - 1

                reader.Read()


                clave = reader.GetString(5).ToString()
                actividad = reader.GetString(1).ToString()
                cantidad = reader.GetString(3).ToString()
                costo = reader.GetString(2).ToString()


                rgrilla.Rows.Add(clave, actividad, cantidad, costo, CDbl(cantidad) * CDbl(costo))


            Next




            reader.Close()
            conexionMysql.Close()
            cerrarconexion()

            '-------------cargo los datos del equipo

            conexionMysql.Open()
            'Dim clave, costo, cantidad, actividad As String
            Dim Sql34 As String
            Sql34 = "select * from equipo where idventa='" & slbfolio.Text & "' and equipo='" & Variable & "';"
            Dim cmd34 As New MySqlCommand(Sql34, conexionMysql)
            reader = cmd34.ExecuteReader()
            reader.Read()
            rtxtequipo.Text = reader.GetString(1).ToString()
            rtxtmodelo.Text = reader.GetString(2).ToString()
            rtxtimei.Text = reader.GetString(3).ToString()
            rtxtpassword.Text = reader.GetString(4).ToString()
            rtxtstate.Text = reader.GetString(5).ToString()
            rtxtproblem.Text = reader.GetString(6).ToString()
            rtxtnote.Text = reader.GetString(7).ToString()


            reader.Close()


            '---------------------------------------------
        End If

    End Function
    Private Sub Rgrilla2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles rgrilla2.CellContentClick
        'AQUI CARGAMOS LOS DATOS DE LA LISTA




    End Sub

    Private Sub txtnombreproducto_LostFocus(sender As Object, e As EventArgs) Handles txtnombreproducto.LostFocus
        'lblistaproducto.Visible = False

    End Sub

    Private Sub rtxtcustomername_LostFocus(sender As Object, e As EventArgs) Handles rtxtcustomername.LostFocus
        ' rlbcustomer.Visible = False
    End Sub

    Private Sub rtxtnombre_LostFocus(sender As Object, e As EventArgs) Handles rtxtnombre.LostFocus
        'lblistarespuestos.Visible = False

    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        'habilitarcajasdevice()
        rgrilla.Rows.Clear()
        'rrlimpiar()
        rtxtnombre.Text = ""
        rtxtequipo.Text = ""
        rtxtdeposito.Text = ""
        'rtxtdeposito.Text = ""
        rtxtimei.Text = ""
        rtxtnote.Text = ""
        rtxtprecio.Text = ""
        rtxtproblem.Text = ""
        'rtxtcity.Text = ""
        'rtxttelephone.Text = ""
        'rtxtaddress.Text = ""
        rtxtmodelo.Text = ""
        rtxtpassword.Text = ""
        'rtxttotal.Text = ""
        rtxtcostoreparacion.Text = ""
        'rtxtresto.Text = ""
        'cbstate.SelectedIndex = 0
        'rcbseller.SelectedIndex = 0
        'rtxtemail.Text = ""
        'rgrilla.Rows.Clear()
        'rgrilla2.Rows.Clear()
        rtxttotaltemporal.Text = ""
        rtxtpriceparts.Text = ""
        'limpiamos las cajas
        'activarbusqueda = False
        'rtxtbusquedafolio.Visible = False
        'rlimpiartodo()
        'habilitarcajasdevice()
        'comprobarventarealizada()
        'rcbitems.Visible = False
        'rcbitemtemporal.Visible = True
        'rcbitemtemporal.Items.Clear()
        'rcbitems.Items.Clear()
        'activarreparacion = False
        'activarbusqueda = False
        rtxtclaveproducto.Text = ""

    End Sub

    Private Sub Rlbcustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rlbcustomer.SelectedIndexChanged

    End Sub

    Function reporteitemname()

        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120

        modificargrilla()
        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        Dim fechacalendarioentrega As String
        fechacalendarioentrega = ssdatecheckout.Value.ToString("yyyy/MM/dd")
        '
        Dim idequipo As Integer
        Try

            'MsgBox(fechacalendarioentrega)
            cerrarconexion()
            conexionMysql.Open()

            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31 = "select idequipo from equipo where equipo like '%" & stxtphonenumber.Text & "%';"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            idequipo = reader.GetString(0).ToString()
            conexionMysql.Close()

        Catch ex As Exception

        End Try


        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql2 As String
            sql2 = "select * from venta where idequipo='" & idequipo & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
            cerrarconexion()
        End Try
    End Function

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub Rcbitems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rcbitems.SelectedIndexChanged
        'cargamos los valores del equipo
        cerrarconexion()
        conexionMysql.Open()

        Dim Sql31 As String
        'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        Sql31 = "SELECT * FROM equipo where equipo='" & rcbitems.Text & "' and idventa='" & rtxtbusquedafolio.Text & "';"
        Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
        reader = cmd31.ExecuteReader()
        reader.Read()
        rtxtequipo.Text = reader.GetString(0).ToString()
        rtxtmodelo.Text = reader.GetString(1).ToString()
        rtxtimei.Text = reader.GetString(2).ToString()
        rtxtpassword.Text = reader.GetString(3).ToString()
        rtxtstate.Text = reader.GetString(4).ToString()
        rtxtproblem.Text = reader.GetString(5).ToString()
        rtxtnote.Text = reader.GetString(6).ToString()
        rtxtcostoreparacion.Text = reader.GetString(7).ToString()
        rtxtpriceparts.Text = reader.GetString(8).ToString()
        conexionMysql.Close()


    End Sub

    Private Sub Rcbitemtemporal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rcbitemtemporal.SelectedIndexChanged

        'funcion que selecciona el valor del combo y actualiza datos
        seleccionaritem()






    End Sub
    Function seleccionaritem()

        rgrilla.Rows.Clear()

        ' MsgBox(activarbusqueda)
        'cargamos los valores del equipo
        cerrarconexion()
        conexionMysql.Open()
        ' MsgBox(rcbitemtemporal.Text)
        Dim folio As String
        If activarbusqueda = True Then
            folio = rtxtbusquedafolio.Text
        Else
            folio = slbfolio.Text
            'rrsumatorio()
        End If



        ' MsgBox(rcbitemtemporal.SelectedIndex)

        '        cb1.SelectedIndex(rcbitemtemporal.SelectedIndex)

        Dim indicefolio As Integer


        indicefolio = rcbitemtemporal.SelectedIndex

        cb1.SelectedIndex = indicefolio

        indicefolio = indicefolio + 1

        Dim Sql31 As String
        'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        Sql31 = "SELECT * FROM equipo where equipo='" & rcbitemtemporal.Text & "' and idventa='" & folio & "' and idequipo='" & cb1.Text & "';"
        Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
        reader = cmd31.ExecuteReader()
        reader.Read()



        Try
            Dim valor As String
            valor = reader.GetString(13).ToString()
            rtxtdatestatus.Text = valor
            ' MsgBox(valor)
        Catch ex As Exception

        End Try







        Try

            rtxtequipo.Text = reader.GetString(1).ToString()
        Catch ex As Exception

        End Try
        Try
            rtxtmodelo.Text = reader.GetString(2).ToString()
        Catch ex As Exception

        End Try
        Try
            rtxtimei.Text = reader.GetString(3).ToString()
        Catch ex As Exception

        End Try
        Try
            rtxtpassword.Text = reader.GetString(4).ToString()
        Catch ex As Exception

        End Try
        Try

            cbstate.Text = reader.GetString(5).ToString()
        Catch ex As Exception

        End Try

        Try

            rtxtproblem.Text = reader.GetString(6).ToString()
        Catch ex As Exception

        End Try
        Try
            rtxtnote.Text = reader.GetString(7).ToString()
        Catch ex As Exception

        End Try
        Try
            rtxtcostoreparacion.Text = reader.GetString(9).ToString()
        Catch ex As Exception

        End Try
        Try
            ' rtxtdatestatus.Text = reader.GetString(13).ToString()
        Catch ex As Exception

        End Try

        'tengo que asignar fechas al calendario.




        ' rtxtdeposito.Text = reader.GetString(13).ToString()

        Try

            rtxtpriceparts.Text = reader.GetString(10).ToString()
        Catch ex As Exception

        End Try




        conexionMysql.Close()



        cerrarconexion()
        conexionMysql.Open()
        Dim Sql312 As String
        'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        Sql312 = "SELECT * FROM equipo where equipo='" & rcbitemtemporal.Text & "' and idventa='" & folio & "'  and idequipo='" & cb1.Text & "';"
        Dim cmd312 As New MySqlCommand(Sql312, conexionMysql)
        reader = cmd312.ExecuteReader()
        reader.Read()
        rtxtpriceparts.Text = reader.GetString(10).ToString()
        conexionMysql.Close()

        'cerrarconexion()
        'conexionMysql.Open()
        'Dim Sql3123 As String
        ''consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        'Sql3123 = "SELECT * FROM mobi.equipo where equipo='" & rcbitemtemporal.Text & "' and idventa='" & folio & "'  and idequipo='" & cb1.Text & "';"
        'Dim cmd3123 As New MySqlCommand(Sql3123, conexionMysql)
        'reader = cmd3123.ExecuteReader()
        'reader.Read()
        'rtxtdeposito.Text = reader.GetString(13).ToString()
        'conexionMysql.Close()

        'cerrarconexion()
        'conexionMysql.Open()
        'Dim Sql31234 As String
        ''consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        'Sql31234 = "SELECT * FROM mobi.equipo where equipo='" & rcbitemtemporal.Text & "' and idventa='" & folio & "'  and idequipo='" & cb1.Text & "';"
        'Dim cmd31234 As New MySqlCommand(Sql31234, conexionMysql)
        'reader = cmd31234.ExecuteReader()
        'reader.Read()
        'rtxtresto.Text = reader.GetString(14).ToString()
        'conexionMysql.Close()
        'cerrarconexion()

        If activarbusqueda = True Then


            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "select * from venta where idventa='" & folio & "' ;"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            reader = cmd2.ExecuteReader()
            reader.Read()
            rtxttotal.Text = reader.GetString(3).ToString()
            'cbformadepagoservicios.Items.Add(reader.GetString(1).ToString())
            '---------------------------------
            cerrarconexion()
        Else
            rrsumatorio()

        End If

        conexionMysql.Open()
        '------------------



        'cargamos las piezas de reparacion en caso de que tenga
        Dim Sql311 As String
        'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        Sql311 = "select count(*)as contador from venta_ind where idventa=" & folio & " and name_item='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "';"
        Dim cmd311 As New MySqlCommand(Sql311, conexionMysql)
        reader = cmd311.ExecuteReader()
        reader.Read()
        Dim cantidad As Integer = reader.GetString(0).ToString()
        conexionMysql.Close()

        Dim j, i As Integer


        'MsgBox("valores:" & cantidad)

        cerrarconexion()
        conexionMysql.Open()
        Dim costo, idproducto, cantidadpro, actividad, idventa_ind As String
        Dim Sql3 As String
        Sql3 = "select * from venta_ind where idventa=" & folio & " and name_item='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "';"
        Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
        reader = cmd3.ExecuteReader()

        For i = 0 To cantidad - 1

            reader.Read()
            'MsgBox(i)
            idventa_ind = reader.GetString(0).ToString()
            actividad = reader.GetString(1).ToString()
            cantidadpro = reader.GetString(2).ToString()
            costo = reader.GetString(3).ToString()
            idproducto = reader.GetString(5).ToString()


            rgrilla.Rows.Add(idventa_ind, idproducto, actividad, costo, cantidadpro, CDbl(cantidadpro) * CDbl(costo))


        Next




    End Function
    Function reportemodel()
        modificargrilla()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120


        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        Dim fechacalendarioentrega As String
        fechacalendarioentrega = ssdatecheckout.Value.ToString("yyyy/MM/dd")
        '
        Dim idequipo As Integer
        Try

            'MsgBox(fechacalendarioentrega)
            cerrarconexion()
            conexionMysql.Open()

            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31 = "select idequipo from equipo where model like '%" & stxtphonenumber.Text & "%';"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            idequipo = reader.GetString(0).ToString()
            conexionMysql.Close()

        Catch ex As Exception

        End Try


        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql2 As String
            sql2 = "select * from venta where idequipo='" & idequipo & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try
    End Function

    Private Sub Rtxtpriceparts_TextChanged(sender As Object, e As EventArgs) Handles rtxtpriceparts.TextChanged

    End Sub

    Private Sub Rcbseller_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rcbseller.SelectedIndexChanged

    End Sub

    Function reporteimei()
        modificargrilla()
        'ssgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        'ssgrilla.RowHeadersVisible = False
        ''ampliar columna 
        ''grillap.Columns(2).Width = 120


        'ssgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        Dim fechacalendarioentrega As String
        fechacalendarioentrega = ssdatecheckout.Value.ToString("yyyy/MM/dd")
        '
        Dim idcliente As Integer
        Try

            'MsgBox(fechacalendarioentrega)
            cerrarconexion()
            conexionMysql.Open()

            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31 = "select idequipo from equipo where imei like '%" & sscustomername.Text & "%';"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            idcliente = reader.GetString(0).ToString()
            conexionMysql.Close()

        Catch ex As Exception

        End Try


        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql2 As String
            sql2 = "select * from venta,equipo where venta.idequipo=equipo.idequipo and venta.equipo='" & idcliente & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd2)
            'cargamos el formulario  resumen
            da.Fill(dt)
            ssgrilla.DataSource = dt
            '  pgrilla.Columns(1).Width = 500
            ' pgrilla.Columns(0).Width = 90
            'pgrilla.Columns(2).Width = 90
            'pgrilla.Columns(3).Width = 90
            'pgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try
    End Function

    Private Sub Rtxtprecio_TextChanged(sender As Object, e As EventArgs) Handles rtxtprecio.TextChanged
        rtxtprecio.BackColor = Color.White

    End Sub

    Private Sub rgrilla2_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles rgrilla2.CellContentDoubleClick
        Try

            'eliminar datos al dar doble clic.
            'grilla.CurrentRow.Index
            'Dim nombreitem As String
            ' nombreitem = rgrilla2.Rows().Cells(4).Value
            Dim Variable As String = rgrilla2.Item(1, rgrilla2.CurrentRow.Index).Value
            MsgBox(Variable)

            rgrilla2.Rows.RemoveAt(rgrilla2.CurrentRow.Index)
            'eliminado el registro, sumamos el total de valores. 
            rrsumatorio()

            'eliminamos los registros

            conexionMysql.Open()

            Dim sql2 As String
            sql2 = "delete from venta_ind where idventa='" & slbfolio.Text & "' and name_item='" & Variable & "'"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()




        Catch ex As Exception

        End Try
    End Sub

    Private Sub rgrilla2_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles rgrilla2.CellBeginEdit

    End Sub

    Private Sub Pgrilla_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles pgrilla.CellContentClick
        Dim Variable As String = pgrilla.Item(0, pgrilla.CurrentRow.Index).Value
        'MsgBox(Variable)
        ptxtclaveproducto.Text = Variable

    End Sub

    Private Sub rgrilla2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles rgrilla2.CellClick
        cargardatosgrilla2()
    End Sub

    Private Sub rlbcustomer_DoubleClick(sender As Object, e As EventArgs) Handles rlbcustomer.DoubleClick
        'rseleccioncliente()
    End Sub

    Private Sub frmindex_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'eliminarregistrostemporalesequipo()
    End Sub

    Private Sub GroupBox9_Enter(sender As Object, e As EventArgs) Handles GroupBox9.Enter

    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Dim formulario As New abono
        formulario.ShowDialog()
    End Sub

    Function comprobarintegridadreparacion()



        Try


            Dim cantidad As String
            cerrarconexion()
            conexionMysql.Open()

            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31 = "select count(*) from venta where idventa='" & slbfolio.Text & "'"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            cantidad = reader.GetString(0).ToString()
            conexionMysql.Close()

            'MsgBox(cantidad)
            If cantidad <= "0" Then
                ' MsgBox("estamos dentro")
                eliminarregistrostemporalesequipo()


            End If
        Catch ex As Exception
            cerrarconexion()
            MsgBox("error", MsgBoxStyle.Information, "MOBI")
        End Try

    End Function
    Function reimprimirreparacion()

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
            PrintDialog1.Document = PrintDocument2
            PrintDialog1.PrinterSettings.PrinterName = impresora
            If PrintDocument2.PrinterSettings.IsValid Then
                PrintDocument2.Print() 'Imprime texto 
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

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        'ssgrilla.Rows.Clear()
        stxtphonenumber.Text = ""
        sscustomername.Text = ""
        stxtmodel.Text = ""
        stxtorder.Text = ""



    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim formulario As New FRinventario
        formulario.ShowDialog()


    End Sub

    Private Sub Button88_Click(sender As Object, e As EventArgs) Handles Button88.Click
        Dim respuesta As String

        respuesta = MsgBox("¿Estas seguro de ELIMINAR completamente el archivo de la BD?", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "CTRL+y")
        If respuesta = vbYes Then
            '--------------------------------- en caso de que la respuesta sea correcta
            '--------------------------------- se procede a eliminar todo
            cerrarconexion()

            conexionMysql.Open()

            Dim sql2 As String
            sql2 = "drop database mobi"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()

            MsgBox("Base de datos eliminada" & Chr(13) &
            "El sistema se cerrara automaticamente para que vuelvas a cargar la BD", MsgBoxStyle.Information, "CTRL+y")
            End


        End If

    End Sub

    Private Sub Ssgrilla_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles ssgrilla.CellContentClick

    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        'vamos a quitar el item seleccionado

        'cerrarconexion()
        'conexionMysql.Open()
        'Dim Sql1 As String
        'Sql1 = "select "
        'Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
        'reader = cmd1.ExecuteReader()
        'reader.Read()
        ''= reader.GetString(1).ToString()
        'conexionMysql.Close()

        cerrarconexion()
        conexionMysql.Open()
        ' MsgBox(rcbitemtemporal.Text)
        Dim folio As String
        If activarbusqueda = True Then
            folio = rtxtbusquedafolio.Text
        Else
            folio = slbfolio.Text
            'rrsumatorio()
        End If


        Dim indicefolio As Integer


        indicefolio = rcbitemtemporal.SelectedIndex

        indicefolio = indicefolio + 1
        ' MsgBox(rcbitemtemporal.SelectedIndex)
        Dim idequipo As Integer
        Try



            Dim Sql31 As String
            'consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
            Sql31 = "SELECT idequipo FROM equipo where equipo='" & rcbitemtemporal.Text & "' and idventa='" & folio & "' and idequipo='" & cb1.Text & "';"
            Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
            reader = cmd31.ExecuteReader()
            reader.Read()
            idequipo = reader.GetString(0).ToString()
            'rtxtpriceparts.Text = reader.GetString(10).ToString()
            conexionMysql.Close()

            cerrarconexion()

            MsgBox(idequipo)
        Catch ex As Exception
            idequipo = 0
            cerrarconexion()
        End Try

        If idequipo = 0 Then

        Else

            Try

                conexionMysql.Open()
                Dim Sql26 As String
                Sql26 = "delete from equipo where idequipo=" & idequipo & ";"
                Dim cmd26 As New MySqlCommand(Sql26, conexionMysql)
                cmd26.ExecuteNonQuery()
                conexionMysql.Close()
            Catch ex As Exception
                cerrarconexion()
            End Try

            'removemos del comboimteremportal el valor seleccionado y limpiamos las cajas de texto


            rcbitemtemporal.Items.Remove(rcbitemtemporal.Text)
            'cb1.Items.Remove(cb1.Text)


            Dim nuevoindice = cb1.Text

            cb1.Items.RemoveAt(indicefolio - 1)
            'indicefolio = indicefolio + 1
            'rtxtcustomername.Text = ""
            'rtxtstate.Text = ""
            rtxtnombre.Text = ""
            rtxtequipo.Text = ""
            rtxtdeposito.Text = ""
            rtxtdeposito.Text = ""
            rtxtimei.Text = ""
            rtxtnote.Text = ""
            rtxtprecio.Text = ""
            rtxtproblem.Text = ""
            'rtxtcity.Text = ""
            'rtxttelephone.Text = ""
            'rtxtaddress.Text = ""
            rtxtmodelo.Text = ""
            rtxtpassword.Text = ""
            'rtxttotal.Text = ""
            rtxtcostoreparacion.Text = ""
            'rtxtresto.Text = ""
            'cbstate.SelectedIndex = 0
            'rcbseller.SelectedIndex = 0
            'rtxtemail.Text = ""
            'rgrilla.Rows.Clear()
            'rgrilla2.Rows.Clear()
            rtxttotaltemporal.Text = ""
            rtxtpriceparts.Text = ""
            'limpiamos las cajas
            'activarbusqueda = False
            'rtxtbusquedafolio.Visible = False
            'rlimpiartodo()
            'habilitarcajasdevice()
            'comprobarventarealizada()
            'rcbitems.Visible = False
            'rcbitemtemporal.Visible = True
            'rcbitemtemporal.Items.Clear()
            'rcbitems.Items.Clear()
            'activarreparacion = False
            'activarbusqueda = False
            rtxtclaveproducto.Text = ""
            'actualizarcalculototal()

            'rsumatorioactualizar()
            '----------------------------------------------------elminamos de la grilla temporal
            For i As Integer = 0 To rgrilla2.Rows.Count - 1
                'Si el valor de la primera celda ubicada, por ejemplo, en la fila 1 es igual a 3
                ' MsgBox("grilla:" & i)

                If rgrilla2.Rows(i).Cells(0).Value = nuevoindice Then
                    'Mueve el cursor a dicha fila
                    '  MsgBox("se elimina")
                    rgrilla2.Rows.RemoveAt(i)
                    'rgrilla2.Rows.RemoveAt(i)
                    ' DataGridView1.Rows.RemoveAt(0)
                    Exit For
                End If
            Next

            rrsumatorio()
            '------------------------------------------------------------
            ' actualizarcalculototal()

            '------------------------------
            'realizamos la busqueda nuevamente de la actual folio
            realizarbusqueda()
        End If


    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click

        cerrarconexion()







        '--------------------agreamos una columna mas a equipo.--------------------------------
        Try

            conexionMysql.Open()
            Dim Sqlu5 As String
            Sqlu5 = "ALTER TABLE `equipo` 
ADD COLUMN `datedelivery` DATE NULL AFTER `posicion`;"
            Dim cmdu5 As New MySqlCommand(Sqlu5, conexionMysql)
            cmdu5.ExecuteNonQuery()
            conexionMysql.Close()
            'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
            conexionMysql.Close()
        Catch ex As Exception
            cerrarconexion()

        End Try



        ''-------------------actualizamos todos los registros anteriores a su fecha de entrega, igual para todos-----------
        'conexionMysql.Open()
        'Dim Sql312 As String
        'Dim cantidad As Integer
        ''consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        'Sql312 = "select count(*) from venta where tipoventa=2;"
        'Dim cmd312 As New MySqlCommand(Sql312, conexionMysql)
        'reader = cmd312.ExecuteReader()
        'reader.Read()
        'cantidad = reader.GetString(0).ToString()
        ''rtxtpriceparts.Text = reader.GetString(10).ToString()
        'conexionMysql.Close()
        ''--------------------------
        'Dim matriz(cantidad, cantidad) As String

        'Dim idequipo As Integer
        'Dim fechaentrega As String
        'conexionMysql.Open()
        '    Dim Sql31 As String
        ''consultamos el id del cliente para obtener un registro de quien es al que se le esta vendiendo
        'Sql31 = "select idventa,fechaentrega from venta where idventa=2;"
        'Dim cmd31 As New MySqlCommand(Sql31, conexionMysql)
        '    reader = cmd31.ExecuteReader()
        'For i = 1 To cantidad


        '    reader.Read()
        '    idequipo = reader.GetString(0).ToString()
        '    fechaentrega = reader.GetString(0).ToString()

        '    matriz(i, i) =

        '    'rtxtpriceparts.Text = reader.GetString(10).ToString()
        '    ' conexionMysql.Close()

        '    Try

        '        conexionMysql.Open()
        '        Dim Sql26 As String
        '        Sql26 = "update equipo set datedelivery='" & fechaentrega & "' where idequipo=" & idequipo & ";"
        '        Dim cmd26 As New MySqlCommand(Sql26, conexionMysql)
        '        cmd26.ExecuteNonQuery()
        '        conexionMysql.Close()
        '    Catch ex As Exception

        '    End Try



        'Next


        '        Try

        '            Try

        '                conexionMysql.Open()
        '                Dim Sqlu56 As String
        '                Sqlu56 = "ALTER TABLE `mobi`.`equipo` 
        'ADD COLUMN `resto` DOUBLE NULL AFTER `deposito`;"
        '                Dim cmdu56 As New MySqlCommand(Sqlu56, conexionMysql)
        '                cmdu56.ExecuteNonQuery()
        '                conexionMysql.Close()
        '                'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
        '                conexionMysql.Close()
        '            Catch ex As Exception
        '                cerrarconexion()

        '            End Try



        Try

            conexionMysql.Open()
            Dim Sqlu As String
            Sqlu = "CREATE USER 'root'@'%' IDENTIFIED BY 'conexion';"
            Dim cmdu As New MySqlCommand(Sqlu, conexionMysql)
            cmdu.ExecuteNonQuery()
            conexionMysql.Close()
            'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
            conexionMysql.Close()
        Catch ex As Exception

            cerrarconexion()
        End Try


        Try

            conexionMysql.Open()

            Dim sql23 As String
            sql23 = "ALTER TABLE `venta_ind` 
ADD COLUMN `idequipo` VARCHAR(45) NULL AFTER `name_item`;;"
            Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
            cmd23.ExecuteNonQuery()

            conexionMysql.Close()
        Catch ex As Exception
            cerrarconexion()
        End Try




        Try

            conexionMysql.Open()

            Dim sql23 As String
            sql23 = "ALTER TABLE `venta_ind` 
ADD COLUMN `idequipo` VARCHAR(45) NULL AFTER `name_item`;;"
            Dim cmd23 As New MySqlCommand(sql23, conexionMysql)
            cmd23.ExecuteNonQuery()

            conexionMysql.Close()
        Catch ex As Exception
            cerrarconexion()
        End Try

        cerrarconexion()




        Try
            conexionMysql.Open()

            Dim sql2 As String
            sql2 = "ALTER TABLE `equipo` 
ADD COLUMN `posicion` INT NULL AFTER `idventa`;"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()


        Catch ex As Exception
            cerrarconexion()
        End Try


        cerrarconexion()
        Try

            conexionMysql.Open()
            Dim Sqlu1 As String
            Sqlu1 = "GRANT ALL PRIVILEGES ON * . * TO 'root'@'%' WITH GRANT OPTION;"
            Dim cmdu1 As New MySqlCommand(Sqlu1, conexionMysql)
            cmdu1.ExecuteNonQuery()
            conexionMysql.Close()


        Catch ex As Exception
            cerrarconexion()
        End Try

        cerrarconexion()

        MsgBox("Parche aplicado", MsgBoxStyle.Information, "MOBI")
        cerrarconexion()




    End Sub

    Private Sub Cb1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb1.SelectedIndexChanged

    End Sub

    Private Sub PrintDocument3_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument3.PrintPage
        'e.Graphics.DrawString(txtetiqueta1, New Font("verdana", 11, FontStyle.Bold), New SolidBrush(Color.Black), 1, 9)
        'e.Graphics.DrawString(txtetiqueta2, New Font("verdana", 9, FontStyle.Bold), New SolidBrush(Color.Black), 1, 28)
        'e.Graphics.DrawString(txtetiqueta, New Font("verdana", 13, FontStyle.Bold), New SolidBrush(Color.Black), 1, 57)
        'traemos la información del ticket, como encabezado, datos de la empresa etc.
        Dim saludo, ticketnombre, ticketcoloniaciudad, tickettelefono, ticketgiro, p1, p2, p3, p4, comentario, saludo2, saludo3, saludo4, saludo5 As String
        Dim callenumero, cp, estado, whatsapp, correo, rfc As String
        Dim x, y, tfuente, tfuente2, tfuente3 As Integer
        'Try

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

        ' Catch ex As Exception
        cerrarconexion()
        'End Try

        tfuente = 10 '7
        tfuente2 = 14
        tfuente3 = 16
        p1 = 10 'posicion de X
        x = 5
        y = 125
        Dim ii, incremento As Integer
        incremento = 14
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
            e.Graphics.DrawImage(pblogoticket.Image, 50, 20, 110, 110)


            'imprimir el titutlo del ticket



            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString(ticketnombre, prFont, Brushes.Black, x, yy(0))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))
            'IMPRESION DE LOGOTIPO,
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(callenumero, prFont, Brushes.Black, x, yy(3))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(ticketcoloniaciudad, prFont, Brushes.Black, x, yy(4))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("CP:" & cp, prFont, Brushes.Black, x, yy(5))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("TEL:" & tickettelefono, prFont, Brushes.Black, x, yy(6))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("RFC:" & rfc, prFont, Brushes.Black, x, yy(7))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, yy(8))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Técnico:" & rcbseller.Text, prFont, Brushes.Black, x, yy(9))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Cliente:" & rtxtcustomername.Text, prFont, Brushes.Black, x, yy(10))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Fecha:" & Date.Now, prFont, Brushes.Black, x, yy(11))
            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString("Orden:" & slbfolio.Text, prFont, Brushes.Black, x, yy(12))


            '---------------------------------------------------------------------------------------------------------------------------------
            'consulto, cuantos dispositivos son, para obtener su informacion

            Dim cantidaddispositivos, vueltas As Integer
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql12 As String
            'temporalmente slbfolio.text por rtxtbusquedatemporal
            Sql12 = "select count(*) from equipo where idventa='" & slbfolio.Text & "';"
            Dim cmd12 As New MySqlCommand(Sql12, conexionMysql)
            reader = cmd12.ExecuteReader()
            reader.Read()
            cantidaddispositivos = reader.GetString(0).ToString()
            'callenumero = reader.GetString(2).ToString()
            'ticketcoloniaciudad = reader.GetString(3).ToString()
            'cp = reader.GetString(4).ToString()
            conexionMysql.Close()
            cerrarconexion()

            conexionMysql.Open()
            Dim Sql123, problema, equipo, modelo, imei, estadox As String
            Sql123 = "select * from equipo where idventa='" & slbfolio.Text & "';"
            Dim cmd123 As New MySqlCommand(Sql123, conexionMysql)
            reader = cmd123.ExecuteReader()

            Dim pp1, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14 As Integer

            pp1 = yy(13)

            p2 = yy(14)
            p3 = yy(15)
            p4 = yy(16)
            p5 = yy(17)
            p6 = yy(18)
            p7 = yy(19)
            p8 = yy(20)
            p9 = yy(21)
            p10 = yy(22)
            p11 = yy(23)
            p12 = yy(24)
            p13 = yy(25)
            p14 = yy(26)




            For vueltas = 1 To cantidaddispositivos


                reader.Read()
                equipo = reader.GetString(1).ToString()
                modelo = reader.GetString(2).ToString()
                imei = reader.GetString(3).ToString()
                estadox = reader.GetString(5).ToString()
                problema = reader.GetString(6).ToString()


                'imprimir el titutlo del ticket
                '----------------------------------------------------------------------------------------------------------------------------

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, pp1)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("--PROBLEMA--", prFont, Brushes.Black, x, p2)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString(problema, prFont, Brushes.Black, x, p3)

                'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                'e.Graphics.DrawString("ID------PRECIO------CANTIDAD----TOTAL", prFont, Brushes.Black, x, yy(15))





                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p4)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("--Datos Disp.--", prFont, Brushes.Black, x, p5)
                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Nombre Disp.:" & equipo, prFont, Brushes.Black, x, p6)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Modelo:" & modelo, prFont, Brushes.Black, x, p7)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("IMEI:" & imei, prFont, Brushes.Black, x, p8)


                'prFont = New Font("Arial", tfuente, FontStyle.Bold)
                'e.Graphics.DrawString("Status:" & estadox, prFont, Brushes.Black, x, p9)
                '----------------------------------------------------------------------------------------------------------------------

                '                yy = yy + (incremento * 3.2)
                '               yy = yy + (incremento * 3.2)




            Next
            conexionMysql.Close()
            cerrarconexion()

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, 0, p8)



            Dim fechacalendarioentrega As String
            fechacalendarioentrega = rcalendario.Value.ToString("yyyy/MM/dd")


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Fecha entrega:" & fechacalendarioentrega, prFont, Brushes.Black, x, p9)


            Dim multi As Integer
            multi = 6

            pp1 = pp1 + (incremento * multi)
            p2 = p2 + (incremento * multi)
            p3 = p3 + (incremento * multi)
            p4 = p4 + (incremento * multi)
            p5 = p5 + (incremento * multi)
            p6 = p6 + (incremento * multi)
            p7 = p7 + (incremento * multi)
            p8 = p8 + (incremento * multi)
            p9 = p9 + (incremento * multi)
            p10 = p10 + (incremento * multi)
            p11 = p11 + (incremento * multi)
            p12 = p12 + (incremento * multi)
            p13 = p13 + (incremento * multi)
            p13 = p14 + (incremento * multi)
            'aqui es donde tenemos que leer todos los datos de la grilla llamada "grilla"

            Dim i As Integer = sgrilla.RowCount
            'MsgBox(i)
            Dim t1, t2, t3, t4, t5 As Integer
            Dim actividad As String
            Dim cantidad, costo, idventa As Double
            Dim idproducto As String
            Dim jj As Integer
            ' t1 = yy(16)
            ' t2 = yy(17)
            ' t3 = yy(18)
            ' t4 = 40
            'MsgBox(jj)
            'MsgBox()
            'suma de valores
            '''''''For jj = 0 To i - 1



            '   MsgBox("valosr de j:" & jj)
            'a = venta.grillaventa.Item(j, 3).Value.ToString()
            '''''''actividad = sgrilla.Rows(jj).Cells(1).Value 'descripcion
            '''''''cantidad = sgrilla.Rows(jj).Cells(2).Value 'cantidad
            '''''''costo = sgrilla.Rows(jj).Cells(3).Value 'costo
            '''''''idproducto = sgrilla.Rows(jj).Cells(0).Value
            '''''''comentario = sgrilla.Rows(jj).Cells(4).Value
            'cerrarconexion()
            'conexionMysql.Open()

            ' MsgBox("el producto es:" & actividad)

            'Dim Sql2 As String
            'Sql2 = "INSERT INTO ventaind (actividad, cantidad, costo, idventa,idproducto) VALUES ('" & actividad & "'," & cantidad & "," & costo & "," & lbfolio.Text & ",'" & idproducto & "');"
            'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            'cmd2.ExecuteNonQuery()
            'conexionMysql.Close()


            'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t2)


            '    'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    'e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t3)
            '    '----------
            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString("$" & costo, prFont, Brushes.Black, x, t3)
            '    '----------
            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString(cantidad, prFont, Brushes.Black, x + 80, t3)
            '    '-----------

            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString("$" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, x + 160, t3)
            ''-----------



            'prFont = New Font("Arial", 10, FontStyle.Bold)
            'e.Graphics.DrawString(cantidad & "-- $" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, 0, t3)

            '''''''   t1 = t1 + (incremento * 3.2)
            '''''''t2 = t2 + (incremento * 3.2)
            '''''''t3 = t3 + (incremento * 3.2)

            ''''''' Next

            t1 = t1 - (incremento * 2)
            t2 = t2 - (incremento * 2)
            t3 = t3 - (incremento * 2)



            '----------------AQUI SE IMPRIME EL TOTAL A PAGAR
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql14, total, deposito, resto As String
            Sql14 = "select total,deposito,resto from venta where idventa='" & slbfolio.Text & "';"
            Dim cmd14 As New MySqlCommand(Sql14, conexionMysql)
            reader = cmd14.ExecuteReader()
            reader.Read()
            total = reader.GetString(0).ToString()
            deposito = reader.GetString(1).ToString()
            resto = reader.GetString(2).ToString()

            conexionMysql.Close()

            cerrarconexion()


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Total:", prFont, Brushes.Black, x, pp1)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & total, prFont, Brushes.Black, x + 150, p2)

            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("Price:", prFont, Brushes.Black, x, p5)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("$ " & rtxtcostoreparacion.Text, prFont, Brushes.Black, x + 150, p6)


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Anticipo:", prFont, Brushes.Black, x, p3)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & deposito, prFont, Brushes.Black, x + 150, p4)


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Resto:", prFont, Brushes.Black, x, p5)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & resto, prFont, Brushes.Black, x + 150, p6)



            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p7)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo, prFont, Brushes.Black, x, p8)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo2, prFont, Brushes.Black, x, p8)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo3, prFont, Brushes.Black, x, p8)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo4, prFont, Brushes.Black, x, p8)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo5, prFont, Brushes.Black, x, p8)
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

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Dim res As Integer
        res = 1
        If ctxtidscustomer.Text = "" Or ctxtcustomername.Text = "" Then
            MsgBox("Hace falta información", MsgBoxStyle.Exclamation, "Sistema")
        Else
            'Try
            Dim clave As String
            cerrarconexion()
            'consulto que el ID no exista para poder ingresar uno nuevo
            conexionMysql.Open()
            Dim Sql As String
            Sql = "select idcustomer from customer where idcustomer='" & ctxtidscustomer.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            Try
                clave = reader.GetString(0).ToString
            Catch ex As Exception
                clave = ""
            End Try
            cerrarconexion()

            conexionMysql.Close()
            'comprobar si devolvio null o un valor real
            If clave = ctxtidscustomer.Text Then
                MsgBox("La clave ya existe, asigna un nuevo valor", MsgBoxStyle.Exclamation, "Sistema")
                'Catch ex As Exception
                res = 0
            End If


            'End Try

            '----------------------------------------- obtener id de proveedor
            Dim idproveedor As Integer
            cerrarconexion()

            'conexionMysql.Open()
            'Dim Sql5 As String
            'Sql5 = "select idproveedor from proveedor where nombre_empresa='" & txtproveedor.Text & "';"
            'Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
            'reader = cmd5.ExecuteReader()
            'reader.Read()

            'idproveedor = reader.GetString(0).ToString

            'conexionMysql.Close()

            ''----------------------------------------- obtener id de tipoproducto
            'Dim idtipoproducto As Integer
            'cerrarconexion()

            'conexionMysql.Open()
            'Dim Sql55 As String
            'Sql55 = "select idtipoproducto from tipoproducto where tipo='" & txttipoproducto.Text & "';"
            'Dim cmd55 As New MySqlCommand(Sql55, conexionMysql)
            'reader = cmd55.ExecuteReader()
            'reader.Read()

            'idtipoproducto = reader.GetString(0).ToString

            'conexionMysql.Close()





            If res <> 0 Then


                Try



                    'If a.Text = "" Then
                    '    a.Text = "-"
                    'End If

                    'If b.Text = "" Then
                    '    b.Text = "-"
                    'End If
                    'If c.Text = "" Then
                    '    c.Text = "-"
                    'End If
                    'If d.Text = "" Then
                    '    d.Text = "00"
                    'End If

                    'If f.Text = "" Then
                    '    f.Text = "-"
                    'End If
                    'comprobar vacio


                    cerrarconexion()

                    conexionMysql.Open()

                    Dim sql2 As String
                    sql2 = "INSERT INTO customer (idcustomer,name, address, state, city, telephone, email) VALUES ('" & ctxtidscustomer.Text & "', '" & ctxtcustomername.Text & "', '" & ctxtadress.Text & "', '" & ctxtstate.Text & "', '" & ctxtcity.Text & "','" & ctxttelephone.Text & "','" & ctxtemail.Text & "');"
                    Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                    cmd2.ExecuteNonQuery()
                    conexionMysql.Close()



                    '---------------------------------------------------------

                    MsgBox("Registro guardado", MsgBoxStyle.Information, "Sistema")
                    'se llena la grilla, tomando en cuenta ninguna elemento.
                    'txtnombrep.Text = ""
                    Call cllenadogrilla()

                Catch ex As Exception
                    MsgBox("Existe un problema al guardar al registro", MsgBoxStyle.Information, "Sistema")
                    cerrarconexion()
                End Try

                Call climpiar()

            End If

        End If
    End Sub
    Function climpiar()
        ctxtidscustomer.Text = ""
        ctxtcustomername.Text = ""
        ctxtadress.Text = ""
        ctxtcity.Text = ""
        ctxtemail.Text = ""
        ctxttelephone.Text = ""
        ctxtstate.Text = ""

    End Function
    Function cllenadogrilla()

        cgrilla.DefaultCellStyle.Font = New Font("Arial", 16)
        cgrilla.RowHeadersVisible = False
        'ampliar columna 
        'grillap.Columns(2).Width = 120


        cgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue

        Try


            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If
            cerrarconexion()

            conexionMysql.Open()
            Dim sql As String
            sql = "select * from customer;"
            Dim cmd As New MySqlCommand(sql, conexionMysql)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            'cargamos el formulario  resumen
            da.Fill(dt)
            cgrilla.DataSource = dt
            cgrilla.Columns(1).Width = 400
            cgrilla.Columns(0).Width = 90
            cgrilla.Columns(2).Width = 90
            cgrilla.Columns(3).Width = 90
            cgrilla.Columns(4).Width = 90
            'grillap.Columns(5).Width = 60
            'grillap.Columns(6).Width = 60
            'grillap.Columns(7).Width = 60

            conexionMysql.Close()
        Catch ex As Exception
            MsgBox("Error del sistema", MsgBoxStyle.Exclamation, "Sistema")
        End Try
    End Function

    Private Sub FlowLayoutPanel7_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel7.Paint

    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        'reporteorderinicial()
        TabControl1.SelectedIndex = 6
        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.FromArgb(27, 38, 44)
        Button14.BackColor = Color.FromArgb(27, 38, 44)
        Button38.BackColor = Color.DimGray
        'Button38.BackColor = Color.FromArgb(27, 38, 44)
        cgrilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        cllenadogrilla()
    End Sub

    Private Sub Cgrilla_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles cgrilla.CellContentClick
        Dim Variable As String = cgrilla.Item(0, cgrilla.CurrentRow.Index).Value
        ' MsgBox(Variable)
        ctxtidscustomer.Text = Variable
    End Sub

    Private Sub Btnreimpresion_Click(sender As Object, e As EventArgs) Handles btnreimpresion.Click
        If activarbusqueda = True Then
            imprimirreparacion()
        End If
    End Sub

    Private Sub Ctxtidscustomer_TextChanged(sender As Object, e As EventArgs) Handles ctxtidscustomer.TextChanged
        If ctxtidscustomer.Text = "" Then
            climpiar()

        Else
            cbuscarid()

        End If
    End Sub
    Function cbuscarid()
        If ctxtidscustomer.Text = "" Then
            'MsgBox("aqui no paso nada")
        Else
            'MsgBox("comenzamos a buscar")
            Try
                Dim cantidad As Integer

                ' respuesta = vbYes

                cerrarconexion()

                If reader.HasRows Then
                    reader.Close()

                End If

                Dim claveproveedor, clavetipoproducto As Integer

                cerrarconexion()

                'Try
                'MsgBox(txtclavep.Text)

                ' Try
                'MsgBox(txtclavep.Text)
                conexionMysql.Open()
                Dim Sql As String
                Sql = "select * from customer where idcustomer='" & ctxtidscustomer.Text & "';"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader()
                reader.Read()
                ctxtcustomername.Text = reader.GetString(1).ToString()
                ctxtadress.Text = reader.GetString(2).ToString()
                ctxtstate.Text = reader.GetString(3).ToString()
                ctxtcity.Text = reader.GetString(4).ToString()
                ctxttelephone.Text = reader.GetString(5).ToString()
                ctxtemail.Text = reader.GetString(6).ToString()

                reader.Close()





                'MsgBox("actividad:" & txtactividad.Text)

                'Catch ex As Exception
                'MsgBox("hay problemas", MsgBoxStyle.Exclamation)
                ' btninconsistencia.Visible = True
                cerrarconexion()
                'ptxtnombreproducto.Text = ""
                'ptxtprecio.Text = ""
                'ptxtcosto.Text = ""
                'ptxtcantidad.Text = ""
                ' End Try


                conexionMysql.Close()




                '   MsgBox(clavetipoproducto)

                If reader.HasRows Then
                    reader.Close()

                End If




            Catch ex As Exception
                'MsgBox("El producto no existe o no se a podido procesar", MsgBoxStyle.Exclamation, "Sistema")

                'tipoingreso()
                cerrarconexion()


                Call climpiar()


                'MsgBox("Hay detalles con el proceso", MsgBoxStyle.Information, "CTRL+y")

            End Try
        End If
    End Function

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If ctxtidscustomer.Text = "" Or ctxtcustomername.Text = "" Then
            MsgBox("Hay cajas vacias, verifica nuevamente", MsgBoxStyle.Information, "Sistema")
        Else





            'Try
            cerrarconexion()

            conexionMysql.Open()

            Dim sql2 As String
            sql2 = "UPDATE customer SET name='" & ctxtcustomername.Text & "', address='" & ctxtadress.Text & "', state='" & ctxtstate.Text & "', city='" & ctxtcity.Text & "',  telephone='" & ctxttelephone.Text & "', email='" & ctxtemail.Text & "'  WHERE idcustomer='" & ctxtidscustomer.Text & "';"
            Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
            cmd2.ExecuteNonQuery()

            conexionMysql.Close()
            MsgBox("Registro actualizado", MsgBoxStyle.Information, "Sistema")


            Call climpiar()


            Call cllenadogrilla()

            'Catch ex As Exception
            cerrarconexion()

            ' End Try



        End If

        'End If
    End Sub

    Private Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        respaldar.DefaultExt = "sql"
        Dim pathmysql As String
        Dim comando As String
        'pathmysql = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\MySQL AB\MYSQL Server 5.5", "Location", 0)
        pathmysql = "C:\Program Files\MySQL\MySQL Server 5.5"
        If pathmysql = Nothing Then
            MsgBox("No se encontro en tu equipo Mysql, escoge la carpeta donde esta ubicado")
            carpeta.ShowDialog()
            pathmysql = carpeta.SelectedPath
        End If
        respaldar.Filter = "File MYSQL (*.sql)|*.sql"
        If respaldar.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Try

                Dim contra As String
                'Call InputBox_Password(frmindex, "*")
                contra = InputBox("Ingresa la contraseña del administrador", "CTRL+Y")

                comando = pathmysql & "\bin\mysqldump --user=root --password=" & contra & " --databases mobi --routines -r """ & respaldar.FileName & """"
                Shell(comando, AppWinStyle.MinimizedFocus, True)
                MsgBox("Se realizo el respaldo correctamente", MsgBoxStyle.Information, "Sistema")
            Catch ex As Exception
                MsgBox("Ocurrio un error al respaldar", MsgBoxStyle.Critical, "Informacion")
            End Try

        End If

    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Dim formulario As New FRcustomer
        formulario.ShowDialog()

    End Sub

    Private Sub PrintDocument4_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument4.PrintPage
        Dim folio As Integer
        If activarbusqueda = True Then
            folio = rtxtbusquedafolio.Text
        Else
            folio = slbfolio.Text
        End If

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

        tfuente = 10 '7
        tfuente2 = 14
        tfuente3 = 16
        p1 = 10 'posicion de X
        x = 5
        y = 5
        Dim ii, incremento As Integer
        incremento = 14
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
            'e.Graphics.DrawImage(pblogoticket.Image, 50, 20, 120, 120)


            'imprimir el titutlo del ticket



            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString(ticketnombre, prFont, Brushes.Black, x, yy(0))
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))
            ''IMPRESION DE LOGOTIPO,
            ''prFont = New Font("Arial", tfuente, FontStyle.Bold)
            ''e.Graphics.DrawString(ticketgiro, prFont, Brushes.Black, x, yy(2))


            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(callenumero, prFont, Brushes.Black, x, yy(3))

            ''imprimir el titutlo del ticket
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(ticketcoloniaciudad, prFont, Brushes.Black, x, yy(4))
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString(cp, prFont, Brushes.Black, x, yy(5))
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("TEL:" & tickettelefono, prFont, Brushes.Black, x, yy(6))
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("LIC." & rfc, prFont, Brushes.Black, x, yy(7))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, yy(1))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Técnico:" & rcbseller.Text, prFont, Brushes.Black, x, yy(2))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Cliente:" & rtxtcustomername.Text, prFont, Brushes.Black, x, yy(3))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Telefono" & rtxttelephone.Text, prFont, Brushes.Black, x, yy(4))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)

            e.Graphics.DrawString("Fecha:" & Date.Now, prFont, Brushes.Black, x, yy(5))
            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString("ORDEN #" & folio, prFont, Brushes.Black, x, yy(6))


            '---------------------------------------------------------------------------------------------------------------------------------
            'consulto, cuantos dispositivos son, para obtener su informacion

            Dim cantidaddispositivos, vueltas As Integer
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql12 As String
            'temporalmente slbfolio.text por rtxtbusquedatemporal
            Sql12 = "select count(*) from equipo where idventa='" & folio & "';"
            Dim cmd12 As New MySqlCommand(Sql12, conexionMysql)
            reader = cmd12.ExecuteReader()
            reader.Read()
            cantidaddispositivos = reader.GetString(0).ToString()
            'callenumero = reader.GetString(2).ToString()
            'ticketcoloniaciudad = reader.GetString(3).ToString()
            'cp = reader.GetString(4).ToString()
            conexionMysql.Close()
            cerrarconexion()

            conexionMysql.Open()
            Dim Sql123, problema, equipo, modelo, imei, estadox, pass, notes As String
            Sql123 = "select * from equipo where idventa='" & folio & "';"
            Dim cmd123 As New MySqlCommand(Sql123, conexionMysql)
            reader = cmd123.ExecuteReader()

            Dim pp1, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14 As Integer

            pp1 = yy(7)

            p2 = yy(8)
            p3 = yy(9)
            p4 = yy(10)
            p5 = yy(11)
            p6 = yy(12)
            p7 = yy(13)
            p8 = yy(14)
            p9 = yy(15)
            p10 = yy(16)
            p11 = yy(17)
            p12 = yy(18)
            p13 = yy(19)
            p14 = yy(20)




            For vueltas = 1 To cantidaddispositivos


                reader.Read()
                equipo = reader.GetString(1).ToString()
                modelo = reader.GetString(2).ToString()
                pass = reader.GetString(4).ToString()
                notes = reader.GetString(7).ToString()
                problema = reader.GetString(6).ToString()


                'imprimir el titutlo del ticket
                '----------------------------------------------------------------------------------------------------------------------------

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, pp1)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("--PROBLEMA--", prFont, Brushes.Black, x, p2)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString(problema, prFont, Brushes.Black, x, p3)

                'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                'e.Graphics.DrawString("ID------PRECIO------CANTIDAD----TOTAL", prFont, Brushes.Black, x, yy(15))





                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p4)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("--Datos Dispo.--", prFont, Brushes.Black, x, p5)
                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Nombre Dispo.:" & equipo, prFont, Brushes.Black, x, p6)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Model0:" & modelo, prFont, Brushes.Black, x, p7)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Password: " & pass, prFont, Brushes.Black, x, p8)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Nota:" & notes, prFont, Brushes.Black, x, p9)
                '----------------------------------------------------------------------------------------------------------------------

                pp1 = pp1 + (incremento * 9)
                p2 = p2 + (incremento * 9)
                p3 = p3 + (incremento * 9)
                p4 = p4 + (incremento * 9)
                p5 = p5 + (incremento * 9)
                p6 = p6 + (incremento * 9)
                p7 = p7 + (incremento * 9)
                p8 = p8 + (incremento * 9)
                p9 = p9 + (incremento * 9)
                'p10 = p10 + (incremento * 7)
                'p11 = p11 + (incremento * 7)
                'p12 = p12 + (incremento * 7)
                'p13 = p13 + (incremento * 7)
                'p14 = p14 + (incremento * 7)
                '                yy = yy + (incremento * 3.2)
                '               yy = yy + (incremento * 3.2)




            Next
            conexionMysql.Close()
            cerrarconexion()

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, 0, pp1)



            Dim fechacalendarioentrega As String
            fechacalendarioentrega = rcalendario.Value.ToString("MM/dd/yyyy")


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Fecha entrega:" & fechacalendarioentrega, prFont, Brushes.Black, x, p2)


            'aqui es donde tenemos que leer todos los datos de la grilla llamada "grilla"

            Dim i As Integer = sgrilla.RowCount
            'MsgBox(i)
            Dim t1, t2, t3, t4, t5 As Integer
            Dim actividad As String
            Dim cantidad, costo, idventa As Double
            Dim idproducto As String
            Dim jj As Integer
            ' t1 = yy(16)
            ' t2 = yy(17)
            ' t3 = yy(18)
            ' t4 = 40
            'MsgBox(jj)
            'MsgBox()
            'suma de valores
            '''''''For jj = 0 To i - 1



            '   MsgBox("valosr de j:" & jj)
            'a = venta.grillaventa.Item(j, 3).Value.ToString()
            '''''''actividad = sgrilla.Rows(jj).Cells(1).Value 'descripcion
            '''''''cantidad = sgrilla.Rows(jj).Cells(2).Value 'cantidad
            '''''''costo = sgrilla.Rows(jj).Cells(3).Value 'costo
            '''''''idproducto = sgrilla.Rows(jj).Cells(0).Value
            '''''''comentario = sgrilla.Rows(jj).Cells(4).Value
            'cerrarconexion()
            'conexionMysql.Open()

            ' MsgBox("el producto es:" & actividad)

            'Dim Sql2 As String
            'Sql2 = "INSERT INTO ventaind (actividad, cantidad, costo, idventa,idproducto) VALUES ('" & actividad & "'," & cantidad & "," & costo & "," & lbfolio.Text & ",'" & idproducto & "');"
            'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            'cmd2.ExecuteNonQuery()
            'conexionMysql.Close()


            'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t2)


            '    'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    'e.Graphics.DrawString(idproducto, prFont, Brushes.Black, x, t3)
            '    '----------
            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString("$" & costo, prFont, Brushes.Black, x, t3)
            '    '----------
            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString(cantidad, prFont, Brushes.Black, x + 80, t3)
            '    '-----------

            '    prFont = New Font("Arial", tfuente3, FontStyle.Bold)
            '    e.Graphics.DrawString("$" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, x + 160, t3)
            ''-----------



            'prFont = New Font("Arial", 10, FontStyle.Bold)
            'e.Graphics.DrawString(cantidad & "-- $" & (CDbl(costo) * CDbl(cantidad)), prFont, Brushes.Black, 0, t3)

            '''''''   t1 = t1 + (incremento * 3.2)
            '''''''t2 = t2 + (incremento * 3.2)
            '''''''t3 = t3 + (incremento * 3.2)

            ''''''' Next

            t1 = t1 - (incremento * 2)
            t2 = t2 - (incremento * 2)
            t3 = t3 - (incremento * 2)



            '----------------AQUI SE IMPRIME EL TOTAL A PAGAR
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql14, total, deposito, resto As String
            Sql14 = "select total,deposito,resto from venta where idventa='" & folio & "';"
            Dim cmd14 As New MySqlCommand(Sql14, conexionMysql)
            reader = cmd14.ExecuteReader()
            reader.Read()
            total = reader.GetString(0).ToString()
            deposito = reader.GetString(1).ToString()
            resto = reader.GetString(2).ToString()

            conexionMysql.Close()

            cerrarconexion()


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Total:", prFont, Brushes.Black, x, p3)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & total, prFont, Brushes.Black, x + 150, p4)

            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("Price:", prFont, Brushes.Black, x, p5)
            'prFont = New Font("Arial", tfuente, FontStyle.Bold)
            'e.Graphics.DrawString("$ " & rtxtcostoreparacion.Text, prFont, Brushes.Black, x + 150, p6)


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Anticipo:", prFont, Brushes.Black, x, p5)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & deposito, prFont, Brushes.Black, x + 150, p6)


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Resto:", prFont, Brushes.Black, x, p7)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & resto, prFont, Brushes.Black, x + 150, p8)



            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p9)
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

    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If ctxtidscustomer.Text = "" Or ctxtcustomername.Text = "" Then
            MsgBox("There are empty boxes, check the information", MsgBoxStyle.Information, "MOBI")
        Else





            Try
                cerrarconexion()

                conexionMysql.Open()

                Dim sql2 As String
                sql2 = "delete from customer WHERE idcustomer='" & ctxtidscustomer.Text & "';"
                Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                cmd2.ExecuteNonQuery()

                conexionMysql.Close()
                MsgBox("customer removed", MsgBoxStyle.Information, "MOBI")


                Call climpiar()


                Call cllenadogrilla()

            Catch ex As Exception
                cerrarconexion()

                MsgBox("Wrong information", MsgBoxStyle.Information, "MOBI")


            End Try



        End If
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        If ptxtclaveproducto.Text = "" Then
            MsgBox("There are empty boxes, check the information", MsgBoxStyle.Information, "Sistema")
        Else




            Try
                cerrarconexion()

                conexionMysql.Open()

                Dim sql2 As String
                sql2 = "delete from producto WHERE idproducto='" & ptxtclaveproducto.Text & "';"
                Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                cmd2.ExecuteNonQuery()

                conexionMysql.Close()
                MsgBox("Product removed", MsgBoxStyle.Information, "MOBI")


                Call plimpiar()


                Call pllenadogrilla()

            Catch ex As Exception
                cerrarconexion()
                MsgBox("Wrong information", MsgBoxStyle.Information, "MOBI")

            End Try



        End If

        'End If
    End Sub

    Private Sub Rchpay_CheckedChanged(sender As Object, e As EventArgs) Handles rchpay.CheckedChanged
        'si esta activo, pagamos el total
        If rchpay.Checked = True Then
            rtxtdeposito.Text = rtxttotal.Text
        Else
            rtxtdeposito.Text = 0

        End If

    End Sub

    Private Sub Cbstate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbstate.SelectedIndexChanged
        ' MsgBox("Valor cambiado")
    End Sub

    Private Sub Btncerrarcajamenu_Click(sender As Object, e As EventArgs) Handles btncerrarcajamenu.Click
        cerrarcaja()
    End Sub
    Function cerrarcaja()
        Dim registrar As Boolean
        Dim idestado, idmaximo, contador As Integer

        Try
            'todos los datos son obtenidos con la fecha actual para evitar conflictos
            Dim dia, mes, año, fecha, fechacaja, horacaja As String
            Dim hora2, minuto, segundo, hora As String
            hora2 = Now.Hour()
            minuto = Now.Minute()
            segundo = Now.Second()

            hora = hora2 & ":" & minuto & ":" & segundo

            dia = Date.Now.Day
            mes = Date.Now.Month
            año = Date.Now.Year
            fecha = año & "-" & mes & "-" & dia
            Dim fechahoy As String




            '---------------------------
            'PRIMERO VERIFICAMOS QUE EXISTA UN REGISTRO EN LA TABLA
            '------------------------------

            conexionMysql.Open()
            Dim sql22 As String
            sql22 = "select count(*) from caja;"
            Dim cmd22 As New MySqlCommand(sql22, conexionMysql)
            reader = cmd22.ExecuteReader
            reader.Read()
            contador = reader.GetString(0).ToString()
            conexionMysql.Close()
            '---------------------------

            If contador <= 0 Then
                idestado = 1
                MsgBox("Al parecer tu sistema es nuevo, inicia abriendo una caja!!!", MsgBoxStyle.Information, "CTRL+y")
            Else

                '---------------------------
                'PRIMERO OBTENERMOS EL MAYOR ID
                '------------------------------
                conexionMysql.Open()
                Dim sql2 As String
                sql2 = "select max(idcaja)as maximo from caja;"
                Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                reader = cmd2.ExecuteReader
                reader.Read()
                idmaximo = reader.GetString(0).ToString()
                conexionMysql.Close()
                '---------------------------

                conexionMysql.Open()
                Dim sql25 As String
                sql25 = "select estado from caja where idcaja=" & idmaximo & ";"
                Dim cmd25 As New MySqlCommand(sql25, conexionMysql)
                reader = cmd25.ExecuteReader
                reader.Read()
                idestado = reader.GetString(0).ToString()
                conexionMysql.Close()
                registrar = False
                'MsgBox(idestado, MsgBoxStyle.Exclamation, "CTRL+y")

                'MsgBox("No existe caja abierta", MsgBoxStyle.Exclamation, "CTRL+y")
            End If

        Catch ex As Exception
            registrar = True
            ' idestado = 1
            ' MsgBox("error")
        End Try



        If idestado = 0 Then
            'en caso de que exista una caja abierta, entonces abrimos la ventana para cerrar la caja
            Dim formulario As New FRcerrarcaja
            formulario.ShowDialog()

        End If
    End Function

    Private Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        Dim respuesta As String

        respuesta = MsgBox("¿Estas seguro de reiniciar el Sistema CTRL+y y eliminar toda la información?", MsgBoxStyle.YesNo, "CTRL+y")
        If respuesta = vbYes Then
            '--------------------------------- en caso de que la respuesta sea correcta
            '--------------------------------- se procede a eliminar todo


            cerrarconexion()

            conexionMysql.Open()
            Dim Sql151 As String
            Sql151 = "delete from customer;"
            Dim cmd151 As New MySqlCommand(Sql151, conexionMysql)
            cmd151.ExecuteNonQuery()
            conexionMysql.Close()




            conexionMysql.Open()
            Dim Sql As String
            Sql = "TRUNCATE datos_empresa;"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            cmd.ExecuteNonQuery()
            conexionMysql.Close()




            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "TRUNCATE equipo;"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            cmd2.ExecuteNonQuery()
            conexionMysql.Close()

            conexionMysql.Open()
            Dim Sql3 As String
            Sql3 = "TRUNCATE table impresora;"
            Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
            cmd3.ExecuteNonQuery()
            conexionMysql.Close()

            conexionMysql.Open()
            Dim Sql4 As String
            Sql4 = "TRUNCATE table logo_empresa;"
            Dim cmd4 As New MySqlCommand(Sql4, conexionMysql)
            cmd4.ExecuteNonQuery()
            conexionMysql.Close()

            conexionMysql.Open()
            Dim Sql5 As String
            Sql5 = "TRUNCATE table producto;"
            Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
            cmd5.ExecuteNonQuery()
            conexionMysql.Close()


            conexionMysql.Open()
            Dim Sql6 As String
            Sql6 = "TRUNCATE table seller;"
            Dim cmd6 As New MySqlCommand(Sql6, conexionMysql)
            cmd6.ExecuteNonQuery()
            conexionMysql.Close()



            conexionMysql.Open()
            Dim Sql8 As String
            Sql8 = "TRUNCATE table state_device;"
            Dim cmd8 As New MySqlCommand(Sql8, conexionMysql)
            cmd8.ExecuteNonQuery()
            conexionMysql.Close()

            conexionMysql.Open()
            Dim Sql7 As String
            Sql7 = "delete from venta;"
            Dim cmd7 As New MySqlCommand(Sql7, conexionMysql)
            cmd7.ExecuteNonQuery()
            conexionMysql.Close()


            conexionMysql.Open()
            Dim Sql89 As String
            Sql89 = "delete from venta_ind;"
            Dim cmd89 As New MySqlCommand(Sql89, conexionMysql)
            cmd89.ExecuteNonQuery()
            conexionMysql.Close()

            Try

                cerrarconexion()
                conexionMysql.Open()
                Dim Sql2X As String
                Sql2X = "INSERT INTO `seller` (`name_seller`) VALUES ('SELLER');"
                Dim cmd2X As New MySqlCommand(Sql2X, conexionMysql)
                cmd2X.ExecuteNonQuery()
                conexionMysql.Close()
                MsgBox("Registro guardado", MsgBoxStyle.Information, "CTRL+y")
                cargarseller()
                conftxtseller.Text = ""
                'obtenerfolio()
            Catch ex As Exception

            End Try

            MsgBox("Reset ok", MsgBoxStyle.Information, "MOBI")
        End If

    End Sub

    Private Sub Rcalendario_ValueChanged(sender As Object, e As EventArgs) Handles rcalendario.ValueChanged
        'If activarbusqueda = True Then
        '    actualizarfecha()

        'End If
    End Sub

    Private Sub Button77_Click(sender As Object, e As EventArgs) Handles Button77.Click


        'actualizamos la impresora seleccionada
        ' Try

        cerrarconexion()
        Dim instance As New Printing.PrinterSettings
        Dim impresosaPredt As String = instance.PrinterName
        'MsgBox("LA IMPRESORA A GUARDAR ES:" & impresosaPredt)

        Dim cad1, cad2, cad3, cad4, cad5 As String
        Dim largo As Integer






        txtnombreimpresora.Text = impresosaPredt

        cad1 = txtnombreimpresora.Text
        cad5 = txtnombreimpresora.Text
        'cad2 = txtnombreimpresora.Text
        largo = Len(txtnombreimpresora.Text)
        largo = largo - 1

        'MsgBox(largo)
        cad3 = cad1.Substring(1, largo)
        'MsgBox(cad1)

        cad4 = cad5.Substring(0, 1)
        Dim nuevaruta, n2 As String

        'MsgBox(cad4)
        If cad4 = "\" Then
            MsgBox("Si es una impresora en red")

            n2 = Replace(cad3, "\", "\\")
            nuevaruta = "\\\" & n2

        Else
            nuevaruta = txtnombreimpresora.Text
        End If


        'nuevaruta = Replace(txtnombreimpresora.Text, "\", "\\\")

        'MsgBox(nuevaruta)

        txtnombreimpresora.Text = nuevaruta
        MsgBox(txtnombreimpresora.Text)
        conexionMysql.Open()
        Dim Sql22 As String
        Sql22 = "UPDATE  impresora SET nombre_impresora = '" & nuevaruta & "';"
        Dim cmd22 As New MySqlCommand(Sql22, conexionMysql)
        cmd22.ExecuteNonQuery()
        conexionMysql.Close()
        MsgBox("Printer update", MsgBoxStyle.Information, "MOBI")
        ' Catch ex As Exception

        ' End Try

    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim formulario As New frR1
        formulario.ShowDialog()








        'frR1.Show()

    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        'se actualiza la fecha del cambio del estado del dispositivo
        actualizarfecha()
        seleccionaritem()
    End Sub

    Private Sub Btventasx_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Ptxtcantidad_TextChanged(sender As Object, e As EventArgs) Handles ptxtcantidad.TextChanged

    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        imprimirreparacion()

    End Sub

    Private Sub rtxtnombre_GotFocus(sender As Object, e As EventArgs) Handles rtxtnombre.GotFocus
        rtxtnombre.BackColor = Color.White

    End Sub

    Private Sub rtxtprecio_GotFocus(sender As Object, e As EventArgs) Handles rtxtprecio.GotFocus
        rtxtprecio.BackColor = Color.White

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtbusquedafolioventa.TextChanged

    End Sub

    Private Sub rtxtcantidad_GotFocus(sender As Object, e As EventArgs) Handles rtxtcantidad.GotFocus
        rtxtcantidad.BackColor = Color.White

    End Sub

    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        realizarbusquedaventa()
    End Sub
    Function realizarbusquedaventa()

        If txtbusquedafolioventa.Text = "" Then
            statusbusquedaventa = False
        Else
            busquedaventa()
            statusbusquedaventa = True
            slbcustomer.Visible = False

        End If


    End Function
    Function cancelartodoventa()
        txtname.Text = ""
        txtaddress.Text = ""
        txtcity.Text = ""
        txtclaveproducto.Text = ""
        txtnombreproducto.Text = ""
        txtprice.Text = ""
        txtpiece.Text = ""
        stxttotal.Text = ""
        txtstate.Text = ""
        txttelephone.Text = ""
        stxttotalfinal.Text = ""
        txtbusquedafolioventa.Text = ""
        statusbusquedaventa = False

        sgrilla.Rows.Clear()
    End Function

    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        cancelartodoventa()

    End Sub

    Function busquedaventa()
        If txtbusquedafolioventa.Text = "" Then
            ' MsgBox("No hay folios que buscar", MsgBoxStyle.Information, "MOBI")
            cancelartodoventa() 'pendiente de cancelar todo
            ' MsgBox("cancel")

        Else
            'MsgBox("cancel")

            'rcbitems.Items.Clear()
            sgrilla.Rows.Clear()
            Dim tipoventa As Integer
            'primero verifico si se trata de un servicio de reparación
            Try
                conexionMysql.Open()
                ' Dim idcustomer, idequipo, idseller As Integer
                Dim Sqlaa As String
                Sqlaa = "select tipoventa from venta where idventa='" & txtbusquedafolioventa.Text & "';"
                Dim cmdaa As New MySqlCommand(Sqlaa, conexionMysql)
                reader = cmdaa.ExecuteReader()
                reader.Read()
                tipoventa = reader.GetString(0).ToString()
                'reader.Close()
                cerrarconexion()
            Catch ex As Exception
                tipoventa = 0
                cerrarconexion()
            End Try


            If tipoventa <> 1 Then
                ' MsgBox("El folio corresponde a una venta directa", MsgBoxStyle.Information, "MOBI")

                cancelartodoventa()
                ' MsgBox("cancel")

            Else


                ' MsgBox("cancel 2")




                conexionMysql.Open()
                Dim idequipobus, idcus, idsellerbus, fechaactualizar, fechaventa As String
                Dim Sql As String
                Sql = "select * from venta where idventa='" & txtbusquedafolioventa.Text & "';"
                Dim cmd As New MySqlCommand(Sql, conexionMysql)
                reader = cmd.ExecuteReader()
                reader.Read()
                idcus = reader.GetString(1).ToString()
                idsellerbus = reader.GetString(2).ToString()
                stxttotalfinal.Text = reader.GetString(3).ToString()
                fechaventa = reader.GetString(4).ToString()
                'fechaactualizar = reader.GetString(5).ToString()

                'rtxtbalancetotal.Text = 
                'rtxtdeposito.Text = reader.GetString(6).ToString()
                'rtxtresto.Text = reader.GetString(7).ToString()

                'rtxttotal.Text = reader.GetString(4).ToString()
                'rcalendario.Text = reader.GetString(6).ToString()
                'reader.Close()
                ' MsgBox(idcustomer)
                'MsgBox(idequipo)
                'MsgBox(idseller)

                cerrarconexion()



                ''----------------------comprobar si no tienen fecha, actualizamos la fecha----------------
                'Dim fecha As String
                'Try

                '    conexionMysql.Open()
                '    Dim Sqla2 As String
                '    Sqla2 = "select datedelivery from equipo where idventa='" & rtxtbusquedafolio.Text & "';"
                '    Dim cmda2 As New MySqlCommand(Sqla2, conexionMysql)
                '    reader = cmda2.ExecuteReader()
                '    reader.Read()
                '    fecha = reader.GetString(0).ToString()
                '    cerrarconexion()
                'Catch ex As Exception
                '    cerrarconexion()
                '    fecha = ""

                'End Try



                'eb caso de que se haga una busqueda con un folio que no tenga el campo deliverydate, entonces lo actualizamos

                'If fecha = "" Then

                '    Dim fechaactualizarfinal As String
                '    fechaactualizarfinal = rcalendario.Value.ToString("yyyy/MM/dd")

                '    'si la fecha esta vacia, actualizo el dato, en caso contrario, no hago nada.
                '    cerrarconexion()
                '    conexionMysql.Open()
                '    Dim Sql2 As String
                '    Sql2 = "update equipo set datedelivery='" & fechaactualizarfinal & "' where idventa='" & rtxtbusquedafolio.Text & "';"
                '    'Sql2 = "UPDATE `mobi`.`customer` SET `name` = '" & rtxtcustomername.Text & "', `address` = '" & rtxtaddress.Text & "', `state` = '" & rtxtstate.Text & "', `city` = '" & rtxtcity.Text & "', `telephone` = '" & rtxttelephone.Text & "', `email` = '" & rtxtemail.Text & "' WHERE (`idcustomer` = '" & customeract & "');"
                '    'Sql2 = "update customer set name='" & rtxtcustomername.Text & "', address='" & rtxtaddress.Text & "', state='" & rtxtcity.Text & "', city='" & rtxtstate.Text & "', telephone='" & rtxttelephone.Text & "', email='" & rtxtemail.Text & "' where idcustomer='" & idcustomerbus & "';"
                '    Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                '    cmd2.ExecuteNonQuery()
                '    conexionMysql.Close()
                '    MsgBox("Date update", MsgBoxStyle.Information, "MOBI")
                'End If

                ''------------------------------------------------------------------------------------------




                'Try

                '    conexionMysql.Open()
                '    'Dim idequipobus, idcus, idsellerbus As String
                '    Dim Sqla As String
                '    Sqla = "select * from venta where idventa='" & txtbusquedafolioventa.Text & "';"
                '    Dim cmda As New MySqlCommand(Sqla, conexionMysql)
                '    reader = cmda.ExecuteReader()
                '    reader.Read()
                '    'idcus = reader.GetString(1).ToString()
                '    'idsellerbus = reader.GetString(2).ToString()
                '    'rtxttotal.Text = reader.GetString(3).ToString()
                '    'rcalendario.Text = reader.GetString(5).ToString()
                '    ''rtxtbalancetotal.Text = 
                '    'rtxtdeposito.Text = reader.GetString(6).ToString()
                '    rtxtresto.Text = reader.GetString(7).ToString()

                '    'rtxttotal.Text = reader.GetString(4).ToString()
                '    'rcalendario.Text = reader.GetString(6).ToString()
                '    'reader.Close()
                '    ' MsgBox(idcustomer)
                '    'MsgBox(idequipo)
                '    'MsgBox(idseller)
                '    cerrarconexion()
                'Catch ex As Exception

                'End Try

                '                MsgBox(rtxttotal.Text)

                '----------------------------------
                cerrarconexion()


                '----------------------------------------------------------------------------------------------------------------------
                ' Try
                ' MsgBox(rtxttotal.Text)
                'cargamos los datos necesarios
                conexionMysql.Open()
                'Dim idcustomer, idequipo, idseller As Integer
                Dim Sql22 As String
                Sql22 = "select * from customer where idcustomer=" & idcus & ";"
                Dim cmd22 As New MySqlCommand(Sql22, conexionMysql)
                reader = cmd22.ExecuteReader()
                reader.Read()

                Try

                    txtaddress.Text = reader.GetString(2).ToString()
                Catch ex As Exception

                End Try
                Try

                    txtstate.Text = reader.GetString(3).ToString()
                Catch ex As Exception

                End Try
                Try

                    txtcity.Text = reader.GetString(4).ToString()
                Catch ex As Exception

                End Try
                Try

                    txttelephone.Text = reader.GetString(5).ToString()
                Catch ex As Exception

                End Try
                Try

                    txtname.Text = reader.GetString(1).ToString()
                Catch ex As Exception

                End Try



                Try

                    stxtemail.Text = reader.GetString(6).ToString()
                Catch ex As Exception

                End Try
                '----------------------------------------------------------------------------------------------------
                'reader.Close()
                cerrarconexion()

                ' MsgBox("1")

                rlbcustomer.Visible = False
                Dim seller As String

                '-------------------------
                conexionMysql.Open()
                ' Dim idcustomer, idequipo, idseller As Integer
                Dim Sqlx2 As String
                Sqlx2 = "select name_seller from seller where idseller='" & idsellerbus & "';"
                Dim cmdx2 As New MySqlCommand(Sqlx2, conexionMysql)
                reader = cmdx2.ExecuteReader()
                reader.Read()
                seller = reader.GetString(0).ToString()
                'reader.Close()
                cerrarconexion()
                'MsgBox("2")
                'MsgBox(seller)

                cbseller.Text = seller

                '----------------------

                conexionMysql.Open()
                ' Dim idcustomer, idequipo, idseller As Integer
                Dim Sqlx3 As String
                Sqlx3 = "select count(*) as contador from venta_ind where idventa=" & txtbusquedafolioventa.Text & ";"
                Dim cmdx3 As New MySqlCommand(Sqlx3, conexionMysql)
                reader = cmdx3.ExecuteReader()
                reader.Read()
                Dim cantidad As Integer = reader.GetString(0).ToString()
                ' reader.Close()
                cerrarconexion()
                'MsgBox("2")
                'MsgBox(cantidad)

                ' MsgBox(cantidad)
                '------------------------

                Dim idequipo, equipo, modelo, imei, password, status, problema, nota, idcustomer2, reparacion, partes, idventa As String
                Dim ii As Integer
                '-------------------------------------- buscamos los valores de la grilla y lo mandamos a eso
                'reader.Close()


                Try

                    cerrarconexion()
                    conexionMysql.Open()
                    Dim idproducto, actividad As String
                    Dim costo, piezas, total As Double
                    Dim Sql3 As String
                    Sql3 = "select * from venta_ind where idventa='" & txtbusquedafolioventa.Text & "';"
                    Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
                    reader = cmd3.ExecuteReader()

                    For ii = 0 To cantidad - 1
                        'MsgBox(ii)
                        reader.Read()


                        actividad = reader.GetString(1).ToString()
                        cantidad = reader.GetString(2).ToString()
                        costo = reader.GetString(3).ToString()
                        password = reader.GetString(4).ToString()
                        'status = reader.GetString(5).ToString()
                        idproducto = reader.GetString(5).ToString()


                        total = CDbl(cantidad) * CDbl(costo)
                        sgrilla.Rows.Add(idproducto, actividad, costo, cantidad, total)


                        'agregamos la fecha del status de la actualizacion
                        'calendariostatus.Value = New Date(1000, 12, 23)



                        ''ocultamos el combotemporal ymostramos el combo de busqueda
                        'rcbitems.Visible = True
                        'rcbitemtemporal.Visible = False
                    Next


                Catch ex As Exception
                    btninconsistencia.Visible = True
                    btninconsistencia.Text = ex.Message
                End Try


                ''----------------------------------
                'conexionMysql.Open()
                'Dim equipoultimo As String
                'Dim Sql34 As String
                'Sql34 = "select * from equipo where idventa='" & rtxtbusquedafolio.Text & "';"
                'Dim cmd34 As New MySqlCommand(Sql34, conexionMysql)
                'reader = cmd34.ExecuteReader()

                'For ii = 0 To cantidad - 1
                '    'MsgBox(ii)
                '    reader.Read()

                '    rcbitemtemporal.Items.Add(equipo)
                '    'rcbitemtemporal.SelectedIndex = 0

                '    ''ocultamos el combotemporal ymostramos el combo de busqueda
                '    'rcbitems.Visible = True
                '    'rcbitemtemporal.Visible = False
                'Next




                '----------------------------------
                'cerrarconexion()

                'conexionMysql.Open()
                'Dim Sql2 As String
                'Sql2 = "select * from tipo_pago;"
                'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                'reader = cmd2.ExecuteReader()

                'For i = 1 To cantidadproveedor

                '    reader.Read()

                '    cbformadepago.Items.Add(reader.GetString(1).ToString())
                '    cbformadepagoservicios.Items.Add(reader.GetString(1).ToString())
                'Next
                '---------------------------------




                reader.Close()
                conexionMysql.Close()
                cerrarconexion()
                '----------------

                'conexionMysql.Open()
                ''Dim idcustomer, idequipo, idseller As Integer
                'Dim Sql23 As String
                'Sql23 = "select * from equipo where idequipo='" & rtxtbusquedafolio.Text & "';"
                'Dim cmd23 As New MySqlCommand(Sql23, conexionMysql)
                'reader = cmd23.ExecuteReader()
                'reader.Read()
                'rtxtequipo.Text = reader.GetString(1).ToString()
                'rtxtmodelo.Text = reader.GetString(2).ToString()
                'rtxtimei.Text = reader.GetString(3).ToString()
                'rtxtpassword.Text = reader.GetString(4).ToString()
                'cbstate.Text = reader.GetString(5).ToString()
                'rtxtproblem.Text = reader.GetString(6).ToString()
                'rtxtnote.Text = reader.GetString(7).ToString()

                'reader.Close()
                'cerrarconexion()

                'conexionMysql.Open()
                ''Dim idcustomer, idequipo, idseller As Integer
                'Dim Sql24 As String
                'Sql24 = "select name_seller from seller  where idseller='" & idseller & "';"
                'Dim cmd24 As New MySqlCommand(Sql24, conexionMysql)
                'reader = cmd24.ExecuteReader()
                'reader.Read()

                'Dim seller As String = reader.GetString(0).ToString()
                'MsgBox(seller)

                'reader.Close()
                'cerrarconexion()
                ' Catch ex As Exception

                'End Try

                '-------------------------------

                '-------------------------------------- buscamos los valores de la grilla y lo mandamos a eso
                'cerrarconexion()
                'conexionMysql.Open()
                'Dim contador As Integer
                'Dim Sql2 As String
                'Sql2 = "select count(*) as contador from venta_ind where idventa='" & rtxtbusquedafolio.Text & "';"
                'Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
                'reader = cmd2.ExecuteReader()
                'reader.Read()
                'contador = reader.GetString(0).ToString()
                'reader.Close()
                'conexionMysql.Close()
                'cerrarconexion()
                ''---------------------------------------------
                'If contador > 0 Then


                '    '-------------------------------------- buscamos los valores de la grilla y lo mandamos a eso
                '    cerrarconexion()
                '    conexionMysql.Open()
                '    'Dim clave, costo, cantidad, actividad As String
                '    Dim Sql33 As String
                '    Sql33 = "select * from venta_ind where idventa='" & rtxtbusquedafolio.Text & "';"
                '    Dim cmd33 As New MySqlCommand(Sql33, conexionMysql)
                '    reader = cmd33.ExecuteReader()

                '    For i = 0 To contador - 1

                '        reader.Read()


                '        clave = reader.GetString(5).ToString()
                '        actividad = reader.GetString(1).ToString()
                '        cantidad = reader.GetString(3).ToString()
                '        costo = reader.GetString(2).ToString()


                '        rgrilla.Rows.Add(clave, actividad, cantidad, costo, CDbl(cantidad) * CDbl(costo))


                '    Next




                '    reader.Close()
                '    conexionMysql.Close()
                '    cerrarconexion()
                '    '---------------------------------------------

                'End If


            End If
        End If



    End Function

    Private Sub Sgrilla_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles sgrilla.CellContentClick

    End Sub

    Private Sub Btnabrircajamenu_Click(sender As Object, e As EventArgs) Handles btnabrircajamenu.Click
        abrircaja()

    End Sub
    Function abrircaja()

        'primero comprobamos si existe caja abierta.
        '------------------------------------------------
        Dim cantidad As Integer

        Try
            'verificamos que exista al menos 1 registro, en caso de que exista 0, indicamos que el valor es 0;
            conexionMysql.Open()
            Dim sql22 As String
            sql22 = "select count(idcaja) from caja;"
            Dim cmd22 As New MySqlCommand(sql22, conexionMysql)
            reader = cmd22.ExecuteReader
            reader.Read()
            cantidad = reader.GetString(0).ToString()
            conexionMysql.Close()
        Catch ex As Exception
            cantidad = 0
        End Try

        'si la cantidad es cero, entonces, significa que si puede abrir una caja, porque no hay nada aun.
        If cantidad = 0 Then


        Else


            Try

                conexionMysql.Open()
                Dim sql2 As String
                sql2 = "select count(idcaja) from caja where estado=0;"
                Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                reader = cmd2.ExecuteReader
                reader.Read()
                cantidad = reader.GetString(0).ToString()
                conexionMysql.Close()
            Catch ex As Exception
                cantidad = 1
            End Try
        End If


        If cantidad = 0 Then


            Dim formulario As New FRabrircaja

            formulario.ShowDialog()
        Else
            Dim respuesta As String
            respuesta = MsgBox("Existen cajas abiertas sin cerrar, ¿deseas forzas cierre?, todo se pondrá en Ceros", MsgBoxStyle.YesNo, "CTRL+y")


            If respuesta = vbYes Then

                cerrarconexion()

                conexionMysql.Open()
                'actualizo el dato
                Dim Sql5 As String
                Sql5 = "UPDATE `caja` SET `estado` = '1';"
                Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
                cmd5.ExecuteNonQuery()
                conexionMysql.Close()
                MsgBox("Cajas cerradas", MsgBoxStyle.Information, "CTR+y")
                Dim formulario As New FRabrircaja
                formulario.ShowDialog()
                '---------------------------------
            End If




        End If

    End Function

    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        'reimprimir reparacion
        reimprimirreparacion()
    End Sub


    Function statuscambio()

        ' Dim value As String = Convert.ToString(segrilla.CurrentRow.Cells("Status").Value)
        ' MsgBox(value)

    End Function
    Private Sub pgrilla_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles pgrilla.CellDoubleClick
        Dim Variable As String = pgrilla.Item(0, pgrilla.CurrentRow.Index).Value
        'MsgBox(Variable)
        ptxtclaveproducto.Text = Variable
        'grilla2p.Visible = False
        'grillap.Visible = True
        'rbclavep.Checked = True
    End Sub

    Private Sub lblistaproducto_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lblistaproducto.MouseDoubleClick

        '    End Sub
        txtnombreproducto.Text = lblistaproducto.Text
        'rtxtclaveproducto.Enabled = False
        'cargamos los datos del producto
        cargardatosproductoventa()
        lblistaproducto.Visible = False
        txtpiece.Focus()

    End Sub

    Private Sub Segrilla_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles segrilla.CellContentClick

    End Sub

    Function cargardatosproductoventa()
        conexionMysql.Open()
        Dim Sql As String
        Sql = "select * from producto where name='" & txtnombreproducto.Text & "';"
        Dim cmd As New MySqlCommand(Sql, conexionMysql)
        reader = cmd.ExecuteReader()
        reader.Read()
        txtprice.Text = reader.GetString(4).ToString()
        txtnombreproducto.Text = reader.GetString(1).ToString()
        ' rtxtcantidad.Text = reader.GetString(2).ToString()
        txtclaveproducto.Text = reader.GetString(0).ToString()
        'ptxtcosto.Text = reader.GetString(3).ToString()

        'txtpreciomayoreop.Text = reader.GetString(5).ToString()

        reader.Close()
    End Function

    Function actualizar2021()



        Try

            cerrarconexion()
            conexionMysql.Open()
            Dim Sql22 As String
            Sql22 = "ALTER TABLE `mobi`.`datos_empresa` 
ADD COLUMN `traduccion` INT NULL AFTER `saludo5`;"
            Dim cmd22 As New MySqlCommand(Sql22, conexionMysql)
            cmd22.ExecuteNonQuery()
            conexionMysql.Close()
            cerrarconexion()
            'actualizar la fecha de entrega de la venta.
            '---------------------------------------------------------------------
        Catch ex As Exception
            cerrarconexion()
        End Try


    End Function

    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        Dim id, valor As Integer
        Try

            conexionMysql.Open()
            Dim Sql111 As String
            Sql111 = "select traduccion from datos_empresa;"
            Dim cmd111 As New MySqlCommand(Sql111, conexionMysql)
            reader = cmd111.ExecuteReader()
            reader.Read()

            id = reader.GetString(0).ToString
        Catch ex As Exception
            cerrarconexion()
            id = 0
        End Try


        If id = 0 Then
            valor = 1
        Else
            valor = 0
        End If
        cerrarconexion()
        'If id = 0 Then
        conexionMysql.Open()
        Dim Sql22 As String
        Sql22 = "UPDATE  datos_empresa SET traduccion = '" & valor & "';"
        Dim cmd22 As New MySqlCommand(Sql22, conexionMysql)
        cmd22.ExecuteNonQuery()
        conexionMysql.Close()
        MsgBox("Cambio realizado, se cerrara el sistema", MsgBoxStyle.Information, "MOBI")
        conexionMysql.Close()
        cerrarconexion()
        End





        'actualizar la fecha de entrega de la venta.
        '---------------------------------------------------------------------
        ' End If


    End Sub
    Function traduccion()

        Button1.Text = "Inicio"
        Button2.Text = "Ventas"
        Button67.Text = "Reparaciones"
        Button14.Text = "Productos"
        Button8.Text = "Busqueda"
        Button38.Text = "Clientes"
        Button12.Text = "Configuración"
        Label11.Text = "Id"
        Label10.Text = "Descripción"
        GroupBox3.Text = "Producto"
        GroupBox1.Text = "Cliente"
        Label1.Text = "Nombre"
        Label2.Text = "Dirección"
        Label6.Text = "Ciudad"
        Label5.Text = "Estado"
        Label3.Text = "Telefono"
        Label4.Text = "Correo"
        Button19.Text = "Agregar"
        Label15.Text = "Técnico"
        Label7.Text = "Folio"
        GroupBox22.Text = "Número de venta"
        Button5.Text = "Vender"
        Label66.Text = "Folio"
        GroupBox26.Text = "Busqueda"
        Button44.Text = "Cancelar"
        Button45.Text = "Reimpresión"
        Label9.Text = "Precio"
        Label8.Text = "Cantidad"
        '---
        GroupBox7.Text = "Cliente"
        Label21.Text = "Nombre"
        Label18.Text = "Dirección"
        Label19.Text = "Ciudad"
        Label20.Text = "Estado"
        Label17.Text = "Telefono"
        Label48.Text = "E-mail"
        GroupBox8.Text = "Dispositivo"
        Label26.Text = "Nombre Dispositivo"
        Label23.Text = "Modelo"
        Label25.Text = "IMEI"
        Label24.Text = "Contraseña"
        Label33.Text = "Estado"
        Label46.Text = "Técnico"
        Label22.Text = "Descripción"
        Label32.Text = "Notas"
        Label39.Text = "Fecha de entrega"
        Label64.Text = "Fecha cambio de estado"

        GroupBox21.Text = "Dispositivos"
        GroupBox16.Text = "Número de venta"
        Label44.Text = "Id del producto"
        Label31.Text = "Pieza"






        Label29.Text = "Precio"
        Label43.Text = "Cantidad"
        Button17.Text = "Agregar"
        Label28.Text = "Precio reparación"
        Label60.Text = "Total en piezas"
        Label30.Text = "Anticipo"
        Label45.Text = "Restante"
        Button13.Text = "Registrar"
        Button22.Text = "Actualizar"
        Button23.Text = "Limpiar"
        Button29.Text = "Agregar dispositivo"
        btnreimpresion.Text = "Reimpresión"


        '----
        GroupBox19.Text = "Reparaciones"
        Label53.Text = "Estados"
        Label56.Text = "Técnico"
        Label55.Text = "Fecha de entrega"
        Label54.Text = "Fecha de venta"
        GroupBox20.Text = "Datos generales"
        Label50.Text = "Nombre de cliente"
        Label52.Text = "Número telefonico"
        Label57.Text = "Modelo equipo"
        Label58.Text = "ID Venta"
        Button32.Text = "Imprimir"
        Button26.Text = "Limpiar"

        '---

        Button11.Text = "Salir"
        '---
        GroupBox31.Text = "Datos de la empresa"
        Label99.Text = "Empresa"
        Label107.Text = "Subnombre"
        Label102.Text = "Calle"
        Button83.Text = "Guardar imagen"
        Button84.Text = "Abrir imagen"
        Button87.Text = "Eliminar imagen"
        GroupBox38.Text = "Configuración del sistema"
        Button52.Text = "Respaldar Base de datos"
        Button65.Text = "Reiniciar sistema"
        Button88.Text = "Eliminar Base de datos"
        GroupBox33.Text = "Configuración de impresora"
        Label109.Text = "Impresora"
        chticket.Text = "Imprimir ticket"
        GroupBox6.Text = "Configurar"
        Label13.Text = "Agregar técnico"
        Button3.Text = "Agregar"
        Label4.Text = "Tecnicos"
        Button4.Text = "Eliminar"
        GroupBox25.Text = "Configuración General"
        Button75.Text = "Actualizar Información"




        'PRODUCTO
        GroupBox13.Text = "Productos"
        Label42.Text = "Id producto"
        Label41.Text = "Nombre"
        Label37.Text = "Cantidad"
        Label40.Text = "Costo"
        Label38.Text = "Precio"
        Button15.Text = "Agregar"
        Button16.Text = "Actualizar"
        Button40.Text = "Eliminar"
        Button24.Text = "Imprimir inventario"

        'clientes
        GroupBox24.Text = "Clientes"
        Label62.Text = "Id cliente"
        Label70.Text = "Nombre"
        a.Text = "Dirección"
        b.Text = "Estado"
        c.Text = "Ciudad"
        d.Text = "Teléfono"
        f.Text = "E-mail"
        Button33.Text = "Agregar"
        Button34.Text = "Actualizar"
        Button39.Text = "Eliminar"
        Button36.Text = "Imprimir"
        'adicionales
        btncerrarcajamenu.Text = "Cerrar Caja"
        btnabrircajamenu.Text = "Abril Caja"



    End Function

    Private Sub GroupBox8_Enter(sender As Object, e As EventArgs) Handles GroupBox8.Enter

    End Sub

    Private Sub rtxtequipo_TextChanged(sender As Object, e As EventArgs) Handles rtxtequipo.TextChanged

    End Sub

    Private Sub rtxtcustomername_KeyDown(sender As Object, e As KeyEventArgs) Handles rtxtcustomername.KeyDown
        If e.KeyCode = Keys.Escape Then
            rlbcustomer.Visible = False
        End If
    End Sub

    Private Sub ssgrilla_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles ssgrilla.CellDoubleClick

        Dim Variable As String = ssgrilla.Item(0, ssgrilla.CurrentRow.Index).Value
        'MsgBox(Variable)
        TabControl1.SelectedIndex = 2
        rtxtbusquedafolio.Text = Variable
        cargarseller()
        realizarbusqueda()
        realizarbusqueda()
        'rtxtbusquedafolio.Visible = False
        rgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        'rcbitems.Visible = False
        'rcbitemtemporal.Visible = True

        cargarlogoticket()
        'cbstate.SelectedIndex = 0
        'rcbseller.SelectedIndex = 0

        'obtenerfolio()
        'rlbcustomer.Visible = False
        'lblistarespuestos.Visible = False

        'TabControl1.SelectedIndex = 2
        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.DimGray
        Button14.BackColor = Color.FromArgb(27, 38, 44)

        'lblistarespuestos.Visible = False
        'comprobarintegridadreparacion()



    End Sub

    Private Sub cgrilla_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles cgrilla.CellDoubleClick

    End Sub

    Private Sub cgrilla_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles cgrilla.CellClick
        Dim Variable As String = cgrilla.Item(0, cgrilla.CurrentRow.Index).Value
        ' MsgBox(Variable)
        ctxtidscustomer.Text = Variable
    End Sub
    Function actualizarfecha()
        'si la busqueda esta en curso, que actualize dependiendo del producto, el folio.

        'obtengo la fecha actual, que será la fecha en que se actualice el dato
        cerrarconexion()

        Dim dia, d2, d3, d4, d5, mes, año, total As Integer
        Dim fecha, fechaatras As String
        dia = Date.Now.Day
        mes = Date.Now.Month
        año = Date.Now.Year
        fecha = año & "-" & mes & "-" & dia

        If activarbusqueda = True Then
            conexionMysql.Open()
            Dim Sql22 As String
            Sql22 = "UPDATE  equipo SET status = '" & cbstate.Text & "', datedelivery='" & fecha & "' where equipo='" & rcbitemtemporal.Text & "' and idequipo='" & cb1.Text & "' and idventa='" & rtxtbusquedafolio.Text & "' ;"
            Dim cmd22 As New MySqlCommand(Sql22, conexionMysql)
            cmd22.ExecuteNonQuery()
            conexionMysql.Close()
            MsgBox("Status update", MsgBoxStyle.Information, "MOBI")
            conexionMysql.Close()
            cerrarconexion()

            rtxtdatestatus.Text = fecha
            'actualizar la fecha de entrega de la venta.
            '---------------------------------------------------------------------
        End If
    End Function

    Private Sub Btninconsistencia_Click(sender As Object, e As EventArgs) Handles btninconsistencia.Click

    End Sub

    Private Sub cbstate_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbstate.SelectedValueChanged
        '    If activarbusqueda = True Then
        'si hay una busqueda en curso entonces se actualiza la fecha, en caso contrario no actualiza nada
        'actualizarfecha()
        'se actauliza la fecha
        '  MsgBox("Fecha actualizada......")
        '  End If

    End Sub



    Private Sub cbstate_MouseClick(sender As Object, e As MouseEventArgs) Handles cbstate.MouseClick
        ' MsgBox("mouse click")
    End Sub

    Private Sub ptxtcantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ptxtcantidad.KeyPress
        If InStr(1, "0123456789.," & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub Button67_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Button67.KeyPress
        If InStr(1, "0123456789.," & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub ptxtprecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ptxtprecio.KeyPress
        If InStr(1, "0123456789.," & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbusquedafolioventa.KeyPress
        If InStr(1, "0123456789.," & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""


        End If
    End Sub

    Private Sub txtbusquedafolioventa_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbusquedafolioventa.KeyDown
        If e.KeyCode = Keys.Enter Then
            realizarbusquedaventa()
        End If

    End Sub






    Private Sub segrilla_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles segrilla.CellEndEdit
        Dim value As String = Convert.ToString(segrilla.CurrentRow.Cells("Status").Value)
        Dim idvalue As String = Convert.ToString(segrilla.CurrentRow.Cells("equipoID").Value)
        'MsgBox(value)
        'MsgBox(idvalue)
        'obtener fecha y hora

        Dim dia, mes, año, fecha, fechaclave As String
        hora2 = Now.Hour()
        minuto = Now.Minute()
        segundo = Now.Second()

        hora = hora2 & ":" & minuto & ":" & segundo

        dia = Date.Now.Day
        mes = Date.Now.Month
        año = Date.Now.Year
        fecha = año & "-" & mes & "-" & dia

        fechaclave = año & mes & dia & hora2 & minuto & segundo

        'despues de obtener los valores, los actualizamos.

        cerrarconexion()
        conexionMysql.Open()
        Try

            Dim Sql36 As String
            Sql36 = "update equipo set status='" & value & "',datedelivery='" & fecha & "' where idequipo=" & idvalue & ";"
            Dim cmd36 As New MySqlCommand(Sql36, conexionMysql)
            cmd36.ExecuteNonQuery()
            conexionMysql.Close()
            MsgBox("Update status", MsgBoxStyle.Information, "MOBI")
        Catch ex As Exception

        End Try
        cerrarconexion()





    End Sub

    Private Sub segrilla_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles segrilla.CellDoubleClick

        Dim Variable As String = segrilla.Item(0, segrilla.CurrentRow.Index).Value
        'MsgBox(Variable)
        TabControl1.SelectedIndex = 2
        rtxtbusquedafolio.Text = Variable
        cargarseller()
        realizarbusqueda()
        realizarbusqueda()
        'rtxtbusquedafolio.Visible = False
        rgrilla.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue
        'rcbitems.Visible = False
        'rcbitemtemporal.Visible = True

        cargarlogoticket()
        'cbstate.SelectedIndex = 0
        'rcbseller.SelectedIndex = 0

        'obtenerfolio()
        'rlbcustomer.Visible = False
        'lblistarespuestos.Visible = False

        'TabControl1.SelectedIndex = 2
        Button1.BackColor = Color.FromArgb(27, 38, 44)
        Button2.BackColor = Color.FromArgb(27, 38, 44)
        Button8.BackColor = Color.FromArgb(27, 38, 44)
        Button12.BackColor = Color.FromArgb(27, 38, 44)
        Button67.BackColor = Color.DimGray
        Button14.BackColor = Color.FromArgb(27, 38, 44)

        'lblistarespuestos.Visible = False
        'comprobarintegridadreparacion()



    End Sub

    Private Sub rtxtequipo_GotFocus(sender As Object, e As EventArgs) Handles rtxtequipo.GotFocus
        rtxtequipo.BackColor = Color.White
    End Sub

    Private Sub rtxtmodelo_GotFocus(sender As Object, e As EventArgs) Handles rtxtmodelo.GotFocus
        rtxtmodelo.BackColor = Color.White
    End Sub

    Private Sub rtxtcostoreparacion_GotFocus(sender As Object, e As EventArgs) Handles rtxtcostoreparacion.GotFocus
        rtxtcostoreparacion.BackColor = Color.White
    End Sub
End Class