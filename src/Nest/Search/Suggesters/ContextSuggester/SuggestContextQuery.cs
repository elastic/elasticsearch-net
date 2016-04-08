using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SuggestContextQuery>))]
	public interface ISuggestContextQuery
	{
		[JsonProperty("context")]
		Context Context { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("prefix")]
		bool? Prefix { get; set; }

		[JsonProperty("precision")]
		Union<Distance, int> Precision { get; set; }

		[JsonProperty("neighbours")]
		Union<Distance[], int[]> Neighbours { get; set; }
	}

	public class SuggestContextQuery : ISuggestContextQuery
	{
		public double? Boost { get; set; }

		public Context Context { get; set; }

		public Union<Distance[], int[]> Neighbours { get; set; }

		public Union<Distance, int> Precision { get; set; }

		public bool? Prefix { get; set; }
	}

	public class SuggestContextQueryDescriptor<T>
		: DescriptorBase<SuggestContextQueryDescriptor<T>, ISuggestContextQuery>, ISuggestContextQuery
	{
		double? ISuggestContextQuery.Boost { get; set; }
		Context ISuggestContextQuery.Context { get; set; }
		Union<Distance[], int[]> ISuggestContextQuery.Neighbours { get; set; }
		Union<Distance, int> ISuggestContextQuery.Precision { get; set; }
		bool? ISuggestContextQuery.Prefix { get; set; }

		public SuggestContextQueryDescriptor<T> Prefix(bool prefix) => Assign(a => a.Prefix = prefix);

		public SuggestContextQueryDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public SuggestContextQueryDescriptor<T> Context(string context) => Assign(a => a.Context = context);

		public SuggestContextQueryDescriptor<T> Context(GeoLocation context) => Assign(a => a.Context = context);

		public SuggestContextQueryDescriptor<T> Precision(Distance precision) => Assign(a => a.Precision = precision);

		public SuggestContextQueryDescriptor<T> Precision(int precision) => Assign(a => a.Precision = precision);

		public SuggestContextQueryDescriptor<T> Neighbours(params int[] neighbours) => Assign(a => a.Neighbours = neighbours);

		public SuggestContextQueryDescriptor<T> Neighbours(params Distance[] neighbours) => Assign(a => a.Neighbours = neighbours);
	}

	public class SuggestContextQueriesDescriptor<T>
		: DescriptorPromiseBase<SuggestContextQueriesDescriptor<T>, IDictionary<string, IList<ISuggestContextQuery>>>
	{
		public SuggestContextQueriesDescriptor() : base(new Dictionary<string, IList<ISuggestContextQuery>>()) { }

		public SuggestContextQueriesDescriptor<T> Context(string name, params Func<SuggestContextQueryDescriptor<T>, ISuggestContextQuery>[] categoryDescriptors) =>
			AddContextQueries(name, categoryDescriptors?.Select(d => d?.Invoke(new SuggestContextQueryDescriptor<T>())).ToList());

		private SuggestContextQueriesDescriptor<T> AddContextQueries(string name, List<ISuggestContextQuery> contextQueries) =>
			contextQueries == null ? this : this.Assign(a => a.Add(name, contextQueries));
	}
}
