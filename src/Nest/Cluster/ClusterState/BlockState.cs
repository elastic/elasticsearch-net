using System.Runtime.Serialization;

namespace Nest
{
	public class BlockState
	{
		[DataMember(Name ="read_only")]
		public bool ReadOnly { get; set; }
	}
}
