using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fejlesztesi_Mintak.Entities
{
    internal class BallFactory
    {
        public Ball CreateNew()
        {
            return new Ball();
        }
    }
}
