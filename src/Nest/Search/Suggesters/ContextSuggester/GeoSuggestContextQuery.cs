using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoSuggestContextQuery : ISuggestContextQuery
	{
		[JsonProperty("precision")]
		Union<Distance, int> Precision { get; set; }

		[JsonProperty("neighbours")]
		Union<Distance[], int[]> Neighbours { get; set; }
	}

	public class GeoSuggestContextQuery : SuggestContextQueryBase, IGeoSuggestContextQuery
	{
		public Union<Distance[], int[]> Neighbours { get; set; }

		public Union<Distance, int> Precision { get; set; }
	}

	public class GeoSuggestContextQueryDescriptor<T>
		: SuggestContextQueryDescriptorBase<GeoSuggestContextQueryDescriptor<T>, IGeoSuggestContextQuery, T>, IGeoSuggestContextQuery
	{
		Union<Distance[], int[]> IGeoSuggestContextQuery.Neighbours { get; set; }

		Union<Distance, int> IGeoSuggestContextQuery.Precision { get; set; }

		public GeoSuggestContextQueryDescriptor<T> Precision(Distance precision) => Assign(a => a.Precision = precision);

		public GeoSuggestContextQueryDescriptor<T> Precision(int precision) => Assign(a => a.Precision = precision);

		public GeoSuggestContextQueryDescriptor<T> Neighbours(params int[] neighbours) => Assign(a => a.Neighbours = neighbours);

		public GeoSuggestContextQueryDescriptor<T> Neighbours(params Distance[] neighbours) => Assign(a => a.Neighbours = neighbours);
	}
}
