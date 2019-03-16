using System;
using System.IO;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IGetBasicLicenseStatusResponse : IResponse
	{
		[DataMember(Name = "eligible_to_start_basic")]
		bool EligableToStartBasic { get; }
	}

	public class GetBasicLicenseStatusResponse : ResponseBase, IGetBasicLicenseStatusResponse
	{
		public bool EligableToStartBasic { get; internal set; }
	}

}
