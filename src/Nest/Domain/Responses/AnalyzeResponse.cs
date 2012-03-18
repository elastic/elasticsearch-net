using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;


namespace Nest
{
	[JsonObject]
	public class AnalyzeResponse : BaseResponse
	{
		public AnalyzeResponse()
		{
			this.IsValid = true;
		}
		[JsonProperty(PropertyName = "tokens")]
		public IEnumerable<AnalyzeToken> Tokens { get; internal set; }
	}
}