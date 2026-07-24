using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace StyleCreator.Tools
{
  public class StyleCreatorCornerRadiusConverter : IValueConverter
  {
    public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( value is not double radius )
        return new CornerRadius( 0 );

      string mode = parameter?.ToString()?.ToLower() ?? "all";

      return mode switch
      {
        "top" => new CornerRadius( radius, radius, 0, 0 ),
        "bottom" => new CornerRadius( 0, 0, radius, radius ),
        "left" => new CornerRadius( radius, 0, 0, radius ),
        "right" => new CornerRadius( 0, radius, radius, 0 ),
        "topleft" => new CornerRadius( radius, 0, 0, 0 ),
        "topright" => new CornerRadius( 0, radius, 0, 0 ),
        "bottomleft" => new CornerRadius( 0, 0, 0, radius ),
        "bottomright" => new CornerRadius( 0, 0, radius, 0 ),
        _ => new CornerRadius( radius )
      };
    }

    public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
    {
      if( value is CornerRadius cr )
        return cr.TopLeft;

      return 0.0;
    }
  }
}
