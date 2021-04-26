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

namespace Nest
{
	public class GetUserAccessTokenResponse : ResponseBase
	{
		[DataMember(Name ="access_token")]
		public string AccessToken { get; set; }

		[DataMember(Name ="expires_in")]
		public long ExpiresIn { get; set; }

		[DataMember(Name ="scope")]
		public string Scope { get; set; }

		[DataMember(Name ="type")]
		public string Type { get; set; }

		[DataMember(Name = "authentication")]
		public Authentication Authentication { get; set; }
	}

	public class Authentication
	{
		[DataMember(Name = "email")]
		public string Email { get; internal set; }

		[DataMember(Name = "full_name")]
		public string FullName { get; internal set; }

		[DataMember(Name = "metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; }
			= EmptyReadOnly<string, object>.Dictionary;

		[DataMember(Name = "roles")]
		public IReadOnlyCollection<string> Roles { get; internal set; }
			= EmptyReadOnly<string>.Collection;

		[DataMember(Name = "username")]
		public string Username { get; internal set; }

		[DataMember(Name = "authentication_realm")]
		public AuthenticationRealmInfo AuthenticationRealm { get; internal set; }

		[DataMember(Name = "lookup_realm")]
		public AuthenticationRealmInfo LookupRealm { get; internal set; }

		[DataMember(Name = "authentication_type")]
		public string AuthenticationType { get; internal set; }
	}

	public class AuthenticationRealmInfo
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "type")]
		public string Type { get; internal set; }
	}
}
