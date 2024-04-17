// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

var settings = new ElasticsearchClientSettings(new Uri("https://primary.es.europe-west3.gcp.cloud.es.io"))
	.Authentication(new BasicAuthentication("elastic", "Oov35Wtxj5DzpZNzYAzFb0KZ"))
	.DisableDirectStreaming()
	.EnableDebugMode(cd =>
	{
		//var request = System.Text.Encoding.Default.GetString(cd.RequestBodyInBytes);
		Console.WriteLine(cd.DebugInformation);
	});

var client = new ElasticsearchClient(settings);
