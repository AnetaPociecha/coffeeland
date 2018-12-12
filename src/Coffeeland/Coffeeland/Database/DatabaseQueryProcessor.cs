using System;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace Coffeeland.Database
{
    public class DatabaseQueryProcessor
    {
        Connector connector;

        public DatabaseQueryProcessor()
        {
            Debug.WriteLine("Test");
            connector = new Connector();
        }


        //-------- CLIENTS --------
        public bool CreateNewClient(String email, String firstName, String lastName, String password, bool newsletter)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(clientId)+1 FROM clients");
            int clientId = ToInt(dataTableId);
            if (clientId == 0)
            {
                clientId = 30;
            }

            String command = "INSERT INTO clients(clientId,email,firstName,lastName,password,newsletter) VALUES "
                + "(" + clientId.ToString() + ",'" + email + "','" + firstName + "','" + lastName + "','" + password + "'," + boolToBit(newsletter) + ")";
            return connector.ExecuteCommand(command);
        }

        public DataTable GetOrders(int clientId)
        {
            String query = "SELECT o.status, o.openDate, o.closeDate, oe.amount, pr.name, pr.price, pay.amount FROM" +
                " clients c NATURAL JOIN orders o NATURAL JOIN order_entries oe NATURAL JOIN products pr JOIN payments pay ON (pay.orderId=o.orderId)" +
                " WHERE c.clientId=" + clientId;
            return connector.ExecuteQuery(query);
        }

        public bool IsEmailAvailable(String email)
        {
            String query = "SELECT email FROM clients UNION SELECT email FROM workers";
            DataTable emailAddresses = connector.ExecuteQuery(query);
            foreach (DataRow e in emailAddresses.Rows)
            {
                if (String.Equals(e["email"], email)) return false;
       
            }
            return true;

        }

        public DataTable GetClients()
        {
            String query = "SELECT firstName, lastName, email FROM clients";
            return connector.ExecuteQuery(query);
        }

        public bool IsSignInCredentialCorrect(String email, String password)
        {
            String query = "SELECT password FROM clients WHERE email='" + email + "' UNION SELECT password FROM workers WHERE email='" + email +"'";
            DataTable correctPassword = connector.ExecuteQuery(query);
            foreach (DataRow p in correctPassword.Rows)
            {
                return (String.Equals(p["password"], password));
            }
            return false;
        }

        public int GetClientId(String email)
        {
            String query = "SELECT clientId FROM clients WHERE email='" + email + "'";
            int id;
            DataTable idTable = connector.ExecuteQuery(query);
            foreach (DataRow i in idTable.Rows)
            {
                Int32.TryParse(i["clientId"].ToString(), out id);
                return id;
            }
            return 0;
        }
        
        public bool IsClientSignInForNewsletter(String email)
        {
            String query = "SELECT newsletter FROM clients WHERE email='" + email + "'";
            DataTable newsletterTable = connector.ExecuteQuery(query);
            foreach (DataRow n in newsletterTable.Rows)
            {
                return String.Equals(n["newsletter"].ToString(), "1");
            }
            return false; //exception? no such address in db
        }
        
        
        public bool UpdateClientCredentials(int clientId, String whatToChange, String value)
        {
            String command = "UPDATE clients SET " + whatToChange + "='" + value + "' WHERE clientId=" + clientId;
            return connector.ExecuteCommand(command);
        }
        

        //-------- ADDRESSES --------
        public bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber, String apartmentNumber)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int addressId = ToInt(dataTableId);
            if (clientId == 0)
            {
                clientId = 40;
            }

            String command = "INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,apartmentNumber) VALUES "
                + "(" + addressId.ToString() + ",'" + clientId.ToString() + "','" + country + "','" 
                + city + "','" + street + "'," + ZIPCode + "," + buildingNumber + ",'" + apartmentNumber + "')";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int addressId = ToInt(dataTableId);
            if (addressId == 0)
            {
                addressId = 40;
            }

            String command = "INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber) VALUES "
                + "(" + addressId.ToString() + ",'" + clientId.ToString() + "','" + country + "','"
                + city + "','" + street + "'," + ZIPCode + "," + buildingNumber + ")";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber, String apartmentNumber,bool isDefault)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int addressId = ToInt(dataTableId);
            if (addressId == 0)
            {
                addressId = 40;
            }

            String command = "INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,apartmentNumber,isDefault) VALUES "
                + "(" + addressId.ToString() + ",'" + clientId.ToString() + "','" + country + "','"
                + city + "','" + street + "'," + ZIPCode + "," + buildingNumber + ",'" + apartmentNumber + "'," +boolToBit(isDefault) +")";
            return connector.ExecuteCommand(command);
        }

        public bool CreateNewAddress(int clientId, String country, String city, String street, int ZIPCode, int buildingNumber,bool isDefault)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(addressId)+1 FROM addresses");
            int addressId = ToInt(dataTableId);
            if (addressId == 0)
            {
                addressId = 40;
            }

            String command = "INSERT INTO addresses(addressId,clientId,country,city,street,ZIPCode,buildingNumber,isDefault) VALUES "
                + "(" + addressId.ToString() + ",'" + clientId.ToString() + "','" + country + "','"
                + city + "','" + street + "'," + ZIPCode + "," + buildingNumber + "," + boolToBit(isDefault) + ")";
            return connector.ExecuteCommand(command);
        }

        public bool DeleteAddress(int addressId)
        {
            String command = "DELETE FROM addresses WHERE addressId=" + addressId;
            return connector.ExecuteCommand(command);
        }


        //-------- COMPLAINTS --------
        public bool CreateNewComplaint(int orderId, int workerId, String description, String openDate, bool isClosed)
        {
            String command = "INSERT INTO complaints(orderId,workerId,description,openDate,isClosed) VALUES "
                + "(" + orderId.ToString() + ",'" + workerId.ToString() + "','" + description + "', DATE '"
                + openDate + "'," + boolToBit(isClosed) + ")";
            return connector.ExecuteCommand(command);
        }

        public DataTable GetComplaints()
        {
            String query = "SELECT c.firstName, c.lastName, pr.name, oe.amount, pr.price, com.description " +
                "FROM products pr NATURAL JOIN order_entries oe NATURAL JOIN orders o NATURAL JOIN clients c " +
                "JOIN complaints com ON(com.orderId= o.orderId)";
            return connector.ExecuteQuery(query);
        }


        //-------- ORDERS --------
        public bool CreateNewOrder(int clientId, int workerId, int addressId, int status, String openDate)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(orderId)+1 FROM orders");
            int orderId = ToInt(dataTableId);
            if (orderId == 0)
            {
                orderId = 50;
            }

            String command = "INSERT INTO orders(orderId,clientId,workerId,addressId,status,openDate) VALUES "
                + "(" + orderId.ToString() + "," + clientId.ToString() + "," + workerId.ToString() +","+addressId.ToString() + ","+status+", DATE '"
                + openDate + "')";
            return connector.ExecuteCommand(command);
        }

        //-------- ORDER ENTRIES --------
        public bool CreateNewOrderEntry(int orderId,int productId, int amount)
        {
            String command = "INSERT INTO order_entries(orderId,productId,amount) VALUES "
                + "(" + orderId.ToString() + "," + productId.ToString() + "," + amount +")";
            return connector.ExecuteCommand(command);
        }

        //-------- PAYMENTS --------
        public bool CreateNewPayment(int orderId, String openDate)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(paymentId)+1 FROM payments");
            int paymentId = ToInt(dataTableId);
            if (paymentId == 0)
            {
                paymentId = 60;
            }

            DataTable dataTableAmount = connector.ExecuteQuery("SELECT SUM(oe.amount*p.price) FROM order_entries oe NATURAL JOIN products p WHERE oe.orderId=" + orderId);
            int amount = ToInt(dataTableAmount);

            String command = "INSERT INTO payments(paymentId,orderId,amount,openDate) VALUES "
                + "(" + paymentId.ToString() + "," + orderId.ToString() + "," + amount.ToString() + ", DATE '"
                + openDate + "')";
            return connector.ExecuteCommand(command);
        }


        //-------- PRODUCTS --------
        public bool CreateNewProduct(String name, int price, String imagePath, String blend, String description)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(productId)+1 FROM products");
            int productId = ToInt(dataTableId);
            if (productId == 0)
            {
                productId = 10;
            }

            String command = "INSERT INTO products(productId,name,price,imagePath,blend,description) VALUES "
                + "(" + productId.ToString() + ",'" + name + "'," + price + ",'"
                + imagePath + "','" + blend + "','" + description + "')";
            return connector.ExecuteCommand(command);
        }

        public bool DeleteProduct(int productId)
        {
            String command = "DELETE FROM products WHERE productId=" + productId;
            return connector.ExecuteCommand(command);
        }

        public bool UpdateProductDescription(int productId, String value)
        {
            String command = "UPDATE products SET description='" + value + "' WHERE productId=" + productId;
            return connector.ExecuteCommand(command);
        }


        //-------- WORKERS --------
        public bool CreateNewWorker(WorkerRole role, String email, String password)
        {
            DataTable dataTableId = connector.ExecuteQuery("SELECT MAX(workerId)+1 FROM workers");
            int workerId = ToInt(dataTableId);
            if (workerId == 0)
            {
                workerId = 20;
            }

            String command = "INSERT INTO workers(workerId,role,email,password) VALUES "
                + "(" + workerId.ToString() + ",'" + role + "','" + email + "','"
                + password + "')";
            return connector.ExecuteCommand(command);
        }

        //-------------------------

        public void FillTheDatabase()
        {

            CreateNewProduct("Lavazza", 45, "/path/to/lavazza", "100% Arabica", "Very nice coffee");
            CreateNewProduct("Jacobs", 25, "/path/to/jacobs", "70% Robusta, 30% Arabica", "Kind of good coffee");
            CreateNewProduct("Vergnano", 95, "/path/to/vergnano", "100% Arabica", "Good as ****");
            CreateNewProduct("Bazarra", 55, "/path/to/bazarra", "60% Arabica, 40% Robusta", "It is ok");
            CreateNewProduct("Konesso", 35, "/path/to/konesso", "50% Arabica, 50% Robusta", "Nice");

            CreateNewWorker(WorkerRole.a, "szef123@buziaczek.pl", "admin123");
            CreateNewWorker(WorkerRole.b, "zwykly_konsultant@interia.pl", "admin123");

            CreateNewClient("olanowak69@gmail.com", "Aleksandra", "Nowak", "admin123", false);

            CreateNewClient("kasiapepsinska@op.pl", "Katarzyna", "Pepsinska", "admin123", true);
            CreateNewAddress(31, "Poland", "Cracow", "Kupa", 31442, 13, 1, true);
            CreateNewOrder(31,21,40,0,"2018-11-05");
            CreateNewOrderEntry(50, 10, 2);
            CreateNewOrderEntry(50, 11, 3);
            CreateNewPayment(50, "2018-11-05");
            CreateNewComplaint(51,21,"It is not good", "2018-11-05",false);

            CreateNewClient("aniaubek@agh.edu.pl", "Anna", "Ubek", "admin123", true);
            CreateNewAddress(32, "Poland", "Warsaw", "Kosciuszki", 32111, 145,5, true);
            CreateNewAddress(32, "Poland", "Warsaw", "Kochanowskiego",32121, 1);
            CreateNewOrder(32, 21,41, 0, "2018-12-07"); CreateNewOrderEntry(50, 10, 2);
            CreateNewOrderEntry(51, 13, 3);
            CreateNewOrderEntry(51, 10, 2);
            CreateNewOrderEntry(51, 14, 3);
            CreateNewPayment(51,"2018-12-07");
            CreateNewOrder(32,21,42,1,"2018-12-09");
            CreateNewOrder(32, 21, 42, 1, "2018-12-09");
            CreateNewPayment(51,"2018-12-09");

        }

        public bool Erase()
        {
            return connector.Erase();
        }

        public int ToInt(DataTable dt)
        {
            int result = 0;
            Int32.TryParse(ToString(dt),out result);
            return result;
        }

        public DataTable SelectAllFrom(String tableName)
        {
            String query = "SELECT * FROM " + tableName;
            return connector.ExecuteQuery(query);
        }

        public String ToString(DataTable dt)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (object dc in dr.ItemArray)
                {
                    builder.Append(dc.ToString());
                    builder.Append(" ");
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }
        
        int boolToBit(bool a)
        {
            if (a) return 1;
            return 0;
        }

    }
}
