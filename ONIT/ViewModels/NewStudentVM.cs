using ONIT.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONIT.Model
{
    internal class NewStudentVM : BaseViewModel
    {
        private string _fio;
        private string _recordBookNumber;
        private DateTime _birthDate;
        private DateTime _admissionDate;


        public string FIO
        {
            get { return _fio; }
            set
            {
                _fio = value;
                OnPropertyChanged(nameof(FIO));
            }
        }
        public string RecordBookNumber
        {
            get { return _recordBookNumber; }
            set
            {
                _recordBookNumber = value;
                OnPropertyChanged(nameof(RecordBookNumber));
            }
        }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }
        public DateTime AdmissionDate
        {
            get { return _admissionDate; }
            set
            {
                _admissionDate = value;
                OnPropertyChanged(nameof(AdmissionDate));
            }
        }
    }
}
