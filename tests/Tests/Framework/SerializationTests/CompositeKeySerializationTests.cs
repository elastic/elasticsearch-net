// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.IO;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;

namespace Tests.Framework.SerializationTests
{
	public class CompositeKeySerializationTests
	{
		[U] public void NullValuesAreSerialized()
		{
			var compositeKey = new CompositeKey(new Dictionary<string, object>
			{
				{ "key_1", "value_1" },
				{ "key_2", null },
			});

			var serializer = TestClient.Default.RequestResponseSerializer;
			var json = serializer.SerializeToString(compositeKey, TestClient.Default.ConnectionSettings.MemoryStreamFactory, SerializationFormatting.None);
			json.Should().Be("{\"key_1\":\"value_1\",\"key_2\":null}");

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
			{
				stream.Position = 0;
				var dictionary = serializer.Deserialize<IReadOnlyDictionary<string, object>>(stream);
				var deserializedCompositeKey = new CompositeKey(dictionary);
				compositeKey.Should().Equal(deserializedCompositeKey);
			}
		}
	}
}
