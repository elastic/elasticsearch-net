// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CategoryDefinition
	{
		/// <summary>
		/// A unique identifier for the category.
		/// </summary>
		[DataMember(Name ="category_id")]
		public long CategoryId { get; internal set; }

		/// <summary>
		/// A list of examples of actual values that matched the category.
		/// </summary>
		[DataMember(Name ="examples")]
		public IReadOnlyCollection<string> Examples { get; internal set; } = EmptyReadOnly<string>.Collection;

		/// <summary>
		/// The unique identifier for the job that these results belong to.
		/// </summary>
		[DataMember(Name ="job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// The maximum length of the fields that matched the category. The value is increased by 10% to enable matching for similar fields that have
		/// not been analyzed.
		/// </summary>
		[DataMember(Name ="max_matching_length")]
		public long MaxMatchingLength { get; internal set; }

		/// <summary>
		/// A regular expression that is used to search for values that match the category.
		/// </summary>
		[DataMember(Name ="regex")]
		public string Regex { get; internal set; }

		/// <summary>
		/// A space separated list of the common tokens that are matched in values of the category.
		/// </summary>
		[DataMember(Name ="terms")]
		public string Terms { get; internal set; }
	}
}
