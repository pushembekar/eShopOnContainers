using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.IntegrationEvents.Events
{
    // Integration events notes:
    // An event is "something that has happened in the past", therefor its name has to be
    // An integration event is an event that can cause side effects to other microservices, bounded contexts or external systems
    public class ProductPriceChangedIntegrationEvent : IntegrationEvent
    {
        public int ProductId { get; set; }

        public decimal NewPrice { get; set; }

        public decimal OldPrice { get; set; }

        public ProductPriceChangedIntegrationEvent(int productId, decimal newPrice, decimal oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
