Imports System.Data.OleDb

Public Class Form1
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click

        If txtUsuario.Text = "" Then
            MsgBox("Debe ingresar un usuario", MsgBoxStyle.Information, "Error")
            txtUsuario.Focus()
            Exit Sub
        End If

        If txtClave.Text = "" Then
            MsgBox("Debe ingresar una clave de usuario", MsgBoxStyle.Information, "Error")
            txtClave.Focus()
            Exit Sub
        End If
        'ABRIMOS CONEXION CON LA BASE DE DATOS
        Dim miCadena As String
        Dim miSQL As String
        Dim miConexion As OleDbConnection
        Dim miAdaptador As OleDbDataAdapter
        Dim miDataSet As DataSet

        'ESTABLECE CONEXION CON LA BASE DE DATOS
        miCadena = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Productos.accdb"
        miConexion = New OleDbConnection(miCadena)

        'HACE LA CONSULTA A LA BASE DE DATOS
        miAdaptador = New OleDbDataAdapter
        miSQL = "select (1) FROM tblUsuario WHERE id = '" +
            txtUsuario.Text + "' and  clave ='" + txtClave.Text + "'"
        miAdaptador.SelectCommand = New OleDbCommand(miSQL, miConexion)
        miDataSet = New DataSet
        miDataSet.Tables.Add("usuario")
        miAdaptador.Fill(miDataSet.Tables("usuario"))

        If miDataSet.Tables("usuario").Rows.Count = 0 Then
            MsgBox("usuarios o clave no validos", MsgBoxStyle.Information, "ERROR")
            txtUsuario.Text = ""
            txtClave.Text = ""
            txtUsuario.Focus()
            miConexion.Close()
            Exit Sub
        End If

        MsgBox("fuck yea", MsgBoxStyle.Exclamation, "como lovio?")



    End Sub
End Class
