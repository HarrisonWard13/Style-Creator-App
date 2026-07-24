using Microsoft.Win32;
using StyleCreator.Tools;
using StyleCreator.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StyleCreator.ViewModels
{
  public class ThemeEditorViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    private Color _accentColor;
    private Color _backgroundColor;
    private Color _foregroundColor;
    private Color _borderColor;
    private Brush _accentBrush;
		private Brush _backgroundBrush;
		private Brush _foregroundBrush;
		private Brush _borderBrush;

    private double _baseFontSize;
    private FontFamily _baseFontFamily;
    private double _cornerRadius = 4;
    private double _controlBorderThickness = 1;

    private Color _buttonBackgroundColor;
    private Color _buttonForegroundColor;
    private Color _buttonHoverColor;
    private Color _buttonPressedColor;
    private Color _buttonDisabledColor;
    private Color _buttonDisabledForegroundColor;
    private Brush _buttonBackgroundBrush;
    private Brush _buttonForegroundBrush;
    private Brush _buttonHoverBrush;
    private Brush _buttonPressedBrush;
    private Brush _buttonDisabledBrush;
    private Brush _buttonDisabledForegroundBrush;

    public Color AccentColor
    {
      get => _accentColor;
      set
      {
        if( _accentColor != value )
        {
          _accentColor = value;
          UpdateResource( "AccentColor", value );
          OnPropertyChanged( nameof( AccentColor ) );
          AccentBrush = new SolidColorBrush(AccentColor);
        }
      }
    }
    public Color BackgroundColor
    {
      get => _backgroundColor;
      set
      {
        if( _backgroundColor != value )
        {
          _backgroundColor = value;
          UpdateResource( "BackgroundColor", value );
          OnPropertyChanged( nameof( BackgroundColor ) );
					BackgroundBrush = new SolidColorBrush(BackgroundColor);
				}
      }
    }
    public Color ForegroundColor
    {
      get => _foregroundColor;
      set
      {
        if( _foregroundColor != value )
        {
          _foregroundColor = value;
          UpdateResource( "ForegroundColor", value );
          OnPropertyChanged( nameof( ForegroundColor ) );
					ForegroundBrush = new SolidColorBrush(ForegroundColor);
				}
      }
    }
    public Color BorderColor
    {
      get => _borderColor;
      set
      {
        if( _borderColor != value )
        {
          _borderColor = value;
          UpdateResource( "BorderColor", value );
          OnPropertyChanged( nameof( BorderColor ) );
					BorderBrush = new SolidColorBrush(BorderColor);
				}
      }
    }
    public Brush AccentBrush
    {
      get => _accentBrush;
      set
      {
        if( _accentBrush != value )
        {
          _accentBrush = value;
          UpdateResource( "AccentBrush", value );
          OnPropertyChanged( nameof( AccentBrush ) );
        }
      }
		}
		public Brush BackgroundBrush
		{
			get => _backgroundBrush;
			set
			{
				if( _backgroundBrush != value )
				{
					_backgroundBrush = value;
					UpdateResource("BackgroundBrush", value);
					OnPropertyChanged(nameof(BackgroundBrush));
				}
			}
		}
		public Brush ForegroundBrush
		{
			get => _foregroundBrush;
			set
			{
				if( _foregroundBrush != value )
				{
					_foregroundBrush = value;
					UpdateResource("ForegroundBrush", value);
					OnPropertyChanged(nameof(ForegroundBrush));
				}
			}
		}
		public Brush BorderBrush
		{
			get => _borderBrush;
			set
			{
				if( _borderBrush != value )
				{
					_borderBrush = value;
					UpdateResource("BorderBrush", value);
					OnPropertyChanged(nameof(BorderBrush));
				}
			}
		}
    public double BaseFontSize
    {
      get => _baseFontSize;
      set
      {
        if( _baseFontSize != value && value >= 0 && value < 50)
        {
          _baseFontSize = value;
          OnPropertyChanged( nameof( BaseFontSize ) );
        }
      }
    }
    public FontFamily BaseFontFamily
    {
      get => _baseFontFamily;
      set
      {
        if( _baseFontFamily != value )
        {
          _baseFontFamily = value;
          OnPropertyChanged( nameof( BaseFontFamily ) );
        }
      }
    }
    public double CornerRadius
    {
      get => _cornerRadius;
      set
      {
        if( _cornerRadius != value )
        {
          _cornerRadius = value;
          OnPropertyChanged( nameof( CornerRadius ) );
        }
      }
    }
    public double ControlBorderThickness
    {
      get => _controlBorderThickness;
      set
      {
        if( _controlBorderThickness != value )
        {
          _controlBorderThickness = value;
          OnPropertyChanged( nameof( _controlBorderThickness ) );
        }
      }
    }
    public Color ButtonBackgroundColor
    {
      get => _buttonBackgroundColor;
      set
      {
        if( _buttonBackgroundColor != value )
        {
          _buttonBackgroundColor = value;
          UpdateResource( "ButtonBackgroundColor", value );
          OnPropertyChanged( nameof( ButtonBackgroundColor ) );
          ButtonBackgroundBrush = new SolidColorBrush( ButtonBackgroundColor );
        }
      }
    }
    public Color ButtonForegroundColor
    {
      get => _buttonForegroundColor;
      set
      {
        if( _buttonForegroundColor != value )
        {
          _buttonForegroundColor = value;
          UpdateResource( "ButtonForegroundColor", value );
          OnPropertyChanged( nameof( ButtonForegroundColor ) );
          ButtonForegroundBrush = new SolidColorBrush( ButtonForegroundColor );
        }
      }
    }
    public Color ButtonHoverColor
    {
      get => _buttonHoverColor;
      set
      {
        if( _buttonHoverColor != value )
        {
          _buttonHoverColor = value;
          UpdateResource( "ButtonHoverColor", value );
          OnPropertyChanged( nameof( ButtonHoverColor ) );
          ButtonHoverBrush = new SolidColorBrush( ButtonHoverColor );
        }
      }
    }
    public Color ButtonPressedColor
    {
      get => _buttonPressedColor;
      set
      {
        if( _buttonPressedColor != value )
        {
          _buttonPressedColor = value;
          UpdateResource( "ButtonPressedColor", value );
          OnPropertyChanged( nameof( ButtonPressedColor ) );
          ButtonPressedBrush = new SolidColorBrush( ButtonPressedColor );
        }
      }
    }
    public Color ButtonDisabledColor
    {
      get => _buttonDisabledColor;
      set
      {
        if( _buttonDisabledColor != value )
        {
          _buttonDisabledColor = value;
          UpdateResource( "ButtonDisabledColor", value );
          OnPropertyChanged( nameof( ButtonDisabledColor ) );
          ButtonDisabledBrush = new SolidColorBrush( ButtonDisabledColor );
        }
      }
    }
    public Color ButtonDisabledForegroundColor
    {
      get => _buttonDisabledForegroundColor;
      set
      {
        if( _buttonDisabledForegroundColor != value )
        {
          _buttonDisabledForegroundColor = value;
          UpdateResource( "ButtonDisabledForegroundColor", value );
          OnPropertyChanged( nameof( ButtonDisabledForegroundColor ) );
          ButtonDisabledForegroundBrush = new SolidColorBrush( ButtonDisabledForegroundColor );
        }
      }
    }
    public Brush ButtonBackgroundBrush
    {
      get => _buttonBackgroundBrush;
      set
      {
        if( _buttonBackgroundBrush != value )
        {
          _buttonBackgroundBrush = value;
          UpdateResource( "ButtonBackgroundBrush", value );
          OnPropertyChanged( nameof( ButtonBackgroundBrush ) );
        }
      }
    }
    public Brush ButtonForegroundBrush
    {
      get => _buttonForegroundBrush;
      set
      {
        if( _buttonForegroundBrush != value )
        {
          _buttonForegroundBrush = value;
          UpdateResource( "ButtonForegroundBrush", value );
          OnPropertyChanged( nameof( ButtonForegroundBrush ) );
        }
      }
    }
    public Brush ButtonHoverBrush
    {
      get => _buttonHoverBrush;
      set
      {
        if( _buttonHoverBrush != value )
        {
          _buttonHoverBrush = value;
          UpdateResource( "ButtonHoverBrush", value );
          OnPropertyChanged( nameof( ButtonHoverBrush ) );
        }
      }
    }
    public Brush ButtonPressedBrush
    {
      get => _buttonPressedBrush;
      set
      {
        if( _buttonPressedBrush != value )
        {
          _buttonPressedBrush = value;
          UpdateResource( "ButtonPressedBrush", value );
          OnPropertyChanged( nameof( ButtonPressedBrush ) );
        }
      }
    }
    public Brush ButtonDisabledBrush
    {
      get => _buttonDisabledBrush;
      set
      {
        if( _buttonDisabledBrush != value )
        {
          _buttonDisabledBrush = value;
          UpdateResource( "ButtonDisabledBrush", value );
          OnPropertyChanged( nameof( ButtonDisabledBrush ) );
        }
      }
    }
    public Brush ButtonDisabledForegroundBrush
    {
      get => _buttonDisabledForegroundBrush;
      set
      {
        if( _buttonDisabledForegroundBrush != value )
        {
          _buttonDisabledForegroundBrush = value;
          UpdateResource( "ButtonDisabledForegroundBrush", value );
          OnPropertyChanged( nameof( ButtonDisabledForegroundBrush ) );
        }
      }
    }
    public ObservableCollection<ListItem> Items { get; } =
    new ObservableCollection<ListItem>
    {
        new ListItem { Name = "Apple", Value = "10" },
        new ListItem { Name = "Banana", Value = "20" },
        new ListItem { Name = "Cherry", Value = "30" }
    };
    public ThemeEditorViewModel()
    {
      _accentColor = (Color)Application.Current.Resources[ "AccentColor" ];
      _backgroundColor = (Color)Application.Current.Resources[ "BackgroundColor" ];
      _foregroundColor = (Color)Application.Current.Resources[ "ForegroundColor" ];
      _borderColor = (Color)Application.Current.Resources[ "BorderColor" ];

      _baseFontSize = (double)Application.Current.Resources[ "BaseFontSize" ];
      _baseFontFamily = (FontFamily)Application.Current.Resources[ "BaseFontFamily" ];

      _accentBrush = (Brush)Application.Current.Resources[ "AccentBrush" ];
			_backgroundBrush = (Brush)Application.Current.Resources["BackgroundBrush"];
			_foregroundBrush = (Brush)Application.Current.Resources["ForegroundBrush"];
			_borderBrush = (Brush)Application.Current.Resources["BorderBrush"];

      _buttonBackgroundColor = (Color)Application.Current.Resources[ "ButtonBackgroundColor" ];
      _buttonForegroundColor = (Color)Application.Current.Resources[ "ButtonForegroundColor" ];
      _buttonHoverColor = (Color)Application.Current.Resources[ "ButtonHoverColor" ];
      _buttonPressedColor = (Color)Application.Current.Resources[ "ButtonPressedColor" ];
      _buttonDisabledColor = (Color)Application.Current.Resources[ "ButtonDisabledColor" ];
      _buttonDisabledForegroundColor = (Color)Application.Current.Resources[ "ButtonDisabledForegroundColor" ];
      _buttonBackgroundBrush = (Brush)Application.Current.Resources[ "ButtonBackgroundBrush" ];
      _buttonForegroundBrush = (Brush)Application.Current.Resources[ "ButtonForegroundBrush" ];
      _buttonHoverBrush = (Brush)Application.Current.Resources[ "ButtonHoverBrush" ];
      _buttonPressedBrush = (Brush)Application.Current.Resources[ "ButtonPressedBrush" ];
      _buttonDisabledBrush = (Brush)Application.Current.Resources[ "ButtonDisabledBrush" ];
      _buttonDisabledForegroundBrush = (Brush)Application.Current.Resources[ "ButtonDisabledForegroundBrush" ];
    }
    public void LoadFromResources()
    {
      AccentColor = (Color)Application.Current.Resources[ "AccentColor" ];
      BackgroundColor = (Color)Application.Current.Resources[ "BackgroundColor" ];
      ForegroundColor = (Color)Application.Current.Resources[ "ForegroundColor" ];
      BorderColor = (Color)Application.Current.Resources[ "BorderColor" ];

      BaseFontSize = (double)Application.Current.Resources[ "BaseFontSize" ];
      BaseFontFamily = (FontFamily)Application.Current.Resources[ "BaseFontFamily" ];

      AccentBrush = (Brush)Application.Current.Resources["AccentBrush"];
			BackgroundBrush = (Brush)Application.Current.Resources["BackgroundBrush"];
			ForegroundBrush = (Brush)Application.Current.Resources["ForegroundBrush"];
			BorderBrush = (Brush)Application.Current.Resources["BorderBrush"];

      ButtonBackgroundColor = (Color)Application.Current.Resources[ "ButtonBackgroundColor" ];
      ButtonForegroundColor = (Color)Application.Current.Resources[ "ButtonForegroundColor" ];
      ButtonHoverColor = (Color)Application.Current.Resources[ "ButtonHoverColor" ];
      ButtonPressedColor = (Color)Application.Current.Resources[ "ButtonPressedColor" ];
      ButtonDisabledColor = (Color)Application.Current.Resources[ "ButtonDisabledColor" ];
      ButtonDisabledForegroundColor = (Color)Application.Current.Resources[ "ButtonDisabledForegroundColor" ];
      ButtonBackgroundBrush = (Brush)Application.Current.Resources[ "ButtonBackgroundBrush" ];
      ButtonForegroundBrush = (Brush)Application.Current.Resources[ "ButtonForegroundBrush" ];
      ButtonHoverBrush = (Brush)Application.Current.Resources[ "ButtonHoverBrush" ];
      ButtonPressedBrush = (Brush)Application.Current.Resources[ "ButtonPressedBrush" ];
      ButtonDisabledBrush = (Brush)Application.Current.Resources[ "ButtonDisabledBrush" ];
      ButtonDisabledForegroundBrush = (Brush)Application.Current.Resources[ "ButtonDisabledForegroundBrush" ];
    }
    private void UpdateResource( string key, Color color )
    {
      Application.Current.Resources[ key ] = color;
    }
    private void UpdateResource( string key, Brush brush )
    {
      Application.Current.Resources[ key ] = brush;
    }
    public void OnExportTheme()
    {
      ThemeImporterExporter exporter = new ThemeImporterExporter();
      exporter.OnExport(this);
    }
    
    private void OnPropertyChanged( string propertyName )
        => PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
  }

  public class ListItem
  {
    public string Name { get; set; }
    public string Value { get; set; }
  }
}
