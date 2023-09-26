using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using paint1.ViewModels;

namespace paint1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowsViewModel mainvm;
        public MainWindow()
        {
            InitializeComponent();
            mainvm = new MainWindowsViewModel();
        }

        private void ToolbarViewControlLoaded(object sender, RoutedEventArgs e)
        {

            ToolbarViewControl.DataContext = mainvm.toolbarVM;
        }

        private void CanvasViewControlLoaded(object sender, RoutedEventArgs e)
        {
            CanvasViewControl.DataContext = mainvm.canvasVM;
        }

    }
}
