using Fejlesztesi_Mintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fejlesztesi_Mintak.Entities
{
    public class Present : Toy
    {

        public SolidBrush ribbonColor { get; private set; }
        public SolidBrush boxColor { get; private set; }

        public Present(Color color, Color color1)
        {
            ribbonColor = new SolidBrush(color);
            boxColor = new SolidBrush(color1);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(boxColor, 0, 0, Width, Height);
            g.FillRectangle(ribbonColor, 0, Height/5*2, Width, Height / 5);
            g.FillRectangle(ribbonColor, Width/5*2, 0, Width / 5, Height);
        }
    }
}
