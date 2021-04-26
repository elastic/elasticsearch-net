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
using System.IO;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public class GetLicenseResponse : ResponseBase
	{
		public override bool IsValid => base.IsValid && (!License?.UID.IsNullOrEmpty() ?? false);

		[DataMember(Name ="license")]
		public LicenseInformation License { get; internal set; }
	}

	public class LicenseInformation
	{
		[DataMember(Name ="expiry_date")]
		public DateTime ExpiryDate { get; internal set; }

		[DataMember(Name ="expiry_date_in_millis")]
		public long ExpiryDateInMilliseconds { get; internal set; }

		[DataMember(Name ="issue_date")]
		public DateTime IssueDate { get; internal set; }

		[DataMember(Name ="issue_date_in_millis")]
		public long IssueDateInMilliseconds { get; internal set; }

		[DataMember(Name ="issued_to")]
		public string IssuedTo { get; internal set; }

		[DataMember(Name ="issuer")]
		public string Issuer { get; internal set; }

		[DataMember(Name ="max_nodes")]
		public long MaxNodes { get; internal set; }

		[DataMember(Name = "max_resource_units")]
		public int? MaxResourceUnits { get; internal set; }

		[DataMember(Name ="status")]
		public LicenseStatus Status { get; internal set; }

		[DataMember(Name ="type")]
		public LicenseType Type { get; internal set; }

		[DataMember(Name ="uid")]
		public string UID { get; internal set; }
	}

	public class License
	{
		[DataMember(Name ="expiry_date_in_millis")]
		public long ExpiryDateInMilliseconds { get; set; }

		[DataMember(Name ="issue_date_in_millis")]
		public long IssueDateInMilliseconds { get; set; }

		[DataMember(Name ="issued_to")]
		public string IssuedTo { get; set; }

		[DataMember(Name ="issuer")]
		public string Issuer { get; set; }

		[DataMember(Name ="max_nodes")]
		public long MaxNodes { get; set; }

		[DataMember(Name ="signature")]
		public string Signature { get; set; }

		[DataMember(Name ="type")]
		public LicenseType Type { get; set; }

		[DataMember(Name ="uid")]
		public string UID { get; set; }

		public static License LoadFromDisk(string path)
		{
			var contents = File.ReadAllText(path);
			var license = JsonSerializer.Deserialize<Wrapped>(contents)?.License;
			return license;
		}

		private class Wrapped
		{
			[DataMember(Name ="license")]
			public License License { get; set; }
		}
	}
}
