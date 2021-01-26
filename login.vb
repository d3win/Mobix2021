Imports MySql.Data.MySqlClient
'librerias para poder importar y exportar una BD de Mysql
Imports System.IO.StreamWriter
Imports System.IO
Imports System.ComponentModel
Imports System.Text
Public Class login

    Public opd As New OpenFileDialog
    Public carpeta As New FolderBrowserDialog

    Private Sub login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'caconexion.conexionok()
        'MsgBox(conexionMysql.State, MsgBoxStyle.Information)
        'If respuesta = False Then
        '    Me.Finalize()
        'End If
        cargarlogoticket()

    End Sub
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
            PictureBox1.Image = Image.FromFile(rutaImagen)
            PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
            PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
            '  btventas.Image = Image.FromFile(rutaImagen)
        Catch ex As Exception
            cerrarconexion()


        End Try



    End Function
    Function creararchivo()
        ' Create a string array with the lines of text
        Try


            Dim IP, port As String

            IP = InputBox("Ingresa la ip del servidor de la BD")
            port = InputBox("Ingresa el puerto del servidor de la BD")


            Dim lines() As String = {IP, port}
            ' Dim lines2() As String = {port}

            ' Set a variable to the My Documents path.
            Dim mydocpath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            ' Write the string array to a new file named "WriteLines.txt".
            Using outputFile As New StreamWriter(mydocpath & Convert.ToString("\SISTEMANOBORRAR.txt"))
                For Each line As String In lines
                    outputFile.WriteLine(line)
                Next
            End Using
        Catch ex As Exception
            MsgBox("Error al generar el archivo .txt", MsgBoxStyle.Information, "CTRL+y")
        End Try


    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conectaralsistema()

    End Sub
    Function conectaralsistema()
        Dim respuestafinal As Boolean
        respuestafinal = caconexion.conexionok()
        Dim respuesta As String

        'MsgBox(respuestafinal)

        If respuestafinal = False Then
            'Me.Finalize()
            respuesta = MsgBox("No se ha establecido una conexion a la BD, posiblemente no tenga la BD, ¿Deseas importarla?", MsgBoxStyle.YesNo, "Sistema")
            'en caso de que no se conecte, vamos a pedirle que selecciona la BD de respaldo para tenerla en mysql.

            If respuesta = vbYes Then

                importarBD()
                'creararchivo()
            Else
                End

            End If



        Else
            frmindex.Show()
            'Form1.Show()

            obtenertipo_usuario()


            Me.Hide()
            Me.Finalize()
        End If
    End Function
    Function importarBD2()
        '   Dim myProcess As New Process()
        '' Process.Start(MySql.exe - -defaults - File = "c:\users\dw\appdata\local\temp\tmp6rnn2n.cnf" - -protocol = tcp - -host = localhost - -user = root - -port = 3306 - -default-character-set=utf8 --comments  < "C:\\Users\\Dw\\Documents\\dumps\\Dump20190529.sql")
        'Dim abrir As String
        'abrir.DefaultExt = "sql"
        'Dim pathmysql As String
        'Dim comando As String
        'Dim arg As String
        'abrir.Filter = "File MYSQL (*.sql)|*.sql"
        'pathmysql = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\MySQL AB\MYSQL Server 5.1", "Location", 0)
        'If pathmysql = Nothing Then
        '    MsgBox("No se encontro en tu equipo Mysql, escoge la carpeta donde esta ubicado")
        '    carpeta.ShowDialog()
        '    pathmysql = carpeta.SelectedPath
        'End If

        'If abrir.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    'Try

        '    comando = pathmysql & "\bin\mysql.exe"
        '    comando.Replace("\\", "\")
        '    arg = " --user=root --password=root --host=localhost --database bddigital < " & Chr(34) & abrir.FileName & Chr(34)
        '    Dim proceso As New Process
        '    proceso.StartInfo.FileName = "cmd.exe"
        '    proceso.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        '    proceso.StartInfo.UseShellExecute = False
        '    proceso.StartInfo.RedirectStandardOutput = True
        '    proceso.StartInfo.RedirectStandardInput = True
        '    proceso.StartInfo.CreateNoWindow = True
        '    proceso.Start()
        '    Dim escribeconsola As StreamWriter = proceso.StandardInput
        '    Dim leyendoconsola As StreamReader = proceso.StandardOutput

        '    escribeconsola.WriteLine(comando & arg)
        '    escribeconsola.Close()
        '    proceso.WaitForExit()
        '    proceso.Close()

        ' End If
    End Function

    Function importarBD()




        Dim IP, port, pass As String

        IP = InputBox("Ingresa la IP del servidor de la BD")
        pass = InputBox("Ingresa la CONTRASEÑA del servidor")

        'port = InputBox("Ingresa el PUERTO del servidor")
        Try

            Dim file As String
            opd.Filter = "SQL Dump File (*.sql)|*.sql|All files (*.*)|*.*"
            If opd.ShowDialog = DialogResult.OK Then
                file = opd.FileName
                Dim myProcess As New Process()
                myProcess.StartInfo.FileName = "cmd.exe"
                myProcess.StartInfo.UseShellExecute = False
                myProcess.StartInfo.WorkingDirectory = "C:\Program Files\MySQL\MySQL Server 5.5\bin\"
                myProcess.StartInfo.RedirectStandardInput = True
                myProcess.StartInfo.RedirectStandardOutput = True
                myProcess.Start()
                Dim myStreamWriter As StreamWriter = myProcess.StandardInput
                Dim mystreamreader As StreamReader = myProcess.StandardOutput

                Dim nuevaruta As String
                nuevaruta = Replace(file, "\", "\\")


                'MsgBox(nuevaruta)
                'myStreamWriter.WriteLine("mysql  --host=localhost --user=root --port=3306 --default-character-set=utf8 --comments  < 'C:\\Users\\Dw\\Documents\\dumps\\Dump20201120.sql'")
                myStreamWriter.WriteLine("mysql -u root --password=" & pass & " -h " & IP & " < " + nuevaruta + "")
                myStreamWriter.Close()
                myProcess.WaitForExit()
                myProcess.Close()


                'LLAMADA DE LA INSERCION DE LOS DATOS POR DEFAULT PARA PODER INICIAR CON EL TRABAJO DEL SISTEMA

                'valoresiniciales()


                MsgBox("Database Restoration Successfully!", MsgBoxStyle.Information, "Restore")

                'MsgBox("Ahora si ya podemos trabajar :D", MsgBoxStyle.Information, "CTRL+y")


            End If

        Catch ex As Exception
            MsgBox("Hubo un problema hay realizar la restauración", MsgBoxStyle.Exclamation)
        End Try
    End Function
    'Function valoresiniciales()


    'Try

    '        conexionMysql.Open()
    '        Dim Sqlu As String
    '        Sqlu = "CREATE USER 'root'@'%' IDENTIFIED BY '" & txtpass.Text & "';"
    '        Dim cmdu As New MySqlCommand(Sqlu, conexionMysql)
    '        cmdu.ExecuteNonQuery()
    '        conexionMysql.Close()
    '        'llamar a la funcion limpiar, para limpiar las cajas cada vez que se agrege una nueva compra.
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql As String
    '        Sql = "INSERT INTO `dwin`.`cliente` (`nombre`, `ap`, `am`, `edad`, `direccion`, `telefono`, `correo`) VALUES ('usuario', 'usuario', 'usuario', '0', '0', '0', '0');"
    '        Dim cmd As New MySqlCommand(Sql, conexionMysql)
    '        cmd.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql3 As String
    '        Sql3 = "INSERT INTO `dwin`.`tipo_usuario` (`tipo_usuario`, `tipo`) VALUES ('1', 'ADMINISTRADOR');"
    '        Dim cmd3 As New MySqlCommand(Sql3, conexionMysql)
    '        cmd3.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql4 As String
    '        Sql4 = "INSERT INTO `dwin`.`tipo_usuario` (`tipo_usuario`, `tipo`) VALUES ('2', 'TRABAJADOR');"
    '        Dim cmd4 As New MySqlCommand(Sql4, conexionMysql)
    '        cmd4.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql5 As String
    '        Sql5 = "INSERT INTO `dwin`.`proveedor` (`nombre_empresa`, `nombre_encargado`, `ap_encargado`, `am_encargado`, `ciudad`, `estado`, `telefono`, `correo`) VALUES ('INDEFINIDO', 'INDEFINIDO', 'INDEFINIDO', 'INDEFINIDO', '00', '00', '00', '00');"
    '        Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
    '        cmd5.ExecuteNonQuery()
    '        conexionMysql.Close()



    '        conexionMysql.Open()
    '        Dim Sql5X As String
    '        Sql5X = "INSERT INTO `dwin`.`tipo_corte` (`idtipo_corte`, `tipo`, `estado`) VALUES ('1', 'USUARIO', '1');"
    '        Dim cmd5X As New MySqlCommand(Sql5X, conexionMysql)
    '        cmd5X.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql5X1 As String
    '        Sql5X1 = "INSERT INTO `dwin`.`tipo_corte` (`idtipo_corte`, `tipo`, `estado`) VALUES ('2', 'TIEMPO', '0');"
    '        Dim cmd5X1 As New MySqlCommand(Sql5X1, conexionMysql)
    '        cmd5X1.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql6 As String
    '        Sql6 = "INSERT INTO `dwin`.`tipoproducto` (`tipo`) VALUES ('INDEFINIDO');"
    '        Dim cmd6 As New MySqlCommand(Sql6, conexionMysql)
    '        cmd6.ExecuteNonQuery()
    '        conexionMysql.Close()



    '        conexionMysql.Open()
    '        Dim Sql6t As String
    '        Sql6t = "INSERT INTO `dwin`.`tipoventa` (`idtipoventa`,`tipoventa`) VALUES ('1','VENTA GENERAL');"
    '        Dim cmd6t As New MySqlCommand(Sql6t, conexionMysql)
    '        cmd6t.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql6tT As String
    '        Sql6tT = "INSERT INTO `dwin`.`tipoventa` (`idtipoventa`,`tipoventa`) VALUES ('2','SERIGRAFIA');"
    '        Dim cmd6tT As New MySqlCommand(Sql6tT, conexionMysql)
    '        cmd6tT.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql6ttt As String
    '        Sql6ttt = "INSERT INTO `dwin`.`tipoventa` (`idtipoventa`,`tipoventa`) VALUES ('3','VINIL');"
    '        Dim cmd6ttt As New MySqlCommand(Sql6ttt, conexionMysql)
    '        cmd6ttt.ExecuteNonQuery()
    '        conexionMysql.Close()




    '        '-----------------------------------------------------------------------------------------

    '        'reiniciamos los valores de autoincrementables en 0
    '        conexionMysql.Open()
    '        Dim Sql7 As String
    '        Sql7 = "alter table tipo_usuario auto_increment=0;"
    '        Dim cmd7 As New MySqlCommand(Sql7, conexionMysql)
    '        cmd7.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql8 As String
    '        Sql8 = "alter table cliente auto_increment=0;"
    '        Dim cmd8 As New MySqlCommand(Sql8, conexionMysql)
    '        cmd8.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql9 As String
    '        Sql9 = "alter table compra auto_increment=0;"
    '        Dim cmd9 As New MySqlCommand(Sql9, conexionMysql)
    '        cmd9.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql91 As String
    '        Sql91 = "alter table compramercancia auto_increment=0;"
    '        Dim cmd91 As New MySqlCommand(Sql91, conexionMysql)
    '        cmd91.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql92 As String
    '        Sql92 = "alter table corte auto_increment=0;"
    '        Dim cmd92 As New MySqlCommand(Sql92, conexionMysql)
    '        cmd92.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql93 As String
    '        Sql93 = "alter table cotizador_marco_ind auto_increment=0;"
    '        Dim cmd93 As New MySqlCommand(Sql93, conexionMysql)
    '        cmd93.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql94 As String
    '        Sql94 = "alter table devolucion auto_increment=0;"
    '        Dim cmd94 As New MySqlCommand(Sql94, conexionMysql)
    '        cmd94.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql95 As String
    '        Sql95 = "alter table proveedor auto_increment=0;"
    '        Dim cmd95 As New MySqlCommand(Sql95, conexionMysql)
    '        cmd95.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql96 As String
    '        Sql96 = "alter table secotizaciontintas auto_increment=0;"
    '        Dim cmd96 As New MySqlCommand(Sql96, conexionMysql)
    '        cmd96.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql97 As String
    '        Sql97 = "alter table servicios_ventas auto_increment=0;"
    '        Dim cmd97 As New MySqlCommand(Sql97, conexionMysql)
    '        cmd97.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        conexionMysql.Open()
    '        Dim Sql98 As String
    '        Sql98 = "alter table tipo_usuario auto_increment=0;"
    '        Dim cmd98 As New MySqlCommand(Sql98, conexionMysql)
    '        cmd98.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql99 As String
    '        Sql99 = "alter table tipoproducto auto_increment=0;"
    '        Dim cmd99 As New MySqlCommand(Sql99, conexionMysql)
    '        cmd99.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql910 As String
    '        Sql910 = "alter table usuario auto_increment=0;"
    '        Dim cmd910 As New MySqlCommand(Sql910, conexionMysql)
    '        cmd910.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql911 As String
    '        Sql911 = "alter table ventaind auto_increment=0;"
    '        Dim cmd911 As New MySqlCommand(Sql911, conexionMysql)
    '        cmd911.ExecuteNonQuery()
    '        conexionMysql.Close()

    '        conexionMysql.Open()
    '        Dim Sql912 As String
    '        Sql912 = "alter table vinildificultad auto_increment=0;"
    '        Dim cmd912 As New MySqlCommand(Sql912, conexionMysql)
    '        cmd912.ExecuteNonQuery()
    '        conexionMysql.Close()


    '        'PARA PODER TENER UN USUARIO, NECESITO LOS TIPOS DE USUARIOS REGISTRADOR EN LOS DOS INSERT ANTERIORES.                          
    '        conexionMysql.Open()
    '        Dim Sql2 As String
    '        Sql2 = "INSERT INTO `dwin`.`usuario` (`usuario`, `nombre`, `ap`, `am`, `telefono`, `correo`, `direccion`, `tipo_usuario`) VALUES ('root', '0', '0', '0', '0', '0', '0', '1');"
    '        Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
    '        cmd2.ExecuteNonQuery()
    '        conexionMysql.Close()

    '    Catch ex As Exception
    '        MsgBox("CONTROLADOR DE ERRORES:" & ex.Message, MsgBoxStyle.Exclamation, "CTRL+y")
    '    End Try




    'End Function
    Function cerrarconexion()
        If conexionMysql.State = ConnectionState.Open Then
            conexionMysql.Close()
        End If
    End Function
    Function obtenertipo_usuario()
        cerrarconexion()
        conexionMysql.Open()
        Dim tipo_usuario As String
        Try
            Dim Sql As String
            Sql = "select tipo_usuario.tipo from tipo_usuario, usuario where usuario.usuario='" & txtusuario.Text & "' and usuario.tipo_usuario=tipo_usuario.tipo_usuario;"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            tipo_usuario = reader.GetString(0).ToString
        Catch ex As Exception
            tipo_usuario = "NO DEFINIDO"
        End Try
        'MsgBox("tu nombre es:" + nombre)
        'pass.Text = nombre
        frmindex.lbusuario.Text = txtusuario.Text & "/" & tipo_usuario

        ''''''''''''''''''''''''''''''''''''''''''''''''''frmindex.usuarioactual = txtusuario.Text
        conexionMysql.Close()

    End Function
    Private Sub txtpass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpass.KeyPress
        If e.KeyChar = Chr(13) Then
            '    entrar()
            conectaralsistema()
        End If
    End Sub

    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged

    End Sub
    Function entrar()
        Try

            Dim nombre As String
            'llamado a la conexion a la bd
            'Call conexionx()

            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()
            End If
            conexionMysql.Open()
            'MsgBox(conexionMysql.State)


            Dim Sql As String
            Sql = "select * from usuario where user='" & txtusuario.Text & "' and pass='" & txtpass.Text & "';"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            nombre = reader.GetString(1).ToString

            'MsgBox("tu nombre es:" + nombre)
            'pass.Text = nombre

            conexionMysql.Close()

            frmindex.Show()
            frmindex.lbusuario.Text = txtusuario.Text

            '''''''''''''''''''''''''''''''''''''''''''''frmindex.usuarioactual = txtusuario.Text
            Me.Hide()
            Me.Finalize()


        Catch ex As Exception
            MsgBox("El usuario no existe o la contraseña es incorrecta", MsgBoxStyle.Exclamation, "Sistema")
            'txtusuario.Text = ""
            txtpass.Text = ""
            txtusuario.Text = ""
            txtusuario.Focus()

        End Try
    End Function

    Private Sub txtusuario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtusuario.KeyPress
        If e.KeyChar = Chr(13) Then
            txtpass.Focus()

        End If
    End Sub

    Private Sub txtusuario_TextChanged(sender As Object, e As EventArgs) Handles txtusuario.TextChanged

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs)
        conexionMysql.Open()
        Dim Sql As String
        'Sql = "select * from producto where idproducto=" & txtclave.Text & ";"

        Sql = "select * from producto where idproducto=1;"
        Dim cmd As New MySqlCommand(Sql, conexionMysql)
        reader = cmd.ExecuteReader()
        reader.Read()
        MsgBox(reader.GetString(1).ToString())


        conexionMysql.Close()
    End Sub

    'Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
    '    End

    'End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    '    creararchivo()

    'End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        creararchivo()
    End Sub
End Class
