using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IDeleteRollupJobResponse : IAcknowledgedResponse { }

	public class DeleteRollupJobResponse : AcknowledgedResponseBase, IDeleteRollupJobResponse
	{
		/// <summary>
		/// Checks whether the response returned a valid HTTP status code and that the delete is acknowledged
		/// in one go. See also <see cref="AcknowledgedResponseBase.Acknowledged"/>
		/// </summary>
		public override bool IsValid => base.IsValid && Acknowledged;
	}
}
