using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	[JsonObject(MemberSerialization.OptIn)]
	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
	public interface IMultiPercolateResponse : IResponse
	{
		IEnumerable<PercolateResponse> Responses { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[Obsolete("Deprecated. Will be removed in the next major release. Use a percolate query with multi search api")]
	public class MultiPercolateResponse : ResponseBase, IMultiPercolateResponse
	{
		public override bool IsValid => base.IsValid && this.Responses.All(r => r.IsValid);

		protected override void DebugIsValid(StringBuilder sb)
		{
			sb.AppendLine($"# Invalid percolations (inspect individual response.DebugInformation for more detail):");
			foreach(var i in AllResponses.Select((item, i) => new { item, i}).Where(i=>!i.item.IsValid))
				sb.AppendLine($"  search[{i.i}]: {i.item}");
		}

		[JsonProperty("responses")]
		internal IReadOnlyCollection<PercolateResponse> AllResponses { get; set; } = EmptyReadOnly<PercolateResponse>.Collection;

		IEnumerable<PercolateResponse> IMultiPercolateResponse.Responses => this.Responses;

		private IEnumerable<PercolateResponse> _allResponses()
		{
			foreach (var r in this.AllResponses)
			{
				IBodyWithApiCallDetails d = r;
				d.ApiCall = this.ApiCall;
				yield return r;
			}
		}

		public IEnumerable<PercolateResponse> Responses => this._allResponses();

	}
}
