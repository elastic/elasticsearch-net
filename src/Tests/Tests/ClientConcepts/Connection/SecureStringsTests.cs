using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;

namespace Tests.ClientConcepts.Connection
{
	public class SecureStringsTests
	{
		[U]
		public void CreateStringMatchesOriginalString()
		{
			var password = "password";
			var secureString = password.CreateSecureString();
			var count = 100;
			var tasks = new Task<string>[count];

			for (var i = 0; i < count; i++)
				tasks[i] = new Task<string>(() => secureString.CreateString());

			for (var i = 0; i < count; i++)
				tasks[i].Start();

			Task.WaitAll(tasks);

			for (var i = 0; i < count; i++)
				tasks[i].Result.Should().Be(password);
		}
	}
}
