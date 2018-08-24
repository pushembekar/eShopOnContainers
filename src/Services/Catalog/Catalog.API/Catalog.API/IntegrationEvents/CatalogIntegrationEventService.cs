using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Events;

namespace Catalog.API.IntegrationEvents
{
    public class CatalogIntegrationEventService : ICatalogIntegrationEventService
    {
        //private readonly Func<DbConnection, IIntegrationEventLogService>


        public Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            throw new NotImplementedException();
        }

        public Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt)
        {
            throw new NotImplementedException();
        }
    }
}
