using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Shared.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public interface IIndexSettingsResponse : IResponse
	{
		IndexSettings IndexSettings { get; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(IndexSettingsResponseConverter))]
	public class IndexSettingsResponse : BaseResponse, IIndexSettingsResponse
	{
		[JsonIgnore]
		public IndexSettings IndexSettings
		{
			get { return Nodes.HasAny() ? Nodes.First().Value : null; }
		}

		[JsonIgnore]
		public Dictionary<string, IndexSettings> Nodes { get; set; }

	}
}
