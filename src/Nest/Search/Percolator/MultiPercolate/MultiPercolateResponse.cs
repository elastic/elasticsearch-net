using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IMultiPercolateResponse : IResponse
	{
		IEnumerable<PercolateResponse> Responses { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class MultiPercolateResponse : ResponseBase, IMultiPercolateResponse
	{
		public override bool IsValid => base.IsValid && this.Responses.All(r => r.IsValid);

		[JsonProperty("responses")]
		internal IEnumerable<PercolateResponse> AllResponses { get; set; }

		IEnumerable<PercolateResponse> IMultiPercolateResponse.Responses => this.Responses;

		private IEnumerable<PercolateResponse> _allResponses() 
		{
			foreach (var r in this.AllResponses)
			{
				IBodyWithApiCallDetails d = r;
				d.CallDetails = this.ApiCall;
				yield return r;
			}
		}

		public IEnumerable<PercolateResponse> Responses => this._allResponses();

	}
}
