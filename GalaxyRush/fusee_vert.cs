using System.Runtime.Intrinsics.X86;

namespace GalaxyRush
{
    class Fusee_vert : skinFusee
    {
        private DefaultFusee defaultFusee;

        public Fusee_vert(DefaultFusee defaultFusee)
        {
            this.defaultFusee = defaultFusee;
        }

        public string getSkin()
        {
            return "\\images\\fusée_vert.png";
        }
    }
}
