using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGetRollupCapabilitiesResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, RollupCapabilities> Indices { get; }
	}

	[JsonFormatter(typeof(ResolvableDictionaryResponseFormatter<GetRollupCapabilitiesResponse, IndexName, RollupCapabilities>))]
	public class GetRollupCapabilitiesResponse : DictionaryResponseBase<IndexName, RollupCapabilities>, IGetRollupCapabilitiesResponse
	{
		public IReadOnlyDictionary<IndexName, RollupCapabilities> Indices => Self.BackingDictionary;
	}

	public class RollupCapabilities
	{
		[DataMember(Name = "rollup_jobs")]
		public IReadOnlyCollection<RollupCapabilitiesJob> RollupJobs { get; internal set; }
	}

	public class RollupCapabilitiesJob
	{
		[DataMember(Name = "fields")]
		public RollupFieldsCapabilitiesDictionary Fields { get; internal set; }

		[DataMember(Name = "index_pattern")]
		public string IndexPattern { get; internal set; }

		[DataMember(Name = "job_id")]
		public string JobId { get; internal set; }

		[DataMember(Name = "rollup_index")]
		public string RollupIndex { get; internal set; }
	}

	public class RollupFieldsCapabilities : IsADictionaryBase<string, object> { }

	[JsonFormatter(typeof(Converter))]
	public class RollupFieldsCapabilitiesDictionary : ResolvableDictionaryProxy<Field, IReadOnlyCollection<RollupFieldsCapabilities>>
	{
		internal RollupFieldsCapabilitiesDictionary(IConnectionConfigurationValues c,
			IReadOnlyDictionary<Field, IReadOnlyCollection<RollupFieldsCapabilities>> b
		) : base(c, b) { }

		public IReadOnlyCollection<RollupFieldsCapabilities> Field<T>(Expression<Func<T, object>> selector) => this[selector];

		internal class Converter
			: ResolvableDictionaryFormatterBase
				<RollupFieldsCapabilitiesDictionary, Field, IReadOnlyCollection<RollupFieldsCapabilities>>
		{
			protected override RollupFieldsCapabilitiesDictionary Create(
				IConnectionSettingsValues s, Dictionary<Field, IReadOnlyCollection<RollupFieldsCapabilities>> d
			) => new RollupFieldsCapabilitiesDictionary(s, d);
		}
	}
}
