using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication14
{
    public partial class CarSaleMain : Form
    {

        public CarSaleMain()
        {
            InitializeComponent();
        }

        private void CarSaleMain_Load(object sender, EventArgs e)
        {

        }
        private void CarSaleMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();

        }
        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarList car = new CarList();
            car.MdiParent = this;
            car.Show();
        }
    }
    }

