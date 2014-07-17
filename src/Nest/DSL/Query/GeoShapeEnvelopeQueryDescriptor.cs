using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.DSL.Query.Behaviour;
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

	public class GeoShapeEnvelopeQuery : PlainQuery, IGeoShapeEnvelopeQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoShape = this;
		}

		bool IQuery.IsConditionless { get { return false; } }

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }

		public IEnvelopeGeoShape Shape { get; set; }
	}

	public class GeoShapeEnvelopeQueryDescriptor<T> : IGeoShapeEnvelopeQuery where T : class
	{
		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		IEnvelopeGeoShape IGeoShapeEnvelopeQuery.Shape { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IGeoShapeQuery)this).Field.IsConditionless() || ((IGeoShapeEnvelopeQuery)this).Shape == null || !((IGeoShapeEnvelopeQuery)this).Shape.Coordinates.HasAny();
			}

		}
		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IGeoShapeQuery)this).Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IGeoShapeQuery)this).Field;
		}
		
		public GeoShapeEnvelopeQueryDescriptor<T> OnField(string field)
		{
			((IGeoShapeQuery)this).Field = field;
			return this;
		}

		public GeoShapeEnvelopeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IGeoShapeQuery)this).Field = objectPath;
			return this;
		}

		public GeoShapeEnvelopeQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (((IGeoShapeEnvelopeQuery)this).Shape == null)
				((IGeoShapeEnvelopeQuery)this).Shape = new EnvelopeGeoShape();
			((IGeoShapeEnvelopeQuery)this).Shape.Coordinates = coordinates;
			return this;
		}
	}
}
