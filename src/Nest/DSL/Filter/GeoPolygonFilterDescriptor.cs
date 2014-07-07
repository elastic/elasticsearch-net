using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoPolygonFilter : IFieldNameFilter
	{
		[JsonProperty("points")]
		IEnumerable<string> Points { get; set; }
	}

	public class GeoPolygonFilter : PlainFilter, IGeoPolygonFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoPolygon = this;
		}

		public PropertyPathMarker Field { get; set; }
		public IEnumerable<string> Points { get; set; }
	}

	public class GeoPolygonFilterDescriptor : FilterBase, IGeoPolygonFilter
	{
		bool IFilter.IsConditionless
		{
			get
			{
				var gf = ((IGeoPolygonFilter)this);
				return !gf.Points.HasAny() || gf.Points.All(p => p.IsNullOrEmpty());
			}

		}

		PropertyPathMarker IFieldNameFilter.Field { get; set; }
		IEnumerable<string> IGeoPolygonFilter.Points { get; set; }
	}
}
