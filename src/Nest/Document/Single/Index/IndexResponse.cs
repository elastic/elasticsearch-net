using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class IndexResponse : WriteResponseBase
	{
		public override bool IsValid => base.IsValid && 
			(Result == Result.Created 
			|| Result == Result.Updated 
			|| Result == Result.Noop) 
		;
	}
}
