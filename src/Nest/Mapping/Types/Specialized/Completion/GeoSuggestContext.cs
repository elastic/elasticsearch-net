using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IGeoSuggestContext : ISuggestContext
	{
		[DataMember(Name = "precision")]
		Union<Distance, int> Precision { get; set; }
	}

	[DataContract]
	public class GeoSuggestContext : SuggestContextBase, IGeoSuggestContext
	{
		public Union<Distance, int> Precision { get; set; }
		public override string Type => "geo";
	}

	[DataContract]
	public class GeoSuggestContextDescriptor<T>
		: SuggestContextDescriptorBase<GeoSuggestContextDescriptor<T>, IGeoSuggestContext, T>, IGeoSuggestContext
		where T : class
	{
		protected override string Type => "geo";

		Union<Distance, int> IGeoSuggestContext.Precision { get; set; }

		public GeoSuggestContextDescriptor<T> Precision(Distance precision) => Assign(precision, (a, v) => a.Precision = v);

		public GeoSuggestContextDescriptor<T> Precision(int precision) => Assign(precision, (a, v) => a.Precision = v);
	}
}
