// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net
{
	/// <summary>
	/// Represents the status of the pre-flight product checks.
	/// </summary>
	public enum ProductCheckStatus
	{
		NotChecked,
		ValidProduct,
		InvalidProduct,
		UndeterminedProduct,
		TransientFailure,
		UnsupportedBuildFlavor
	}
}
