using System.Runtime.Serialization;

namespace Nest
{
	public class SqlColumn
	{
		[DataMember(Name ="name")]
		public string Name { get; set; }

		[DataMember(Name ="type")]
		public string Type { get; set; }
	}
}
