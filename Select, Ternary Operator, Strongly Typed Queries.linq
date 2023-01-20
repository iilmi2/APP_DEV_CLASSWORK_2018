<Query Kind="Program">
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

#load "..\LINQ\ViewModels\*.cs"
void Main()
{
	string partialSongName = "dance";
	
	//	anonymous dataset
	IEnumerable anonymousTypeResults = AnonymousTypeSongByPartialName(partialSongName);
	anonymousTypeResults.Dump();

	//  strongly type dataset
	List<SongView> stronglyTypeResults = StronglyTypeSongByPartialName(partialSongName);
	stronglyTypeResults.Dump();
}

// You can define other methods, fields, classes and namespaces here
private IEnumerable AnonymousTypeSongByPartialName(string partialSongName)
{
	var songCollection =
	Tracks
		  .Where(x => x.Name.Contains(partialSongName))
		  .Select(x => new 
		  {
			  AlbumTitle = x.Album.Title, //  We are using the navigation property 
			  SongTitle = x.Name,
			  Artist = x.Album.Artist.Name  //  We are using the navigation property 
		  });
	return songCollection;
}

private List<SongView> StronglyTypeSongByPartialName(string partialSongName)
{
	var songCollection =
	Tracks
		  .Where(x => x.Name.Contains(partialSongName))
		  .Select(x => new SongView
		  {
			  AlbumTitle = x.Album.Title, //  We are using the navigation property 
			  SongTitle = x.Name,
			  Artist = x.Album.Artist.Name  //  We are using the navigation property 
		  });
	return songCollection.ToList();
}

public class SongView
{
	public string AlbumTitle {get; set;}
	public string SongTitle {get; set;}
	public string Artist {get; set;}
}