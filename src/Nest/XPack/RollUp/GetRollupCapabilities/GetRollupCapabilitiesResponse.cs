using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGetRollupCapabilitiesResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, RollupCapabilities> Indices { get; }
	}

	[JsonConverter(typeof(ResolvableDictionaryResponseJsonConverter<GetRollupCapabilitiesResponse, IndexName, RollupCapabilities>))]
	public class GetRollupCapabilitiesResponse : DictionaryResponseBase<IndexName, RollupCapabilities>, IGetRollupCapabilitiesResponse
	{
		public IReadOnlyDictionary<IndexName, RollupCapabilities> Indices => Self.BackingDictionary;
	}

	public class RollupCapabilities
	{
		[JsonProperty("rollup_jobs")]
		public IReadOnlyCollection<RollupCapabilitiesJob> RollupJobs { get; internal set; }
	}

	public class RollupCapabilitiesJob
	{
		[JsonProperty("fields")]
		public RollupFieldsCapabilitiesDictionary Fields { get; internal set; }

		[JsonProperty("index_pattern")]
		public string IndexPattern { get; internal set; }

		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		[JsonProperty("rollup_index")]
		public string RollupIndex { get; internal set; }
	}

	public class RollupFieldsCapabilities : IsADictionaryBase<string, string> { }

	[JsonConverter(typeof(Converter))]
	public class RollupFieldsCapabilitiesDictionary : ResolvableDictionaryProxy<Field, IReadOnlyCollection<RollupFieldsCapabilities>>
	{
		internal RollupFieldsCapabilitiesDictionary(IConnectionConfigurationValues c,
			IReadOnlyDictionary<Field, IReadOnlyCollection<RollupFieldsCapabilities>> b
		) : base(c, b) { }

		public IReadOnlyCollection<RollupFieldsCapabilities> Field<T>(Expression<Func<T, object>> selector) => this[selector];

		internal class Converter
			: ResolvableDictionaryJsonConverterBase
				<RollupFieldsCapabilitiesDictionary, Field, IReadOnlyCollection<RollupFieldsCapabilities>>
		{
			protected override RollupFieldsCapabilitiesDictionary Create(
				IConnectionSettingsValues s, Dictionary<Field, IReadOnlyCollection<RollupFieldsCapabilities>> d
			) => new RollupFieldsCapabilitiesDictionary(s, d);
		}
	}
}
