using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace paint1.Models
{
    public class ToolbarModel { }

    public class Toolbar : INotifyPropertyChanged
    {
        private bool _isShapeSelected;
        private bool _isDrawModeSelected;

        public bool isDrawModeDisabled
        {
            get => !_isDrawModeSelected;
        }

        public bool isDrawModeSelected
        {
            get => _isDrawModeSelected;
            set
            {
                if (_isDrawModeSelected != value)
                {
                    _isDrawModeSelected = value;
                    RaisePropertyChanged("isDrawModeSelected");
                    RaisePropertyChanged("isDrawModeDisabled");
                    RaisePropertyChanged("visible");
                }
            }
        }

        public bool isShapeSelected
        {
            get => _isShapeSelected;
            set
            {
                if (_isShapeSelected != value)
                {
                    _isShapeSelected = value;
                    RaisePropertyChanged("isShapeSelected");
                    RaisePropertyChanged("isDrawModeDisabled");
                    RaisePropertyChanged("visible");
                }
            }
        }

        public Visibility visible
        {
            get
            {
                if (_isShapeSelected && !_isDrawModeSelected)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
