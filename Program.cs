using MusicArchive.Services;
using MusicArchive.UI;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var repository = new SongRepository();
var menu = new Menu(repository);

menu.Run();
