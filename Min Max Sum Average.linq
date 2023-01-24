<Query Kind="Statements">
  <Connection>
    <ID>4e88f938-52a6-4444-acd7-5639e16c2da2</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-4OM47VD</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>ChinookSept2018</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

(from track in Tracks
select track.Milliseconds / 1000).Average().Dump();

Tracks
	.Select(x => x.Milliseconds / 1000)
	.Average()
	.Dump();

Tracks.Average(x => x.Milliseconds / 1000).Dump();

Albums
	.Where(x => x.ReleaseYear >= 1960 && x.ReleaseYear < 1970)
	.OrderBy(x => x.Title)
	.Select(x => new
	{
		Title = x.Title,
		ArtistName = x.Artist.Name,
		TrackCount = x.Tracks.Count(),
		//LongestTrack = x.Tracks.Select(x => x.Milliseconds / 1000).Max()
		LongestTrack = x.Tracks.Max(x => x.Milliseconds / 1000),
		ShortestTrack = x.Tracks.Min(x => x.Milliseconds / 1000),
		Price = x.Tracks.Sum(x => x.UnitPrice),
		AvgTrackLength = x.Tracks.Average( c => c.Milliseconds / 1000)
	}
	)
	.Dump();