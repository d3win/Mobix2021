<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frR1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource3 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frR1))
        Me.dslogoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsdatos_empresaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsstatusBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.data = New Ctrly.data()
        Me.dsstatus2BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.dslogoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsdatos_empresaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsstatusBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.data, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsstatus2BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "dtlogo"
        ReportDataSource1.Value = Me.dslogoBindingSource
        ReportDataSource2.Name = "dtdatos_empresa"
        ReportDataSource2.Value = Me.dsdatos_empresaBindingSource
        ReportDataSource3.Name = "dtstatus"
        ReportDataSource3.Value = Me.dsstatus2BindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource3)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Ctrly.RPstatus2.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(1292, 732)
        Me.ReportViewer1.TabIndex = 0
        '
        'data
        '
        Me.data.DataSetName = "data"
        Me.data.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dsstatus2BindingSource
        '
        Me.dsstatus2BindingSource.DataMember = "dsstatus2"
        Me.dsstatus2BindingSource.DataSource = Me.data
        '
        'frR1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1292, 732)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frR1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report"
        CType(Me.dslogoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsdatos_empresaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsstatusBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.data, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsstatus2BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dsdatos_empresaBindingSource As BindingSource
    Friend WithEvents dslogoBindingSource As BindingSource
    Friend WithEvents dsstatusBindingSource As BindingSource
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents dsstatus2BindingSource As BindingSource
    Friend WithEvents data As data
End Class
