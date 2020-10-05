// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
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

			// ReSharper disable once CoVariantArrayConversion
			Task.WaitAll(tasks);

			for (var i = 0; i < count; i++)
				tasks[i].Result.Should().Be(password);
		}
	}
}
