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

namespace Nest.Tests.Unit
{
	public class BaseJsonTests
	{
		protected readonly IConnectionSettings _settings;
		protected readonly IElasticClient _client;

		public BaseJsonTests()
		{
			this._settings = new ConnectionSettings(Test.Default.Uri)
				.SetDefaultIndex(Test.Default.DefaultIndex);
			this._client = new ElasticClient(this._settings);
		}

		protected void JsonEquals(object o, MethodBase method)
		{
			var type = method.DeclaringType;
			var @namespace = method.DeclaringType.Namespace;
			var folder = @namespace.Replace("Nest.Tests.Unit.", "").Replace(".", "\\");
			var file = Path.Combine(folder, method.Name + ".json");
			var json = TestElasticClient.Serialize(o);
			var expected = File.ReadAllText(file);
			Assert.True(json.JsonEquals(expected), json);
		}
		protected void JsonEquals(string json, MethodBase method)
		{
			var type = method.DeclaringType;
			var @namespace = method.DeclaringType.Namespace;
			var folder = @namespace.Replace("Nest.Tests.Unit.", "").Replace(".", "\\");
			var file = Path.Combine(folder, method.Name + ".json");
			var expected = File.ReadAllText(file);
			Assert.True(json.JsonEquals(expected), json);
		}
	}
}
