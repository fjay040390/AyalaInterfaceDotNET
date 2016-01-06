using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ayala_Interface_dotNet
{
    public partial class frmReprocess : Form
    {
        public frmReprocess()
        {
            InitializeComponent();
        }

        private void btnReprocess_Click(object sender, EventArgs e)
        {

        }

        private void monthStart_DateChanged(object sender, DateRangeEventArgs e)
        {
            lblStart.Text = monthStart.SelectionStart.ToShortDateString();
        }

        private void monthEnd_DateChanged(object sender, DateRangeEventArgs e)
        {
            lblEnd.Text = monthEnd.SelectionStart.ToShortDateString();
        }

        private void frmReprocess_Load(object sender, EventArgs e)
        {

        }

    }
}
