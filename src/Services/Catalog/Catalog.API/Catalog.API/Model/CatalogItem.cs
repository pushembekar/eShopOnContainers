using Catalog.API.Infrastructure.Exceptions;
using System;

namespace Catalog.API.Model
{
    public class CatalogItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        public int CatalogTypeId { get; set; }

        public CatalogType CatalogType { get; set; }

        public int CatalogBrandId { get; set; }

        public CatalogBrand CatalogBrand { get; set; }

        // Quantity in stock
        public int AvailableStock { get; set; }

        // Available stock at which we should reorder
        public int RestockThreshold { get; set; }

        // Max number of units that can be in-stock at any time (due to constraints in warehouses)
        public int MaxStockThreshold { get; set; }

        /// <summary>
        /// True if item is on reorder
        /// </summary>
        public bool OnReorder { get; set; }

        /// <summary>
        /// 1. Decrement the quantity of an item in inventory until restock threshold is reached
        /// 2. If restock threshold is reached, generate a restock request
        /// 3. If there is sufficient stock in inventory, then the integer returned is same as 'quantityDesired' parameter
        /// 4. If the inventory falls short then the method will return whatever stock is available
        /// 5. In the condition above, it is the responsibility of the client to determine if the amt returned is same as 'quantityDesired'
        /// 6. The method does not take in negative numbers
        /// </summary>
        /// <param name="quantityDesired"></param>
        /// <returns></returns>
        public int RemoveStock(int quantityDesired)
        {
            if (AvailableStock == 0)
            {
                throw new CatalogDomainException($"Empty stock, product item {Name} is sold out");
            }

            if (quantityDesired <= 0)
            {
                throw new CatalogDomainException($"Item units desired must be greater than zero");
            }

            int removed = Math.Min(quantityDesired, AvailableStock);

            AvailableStock -= removed;

            return removed;
        }

        /// <summary>
        /// 1. Increments the quantity of a particular item in inventory
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns>int: Returns the quantity that has been added to stock</returns>
        public int AddStock(int quantity)
        {
            int original = AvailableStock;

            // The quantity that the client is trying to add to stock is greater than what can be physically accommodated in the warehouse
            if((AvailableStock+quantity) > MaxStockThreshold)
            {
                // For now, this method only adds new units up to maximum stock threshold
                // TODO: Include tracking for the remaining units and store information about overstock elsewhere
                AvailableStock += MaxStockThreshold = AvailableStock;
            }
            else
            {
                AvailableStock += quantity;
            }

            OnReorder = false;

            return AvailableStock - original;
        }
    }
}
