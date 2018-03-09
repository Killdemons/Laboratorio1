using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Lab1
{
    public partial class Form1 : Form
    {
        string dir;
        string rutaxml = "E:/Labs1.xml";

        public Form1()
        {
            InitializeComponent();
        }

        public void AñadirInfo(int val)
        {
            XML xml = new XML();
            xml.crearXml(rutaxml, "Hotel");
            if (val.Equals(1))
            {
                xml.Añadir(val, txtId.Text, txtNombre.Text, txtPais.Text, txtLugar.Text, Convert.ToString(spnHabitaciones.Value), dir, "", "");
            }
            else
            {
                xml.Añadir(val, txtIdT.Text, "", "", "", "", "", txtRutaT.Text, txtPrecioT.Text);
            }


        }

        public XmlNode BuscarInfo(string id, string tipo)
        {
            XML xml = new XML();
            xml.crearXml(rutaxml, "Hotel");
            return xml.ReadXml(id, tipo);

        }

        public void GuardarInfo(string tipo)
        {
            XML xml = new XML();
            xml.crearXml(rutaxml, "Hotel");
            if (tipo.Equals("hotel"))
            {
                xml.UpdateXml(tipo, txtIdM.Text, txtNombreM.Text, txtPaisM.Text, txtLugarM.Text, Convert.ToString(spnHabitacionesM.Value), dir, "", "");
            }
            else
            {
                xml.UpdateXml(tipo, txtIdTM.Text, "", "", "", "", "", txtRutaTM.Text, txtPrecioTM.Text);
            }

        }

        public void controlPress(int val, KeyPressEventArgs e)
        {
            if (val.Equals(1))
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;

                }
            }
            else
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void btnFoto_Click(object sender, EventArgs e)
        {
            try
            {
                this.ofdLab1.ShowDialog();
                if (this.ofdLab1.FileName.Equals("") == false)
                {
                    dir = ofdLab1.FileName;
                    picPais.Load(this.ofdLab1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la imagen: " + ex.ToString());
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtLugar_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtIdM_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void txtNombreM_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtPaisM_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtLugarM_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtIdD_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            XML xml = new XML();
            xml.crearXml(rutaxml, "Hotel");
            Boolean validacion = xml.ValidarId(txtId.Text, "hotel");
            if (validacion.Equals(false))
            {
                AñadirInfo(1);
                limpiar();
            }
            else
            {
                MessageBox.Show("ID ya existe, usar un ID diferente");
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtIdM.ReadOnly = true;
            XmlNode unhotel = BuscarInfo(txtIdM.Text, "hotel");
            txtNombreM.Text = unhotel.SelectSingleNode("nombre").InnerText;
            txtPaisM.Text = unhotel.SelectSingleNode("pais").InnerText;
            txtLugarM.Text = unhotel.SelectSingleNode("lugar").InnerText;
            string hab = unhotel.SelectSingleNode("habitaciones").InnerText;
            spnHabitacionesM.Value = Convert.ToInt32(hab);
            picFotoM.Load(unhotel.SelectSingleNode("foto").InnerText);
            dir = unhotel.SelectSingleNode("foto").InnerText;

        }

        private void btnFotoM_Click(object sender, EventArgs e)
        {
            try
            {
                this.ofdLab1.ShowDialog();
                if (this.ofdLab1.FileName.Equals("") == false)
                {
                    dir = ofdLab1.FileName;
                    picFotoM.Load(this.ofdLab1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar la imagen: " + ex.ToString());
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            GuardarInfo("hotel");
            limpiar();
            txtIdM.ReadOnly = false;
        }

        private void btnBuscarD_Click(object sender, EventArgs e)
        {
            XmlNode unhotel = BuscarInfo(txtIdD.Text, "hotel");
            txtNombreD.Text = unhotel.SelectSingleNode("nombre").InnerText;
            txtPaisD.Text = unhotel.SelectSingleNode("pais").InnerText;
            txtLugarD.Text = unhotel.SelectSingleNode("lugar").InnerText;
            string hab = unhotel.SelectSingleNode("habitaciones").InnerText;
            spnHabitacionesD.Value = Convert.ToInt32(hab);
            picFotoD.Load(unhotel.SelectSingleNode("foto").InnerText);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            XML xml = new XML();
            xml.crearXml(rutaxml, "Hotel");
            xml.DeleteNodo(txtIdD.Text, "hotel");
            limpiar();
        }

        private void btnRegistrarT_Click(object sender, EventArgs e)
        {
            XML xml = new XML();
            xml.crearXml(rutaxml, "Hotel");
            Boolean validacion = xml.ValidarId(txtIdT.Text, "tarifa");
            if (validacion.Equals(false))
            {
                AñadirInfo(0);
                limpiar();
            }
            else
            {
                MessageBox.Show("ID ya existe, usar un ID diferente");
            }
        }

        private void txtIdT_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void txtRutaT_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtPrecioT_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void txtIdTM_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void txtRutaTM_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtPrecioTM_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void txtIdTD_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void txtRutaTD_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(1, e);
        }

        private void txtPrecioTD_KeyPress(object sender, KeyPressEventArgs e)
        {
            controlPress(0, e);
        }

        private void btnBuscarT_Click(object sender, EventArgs e)
        {
            txtIdTM.ReadOnly = true;
            XmlNode untarifa = BuscarInfo(txtIdTM.Text, "tarifa");
            txtRutaTM.Text = untarifa.SelectSingleNode("ruta").InnerText;
            txtPrecioTM.Text = untarifa.SelectSingleNode("precio").InnerText;
        }

        private void btnModificarT_Click(object sender, EventArgs e)
        {
            GuardarInfo("tarifa");
            limpiar();
            txtIdTM.ReadOnly = false;
        }

        private void btnBuscarTD_Click(object sender, EventArgs e)
        {
            XmlNode untarifa = BuscarInfo(txtIdTD.Text, "tarifa");
            txtRutaTD.Text = untarifa.SelectSingleNode("ruta").InnerText;
            txtPrecioTD.Text = untarifa.SelectSingleNode("precio").InnerText;
        }

        private void btnEliminarT_Click(object sender, EventArgs e)
        {
            XML xml = new XML();
            xml.crearXml(rutaxml, "Hotel");
            xml.DeleteNodo(txtIdTD.Text, "tarifa");
            limpiar();
        }

        public void limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtPais.Clear();
            txtLugar.Clear();
            spnHabitaciones.Value = 0;
            picPais.Image = null;
            txtIdM.Clear();
            txtNombreM.Clear();
            txtPaisM.Clear();
            txtLugarM.Clear();
            spnHabitacionesM.Value = 0;
            picFotoM.Image = null;
            txtIdD.Clear();
            txtNombreD.Clear();
            txtPaisD.Clear();
            txtLugarD.Clear();
            spnHabitacionesD.Value = 0;
            picFotoD.Image = null;
            txtIdT.Clear();
            txtRutaT.Clear();
            txtPrecioT.Clear();
            txtIdTM.Clear();
            txtRutaTM.Clear();
            txtPrecioTM.Clear();
            txtIdTD.Clear();
            txtRutaTD.Clear();
            txtPrecioTD.Clear();
        }
    }
}
