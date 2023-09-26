using System;
using System.Collections.Generic;
using System.Text;
using paint1.Models;
using System.Collections.ObjectModel;
using System.Windows;
using paint1.ViewModels;

namespace paint1.ViewModels
{
    public class ToolbarViewModel
    {
        private MainWindowsViewModel _mainvm;
        public Toolbar toolbar { get; set; }
        public ICommandImpl createShape { get; set; }
        public ICommandImpl drawShape { get; set; }
        public ICommandImpl removeShape { get; set; }

        public ToolbarViewModel(MainWindowsViewModel mainvm)
        {
            _mainvm = mainvm;
            toolbar = new Toolbar { isShapeSelected = false, isDrawModeSelected = false };
            createShape = new ICommandImpl(obj => CreateShape((string)obj));
            drawShape = new ICommandImpl(obj => DrawShape());
            removeShape = new ICommandImpl(obj => RemoveShape());
        }

        private void CreateShape(string shapeType)
        {
            _mainvm.canvasVM.CreateShape(shapeType);
        }

        private void DrawShape()
        {
            toolbar.isDrawModeSelected = !toolbar.isDrawModeSelected;
            _mainvm.canvasVM.DrawShape();
            if (_mainvm.selEl == null)
            { toolbar.isShapeSelected = false; }
        }

        private void RemoveShape()
        {
            _mainvm.canvasVM.RemoveShape();
        }
    }
}
