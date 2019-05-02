using System.Runtime.Serialization;

namespace Nest
{
	public class StartTrialLicenseResponse : AcknowledgedResponseBase
	{
		[DataMember(Name ="error_message")]
		public string ErrorMessage { get; internal set; }

		[DataMember(Name ="trial_was_started")]
		public bool TrialWasStarted { get; internal set; }
	}
}
