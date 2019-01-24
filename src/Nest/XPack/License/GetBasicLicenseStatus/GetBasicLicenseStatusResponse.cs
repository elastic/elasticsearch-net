using System;
using System.IO;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetBasicLicenseStatusResponse : IResponse
	{
		[JsonProperty("eligible_to_start_basic")]
		bool EligableToStartBasic { get; }
	}

	public class GetBasicLicenseStatusResponse : ResponseBase, IGetBasicLicenseStatusResponse
	{
		public bool EligableToStartBasic { get; internal set; }
	}

}
