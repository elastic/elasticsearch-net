using System.Collections.Concurrent;
using System.Net;
using System.Net.Security;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using FluentAssertions;
using Nest;
using Nest.Tests.Integration;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Connection.Security
{
	[TestFixture]
	[Ignore("Relies on having a local cluster with shield configured")]
	public class SniffingUsingSSLTests
	{

		private static ConcurrentDictionary<string, bool> _knownPrints = new ConcurrentDictionary<string, bool>();
		private static X509Certificate2 _issuer = new X509Certificate2(@"c:\Data\certificates\ca\certs\cacert.pem", "qwerty");
		private bool IsValidCertificate(X509Certificate certificate, X509Chain chain)
		{
			var privateChain = new X509Chain();
			//do not do this if you are not in charge of your CA.
			//revocation is a real security concern!
			privateChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

			var cert2 = new X509Certificate2(certificate);
			privateChain.ChainPolicy.ExtraStore.Add(_issuer);
			privateChain.Build(cert2);
			
			//Assert our chain has the same number of elements as the certifcate presented by the server
			if (chain.ChainElements.Count != privateChain.ChainElements.Count)
				return false;

			//lets validate the our chain status
			foreach (X509ChainStatus chainStatus in privateChain.ChainStatus)
			{
				//If you are working with custom CA's the only way to get it to be tusted
				//Is to add your CA to the machine trusted store. 
				//Otherwise you'd want to return false from the following statement
				if (chainStatus.Status == X509ChainStatusFlags.UntrustedRoot) continue;
				//if the chain has any error of any sort return false
				if (chainStatus.Status != X509ChainStatusFlags.NoError)
					return false;
			}

			int i = 0;
			var found = false;
			//We are going to walk both chains and make sure the thumbprints lign up
			//while making sure find our CA thumprint in the chain presented by the server
			foreach (var element in chain.ChainElements)
			{
				var c = element.Certificate.Thumbprint;
				if (c == _issuer.Thumbprint)
					found = true;

				var cPrivate = privateChain.ChainElements[i].Certificate.Thumbprint;
				if (c != cPrivate)
					return false;
				i++;
			}
			return found;
		}
		[Test]
		public void NodesDiscoveredDuringSniffShouldBeHttps()
		{

			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) =>
			{
				if (errors == SslPolicyErrors.None)
					return true;

				string certificateHash = certificate.GetCertHashString();
				bool knownThumbprintIsValid = false;
				if (_knownPrints.TryGetValue(certificateHash, out knownThumbprintIsValid))
					return knownThumbprintIsValid;

				var isValid = IsValidCertificate(certificate, chain);
				_knownPrints.AddOrUpdate(certificateHash, isValid, (s, b) => isValid);
				return isValid;

			};
			var uris = new[]
			{
				new Uri("https://localhost:9200")
			};
			var connectionPool = new SniffingConnectionPool(uris, randomizeOnStartup: false);
			var settings = new ConnectionSettings(connectionPool, ElasticsearchConfiguration.DefaultIndex)
				.SniffOnStartup()
				.SetBasicAuthentication("mpdreamz", "blahblah")
				.ExposeRawResponse()
				//.SetPingTimeout(1000)
				.SetTimeout(2000);
			var client = new ElasticClient(settings);

			var results = client.NodesInfo();
			results.IsValid.Should().BeTrue("{0}", results.ConnectionStatus.ToString());
			results.ConnectionStatus.NumberOfRetries.Should().Be(0);
			var uri = new Uri(results.ConnectionStatus.RequestUrl);
			uri.Port.Should().Be(9200);
			uri.Scheme.Should().Be("https");

			results = client.NodesInfo();
			results.IsValid.Should().BeTrue("{0}", results.ConnectionStatus.ToString());
			results.ConnectionStatus.NumberOfRetries.Should().Be(0);
			uri = new Uri(results.ConnectionStatus.RequestUrl);
			uri.Port.Should().Be(9201);
			uri.Scheme.Should().Be("https");
		}


	}
}