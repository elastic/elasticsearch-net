using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.ClientConcepts.Certificates
{
	/**=== Working with certificates
	 *
	 * If you've enabled SSL on Elasticsearch with https://www.elastic.co/products/x-pack[X-Pack] or through a
	 * proxy in front of Elasticsearch, and the Certificate Authority (CA)
	 * that generated the certificate is trusted by the machine running the client code, there should be nothing you'll have to do to talk
	 * to the cluster over HTTPS with the client.
	 *
	 * If you are using your own CA which is not trusted however, .NET won't allow you to make HTTPS calls to that endpoint by default. With .NET,
	 * you can pre-empt this though a custom validation callback on the global static
	 * `ServicePointManager.ServerCertificateValidationCallback`. Most examples you will find doing this this will simply return `true` from the
	 * validation callback and merrily whistle off into the sunset. **This is not advisable** as it allows *any* HTTPS traffic through in the
	 * current `AppDomain` *without* any validation. Here's a concrete example:
	 *
	 */
	public class WorkingWithCertificates
	{
		/** Imagine you deploy a web application that talks to Elasticsearch over HTTPS through NEST, and also uses some third party SOAP/WSDL endpoint;
		* by setting
		*/
		public void ServerValidationCallback()
		{
			ServicePointManager.ServerCertificateValidationCallback +=
				(sender, cert, chain, errors) => true;
		}
		/**
		 * validation will not be performed for HTTPS connections to *both* Elasticsearch *and* that external web service.
		 *
		 * ==== Validation configuration
		 *
		 * It's possible to also set a callback per service endpoint with .NET, and both Elasticsearch.NET and NEST expose this through
		 * connection settings (`ConnectionConfiguration` with Elasticsearch.Net and `ConnectionSettings` with NEST). You can do
		 * your own validation in that handler or use one of the baked in handlers that we ship with out of the box, on the static class
		 * `CertificateValidations`.
		 *
		 * The two most basic ones are `AllowAll` and `DenyAll`, which accept or deny all SSL traffic to our nodes, respectively. Here's
		 * a couple of examples.
		 *
		 * ===== Denying all certificate validation
		 *
		 * Here we set up `ConnectionSettings` with a validation callback that denies all certificate validation
		 */
		public class DenyAllCertificatesCluster : SslAndKpiXPackCluster
		{
			protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
				.ServerCertificateValidationCallback((o, certificate, chain, errors) => false)
				.ServerCertificateValidationCallback(CertificateValidations.DenyAll); // <1> synonymous with the previous lambda expression
		}

		//hide
		[IntegrationOnly]
		public class DenyAllSslCertificatesApiTests : ConnectionErrorTestBase<DenyAllCertificatesCluster>
		{
			public DenyAllSslCertificatesApiTests(DenyAllCertificatesCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

			[I]
			public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));

			protected override void AssertWebException(WebException e) =>
				e.Message.Should().Contain("Could not establish trust relationship for the SSL/TLS secure channel.");

			protected override void AssertHttpRequestException(HttpRequestException e)
			{
			}
		}

		/**===== Allowing all certificate validation
		 *
		 * Here we set up `ConnectionSettings` with a validation callback that allows all certificate validation
		*/
		public class AllowAllCertificatesCluster : SslAndKpiXPackCluster
		{
			protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
				.ServerCertificateValidationCallback((o, certificate, chain, errors) => true)
				.ServerCertificateValidationCallback(CertificateValidations.AllowAll); // <1> synonymous with the previous lambda expression
		}
		/**
		 * This is not recommended in production.
		 */

		//hide
		[IntegrationOnly]
		public class AllowAllSslCertificatesApiTests : CanConnectTestBase<AllowAllCertificatesCluster>
		{
			public AllowAllSslCertificatesApiTests(AllowAllCertificatesCluster cluster, EndpointUsage usage) : base(cluster, usage)
			{
			}

			[I]
			public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));
		}

		/**===== Allowing certificates from a Certificate Authority
		 *
		 * If your client application has access to the public CA certificate locally, Elasticsearch.NET and NEST ship with some handy helpers
		 * that can assert that a certificate the server presents is one that came from the local CA.
		 *
		 * If you use X-Pack's {ref_current}/certutil.html[+elasticsearch-certutil+ tool] to generate SSL certificates, the generated node certificate
		 * does not include the CA in the certificate chain, in order to cut down on SSL handshake size. In those case you can use
		 * `CertificateValidations.AuthorityIsRoot` and pass it your local copy of the CA public key to assert that
		 * the certificate the server presented was generated using it
		 */
		public class CertgenCaCluster : SslAndKpiXPackCluster
		{
            public CertgenCaCluster() : this(new SslAndKpiClusterConfiguration()) { }
            public CertgenCaCluster(SslAndKpiClusterConfiguration configuration) : base(configuration) { }
			protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
				.ServerCertificateValidationCallback(
					CertificateValidations.AuthorityIsRoot(new X509Certificate(this.ClusterConfiguration.FileSystem.CaCertificate))
				);
		}

		//hide
		[IntegrationOnly]
		public class CertgenCaApiTests : CanConnectTestBase<CertgenCaCluster>
		{
			public CertgenCaApiTests(CertgenCaCluster cluster, EndpointUsage usage) : base(cluster, usage)
			{
			}

			[I]
			public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));
		}

		/**
		 * If your local copy does not match the server's CA, the client will fail to connect
		 */
		public class BadCertgenCaCluster : SslAndKpiXPackCluster
		{
			protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
				.ServerCertificateValidationCallback(
					CertificateValidations.AuthorityPartOfChain(new X509Certificate(this.ClusterConfiguration.FileSystem.UnusedCaCertificate))
				);
		}

		[IntegrationOnly]
		public class BadCertgenCaApiTests : ConnectionErrorTestBase<BadCertgenCaCluster>
		{
			public BadCertgenCaApiTests(BadCertgenCaCluster cluster, EndpointUsage usage) : base(cluster, usage)
			{
			}

			// hide
			[I]
			public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));

			protected override void AssertWebException(WebException e) =>
				e.Message.Should().Contain("Could not establish trust relationship for the SSL/TLS secure channel."); // <1> Exception is thrown, indicating that a secure connection could not be established

			// hide
			protected override void AssertHttpRequestException(HttpRequestException e)
			{
			}
		}
		/**
		* If you go for a vendor generated SSL certificate, it's common practice for the certificate to include the CA _and_ any intermediary CAs
		* in the certificate chain. When using such a certificate, use `CertificateValidations.AuthorityPartOfChain` which validates that
		* the local CA certificate is part of the chain that was used to generate the servers key.
		*/

		/**
		 * ==== Client Certificates
		 *
		 * X-Pack also allows you to configure a {xpack_current}/pki-realm.html[PKI realm] to enable user authentication
		 * through client certificates. The {ref_current}/certutil.html[+elasticsearch-certutil+ tool] included with X-Pack allows you to
		 * generate client certificates as well and assign the distinguished name (DN) of the
		 * certificate to a user with a certain role.
		 *
		 * By default, the `elasticsearch-certutil` tool only generates a public certificate (`.cer`) and a private key `.key`. To authenticate with client certificates, you need to present both
		 * as one certificate. The easiest way to do this is to generate a `pfx` or `p12` file from the `.cer` and `.key`
		 * and attach these to requests using `new X509Certificate(pathToPfx)`.
		 *
		 * You can pass a client certificate on `ConnectionSettings` for *all* requests.
		 *
		 */
		public class PkiCluster : CertgenCaCluster
		{
			public PkiCluster() : base(new SslAndKpiClusterConfiguration
			{
				DefaultNodeSettings =
				{
					{"xpack.security.authc.realms.file1.enabled", "false"},
					{"xpack.security.http.ssl.client_authentication", "required"}
				}
			}) { }

			protected override ConnectionSettings Authenticate(ConnectionSettings s) => s // <1> Set the client certificate on `ConnectionSettings`
				.ClientCertificate(new X509Certificate2(this.ClusterConfiguration.FileSystem.ClientCertificate));
		}

		//hide
		[IntegrationOnly]
		[SkipVersion(">2.0.0", "Skipping this tests for now investigating over at Elastic.Managed.Ephemeral why client certs are not generated correctly")]
		public class PkiApiTests : CanConnectTestBase<PkiCluster>
		{
			public PkiApiTests(PkiCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

			[I] public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));
		}
	}

	/**
	 * Or on a per request basis on `RequestConfiguration` which will take precedence over the ones defined on `ConnectionConfiguration`
	 */
	public class BadPkiCluster : WorkingWithCertificates.PkiCluster { }

	[IntegrationOnly]
	public class BadCustomCertificatePerRequestWinsApiTests : ConnectionErrorTestBase<BadPkiCluster>
	{
		public BadCustomCertificatePerRequestWinsApiTests(BadPkiCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		// hide
		[I] public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));

		// a bad certificate
		// hide
		private string Certificate => this.Cluster.ClusterConfiguration.FileSystem.ClientCertificate;

		/**
		 * ==== Object Initializer syntax example */
		protected override RootNodeInfoRequest Initializer => new RootNodeInfoRequest
		{
			RequestConfiguration = new RequestConfiguration
			{
				ClientCertificates = new X509Certificate2Collection { new X509Certificate2(this.Certificate) }
			}
		};

		/**
		 * ==== Fluent DSL example */
		protected override Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> Fluent => s => s
			.RequestConfiguration(r => r
					.ClientCertificate(this.Certificate)
			);

		// hide
		protected override void AssertWebException(WebException e)
		{
			if (e.InnerException != null)
				e.InnerException.Message.Should()
					.Contain("Authentication failed because the remote party has closed the transport stream");
			else
				e.Message.Should().Contain("Could not create SSL/TLS secure channel");
		}

		protected override void AssertHttpRequestException(HttpRequestException e)
		{
		}
	}
}
