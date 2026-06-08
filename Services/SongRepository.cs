using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MusicArchive.Models;
using Newtonsoft.Json;

namespace MusicArchive.Services
{
    public class SongRepository
    {
        private const string FilePath = "songs.json";
        private List<Song> _songs = new();

        public SongRepository()
        {
            LoadFromFile();
        }

        public void AddSong(Song song)
        {
            song.Id = _songs.Any() ? _songs.Max(s => s.Id) + 1 : 1;
            _songs.Add(song);
            SaveToFile();
            Console.WriteLine($"✓ Песня '{song.Title}' успешно добавлена!");
        }

        public void RemoveSong(int id)
        {
            var song = _songs.FirstOrDefault(s => s.Id == id);
            if (song != null)
            {
                _songs.Remove(song);
                SaveToFile();
                Console.WriteLine($"✓ Песня '{song.Title}' удалена!");
            }
            else
            {
                Console.WriteLine("✗ Песня с таким ID не найдена!");
            }
        }

        public List<Song> GetAllSongs()
        {
            return _songs.OrderBy(s => s.Id).ToList();
        }

        public Song? GetSongById(int id)
        {
            return _songs.FirstOrDefault(s => s.Id == id);
        }

        public List<Song> SearchByTitle(string title)
        {
            return _songs
                .Where(s => s.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.Title)
                .ToList();
        }

        public List<Song> SearchByArtist(string artist)
        {
            return _songs
                .Where(s => s.Artist.Contains(artist, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.Artist)
                .ToList();
        }

        public List<Song> SearchByGenre(string genre)
        {
            return _songs
                .Where(s => s.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.Genre)
                .ToList();
        }

        public List<Song> SearchByYear(int year)
        {
            return _songs
                .Where(s => s.Year == year)
                .OrderBy(s => s.Title)
                .ToList();
        }

        public void SaveToFile()
        {
            try
            {
                var json = JsonConvert.SerializeObject(_songs, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Ошибка при сохранении: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    var json = File.ReadAllText(FilePath);
                    _songs = JsonConvert.DeserializeObject<List<Song>>(json) ?? new();
                }
                catch
                {
                    _songs = new();
                    Console.WriteLine("⚠ Не удалось загрузить файл. Начинаем с пустого архива.");
                }
            }
        }
    }
}
