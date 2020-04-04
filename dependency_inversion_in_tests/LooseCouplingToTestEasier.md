# Loose coupling to test easier

Tight coupling is a situation where a component has a lot of knowledge of other separate components. It usually implies that it depends on too much details of other components.

A situation like that could really complicate creating unit tests. It could be really hard to create an instance of a tightly-coupled component in a test code. Imagine that in order to test your class, you have to create a few different classes that communicate with the database, read files and perform web requests. Oh, of course, mocking concrete classes is not an option. It is of course possible, but requires a lot of setup and boilerplate code and could be unreliable (e.g. because said web server is currently unavailable).

Such a situation is probably not very often seen while creating a new code, especially using TDD approach. Much more often you can find it in a legacy code. You are asked to fix some bug or provide extra functionality to some part of a system and you would like to test that. You see that the piece of system has not yet been tested, so you think about providing unit tests for the unchanged parts of the component as well... and then you hit the wall. The wall built of all these other classes and dependencies.

## Current state of the system

Let's say you have a system that needs an information about speed limits in several countries. It is contained in configuration file.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="trafficCode" type="FleetManagement.Configuration.TrafficCode, FleetManagement" />
  </configSections>
  <trafficCode>
    <speedLimits>
      <country name="Poland" limit="90" unit="Kmh" />
      <country name="UK" limit="60" unit="Mph" />
    </speedLimits>
  </trafficCode>
</configuration>
```

All that data is being exposed via a configuration provider.

```c#
public interface IConfigurationProvider
{
    TrafficCode TrafficCode { get; }
}

public class ConfigurationProvider : IConfigurationProvider
{
    public TrafficCode TrafficCode
    {
        get
        {
            return ConfigurationManager.GetSection("trafficCode") as TrafficCode;
        }
    }
}

public class TrafficCode : ConfigurationSection
{
    private const string SpeedLimitsPropertyName = "speedLimits";

    [ConfigurationProperty(SpeedLimitsPropertyName)]
    public SpeedLimitCollection SpeedLimits
    {
        get
        {
            return (SpeedLimitCollection)base[SpeedLimitsPropertyName];
        }
    }
}

[ConfigurationCollection(typeof(SpeedLimitElement), AddItemName = "country")]
public class SpeedLimitCollection : ConfigurationElementCollection
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

public class SpeedLimitElement : ConfigurationElement
{
    private const string NamePropertyName = "name";
    private const string LimitPropertyName = "limit";
    private const string UnitPropertyName = "unit";

    [ConfigurationProperty(NamePropertyName, IsRequired = true, IsKey = true)]
    public string CountryName
    {
        get { return (string)base[NamePropertyName]; }
    }

    [ConfigurationProperty(LimitPropertyName, IsRequired = true)]
    public int Limit
    {
        get { return (int)base[LimitPropertyName]; }
    }

    [ConfigurationProperty(UnitPropertyName, IsRequired = true)]
    public SpeedUnit Unit
    {
        get { return (SpeedUnit)base[UnitPropertyName]; }
    }
}

public enum SpeedUnit
{
    Undefined,
    Kmh,
    Mph,
}
```