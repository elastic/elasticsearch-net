// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	public class InvalidateApiKeyResponse : ResponseBase
	{
		/// <summary>
		/// The number of errors that were encountered when invalidating the API keys.
		/// </summary>
		[DataMember(Name = "error_count")]
		public int? ErrorCount { get; internal set; }

		/// <summary>
		/// Details about these errors. This field is not present in the response when there are no errors.
		/// </summary>
		[DataMember(Name = "error_details")]
		public IReadOnlyCollection<ErrorCause> ErrorDetails { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;

		/// <summary>
		/// The ids of the API keys that were invalidated as part of this request.
		/// </summary>
		[DataMember(Name = "invalidated_api_keys")]
		public IReadOnlyCollection<string> InvalidatedApiKeys { get; internal set; } = EmptyReadOnly<string>.Collection;

		/// <summary>
		/// The ids of the API keys that were already invalidated.
		/// </summary>
		[DataMember(Name = "previously_invalidated_api_keys")]
		public IReadOnlyCollection<string> PreviouslyInvalidatedApiKeys { get; internal set; } = EmptyReadOnly<string>.Collection;
	}
}
