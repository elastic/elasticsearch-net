using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IAcknowledgedResponse : IResponse
	{
		[DataMember(Name = "acknowledged")]
		bool Acknowledged { get; }
	}

	public abstract class AcknowledgedResponseBase : ResponseBase, IAcknowledgedResponse
	{
		public bool Acknowledged { get; internal set; }
	}
}
