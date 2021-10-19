// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace Tests
{
	public abstract class SerialisationTestBase<TResponse>
	{
		private readonly Serializer _serializer = new DefaultHighLevelSerializer(new ElasticsearchClientSettings());

		protected abstract string ResponseJson { get; }

		protected abstract void Validate(TResponse response);

		[U]
		public void ValidateResponse()
		{
			var ms = new MemoryStream(ResponseJson.Utf8Bytes());
			var response = _serializer.Deserialize<TResponse>(ms);
			Validate(response);
		}
	}
}
