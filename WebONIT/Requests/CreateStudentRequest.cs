using System.ComponentModel.DataAnnotations;

namespace WebONIT.Requests
{
    public class CreateStudentRequest
    {
        public string FIO { get; set; }
        public string RecordBookNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime AdmissionDate { get; set; }
    }
}
