using FluentAssertions;
using Nest;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Security.BasicAuthTests
{
	[TestFixture]
	[Ignore]
	public class BasicAuthorizationTests
	{
		[Test]
		public void No_Credentials_Result_In_401()
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
			var client = new ElasticClient(settings);
			var response = client.RootNodeInfo();
			response.IsValid.Should().BeFalse();
			response.ConnectionStatus.HttpStatusCode.Should().Be(401);
		}

		[Test]
		public void Invalid_Credentials_Result_In_401()
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
				.SetBasicAuthorization("nestuser", "incorrectpassword");
			var client = new ElasticClient(settings);
			var response = client.RootNodeInfo();
			response.IsValid.Should().BeFalse();
			response.ConnectionStatus.HttpStatusCode.Should().Be(401);
		}

		[Test]
		public void Valid_Credentials_Result_In_200()
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
				.SetBasicAuthorization("nestuser", "elastic");
			var client = new ElasticClient(settings);
			var response = client.RootNodeInfo();
			response.IsValid.Should().BeTrue();
		}

		[Test]
		public void Credentials_On_URI_Result_In_200()
		{
			var settings = new ConnectionSettings(new Uri("http://nestuser:elastic@localhost:9200"));
			var client = new ElasticClient(settings);
			var response = client.RootNodeInfo();
			response.IsValid.Should().BeTrue();
		}

		[Test]
		public void Escaped_Credentials_On_URI_Result_In_200()
		{
			var settings = new ConnectionSettings(new Uri("http://gmarz:p%40ssword@localhost:9200"));
			var client = new ElasticClient(settings);
			var response = client.RootNodeInfo();
			response.IsValid.Should().BeTrue();
		}

		[Test]
		public void ConnectionSettings_Overrides_URI()
		{
			var settings = new ConnectionSettings(new Uri("http://invalid:user@localhost:9200"))
				.SetBasicAuthorization("nestuser", "elastic");
			var client = new ElasticClient(settings);
			var response = client.RootNodeInfo();
			response.IsValid.Should().BeTrue();
		}

		[Test]
		public void RequestConfiguration_Overrides_ConnectionSettings()
		{
			var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
				.SetBasicAuthorization("invalid", "user");
			var client = new ElasticClient(settings);
			var response = client.RootNodeInfo(c => c
				.RequestConfiguration(rc => rc
					.BasicAuthorization("nestuser", "elastic")
				)
			);
			response.IsValid.Should().BeTrue();
		}
	}
}
