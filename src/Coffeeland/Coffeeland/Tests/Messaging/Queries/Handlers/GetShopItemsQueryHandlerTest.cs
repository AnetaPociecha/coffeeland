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
        public void GetShopItems_CorrectData_Success()
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
        }
    }
}