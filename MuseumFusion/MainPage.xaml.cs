using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using MuseumFusion.Model;

namespace MuseumFusion
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Museum> MuseumTiles;

        public MainPage()
        {
            InitializeComponent();
            MuseumTiles = new ObservableCollection<Museum>(GetSampleMuseums());
            museumTilesCollectionView.ItemsSource = MuseumTiles;
        }

        private void OnSortChanged(object sender, EventArgs e)
        {
            var property = SortPicker.SelectedItem as string;

            switch (property)
            {
                case "Name":
                    MuseumTiles = new ObservableCollection<Museum>(MuseumTiles.OrderBy(m => m.Name));
                    break;
                case "Place":
                    MuseumTiles = new ObservableCollection<Museum>(MuseumTiles.OrderBy(m => m.Place));
                    break;
                case "Date":
                    MuseumTiles = new ObservableCollection<Museum>(MuseumTiles.OrderBy(m => m.Date));
                    break;
            }

            museumTilesCollectionView.ItemsSource = MuseumTiles;
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = e.NewTextValue.ToLower();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                museumTilesCollectionView.ItemsSource = MuseumTiles;
            }
            else
            {
                var filteredMuseums = MuseumTiles
                    .Where(m => m.Name.ToLower().Contains(keyword) || m.Place.ToLower().Contains(keyword))
                    .ToList();

                museumTilesCollectionView.ItemsSource = new ObservableCollection<Museum>(filteredMuseums);
            }
        }

        private void OnMuseumSelected(object sender, EventArgs e)
        {
            var selectedMuseum = (sender as View)?.BindingContext as Museum;

            if (selectedMuseum != null)
            {
                // Navigate to the 'museumDetails' page with selectedMuseum details
                Navigation.PushAsync(new MuseumDetailsPage(selectedMuseum));
            }
        }

        public static ObservableCollection<Museum> GetSampleMuseums()
        {
            return new ObservableCollection<Museum>
            {
                new Museum { Name = "Museum A", Place = "City A", Date = DateTime.Now.AddDays(1), ImageUrl = "museum1.jpg", 
                            Price = 800, Description = "asdfg asdfg asdfg asdfg asdfg asdfg asdfg, asdfg asdfg asdfg asdfg" },
                new Museum { Name = "Museum B", Place = "City B", Date = DateTime.Now.AddDays(-10), ImageUrl = "museum1.jpg", 
                            Price= 1500, Description = "asdfg asdfg asdfg asdfg asdfg asdfg asdfg, asdfg asdfg asdfg asdfg" },
                // Add more museums as needed
            };
        }
    }

    //public partial class MainPage : ContentPage
    //{
    //    public ObservableCollection<Museum> Museums { get; set; }
    //    public ObservableCollection<Museum> FilteredMuseums { get; set; }
    //    public List<string> SortOptions { get; } = new List<string> { "Name", "Place", "Date" };

    //    private string currentSortOption = "Name"; // Default sorting option

    //    public MainPage()
    //    {
    //        InitializeComponent();

    //        Museums = new ObservableCollection<Museum>
    //        {
    //            new Museum { Name = "Museum 1", ImageUrl = "museum1.jpg", Place = "Place 1", Date = DateTime.Now.AddDays(1) },
    //            new Museum { Name = "Museum 2", ImageUrl = "museum1.jpg", Place = "Place 0", Date = DateTime.Now.AddDays(2) },
    //            // Add more museums as needed
    //        };

    //        FilteredMuseums = new ObservableCollection<Museum>(Museums);

    //        BindingContext = this;
    //    }

    //    private void OnMuseumSelected(object sender, SelectionChangedEventArgs e)
    //    {
    //        if (e.CurrentSelection.FirstOrDefault() is Museum selectedMuseum)
    //        {
    //            Navigation.PushAsync(new MuseumDetailsPage(selectedMuseum));
    //            ((CollectionView)sender).SelectedItem = null; // Reset selection
    //        }
    //    }

    //    private void OnSortChanged(object sender, EventArgs e)
    //    {
    //        currentSortOption = SortPicker.SelectedItem.ToString();
    //        SortMuseums();
    //    }

    //    private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
    //    {
    //        FilterMuseums(FilterEntry.Text);
    //    }

    //    private void SortMuseums()
    //    {
    //        switch (currentSortOption)
    //        {
    //            case "Name":
    //                FilteredMuseums = new ObservableCollection<Museum>(FilteredMuseums.OrderBy(m => m.Name));
    //                break;
    //            case "Place":
    //                FilteredMuseums = new ObservableCollection<Museum>(FilteredMuseums.OrderBy(m => m.Place));
    //                break;
    //            case "Date":
    //                FilteredMuseums = new ObservableCollection<Museum>(FilteredMuseums.OrderBy(m => m.Date));
    //                break;
    //        }
    //    }

    //    private void FilterMuseums(string filter)
    //    {
    //        if (string.IsNullOrEmpty(filter))
    //        {
    //            FilteredMuseums = new ObservableCollection<Museum>(Museums);
    //        }
    //        else
    //        {
    //            FilteredMuseums = new ObservableCollection<Museum>(
    //                Museums.Where(m => m.Name.ToLower().Contains(filter.ToLower())));
    //        }

    //        SortMuseums();
    //    }
    //}


}
