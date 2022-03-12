// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;

namespace Tests.ClientConcepts.Connection
{
	public class ConnectingToElasticsearchV8
	{
		/**[[connecting-to-elasticsearch-v8]]
		 * === Connecting to Elasticsearch v8.x using the v7.x client
		 *
		 * We recommend using the latest client with a corresponding major version when connecting to Elasticsearch. Until the v7 .NET client is 
		 * generally available, you may use the v7.17.x client to communicate with a 8.x Elasticsearch cluster. There are several important considerations 
		 * regarding configuration.
		 *
		 * :security: {ref_current}/modules-http.html
		 * :security-clients: {ref_current}/modules-http.html#_connect_clients_to_elasticsearch_5
		 *
		 * ==== Security and Certificates
		 * Newly installed Elasticsearch v8 clusters start with security configuration {security}[enabled automatically by default]. As a result, 
		 * a certificate authority and certificate is created for secure HTTPS communication. Additionally, an `elastic` user is created with a
		 * unique, secure password. Elasticsearch logs details of the security configuration when it first starts, enabling the collection of a
		 * certificate fingerprint, along with the password configured for the `elastic` user. In a development environment, you will need to collect
		 * these pieces of information, required to configure the client to securely communicate with the server. The 
		 * {security-clients}[Elasticsearch documentation] provides commands which may also be used to retrieve this information after the cluster has started.
		 * 
		 * [[ca-fingerprint]]
		 * ===== Applying the CA Fingerprint
		 *
		 * The simplest configuration option during development is to connect to the server using the CA fingerprint logged by the server at initial startup. 
		 * The fingerprint can be set by calling the `CertificateFingerprint` method on a ConnectionSettings instance.
		 */
		[U] public void CertificateFingerprint()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));

			var settings = new ConnectionSettings(pool)
				.CertificateFingerprint("94:75:CE:4F:EB:05:32:83:40:B8:18:BB:79:01:7B:E0:F0:B6:C3:01:57:DB:4D:F5:D8:B8:A6:BA:BD:6D:C5:C4");

			var client = new ElasticClient(settings);
		}

		/**
		 * If preferred, you may also configure the client to work with the certificate in the usual way. 
		 * See <<working-with-certificates, Working with certificates>> for further details.

		 * [[basic-authentication]]
		 * ===== Basic Authentication
		 * TODO

		 * [[enabling-compatibility mode]]
		 * ==== Enabling Compatibility Mode
		 * TODO
		 */
	}
}
