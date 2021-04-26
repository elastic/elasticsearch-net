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
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(IndicesOptions))]
	public interface IIndicesOptions
	{
		[DataMember(Name = "allow_no_indices")]
		bool? AllowNoIndices { get; set; }

		/// <summary>
		/// Determines how to expand indices wildcards.
		/// <para>NOTE: Elasticsearch 7.10.0 and prior supports only a single value. Elasticsearch 7.10.1 and later support multiple values.</para>
		/// </summary>
		[DataMember(Name = "expand_wildcards")]
		[JsonFormatter(typeof(ExpandWildcardsFormatter))]
		IEnumerable<ExpandWildcards> ExpandWildcards { get; set; }

		[DataMember(Name = "ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }
	}

	[DataContract]
	
	public class IndicesOptions : IIndicesOptions
	{
		public bool? AllowNoIndices { get; set; }
		/// <inheritdoc />
		public IEnumerable<ExpandWildcards> ExpandWildcards { get; set; }
		public bool? IgnoreUnavailable { get; set; }
	}

	public class IndicesOptionsDescriptor : DescriptorBase<IndicesOptionsDescriptor, IIndicesOptions>, IIndicesOptions
	{
		bool? IIndicesOptions.AllowNoIndices { get; set; }
		IEnumerable<ExpandWildcards> IIndicesOptions.ExpandWildcards { get; set; }
		bool? IIndicesOptions.IgnoreUnavailable { get; set; }

		/// <inheritdoc cref="IIndicesOptions.ExpandWildcards"/>
		public IndicesOptionsDescriptor ExpandWildcards(IEnumerable<ExpandWildcards> expandWildcards) =>
			Assign(expandWildcards, (a, v) => a.ExpandWildcards = v);

		/// <inheritdoc cref="IIndicesOptions.ExpandWildcards"/>
		public IndicesOptionsDescriptor ExpandWildcards(params ExpandWildcards[] expandWildcards) =>
			Assign(expandWildcards, (a, v) => a.ExpandWildcards = v);

		public IndicesOptionsDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) =>
			Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);

		public IndicesOptionsDescriptor AllowNoIndices(bool? allowNoIndices = true) =>
			Assign(allowNoIndices, (a, v) => a.AllowNoIndices = v);
	}
}
