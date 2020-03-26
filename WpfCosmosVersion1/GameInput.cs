using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCosmosVersion1
{
    class GameInput
    {
        public bool left, right, up, down, fire;

        public GameInput()
        {
            left = false;
            right = false;
            up = false;
            down = false;
            fire = false;
        }

        public void reset()
        {
            left = false;
            right = false;
            up = false;
            down = false;
            fire = false;
        }
    }
}
