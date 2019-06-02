using System.Runtime.Serialization;

namespace Nest
{

	public class GetBasicLicenseStatusResponse : ResponseBase
	{
		[DataMember(Name = "eligible_to_start_basic")]
		public bool EligableToStartBasic { get; internal set; }
	}

}
