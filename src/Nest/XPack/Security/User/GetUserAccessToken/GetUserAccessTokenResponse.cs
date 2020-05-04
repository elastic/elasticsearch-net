// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

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
	}
}
