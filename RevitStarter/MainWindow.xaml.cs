using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace RevitStarter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.viewModel = new MainViewModel();
            this.DataContext = this.viewModel;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in viewModel.RevitPlugingList)
            {
                if (item.IsSelected)
                {
                    if (item.Path.Contains(Global.CustomSuffix))
                    {
                       var newPath = item.Path.Replace(Global.CustomSuffix, Global.AddinSuffix);
                        File.Move(item.Path, newPath);
                    }
                }
                else
                {
                    if (item.Path.Contains(Global.AddinSuffix))
                    {
                        var newPath = item.Path.Replace(Global.AddinSuffix, Global.CustomSuffix);
                        File.Move(item.Path, newPath);
                    }
                }
            }

            Process.Start(viewModel.SelectedRevitApp.Location);
            //外部程序,启动完成！

            this.Close();
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.viewModel.RevitPlugingList)
            {
                item.IsSelected = true;
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.viewModel.RevitPlugingList)
            {
                item.IsSelected = false;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.RevitPlugingList.Clear();
            Utils.GetAllAddIns(viewModel.SelectedRevitApp.Version).ForEach(o => viewModel.RevitPlugingList.Add(o));

        }
    }
}
