namespace HSOHomeTest.Models
{
    public class Patient
    {
        public string? PatientName { get; set; }
        public string? SSN { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Status>? Status { get; set; }
    }
}
