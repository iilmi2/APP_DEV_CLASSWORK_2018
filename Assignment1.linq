<Query Kind="Statements">
  <Connection>
    <ID>f6755e73-7121-40a3-87f7-60ea69ae2796</ID>
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
		.Dump();

Stores
	.Select(x => new
	{
		Location = x.Location,
		Clients = Orders.Where(y => y.StoreID == x.StoreID).GroupBy(q => q.CustomerID).Select(a => new
		{
			Address = Customers.Where(b => b.CustomerID == a.Key).Select(c => c.Address).FirstOrDefault(),
			City = Customers.Where(b => b.CustomerID == a.Key).Select(c => c.City).FirstOrDefault(),
			Province = Customers.Where(b => b.CustomerID == a.Key).Select(c => c.Province).FirstOrDefault()
		}
		)
	}
	)
	.OrderBy(z => z.Location)
	.Dump();

Stores
	.OrderBy(x => x.City)
	.ThenBy(x => x.Location)
	.Select(x => new
	{
		city = x.City,
		location = x.Location,
		sales = Orders.Where(orders => orders.OrderDate.Month == 12 && orders.StoreID == x.StoreID)
				.GroupBy(gb => gb.OrderDate)
				.Select(day => new
				{
					date = day.Key,
					numberoforders = day.Count(),
					productsales = day.Sum(x => x.SubTotal),
					gst = day.Sum(x => x.GST)
				})
	}).Dump();


OrderLists
	.Where(x => x.OrderID == 33)
	.GroupBy(categories => categories.Product.Category)
	.Select(x => new
	{
		Category = x.Key.Description,
		OrderProducts = x.ToList().Select(q => new
		{
			Product = q.Product.Description,
			Price = q.Price,
			PickedQty = q.QtyPicked,
			Discount = q.Discount,
			Subtotal = q.QtyOrdered > 1 ? (q.Price * Convert.ToDecimal(q.QtyOrdered)) : q.Price,
			Tax = (q.Price * Convert.ToDecimal(q.QtyOrdered)) * Convert.ToDecimal(0.05),
			ExtendedPrice = 
		})
	}
	)
	.OrderBy(x => x.Category)


Orders
	.GroupBy(x => x.Store)
	.Select(x => new
	{
		Location = x.Key.Location,
		Orders = x.Count(),
		Avg = OrderLists.Where(q => q.Order.Store.Location == x.Key.Location).Count() / x.Count(),
		AvgRevenue = x.Average(a => a.SubTotal)
	})
	.OrderBy(x => x.Location)
	.Dump();
	
Customers
	.Where(x => x.CustomerID == 1)
	.Select(x => new
	{
		Customer = x.FirstName +" " + x.LastName,
		OrdersCount = x.Orders.Count(),
		Items = OrderLists.Where(items => items.Order.CustomerID == x.CustomerID)
					.GroupBy(gb => gb.Product)
					.Select(a => new {
						description = a.Key.Description,
						timesbought = a.Count()
					})
					.OrderByDescending(d => d.timesbought)
					.ThenBy(d => d.description)
	})
	.Dump();