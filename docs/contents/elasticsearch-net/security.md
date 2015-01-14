---
template: layout.jade
title: Connecting
menusection: 
menuitem: esnet-security
---

# Security


## Authorization

NEST and Elasticsearch.NET support basic auth out of the box by setting your credentials on `ConnectionSettings`

```
var settings = new ConnectionSettings()
	.SetBasicAuthentication("mpdreamz", "blahblah")
```

You can override the credentials for individual requests using:

```
var response = client.RootNodeInfo(c => c
	.RequestConfiguration(rc => rc
		.BasicAuthentication("nestuser", "elastic")
	)
);
```

## SSL support

Nest supports SSL throughout out of the box just pass `https` `Uri`'s instead of `http`.

```
var uris = new[]
{
	new Uri("https://localhost:9200")
};
var connectionPool = new SniffingConnectionPool(uris, randomizeOnStartup: false);
var settings = new ConnectionSettings(connectionPool, ElasticsearchConfiguration.DefaultIndex)
	.SniffOnStartup()
	.SetBasicAuthentication("mpdreamz", "blahblah")
	//Initial handshake can be slow NEST defaults to 500ms for SSL
	.SetPingTimeout(1000)
	//request timeout
	.SetTimeout(2000);
var client = new ElasticClient(settings);
```

## Working with untrusted SSL certificates

When working with SSL's chances are you end up with self signed certificates which are not trusted by your machine. 

We recommend installing your custom CA's in the computer's trust store [as outline for instance here](http://blogs.technet.com/b/sbs/archive/2007/04/10/installing-a-self-signed-certificate-as-a-trusted-root-ca-in-windows-vista.aspx)

SSL can however be configured in code outside of the client using .NET's [ServicePointManager](http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager%28v=vs.110%29.aspx
) class and setting the [ServerCertificateValidationCallback](http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.servercertificatevalidationcallback.aspx) property.

The bare minimum to make .NET accept self-signed SSL certs that are not in the Window's CA store would be to have the callback simply return `true`:

    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, errors) => true;

However, this will accept all requests from the AppDomain to untrusted SSL sites, therefore we recommend doing some minimal introspection on the passed in certificate.
You can also write code to forcefully accept SSL's:

### Trusting CA in code example

Given your application has access to your CA public key:

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