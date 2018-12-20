using Coffeeland.Database;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Messaging.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coffeeland.Messaging.Queries.Handlers
{
    public class GetShopItemsQueryHandler : IQueryHandler<GetShopItemsQuery>
    {
        public IResult Handle(GetShopItemsQuery query)
        {
            var items = DatabaseQueryProcessor.GetProducts();

            if (items.Count == 0)
            {
                throw new Exception();
            }

            var shopItemsDto = new ShopItemDto[items.Count];
            for(var i = 0; i < items.Count; i++)
            {
                shopItemsDto[i] = new ShopItemDto();
                shopItemsDto[i].key = items[i].productId;
                shopItemsDto[i].name = items[i].name;
                shopItemsDto[i].price = items[i].price;
                shopItemsDto[i].img = items[i].imagePath;
                shopItemsDto[i].description = items[i].description;
            }

            return new ShopItemsDto()
            {
                isSuccess = true,
                shopItems = shopItemsDto
            };
        }
    }
}