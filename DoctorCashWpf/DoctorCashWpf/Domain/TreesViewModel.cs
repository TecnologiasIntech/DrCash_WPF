﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MaterialDesignColors.WpfExample.Domain
{
    public sealed class Movie
    {
        public Movie(string name, string director)
        {
            Name = name;
            Director = director;
        }

        public string Name { get; }

        public string Director { get; }
    }

    public sealed class MovieCategory
    {
        public MovieCategory(string name, params Movie[] movies)
        {
            Name = name;
            Movies = new ObservableCollection<Movie>(movies);
        }

        public string Name { get; }

        public ObservableCollection<Movie> Movies { get; }
    }

    public sealed class TreesViewModel : INotifyPropertyChanged
    {
        private object _selectedItem;

        public ObservableCollection<MovieCategory> MovieCategories { get; }

        public AnotherCommandImplementation AddCommand { get; }

        public AnotherCommandImplementation RemoveSelectedItemCommand { get; }

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                this.MutateVerbose(ref _selectedItem, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        public TreesViewModel()
        {
            MovieCategories = new ObservableCollection<MovieCategory>
            {
                new MovieCategory("Action",                
                    new Movie ("Predator", "John McTiernan"),
                    new Movie("Alien", "Ridley Scott"),
                    new Movie("Prometheus", "Ridley Scott")),
                new MovieCategory("Comedy",
                    new Movie("EuroTrip", "Jeff Schaffer"),
                    new Movie("EuroTrip", "Jeff Schaffer")                                            
                )
            };

            AddCommand = new AnotherCommandImplementation(
                _ =>
                {
                    if (!MovieCategories.Any())
                    {
                        MovieCategories.Add(new MovieCategory(GenerateString(15)));
                    }
                    else
                    {
                        var index = new Random().Next(0, MovieCategories.Count);

                        MovieCategories[index].Movies.Add(
                            new Movie(GenerateString(15), GenerateString(20)));
                    }
                });

            RemoveSelectedItemCommand = new AnotherCommandImplementation(
                _ =>
                {
                    var movieCategory = SelectedItem as MovieCategory;
                    if (movieCategory != null)
                    {
                        MovieCategories.Remove(movieCategory);
                    }
                    else
                    {
                        var movie = SelectedItem as Movie;
                        if (movie == null) return;
                        MovieCategories.FirstOrDefault(v => v.Movies.Contains(movie))?.Movies.Remove(movie);
                    }
                },
                _ => SelectedItem != null);
        }

        private static string GenerateString(int length)
        {
            var random = new Random();

            return string.Join(string.Empty,
                Enumerable.Range(0, length)
                .Select(v => (char) random.Next('a', 'z' + 1)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
