using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaContactos
{
    public partial class frmAMCContactos : Form
    {
        int Codigo;
        public frmAMCContactos()
        {
            InitializeComponent();
            CargarCmbCategorias();
        }
        public void Abrir(int Modo, int Codigo)
        {
            this.Codigo = Codigo;
            switch (Modo)
            {
                //Consultar
                case 0:
                    this.Text = "Consultar";

                    btnGrabar.Visible = false;
                    txtNombre.Enabled = false;
                    txtApellido.Enabled = false;
                    txtTelefono.Enabled = false;
                    txtCorreo.Enabled = false;
                    cmbCategorias.Enabled = false;

                    AbrirConsulta(Codigo);

                    break;
                //Agregar
                case 1:
                    this.Text = "Agregar";

                    break;
                //Modificar
                case 2:
                    this.Text = "Modificar";

                    AbrirModificar(Codigo);

                    break;
            }
            this.ShowDialog();
        }
        public void AbrirConsulta(int Codigo)
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Contactos p where p.Codigo = " + Codigo;

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtApellido.Text = reader["Apellido"].ToString();
                            txtTelefono.Text = reader["Telefono"].ToString();
                            txtCorreo.Text = reader["Correo"].ToString();
                            cmbCategorias.SelectedIndex = Convert.ToInt32(reader["CategoriaId"]) - 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al consultar el contacto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void AbrirModificar(int Codigo)
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Contactos p where p.Codigo = " + Codigo;

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtApellido.Text = reader["Apellido"].ToString();
                            txtTelefono.Text = reader["Telefono"].ToString();
                            txtCorreo.Text = reader["Correo"].ToString();
                            cmbCategorias.SelectedIndex = Convert.ToInt32(reader["CategoriaId"]) - 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al cargar el contacto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void CargarCmbCategorias()
        {
            cmbCategorias.Items.Clear();
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Categorias";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbCategorias.Items.Insert(Convert.ToInt32(reader["Id"]) - 1, reader["Nombre"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al cargar las categorias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (this.Text == "Agregar")
            {
                clsConexionBD objConnection = new clsConexionBD();
                if (objConnection.GetError() == "")
                {
                    try
                    {
                        string strQuery = "INSERT INTO Contactos (Nombre, Apellido, Telefono, Correo, CategoriaId) VALUES (@nombre, @apellido, @telefono, @correo, @categoriaId)";

                        SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                        objCommand.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        objCommand.Parameters.AddWithValue("@apellido", txtApellido.Text);
                        objCommand.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                        objCommand.Parameters.AddWithValue("@correo", txtCorreo.Text);
                        objCommand.Parameters.AddWithValue("@categoriaId", Convert.ToInt32(cmbCategorias.SelectedIndex) + 1);
                        objCommand.ExecuteNonQuery();
                        MessageBox.Show("Se agrego con exito el contacto", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al grabar el contacto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                objConnection.CloseConnection();
            }
            else
            {
                if (this.Text == "Modificar")
                {
                    clsConexionBD objConnection = new clsConexionBD();
                    if (objConnection.GetError() == "")
                    {
                        try
                        {
                            string strQuery = "UPDATE Contactos Set Nombre = @nombre, Apellido = @apellido, Telefono = @telefono, Correo = @correo, CategoriaId = @categoriaid where Codigo = @codigo";

                            SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                            objCommand.Parameters.AddWithValue("@codigo", Codigo);
                            objCommand.Parameters.AddWithValue("@nombre", txtNombre.Text);
                            objCommand.Parameters.AddWithValue("@apellido", txtApellido.Text);
                            objCommand.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                            objCommand.Parameters.AddWithValue("@correo", txtCorreo.Text);
                            objCommand.Parameters.AddWithValue("@categoriaId", Convert.ToInt32(cmbCategorias.SelectedIndex) + 1);
                            objCommand.ExecuteNonQuery();
                            MessageBox.Show("Se modifico con exito el contacto", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al modificar el contacto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    objConnection.CloseConnection();
                }
            }
        }
    }
}
