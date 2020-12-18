// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elasticsearch.Net
{
	internal sealed class MetaDataHeaders : IMetaDataHeaders
	{
		public MetaDataHeaders(ClientVersionInfo clientVersionInfo)
		{
			AsyncMetaDataHeader = new MetaDataHeader(clientVersionInfo, "es", true);
			SyncMetaDataHeader = new MetaDataHeader(clientVersionInfo, "es", false);
		}

		public MetaDataHeader AsyncMetaDataHeader { get; private set; }

		public MetaDataHeader SyncMetaDataHeader { get; private set; }

		public string AsyncMetaDataHeaderPrefix => AsyncMetaDataHeader.ToString();

		public string SyncMetaDataHeaderPrefix => SyncMetaDataHeader.ToString();
	}
}
