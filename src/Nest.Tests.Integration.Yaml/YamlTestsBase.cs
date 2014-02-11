using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Yaml
{
	public class YamlTestsBase
	{
		protected readonly RawElasticClient _client;
		protected object _body;
		protected ConnectionStatus _status;
		protected dynamic _response;
		protected Dictionary<string, dynamic> _responseDictionary;

		public YamlTestsBase()
		{
			var host = "localhost";
			if (Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";
			var uri = new Uri("http://"+host+":9200/");
			var settings = new ConnectionSettings(uri, "nest-default-index");
			_client = new RawElasticClient(settings);

			_client.IndicesDelete(d => d.MasterTimeout("1m").Timeout("1m"));
		}

		protected void Do(Func<ConnectionStatus> action)
		{
			this._status = action();
			this._response = this._status.Deserialize<dynamic>();
			this._responseDictionary = this._status.Deserialize<Dictionary<string, object>>();
		}

		protected void IsTrue(object o)
		{
			if (o == null)
				Assert.Fail("null is not true value");

			string message = "Unknown type:" + o.GetType().FullName;
			if (o is JValue) o = ((JValue) o).Value;
			if (o is ConnectionStatus)
			{
				var c = o as ConnectionStatus;
				if (c.RequestMethod == "HEAD" && c.Error != null)
				{
					Assert.Fail("HEAD request returned status:" + c.Error.HttpStatusCode);
				}
				else if (c.RequestMethod == "HEAD") return;
				o = c.Result;
			}

			//The specified key exists and has a true value (ie not 0, false, undefined, null or the empty string)
			if (o is int)
			{
				var i = (int)o;
				if (i != 0) Assert.Fail("Expected int to be not 0");
			}
			else if (o is bool)
			{
				var b = (bool)o;
				if (!b) Assert.Fail("Expected bool to be true");
			}
			else if (o is string)
			{
				var s = (string)o;
				if (string.IsNullOrWhiteSpace(s)) Assert.Fail("Expected string to not be null or whitespace");
			}
			else if (o is JObject)
			{
				if (!((JObject)o).HasValues) Assert.Fail("Expected object not to be empty");
			}
			else Assert.Fail(message);

		}

		protected void IsFalse(object o)
		{
			if (o == null)
				return;

			if (o is JValue) o = ((JValue) o).Value;
			if (o is ConnectionStatus)
			{
				var c = o as ConnectionStatus;
				if (c.RequestMethod == "HEAD" && c.Error == null)
				{
					Assert.Fail("HEAD request did not return error status but:" 
						+ c.Error.HttpStatusCode);
				}
				else if (c.RequestMethod == "HEAD") return;
				o = c.Result;
			}
			//The specified key exists and has a true value (ie not 0, false, undefined, null or the empty string)
			string message = "Unknown type:" + o.GetType().FullName;
			if (o is int)
			{
				var i = (int)o;
				if (i == 0) Assert.Fail("Expected int to be  0");
			}
			else if (o is bool)
			{
				var b = (bool)o;
				if (b) Assert.Fail("Expected bool to be false");
			}
			else if (o is string)
			{
				var s = (string)o;
				if (!string.IsNullOrWhiteSpace(s)) Assert.Fail("Expected string to be null or whitespace");
			}
			else if (o is JObject)
			{
				if (((JObject)o).HasValues) Assert.Fail("Expected object to be empty");
			}
			else Assert.Fail(message);
		}

		protected void IsLowerThan(object o, int value)
		{
			if (o is JValue) o = ((JValue) o).Value;
			if (o is int)
			{
				var i = (int) o;
				Assert.Less(i, value);
			}
			else if (o is long)
			{
				var i = (long) o;
				Assert.Less(i, value);
			}
			else Assert.Fail("unknown type for lt: " + o.GetType().FullName);
		}

		protected void IsGreaterThan(object o, int value)
		{
			if (o is JValue) o = ((JValue) o).Value;
			if (o is int)
			{
				var i = (int) o;
				Assert.Greater(i, value);
			}
			else if (o is long)
			{
				var i = (long) o;
				Assert.Greater(i, value);
			}
			else Assert.Fail("unknown type for gt: " + o.GetType().FullName);
		}
		
		protected void IsLength(object o, int value)
		{
			
		}

		protected void IsMatch(object o, object value)
		{
			if (o is JValue) o = ((JValue) o).Value;
			if (o is int)
			{
				var i = (int)o;
				var v = (int)value;
				Assert.AreEqual(v,i);
			}
			if (o is long)
			{
				var i = (long)o;
				var v = (int)value;
				Assert.AreEqual(v,i);
			}
		}
	}


}
