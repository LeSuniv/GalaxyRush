﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyRush
{
    class Fusee_missile : skinFusee
    {
        private DefaultFusee defaultFusee;

        public Fusee_missile(DefaultFusee defaultFusee)
        {
            this.defaultFusee = defaultFusee;
        }

        public string getSkin()
        {
            return "\\images\\fusée_missile.png";
        }
    }
}
