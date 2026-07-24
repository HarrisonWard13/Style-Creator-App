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

namespace StyleTester
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public ObservableCollection<ListItem> Items { get; } =
    new ObservableCollection<ListItem>
    {
        new ListItem { Name = "Apple", Value = "10" },
        new ListItem { Name = "Banana", Value = "20" },
        new ListItem { Name = "Cherry", Value = "30" }
    };
    public MainWindow()
    {
      InitializeComponent();
      Items =
        new ObservableCollection<ListItem>
        {
            new ListItem { Name = "Apple", Value = "10" },
            new ListItem { Name = "Banana", Value = "20" },
            new ListItem { Name = "Cherry", Value = "30" }
        };
    }
  }

  public class ListItem
  {
    public string Name { get; set; }
    public string Value { get; set; }
  }
}