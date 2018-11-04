using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(FeaturesJsonConverter))]
	public class Features : IUrlParameter
	{
		private readonly Feature _enumValue;

		internal Features(Feature feature) => _enumValue = feature;

		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => _enumValue.GetStringValue();

		public static implicit operator Features(Feature feature) => new Features(feature);

		public static implicit operator Features(string features) => FromString(features);

		public static Features FromString(string features)
		{
			var parts = features.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			var feature = 0;
			foreach (var p in parts)
			{
				var f = p.ToEnum<Feature>();
				if (f != null) feature |= (int)f;
			}
			return (Feature)feature;
		}
	}
}
