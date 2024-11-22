using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using ONIT.Model;
using ONIT.Util;
using WebONIT.Data;
using WebONIT.Requests;



namespace ONIT.ViewModels
{
    internal class MainWindowVM : BaseViewModel
    {
        public MainWindowVM() 
        {
            NewStudent = new NewStudentVM();
            FiltrationSettings = new FiltrationSettingsVM();
        }
        public NewStudentVM NewStudent { get; set; }
        public FiltrationSettingsVM FiltrationSettings { get; set; }

        private List<Student> _data;
        private Student _selectedStudent;

        private string api = "http://93.183.80.153:8080/api/Students/"; 
        public List<Student> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value; 
                OnPropertyChanged(nameof(SelectedStudent));
            }
        }

        private ICommand addDataCommand;
        private ICommand deleteTupleCommand;
        private ICommand resetFilterCommand;
        private ICommand getDataCommand;


        public async void AddData()
        {
            CreateStudentRequest createStudentRequest = new CreateStudentRequest()
            {
                FIO = NewStudent.FIO,
                RecordBookNumber = NewStudent.RecordBookNumber,
                AdmissionDate = NewStudent.AdmissionDate,
                BirthDate = NewStudent.BirthDate,
            };

            var json = JsonConvert.SerializeObject(createStudentRequest);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PostAsync(api + $"create", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error getting students: " + response.ReasonPhrase);
            GetData();
        }
        public async void DeleteTuple()
        {
            if (SelectedStudent == null) 
                throw new Exception("Студент не выбран");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(api + $"delete/" + SelectedStudent.Id );
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error getting students: " + response.ReasonPhrase);
            GetData();

        }
        public async void GetData()
        {
            Filter filter = new Filter()
            {
                AdmissionDateAfter = FiltrationSettings.AdmissionDateAfter,
                AdmissionDateBefore = FiltrationSettings.AdmissionDateBefore,
                Older = FiltrationSettings.Older,
                Younger = FiltrationSettings.Younger,
            };

            var json = JsonConvert.SerializeObject(filter);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PostAsync(api + $"read", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error getting students: " + response.ReasonPhrase);

            Data = await response.Content.ReadAsAsync<List<Student>>();

        }
        public void ResetFilter()
        {
            FiltrationSettings.AdmissionDateAfter = null;
            FiltrationSettings.AdmissionDateBefore = null;
            FiltrationSettings.Older = null;
            FiltrationSettings.Younger = null;
            GetData();
        }
        public ICommand AddDataCommand
        {
            get
            {
                return addDataCommand ??= new RelayCommand(t => true, (obj) =>
                {
                    try
                    {
                        AddData();

                    }
                    catch { }
                });
            }
        }
        public ICommand DeleteTupleCommand
        {
            get
            {
                return deleteTupleCommand ??= new RelayCommand(t => true, (obj) => 
                {
                    try
                    {
                        DeleteTuple();
                       
                    }
                    catch { }
                });
            }
        }
        public ICommand ResetFilterCommand
        {
            get
            {
                return resetFilterCommand ??= new RelayCommand(t => true, (obj) =>
                {
                    try
                    {
                        ResetFilter();
                    } 
                    catch { }

                    });
            }
        }
        public ICommand GetDataCommand
        {
            get
            {
                return getDataCommand ??= new RelayCommand(t => true, (obj) => {
                    try
                    {
                        GetData();
                    }
                    catch { }
                    });
            }
        }


    }
}
