using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoMixiote
{
    public partial class InicializarMesas : Form
    {
        public InicializarMesas()
        {
            InitializeComponent();
        }

        private void btnfijar_Click(object sender, EventArgs e)
        {
            int nmesas = Convert.ToInt32(txtmesas.Text);
            gbjardin.Controls.Clear();

            int q = 45; //Altura de las etiquetas (libre/Ocupado)
            int y = 38; //Altura de los botones (mesa n)

            for (int x=33;x<41;x++)
            {
                Label lbl = new Label();
                lbl.Location = new Point(15, q);
                lbl.Width = 40;
                lbl.Height = 18;
                lbl.Font = new Font(lbl.Font.FontFamily, 11);
                lbl.ForeColor = Color.FromArgb(0, 192, 0);
                q += 34;
                lbl.Name = "lblmesa" + x;
                lbl.Text = "Libre";

                Button btn = new Button();
                btn.Location=new Point(90,y);
                btn.Width = 80;
                btn.Height = 28;
                btn.Font = new Font(btn.Font.FontFamily, 11);
                y += 34;
                btn.Name = "btnmesa" + x;
                btn.Text = "Mesa " + x;

                btn.Click += new EventHandler(handlerComun_Click);
                gbjardin.Controls.Add(lbl);
                gbjardin.Controls.Add(btn);
            }
        }

        private void handlerComun_Click(object sender, EventArgs e) //Evento clic de los botones de las mesas
        {
            MessageBox.Show("Hola");
        }
    }
}
