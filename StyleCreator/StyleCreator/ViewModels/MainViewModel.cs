using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleCreator.ViewModels
{
  public class MainViewModel
  {
    // Bindings for Layout
    private int _controlsColumnA = 1;
    public int ControlsColumnA
    {
      get { return _controlsColumnA; }
    }
    private int _controlsColumnB = 2;
    public int ControlsColumnB
    {
      get { return _controlsColumnB; }
    }


    public MainViewModel()
    {

    }
  }
}
