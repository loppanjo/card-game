using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public partial class GameWindow : Component
    {
        

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
