using Microsoft.Win32;
using StyleCreator.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StyleCreator.Tools
{
  public class ThemeImporterExporter
  {
    public void OnExport( ThemeEditorViewModel VM )
    {
      var dialog = new SaveFileDialog
      {
        Title = "Export Theme",
        FileName = "Theme.xaml",
        DefaultExt = ".xaml",
        Filter = "XAML Files (*.xaml)|*.xaml"
      };

      if( dialog.ShowDialog() == true )
      {
        string path = dialog.FileName;
        ExportTheme( VM, path );
      }
    }
    private void ExportTheme( ThemeEditorViewModel VM, string path )
    {
      string xaml = $@"<ResourceDictionary
  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
  xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
  xmlns:sys=""clr-namespace:System;assembly=mscorlib"">";

      // ======================
      // Colors/Text/Alignment
      // ======================
      xaml += $@"
  <!-- Colors -->
  <Color x:Key=""AccentColor"">{VM.AccentColor}</Color>
  <Color x:Key=""BackgroundColor"">{VM.BackgroundColor}</Color>
  <Color x:Key=""ForegroundColor"">{VM.ForegroundColor}</Color>
  <Color x:Key=""BorderColor"">{VM.BorderColor}</Color>

  <!-- Brushes -->
  <SolidColorBrush x:Key=""AccentBrush"" Color=""{{DynamicResource AccentColor}}"" />
  <SolidColorBrush x:Key=""BackgroundBrush"" Color=""{{DynamicResource BackgroundColor}}"" />
  <SolidColorBrush x:Key=""ForegroundBrush"" Color=""{{DynamicResource ForegroundColor}}"" />
  <SolidColorBrush x:Key=""BorderBrush"" Color=""{{DynamicResource BorderColor}}"" />

  <!--  Button colors  -->
  <Color x:Key=""ButtonBackgroundColor"">{VM.ButtonBackgroundColor}</Color>
  <Color x:Key=""ButtonHoverColor"">{VM.ButtonHoverColor}</Color>
  <Color x:Key=""ButtonPressedColor"">{VM.ButtonPressedColor}</Color>
  <Color x:Key=""ButtonDisabledColor"">{VM.ButtonDisabledColor}</Color>
  <Color x:Key=""ButtonForegroundColor"">{VM.ButtonForegroundColor}</Color>
  <Color x:Key=""ButtonDisabledForegroundColor"">{VM.ButtonDisabledForegroundColor}</Color>

  <!--  Button Brushes  -->
  <SolidColorBrush x:Key=""ButtonBackgroundBrush"" Color=""{{DynamicResource ButtonBackgroundColor}}"" />
  <SolidColorBrush x:Key=""ButtonHoverBrush"" Color=""{{DynamicResource ButtonHoverColor}}"" />
  <SolidColorBrush x:Key=""ButtonPressedBrush"" Color=""{{DynamicResource ButtonPressedColor}}"" />
  <SolidColorBrush x:Key=""ButtonDisabledBrush"" Color=""{{DynamicResource ButtonDisabledColor}}"" />
  <SolidColorBrush x:Key=""ButtonForegroundBrush"" Color=""{{DynamicResource ButtonForegroundColor}}"" />
  <SolidColorBrush x:Key=""ButtonDisabledForegroundBrush"" Color=""{{DynamicResource ButtonDisabledForegroundColor}}"" />

  <!-- Layout -->
  <CornerRadius x:Key=""ControlCornerRadius"">{VM.CornerRadius}</CornerRadius>
  <CornerRadius x:Key=""TabItemTopCornerRadius"">{VM.CornerRadius},{VM.CornerRadius},0,0</CornerRadius>
  <CornerRadius x:Key=""TabControlCornerRadius"">0,{VM.CornerRadius},{VM.CornerRadius},{VM.CornerRadius}</CornerRadius>
  <Thickness x:Key=""ControlPadding"">8,4</Thickness>
  <Thickness x:Key=""ControlMargin"">0</Thickness>

  <Thickness x:Key=""FocusBorderThickness"">2</Thickness>
  <Thickness x:Key=""ControlBorderThickness"">1</Thickness>
  <Thickness x:Key=""GroupBoxBorderThickness"">1</Thickness>

  <!--  =====================  -->
  <!--  Typography  -->
  <!--  =====================  -->

  <FontFamily x:Key=""BaseFontFamily"">{VM.BaseFontFamily}</FontFamily>
  <sys:Double x:Key=""BaseFontSize"">{VM.BaseFontSize}</sys:Double>
";

      // ======================
      // Base
      // ======================
      xaml += $@"
  <!--  =====================  -->
  <!--  Base Control Style  -->
  <!--  =====================  -->

  <Style x:Key=""BaseControlStyle"" TargetType=""Control"">
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""Padding"" Value=""{{DynamicResource ControlPadding}}"" />
    <Setter Property=""Margin"" Value=""{{DynamicResource ControlMargin}}"" />
    <Setter Property=""SnapsToDevicePixels"" Value=""True"" />
    <Setter Property=""UseLayoutRounding"" Value=""True"" />
  </Style>
";

      // ======================
      // Button
      // ======================
      xaml += $@"
<Style BasedOn=""{{StaticResource BaseControlStyle}}"" TargetType=""Button"">

    <Setter Property=""Foreground"" Value=""{{DynamicResource ButtonForegroundBrush}}"" />
    <Setter Property=""Background"" Value=""{{DynamicResource ButtonBackgroundBrush}}"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource BorderBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""{{DynamicResource ControlBorderThickness}}"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""Cursor"" Value=""Hand"" />

    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""Button"">
          <Border x:Name=""Root""
                  Background=""{{TemplateBinding Background}}""
                  BorderBrush=""{{TemplateBinding BorderBrush}}""
                  BorderThickness=""{{TemplateBinding BorderThickness}}""
                  CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

            <ContentPresenter HorizontalAlignment=""Center""
                              VerticalAlignment=""Center""
                              RecognizesAccessKey=""True"" />
          </Border>

          <ControlTemplate.Triggers>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""Root"" Property=""Background"" Value=""{{DynamicResource ButtonHoverBrush}}"" />
            </Trigger>

            <!--  Pressed  -->
            <Trigger Property=""IsPressed"" Value=""True"">
              <Setter TargetName=""Root"" Property=""Background"" Value=""{{DynamicResource ButtonPressedBrush}}"" />
            </Trigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter TargetName=""Root"" Property=""Background"" Value=""{{DynamicResource ButtonDisabledBrush}}"" />
              <Setter Property=""Foreground"" Value=""{{DynamicResource ButtonDisabledForegroundBrush}}"" />
              <Setter Property=""Cursor"" Value=""Arrow"" />
            </Trigger>

          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>

  </Style>
";

      // ======================
      // CheckBox
      // ======================
      xaml += $@"
<Style TargetType=""CheckBox"">
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""Padding"" Value=""6,0"" />
    <Setter Property=""VerticalContentAlignment"" Value=""Center"" />
    <Setter Property=""SnapsToDevicePixels"" Value=""True"" />

    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""CheckBox"">
          <Grid>
            <Grid.ColumnDefinitions>
              <ColumnDefinition Width=""Auto"" />
              <ColumnDefinition Width=""*"" />
            </Grid.ColumnDefinitions>

            <!--  Checkbox box  -->
            <Border x:Name=""Box""
                    Width=""16""
                    Height=""16""
                    VerticalAlignment=""Center""
                    Background=""{{DynamicResource BackgroundBrush}}""
                    BorderBrush=""{{DynamicResource BorderBrush}}""
                    BorderThickness=""1""
                    CornerRadius=""{{DynamicResource ControlCornerRadius}}""
                    Cursor=""Hand"">

              <!--  Checkmark  -->
              <Path x:Name=""CheckMark""
                    Cursor=""Hand""
                    Data=""M 2 8 L 6 12 L 14 2""
                    Stroke=""{{DynamicResource ButtonPressedBrush}}""
                    StrokeThickness=""2""
                    Visibility=""Collapsed"" />
            </Border>

            <!--  Content  -->
            <ContentPresenter Grid.Column=""1""
                              Margin=""{{TemplateBinding Padding}}""
                              VerticalAlignment=""{{TemplateBinding VerticalContentAlignment}}""
                              RecognizesAccessKey=""True""
                              TextBlock.FontFamily=""{{DynamicResource BaseFontFamily}}""
                              TextBlock.FontSize=""{{DynamicResource BaseFontSize}}"" />
          </Grid>

          <ControlTemplate.Triggers>
            <!--  Checked  -->
            <Trigger Property=""IsChecked"" Value=""True"">
              <Setter TargetName=""CheckMark"" Property=""Visibility"" Value=""Visible"" />
              <Setter TargetName=""Box"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <!--  Indeterminate  -->
            <Trigger Property=""IsChecked"" Value=""{{x:Null}}"">
              <Setter TargetName=""CheckMark"" Property=""Data"" Value=""M 3 8 L 13 8"" />
              <Setter TargetName=""CheckMark"" Property=""Visibility"" Value=""Visible"" />
            </Trigger>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""Box"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.6"" />
            </Trigger>
          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // ComboBox
      // ======================
      xaml += $@"
<Style TargetType=""ComboBox"">
    <Setter Property=""Background"" Value=""{{DynamicResource BackgroundBrush}}"" />
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource BorderBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""{{DynamicResource ControlBorderThickness}}"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""Padding"" Value=""6,4"" />
    <Setter Property=""MinHeight"" Value=""28"" />
    <Setter Property=""SnapsToDevicePixels"" Value=""True"" />

    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""ComboBox"">
          <Grid>
            <!--  Main Border  -->
            <Border x:Name=""Border""
                    Background=""{{TemplateBinding Background}}""
                    BorderBrush=""{{TemplateBinding BorderBrush}}""
                    BorderThickness=""{{TemplateBinding BorderThickness}}""
                    CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

              <Grid>
                <Grid.ColumnDefinitions>
                  <ColumnDefinition />
                  <ColumnDefinition Width=""32"" />
                </Grid.ColumnDefinitions>

                <!--  Selected Item  -->
                <ContentPresenter Margin=""{{TemplateBinding Padding}}""
                                  HorizontalAlignment=""Left""
                                  VerticalAlignment=""Center""
                                  Content=""{{TemplateBinding SelectionBoxItem}}""
                                  ContentTemplate=""{{TemplateBinding SelectionBoxItemTemplate}}""
                                  ContentTemplateSelector=""{{TemplateBinding ItemTemplateSelector}}"" />

                <!--  Drop-down Button  -->
                <ToggleButton x:Name=""PART_ToggleButton""
                              Grid.Column=""1""
                              Background=""Transparent""
                              BorderThickness=""0""
                              Focusable=""False""
                              IsChecked=""{{Binding IsDropDownOpen, Mode=TwoWay, RelativeSource={{RelativeSource TemplatedParent}}}}"">
                  <Path HorizontalAlignment=""Center""
                        VerticalAlignment=""Center""
                        Data=""M 0 0 L 4 4 L 8 0 Z""
                        Fill=""{{DynamicResource ForegroundBrush}}"" />
                </ToggleButton>
              </Grid>
            </Border>

            <!--  Popup  -->
            <Popup x:Name=""PART_Popup""
                   AllowsTransparency=""True""
                   Focusable=""False""
                   IsOpen=""{{TemplateBinding IsDropDownOpen}}""
                   Placement=""Bottom""
                   PopupAnimation=""Fade"">

              <Border Padding=""2""
                      Background=""{{DynamicResource BackgroundBrush}}""
                      BorderBrush=""{{DynamicResource BorderBrush}}""
                      BorderThickness=""1""
                      CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

                <ScrollViewer>
                  <ItemsPresenter />
                </ScrollViewer>

              </Border>
            </Popup>
          </Grid>

          <!--  States  -->
          <ControlTemplate.Triggers>
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""Border"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <Trigger Property=""IsFocused"" Value=""True"">
              <Setter TargetName=""Border"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.6"" />
            </Trigger>
          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // ComboBoxItem
      // ======================
      xaml += $@"
  <Style TargetType=""ComboBoxItem"">
    <Setter Property=""Background"" Value=""Transparent"" />
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""{{DynamicResource ControlBorderThickness}}"" />
    <Setter Property=""Padding"" Value=""6,4"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""HorizontalContentAlignment"" Value=""Left"" />
    <Setter Property=""VerticalContentAlignment"" Value=""Center"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""ComboBoxItem"">
          <Border x:Name=""Border""
                  Background=""{{TemplateBinding Background}}""
                  CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

            <ContentPresenter Margin=""{{TemplateBinding Padding}}""
                              HorizontalAlignment=""{{TemplateBinding HorizontalContentAlignment}}""
                              VerticalAlignment=""{{TemplateBinding VerticalContentAlignment}}""
                              RecognizesAccessKey=""True"" />
          </Border>

          <ControlTemplate.Triggers>

            <!--  Hover  -->
            <Trigger Property=""IsHighlighted"" Value=""True"">
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource ButtonHoverBrush}}"" />
            </Trigger>

            <!--  Selected  -->
            <Trigger Property=""IsSelected"" Value=""True"">
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource AccentBrush}}"" />
              <Setter Property=""Foreground"" Value=""White"" />
            </Trigger>

            <!--  Selected + Hover  -->
            <MultiTrigger>
              <MultiTrigger.Conditions>
                <Condition Property=""IsSelected"" Value=""True"" />
                <Condition Property=""IsHighlighted"" Value=""True"" />
              </MultiTrigger.Conditions>
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource AccentBrush}}"" />
            </MultiTrigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.5"" />
            </Trigger>

          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // GridViewColumnHeader
      // ======================
      xaml += $@"
  <Style TargetType=""GridViewColumnHeader"">
    <Setter Property=""Background"" Value=""{{DynamicResource BackgroundBrush}}"" />
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""Padding"" Value=""8,4"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource BorderBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""0,0,0,1"" />
    <Setter Property=""HorizontalContentAlignment"" Value=""Left"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""GridViewColumnHeader"">
          <Border Background=""{{TemplateBinding Background}}""
                  BorderBrush=""{{TemplateBinding BorderBrush}}""
                  BorderThickness=""{{TemplateBinding BorderThickness}}"">

            <ContentPresenter Margin=""{{TemplateBinding Padding}}""
                              HorizontalAlignment=""{{TemplateBinding HorizontalContentAlignment}}""
                              VerticalAlignment=""Center"" />
          </Border>

          <ControlTemplate.Triggers>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter Property=""Background"" Value=""{{DynamicResource ButtonHoverBrush}}"" />
            </Trigger>

            <!--  Pressed  -->
            <Trigger Property=""IsPressed"" Value=""True"">
              <Setter Property=""Background"" Value=""{{DynamicResource AccentBrush}}"" />
              <Setter Property=""Foreground"" Value=""White"" />
            </Trigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.5"" />
            </Trigger>

          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // ListBox
      // ======================
      xaml += $@"
  <Style TargetType=""ListBox"">
    <Setter Property=""Background"" Value=""{{DynamicResource BackgroundBrush}}"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource BorderBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""{{DynamicResource ControlBorderThickness}}"" />
    <Setter Property=""Padding"" Value=""2"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""ListBox"">
          <Border Background=""{{TemplateBinding Background}}""
                  BorderBrush=""{{TemplateBinding BorderBrush}}""
                  BorderThickness=""{{TemplateBinding BorderThickness}}""
                  CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

            <ScrollViewer Padding=""{{TemplateBinding Padding}}"" Focusable=""False"">
              <ItemsPresenter />
            </ScrollViewer>
          </Border>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // ListBoxItem
      // ======================
      xaml += $@"
  <Style TargetType=""ListBoxItem"">
    <Setter Property=""Background"" Value=""Transparent"" />
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""Padding"" Value=""6,4"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""HorizontalContentAlignment"" Value=""Left"" />
    <Setter Property=""VerticalContentAlignment"" Value=""Center"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""ListBoxItem"">
          <Border x:Name=""Border""
                  Background=""{{TemplateBinding Background}}""
                  CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

            <ContentPresenter Margin=""{{TemplateBinding Padding}}""
                              HorizontalAlignment=""{{TemplateBinding HorizontalContentAlignment}}""
                              VerticalAlignment=""{{TemplateBinding VerticalContentAlignment}}""
                              RecognizesAccessKey=""True"" />
          </Border>

          <ControlTemplate.Triggers>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource ButtonHoverBrush}}"" />
            </Trigger>

            <!--  Selected  -->
            <Trigger Property=""IsSelected"" Value=""True"">
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource AccentBrush}}"" />
              <Setter Property=""Foreground"" Value=""White"" />
            </Trigger>

            <!--  Selected + Hover  -->
            <MultiTrigger>
              <MultiTrigger.Conditions>
                <Condition Property=""IsSelected"" Value=""True"" />
                <Condition Property=""IsMouseOver"" Value=""True"" />
              </MultiTrigger.Conditions>
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource AccentBrush}}"" />
            </MultiTrigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.5"" />
            </Trigger>

          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // ListView
      // ======================
      xaml += $@"
  <Style TargetType=""ListView"">
    <Setter Property=""Background"" Value=""{{DynamicResource BackgroundBrush}}"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource BorderBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""{{DynamicResource ControlBorderThickness}}"" />
    <Setter Property=""Padding"" Value=""2"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""ListView"">
          <Border Background=""{{TemplateBinding Background}}""
                  BorderBrush=""{{TemplateBinding BorderBrush}}""
                  BorderThickness=""{{TemplateBinding BorderThickness}}""
                  CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

            <ScrollViewer Padding=""{{TemplateBinding Padding}}"" Focusable=""False"">
              <ItemsPresenter />
            </ScrollViewer>
          </Border>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // ListViewItem
      // ======================
      xaml += $@"
  <Style TargetType=""ListViewItem"">
    <Setter Property=""Background"" Value=""Transparent"" />
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""Padding"" Value=""6,4"" />
    <Setter Property=""HorizontalContentAlignment"" Value=""Left"" />
    <Setter Property=""VerticalContentAlignment"" Value=""Center"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""ListViewItem"">
          <Border x:Name=""Border"" Background=""{{TemplateBinding Background}}"">
            <GridViewRowPresenter Margin=""{{TemplateBinding Padding}}""
                                  Columns=""{{Binding RelativeSource={{RelativeSource AncestorType=ListView}}, Path=View.Columns}}""
                                  Content=""{{TemplateBinding Content}}"" />
          </Border>

          <ControlTemplate.Triggers>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource HoverBrush}}"" />
            </Trigger>

            <!--  Selected  -->
            <Trigger Property=""IsSelected"" Value=""True"">
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource ButtonPressedBrush}}"" />
              <Setter Property=""Foreground"" Value=""White"" />
            </Trigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.5"" />
            </Trigger>

          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // PasswordBox
      // ======================
      xaml += $@"
  <Style BasedOn=""{{StaticResource BaseControlStyle}}"" TargetType=""PasswordBox"">
    <Setter Property=""Background"" Value=""{{DynamicResource BackgroundBrush}}"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource BorderBrush}}"" />
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""{{DynamicResource ControlBorderThickness}}"" />
    <Setter Property=""Padding"" Value=""8,4"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""VerticalContentAlignment"" Value=""Center"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""PasswordBox"">
          <Border x:Name=""Border""
                  Background=""{{TemplateBinding Background}}""
                  BorderBrush=""{{TemplateBinding BorderBrush}}""
                  BorderThickness=""{{TemplateBinding BorderThickness}}""
                  CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

            <!--  Required PART  -->
            <ScrollViewer x:Name=""PART_ContentHost"" Margin=""{{TemplateBinding Padding}}"" />
          </Border>

          <ControlTemplate.Triggers>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""Border"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <!--  Focus  -->
            <Trigger Property=""IsKeyboardFocused"" Value=""True"">
              <Setter TargetName=""Border"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.5"" />
            </Trigger>

          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // RadioButton
      // ======================
      xaml += $@"
  <Style TargetType=""RadioButton"">
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""Padding"" Value=""6,0"" />
    <Setter Property=""VerticalContentAlignment"" Value=""Center"" />
    <Setter Property=""SnapsToDevicePixels"" Value=""True"" />

    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""RadioButton"">
          <Grid>
            <Grid.ColumnDefinitions>
              <ColumnDefinition Width=""Auto"" />
              <ColumnDefinition Width=""*"" />
            </Grid.ColumnDefinitions>

            <!--  Outer Circle  -->
            <Border x:Name=""OuterCircle""
                    Width=""16""
                    Height=""16""
                    VerticalAlignment=""Center""
                    Background=""{{DynamicResource BackgroundBrush}}""
                    BorderBrush=""{{DynamicResource BorderBrush}}""
                    BorderThickness=""1""
                    CornerRadius=""8""
                    Cursor=""Hand"">

              <!--  Inner Dot  -->
              <Ellipse x:Name=""InnerDot""
                       Width=""8""
                       Height=""8""
                       HorizontalAlignment=""Center""
                       VerticalAlignment=""Center""
                       Cursor=""Hand""
                       Fill=""{{DynamicResource ButtonPressedBrush}}""
                       Visibility=""Collapsed"" />
            </Border>

            <!--  Content  -->
            <ContentPresenter Grid.Column=""1""
                              Margin=""{{TemplateBinding Padding}}""
                              VerticalAlignment=""{{TemplateBinding VerticalContentAlignment}}""
                              RecognizesAccessKey=""True""
                              TextBlock.FontFamily=""{{DynamicResource BaseFontFamily}}""
                              TextBlock.FontSize=""{{DynamicResource BaseFontSize}}"" />
          </Grid>

          <ControlTemplate.Triggers>
            <!--  Checked  -->
            <Trigger Property=""IsChecked"" Value=""True"">
              <Setter TargetName=""InnerDot"" Property=""Visibility"" Value=""Visible"" />
              <Setter TargetName=""OuterCircle"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""OuterCircle"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.6"" />
            </Trigger>
          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // ScrollBar
      // ======================
      xaml += $@"
  <!--  =====================  -->
  <!--  Thumb  -->
  <!--  =====================  -->
  <Style x:Key=""ThemeScrollBarThumb"" TargetType=""Thumb"">
    <Setter Property=""Background"" Value=""{{DynamicResource ButtonBackgroundBrush}}"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""Thumb"">
          <Border Background=""{{TemplateBinding Background}}"" CornerRadius=""{{DynamicResource ControlCornerRadius}}"" />
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>

  <!--  =====================  -->
  <!--  RepeatButton  -->
  <!--  =====================  -->
  <Style x:Key=""ThemeScrollBarButton"" TargetType=""RepeatButton"">
    <Setter Property=""Background"" Value=""{{DynamicResource BackgroundBrush}}"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""RepeatButton"">
          <Border Background=""{{TemplateBinding Background}}"" />
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>

  <!--  =====================  -->
  <!--  ScrollBar  -->
  <!--  =====================  -->
  <Style TargetType=""ScrollBar"">
    <Setter Property=""Width"" Value=""12"" />
    <Setter Property=""Background"" Value=""{{DynamicResource BackgroundBrush}}"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""ScrollBar"">
          <Grid Background=""{{TemplateBinding Background}}"">
            <Track x:Name=""PART_Track"" IsDirectionReversed=""true"">

              <Track.DecreaseRepeatButton>
                <RepeatButton Command=""ScrollBar.LineUpCommand"" Style=""{{StaticResource ThemeScrollBarButton}}"" />
              </Track.DecreaseRepeatButton>

              <Track.Thumb>
                <Thumb Style=""{{StaticResource ThemeScrollBarThumb}}"" />
              </Track.Thumb>

              <Track.IncreaseRepeatButton>
                <RepeatButton Command=""ScrollBar.LineDownCommand"" Style=""{{StaticResource ThemeScrollBarButton}}"" />
              </Track.IncreaseRepeatButton>

            </Track>
          </Grid>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // TabView
      // ======================
      xaml += $@"
 <!--  =====================  -->
  <!--  TabItem  -->
  <!--  =====================  -->
  <Style TargetType=""TabItem"">
    <Setter Property=""Foreground"" Value=""{{DynamicResource ButtonForegroundBrush}}"" />
    <Setter Property=""Background"" Value=""{{DynamicResource ButtonBackgroundBrush}}"" />
    <Setter Property=""Padding"" Value=""10,4"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""TabItem"">
          <Border x:Name=""Border""
                  Padding=""{{TemplateBinding Padding}}""
                  Background=""{{TemplateBinding Background}}""
                  CornerRadius=""{{DynamicResource TabItemTopCornerRadius}}"">
            <ContentPresenter HorizontalAlignment=""Center""
                              VerticalAlignment=""Center""
                              ContentSource=""Header"" />
          </Border>

          <ControlTemplate.Triggers>

            <!--  Selected  -->
            <Trigger Property=""IsSelected"" Value=""True"">
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource ButtonPressedBrush}}"" />
              <Setter Property=""Foreground"" Value=""White"" />
            </Trigger>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""Border"" Property=""Background"" Value=""{{DynamicResource ButtonHoverBrush}}"" />
            </Trigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.5"" />
            </Trigger>

          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>

  <!--  =====================  -->
  <!--  TabControl  -->
  <!--  =====================  -->
  <Style TargetType=""TabControl"">
    <Setter Property=""Background"" Value=""{{DynamicResource ButtonBackgroundBrush}}"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource ButtonBorderBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""1"" />
    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""TabControl"">
          <DockPanel>

            <TabPanel Background=""{{DynamicResource AccentBrush}}""
                      DockPanel.Dock=""Top""
                      IsItemsHost=""True"" />

            <Border Background=""{{DynamicResource BackgroundBrush}}""
                    BorderBrush=""{{TemplateBinding BorderBrush}}""
                    BorderThickness=""{{TemplateBinding BorderThickness}}""
                    CornerRadius=""{{DynamicResource TabControlCornerRadius}}"">

              <ContentPresenter Margin=""6"" ContentSource=""SelectedContent"" />

            </Border>

          </DockPanel>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // TextBox
      // ======================
      xaml += $@"
  <Style TargetType=""TextBox"">
    <Setter Property=""Background"" Value=""{{DynamicResource BackgroundBrush}}"" />
    <Setter Property=""Foreground"" Value=""{{DynamicResource ForegroundBrush}}"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource BorderBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""{{DynamicResource ControlBorderThickness}}"" />
    <Setter Property=""Padding"" Value=""6,4"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""MinHeight"" Value=""28"" />
    <Setter Property=""VerticalContentAlignment"" Value=""Center"" />
    <Setter Property=""SnapsToDevicePixels"" Value=""True"" />

    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""TextBox"">
          <Border x:Name=""Border""
                  Background=""{{TemplateBinding Background}}""
                  BorderBrush=""{{TemplateBinding BorderBrush}}""
                  BorderThickness=""{{TemplateBinding BorderThickness}}""
                  CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

            <!--  THIS IS THE IMPORTANT PART  -->
            <ScrollViewer x:Name=""PART_ContentHost""
                          Margin=""{{TemplateBinding Padding}}""
                          Focusable=""False"" />
          </Border>

          <ControlTemplate.Triggers>
            <Trigger Property=""IsFocused"" Value=""True"">
              <Setter TargetName=""Border"" Property=""BorderBrush"" Value=""{{DynamicResource AccentBrush}}"" />
            </Trigger>

            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter Property=""Opacity"" Value=""0.6"" />
            </Trigger>
          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>
  </Style>
";

      // ======================
      // ToggleButton
      // ======================
      xaml += $@"
  <Style BasedOn=""{{StaticResource BaseControlStyle}}"" TargetType=""ToggleButton"">

    <Setter Property=""Foreground"" Value=""{{DynamicResource ButtonForegroundBrush}}"" />
    <Setter Property=""Background"" Value=""{{DynamicResource ButtonBackgroundBrush}}"" />
    <Setter Property=""BorderBrush"" Value=""{{DynamicResource BorderBrush}}"" />
    <Setter Property=""BorderThickness"" Value=""{{DynamicResource ControlBorderThickness}}"" />
    <Setter Property=""FontSize"" Value=""{{DynamicResource BaseFontSize}}"" />
    <Setter Property=""FontFamily"" Value=""{{DynamicResource BaseFontFamily}}"" />
    <Setter Property=""Cursor"" Value=""Hand"" />

    <Setter Property=""Template"">
      <Setter.Value>
        <ControlTemplate TargetType=""ToggleButton"">
          <Border x:Name=""Root""
                  Background=""{{TemplateBinding Background}}""
                  BorderBrush=""{{TemplateBinding BorderBrush}}""
                  BorderThickness=""{{TemplateBinding BorderThickness}}""
                  CornerRadius=""{{DynamicResource ControlCornerRadius}}"">

            <ContentPresenter HorizontalAlignment=""Center""
                              VerticalAlignment=""Center""
                              RecognizesAccessKey=""True"" />
          </Border>

          <ControlTemplate.Triggers>

            <!--  Hover  -->
            <Trigger Property=""IsMouseOver"" Value=""True"">
              <Setter TargetName=""Root"" Property=""Background"" Value=""{{DynamicResource ButtonHoverBrush}}"" />
            </Trigger>

            <!--  Pressed  -->
            <Trigger Property=""IsPressed"" Value=""True"">
              <Setter TargetName=""Root"" Property=""Background"" Value=""{{DynamicResource ButtonPressedBrush}}"" />
            </Trigger>

            <!--  Disabled  -->
            <Trigger Property=""IsEnabled"" Value=""False"">
              <Setter TargetName=""Root"" Property=""Background"" Value=""{{DynamicResource ButtonDisabledBrush}}"" />
              <Setter Property=""Foreground"" Value=""{{DynamicResource ButtonDisabledForegroundBrush}}"" />
              <Setter Property=""Cursor"" Value=""Arrow"" />
            </Trigger>

            <!--  Toggled  -->
            <Trigger Property=""IsChecked"" Value=""True"">
              <Setter TargetName=""Root"" Property=""Background"" Value=""{{DynamicResource ButtonPressedBrush}}"" />
            </Trigger>

          </ControlTemplate.Triggers>
        </ControlTemplate>
      </Setter.Value>
    </Setter>

  </Style>
";

      xaml += $@"</ResourceDictionary>";

      File.WriteAllText( path, xaml );
    }
  }
}
