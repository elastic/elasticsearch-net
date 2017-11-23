using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// The state of a machine learning job
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum JobState
	{
		/// <summary>
		/// The job close action is in progress and has not yet completed. A closing job cannot accept further data.
		/// </summary>
		[EnumMember(Value = "closing")]
		Closing,
		/// <summary>
		/// The job finished successfully with its model state persisted.
		/// The job must be opened before it can accept further data.
		/// </summary>
		[EnumMember(Value = "closed")]
		Closed,
		/// <summary>
		/// The job is available to receive and process data.
		/// </summary>
		[EnumMember(Value = "opened")]
		Opened,
		/// <summary>
		/// The job did not finish successfully due to an error. This situation can occur due to invalid input data.
		/// If the job had irrevocably failed, it must be force closed and then deleted.
		/// If the datafeed can be corrected, the job can be closed and then re-opened.
		/// </summary>
		[EnumMember(Value = "failed")]
		Failed,
		/// <summary>
		/// The job open action is in progress and has not yet completed.
		/// </summary>
		[EnumMember(Value = "opening")]
		Opening
	}
}
