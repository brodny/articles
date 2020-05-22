using System;
using System.Configuration;

namespace FleetManagement.Configuration
{

    [ConfigurationCollection(typeof(SpeedLimitElement), AddItemName = "country")]
    public class SpeedLimitCollection : ConfigurationElementCollection, ISpeedLimitCollection
    {
        private const string PropertyName = "country";

        public new SpeedLimitElement this[string countryName]
        {
            get { return (SpeedLimitElement)BaseGet(countryName); }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SpeedLimitElement)element).CountryName;
        }

        protected override string ElementName
        {
            get { return PropertyName; }
        }

        protected override bool IsElementName(string elementName)
        {
            return PropertyName.Equals(elementName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new SpeedLimitElement();
        }
    }
}