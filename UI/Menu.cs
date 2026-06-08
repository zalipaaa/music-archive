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
                Console.WriteLine("════════════════════════════════════════");
                Console.WriteLine("    🎵 МУЗЫКАЛЬНЫЙ АРХИВ 🎵");
                Console.WriteLine("════════════════════════════════════════");
                Console.WriteLine("1. Добавить песню");
                Console.WriteLine("2. Удалить песню");
                Console.WriteLine("3. Показать все песни");
                Console.WriteLine("4. Поиск по названию");
                Console.WriteLine("5. Поиск по исполнителю");
                Console.WriteLine("6. Поиск по жанру");
                Console.WriteLine("7. Поиск по году");
                Console.WriteLine("8. Выход");
                Console.WriteLine("════════════════════════════════════════");
                Console.Write("Выберите опцию: ");

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
                        Console.WriteLine("\nДо свидания! 👋");
                        break;
                    default:
                        Console.WriteLine("✗ Неверная опция!");
                        break;
                }

                if (running && choice != "3" && choice != "4" && choice != "5" && choice != "6" && choice != "7")
                {
                    Console.WriteLine("\nНажмите Enter для продолжения...");
                    Console.ReadLine();
                }
            }
        }

        private void AddSong()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("    ДОБАВИТЬ НОВУЮ ПЕСНЮ");
            Console.WriteLine("═══════════════════════════════════════");

            Console.Write("Название песни: ");
            string? title = Console.ReadLine();

            Console.Write("Исполнитель: ");
            string? artist = Console.ReadLine();

            Console.Write("Альбом: ");
            string? album = Console.ReadLine();

            Console.Write("Год выпуска: ");
            int year = int.TryParse(Console.ReadLine(), out int y) ? y : DateTime.Now.Year;

            Console.Write("Жанр: ");
            string? genre = Console.ReadLine();

            Console.Write("Длительность (в секундах): ");
            int duration = int.TryParse(Console.ReadLine(), out int d) ? d : 0;

            if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(artist))
            {
                var song = new Song
                {
                    Title = title,
                    Artist = artist,
                    Album = album ?? string.Empty,
                    Year = year,
                    Genre = genre ?? string.Empty,
                    DurationSeconds = duration
                };

                _repository.AddSong(song);
            }
            else
            {
                Console.WriteLine("✗ Название и исполнитель обязательны!");
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        private void RemoveSong()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("    УДАЛИТЬ ПЕСНЮ");
            Console.WriteLine("═══════════════════════════════════════");

            ShowAllSongs();

            Console.Write("\nВведите ID песни для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                _repository.RemoveSong(id);
            }
            else
            {
                Console.WriteLine("✗ Неверный ID!");
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        private void ShowAllSongs()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("    ВСЕ ПЕСНИ В АРХИВЕ");
            Console.WriteLine("═══════════════════════════════════════");

            var songs = _repository.GetAllSongs();

            if (!songs.Any())
            {
                Console.WriteLine("Архив пуст! Добавьте первую песню.");
                Console.WriteLine("\nНажмите Enter для продолжения...");
                Console.ReadLine();
                return;
            }

            foreach (var song in songs)
            {
                Console.WriteLine(song);
            }

            Console.WriteLine($"\nВсего песен: {songs.Count}");
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }

        private void SearchByTitle()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("    ПОИСК ПО НАЗВАНИЮ");
            Console.WriteLine("═══════════════════════════════════════");

            Console.Write("Введите название или часть названия: ");
            string? title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("✗ Введите название!");
                return;
            }

            var results = _repository.SearchByTitle(title);
            DisplaySearchResults(results);
        }

        private void SearchByArtist()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("    ПОИСК ПО ИСПОЛНИТЕЛЮ");
            Console.WriteLine("═══════════════════════════════════════");

            Console.Write("Введите имя исполнителя: ");
            string? artist = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(artist))
            {
                Console.WriteLine("✗ Введите имя исполнителя!");
                return;
            }

            var results = _repository.SearchByArtist(artist);
            DisplaySearchResults(results);
        }

        private void SearchByGenre()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("    ПОИСК ПО ЖАНРУ");
            Console.WriteLine("═══════════════════════════════════════");

            Console.Write("Введите жанр: ");
            string? genre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(genre))
            {
                Console.WriteLine("✗ Введите жанр!");
                return;
            }

            var results = _repository.SearchByGenre(genre);
            DisplaySearchResults(results);
        }

        private void SearchByYear()
        {
            Console.Clear();
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("    ПОИСК ПО ГОДУ");
            Console.WriteLine("═══════════════════════════════════════");

            Console.Write("Введите год: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var results = _repository.SearchByYear(year);
                DisplaySearchResults(results);
            }
            else
            {
                Console.WriteLine("✗ Введите корректный год!");
            }
        }

        private void DisplaySearchResults(List<Song> results)
        {
            Console.WriteLine("═══════════════════════════════════════");

            if (!results.Any())
            {
                Console.WriteLine("✗ Ничего не найдено!");
            }
            else
            {
                Console.WriteLine($"Найдено {results.Count} результатов:");
                Console.WriteLine("─────────────────────────────────────");
                foreach (var song in results)
                {
                    Console.WriteLine(song);
                }
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}
