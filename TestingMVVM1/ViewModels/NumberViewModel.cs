using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingMVVM1.ViewModels
{
    public class NumberViewModel : PropertyChangedBase
    {
        private int _number;
        public int Number
        {
            get => _number;
            set => SetField(ref _number, value);
        }

        public NumberViewModel(int number)
        {
            _number = number;
        }
    }
}
