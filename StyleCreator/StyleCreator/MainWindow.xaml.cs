using StyleCreator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StyleCreator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			//DataContext = new MainViewModel();
			InitializeComponent();

      if( DataContext is ThemeEditorViewModel vm )
      {
        vm.PropertyChanged += OnThemePropertyChanged;
        ApplyTheme( vm );
      }
		}
    private void OnThemeEditorLoaded( object sender, RoutedEventArgs e )
    {
      if( ( (FrameworkElement)sender ).DataContext is ThemeEditorViewModel vm )
        vm.LoadFromResources();
    }

    private void OnThemePropertyChanged( object sender, PropertyChangedEventArgs e )
    {
      if(sender != null && sender is ThemeEditorViewModel vm )
      {
        ApplyTheme( vm );
      }
    }

    private void ApplyTheme( ThemeEditorViewModel vm )
    {
      Resources[ "BackgroundColor" ] = vm.AccentColor;
      Resources[ "ForegroundColor" ] = vm.ForegroundColor;
      Resources[ "BorderColor" ] = vm.BorderColor;
      Resources[ "BackgroundColor" ] = vm.BackgroundColor ;

      Resources[ "BaseFontSize" ] = vm.BaseFontSize;
      Resources[ "BaseFontFamily" ] = vm.BaseFontFamily;
      Resources[ "ControlCornerRadius" ] = new CornerRadius( vm.CornerRadius );
      Resources[ "TabItemTopCornerRadius" ] = new CornerRadius( vm.CornerRadius, vm.CornerRadius, 0, 0 );
      Resources[ "TabControlCornerRadius" ] = new CornerRadius( 0, vm.CornerRadius, vm.CornerRadius, vm.CornerRadius );
      Resources[ "ControlBorderThickness" ] = new Thickness(vm.ControlBorderThickness );

      Resources[ "AccentBrush" ] = vm.AccentBrush;
			Resources[ "BackgroundBrush" ] = vm.BackgroundBrush;
			Resources[ "ForegroundBrush" ] = vm.ForegroundBrush;
			Resources[ "BorderBrush" ] = vm.BorderBrush;

      Resources[ "ButtonBackgroundColor" ] = vm.ButtonBackgroundColor;
      Resources[ "ButtonForegroundColor" ] = vm.ButtonForegroundColor;
      Resources[ "ButtonHoverColor" ] = vm.ButtonHoverColor;
      Resources[ "ButtonPressedColor" ] = vm.ButtonPressedColor;
      Resources[ "ButtonDisabledColor" ] = vm.ButtonDisabledColor;
      Resources[ "ButtonDisabledForegroundColor" ] = vm.ButtonDisabledForegroundColor;
      Resources[ "ButtonBackgroundBrush" ] = vm.ButtonBackgroundBrush;
      Resources[ "ButtonForegroundBrush" ] = vm.ButtonForegroundBrush;
      Resources[ "ButtonHoverBrush" ] = vm.ButtonHoverBrush;
      Resources[ "ButtonPressedBrush" ] = vm.ButtonPressedBrush;
      Resources[ "ButtonDisabledBrush" ] = vm.ButtonDisabledBrush;
      Resources[ "ButtonDisabledForegroundBrush" ] = vm.ButtonDisabledForegroundBrush;
    }

    private void OnExportTheme( object sender, RoutedEventArgs e )
    {
      if( ( (FrameworkElement)sender ).DataContext is ThemeEditorViewModel vm )
        vm.OnExportTheme();
    }
  }
}
