Imports Npgsql


Public Class frmLogin
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
        Dim MiConexion As NpgsqlConnection = New NpgsqlConnection()
        Dim miSQL As String
        Dim miComando As NpgsqlCommand
        Dim miAdaptador As NpgsqlDataReader
        Dim miDataSet As DataSet

        MiConexion.ConnectionString = "Server=test-postgresql.cswqokuzl0hv.us-east-1.rds.amazonaws.com;Port=5432;Database=dev_itague_identity;UserId=dev_itague;Password=fD2I41T.@)-u;"

        Try
            MiConexion.Open()
            If MiConexion.State Then

                'HACE LA CONSULTA A LA BASE DE DATOS
                miSQL = "select (1) as valido FROM tblUsuario WHERE id = '" +
                txtUsuario.Text + "' and  clave ='" + txtClave.Text + "'"

                miComando = New NpgsqlCommand(miSQL, MiConexion)
                miAdaptador = miComando.ExecuteReader()
                miDataSet = New DataSet()
                miDataSet.Tables.Add("usuario")
                miDataSet.Tables("usuario").Load(miAdaptador)

                If miDataSet.Tables("usuario").Rows.Count = 0 Then
                    MsgBox("usuarios o clave no validos", MsgBoxStyle.Information, "ERROR")
                    txtUsuario.Text = ""
                    txtClave.Text = ""
                    txtUsuario.Focus()
                Else
                    MsgBox("fuck yea", MsgBoxStyle.Exclamation, "como lovio?")

                    'Abrir formulario dashboard
                    Me.Hide()
                    Dim frmDashboard As frmDashboard = New frmDashboard()
                    frmDashboard.Show()

                End If

                'Se cierra la conexión a la base de datos
                MiConexion.Close()

            Else
                MessageBox.Show("Error de conexión")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
End Class
