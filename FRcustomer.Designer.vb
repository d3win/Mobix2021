<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FRcustomer
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
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.data = New Mobi.data()
        Me.dslogoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dsdatos_empresaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.dscustomerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.data, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dslogoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsdatos_empresaBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dscustomerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource1.Name = "dtlogo"
        ReportDataSource1.Value = Me.dslogoBindingSource
        ReportDataSource2.Name = "dtdatos_empresa"
        ReportDataSource2.Value = Me.dsdatos_empresaBindingSource
        ReportDataSource3.Name = "dtcustomer"
        ReportDataSource3.Value = Me.dscustomerBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource3)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "Mobi.RPCustomer.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.ServerReport.BearerToken = Nothing
        Me.ReportViewer1.Size = New System.Drawing.Size(1169, 705)
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
        'dscustomerBindingSource
        '
        Me.dscustomerBindingSource.DataMember = "dscustomer"
        Me.dscustomerBindingSource.DataSource = Me.data
        '
        'FRcustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1169, 705)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Name = "FRcustomer"
        Me.Text = "FRcustomer"
        CType(Me.data, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dslogoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsdatos_empresaBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dscustomerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents dslogoBindingSource As BindingSource
    Friend WithEvents data As data
    Friend WithEvents dsdatos_empresaBindingSource As BindingSource
    Friend WithEvents dscustomerBindingSource As BindingSource
End Class
