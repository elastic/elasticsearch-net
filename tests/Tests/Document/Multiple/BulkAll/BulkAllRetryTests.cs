// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Document.Multiple.BulkAll;

public class BulkAllRetryTests
{
    [U]
    public void RetriedButFailedDocuments_InvokeDroppedDocumentsCallback()
    {
        var response = @"{""took"":30,""errors"":true,""items"":[{""index"":{""_index"":""thing"",""_id"":""1"",""status"":429}}]}";

        var responseBytes = Encoding.UTF8.GetBytes(response);
        var connection = new InMemoryConnection(responseBytes, 200);
        var connectionPool = new SingleNodePool(new Uri("http://localhost:9200"));
        var settings = new ElasticsearchClientSettings(connectionPool, connection).DefaultIndex("thing");
        var client = new ElasticsearchClient(settings);

        var docs = new Thing[] { new Thing { Id = 100 } };
        var dropped = new List<Thing>();

        var bulkAll = client.BulkAll(docs, b => b
            .BackOffRetries(2)
            .BackOffTime("1ms")
            .ContinueAfterDroppedDocuments()
            .DroppedDocumentCallback((r, d) => { dropped.Add(d); }));

        try
        {
            bulkAll.Wait(TimeSpan.FromMinutes(1), r => { });
        }
        catch
        {
        }

        dropped.Should().HaveCount(1);
        dropped[0].Id.Should().Be(100);
    }

    private class Thing
    {
        public int Id { get; set; }
    }
}
