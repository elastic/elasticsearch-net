// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Transport;
using Moq;
using Playground;

var client = new ElasticsearchClient();

var resp = client.Search<Person>();

var adj = resp.Aggregations!.GetAdjacencyMatrix("test");
var firstAdj = adj!.Buckets.First();
var firstAdjSub = firstAdj.GetBoxplot("sub");

var mis = resp.Aggregations!.GetMissing("test");
var misSub = mis!.GetBoxplot("sub");

