using System.Runtime.Serialization;

namespace Nest
{
	public interface IStartTrialLicenseResponse : IResponse
	{
		[DataMember(Name ="error_message")]
		string ErrorMessage { get; }

		[DataMember(Name ="trial_was_started")]
		bool TrialWasStarted { get; }
	}

	public class StartTrialLicenseResponse : ResponseBase, IStartTrialLicenseResponse
	{
		public string ErrorMessage { get; internal set; }
		public bool TrialWasStarted { get; internal set; }
	}
}
