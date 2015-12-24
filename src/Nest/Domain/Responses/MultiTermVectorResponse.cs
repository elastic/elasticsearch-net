using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiTermVectorResponse : IResponse
	{
		[Obsolete("In 1.x this property does not return metadata for the documents, switch to .Docs or upgrade to 2.0 when its release")]
		IEnumerable<TermVectorResponse> Documents { get; }
		IEnumerable<MultiTermVectorHit> Docs { get; }
	}

	[JsonObject]
	public class MultiTermVectorResponse : BaseResponse, IMultiTermVectorResponse
	{
		public MultiTermVectorResponse()
		{
			IsValid = true;
			Docs = new List<MultiTermVectorHit>();
		}

		[Obsolete("In 1.x this property does not return metadata for the documents, switch to .Docs or upgrade to 2.0 when its release")]
		public IEnumerable<TermVectorResponse> Documents { get { return this.Docs; } }

		[JsonProperty("docs")]
		public IEnumerable<MultiTermVectorHit> Docs { get; internal set; }
	}
}
