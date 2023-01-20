<Query Kind="Statements">
  <Connection>
    <ID>276dc2f1-e038-4ebd-9d73-0b3610f6142e</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>WB304-05\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>ChinookSept2018</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

//	Selecting colomns from the table
Albums
	.Select(x => new {Year = x.ReleaseYear, x.Artist, title = x.Title})
	.Dump();

// 	Ternarary Opeator '?'
Albums
	.OrderBy(x => x.ReleaseLabel)
	.Select(x => new 
	{
		Title = x.Title,
		Label = x.ReleaseLabel == null ? "Uknown" : x.ReleaseLabel
		
	})
	.Dump();

Albums
	.OrderBy(x => x.ReleaseYear)
	.Select(x => new
	{
		Title =   x.Title,
		ArtistName = x.Artist.Name,
		Decade = x.ReleaseYear < 1970 ? "Oldies" : 
		x.ReleaseYear < 1980 ? "70s" :
		x.ReleaseYear < 1990 ? "80s" :
		x.ReleaseYear < 2000 ? "90s" :
		x.ReleaseYear >= 2000 ? "Modern" : "Data is probably wrong"
	})
	.OrderBy(x => x.Decade)
	.Dump();