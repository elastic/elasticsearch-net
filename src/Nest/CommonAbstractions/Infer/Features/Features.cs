using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class Features : IUrlParameter
	{
		public string GetString(IConnectionConfigurationValues settings) => KnownEnums.Resolve(_enumValue);

		private readonly Enum _enumValue;

		internal Features(Feature feature) { _enumValue = feature; }

		public static implicit operator Features(Feature feature) => new Features(feature);
	}
}
