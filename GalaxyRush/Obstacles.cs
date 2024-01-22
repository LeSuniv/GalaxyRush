using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace GalaxyRush
{
    internal class Obstacles
    {
        private Rectangle asteroide;
        private Rectangle ovni;

        public Rectangle Asteroide
        {
            get
            {
                return asteroide;
            }

            set
            {
                this.asteroide = value;
            }
        }

        public Rectangle Ovni
        {
            get
            {
                return this.ovni;
            }

            set
            {
                this.ovni = value;
            }
        }
    }
}
