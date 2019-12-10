using System.Runtime.Serialization;
using Nest;

namespace Examples.Models
{
	public class MyDocument
	{
		[DataMember(Name = "my_field")]
		public string MyField { get; set; }
	}
}
