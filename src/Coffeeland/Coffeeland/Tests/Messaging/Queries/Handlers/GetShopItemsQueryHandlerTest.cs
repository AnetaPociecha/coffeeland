using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;
using Coffeeland.Messaging.Dtos;
using Coffeeland.Messaging.Queries.Handlers;
using Coffeeland.Messaging.Queries.Queries;
using Coffeeland.Session;
using Coffeeland.Tests.TestsShared;
using NUnit.Framework;

namespace Coffeeland.Tests.Messaging.Queries.Handlers
{
    [TestFixture]
    public class GetShopItemsQueryHandlerTest
    {
        [Test]
        public void GetShopItems_FilledDatabase_Success()
        {
            int productsCount = 2;

            DatabaseQueryProcessor.Erase();
            Shared.FillTheDatabase();
            
            var getShopItemsQuery = new GetShopItemsQuery
            {
            };

            var handler = new GetShopItemsQueryHandler();
            var result = (ShopItemsDto) handler.Handle(getShopItemsQuery);

            DatabaseQueryProcessor.Erase();

            Assert.IsTrue(result.isSuccess);
            Assert.AreEqual(productsCount, result.shopItems.Length);
            Assert.AreEqual(result.shopItems[0].name, "Lavazza");
            Assert.AreEqual(result.shopItems[0].price, 1500);
            Assert.AreEqual(result.shopItems[1].name, "Vergnano");
            Assert.AreEqual(result.shopItems[1].price, 2500);
        }

        [Test]
        public void GetShopItems_EmptyDatabase_Success()
        {
            int productsCount = 0;

            DatabaseQueryProcessor.Erase();

            var getShopItemsQuery = new GetShopItemsQuery
            {
            };

            var handler = new GetShopItemsQueryHandler();
            var result = (ShopItemsDto)handler.Handle(getShopItemsQuery);

            DatabaseQueryProcessor.Erase();

            Assert.IsTrue(result.isSuccess);
            Assert.AreEqual(productsCount, result.shopItems.Length);
        }
    }
}