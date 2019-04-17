using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class DeleteRollupJobResponse : AcknowledgedResponseBase
	{
		/// <summary>
		/// Checks whether the response returned a valid HTTP status code and that the delete is acknowledged
		/// in one go. See also <see cref="AcknowledgedResponseBase.Acknowledged"/>
		/// </summary>
		public override bool IsValid => base.IsValid && Acknowledged;
	}
}
