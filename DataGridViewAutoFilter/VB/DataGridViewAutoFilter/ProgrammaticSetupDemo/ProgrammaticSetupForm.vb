'---------------------------------------------------------------------
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Imports DataGridViewAutoFilter
Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class ProgrammaticSetupForm
    Inherits Form

    Dim WithEvents dataGridView1 As New DataGridView()
    Dim statusStrip1 As New StatusStrip()
    Dim filterStatusLabel As New ToolStripStatusLabel()
    Dim WithEvents showAllLabel As New ToolStripStatusLabel("Show &All")

    ' Initializes the form.
    Sub New()

        Me.dataGridView1.Dock = DockStyle.Fill

        With showAllLabel
            .Visible = False
            .IsLink = True
            .LinkBehavior = LinkBehavior.HoverUnderline
        End With

        With statusStrip1
            .Cursor = Cursors.Default
            .Items.AddRange(New ToolStripItem() { _
                filterStatusLabel, showAllLabel})
        End With

        With Me
            .Text = "DataGridView AutoFilter Demo (VB Programmatic Setup)"
            .Width *= 3
            .Height *= 2
            .Controls.AddRange(New Control() { _
                dataGridView1, statusStrip1})
        End With

    End Sub

    ' Initializes the data source.
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Dim data As New DataTable()
        data.ReadXmlSchema("..\\..\\..\\..\\..\\TestData.xsd")
        data.ReadXml("..\\..\\..\\..\\..\\TestData.xml")
        Dim dataSource As New BindingSource(data, Nothing)
        dataGridView1.DataSource = dataSource
        MyBase.OnLoad(e)
    End Sub

    ' Configures the autogenerated columns, replacing their header
    ' cells with AutoFilter header cells. 
    Private Sub dataGridView1_BindingContextChanged(ByVal sender As Object, _
        ByVal e As EventArgs) Handles dataGridView1.BindingContextChanged

        ' Continue only if the data source has been set.
        If dataGridView1.DataSource Is Nothing Then
            Return
        End If

        ' Add the AutoFilter header cell to each column.
        For Each col As DataGridViewColumn In dataGridView1.Columns
            col.HeaderCell = New  _
                DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell)
        Next

        ' Format the OrderTotal column as currency. 
        dataGridView1.Columns("OrderTotal").DefaultCellStyle.Format = "c"

        ' Resize the columns to fit their contents.
        dataGridView1.AutoResizeColumns()

    End Sub

    ' Displays the drop-down list when the user presses 
    ' ALT+DOWN ARROW or ALT+UP ARROW.
    Private Sub dataGridView1_KeyDown(ByVal sender As Object, _
        ByVal e As KeyEventArgs) Handles dataGridView1.KeyDown

        If e.Alt AndAlso (e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Up) Then

            Dim filterCell As DataGridViewAutoFilterColumnHeaderCell = _
                TryCast(dataGridView1.CurrentCell.OwningColumn.HeaderCell,  _
                DataGridViewAutoFilterColumnHeaderCell)
            If filterCell IsNot Nothing Then
                filterCell.ShowDropDownList()
                e.Handled = True
            End If

        End If

    End Sub

    ' Updates the filter status label. 
    Private Sub dataGridView1_DataBindingComplete(ByVal sender As Object, _
        ByVal e As DataGridViewBindingCompleteEventArgs) _
        Handles dataGridView1.DataBindingComplete

        Dim filterStatus As String = DataGridViewAutoFilterColumnHeaderCell _
            .GetFilterStatus(dataGridView1)
        If String.IsNullOrEmpty(filterStatus) Then
            showAllLabel.Visible = False
            filterStatusLabel.Visible = False
        Else
            showAllLabel.Visible = True
            filterStatusLabel.Visible = True
            filterStatusLabel.Text = filterStatus
        End If

    End Sub

    ' Clears the filter when the user clicks the "Show All" link
    ' or presses ALT+A. 
    Private Sub showAllLabel_Click(ByVal sender As Object, _
        ByVal e As EventArgs) Handles showAllLabel.Click

        DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridView1)

    End Sub

    Private Sub ProgrammaticSetupForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ProgrammaticSetupForm
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Name = "ProgrammaticSetupForm"
        Me.ResumeLayout(False)

    End Sub
End Class
