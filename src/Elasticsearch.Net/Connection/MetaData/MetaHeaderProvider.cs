// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net
{
	/// <summary>
	/// Produces the meta header when this functionality is enabled in the <see cref="ConnectionConfiguration"/>.
	/// </summary>
	internal sealed class MetaHeaderProvider : IHeaderProvider
	{
		private const string MetaHeaderName = "x-elastic-client-meta";

		public string HeaderName => MetaHeaderName;

		public string ProduceHeaderValue(RequestData requestData)
		{
			try
			{
				if (requestData.ConnectionSettings.DisableMetaHeader)
					return null;

				var headerValue = requestData.IsAsync
					? requestData.ConnectionSettings.MetaDataHeaders.AsyncMetaDataHeaderPrefix
					: requestData.ConnectionSettings.MetaDataHeaders.SyncMetaDataHeaderPrefix;

				if (requestData.RequestMetaData.TryGetValue(RequestMetaData.HelperKey, out var helperSuffix))
					headerValue = $"{headerValue},{helperSuffix}";

				return headerValue;
			}
			catch
			{
				// don't fail the application just because we cannot create this optional header
			}

			return string.Empty;
		}
	}
}
