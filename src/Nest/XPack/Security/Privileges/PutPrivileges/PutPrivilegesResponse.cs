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
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(DictionaryResponseFormatter<PutPrivilegesResponse, string, IDictionary<string, PutPrivilegesStatus>>))]
	public class PutPrivilegesResponse : DictionaryResponseBase<string, IDictionary<string, PutPrivilegesStatus>>
	{
		[IgnoreDataMember]
		public IReadOnlyDictionary<string, IDictionary<string, PutPrivilegesStatus>> Applications => Self.BackingDictionary;
	}

	public class PutPrivilegesStatus
	{
		/// <summary>
		/// Whether the privilege has been created or updated.
		/// When an existing privilege is updated, created is set to false.
		/// </summary>
		[DataMember(Name = "created")]
		public bool Created { get; internal set; }
	}
}
