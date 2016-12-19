using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
        }

        public string Ip { get; private set; }
        public int Port { get; private set; }
        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Ip = tbxIp.Text;
            Port = (int)numPort.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
