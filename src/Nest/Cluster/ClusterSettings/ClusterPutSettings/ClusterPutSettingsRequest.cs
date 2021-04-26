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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("cluster.put_settings.json")]
	public partial interface IClusterPutSettingsRequest
	{
		[DataMember(Name ="persistent")]
		IDictionary<string, object> Persistent { get; set; }

		[DataMember(Name ="transient")]
		IDictionary<string, object> Transient { get; set; }
	}

	public partial class ClusterPutSettingsRequest
	{
		public IDictionary<string, object> Persistent { get; set; }

		public IDictionary<string, object> Transient { get; set; }
	}

	public partial class ClusterPutSettingsDescriptor
	{
		IDictionary<string, object> IClusterPutSettingsRequest.Persistent { get; set; }

		IDictionary<string, object> IClusterPutSettingsRequest.Transient { get; set; }

		public ClusterPutSettingsDescriptor Persistent(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Persistent = v?.Invoke(new FluentDictionary<string, object>()));

		public ClusterPutSettingsDescriptor Transient(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Transient = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
