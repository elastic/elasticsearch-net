// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class SampleDiversity
	{
		[DataMember(Name ="field")]
		public Field Field { get; set; }

		[DataMember(Name ="max_docs_per_value")]
		public int? MaxDocumentsPerValue { get; set; }
	}
}
