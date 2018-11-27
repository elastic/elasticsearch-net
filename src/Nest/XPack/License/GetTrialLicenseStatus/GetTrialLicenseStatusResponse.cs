using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetTrialLicenseStatusResponse : IResponse
	{
		[DataMember(Name ="eligible_to_start_trial")]
		bool EligibleToStartTrial { get; }
	}

	public class GetTrialLicenseStatusResponse : ResponseBase, IGetTrialLicenseStatusResponse
	{
		public bool EligibleToStartTrial { get; internal set; }
	}
}
