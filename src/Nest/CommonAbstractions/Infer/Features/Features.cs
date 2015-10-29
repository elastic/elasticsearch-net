using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(FeaturesJsonConverter))]
	public class Features : IUrlParameter
	{
		string IUrlParameter.GetString(IConnectionConfigurationValues settings) => KnownEnums.Resolve(_enumValue);

		private readonly Enum _enumValue;

		internal Features(Feature feature) { _enumValue = feature; }

		public static implicit operator Features(Feature feature) => new Features(feature);
		public static implicit operator Features(string features) => FromString(features);
		
		public static Features FromString(string features)
		{
			var parts = features.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			int feature = 0;
			foreach (var p in parts)
			{
				var f = p.ToEnum<Feature>();
				if (f != null) feature |= (int)f;
			}
			return (Feature)feature;
		}
	}
}
