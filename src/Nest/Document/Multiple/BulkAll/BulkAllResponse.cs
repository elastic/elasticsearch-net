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

namespace Nest
{
	/// <inheritdoc />
	[DataContract]
	public class BulkAllResponse
	{
		/// <summary>This is the Nth buffer.</summary>
		public long Page { get; internal set; }

		/// <summary>The number of back off retries were needed to store this document.</summary>
		public int Retries { get; internal set; }

		/// <summary>The items returned from the bulk response</summary>
		public IReadOnlyCollection<BulkResponseItemBase> Items { get; internal set; } = EmptyReadOnly<BulkResponseItemBase>.Collection;
	}
}
