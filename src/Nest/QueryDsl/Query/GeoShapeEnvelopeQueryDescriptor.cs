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

		public string Name { get; set; }
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
		private IGeoShapeEnvelopeQuery Self { get { return this; } }

		PropertyPathMarker IGeoShapeQuery.Field { get; set; }
		
		IEnvelopeGeoShape IGeoShapeEnvelopeQuery.Shape { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Field.IsConditionless() || Self.Shape == null || !Self.Shape.Coordinates.HasAny();
			}
		}

		string IQuery.Name { get; set; }

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return Self.Field;
		}

		public GeoShapeEnvelopeQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		
		public GeoShapeEnvelopeQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoShapeEnvelopeQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			Self.Field = objectPath;
			return this;
		}

		public GeoShapeEnvelopeQueryDescriptor<T> Coordinates(IEnumerable<IEnumerable<double>> coordinates)
		{
			if (Self.Shape == null)
				Self.Shape = new EnvelopeGeoShape();
			Self.Shape.Coordinates = coordinates;
			return this;
		}
	}
}
