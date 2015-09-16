using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IClusterSettingsRequest : IRequest<ClusterSettingsRequestParameters>
	{
		[JsonProperty(PropertyName = "persistent")]
		IDictionary<string, object> Persistent { get; set; }

		[JsonProperty(PropertyName = "transient")]
		IDictionary<string, object> Transient { get; set; }

	}
	
	public partial class ClusterSettingsRequest : RequestBase<ClusterSettingsRequestParameters>, IClusterSettingsRequest
	{
		public IDictionary<string, object> Persistent { get; set; }

		public IDictionary<string, object> Transient { get; set; }
	}

	[DescriptorFor("ClusterPutSettings")]
	public partial class ClusterSettingsDescriptor : 
		RequestDescriptorBase<ClusterSettingsDescriptor, ClusterSettingsRequestParameters>
		, IClusterSettingsRequest
	{
		protected IClusterSettingsRequest Self => this;

		IDictionary<string, object> IClusterSettingsRequest.Persistent { get; set; }

		IDictionary<string, object> IClusterSettingsRequest.Transient { get; set; }

		public ClusterSettingsDescriptor Persistent(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			selector.ThrowIfNull("selector");
			Self.Persistent = selector(new FluentDictionary<string, object>());
			return this;
		}

		public ClusterSettingsDescriptor Transient(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			selector.ThrowIfNull("selector");
			Self.Transient = selector(new FluentDictionary<string, object>());
			return this;
		}
	}
}
