using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IGeoSuggestContext : ISuggestContext
	{
		[DataMember(Name = "neighbors")]
		bool? Neighbors { get; set; }

		[DataMember(Name = "precision")]
		IEnumerable<string> Precision { get; set; }
	}

	[DataContract]
	public class GeoSuggestContext : SuggestContextBase, IGeoSuggestContext
	{
		public bool? Neighbors { get; set; }

		public IEnumerable<string> Precision { get; set; }
		public override string Type => "geo";
	}

	[DataContract]
	public class GeoSuggestContextDescriptor<T>
		: SuggestContextDescriptorBase<GeoSuggestContextDescriptor<T>, IGeoSuggestContext, T>, IGeoSuggestContext
		where T : class
	{
		protected override string Type => "geo";
		bool? IGeoSuggestContext.Neighbors { get; set; }
		IEnumerable<string> IGeoSuggestContext.Precision { get; set; }

		public GeoSuggestContextDescriptor<T> Precision(params string[] precisions) => Assign(a => a.Precision = precisions);

		public GeoSuggestContextDescriptor<T> Neighbors(bool? neighbors = true) => Assign(a => a.Neighbors = neighbors);
	}
}
