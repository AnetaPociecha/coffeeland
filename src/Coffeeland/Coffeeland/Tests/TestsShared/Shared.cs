using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coffeeland.Database;

namespace Coffeeland.Tests.TestsShared
{
    public static class Shared
    {
        public static void FillTheDatabase()
        {
            DatabaseQueryProcessor.CreateNewProduct("Lavazza", 1500, "./img.jpg", "100% Arabica", "Good");
            DatabaseQueryProcessor.CreateNewProduct("Vergnano", 2500, "./img.jpg", "100% Robusta", "Strong");

            DatabaseQueryProcessor.CreateNewWorker(WorkerRole.a, "worker1@gmail.com", "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9");
            DatabaseQueryProcessor.CreateNewWorker(WorkerRole.b, "worker2@gmail.com", "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9");

            DatabaseQueryProcessor.CreateNewClient("jane_doe@gmail.com", "Jane", "Doe", "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9", "jane_doe@gmail.com");
            DatabaseQueryProcessor.CreateNewAddress(0, "Poland", "Gdynia", "Rzemieslnicza", 30445, 12, "1a");
            DatabaseQueryProcessor.CreateNewAddress(0, "Poland", "Warsaw", "Grodzka", 25487, 23, "");

            DatabaseQueryProcessor.CreateNewOrder(0, 0, 0, 1, "2018-05-12");
            DatabaseQueryProcessor.CreateNewOrderEntry(0, 0, 1);
            DatabaseQueryProcessor.CreateNewOrderEntry(0, 1, 2);

            DatabaseQueryProcessor.CreateNewOrder(0, 1, 1, 0, "2018-10-12");
            DatabaseQueryProcessor.CreateNewOrderEntry(1,0,1);
            DatabaseQueryProcessor.UpdateOrder(1, 1);
            DatabaseQueryProcessor.UpdateOrder(1, "2018-10-14");
            DatabaseQueryProcessor.CreateNewComplaint(1, 1, "I am dissatisfied", "2018-10-15", true);

            DatabaseQueryProcessor.CreateNewClient("john_doe@gmail.com", "John", "Doe", "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9", "john_doe@gmail.com");
            DatabaseQueryProcessor.CreateNewAddress(1, "Poland", "Cracow", "Krakowska", 30000, 1, "");

            DatabaseQueryProcessor.CreateNewOrder(1,0,2,0,"2018-06-12");
            DatabaseQueryProcessor.CreateNewOrderEntry(2,1,1);

            DatabaseQueryProcessor.CreateNewClient("anne_bottom@gmail.com", "Anne", "Bottom", "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9", "anne_bottom@gmail.com");
        }
            
    }
}