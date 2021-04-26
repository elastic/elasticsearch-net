/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;

namespace Nest
{
	public class InvalidateUserAccessTokenResponse : ResponseBase
	{
		/// <summary>
		/// The number of the tokens that were invalidated as part of this request.
		/// </summary>
		[DataMember(Name = "invalidated_tokens")]
		public long InvalidatedTokens { get; internal set;  }

		/// <summary>
		/// The number of tokens that were already invalidated.
		/// </summary>
		[DataMember(Name = "previously_invalidated_tokens")]
		public long PreviouslyInvalidatedTokens { get; internal set;  }

		/// <summary>
		/// The number of errors that were encountered when invalidating the tokens.
		/// </summary>
		[DataMember(Name = "error_count")]
		public long ErrorCount { get; internal set;  }

		/// <summary>
		/// Details about these errors. This field is not present in the response when there are no errors.
		/// </summary>
		[DataMember(Name = "error_details")]
		public IReadOnlyCollection<ErrorCause> ErrorDetails { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;
	}
}
