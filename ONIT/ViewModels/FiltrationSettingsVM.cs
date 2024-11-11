using ONIT.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONIT.Model
{
    public class FiltrationSettingsVM : BaseViewModel
    {
        private DateTime? _admissionDateAfter;
        private DateTime? _admissionDateBefore;
        private int? _older;
        private int? _younger;


        public DateTime? AdmissionDateAfter
        {
            get { return _admissionDateAfter;}
            set 
            { 
                _admissionDateAfter = value; 
                OnPropertyChanged(nameof(AdmissionDateAfter));
            }
        }
        public DateTime? AdmissionDateBefore
        {
            get { return _admissionDateBefore; }
            set
            {
                _admissionDateBefore = value;
                OnPropertyChanged(nameof(AdmissionDateBefore));
            }
        }
        public int? Older
        {
            get { return _older; }
            set
            {
                _older = value;
                OnPropertyChanged(nameof(Older));
            }
        }
        public int? Younger
        {
            get { return _younger; }
            set
            {
                _younger = value;
                OnPropertyChanged(nameof(Younger));
            }
        }
    }
}
