Imports MySql.Data.MySqlClient
Public Class frmproducto
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Try


            If rtxtclaveproducto.Text = "" Or rtxtnombre.Text = "" Or rtxtcantidad.Text = "" Or rtxtcosto.Text = "" Or rtxtprecio.Text = "" Then
                MsgBox("Es necesario toda la información", MsgBoxStyle.Information, "MOBI")
            Else

                'verificamos que no exista la clave que quieren insertar

                Dim idproducto As Integer

                Try

                    conexionMysql.Open()
                    Dim Sql As String
                    Sql = "select idproducto from producto where idproducto='" & rtxtclaveproducto.Text & "';"
                    Dim cmd As New MySqlCommand(Sql, conexionMysql)
                    reader = cmd.ExecuteReader()
                    reader.Read()
                    idproducto = reader.GetString(0).ToString()
                    reader.Close()
                    conexionMysql.Close()
                Catch ex As Exception
                    cerrarconexion()
                    idproducto = 0
                End Try

                If idproducto = 0 Then


                    conexionMysql.Open()
                    Dim Sqlx1 As String
                    Sqlx1 = "INSERT INTO `producto` (`idproducto`, `name`, `cantidad`, `costo`, `precio`) VALUES ( '" & rtxtclaveproducto.Text & "', '" & rtxtnombre.Text & "', '" & rtxtcantidad.Text & "', '" & rtxtcosto.Text & "', '" & rtxtprecio.Text & "');"
                    Dim cmdx1 As New MySqlCommand(Sqlx1, conexionMysql)
                    cmdx1.ExecuteNonQuery()
                    conexionMysql.Close()
                    'txttotalpagar.Text = ""
                    conexionMysql.Close()
                    MsgBox("Producto almacenado", MsgBoxStyle.Information, "MOBI")
                    Me.Close()

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Rtxtclaveproducto_TextChanged(sender As Object, e As EventArgs) Handles rtxtclaveproducto.TextChanged
        If rtxtclaveproducto.Text = "" Then
            rlimpiar()

        Else
            rbuscarid()
            'lblistarespuestos.Visible = False
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

                    rtxtcantidad.Text = reader.GetString(2).ToString()
                    rtxtcosto.Text = reader.GetString(3).ToString()

                    'txtpreciomayoreop.Text = reader.GetString(5).ToString()

                    reader.Close()

                    conexionMysql.Close()

                    'MsgBox("entro")



                Catch ex As Exception
                    'btninconsistencia.Visible = True
                    cerrarconexion()
                    'rlimpiar()
                    rtxtnombre.Text = ""
                    rtxtcantidad.Text = ""
                    rtxtcosto.Text = ""
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
        rtxtcosto.Text = ""


    End Function

    Private Sub Rtxtcantidad_TextChanged(sender As Object, e As EventArgs) Handles rtxtcantidad.TextChanged

    End Sub

    Private Sub rtxtcantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtxtcantidad.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub Rtxtcosto_TextChanged(sender As Object, e As EventArgs) Handles rtxtcosto.TextChanged

    End Sub

    Private Sub rtxtcosto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtxtcosto.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub Rtxtprecio_TextChanged(sender As Object, e As EventArgs) Handles rtxtprecio.TextChanged

    End Sub

    Private Sub rtxtprecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtxtprecio.KeyPress
        If InStr(1, "0123456789.-" & Chr(8), e.KeyChar) = 0 Then
            e.KeyChar = ""
        End If
    End Sub
End Class