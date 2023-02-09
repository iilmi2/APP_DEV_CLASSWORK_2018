<Query Kind="Expression">
  <Connection>
    <ID>85cf5cbc-ab84-4a19-ba62-eba0bdf5367a</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-4OM47VD</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>GroceryList</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
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

Stores
	.OrderBy(x => x.City)
	.ThenBy(x => x.Location)
	.Select(x => new
	{
		city = x.City,
		location = x.Location,
		sales = Orders.Where(orders => orders.OrderDate.Month == 12 && orders.StoreID == x.StoreID )
				.GroupBy(gb => gb.OrderDate)
				.Select(day => new
					{
						date = day.Key,
						numberoforders = day.Count(),
						productsales = day.Sum(x => x.SubTotal)
					})
	})
	

