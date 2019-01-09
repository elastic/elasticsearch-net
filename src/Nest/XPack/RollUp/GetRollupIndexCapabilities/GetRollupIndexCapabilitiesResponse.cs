using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetRollupIndexCapabilitiesResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, RollupIndexCapabilities> Indices { get; }
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetRollupIndexCapabilitiesResponse, IndexName, RollupIndexCapabilities>))]
	public class GetRollupIndexCapabilitiesResponse : DictionaryResponseBase<IndexName, RollupIndexCapabilities>, IGetRollupIndexCapabilitiesResponse
	{
		public IReadOnlyDictionary<IndexName, RollupIndexCapabilities> Indices => Self.BackingDictionary;
	}

	public class RollupIndexCapabilities
	{
		[JsonProperty("rollup_jobs")]
		public IReadOnlyCollection<RollupIndexCapabilitiesJob> RollupJobs { get; internal set; }
	}

	public class RollupIndexCapabilitiesJob
	{
		[JsonProperty("fields")]
		public RollupFieldsIndexCapabilitiesDictionary Fields { get; internal set; }

		[JsonProperty("index_pattern")]
		public string IndexPattern { get; internal set; }

		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		[JsonProperty("rollup_index")]
		public string RollupIndex { get; internal set; }
	}

	public class RollupFieldsIndexCapabilities : IsADictionaryBase<string, string> { }

	[JsonConverter(typeof(Converter))]
	public class RollupFieldsIndexCapabilitiesDictionary : ResolvableDictionaryProxy<Field, IReadOnlyCollection<RollupFieldsIndexCapabilities>>
	{
		internal RollupFieldsIndexCapabilitiesDictionary(IConnectionConfigurationValues c,
			IReadOnlyDictionary<Field, IReadOnlyCollection<RollupFieldsIndexCapabilities>> b
		) : base(c, b) { }

		public IReadOnlyCollection<RollupFieldsIndexCapabilities> Field<T>(Expression<Func<T, object>> selector) => this[selector];

		internal class Converter
			: ResolvableDictionaryJsonConverterBase
				<RollupFieldsIndexCapabilitiesDictionary, Field, IReadOnlyCollection<RollupFieldsIndexCapabilities>>
		{
			protected override RollupFieldsIndexCapabilitiesDictionary Create(
				IConnectionSettingsValues s, Dictionary<Field, IReadOnlyCollection<RollupFieldsIndexCapabilities>> d
			) => new RollupFieldsIndexCapabilitiesDictionary(s, d);
		}
	}
}
