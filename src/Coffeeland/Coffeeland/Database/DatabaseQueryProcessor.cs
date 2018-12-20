using System;
using System.Text;
using System.Data;
using System.Diagnostics;
using Coffeeland.Database.Records;
using System.Collections.Generic;

namespace Coffeeland.Database
{
    public static class DatabaseQueryProcessor
    {
        static Connector connector;

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
                ProductRecord record = new ProductRecord();
                record.Fill(dr);
                productRecords.Add(record);
            }
            return productRecords;
        }

        public static ProductRecord GetProduct(int productId)
        {
            String query = $"SELECT * FROM products WHERE productId={productId}";
            DataTable addresses = connector.ExecuteQuery(query);
            ProductRecord record = new ProductRecord();
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
                AddressRecord record = new AddressRecord();
                record.Fill(dr);
                addressRecords.Add(record);
            }
            return addressRecords;
        }

        public static AddressRecord GetAddress(int addressId)
        {
            String query = $"SELECT * FROM addresses WHERE addressId={addressId}";
            DataTable addresses = connector.ExecuteQuery(query);
            AddressRecord record = new AddressRecord();
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
                ClientInfoRecord record = new ClientInfoRecord();
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
            ClientInfoRecord record = new ClientInfoRecord();
            if (clients.Rows.Count == 0)
            {
                return null;
            }
            record.Fill(clients.Rows[0]);
            return record;
        }

        public static List<ComplaintRecord> GetComplaints()
        {
            List<ComplaintRecord> complaintRecords = new List<ComplaintRecord>();
            String query = "SELECT orderId, workerId, description, DATE_FORMAT(openDate,'%Y-%m-%d') AS openDate, isClosed  FROM complaints";
            DataTable complaints = connector.ExecuteQuery(query);

            foreach (DataRow dr in complaints.Rows)
            {
                ComplaintRecord record = new ComplaintRecord();
                record.Fill(dr);
                complaintRecords.Add(record);
            }
            return complaintRecords;
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
                OrderRecord record = new OrderRecord();
                record.Fill(dr);
                ordersRecords.Add(record);
            }
            return ordersRecords;

        }


        //-------- CREATE --------
        public static bool CreateNewClient(String email, String firstName, String lastName, String password, String newsletterEmail)
        {
            
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(clientId)+1 FROM clients");
            int.TryParse(dataTableId.Rows[0]["MAX(clientId)+1"].ToString(), out int clientId);

            String command = $"INSERT INTO clients(clientId,email,firstName,lastName,password,newsletterEmail) VALUES({clientId},'{email}','{firstName}','{lastName}','{password}','{newsletterEmail}')";

            return connector.ExecuteCommand(command);
        }

        public static bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber, String apartmentNumber)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int.TryParse(dataTableId.Rows[0]["MAX(addressId)+1"].ToString(), out int addressId);

            String command = $"INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,apartmentNumber,isActive) VALUES({addressId},{clientId},'{country}','{city}','{street}',{ZIPCode},{buildingNumber},'{apartmentNumber}',1)";
            return connector.ExecuteCommand(command);
        }

       
        public static bool CreateNewComplaint(int orderId, int workerId, String description, String openDate, bool isClosed)
        {
            String command = "INSERT INTO complaints(orderId,workerId,description,openDate,isClosed) VALUES "
                + "(" + orderId + ",'" + workerId + "','" + description + "', DATE '"
                + openDate + "'," + (isClosed ? 1 : 0) + ")";
            return connector.ExecuteCommand(command);
        }

        public static bool CreateNewOrderEntry(int orderId, int productId, int amount)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(orderEntryId)+1 FROM order_entries");
            int.TryParse(dataTableId.Rows[0]["MAX(orderEntryId)+1"].ToString(), out int orderEntryId);

            String command = "INSERT INTO order_entries(orderEntryId,orderId,productId,amount) VALUES "
                + "(" + orderEntryId +"," + orderId + "," + productId + "," + amount + ")";
            return connector.ExecuteCommand(command);
        }

        public static bool CreateNewPayment(int orderId, String openDate)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(paymentId)+1 FROM payments");
            int.TryParse(dataTableId.Rows[0]["MAX(paymentId)+1"].ToString(), out int paymentId);

            DataTable dataTableAmount = connector.ExecuteQuery("SELECT SUM(oe.amount*p.price) FROM order_entries oe NATURAL JOIN products p WHERE oe.orderId=" + orderId);
            int.TryParse(dataTableAmount.Rows[0]["SUM(oe.amount*p.price)"].ToString(), out int amount);

            String command = "INSERT INTO payments(paymentId,orderId,amount,openDate) VALUES "
                + "(" + paymentId + "," + orderId + "," + amount + ", DATE '"
                + openDate + "')";
            return connector.ExecuteCommand(command);
        }

        public static bool CreateNewProduct(String name, int price, String imagePath, String productType, String description)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(productId)+1 FROM products");
            int.TryParse(dataTableId.Rows[0]["MAX(productId)+1"].ToString(),out int productId);

            String command = "INSERT INTO products(productId,name,price,imagePath,productType,description) VALUES "
                + "(" + productId + ",'" + name + "'," + price + ",'"
                + imagePath + "','" + productType + "','" + description + "')";
            return connector.ExecuteCommand(command);
        }

        public static bool CreateNewOrder(int clientId, int workerId, int addressId, int status, String openDate)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(orderId)+1 FROM orders");
            int.TryParse(dataTableId.Rows[0]["MAX(orderId)+1"].ToString(), out int orderId);


            String command = "INSERT INTO orders(orderId,clientId,workerId,addressId,status,openDate) VALUES "
                + "(" + orderId + "," + clientId + "," + workerId + "," + addressId + "," + status + ", DATE '"
                + openDate + "')";
            return connector.ExecuteCommand(command);
        }

        public static bool CreateNewWorker(WorkerRole role, String email, String password)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(workerId)+1 FROM workers");
            int.TryParse(dataTableId.Rows[0]["MAX(workerId)+1"].ToString(), out int workerId);

            String command = "INSERT INTO workers(workerId,role,email,password) VALUES "
                + "(" + workerId + ",'" + role + "','" + email + "','"
                + password + "')";
            return connector.ExecuteCommand(command);
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
