using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace paint1.Models
{
    public class CanvasModel { }

    public class MyCanvas : INotifyPropertyChanged
    {
        private Canvas _canvas;
        private bool _drawing;
        private Point _startPoint;
        private Polyline _line;

        public Point startPoint
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
                RaisePropertyChanged("startPoint");
            }
        }

        public Polyline line
        {
            get => _line;
            set
            {
                if (value != null)
                {
                    _line = value;
                    RaisePropertyChanged("line");
                }
                else
                {
                    _line = null;
                }
            }
        }

        public bool drawing
        {
            get => _drawing;
            set
            {
                _drawing = value;
                RaisePropertyChanged("drawing");
            }
        }

        public Canvas canvas
        {
            get => _canvas;
            set
            {
                if (_canvas != value)
                {
                    _canvas = value;
                    RaisePropertyChanged("canvas");
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
