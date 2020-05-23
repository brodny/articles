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

## And we have to evolve

Let's just say you are about to implement an enhancement to the system. You have to write a class that calculates the traffic ticket that the driver would get for driving at certain speed. You plan to go with TDD, so you start with tests.

You create a new TicketCalculator class and a test class for it. The calculator would need a configuration provider for sure. It would be much more useful to provide a mock instead of a real provider. That would allow to both avoid providing an app.config file with specific values and create specific conditions for the tests (e.g. to check the boundary conditions).
Unfortunately, current implementation does not allow for that. You would have to provide a real instance of a TrafficCode class and it doesn't allow to set SpeedLimits value. We would have to somehow change the values in app.config file while the tests run or somehow change it for a base class. Both of them seem crappy to be honest.

## Dependency inversion

What is dependency inversion? It's the last (but definitely not least) of the SOLID principles. It's the rule that says that abstractions should not depend on implementation. It should be the other way around - the implementation should depend on abstractions.
Why is it important in our case? We have a configuration provider hidden by an abstraction (interface) that depends on conrete class - TrafficCode. We are going to fix that violation and loose coupling by introducing an abstraction for TrafficCode. This way we will be able to provide a valid mock for that.
The first thing we would need to do is to add an empty ITrafficCode interface and make TrafficCode class to implement it.
Now we can make our abstraction (IConfigurationProvider) to depend on other abstraction - the newly created ITrafficCode. However, this change breaks the existing code as it is not building anymore because of lack of SpeedLimits property in ITrafficCode. This breaks our tests. This is why we need to proceed with refactor and pull the member up to make it an interface member. Rebuild, run the tests - we're still OK. That was quite an easy refactor.
We are now able to create a mock for TrafficCode. But what we've done is we just moved the original problem one step further. It is now ITrafficCode that depends on an implementation. We have to repeat the previous steps - for SpeedLimitCollection now.
Create an empty interface, use that interface in ITrafficCode, pull the required members up. Rebuild, run tests. The tests fail, which is why we needed to change the implementation to still use the concrete class in a private property and return that property in a public property which is required by an interface implementation. Nevertheless, another coupling loosed a bit.
And once again we have to reply the steps for ISpeedLimits interface to get rid of dependency on concrete SpeedLimitElement class.

Now we can create mocks freely and set up any conditions in tests. Respecting dependency inversion rule has helped to loose coupling and helped to provide an architecture that is easier to test. It also allows for more flexibility now as configuration does not need to be read from app.config file and for example downloaded from a web service instead only by providing a new implementation of the interfaces and such a change should be transparent to the clients.