using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace StyleCreator.Views
{
	public partial class ColorEditor : UserControl
	{
		public ColorEditor()
		{
			InitializeComponent();
		}

		#region ColorProperty
		public static readonly DependencyProperty ColorProperty =
		DependencyProperty.Register(
				nameof(Color),
				typeof(Color),
				typeof(ColorEditor),
				new FrameworkPropertyMetadata(
						default(Color),
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
						OnColorChanged));
		public Color Color
		{
			get => (Color)GetValue(ColorProperty);
			set => SetValue(ColorProperty, value);
		}
    #endregion
    private static void OnColorChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
		{
			if( d is ColorEditor editor &&
					e.NewValue is Color color )
      {
        editor.Red = color.R;
        editor.Green = color.G;
        editor.Blue = color.B;
        editor.HexValue = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
			}
		}
    #region RedProperty
    public static readonly DependencyProperty RedProperty =
    DependencyProperty.Register(
        nameof( Red ),
        typeof( byte ),
        typeof( ColorEditor ),
        new FrameworkPropertyMetadata(
            (byte)0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnRgbChanged ) );
    public byte Red
    {
      get => (byte)GetValue( RedProperty );
      set => SetValue( RedProperty, value );
    }
    #endregion
		#region GreenProperty
    public static readonly DependencyProperty GreenProperty =
    DependencyProperty.Register(
        nameof( Green ),
        typeof( byte ),
        typeof( ColorEditor ),
        new FrameworkPropertyMetadata(
            (byte)0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnRgbChanged ) );
    public byte Green
    {
      get => (byte)GetValue( GreenProperty );
      set => SetValue( GreenProperty, value );
    }
    #endregion
    #region BlueProperty
    public static readonly DependencyProperty BlueProperty =
    DependencyProperty.Register(
        nameof( Blue ),
        typeof( byte ),
        typeof( ColorEditor ),
        new FrameworkPropertyMetadata(
            (byte)0,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnRgbChanged ) );
    public byte Blue
    {
      get => (byte)GetValue( BlueProperty );
      set => SetValue( BlueProperty, value );
    }
    #endregion
    private static void OnRgbChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
      if( d is ColorEditor editor )
      {
        var newColor = Color.FromRgb( editor.Red, editor.Green, editor.Blue );

        if( editor.Color != newColor )
          editor.Color = newColor;
      }
    }
    #region LabelProperty
    public static readonly DependencyProperty LabelProperty =
		DependencyProperty.Register(
				nameof(Label),
				typeof(string),
				typeof(ColorEditor),
				new PropertyMetadata(string.Empty));

		public string Label
		{
			get => (string)GetValue(LabelProperty);
			set => SetValue(LabelProperty, value);
		}
    #endregion
    #region HexValueProperty
    public static readonly DependencyProperty HexValueProperty =
		DependencyProperty.Register(
				nameof(HexValue),
				typeof(string),
				typeof(ColorEditor),
				new FrameworkPropertyMetadata(
						"#000000",
						FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
						OnHexValueChanged));
		public string HexValue
		{
			get => (string)GetValue(HexValueProperty);
			set => SetValue(HexValueProperty, value);
		}
    #endregion
		private static void OnHexValueChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
		{
			if( d is ColorEditor editor &&
					e.NewValue is string hex &&
					TryParse(hex, out var color) )
			{
				editor.Color = color;
			}
		}
		private static bool TryParse( string hex, out Color color )
		{
			color = default;
			if( string.IsNullOrWhiteSpace(hex) )
				return false;

			try
			{
				color = (Color)ColorConverter.ConvertFromString(hex);
				return true;
			}
			catch
			{
				return false;
			}
		}
    #region SlidersVisibleProperty
    public static readonly DependencyProperty SlidersVisibleProperty =
    DependencyProperty.Register(
        nameof( SlidersVisible ),
        typeof( bool ),
        typeof( ColorEditor ),
        new FrameworkPropertyMetadata(
            false,
            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
            OnSliderValueChanged ) );
		public bool SlidersVisible
		{
      get => (bool)GetValue( SlidersVisibleProperty );
      set => SetValue( SlidersVisibleProperty, value );
    }
    #endregion
    private static void OnSliderValueChanged( DependencyObject d, DependencyPropertyChangedEventArgs e )
    {
      if( d is ColorEditor editor && e.NewValue is bool visible)
      {
        editor.SlidersVisible = visible;
      }
    }
  }
}
