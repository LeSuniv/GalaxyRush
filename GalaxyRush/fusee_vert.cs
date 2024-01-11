using System.Runtime.Intrinsics.X86;

namespace GalaxyRush
{
    class fusee_vert : skinFusee
    {
        private DefaultFusee defaultFusee;

        public fusee_vert(DefaultFusee defaultFusee)
        {
            this.defaultFusee = defaultFusee;
        }

        public string getSkin()
        {
            return "P:\\SAE 01.01-02\\GalaxyRush\\GalaxyRush\\images\\fusée_vert.png";
        }
    }
}
