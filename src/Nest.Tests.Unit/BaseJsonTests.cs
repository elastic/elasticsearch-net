using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;
using System.Reflection;
using System.IO;
using Moq;

namespace Nest.Tests.Unit
{
	public class BaseJsonTests
	{
		protected readonly IConnectionSettings _settings;
		protected readonly IElasticClient _client;
		protected readonly IConnection _connection;

		public BaseJsonTests()
		{
			this._settings = new ConnectionSettings(Test.Default.Uri)
				.SetDefaultIndex(Test.Default.DefaultIndex);
			this._connection = new InMemoryConnection(this._settings);
			this._client = new ElasticClient(this._settings, this._connection);
		}

		protected void JsonEquals(object o, MethodBase method, string fileName = null)
		{
			var json = TestElasticClient.Serialize(o);
			this.JsonEquals(json, method, fileName);
		}
		protected void JsonEquals(string json, MethodBase method, string fileName = null)
		{
			var type = method.DeclaringType;
			var @namespace = method.DeclaringType.Namespace;
			var folder = @namespace.Replace("Nest.Tests.Unit.", "").Replace(".", "\\");

			var file = Path.Combine(folder, (fileName ?? method.Name) + ".json");
			file = Path.Combine(Environment.CurrentDirectory.Replace("bin\\Debug", "").Replace("bin\\Release", ""), file);


			var expected = File.ReadAllText(file);
			Assert.True(json.JsonEquals(expected), json);
		}
		protected void JsonNotEquals(object o, MethodBase method, string fileName = null)
		{
			var json = TestElasticClient.Serialize(o);
			this.JsonNotEquals(json, method, fileName);
		}
		protected void JsonNotEquals(string json, MethodBase method, string fileName = null)
		{
			var type = method.DeclaringType;
			var @namespace = method.DeclaringType.Namespace;
			var folder = @namespace.Replace("Nest.Tests.Unit.", "").Replace(".", "\\");

			var file = Path.Combine(folder, (fileName ?? method.Name) + ".json");
			file = Path.Combine(Environment.CurrentDirectory.Replace("bin\\Debug", "").Replace("bin\\Release", ""), file);


			var expected = File.ReadAllText(file);
			Assert.False(json.JsonEquals(expected), json);
		}
	}
}
