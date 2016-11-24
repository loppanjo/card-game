using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class GameWindow : Control
    {
        public GameWindow()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        public Game Game { get; set; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Game.Draw(pe.Graphics);
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }
    }
}
