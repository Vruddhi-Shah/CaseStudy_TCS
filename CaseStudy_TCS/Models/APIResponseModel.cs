namespace CaseStudy_TCS.Models
{
    public class APIResponseModel
    {
        public int Code { get; set; }
        public Meta meta { get; set; }
        public List<EmployeeDetails> data { get; set; }
    }

    public class EmployeeDetailById
    {
        public int Code { get; set; }
        public object meta { get; set; }
        public EmployeeDetails data { get; set; }
    }
    public class Meta
    {
        public Pagination pagination { get; set; }

    }

    public class Pagination
    {
        public int Total { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
    public class EmployeeDetails
    {
        public int? id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
    }
}
