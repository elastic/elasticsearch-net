using System.Runtime.Serialization;

namespace Nest
{
	public interface IStartTrialLicenseResponse : IAcknowledgedResponse
	{
		[DataMember(Name ="error_message")]
		string ErrorMessage { get; }

		[DataMember(Name ="trial_was_started")]
		bool TrialWasStarted { get; }
	}

	public class StartTrialLicenseResponse : AcknowledgedResponseBase, IStartTrialLicenseResponse
	{
		public string ErrorMessage { get; internal set; }
		public bool TrialWasStarted { get; internal set; }
	}
}
