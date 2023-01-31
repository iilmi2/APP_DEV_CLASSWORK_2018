<Query Kind="Expression">
  <Connection>
    <ID>5fa65548-36a3-4a37-83e2-2c73f088e993</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WB320-04\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>GroceryList</Database>
  </Connection>
</Query>

OrderLists
	.GroupBy(a => a.ProductID)
	.Select(x => new
		{
			ProductName = Products.Where(p => x.Key == p.ProductID).Select(a => a.Description),
			TimesPurchased = x.Count()
		})
		.OrderByDescending(x => x.TimesPurchased)

Stores
	.Select(x => new
	{
		Location = x.Location,
		Clients = Orders.Where(y => y.StoreID == x.StoreID).GroupBy(q => q.CustomerID).Select( a => new 
		{
			Address = Customers.Where(b => b.CustomerID == a.Key).Select(c => c.Address).FirstOrDefault(),
			City = Customers.Where(b => b.CustomerID == a.Key).Select(c => c.City).FirstOrDefault(),
			Province = Customers.Where(b => b.CustomerID == a.Key).Select(c => c.Province).FirstOrDefault()
		}
		)
	}
	)
	.OrderBy(z => z.Location)