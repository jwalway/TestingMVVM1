using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace TestingMVVM1.ViewModels
{

    public class MainViewModel : PropertyChangedBase
    {
        private ObservableCollection<NumberViewModel> _numbers = new();
        public ObservableCollection<NumberViewModel> Numbers
        {
            get => _numbers;
            set => SetField(ref _numbers, value);
        }



    }
}