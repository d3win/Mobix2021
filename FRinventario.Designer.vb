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
        Dim ReportDataSource4 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource5 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource6 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRinventario))
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.data = New Mobi.data()
        Me.dslogoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsdatos_empresaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsproductoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.data, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dslogoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsdatos_empresaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsproductoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource4.Name = "dtlogo"
        ReportDataSource4.Value = Me.dslogoBindingSource
        ReportDataSource5.Name = "dtdatos_empresa"
        ReportDataSource5.Value = Me.dsdatos_empresaBindingSource
        ReportDataSource6.Name = "dtproducto"
        ReportDataSource6.Value = Me.dsproductoBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource4)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource5)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource6)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Mobi.RPinventario.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(1133, 692)
        Me.ReportViewer1.TabIndex = 0
        '
        'data
        '
        Me.data.DataSetName = "data"
        Me.data.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dslogoBindingSource
        '
        Me.dslogoBindingSource.DataMember = "dslogo"
        Me.dslogoBindingSource.DataSource = Me.data
        '
        'dsdatos_empresaBindingSource
        '
        Me.dsdatos_empresaBindingSource.DataMember = "dsdatos_empresa"
        Me.dsdatos_empresaBindingSource.DataSource = Me.data
        '
        'dsproductoBindingSource
        '
        Me.dsproductoBindingSource.DataMember = "dsproducto"
        Me.dsproductoBindingSource.DataSource = Me.data
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
        CType(Me.data, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dslogoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsdatos_empresaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsproductoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents dslogoBindingSource As BindingSource
    Friend WithEvents data As data
    Friend WithEvents dsdatos_empresaBindingSource As BindingSource
    Friend WithEvents dsproductoBindingSource As BindingSource
End Class
