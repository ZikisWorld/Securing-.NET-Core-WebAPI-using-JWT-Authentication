using System.ComponentModel.DataAnnotations;

namespace WebApiDempApp.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }    
        public decimal Salary { get; set; }
    }
}
