using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Argus.InventoryExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public class InventoryItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public int Quantity { get; set; }
        }

        public ObservableCollection<InventoryItem> InventoryItems { get; } =
            new ObservableCollection<InventoryItem>
            {
                new InventoryItem { Id = 1, Name = "Widget", Quantity = 25 },
                new InventoryItem { Id = 2, Name = "Gadget", Quantity = 10 }
            };
        }
}