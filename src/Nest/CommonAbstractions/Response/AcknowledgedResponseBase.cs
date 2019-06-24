using System.Runtime.Serialization;

namespace Nest
{
	public abstract class AcknowledgedResponseBase : ResponseBase
	{
		[DataMember(Name = "acknowledged")]
		public bool Acknowledged { get; internal set; }

		public override bool IsValid => base.IsValid && Acknowledged;
	}
}
