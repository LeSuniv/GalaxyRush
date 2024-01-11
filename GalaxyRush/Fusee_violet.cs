using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyRush
{
    class Fusee_violet : skinFusee
    {
        private DefaultFusee defaultFusee;

        public Fusee_violet(DefaultFusee defaultFusee)
        {
            this.defaultFusee = defaultFusee;
        }

        public string getSkin()
        {
            return "P:\\SAE 01.01-02\\GalaxyRush\\GalaxyRush\\images\\fusée_violet.png";
        }
    }
}
