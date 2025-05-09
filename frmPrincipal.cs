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
            CrearNodosTreeview();
            ActualizarTreeView();
        }
        public void ActualizarTreeView()
        {

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
    }
}
