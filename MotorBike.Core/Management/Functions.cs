using System;

namespace BikeCommander.MotorBike.Core.Management
{
    class Functions
    {

        #region ABS_MANAGER
        private static void ABSModify(double[] Values)
        {
            if (Values.Length != 0)
            {
                Console.WriteLine("ABS_MODIFIED: {0}", Values[0].ToString());

            }
        }

        public static double ABSValue()
        {
            double ABSValue = 3.14;
            double[] ABSModifiers = new double[] { 1.034345435, 3.348f, 2, 7 };

            if(ABSValue >= ABS.Constructor.ABSValues["THRESHOLD"])
            {
                ABSModify(ABSModifiers);
            }

            return ABSValue;
        }
        #endregion

        #region LIGHT_MANAGER
        public static bool ActivateLights()
        {
            return true;
        }

        public static bool DisableLights()
        {
            return true;
        }
        #endregion

    }
}
