Imports MySql.Data.MySqlClient
Public Class abono
    Public folio, total, deposito, resto As String
    Private Sub Abono_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim folio As String
        folio = frmindex.rtxtbusquedafolio.Text
        cerrarconexion()
        'buscamos el folio de venta en la bd
        'MsgBox(folio)
        conexionMysql.Open()
        'Dim total, deposito, resto As String
        Dim Sql As String
        Sql = "select total,deposito,resto from venta where idventa='" & folio & "';"
        Dim cmd As New MySqlCommand(Sql, conexionMysql)
        reader = cmd.ExecuteReader()
        reader.Read()
        total = reader.GetString(0).ToString()
        deposito = reader.GetString(1).ToString()
        resto = reader.GetString(2).ToString()

        'rtxttotal.Text = reader.GetString(4).ToString()
        'rcalendario.Text = reader.GetString(6).ToString()
        'reader.Close()
        ' MsgBox(idcustomer)
        'MsgBox(idequipo)
        'MsgBox(idseller)
        cerrarconexion()

        'actualizamos el registro








    End Sub

    Private Sub Rtxttotal_TextChanged(sender As Object, e As EventArgs) Handles rtxttotal.TextChanged
        'chauto.Checked = False


        '        MsgBox(resto)
        '       MsgBox(rtxttotal.Text)
        Try



            If CDbl(rtxttotal.Text) > CDbl(resto) Then
                MsgBox("Excedes el valor", MsgBoxStyle.Information, "MOBI")
                rtxttotal.Text = ""




            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles chauto.CheckedChanged
        rtxttotal.Text = resto
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'e.Graphics.DrawString(txtetiqueta1, New Font("verdana", 11, FontStyle.Bold), New SolidBrush(Color.Black), 1, 9)
        'e.Graphics.DrawString(txtetiqueta2, New Font("verdana", 9, FontStyle.Bold), New SolidBrush(Color.Black), 1, 28)
        'e.Graphics.DrawString(txtetiqueta, New Font("verdana", 13, FontStyle.Bold), New SolidBrush(Color.Black), 1, 57)
        'traemos la información del ticket, como encabezado, datos de la empresa etc.
        Dim saludo, ticketnombre, ticketcoloniaciudad, tickettelefono, ticketgiro, p1, p2, p3, p4, comentario As String
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
            e.Graphics.DrawImage(frmindex.pblogoticket.Image, 50, 20, 110, 110)


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
            e.Graphics.DrawString("LIC. " & rfc, prFont, Brushes.Black, x, yy(7))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, yy(8))

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Tech: " & frmindex.rcbseller.Text, prFont, Brushes.Black, x, yy(9))

            'imprimir el titutlo del ticket
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Customer:" & frmindex.rtxtcustomername.Text, prFont, Brushes.Black, x, yy(10))
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Date:" & Date.Now, prFont, Brushes.Black, x, yy(11))
            prFont = New Font("Arial", tfuente2, FontStyle.Bold)
            e.Graphics.DrawString("ORDER #" & frmindex.rtxtbusquedafolio.Text, prFont, Brushes.Black, x, yy(12))


            '---------------------------------------------------------------------------------------------------------------------------------
            'consulto, cuantos dispositivos son, para obtener su informacion

            Dim cantidaddispositivos, vueltas As Integer
            cerrarconexion()
            conexionMysql.Open()
            Dim Sql12 As String
            'temporalmente slbfolio.text por rtxtbusquedatemporal
            Sql12 = "select count(*) from equipo where idventa='" & frmindex.rtxtbusquedafolio.Text & "';"
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
            Sql123 = "select * from equipo where idventa='" & frmindex.rtxtbusquedafolio.Text & "';"
            Dim cmd123 As New MySqlCommand(Sql123, conexionMysql)
            reader = cmd123.ExecuteReader()

            Dim pp1, p5, p6, p7, p8, p9, p10, p11, p12 As Integer

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
                e.Graphics.DrawString("--PROBLEM--", prFont, Brushes.Black, x, p2)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString(problema, prFont, Brushes.Black, x, p3)

                'prFont = New Font("Arial", tfuente3, FontStyle.Bold)
                'e.Graphics.DrawString("ID------PRECIO------CANTIDAD----TOTAL", prFont, Brushes.Black, x, yy(15))





                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p4)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("--Device--", prFont, Brushes.Black, x, p5)
                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Item name:" & equipo, prFont, Brushes.Black, x, p6)

                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Model:" & modelo, prFont, Brushes.Black, x, p7)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("IMEI:" & imei, prFont, Brushes.Black, x, p8)


                prFont = New Font("Arial", tfuente, FontStyle.Bold)
                e.Graphics.DrawString("Status:" & estadox, prFont, Brushes.Black, x, p9)
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
                p10 = p10 + (incremento * 9)
                p11 = p11 + (incremento * 9)
                p12 = p12 + (incremento * 9)
                '                yy = yy + (incremento * 3.2)
                '               yy = yy + (incremento * 3.2)




            Next
            conexionMysql.Close()
            cerrarconexion()

            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, 0, pp1)



            Dim fechacalendarioentrega As String
            fechacalendarioentrega = frmindex.rcalendario.Value.ToString("MM/dd/yyyy")


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Delivery date:" & fechacalendarioentrega, prFont, Brushes.Black, x, p2)


            'aqui es donde tenemos que leer todos los datos de la grilla llamada "grilla"

            Dim i As Integer = frmindex.sgrilla.RowCount
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
            Sql14 = "select total,deposito,resto from venta where idventa='" & frmindex.rtxtbusquedafolio.Text & "';"
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
            e.Graphics.DrawString("Down payment:", prFont, Brushes.Black, x, p5)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & deposito, prFont, Brushes.Black, x + 150, p6)


            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("Balance:", prFont, Brushes.Black, x, p7)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("$ " & resto, prFont, Brushes.Black, x + 150, p8)



            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString("==================================", prFont, Brushes.Black, x, p9)
            prFont = New Font("Arial", tfuente, FontStyle.Bold)
            e.Graphics.DrawString(saludo, prFont, Brushes.Black, x, p10)
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

    Private Sub rtxttotal_KeyDown(sender As Object, e As KeyEventArgs) Handles rtxttotal.KeyDown
        If e.KeyCode = Keys.Enter Then

            sumaranticipo()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()


        End If

    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        sumaranticipo()
    End Sub
    Function sumaranticipo()


        Dim totaldeposito As Double

        If chauto.Checked = True Then

            totaldeposito = total

            resto = 0
        Else


            totaldeposito = CDbl(deposito) + CDbl(rtxttotal.Text)
            resto = total - totaldeposito

        End If

        'MsgBox(totaldeposito)



        If totaldeposito <= total Then



            cerrarconexion()
            conexionMysql.Open()
            Dim Sql2 As String
            Sql2 = "update venta set deposito='" & totaldeposito & "', resto='" & resto & "' where idventa='" & folio & "';"
            Dim cmd2 As New MySqlCommand(Sql2, conexionMysql)
            cmd2.ExecuteNonQuery()
            conexionMysql.Close()



            MsgBox("Registro actualizado", MsgBoxStyle.Information, "MOBI")

            imprimirabono()




            frmindex.realizarbusqueda()
            Me.Close()




        Else
            MsgBox("El valor excede", MsgBoxStyle.Information, "MOBI")
        End If

    End Function
    Function imprimirabono()
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
End Class