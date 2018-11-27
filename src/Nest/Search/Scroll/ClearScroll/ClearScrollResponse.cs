using System.Runtime.Serialization;

namespace Nest
{
	public interface IClearScrollResponse : IResponse { }

	[DataContract]
	public class ClearScrollResponse : ResponseBase, IClearScrollResponse { }
}
