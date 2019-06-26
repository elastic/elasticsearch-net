using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class DeleteResponse : WriteResponseBase
	{
		public override bool IsValid => base.IsValid && Result == Result.Deleted;
	}
}
