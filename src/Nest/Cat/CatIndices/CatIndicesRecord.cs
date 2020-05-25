// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatIndicesRecord : ICatRecord
	{
		[DataMember(Name = "docs.count")]
		public string DocsCount { get; set; }

		[DataMember(Name = "docs.deleted")]
		public string DocsDeleted { get; set; }

		[DataMember(Name = "health")]
		public string Health { get; set; }

		[DataMember(Name = "index")]
		public string Index { get; set; }

		[DataMember(Name = "uuid")]
		public string UUID { get; set; }

		[DataMember(Name = "pri")]
		public string Primary { get; set; }

		[DataMember(Name = "pri.store.size")]
		public string PrimaryStoreSize { get; set; }

		[DataMember(Name = "rep")]
		public string Replica { get; set; }

		[DataMember(Name = "status")]
		public string Status { get; set; }

		[DataMember(Name = "store.size")]
		public string StoreSize { get; set; }

		[DataMember(Name = "tm")]
		public string TotalMemory { get; set; }
	}
}
