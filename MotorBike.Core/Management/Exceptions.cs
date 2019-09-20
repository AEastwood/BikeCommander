using System;

namespace BikeCommander.MotorBike.Core.Management
{
    [Serializable]
    class EngineNoPowerException : Exception
    {
        public EngineNoPowerException()
        {
        }

        public EngineNoPowerException(string message) : base(message)
        {
        }

        public EngineNoPowerException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
