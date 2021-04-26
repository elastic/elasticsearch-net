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
	public abstract class NodesResponseBase : ResponseBase
	{
		[DataMember(Name = "_nodes")]
		public NodeStatistics NodeStatistics { get; internal set; }
	}

	[DataContract]
	public class NodeStatistics
	{
		[DataMember(Name = "failed")]
		public int Failed { get; internal set; }

		[DataMember(Name = "successful")]
		public int Successful { get; internal set; }

		[DataMember(Name = "total")]
		public int Total { get; internal set; }

		[DataMember(Name = "failures")]
		public IReadOnlyCollection<ErrorCause> Failures { get; internal set; } = EmptyReadOnly<ErrorCause>.Collection;
	}
}
