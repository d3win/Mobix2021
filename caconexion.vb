Imports MySql.Data.MySqlClient
Imports System.IO
Module caconexion
    Public conexionMysql As New MySqlConnection
    Public reader As MySqlDataReader
    Public respuesta As Boolean
    Public nombreusuario As String
    Public user, pass As String
    Public d1, d2, d3, d4, d5, strconexion, idvinil, idvinilusuario, idvinilresto, idvinilanticipo, idvinilbusquedaventa As String
    Public idtimer As Integer



    'Dim strconexion As String

    '----------

    Public Function conexionok()
        Try

            '------------------- LEER INFORMACIÓN DE ARCHIVO ---------------------------

            Dim lineas As New ArrayList()
            Dim carpeta As String
            Dim servidor, port, puerto, bd As String

            carpeta = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

            Dim freader As New StreamReader(carpeta & "\SISTEMANOBORRAR.txt")

            servidor = freader.ReadLine() 'leo primera linea
            port = freader.ReadLine() 'leo primera linea
            'MsgBox(port)
            'servidor = "localhost"
            'MsgBox(servidor)
            'MsgBox(contenido) 'pego primera linea
            bd = "mobi" 'leo segunda linea
            'bd = "u594602316_m1"
            'user = freader.ReadLine()
            'pass = freader.ReadLine()
            puerto = port
            'MsgBox(contenido) 'pego segunda linea


            user = login.txtusuario.Text
            pass = login.txtpass.Text



            'freader.Close()
            '-------------------------------------------------------------------------
            If conexionMysql.State = ConnectionState.Open Then
                conexionMysql.Close()

            End If

            '------------- CONECTAR AL SERVIDOR ---------------------------

            conexionMysql.ConnectionString = "Server=" & servidor & "; DataBase=" & bd & "; UID=" & user & "; PWD=" & pass & "; Port=" & puerto & ";"
            'conexionMysql.ConnectionString = "Server=localhost; DataBase=dwin; UID=root; PWD=; Port=3306"
            'strconexion = "Server=" & servidor & "; DataBase=" & bd & "; UID=" & user & "; PWD=" & pass & "; Port=" & puerto & ";"
            'MsgBox("La conexion es correcta")


            nombreusuario = user

            d1 = servidor
            d2 = bd
            d3 = user
            d4 = pass
            d5 = puerto

            conexionMysql.Open()

            conexionMysql.Close()
            'MsgBox("¡¡¡Conexión Exitosa!!!", MsgBoxStyle.Information, "Sistema")
            'Form1.Timer1.Enabled = False
            respuesta = True
            Return respuesta
            'MsgBox("ando aqui")
        Catch ex As Exception
            MsgBox("Upss... La conexion no se realizo, intenta ingresar los datos correctos.", MsgBoxStyle.Exclamation, "Sistema")
            MsgBox("Verifica que exista la ip del servidor de la BD", MsgBoxStyle.Exclamation, "Sistema")

            '-----------------------------


            'Dim frm As New frmconexion()
            'frm.Show()

            'Dim frminicio As New inicio()
            'respuesta = False
            '-----------------------


            'Me.Close()

            '            Me.Hide()


            'Me.Hide()


            'valor = 0
            'Form1.Timer1.Enabled = True 'version nueva-------------------------------------->

            'End

        End Try
        'com

    End Function
    Function cerrarconexion()
        If conexionMysql.State = ConnectionState.Open Then
            conexionMysql.Close()

        End If
    End Function
    Function tipoingreso()

        Try

            'funcion para determinar el tipo de ingreso, en caso de generar algun error.
            cerrarconexion()

            Dim tipoidusuario As Integer
            'obtenemos el id del usuario para saber si es trabajador o administrador, sabiendo que es ahora
            'lo podemos llamar desde cualquier error.

            conexionMysql.Open()
            Dim sql1 As String
            sql1 = "select tipo_usuario from usuario where usuario='" & nombreusuario & "';"
            Dim cmd1 As New MySqlCommand(sql1, conexionMysql)
            reader = cmd1.ExecuteReader
            reader.Read()
            tipoidusuario = reader.GetString(0).ToString()
            conexionMysql.Close()

            'retornamos el valor a quien lo llame
            Return tipoidusuario
        Catch ex As Exception
            If conexionMysql.State = ConnectionState.Open Then

                conexionMysql.Close()
            End If
        End Try

    End Function
End Module

