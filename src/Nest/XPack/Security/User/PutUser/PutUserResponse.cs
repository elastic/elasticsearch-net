using System.Runtime.Serialization;

namespace Nest
{
	public class PutUserResponse : ResponseBase
	{
		[DataMember(Name ="created")]
		public bool Created { get; internal set; }
	}
}
