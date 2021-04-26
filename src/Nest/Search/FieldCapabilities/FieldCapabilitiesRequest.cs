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

using System;
using System.Runtime.Serialization;
using Elastic.Transport;

namespace Nest
{
	[MapsApi("field_caps.json")]
	public partial interface IFieldCapabilitiesRequest
	{
		[DataMember(Name = "index_filter")]
		QueryContainer IndexFilter { get; set; }
	}

	public partial class FieldCapabilitiesRequest
	{
		protected override HttpMethod? DynamicHttpMethod => IndexFilter != null? HttpMethod.POST : HttpMethod.GET;

		public QueryContainer IndexFilter { get; set; }
	}

	public partial class FieldCapabilitiesDescriptor
	{
		QueryContainer IFieldCapabilitiesRequest.IndexFilter { get; set; }

		protected override HttpMethod? DynamicHttpMethod => Self.IndexFilter != null? HttpMethod.POST : HttpMethod.GET;

		public FieldCapabilitiesDescriptor IndexFilter<T>(Func<QueryContainerDescriptor<T>, QueryContainer> query) where T : class =>
			Assign(query, (a, v) => a.IndexFilter = v?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
