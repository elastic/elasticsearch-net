using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Open a machine learning job.
	/// </summary>
	public partial interface IOpenJobRequest
	{
		/// <summary>
		/// Controls the time to wait until a job has opened. The default value is 30 minutes.
		/// </summary>
		[JsonProperty("timeout")]
		Time Timeout { get; set; }
	}

	/// <inheritdoc />
	public partial class OpenJobRequest
	{
		/// <inheritdoc />
		public Time Timeout { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlOpenJob")]
	public partial class OpenJobDescriptor
	{
		Time IOpenJobRequest.Timeout { get; set; }

		/// <inheritdoc />
		public OpenJobDescriptor Timeout(Time timeout) => Assign(a => a.Timeout = timeout);
	}
}
