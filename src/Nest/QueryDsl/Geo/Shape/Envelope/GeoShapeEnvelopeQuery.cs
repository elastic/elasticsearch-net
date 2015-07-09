using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoShapeEnvelopeQuery : IGeoShapeQuery
	{
		[JsonProperty("shape")]
		IEnvelopeGeoShape Shape { get; set; }
	}

	public class GeoShapeEnvelopeQuery : FieldNameQuery, IGeoShapeEnvelopeQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnvelopeGeoShape Shape { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoShape = this;
		internal static bool IsConditionless(IGeoShapeEnvelopeQuery q) => q.Field.IsConditionless() || q.Shape == null || !q.Shape.Coordinates.HasAny();
	}

	public class GeoShapeEnvelopeQueryDescriptor<T> 
		: FieldNameQueryDescriptor<GeoShapeEnvelopeQueryDescriptor<T>, IGeoShapeEnvelopeQuery, T>
		, IGeoShapeEnvelopeQuery where T : class
	{
		private IGeoShapeEnvelopeQuery Self => this;
		bool IQuery.Conditionless => GeoShapeEnvelopeQuery.IsConditionless(this);
		IEnvelopeGeoShape IGeoShapeEnvelopeQuery.Shape { get; set; }

		public GeoShapeEnvelopeQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new EnvelopeGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}
	}
}
