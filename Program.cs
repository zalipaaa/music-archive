using System;
using System.Text;
using MusicArchive.Services;
using MusicArchive.UI;

Console.OutputEncoding = Encoding.UTF8;

var repository = new SongRepository();
var menu = new Menu(repository);

menu.Run();