using System;

namespace FleetManagement
{
    public class PolandTicketCalculator
    {
        private IConfigurationProvider configurationProvider;

        public PolandTicketCalculator(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider
                ?? throw new ArgumentNullException(nameof(configurationProvider));
        }
    }
}