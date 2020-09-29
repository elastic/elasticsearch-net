// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(CatFielddataRecordFormatter))]
	public class CatFielddataRecord : ICatRecord
	{
		public string Field { get; set; }
		public string Host { get; set; }
		public string Id { get; set; }
		public string Ip { get; set; }
		public string Node { get; set; }
		public string Size { get; set; }
	}
}
