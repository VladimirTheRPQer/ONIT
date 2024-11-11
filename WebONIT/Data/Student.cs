using System.ComponentModel.DataAnnotations;

namespace WebONIT.Data
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        [StringLength(6)]
        public string RecordBookNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public DateTime AdmissionDate { get; set; }
    }
}
