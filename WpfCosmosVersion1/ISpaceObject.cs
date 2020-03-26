using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfCosmosVersion1
{
    interface ISpaceObject
    {
        Vector Position { get; set; }
        Vector Speed { get; set; }
        double Radius { get; set; }
        
        void Update(double tickTime);
        void SolveWalls(float height, float width);
        Shape GetImage();
    }
}
