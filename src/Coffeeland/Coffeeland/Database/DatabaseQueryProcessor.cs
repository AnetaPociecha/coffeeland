using System;
using System.Text;
using System.Data;
using System.Diagnostics;
using Coffeeland.Database.Records;
using System.Collections.Generic;
using Coffeeland.Tests.TestsShared;
using MySql.Data.MySqlClient;
using System.IO;

namespace Coffeeland.Database
{
    public static class DatabaseQueryProcessor
    {
        private static Connector connector;

        static DatabaseQueryProcessor()
        {
            connector = new Connector();
        }
        //-------- GET --------
        
        public static List<ProductRecord> GetProducts()
        {
            List<ProductRecord> productRecords = new List<ProductRecord>();

            String query = "SELECT * FROM products";
            DataTable products = connector.ExecuteQuery(query);

            foreach (DataRow dr in products.Rows)
            {
                var record = new ProductRecord();
                record.Fill(dr);
                productRecords.Add(record);
            }
            return productRecords;
        }

        public static ProductRecord GetProduct(int productId)
        {
            String query = $"SELECT * FROM products WHERE productId={productId}";
            DataTable addresses = connector.ExecuteQuery(query);
            var record = new ProductRecord();
            record.Fill(addresses.Rows[0]);
            return record;
        }

        public static List<AddressRecord> GetAddresses(int clientId)
        {
            List<AddressRecord> addressRecords = new List<AddressRecord>();
            String query = $"SELECT * FROM addresses WHERE clientId={clientId}";
            DataTable addresses = connector.ExecuteQuery(query);

            foreach (DataRow dr in addresses.Rows)
            {
                var record = new AddressRecord();
                record.Fill(dr);
                addressRecords.Add(record);
            }
            return addressRecords;
        }

        public static AddressRecord GetAddress(int addressId)
        {
            String query = $"SELECT * FROM addresses WHERE addressId={addressId}";
            DataTable addresses = connector.ExecuteQuery(query);
            var record = new AddressRecord();
            if (addresses.Rows.Count == 0)
            {
                return null;
            }
            record.Fill(addresses.Rows[0]);
            return record;
        }

        public static AddressRecord GetAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber, String apartmentNumber)
        {
            var query = $"SELECT * FROM addresses WHERE clientId={clientId} and country='{country}' and city='{city}' and street='{street}' and ZIPCode={ZIPCode} and buildingNumber={buildingNumber} and apartmentNumber='{apartmentNumber}'";
            var addresses = connector.ExecuteQuery(query);
            if (addresses.Rows.Count == 0)
                return null;
            var record = new AddressRecord();
            record.Fill(addresses.Rows[0]);
            return record;
        }

        public static List<ClientInfoRecord> GetClients()
        {
            List<ClientInfoRecord> clientsRecords = new List<ClientInfoRecord>();

            String query = "SELECT * FROM clients";
            DataTable clients = connector.ExecuteQuery(query);

            foreach (DataRow dr in clients.Rows)
            {
                var record = new ClientInfoRecord();
                record.Fill(dr);
                clientsRecords.Add(record);
            }
            return clientsRecords;
        }

        public static ClientInfoRecord GetClient(string email, string password)
        {
            String query = $"SELECT * FROM clients WHERE email='{email}' and password='{password}'";
            DataTable clients = connector.ExecuteQuery(query);
            if (clients.Rows.Count == 0)
                return null;
            var record = new ClientInfoRecord();
            record.Fill(clients.Rows[0]);
            return record;
        }

        public static ClientInfoRecord GetClient(int clientId)
        {
            String query = $"SELECT * FROM clients WHERE clientId='{clientId}'";
            DataTable clients = connector.ExecuteQuery(query);
            if (clients.Rows.Count == 0)
                return null;
            var record = new ClientInfoRecord();
            record.Fill(clients.Rows[0]);
            return record;
        }


        public static List<ComplaintRecord> GetComplaints()
        {
            List<ComplaintRecord> complaintRecords = new List<ComplaintRecord>();
            String query = "SELECT orderId, workerId, description, DATE_FORMAT(openDate,'%Y-%m-%d') AS openDate, isClosed  FROM complaints ";
            DataTable complaints = connector.ExecuteQuery(query);

            foreach (DataRow dr in complaints.Rows)
            {
                ComplaintRecord record = new ComplaintRecord();
                record.Fill(dr);
                complaintRecords.Add(record);
            }
            return complaintRecords;
        }

        public static ComplaintRecord GetComplaint(int orderId)
        {
            String query = $"SELECT orderId, workerId, description, DATE_FORMAT(openDate,'%Y-%m-%d') AS openDate, isClosed  FROM complaints WHERE orderId='{orderId}'";
            DataTable complaint = connector.ExecuteQuery(query);

            if (complaint.Rows.Count == 0)
                return null;

            var record = new ComplaintRecord();
            record.Fill(complaint.Rows[0]);

            return record;
        }

        public static List<OrderEntryRecord> GetOrderEntries(int orderId)
        {
            List<OrderEntryRecord> orderEntryRecords = new List<OrderEntryRecord>();

            String query = $"SELECT * FROM order_entries WHERE orderId={orderId}";
            DataTable orderEntries = connector.ExecuteQuery(query);

            foreach (DataRow dr in orderEntries.Rows)
            {
                OrderEntryRecord record = new OrderEntryRecord();
                record.Fill(dr);
                orderEntryRecords.Add(record);
            }
            return orderEntryRecords;
        }

        public static List<OrderRecord> GetOrders(int clientId)
        {
            List<OrderRecord> ordersRecords = new List<OrderRecord>();

            String query = $"SELECT orderId, clientId, workerId, addressId, status, DATE_FORMAT(openDate,'%Y-%m-%d') as openDate, DATE_FORMAT(closeDate,'%Y-%m-%d') as closeDate FROM orders WHERE clientId={clientId}";
            DataTable orders = connector.ExecuteQuery(query);

            foreach (DataRow dr in orders.Rows)
            {
                var record = new OrderRecord();
                record.Fill(dr);
                ordersRecords.Add(record);
            }
            return ordersRecords;

        }

        public static OrderRecord GetOrder(int orderId)
        {
            String query = $"SELECT orderId, clientId, workerId, addressId, status, DATE_FORMAT(openDate,'%Y-%m-%d') as openDate, DATE_FORMAT(closeDate,'%Y-%m-%d') as closeDate FROM orders WHERE orderId={orderId}";

            var order = connector.ExecuteQuery(query);
            var record = new OrderRecord();

            if (order.Rows.Count == 0)
                return null;
            
            record.Fill(order.Rows[0]);
            return record;
        }

        //-------- CREATE --------
        public static int CreateNewClient(String email, String firstName, String lastName, String password, String newsletterEmail)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(clientId)+1 FROM clients");
            int.TryParse(dataTableId.Rows[0]["MAX(clientId)+1"].ToString(), out int clientId);

            String command = $"INSERT INTO clients(clientId,email,firstName,lastName,password,newsletterEmail) VALUES({clientId},'{email}','{firstName}','{lastName}','{password}','{newsletterEmail}')";
            connector.ExecuteCommand(command);
            return clientId;
        }

        public static int CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber, String apartmentNumber)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int.TryParse(dataTableId.Rows[0]["MAX(addressId)+1"].ToString(), out int addressId);

            String command = $"INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,apartmentNumber,isActive) VALUES({addressId},{clientId},'{country}','{city}','{street}',{ZIPCode},{buildingNumber},'{apartmentNumber}',1)";
            connector.ExecuteCommand(command);
            return addressId;
        }

       
        public static int CreateNewComplaint(int orderId, int workerId, String description, String openDate, bool isClosed)
        {
            String command = "INSERT INTO complaints(orderId,workerId,description,openDate,isClosed) VALUES "
                + "(" + orderId + ",'" + workerId + "','" + description + "', DATE '"
                + openDate + "'," + (isClosed ? 1 : 0) + ")";
            connector.ExecuteCommand(command);
            return orderId;
        }

        public static int CreateNewOrderEntry(int orderId, int productId, int quantity)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(orderEntryId)+1 FROM order_entries");
            int.TryParse(dataTableId.Rows[0]["MAX(orderEntryId)+1"].ToString(), out int orderEntryId);

            String command = "INSERT INTO order_entries(orderEntryId,orderId,productId,quantity) VALUES "
                + "(" + orderEntryId +"," + orderId + "," + productId + "," + quantity + ")";
            connector.ExecuteCommand(command);
            return orderEntryId;
        }

        public static string CreateNewPayment(string paymentId, int orderId, int amount, String openDate)
        {
            String command = "INSERT INTO payments(paymentId,orderId,amount,openDate) VALUES "
                + "('" + paymentId + "'," + orderId + "," + amount + ", DATE '"
                + openDate + "')";
            connector.ExecuteCommand(command);
            return paymentId;
        }

        public static int CreateNewProduct(String name, int price, String imagePath, String productType, String description)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(productId)+1 FROM products");
            int.TryParse(dataTableId.Rows[0]["MAX(productId)+1"].ToString(),out int productId);

            String command = "INSERT INTO products(productId,name,price,imagePath,productType,description) VALUES "
                + "(" + productId + ",'" + name + "'," + price + ",'"
                + imagePath + "','" + productType + "','" + description + "')";
            connector.ExecuteCommand(command);
            return productId;
        }

        public static int CreateNewOrder(int clientId, int workerId, int addressId, int status, String openDate)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(orderId)+1 FROM orders");
            int.TryParse(dataTableId.Rows[0]["MAX(orderId)+1"].ToString(), out int orderId);


            String command = "INSERT INTO orders(orderId,clientId,workerId,addressId,status,openDate) VALUES "
                + "(" + orderId + "," + clientId + "," + workerId + "," + addressId + "," + status + ", DATE '"
                + openDate + "')";
            connector.ExecuteCommand(command);
            return orderId;
        }

        public static int CreateNewWorker(WorkerRole role, String email, String password)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(workerId)+1 FROM workers");
            int.TryParse(dataTableId.Rows[0]["MAX(workerId)+1"].ToString(), out int workerId);

            String command = "INSERT INTO workers(workerId,role,email,password) VALUES "
                + "(" + workerId + ",'" + role + "','" + email + "','"
                + password + "')";
            connector.ExecuteCommand(command);
            return workerId;
        }

        //-------- UPDATE --------
        public static bool UpdateClientCredentials(int clientId, String whatToChange, String value)
        {
            String command = "UPDATE clients SET " + whatToChange + "='" + value + "' WHERE clientId=" + clientId;
            return connector.ExecuteCommand(command);
        }
        
        public static bool UpdateProductDescription(int productId, String value)
        {
            String command = "UPDATE products SET description='" + value + "' WHERE productId=" + productId;
            return connector.ExecuteCommand(command);
        }

        public static bool UpdateAddress(int addressId, bool isActive)
        {
            String command = $"UPDATE addresses SET isActive={(isActive ? 1 : 0)} WHERE addressId={addressId}";
            return connector.ExecuteCommand(command);
        }

        public static bool UpdateOrder(int orderId, String closeDate)
        {
            String command = $"UPDATE orders SET closeDate='{closeDate}'WHERE orderId={orderId}";
            return connector.ExecuteCommand(command);
        }

        public static bool UpdateOrder(int orderId, int status)
        {
            String command = $"UPDATE orders SET status={status} WHERE orderId={orderId}";
            return connector.ExecuteCommand(command);
        }

        public static bool UpdateComplaint(int orderId, bool isClosed)
        {
            String command = $"UPDATE complaints SET isClosed={(isClosed ? 1 : 0)} WHERE orderId={orderId}";
            return connector.ExecuteCommand(command);
        }

        public static bool Erase()
        {
            return connector.Erase();
        }

    }
}
