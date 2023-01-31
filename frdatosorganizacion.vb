Imports MySql.Data.MySqlClient
Public Class frdatosorganizacion
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter




    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'Dim respuesta As String
        'If ctxtnombre.Text = "" Or ctxtcallenumero.Text = "" Or ctxtcoloniaciudad.Text = "" Or ctxtcp.Text = "" Or ctxtestado.Text = "" Or ctxttelefono.Text = "" Or ctxtwhatsapp.Text = "" Or ctxtcorreo.Text = "" Or ctxtfacebook.Text = "" Or ctxtsitio.Text = "" Or ctxtencargado.Text = "" Or ctxthorario.Text = "" Then
        'respuesta = MsgBox("¿Estas seguro de dejar algunos campos vacios?", MsgBoxStyle.YesNo, "Ctrl+y")




        'If respuesta = vbYes Then
        conexionMysql.Open()

        Dim Sql5 As String
        Sql5 = "UPDATE datos_empresa SET nombre='" & ctxtnombre.Text & "', calle_numero='" & ctxtcallenumero.Text & "', colonia_ciudad='" & ctxtcoloniaciudad.Text & "', cp='" & ctxtcp.Text & "', estado='" & ctxtestado.Text & "', telefono='" & ctxttelefono.Text & "', whatsapp='" & ctxtwhatsapp.Text & "', correo='" & ctxtcorreo.Text & "', fanpage='" & ctxtfacebook.Text & "', sitio_web='" & ctxtsitio.Text & "', director='" & ctxtencargado.Text & "', horario='" & ctxthorario.Text & "', giro='" & ctxtgiro.Text & "';"
        Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
        cmd5.ExecuteNonQuery()
        conexionMysql.Close()
        MsgBox("Datos actualizados correctamente", MsgBoxStyle.Information, "Ctrl+y")
        frmindex.cargardatos_empresa()
        Me.Close()

        'End If


        'End If


    End Sub

    Private Sub Frdatosorganizacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'comprobar si ya existe un registro, en caso contrario lo insertamos.
        cerrarconexion()



        Try

            conexionMysql.Open()


            Dim Sql As String
            Sql = "select count(*) from datos_empresa;"
            Dim cmd As New MySqlCommand(Sql, conexionMysql)
            reader = cmd.ExecuteReader()
            reader.Read()
            Dim cantidad As Integer = reader.GetString(0).ToString()
            conexionMysql.Close()



            If cantidad = 0 Then
                'en caso de que sea cero, significa que aun no hay ningun dato insertado, entonces hacemos la insercion de datos por default

                conexionMysql.Open()

                Dim Sql51 As String
                Sql51 = "insert into datos_empresa  (nombre,calle_numero,colonia_ciudad,cp,estado,telefono,whatsapp,correo,fanpage,sitio_web,director,horario,giro,p1,p2,p3,p4,obligarticket,obligarcambio,saludo_ticket)  values (nombre='" & ctxtnombre.Text & "', calle_numero='" & ctxtcallenumero.Text & "', colonia_ciudad='" & ctxtcoloniaciudad.Text & "', cp='" & ctxtcp.Text & "', estado='" & ctxtestado.Text & "', telefono='" & ctxttelefono.Text & "', whatsapp='" & ctxtwhatsapp.Text & "', correo='" & ctxtcorreo.Text & "', fanpage='" & ctxtfacebook.Text & "', sitio_web='" & ctxtsitio.Text & "', director='" & ctxtencargado.Text & "', horario='" & ctxthorario.Text & "', giro='" & ctxtgiro.Text & "',0,0,0,0,0,0,'');"
                Dim cmd51 As New MySqlCommand(Sql51, conexionMysql)
                cmd51.ExecuteNonQuery()
                conexionMysql.Close()
                '           MsgBox("Datos actualizados correctamente", MsgBoxStyle.Information, "Ctrl+y")
                'Me.Close()


            Else
                'en caso contrario cargamos los datos que existan para evitar escribir todo nuevamente
                'Try


                conexionMysql.Open()
                Dim Sql1 As String
                Sql1 = "select * from datos_empresa;"
                Dim cmd1 As New MySqlCommand(Sql1, conexionMysql)
                reader = cmd1.ExecuteReader()
                reader.Read()
                ctxtnombre.Text = reader.GetString(1).ToString()
                ctxtcallenumero.Text = reader.GetString(2).ToString()
                ctxtcoloniaciudad.Text = reader.GetString(3).ToString()
                ctxtcp.Text = reader.GetString(4).ToString()
                ctxtestado.Text = reader.GetString(5).ToString()
                ctxttelefono.Text = reader.GetString(6).ToString()
                ctxtwhatsapp.Text = reader.GetString(7).ToString()
                ctxtcorreo.Text = reader.GetString(8).ToString()
                ctxtfacebook.Text = reader.GetString(9).ToString()
                ctxtsitio.Text = reader.GetString(10).ToString()
                ctxtencargado.Text = reader.GetString(11).ToString()
                ctxthorario.Text = reader.GetString(12).ToString()
                ctxtgiro.Text = reader.GetString(13).ToString()
                'txtsaludo.Text = reader.GetString(20).ToString()
                ctxtrfc.Text = reader.GetString(22).ToString()
                txtsaludoticket.Text = reader.GetString(24).ToString()
                txts2.Text = reader.GetString(25).ToString()
                txts3.Text = reader.GetString(26).ToString()
                txts4.Text = reader.GetString(27).ToString()
                txts5.Text = reader.GetString(28).ToString()

                conexionMysql.Close()

                'Catch ex As Exception
                If conexionMysql.State = ConnectionState.Open Then
                    conexionMysql.Close()

                End If
                'End Try


            End If

        Catch ex As Exception
            cerrarconexion()
        End Try




    End Sub

    Private Sub frdatosorganizacion_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            frmindex.cargardatos_empresa()
            Me.Close()

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Ctxtnombre_TextChanged(sender As Object, e As EventArgs) Handles ctxtnombre.TextChanged

    End Sub

    Private Sub ctxtnombre_KeyDown(sender As Object, e As KeyEventArgs) Handles ctxtnombre.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            frmindex.cargardatos_empresa()
            Me.Close()

        End If
    End Sub

    Function cerrarconexion()
        If conexionMysql.State = ConnectionState.Open Then
            conexionMysql.Close()

        End If
    End Function
    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click

        'Dim respuesta As String
        'If ctxtnombre.Text = "" Or ctxtcallenumero.Text = "" Or ctxtcoloniaciudad.Text = "" Or ctxtcp.Text = "" Or ctxtestado.Text = "" Or ctxttelefono.Text = "" Or ctxtwhatsapp.Text = "" Or ctxtcorreo.Text = "" Or ctxtfacebook.Text = "" Or ctxtsitio.Text = "" Or ctxtencargado.Text = "" Or ctxthorario.Text = "" Then
        'respuesta = MsgBox("¿Estas seguro de dejar algunos campos vacios?", MsgBoxStyle.YesNo, "Ctrl+y")

        'Dim idioma As Integer

        'If cbidioma.Text = "español" Then
        '    idioma = 0
        'Else
        '    idioma = 1

        'End If


        cerrarconexion()

        'Try


        'If respuesta = vbYes Then
        conexionMysql.Open()

            Dim Sql5 As String
        Sql5 = "UPDATE datos_empresa SET nombre='" & ctxtnombre.Text & "', calle_numero='" & ctxtcallenumero.Text & "', colonia_ciudad='" & ctxtcoloniaciudad.Text & "', cp='" & ctxtcp.Text & "', estado='" & ctxtestado.Text & "', telefono='" & ctxttelefono.Text & "', whatsapp='" & ctxtwhatsapp.Text & "', correo='" & ctxtcorreo.Text & "', fanpage='" & ctxtfacebook.Text & "', sitio_web='" & ctxtsitio.Text & "', director='" & ctxtencargado.Text & "', horario='" & ctxthorario.Text & "', giro='" & ctxtgiro.Text & "', rfc='" & ctxtrfc.Text & "', saludo_ticket='" & txtsaludoticket.Text & "', saludo2='" & txts2.Text & "',saludo3='" & txts3.Text & "',saludo4='" & txts4.Text & "',saludo5='" & txts5.Text & "';"
        Dim cmd5 As New MySqlCommand(Sql5, conexionMysql)
            cmd5.ExecuteNonQuery()
            conexionMysql.Close()
            MsgBox("Datos actualizados correctamente", MsgBoxStyle.Information, "Ctrl+y")
            frmindex.cargardatos_empresa()
            Me.Close()
        ' Catch ex As Exception
        'MsgBox("Al parecer hay datos demasiado largos, revisa nuevamente", MsgBoxStyle.Exclamation, "CTRL+y")
        'cerrarconexion()
        'End Try

        'End If


        'End If
    End Sub
End Class