using Newtonsoft.Json;

namespace Nest
{
	public interface IGetTrialLicenseStatusResponse : IResponse
	{
		[JsonProperty("eligible_to_start_trial")]
		bool EligibleToStartTrial { get; }
	}

	public class GetTrialLicenseStatusResponse : ResponseBase, IGetTrialLicenseStatusResponse
	{
		public bool EligibleToStartTrial { get; internal set; }
	}
}
