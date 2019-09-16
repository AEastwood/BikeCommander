using System.Collections.Generic;

namespace BikeCommander.Framework.DataTypes
{
    class Component
    {
        public string Name { get; set; } = "DEFAULT_COMPONENT";
        private dynamic componentValue { get; set; } = 0;
        private List<dynamic> componentValues = new List<dynamic>();
        private Dictionary<string, dynamic> componentMapping = new Dictionary<string, dynamic>();
        private Dictionary<string, dynamic> componentState = new Dictionary<string, dynamic>();

        public void AddComponentMapping(string Key, dynamic Value)
        {
            componentMapping.Add(Key, Value);
        }
    }
}
