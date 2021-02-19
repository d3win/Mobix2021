<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRinventario
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
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource3 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRinventario))
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.dslogoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsdatos_empresaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsproductoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.dslogoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsdatos_empresaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsproductoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "dtlogo"
        ReportDataSource1.Value = Me.dslogoBindingSource
        ReportDataSource2.Name = "dtdatos_empresa"
        ReportDataSource2.Value = Me.dsdatos_empresaBindingSource
        ReportDataSource3.Name = "dtproducto"
        ReportDataSource3.Value = Me.dsproductoBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource3)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Mobi.RPinventario.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(1133, 692)
        Me.ReportViewer1.TabIndex = 0
        '
        'FRinventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1133, 692)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FRinventario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FRinventario"
        CType(Me.dslogoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsdatos_empresaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsproductoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents dslogoBindingSource As BindingSource
    Friend WithEvents dsdatos_empresaBindingSource As BindingSource
    Friend WithEvents dsproductoBindingSource As BindingSource
End Class
