using System;
using System.Text;
using System.Data;
using System.Diagnostics;
using Coffeeland.Database.Records;
using System.Collections.Generic;

namespace Coffeeland.Database
{
    public class DatabaseQueryProcessor
    {
        Connector connector;

        public DatabaseQueryProcessor()
        {
           connector = new Connector();
        }


        //-------- GET --------
        public List<ProductRecord> GetProducts()
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

        public List<AddressRecord> GetAddresses(int clientId)
        {
            List<AddressRecord> addressRecords = new List<AddressRecord>();
            String query = "SELECT * FROM addresses WHERE clientId=" + clientId;
            DataTable addresses = connector.ExecuteQuery(query);

            foreach (DataRow dr in addresses.Rows)
            {
                AddressRecord record = new AddressRecord();
                record.Fill(dr);
                addressRecords.Add(record);
            }
            return addressRecords;
        }

        public List<ClientRecord> GetClients()
        {
            List<ClientRecord> clientsRecords = new List<ClientRecord>();

            String query = "SELECT * FROM clients";
            DataTable clients = connector.ExecuteQuery(query);

            foreach (DataRow dr in clients.Rows)
            {
                ClientRecord record = new ClientRecord();
                record.Fill(dr);
                clientsRecords.Add(record);
            }
            return clientsRecords;
        }

        public List<ComplaintRecord> GetComplaints()
        {
            List<ComplaintRecord> complaintRecords = new List<ComplaintRecord>();
            String query = "SELECT * FROM complaints";
            DataTable complaints = connector.ExecuteQuery(query);

            foreach (DataRow dr in complaints.Rows)
            {
                ComplaintRecord record = new ComplaintRecord();
                record.Fill(dr);
                complaintRecords.Add(record);
            }
            return complaintRecords;
        }

        public List<OrderEntryRecord> GetOrderEntries(int orderId)
        {
            List<OrderEntryRecord> orderEntryRecords = new List<OrderEntryRecord>();

            String query = "SELECT * FROM order_entries WHERE orderId=" + orderId;
            DataTable orderEntries = connector.ExecuteQuery(query);

            foreach (DataRow dr in orderEntries.Rows)
            {
                OrderEntryRecord record = new OrderEntryRecord();
                record.Fill(dr);
                orderEntryRecords.Add(record);
            }
            return orderEntryRecords;
        }

        public List<OrderRecord> GetOrders(int clientId)
        {
            List<OrderRecord> ordersRecords = new List<OrderRecord>();

            String query = "SELECT * FROM orders WHERE clientId=" + clientId;
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
        public bool CreateNewClient(String email, String firstName, String lastName, String password, bool newsletter)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(clientId)+1 FROM clients");
            int clientId = Convert.ToInt32(dataTableId.Rows[0]["MAX(clientId)+1"]);

            String command = $"INSERT INTO clients(clientId,email,firstName,lastName,password,newsletter) VALUES({clientId},'{email}','{firstName}','{lastName}','{password}',{(newsletter ? 1 : 0)})";

            //String command = "INSERT INTO clients(clientId,email,firstName,lastName,password,newsletter) VALUES "
            //    + "(" + clientId + ",'" + email + "','" + firstName + "','" + lastName + "','" + password + "'," + (newsletter ? 1 : 0) + ")";

            return connector.ExecuteCommand(command);
        }

        public bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber, String apartmentNumber)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int addressId = Convert.ToInt32(dataTableId.Rows[0]["MAX(addressId)+1"]);
            if (clientId == 0)
            {
                clientId = 40;
            }

            String command = "INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,apartmentNumber) VALUES "
                + "(" + addressId + ",'" + clientId + "','" + country + "','"
                + city + "','" + street + "'," + ZIPCode + "," + buildingNumber + ",'" + apartmentNumber + "')";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int addressId = Convert.ToInt32(dataTableId.Rows[0]["MAX(addressId)+1"]);


            String command = "INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber) VALUES "
                + "(" + addressId + ",'" + clientId + "','" + country + "','"
                + city + "','" + street + "'," + ZIPCode + "," + buildingNumber + ")";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber, String apartmentNumber, bool isDefault)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int addressId = Convert.ToInt32(dataTableId.Rows[0]["MAX(addressId)+1"]);

            String command = "INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,apartmentNumber,isDefault) VALUES "
                + "(" + addressId + ",'" + clientId + "','" + country + "','"
                + city + "','" + street + "'," + ZIPCode + "," + buildingNumber + ",'" + apartmentNumber + "'," + (isDefault ? 1 : 0) + ")";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber, bool isDefault)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int addressId = Convert.ToInt32(dataTableId.Rows[0]["MAX(addressId)+1"]);


            String command = "INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,isDefault) VALUES "
                + "(" + addressId + ",'" + clientId + "','" + country + "','"
                + city + "','" + street + "'," + ZIPCode + "," + buildingNumber + "," + (isDefault ? 1 : 0) + ")";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewComplaint(int orderId, int workerId, String description, String openDate, bool isClosed)
        {
            String command = "INSERT INTO complaints(orderId,workerId,description,openDate,isClosed) VALUES "
                + "(" + orderId + ",'" + workerId + "','" + description + "', DATE '"
                + openDate + "'," + (isClosed ? 1 : 0) + ")";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewOrderEntry(int orderId, int productId, int amount)
        {
            String command = "INSERT INTO order_entries(orderId,productId,amount) VALUES "
                + "(" + orderId + "," + productId + "," + amount + ")";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewPayment(int orderId, String openDate)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(paymentId)+1 FROM payments");
            int paymentId = Convert.ToInt32(dataTableId.Rows[0]["MAX(paymentId)+1"]);

            DataTable dataTableAmount = connector.ExecuteQuery("SELECT SUM(oe.amount*p.price) FROM order_entries oe NATURAL JOIN products p WHERE oe.orderId=" + orderId);
            int amount = Convert.ToInt32(dataTableId.Rows[0]["SUM(oe.amount*p.price)"]);

            String command = "INSERT INTO payments(paymentId,orderId,amount,openDate) VALUES "
                + "(" + paymentId + "," + orderId + "," + amount + ", DATE '"
                + openDate + "')";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewProduct(String name, int price, String imagePath, String blend, String description)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(productId)+1 FROM products");
            int productId = Convert.ToInt32(dataTableId.Rows[0]["MAX(productId)+1"]);

            String command = "INSERT INTO products(productId,name,price,imagePath,blend,description) VALUES "
                + "(" + productId + ",'" + name + "'," + price + ",'"
                + imagePath + "','" + blend + "','" + description + "')";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewOrder(int clientId, int workerId, int addressId, int status, String openDate)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(orderId)+1 FROM orders");
            int orderId = Convert.ToInt32(dataTableId.Rows[0]["MAX(orderId)+1"]);


            String command = "INSERT INTO orders(orderId,clientId,workerId,addressId,status,openDate) VALUES "
                + "(" + orderId + "," + clientId + "," + workerId + "," + addressId + "," + status + ", DATE '"
                + openDate + "')";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewWorker(WorkerRole role, String email, String password)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(workerId)+1 FROM workers");
            int workerId = Convert.ToInt32(dataTableId.Rows[0]["MAX(workerId)+1"]);

            String command = "INSERT INTO workers(workerId,role,email,password) VALUES "
                + "(" + workerId + ",'" + role + "','" + email + "','"
                + password + "')";
            return connector.ExecuteCommand(command);
        }


        //-------- DELETE --------
        public bool DeleteAddress(int addressId)
        {
            String command = "DELETE FROM addresses WHERE addressId=" + addressId;
            return connector.ExecuteCommand(command);
        }

        public bool DeleteProduct(int productId)
        {
            String command = "DELETE FROM products WHERE productId=" + productId;
            return connector.ExecuteCommand(command);
        }


        //-------- UPDATE --------
        public bool UpdateClientCredentials(int clientId, String whatToChange, String value)
        {
            String command = "UPDATE clients SET " + whatToChange + "='" + value + "' WHERE clientId=" + clientId;
            return connector.ExecuteCommand(command);
        }
        
        public bool UpdateProductDescription(int productId, String value)
        {
            String command = "UPDATE products SET description='" + value + "' WHERE productId=" + productId;
            return connector.ExecuteCommand(command);
        }
        
    }
}
