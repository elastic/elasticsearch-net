using System.Runtime.Serialization;

namespace Nest
{
	public abstract class AcknowledgedResponseBase : ResponseBase
	{
		[DataMember(Name = "acknowledged")]
		public bool Acknowledged { get; internal set; }

		/// <summary>
		/// Checks whether the response returned a valid HTTP status code and that the delete is acknowledged
		/// in one go. See also <see cref="AcknowledgedResponseBase.Acknowledged"/>
		/// </summary>
		public override bool IsValid => base.IsValid && Acknowledged;
	}
}
