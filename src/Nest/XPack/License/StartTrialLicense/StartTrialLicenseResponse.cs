using Newtonsoft.Json;

namespace Nest
{
	public interface IStartTrialLicenseResponse : IResponse
	{
		[JsonProperty("trial_was_started")]
		bool TrialWasStarted { get; }

		[JsonProperty("error_message")]
		string ErrorMessage { get; }
	}

	public class StartTrialLicenseResponse : ResponseBase, IStartTrialLicenseResponse
	{
		public bool TrialWasStarted { get; internal set; }

		public string ErrorMessage { get; internal set; }
	}
}
