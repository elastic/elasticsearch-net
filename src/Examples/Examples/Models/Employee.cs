using Nest;

namespace Examples.Models
{
	public class Employee
	{
		public int? Age { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		[PropertyName("employee-id")]
		public string EmployeeId { get; set; }
	}
}
