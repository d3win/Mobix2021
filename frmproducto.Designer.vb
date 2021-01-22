<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmproducto
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmproducto))
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rtxtprecio = New System.Windows.Forms.TextBox()
        Me.rtxtclaveproducto = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.rtxtcantidad = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.rtxtcosto = New System.Windows.Forms.TextBox()
        Me.Button17 = New System.Windows.Forms.Button()
        Me.rtxtnombre = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.GroupBox9.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Label1)
        Me.GroupBox9.Controls.Add(Me.rtxtprecio)
        Me.GroupBox9.Controls.Add(Me.rtxtclaveproducto)
        Me.GroupBox9.Controls.Add(Me.Label44)
        Me.GroupBox9.Controls.Add(Me.Label43)
        Me.GroupBox9.Controls.Add(Me.rtxtcantidad)
        Me.GroupBox9.Controls.Add(Me.Label29)
        Me.GroupBox9.Controls.Add(Me.rtxtcosto)
        Me.GroupBox9.Controls.Add(Me.Button17)
        Me.GroupBox9.Controls.Add(Me.rtxtnombre)
        Me.GroupBox9.Controls.Add(Me.Label31)
        Me.GroupBox9.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(286, 374)
        Me.GroupBox9.TabIndex = 101
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Costos"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 254)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 17)
        Me.Label1.TabIndex = 1115
        Me.Label1.Text = "Precio"
        '
        'rtxtprecio
        '
        Me.rtxtprecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtprecio.Location = New System.Drawing.Point(13, 274)
        Me.rtxtprecio.MaxLength = 5
        Me.rtxtprecio.Name = "rtxtprecio"
        Me.rtxtprecio.Size = New System.Drawing.Size(111, 23)
        Me.rtxtprecio.TabIndex = 5
        '
        'rtxtclaveproducto
        '
        Me.rtxtclaveproducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtclaveproducto.Location = New System.Drawing.Point(13, 47)
        Me.rtxtclaveproducto.MaxLength = 35
        Me.rtxtclaveproducto.Name = "rtxtclaveproducto"
        Me.rtxtclaveproducto.Size = New System.Drawing.Size(251, 23)
        Me.rtxtclaveproducto.TabIndex = 1
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(16, 27)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(103, 17)
        Me.Label44.TabIndex = 17
        Me.Label44.Text = "Clave producto"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(10, 139)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(64, 17)
        Me.Label43.TabIndex = 16
        Me.Label43.Text = "Cantidad"
        '
        'rtxtcantidad
        '
        Me.rtxtcantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtcantidad.Location = New System.Drawing.Point(13, 159)
        Me.rtxtcantidad.MaxLength = 3
        Me.rtxtcantidad.Name = "rtxtcantidad"
        Me.rtxtcantidad.Size = New System.Drawing.Size(111, 23)
        Me.rtxtcantidad.TabIndex = 3
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(11, 195)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(44, 17)
        Me.Label29.TabIndex = 14
        Me.Label29.Text = "Costo"
        '
        'rtxtcosto
        '
        Me.rtxtcosto.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtcosto.Location = New System.Drawing.Point(13, 215)
        Me.rtxtcosto.MaxLength = 5
        Me.rtxtcosto.Name = "rtxtcosto"
        Me.rtxtcosto.Size = New System.Drawing.Size(111, 23)
        Me.rtxtcosto.TabIndex = 4
        '
        'Button17
        '
        Me.Button17.BackColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(184, Byte), Integer))
        Me.Button17.FlatAppearance.BorderSize = 0
        Me.Button17.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button17.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Button17.Image = CType(resources.GetObject("Button17.Image"), System.Drawing.Image)
        Me.Button17.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button17.Location = New System.Drawing.Point(30, 314)
        Me.Button17.Margin = New System.Windows.Forms.Padding(4)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(223, 36)
        Me.Button17.TabIndex = 6
        Me.Button17.Text = "Add list"
        Me.Button17.UseVisualStyleBackColor = False
        '
        'rtxtnombre
        '
        Me.rtxtnombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtxtnombre.Location = New System.Drawing.Point(12, 103)
        Me.rtxtnombre.MaxLength = 35
        Me.rtxtnombre.Name = "rtxtnombre"
        Me.rtxtnombre.Size = New System.Drawing.Size(252, 23)
        Me.rtxtnombre.TabIndex = 2
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(15, 83)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(58, 17)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "Nombre"
        '
        'frmproducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(310, 399)
        Me.Controls.Add(Me.GroupBox9)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmproducto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmproducto"
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents rtxtclaveproducto As TextBox
    Friend WithEvents Label44 As Label
    Friend WithEvents Label43 As Label
    Friend WithEvents rtxtcantidad As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents rtxtcosto As TextBox
    Friend WithEvents Button17 As Button
    Friend WithEvents rtxtnombre As TextBox
    Friend WithEvents Label31 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents rtxtprecio As TextBox
End Class
