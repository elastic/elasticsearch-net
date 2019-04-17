using System.Runtime.Serialization;

namespace Nest
{
	public class GetTrialLicenseStatusResponse : ResponseBase
	{
		[DataMember(Name ="eligible_to_start_trial")]
		public bool EligibleToStartTrial { get; internal set; }
	}
}
