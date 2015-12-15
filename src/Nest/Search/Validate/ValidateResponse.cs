using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IValidateResponse : IResponse
	{
		bool Valid { get; }
		ShardsMetaData Shards { get; }
		IList<ValidationExplanation> Explanations { get; set; }
	}

	[JsonObject]
	public class ValidateResponse : BaseResponse, IValidateResponse
	{
		[JsonProperty(PropertyName = "valid")]
		public bool Valid { get; internal set; }

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		/// <summary>
		/// Gets the explanations if Explain() was set.
		/// </summary>
		[JsonProperty(PropertyName = "explanations")]
		public IList<ValidationExplanation> Explanations { get; set;}
	}
}