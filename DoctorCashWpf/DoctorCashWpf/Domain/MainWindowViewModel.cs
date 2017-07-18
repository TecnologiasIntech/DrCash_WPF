using System.Configuration;
using MaterialDesignDemo;
using MaterialDesignDemo.Domain;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Controls;
using DoctorCashWpf;

namespace MaterialDesignColors.WpfExample.Domain
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            DemoItems = new[]
            {
                new DemoItem("Home", new Home(),
                    new []
                    {
                        new DocumentationLink(DocumentationLinkType.Wiki, $"{ConfigurationManager.AppSettings["GitHub"]}/wiki", "WIKI"),
                        DocumentationLink.DemoPageLink<Home>()
                    }
                ),
                new DemoItem("Palette", new PaletteSelector { DataContext = new PaletteSelectorViewModel() },
                    new []
                    {
                        DocumentationLink.WikiLink("Brush-Names", "Brushes"),
                        DocumentationLink.WikiLink("Custom-Palette-Hues", "Custom Palettes"),
                        DocumentationLink.WikiLink("Swatches-and-Recommended-Colors", "Swatches"),
                        DocumentationLink.DemoPageLink<PaletteSelector>("Demo View"),
                        DocumentationLink.DemoPageLink<PaletteSelectorViewModel>("Demo View Model"),
                        DocumentationLink.ApiLink<PaletteHelper>()
                    }),

            };
        }

        public DemoItem[] DemoItems { get; }
    }
}