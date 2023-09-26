using paint1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using paint1.ViewModels;

namespace paint1.ViewModels
{
    public class CanvasViewModel
    {
        private MainWindowsViewModel _mainvm;
        public MyCanvas DrawingBoard { get; set; }

        public CanvasViewModel(MainWindowsViewModel mainvm)
        {
            _mainvm = mainvm;
            DrawingBoard = new MyCanvas { canvas = new Canvas(), drawing = false };
            DrawingBoard.canvas.Background = new SolidColorBrush(Colors.WhiteSmoke);
            DrawingBoard.canvas.AllowDrop = true;
            DrawingBoard.canvas.Focusable = true;
            DrawingBoard.canvas.MouseDown += CanvasClicked;
            DrawingBoard.canvas.MouseMove += CanvasMove;
            DrawingBoard.canvas.MouseUp += CanvasRelease;
        }

        public void DrawShape()
        {
            if (_mainvm.selEl != null)
            {
                _mainvm.selEl.Opacity = 1;
                _mainvm.selEl.ReleaseMouseCapture();
                _mainvm.selEl = null;
            }

        }

        private void CanvasMove(object sender, RoutedEventArgs e)
        {
            if (_mainvm.toolbarVM.toolbar.isDrawModeSelected)
            {
                if (DrawingBoard.drawing)
                {
                    Point currentpos = Mouse.GetPosition(DrawingBoard.canvas);
                    if (currentpos != DrawingBoard.startPoint)
                    {
                        DrawingBoard.line.Points.Add(currentpos);
                    }
                }
            }
        }

        private void CanvasRelease(object sender, RoutedEventArgs e)
        {
            if (_mainvm.toolbarVM.toolbar.isDrawModeSelected)
            {
                DrawingBoard.drawing = false;
            }
        }

        private void CanvasClicked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Canvas)
            {
                if (_mainvm.selEl != null)
                {
                    _mainvm.selEl.Opacity = 1;
                    _mainvm.selEl = null;
                    _mainvm.toolbarVM.toolbar.isShapeSelected = false;
                }
            }

            if (_mainvm.toolbarVM.toolbar.isDrawModeSelected)
            {
                DrawingBoard.drawing = true;
                DrawingBoard.startPoint = Mouse.GetPosition(DrawingBoard.canvas);
                DrawingBoard.line = new Polyline();
                DrawingBoard.line.Stroke = new SolidColorBrush(Colors.Black);
                DrawingBoard.line.StrokeThickness = 5;
                DrawingBoard.line.MouseDown += ClickShape;
                DrawingBoard.line.MouseUp += ShapeReleased;
                DrawingBoard.line.MouseMove += ShapeMove;
                DrawingBoard.canvas.Children.Add(DrawingBoard.line);
            }
        }

        private void ShapeMove(object sender, RoutedEventArgs e)
        {
            UIElement el = (UIElement)sender;
            if (!el.IsMouseCaptured) return;
            else
            {
                Point p = Mouse.GetPosition(DrawingBoard.canvas);
                Shape sp = (Shape)e.Source;
                if (e.OriginalSource is Line)
                {
                    Line ln = (Line)sp;
                    ln.X2 = ln.X2 - ln.X1 + p.X;
                    ln.Y2 = ln.Y2 - ln.Y1 + p.Y;
                    ln.X1 = p.X;
                    ln.Y1 = p.Y;
                }
                else
                {
                    double left = p.X - (sp.ActualWidth / 2);
                    double top = p.Y - (sp.ActualHeight / 2);
                    Canvas.SetLeft(sp, left);
                    Canvas.SetTop(sp, top);
                }
            }
        }

        private void ShapeReleased(object sender, RoutedEventArgs e)
        {
            UIElement el = (UIElement)sender;
            el.ReleaseMouseCapture();
        }

        private void ClickShape(object sender, RoutedEventArgs e)
        {
            if (_mainvm.toolbarVM.toolbar.isDrawModeDisabled)
            {
                _mainvm.toolbarVM.toolbar.isShapeSelected = true;
                UIElement el = (UIElement)sender;
                if ((_mainvm.selEl != null) && (el != _mainvm.selEl))
                {
                    _mainvm.selEl.Opacity = 1;
                    _mainvm.selEl.ReleaseMouseCapture();
                    _mainvm.selEl = null;
                }
                _mainvm.selEl = el;
                _mainvm.selEl.CaptureMouse();
                _mainvm.selEl.Opacity = 0.475;
            }
            else
            {
                CanvasClicked(sender, e);
            }
        }

      

        public void CreateShape(string shape)
        {
            if ((_mainvm.selEl != null))
            {
                _mainvm.selEl.Opacity = 1;
                _mainvm.selEl = null;
                _mainvm.toolbarVM.toolbar.isShapeSelected = false;
            }
            Binding bindEnabled = new Binding();
            bindEnabled.Source = _mainvm.toolbarVM;
            bindEnabled.Path = new PropertyPath("toolbar.isDrawModeDisabled");
            bindEnabled.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            Random rand = new Random();
            switch (shape)
            {
                case "Rectangle":
                    Rectangle rect = new Rectangle();
                    rect.Stroke = new SolidColorBrush(Colors.Blue);
                    rect.Fill = new SolidColorBrush(Colors.Blue);
                    rect.Width = 60;
                    rect.Height = 40;
                    Canvas.SetLeft(rect, rand.Next(400));
                    Canvas.SetTop(rect, rand.Next(400));
                    rect.MouseDown += ClickShape;
                    rect.MouseUp += ShapeReleased;
                    rect.MouseMove += ShapeMove;
                    BindingOperations.SetBinding(rect, Shape.IsEnabledProperty, bindEnabled);
                    DrawingBoard.canvas.Children.Add(rect);
                    break;

                case "Ellipse":
                    Ellipse ellps = new Ellipse();
                    ellps.Width = 60;
                    ellps.Height = 40;
                    ellps.Stroke = new SolidColorBrush(Colors.Red);
                    ellps.Fill = new SolidColorBrush(Colors.Red);
                    Canvas.SetLeft(ellps, rand.Next(400));
                    Canvas.SetTop(ellps, rand.Next(400));
                    ellps.MouseDown += ClickShape;
                    ellps.MouseUp += ShapeReleased;
                    ellps.MouseMove += ShapeMove;
                    BindingOperations.SetBinding(ellps, Shape.IsEnabledProperty, bindEnabled);
                    DrawingBoard.canvas.Children.Add(ellps);
                    break;

                case "Line":
                    Line ln = new Line();
                    ln.Fill = new SolidColorBrush(Colors.Green);
                    ln.Stroke = new SolidColorBrush(Colors.Green);
                    ln.StrokeThickness = 5;
                    ln.X1 = rand.Next(300);
                    ln.Y1 = rand.Next(300);
                    ln.X2 = ln.X1 + 75;
                    ln.Y2 = ln.Y1;
                    ln.MouseDown += ClickShape;
                    ln.MouseUp += ShapeReleased;
                    ln.MouseMove += ShapeMove;
                    BindingOperations.SetBinding(ln, Shape.IsEnabledProperty, bindEnabled);
                    DrawingBoard.canvas.Children.Add(ln);
                    break;
                default:
                    break;
            }
        }

        public void RemoveShape()
        {
            if (_mainvm.selEl != null)
            {
                DrawingBoard.canvas.Children.Remove(_mainvm.selEl);
                _mainvm.selEl = null;
                _mainvm.toolbarVM.toolbar.isShapeSelected = false;
            }
        }
    }
}
