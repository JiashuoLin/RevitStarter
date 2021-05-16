using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RevitStarter
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            this.RevitAppList = new ObservableCollection<RevitAppInfo>();
            Utils.GetAllRevitAppInfo().ForEach(o => this.RevitAppList.Add(o));
            this.SelectedRevitApp = this.RevitAppList.FirstOrDefault();
            this.RevitPlugingList = new ObservableCollection<RevitPlugingInfo>(); 
            Utils.GetAllAddIns(this.SelectedRevitApp.Version).ForEach(o => this.RevitPlugingList.Add(o)); 
        }

        public ObservableCollection<RevitPlugingInfo> RevitPlugingList { get; set; }

        public ObservableCollection<RevitAppInfo> RevitAppList { get; set; }

        private RevitAppInfo _selectedRevitApp;
        public RevitAppInfo SelectedRevitApp
        {
            get
            {
                return _selectedRevitApp;
            }
            set
            {
                _selectedRevitApp = value;
                OnPropertyChanged();
            }
        } 
    }
}
