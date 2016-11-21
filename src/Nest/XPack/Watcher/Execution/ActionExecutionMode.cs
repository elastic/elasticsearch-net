using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ActionExecutionMode
	{
		/// <summary>
		///The action will be simulated (not actually executed)
		/// and it will be throttled if needed.
		/// </summary>
		[EnumMember(Value = "simulate")]
		Simulate,

		/// <summary>
		/// The action will be simulated (not actually executed) and it will
		/// not be throttled.
		/// </summary>
		[EnumMember(Value = "force_simulate")]
		ForceSimulate,

		/// <summary>
		/// The action will be executed and it will be throttled if needed.
		/// </summary>
		[EnumMember(Value = "execute")]
		Execute,

		/// <summary>
		/// The action will be executed and it will not be throttled.
		/// </summary>
		[EnumMember(Value = "force_execute")]
		ForceExecute,

		/// <summary>
		/// The action will be skipped (it won't be executed nor simulated)
		/// - effectively it will be forcefully throttled
		/// </summary>
		[EnumMember(Value = "skip")]
		Skip
	}
}
