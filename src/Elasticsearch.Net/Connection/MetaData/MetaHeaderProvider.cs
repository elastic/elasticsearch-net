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

namespace Elasticsearch.Net
{
	/// <summary>
	/// Produces the meta header when this functionality is enabled in the <see cref="ConnectionConfiguration"/>.
	/// </summary>
	public class MetaHeaderProvider
	{
		private const string MetaHeaderName = "x-elastic-client-meta";
		
		private readonly MetaDataHeader _asyncMetaDataHeader;
		private readonly MetaDataHeader _syncMetaDataHeader;

		public MetaHeaderProvider()
		{
			var clientVersionInfo = ClientVersionInfo.Create<IElasticLowLevelClient>();
			_asyncMetaDataHeader = new MetaDataHeader(clientVersionInfo, "es", true);
			_syncMetaDataHeader = new MetaDataHeader(clientVersionInfo, "es", false);
		}
		
		public string HeaderName => MetaHeaderName;

		public string ProduceHeaderValue(RequestData requestData)
		{
			try
			{
				if (requestData.ConnectionSettings.DisableMetaHeader)
					return null;

				var headerValue = requestData.IsAsync
					? _asyncMetaDataHeader.ToString()
					: _syncMetaDataHeader.ToString();

				if (requestData.RequestMetaData.TryGetValue(RequestMetaData.HelperKey, out var helperSuffix))
					headerValue = $"{headerValue},h={helperSuffix}";

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
