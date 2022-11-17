using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using PdfSharp.Pdf;

namespace Cachina_Fashion_Raul
{
    public partial class Fro_cachina : Form
    {
        string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
        DataTable Proveedor()
        {
            SqlConnection cn=new SqlConnection(cadena);
            SqlDataAdapter da = new SqlDataAdapter("exec usp_Proveedor", cn);

            DataTable tb=new DataTable();
            da.Fill(tb);
            return tb;  
        }
        public Fro_cachina()
        {
            InitializeComponent();
          

            dgProveedores.DataSource = Proveedor();
        }

        private void Fro_cachina_Load(object sender, EventArgs e)
        {
           
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e, DataGridView f)
        {
            DataGridViewRow currentRow = dgProveedores.CurrentRow;
            txtNombreproveedor.Text = f.SelectedCells[0].Value.ToString();
            txtDNI_proveedor.Text = f.SelectedCells[0].Value.ToString();
            txtDescripcion.Text = f.SelectedCells[0].Value.ToString();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                SqlCommand cmd = new SqlCommand("usp_inserta_proveedor", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", txtNombreproveedor.Text);
                cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@DNI", txtDNI_proveedor.Text);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                MessageBox.Show($"Se ha agregado{i} proveedor");
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { cn.Close(); }
            dgProveedores.DataSource = Proveedor();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            SqlConnection cm= new SqlConnection(cadena);
            try{
                SqlCommand cmd = new SqlCommand("usp_actualiar_proveedor", cm);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", txtNombreproveedor.Text);
                cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@DNI", txtDNI_proveedor.Text);

                cm.Open();
                int i = cmd.ExecuteNonQuery();
                MessageBox.Show($"se ha actualizado el registro de proveedores{i}proveedor");
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {cm.Close(); }
            dgProveedores = null;   
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            /*aqui va el impirmir*/
         
        }
    }
}
