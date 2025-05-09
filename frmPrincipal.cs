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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            CargarCmbBuscarPor();
            ActualizarTreeView(0);
        }
        public void CargarCmbBuscarPor()
        {
            cmbBuscarPor.Items.Clear();
            cmbBuscarPor.Items.Insert(0, "Todos");
            cmbBuscarPor.Items.Insert(1, "Nombre");
            cmbBuscarPor.Items.Insert(2, "Telefono");
            cmbBuscarPor.Items.Insert(3, "Correo");
            cmbBuscarPor.SelectedIndex = 0;
        }
        public void ActualizarTreeView(int Index)
        {
            trvContactos.Nodes.Clear();
            CrearNodosTreeview();
            switch (Index)
            {
                case 0:
                    ActualizarTreeViewTodos();
                    break;
                case 1:
                    ActualizarTreeViewNombre();
                    break;
                case 2:
                    ActualizarTreeViewTelefono();
                    break;
                case 3:
                    ActualizarTreeViewCorreo();
                    break;
            }
        }
        public void ActualizarTreeViewNombre()
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Contactos where Nombre like '%" + txtBuscar.Text + "%'";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TreeNode nuevoNodo = new TreeNode(reader["Apellido"].ToString() + ", " + reader["Nombre"].ToString());
                            nuevoNodo.Tag = Convert.ToInt32(reader["Codigo"]);

                            trvContactos.Nodes[Convert.ToInt32(reader["CategoriaId"]) - 1].Nodes.Add(nuevoNodo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al cargar los contactos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void ActualizarTreeViewTelefono()
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Contactos where Telefono like '%" + txtBuscar.Text + "%'";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TreeNode nuevoNodo = new TreeNode(reader["Apellido"].ToString() + ", " + reader["Nombre"].ToString());
                            nuevoNodo.Tag = Convert.ToInt32(reader["Codigo"]);

                            trvContactos.Nodes[Convert.ToInt32(reader["CategoriaId"]) - 1].Nodes.Add(nuevoNodo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al cargar los contactos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void ActualizarTreeViewCorreo()
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Contactos where Correo like '%" + txtBuscar.Text + "%'";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TreeNode nuevoNodo = new TreeNode(reader["Apellido"].ToString() + ", " + reader["Nombre"].ToString());
                            nuevoNodo.Tag = Convert.ToInt32(reader["Codigo"]);

                            trvContactos.Nodes[Convert.ToInt32(reader["CategoriaId"]) - 1].Nodes.Add(nuevoNodo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al cargar los contactos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void ActualizarTreeViewTodos()
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    string strQuery = "select * from Contactos";

                    SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                    using (SqlDataReader reader = objCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TreeNode nuevoNodo = new TreeNode(reader["Apellido"].ToString() + ", " + reader["Nombre"].ToString());
                            nuevoNodo.Tag = Convert.ToInt32(reader["Codigo"]);

                            trvContactos.Nodes[Convert.ToInt32(reader["CategoriaId"]) - 1].Nodes.Add(nuevoNodo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al cargar los contactos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }
        public void CrearNodosTreeview()
        {
            trvContactos.Nodes.Clear();
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
                            trvContactos.Nodes.Insert(Convert.ToInt32(reader["Id"]) - 1, reader["Nombre"].ToString());
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
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int Codigo = Convert.ToInt32(trvContactos.SelectedNode.Tag);

                frmAMCContactos frmConsulta = new frmAMCContactos();
                frmConsulta.Abrir(0, Codigo);
                ActualizarTreeView(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al abrir el formulario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmAMCContactos frmAgregar = new frmAMCContactos();
                frmAgregar.Abrir(1, 0);
                ActualizarTreeView(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al abrir el formulario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                int Codigo = Convert.ToInt32(trvContactos.SelectedNode.Tag);

                frmAMCContactos frmModificar = new frmAMCContactos();
                frmModificar.Abrir(2, Codigo);
                ActualizarTreeView(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al abrir el formulario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            clsConexionBD objConnection = new clsConexionBD();
            if (objConnection.GetError() == "")
            {
                try
                {
                    if (MessageBox.Show("Cuando borra un producto es permantente, Quiere eliminar el producto?", "Confirmacion Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        string strQuery = "DELETE FROM Contactos where Codigo = @codigo";

                        SqlCommand objCommand = new SqlCommand(strQuery, objConnection.GetConnection());
                        objCommand.Parameters.AddWithValue("@codigo", Convert.ToInt32(trvContactos.SelectedNode.Tag));
                        objCommand.ExecuteNonQuery();
                        MessageBox.Show("Se elimino el producto con exito", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ActualizarTreeView(0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio el siguiente error: " + ex.Message, "Error al borrar el producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show(objConnection.GetError(), "Error al conectar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            objConnection.CloseConnection();
        }

        private void btnActualizarListado_Click(object sender, EventArgs e)
        {
            ActualizarTreeView(cmbBuscarPor.SelectedIndex);
            txtBuscar.Text = "";
        }
    }
}
