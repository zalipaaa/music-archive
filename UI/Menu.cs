using System;
using System.Collections.Generic;
using System.Linq;
using MusicArchive.Models;
using MusicArchive.Services;

namespace MusicArchive.UI
{
    public class Menu
    {
        private readonly SongRepository _repository;

        public Menu(SongRepository repository)
        {
            _repository = repository;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║       🎵 МУЗЫКАЛЬНЫЙ АРХИВ 🎵          ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("  1. ➕ Добавить песню");
                Console.WriteLine("  2. ➖ Удалить песню");
                Console.WriteLine("  3. 📋 Показать все песни");
                Console.WriteLine("  4. 🔍 Поиск по названию");
                Console.WriteLine("  5. 🎤 Поиск по исполнителю");
                Console.WriteLine("  6. 🎼 Поиск по жанру");
                Console.WriteLine("  7. 📅 Поиск по году");
                Console.WriteLine("  8. ❌ Выход");
                Console.WriteLine();
                Console.Write("  Выберите опцию (1-8): ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddSong();
                        break;
                    case "2":
                        RemoveSong();
                        break;
                    case "3":
                        ShowAllSongs();
                        break;
                    case "4":
                        SearchByTitle();
                        break;
                    case "5":
                        SearchByArtist();
                        break;
                    case "6":
                        SearchByGenre();
                        break;
                    case "7":
                        SearchByYear();
                        break;
                    case "8":
                        running = false;
                        Console.WriteLine("\n  До свидания! 👋\n");
                        break;
                    default:
                        Console.WriteLine("\n  ✗ Неверная опция! Пожалуйста, выберите 1-8.");
                        PauseMenu();
                        break;
                }
            }
        }

        private void AddSong()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║      ➕ ДОБАВИТЬ НОВУЮ ПЕСНЮ           ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            Console.Write("  Название песни: ");
            string? title = Console.ReadLine();

            Console.Write("  Исполнитель: ");
            string? artist = Console.ReadLine();

            Console.Write("  Альбом: ");
            string? album = Console.ReadLine();

            Console.Write("  Год выпуска: ");
            int year = int.TryParse(Console.ReadLine(), out int y) ? y : DateTime.Now.Year;

            Console.Write("  Жанр: ");
            string? genre = Console.ReadLine();

            Console.Write("  Длительность (в секундах): ");
            int duration = int.TryParse(Console.ReadLine(), out int d) ? d : 0;

            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(artist))
            {
                var song = new Song
                {
                    Title = title,
                    Artist = artist,
                    Album = album ?? "Unknown",
                    Year = year,
                    Genre = genre ?? "Unknown",
                    DurationSeconds = duration
                };

                _repository.AddSong(song);
            }
            else
            {
                Console.WriteLine("\n  ✗ Название и исполнитель обязательны!");
            }

            PauseMenu();
        }

        private void RemoveSong()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         ➖ УДАЛИТЬ ПЕСНЮ              ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var songs = _repository.GetAllSongs();
            if (songs.Count == 0)
            {
                Console.WriteLine("  Архив пуст! Нечего удалять.");
                PauseMenu();
                return;
            }

            DisplaySongs(songs);

            Console.WriteLine();
            Console.Write("  Введите ID песни для удаления (0 для отмены): ");
            if (int.TryParse(Console.ReadLine(), out int id) && id != 0)
            {
                _repository.RemoveSong(id);
            }
            else if (id == 0)
            {
                Console.WriteLine("  ℹ Отмена удаления.");
            }
            else
            {
                Console.WriteLine("  ✗ Неверный ID!");
            }

            PauseMenu();
        }

        private void ShowAllSongs()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       📋 ВСЕ ПЕСНИ В АРХИВЕ           ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var songs = _repository.GetAllSongs();

            if (!songs.Any())
            {
                Console.WriteLine("  Архив пуст! Добавьте первую песню.");
                PauseMenu();
                return;
            }

            DisplaySongs(songs);
            Console.WriteLine();
            Console.WriteLine($"  📊 Всего песен: {songs.Count}");
            PauseMenu();
        }

        private void SearchByTitle()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║      🔍 ПОИСК ПО НАЗВАНИЮ             ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            Console.Write("  Введите название или часть названия: ");
            string? title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("  ✗ Введите название!");
                PauseMenu();
                return;
            }

            var results = _repository.SearchByTitle(title);
            DisplaySearchResults(results, "Поиск по названию");
        }

        private void SearchByArtist()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║     🎤 ПОИСК ПО ИСПОЛНИТЕЛЮ           ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            Console.Write("  Введите имя исполнителя: ");
            string? artist = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(artist))
            {
                Console.WriteLine("  ✗ Введите имя исполнителя!");
                PauseMenu();
                return;
            }

            var results = _repository.SearchByArtist(artist);
            DisplaySearchResults(results, "Поиск по исполнителю");
        }

        private void SearchByGenre()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║        🎼 ПОИСК ПО ЖАНРУ              ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            Console.Write("  Введите жанр: ");
            string? genre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(genre))
            {
                Console.WriteLine("  ✗ Введите жанр!");
                PauseMenu();
                return;
            }

            var results = _repository.SearchByGenre(genre);
            DisplaySearchResults(results, "Поиск по жанру");
        }

        private void SearchByYear()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║        📅 ПОИСК ПО ГОДУ               ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            Console.Write("  Введите год: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var results = _repository.SearchByYear(year);
                DisplaySearchResults(results, "Поиск по году");
            }
            else
            {
                Console.WriteLine("  ✗ Введите корректный год!");
                PauseMenu();
            }
        }

        private void DisplaySongs(List<Song> songs)
        {
            Console.WriteLine("  ╔════════════════════════════════════════════════════════════════════════════╗");
            foreach (var song in songs)
            {
                Console.WriteLine($"  {song}");
            }
            Console.WriteLine("  ╚════════════════════════════════════════════════════════════════════════════╝");
        }

        private void DisplaySearchResults(List<Song> results, string searchType)
        {
            Console.WriteLine();
            if (!results.Any())
            {
                Console.WriteLine($"  ✗ По запросу '{searchType}' ничего не найдено!");
            }
            else
            {
                Console.WriteLine($"  ✓ Найдено {results.Count} результатов:");
                Console.WriteLine();
                DisplaySongs(results);
            }
            PauseMenu();
        }

        private void PauseMenu()
        {
            Console.WriteLine();
            Console.Write("  Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}