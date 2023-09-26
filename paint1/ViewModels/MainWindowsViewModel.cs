using paint1.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace paint1.ViewModels
{
    public class MainWindowsViewModel
    {
        private UIElement _selEl;
        public CanvasViewModel canvasVM { get; set; }
        public ToolbarViewModel toolbarVM { get; set; }
        public UIElement selEl
        {
            get
            {
                return _selEl;
            }
            set
            {
                if (value != null)
                {
                    _selEl = value;
                }
                else
                {
                    _selEl = null;
                }
            }
        }

        public MainWindowsViewModel()
        {
            canvasVM = new CanvasViewModel(this);
            toolbarVM = new ToolbarViewModel(this);
        }
    }
}
