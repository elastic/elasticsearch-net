using System.Runtime.Serialization;

namespace Examples.Models
{
	public class Account
	{
		[DataMember(Name ="account_number")]
		public string AccountNumber { get; set; }

		public string Address { get; set; }
		public int? Age { get; set; }

		public double? Balance { get; set; }
		public string Gender { get; set; }
		public string State { get; set; }
		public string User { get; set; }

		[DataMember(Name = "tag")]
		public string[] Tags { get; set; }
	}
}
