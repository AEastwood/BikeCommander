using System.Collections.Generic;


namespace BikeCommander.Framework.DataTypes
{
    class Sensor
    {
        public string sensorName { get; set; } = "DEFAULT_SENSOR";
        public dynamic sensorValue { get; set; } = 0;
        public List<dynamic> sensorValues = new List<dynamic>();
        public Dictionary<string, dynamic> sensorMapping = new Dictionary<string, dynamic>();
    }

}
