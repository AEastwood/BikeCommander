using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeCommander.MotorBike.Core.Diagnostic.Engine
{
    class EngineDiagnostics
    {

        internal static int EngineCheck()
        {
            MotorBike.Core.MotorBikeCore.SendMessage("Checking Engine");
            MotorBike.Core.MotorBikeCore.SendMessage("Engine OK!");

            return 1;
        }

    }
}
