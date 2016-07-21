using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	///
	/// </summary>
	public interface IBulkAllResponse
	{
		/// <summary>This is the Nth buffer.</summary>
		long Page { get; }

		/// <summary>The number of back off retries were needed to store this document.</summary>
		int Retries { get; }
	}

	/// <summary>
	/// POCO representing the reindex response for a each step
	/// </summary>
	[JsonObject]
	public class BulkAllResponse : IBulkAllResponse
	{
		/// <inheritdoc />
		public long Page { get; internal set; }

		/// <inheritdoc />
		public int Retries { get; internal set; }

		/// <inheritdoc />
		public bool IsValid => true;
	}
}
