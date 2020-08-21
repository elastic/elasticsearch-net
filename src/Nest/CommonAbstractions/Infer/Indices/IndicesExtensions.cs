// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public static class IndicesExtensions
	{
		public static string Resolve(this Indices marker, IConnectionSettingsValues connectionSettings)
		{
			if (marker == null) return null;

			connectionSettings.ThrowIfNull(nameof(connectionSettings));
			return connectionSettings.Inferrer.Resolve(marker);
		}
	}
}
