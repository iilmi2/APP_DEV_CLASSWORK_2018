<Query Kind="Program">
  <Connection>
    <ID>226d6120-cca7-4aa6-b6ab-249d6d165767</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-4OM47VD</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WestWind</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

void Main()
{
    Customers
    .Where(c => c.CustomerID == "ALFKI")
        .OrderBy(c => c.CompanyName)
        .Select(c => new CustomerView
        {
            Name = c.CompanyName,
            Orders = Orders
                    .Where(o => o.CustomerID == c.CustomerID)
                    .Select(o => new OrderView
                    {
                        OrderId = o.OrderID,
                        Date = o.OrderDate,
                        OrderDetails = OrderDetails
                                        .Where(od => od.OrderID == o.OrderID)
                                        .Select(od => new OrderDetailView
                                        {
                                            OrderDetailID = od.OrderDetailID,
                                            Product = od.Product.ProductName,
                                            Qty = od.Quantity,
                                            Price = od.UnitPrice,
                                            ExtPrice = od.Quantity * od.UnitPrice
                                        }).ToList()
                    }).ToList()
        }).Dump();
}
public class CustomerView
{
    public string Name { get; set; }
    public List<OrderView> Orders {get; set;}
}
public class OrderView
{
public int OrderId {get; set;}
public DateTime? Date {get; set;}
public List<OrderDetailView> OrderDetails {get; set;}
}
public class OrderDetailView
{
    public int OrderDetailID {get; set;}
    public string Product {get; set;}
    public decimal Qty {get; set;}
    public decimal Price {get; set;}
    public decimal ExtPrice {get; set;}
}
public CustomerView GetCustomer(string customerID)
{
    return Customers
            .Where(c => c.CustomerID == customerID)
            .Select(c => new CustomerView
            {
                Name = c.CompanyName,
                Orders = Orders
                    .Where(o => o.CustomerID == c.CustomerID)
                    .Select(o => new OrderView
                    {
                        OrderId = o.OrderID,
                        Date = o.OrderDate,
                        OrderDetails = OrderDetails
                                        .Where(od => od.OrderID == o.OrderID)
                                        .Select(od => new OrderDetailView
                                        {
                                            OrderDetailID = od.OrderDetailID,
                                            Product = od.Product.ProductName,
                                            Qty = od.Quantity,
                                            Price = od.UnitPrice,
                                            ExtPrice = od.Quantity * od.UnitPrice
                                        }).ToList()
                    }).ToList()
            }).FirstOrDefault();
}
public void UpdateOrderDetail(OrderDetailView orderDetai)
{
    //  Update the tables
}