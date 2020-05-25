// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// The status of categorization for the job.
	/// </summary>
	[StringEnum]
	public enum ModelCategorizationStatus
	{
		/// <summary>
		/// Categorization is performing acceptably well (or not being used at all).
		/// </summary>
		[EnumMember(Value = "ok")]
		OK,

		/// <summary>
		/// Categorization is detecting a distribution of categories that suggests the input data is inappropriate for categorization.
		/// Problems could be that there is only one category, more than 90% of categories are rare, the number of categories is greater
		/// than 50% of the number of categorized documents, there are no frequently matched categories, or more than 50% of categories
		/// are dead.
		/// </summary>
		[EnumMember(Value = "warn")]
		Warn
	}
}
