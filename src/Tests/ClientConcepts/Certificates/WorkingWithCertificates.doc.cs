using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.ClientConcepts.Certificates
{
	/**== Working with certificates
	 *
	 * === Server Certificates
	 *
	 * If you've enabled SSL on elasticsearch with x-pack or through a proxy in front of elasticsearch and the Certificate Authority (CA)
	 * That generated the certificate is trusted by the machine running the client code there should be nothing you'll have to do to to talk
	 * to over https with the client. If you are using your own CA which is not trusted .NET won't allow you to make https calls to that endpoint.
	 *
	 * .NET allows you to preempt this though through a custom validation through the the global static `ServicePointManager.ServerCertificateValidationCallback`.
	 * Most examples you will find on the .NET will simply return `true` from this delegate and call it quits. This is not advisable as this will allow any HTTPS
	 * traffic in the current AppDomain and not run any validations. Imagine you deploy a web app that talks to Elasticsearch over HTTPS but also some third party
	 * SOAP/WSDL endpoint setting `ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;` will skip validation of BOTH
	 * Elasticsearch and that external web service.

	 * .NET also allows you to set that callback per service endpoint and Elasticsearch.NET/NEST exposes this through connection settings.
	 * You can do your own validation in that handler or simply assign baked in handler that we ship with out of the box on the static
	 * class `CertificateValidations`.
	 *
	 * The two most basic ones are `AllowAll` and `DenyAll` which does accept or deny any ssl trafic to our nodes`:
	 *
	 */
	public class DenyAllCertificatesCluster : SslAndKpiXPackCluster
	{
		protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback((o, certificate, chain, errors) => false)
			.ServerCertificateValidationCallback(CertificateValidations.DenyAll);
	}
	//hide
	public class DenyAllSslCertificatesApiTests : ConnectionErrorTestBase<DenyAllCertificatesCluster>
	{
		public DenyAllSslCertificatesApiTests(DenyAllCertificatesCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		[I] public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));

		protected override void AssertException(WebException e) =>
			e.Message.Should().Contain("Could not establish trust relationship for the SSL/TLS secure channel.");

		protected override void AssertException(HttpRequestException e) { }
	}

	public class AllowAllCertificatesCluster : SslAndKpiXPackCluster
	{
		protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback((o, certificate, chain, errors) => true)
			.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
	}
	//hide
	public class AllowAllSllCertificatesApiTests : CanConnectTestBase<AllowAllCertificatesCluster>
	{
		public AllowAllSllCertificatesApiTests(AllowAllCertificatesCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		[I] public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));
	}

	/**
	 * If your client application however has access to the public CA certificate locally Elasticsearch.NET/NEST ships with handy helpers that assert
	 * that the certificate that the server presented was one that came from our local CA certificate. If you use x-pack's `certgen` tool to
	 * [generate SSL certificates](https://www.elastic.co/guide/en/x-pack/current/ssl-tls.html) the generated node certificate does not include the CA in the
	 * certificate chain. This to cut back on SSL handshake size. In those case you can use `CertificateValidations.AuthorityIsRoot` and pass it your local copy
	 * of the CA public key to assert that the certificate the server presented was generated off that.
	 */

	public class CertgenCaCluster : SslAndKpiXPackCluster
	{
		protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback(
				CertificateValidations.AuthorityIsRoot(new X509Certificate(this.Node.FileSystem.CaCertificate))
			);
	}

	//hide
	public class CertgenCaApiTests : CanConnectTestBase<CertgenCaCluster>
	{
		public CertgenCaApiTests(CertgenCaCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		[I] public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));
	}

	/**
	 * If your local copy does not match the servers CA Elasticsearch.NET/NEST will fail to connect
	 */
	public class BadCertgenCaCluster : SslAndKpiXPackCluster
	{
		protected override ConnectionSettings ConnectionSettings(ConnectionSettings s) => s
			.ServerCertificateValidationCallback(
				CertificateValidations.AuthorityPartOfChain(new X509Certificate(this.Node.FileSystem.UnusedCaCertificate))
			);
	}

	//hide
	public class BadCertgenCaApiTests : ConnectionErrorTestBase<BadCertgenCaCluster>
	{
		public BadCertgenCaApiTests(BadCertgenCaCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		[I] public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));

		protected override void AssertException(WebException e) =>
			e.Message.Should().Contain("Could not establish trust relationship for the SSL/TLS secure channel.");

		protected override void AssertException(HttpRequestException e) { }

	}
	/**
	 * If you go for a vendor generated SSL certificate its common practice for them to include the CA and any intermediary CA's in the certificate chain
	 * in those case use `CertificateValidations.AuthorityPartOfChain` which validates that the local CA certificate is part of that chain and was used to
	 * generate the servers key.
	 */

#if !DOTNETCORE
	/**
	 * === Client Certificates
	 * X-Pack also allows you to configure a [PKI realm](https://www.elastic.co/guide/en/x-pack/current/pki-realm.html) to enable user authentication
	 * through client certificates. The `certgen` tool included with X-Pack allows you to
	 * [generate client certificates as well](https://www.elastic.co/guide/en/x-pack/current/ssl-tls.html#CO13-4) and assign the distinguished name (DN) of the
	 * certificate as a user with a certain role.
	 *
	 * certgen by default only generates a public certificate (`.cer`) and a private key `.key`. To authenticate with client certificates you need to present both
	 * as one certificate. The easiest way to do this is to generate a `pfx` or `p12` file from the two and present that to `new X509Certificate(pathToPfx)`.

	 * If you do not have a way to run `openssl` or `Pvk2Pfx` to do so as part of your deployments the clients ships with a handy helper to generate one
	 * on the fly in code based of `.cer`  and `.key` files that `certgen` outputs. Sadly this is not available on .NET core because we can no longer set `PublicKey`
	 * crypto service provider.

	 * You can set Client Certificates to use on all connections on `ConnectionSettings`

	 */
	public class PkiCluster : CertgenCaCluster
	{
		public override ConnectionSettings Authenticate(ConnectionSettings s) => s
			//.ClientCertificate(this.Node.FileSystem.ClientCertificate);
			.ClientCertificate(
				ClientCertificate.LoadWithPrivateKey(this.Node.FileSystem.ClientCertificate, this.Node.FileSystem.ClientPrivateKey, "")
			);

		protected override string[] AdditionalServerSettings => base.AdditionalServerSettings.Concat(new []
		{
			"xpack.security.authc.realms.file1.enabled=false",
			"xpack.security.http.ssl.client_authentication=required"
		}).ToArray();
	}
	//hide
	public class PkiApiTests : CanConnectTestBase<PkiCluster>
	{
		public PkiApiTests(PkiCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		[I] public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));
	}

	/**
	 * Or per request on `RequestConfiguration` which will take precedence over the ones defined on `ConnectionConfiguration`
	 */
	public class BadPkiCluster : PkiCluster {}
	public class BadCustomCertificatePerRequestWinsApiTests : ConnectionErrorTestBase<BadPkiCluster>
	{
		public BadCustomCertificatePerRequestWinsApiTests(BadPkiCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		[I] public async Task UsedHttps() => await AssertOnAllResponses(r => r.ApiCall.Uri.Scheme.Should().Be("https"));

		private string BadCertificate => this.Cluster.Node.FileSystem.ClientCertificate;

		protected override RootNodeInfoRequest Initializer => new RootNodeInfoRequest
		{
			RequestConfiguration = new RequestConfiguration
			{
				ClientCertificates = new X509Certificate2Collection { new X509Certificate2(this.BadCertificate) }
			}
		};

		protected override Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> Fluent => s => s
			.RequestConfiguration(r => r
				.ClientCertificate(this.BadCertificate)

			);

		protected override void AssertException(WebException e) =>
			e.Message.Should().Contain("Could not create SSL/TLS secure channel");

		protected override void AssertException(HttpRequestException e) { }

	}
#endif

}
