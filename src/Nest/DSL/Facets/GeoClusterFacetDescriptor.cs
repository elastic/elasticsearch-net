using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest.DSL.Facets
{

	[JsonConverter(typeof(CustomJsonConverter))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoClusterFacetRequest : IFacetRequest, ICustomJson
	{

		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "factor")]
		double Factor { get; set; }

	}

	public class GeoClusterFacetRequest : FacetRequest, IGeoClusterFacetRequest
	{
		public PropertyPathMarker Field { get; set; }

		public double Factor { get; set; }

		public static object CustomJson(IGeoClusterFacetRequest geoClusterFacetRequest)
		{
			return new Dictionary<object, object>
			{
				{ "field", geoClusterFacetRequest.Field },
				{ "factor", geoClusterFacetRequest.Factor }
			};
		}
		public object GetCustomJson()
		{
			return CustomJson(this);
		}
	}


	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class GeoClusterFacetDescriptor<T> : BaseFacetDescriptor<GeoClusterFacetDescriptor<T>, T>, IGeoClusterFacetRequest where T : class
	{
		public GeoClusterFacetDescriptor()
		{
			Factor = 0.5;
		}
		protected IGeoClusterFacetRequest Self { get { return this; } }

		public GeoClusterFacetDescriptor<T> OnField(string field)
		{
			field.ThrowIfNullOrEmpty("field");
			Self.Field = field;
			return this;
		}

		public GeoClusterFacetDescriptor<T> OnFactor(double factor)
		{
			Self.Factor = factor;
			return this;
		}

		public GeoClusterFacetDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			objectPath.ThrowIfNull("objectPath");
			Self.Field = objectPath;
			return this;
		}
		PropertyPathMarker IGeoClusterFacetRequest.Field { get; set; }

		public double Factor { get; set; }

		object ICustomJson.GetCustomJson()
		{
			return GeoClusterFacetRequest.CustomJson(Self);
		}
	}
}
