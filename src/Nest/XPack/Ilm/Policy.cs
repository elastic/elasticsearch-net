// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[ReadAs(typeof(Policy))]
	public interface IPolicy
	{
		/// <summary>
		/// Custom meta data to associated with the policy. Not used by Elasticsearch,
		/// but can be used to store application-specific metadata.
		/// </summary>
		[DataMember(Name = "_meta")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, object>))]
		IDictionary<string, object> Meta { get; set; }

		[DataMember(Name = "phases")]
		IPhases Phases { get; set; }
	}

	public class Policy : IPolicy
	{
		/// <inheritdoc />
		public IDictionary<string, object> Meta { get; set; }

		public IPhases Phases { get; set; }
	}

	public class PolicyDescriptor : DescriptorBase<PolicyDescriptor, IPolicy>, IPolicy
	{
		IDictionary<string, object> IPolicy.Meta { get; set; }
		IPhases IPolicy.Phases { get; set; }
		
		/// <inheritdoc cref="IPolicy.Meta" />
		public PolicyDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> metaSelector) =>
			Assign(metaSelector(new FluentDictionary<string, object>()), (a, v) => a.Meta = v);

		/// <inheritdoc cref="IPolicy.Meta" />
		public PolicyDescriptor Meta(Dictionary<string, object> metaDictionary) => Assign(metaDictionary, (a, v) => a.Meta = v);
		
		/// <inheritdoc cref="ITypeMapping.Properties" />
		public PolicyDescriptor Phases(Func<PhasesDescriptor, IPhases> selector) =>
			Assign(selector, (a, v) => a.Phases = v?.InvokeOrDefault(new PhasesDescriptor()));
	}
}
