using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CreateResponse : WriteResponseBase
	{
		public override bool IsValid => base.IsValid && Result == Result.Created;
	}
}
