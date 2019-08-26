using Nest;

namespace Examples.Models
{
	public class MyDocument
	{
		[PropertyName("my_field")]
		public string MyField { get; set; }
	}
}
