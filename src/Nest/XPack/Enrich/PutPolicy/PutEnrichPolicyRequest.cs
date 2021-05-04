// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Creates an enrich policy
	/// </summary>
	[MapsApi("enrich.put_policy")]
	[ReadAs(typeof(PutEnrichPolicyRequest))]
	public partial interface IPutEnrichPolicyRequest
	{
		/// <summary>
		/// Matches enrich data to incoming documents based on a precise value, such as an email address or ID, using a term query.
		/// </summary>
		[DataMember(Name = "match")]
		IEnrichPolicy Match { get; set; }

		/// <summary>
		/// Matches enrich data to incoming documents based on a geographic location using a geo_shape query.
		/// </summary>
		[DataMember(Name = "geo_match")]
		IEnrichPolicy GeoMatch { get; set; }
	}

	/// <inheritdoc cref="IPutEnrichPolicyRequest"/>
	public partial class PutEnrichPolicyRequest
	{
		/// <inheritdoc />
		public IEnrichPolicy Match { get; set; }
		/// <inheritdoc />
		public IEnrichPolicy GeoMatch { get; set; }
	}

	/// <inheritdoc cref="IPutEnrichPolicyRequest"/>
	public partial class PutEnrichPolicyDescriptor<TDocument> where TDocument : class
	{
		IEnrichPolicy IPutEnrichPolicyRequest.GeoMatch { get; set; }
		IEnrichPolicy IPutEnrichPolicyRequest.Match { get; set; }

		public PutEnrichPolicyDescriptor<TDocument> Match(Func<EnrichPolicyDescriptor<TDocument>, IEnrichPolicy> selector) =>
			Assign(selector?.Invoke(new EnrichPolicyDescriptor<TDocument>()), (a, v) => a.Match = v);

		public PutEnrichPolicyDescriptor<TDocument> GeoMatch(Func<EnrichPolicyDescriptor<TDocument>, IEnrichPolicy> selector) =>
			Assign(selector?.Invoke(new EnrichPolicyDescriptor<TDocument>()), (a, v) => a.GeoMatch = v);
	}
}
