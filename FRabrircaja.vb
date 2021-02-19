Imports MySql.Data.MySqlClient
Public Class FRabrircaja
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        abrircaja()
    End Sub
    Function abrircaja()
        If txtmontoinicial.Text = "" Then
            MsgBox("Ingresa el valor inicial del sistema", MsgBoxStyle.Information, "CTRL+y")
        Else
            Try


                Dim estado As Integer
                Dim registrar As Boolean
                'buscamos si hay ya abierta una caja, osea un registro de tal hora y tal fecha
                'Try
                Dim hora, fecha, fechahoy, fechaatras, dia, mes, año As String
                dia = Date.Now.Day
                mes = Date.Now.Month
                año = Date.Now.Year
                fecha = año & "-" & mes & "-" & dia



                Dim hora2, minuto, segundo As String
                Dim horax As String
                'MsgBox("holaaaaaaaaaaaaaaaaaaaa")


                hora2 = Now.Hour()
                minuto = Now.Minute()
                segundo = Now.Second()

                horax = hora2 & ":" & minuto & ":" & segundo

                '--------------------------------------------------------
                Try

                    conexionMysql.Open()
                    Dim sql2 As String
                    sql2 = "select * from caja where fecha='" & fecha & "' and estado='0';"
                    Dim cmd2 As New MySqlCommand(sql2, conexionMysql)
                    reader = cmd2.ExecuteReader
                    reader.Read()
                    fechahoy = reader.GetString(0).ToString()
                    conexionMysql.Close()
                    registrar = False
                    MsgBox("Existe ya un registro abierto, primero cierra tu cuenta de caja", MsgBoxStyle.Exclamation, "CTRL+y")
                    Me.Close()

                Catch ex As Exception
                    registrar = True
                End Try

                'MsgBox(registrar)
                'si registrar es verdadero, significa que no hay ninguna caja abierta, todas estan cerradas
                If registrar = True Then

                    'primero verificamos que no exista ningun registro realizado en tal fecha y tal hora.

                    cerrarconexion()

                    conexionMysql.Open()
                    'se registra el valor
                    Dim Sql2X As String
                    Sql2X = "INSERT INTO `caja` (`monto_inicial`, `fecha`, `hora_inicial`, `estado`) VALUES ('" & txtmontoinicial.Text & "', '" & fecha & "', '" & horax & "', '0');"
                    Dim cmd2X As New MySqlCommand(Sql2X, conexionMysql)
                    cmd2X.ExecuteNonQuery()
                    conexionMysql.Close()



                    MsgBox("Caja Abierta", MsgBoxStyle.Information, "CTRL+y")
                    Me.Close()
                    frmindex.btnabrircajamenu.Visible = False
                    frmindex.btncerrarcajamenu.Visible = True
                Else

                    '--------------------------------------------------------
                    'conexionMysql.Open()
                    'Dim sql As String
                    'sql = "select max(idcaja), estado from caja where fecha='" & fecha & "';"
                    'Dim cmd As New MySqlCommand(sql, conexionMysql)
                    'reader = cmd.ExecuteReader
                    'reader.Read()
                    'estado = reader.GetString(0).ToString()
                    'conexionMysql.Close()
                    ''--------------------------------------------------------
                End If
            Catch ex As Exception
                MsgBox("Existe un problema con la entrada de información", MsgBoxStyle.Information, "CTRL+y")
            End Try

            'Catch ex As Exception
            '    cerrarconexion()
            'End Try

        End If
    End Function
    Function cerrarconexion()
        If conexionMysql.State = ConnectionState.Open Then
            conexionMysql.Close()

        End If
    End Function

    Private Sub FRabrircaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtmontoinicial.Focus()
    End Sub

    Private Sub Btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Me.Close()
    End Sub

    Private Sub Txtmontoinicial_TextChanged(sender As Object, e As EventArgs) Handles txtmontoinicial.TextChanged

    End Sub

    Private Sub txtmontoinicial_KeyDown(sender As Object, e As KeyEventArgs) Handles txtmontoinicial.KeyDown
        If e.KeyCode = Keys.Enter Then
            abrircaja()
        End If
    End Sub
End Class