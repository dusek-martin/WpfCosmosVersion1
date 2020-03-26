using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCosmosVersion1
{
    interface IGravityObject
    {
        Vector Position { get; set; }
        
        void Update(double tickTime, Vector gravityForce);

        double GetWeight();

    }
}
