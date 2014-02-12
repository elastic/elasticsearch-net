using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace Nest.Tests.Integration.Yaml
{
	public class YamlTestsBase
	{
		protected readonly RawElasticClient _client;
		protected readonly Version _versionNumber;
		
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
			_client.IndicesDeleteTemplate("*");
			var info = _client.InfoGet().Deserialize<dynamic>();
			string version = info.version.number;
			this._versionNumber = new Version(version);
		}

		protected void Do(Func<ConnectionStatus> action, string shouldCatch = null)
		{
			this._status = action();
			if (shouldCatch == "missing")
			{
				Assert.NotNull(this._status.Error, "call specified missing is expected");
				Assert.AreEqual(this._status.Error.HttpStatusCode,HttpStatusCode.NotFound, "call specified missing (404) is expected");
			}
			else if (shouldCatch == "conflict")
			{
				Assert.NotNull(this._status.Error, "call specified conflict is expected");
				Assert.AreEqual(this._status.Error.HttpStatusCode,HttpStatusCode.Conflict, "call specified conflict (409) is expected");
				
			}
			else if (shouldCatch == "forbidden")
			{
				Assert.NotNull(this._status.Error, "call specified forbidden is expected");
				Assert.AreEqual(this._status.Error.HttpStatusCode,HttpStatusCode.Forbidden, "call specified conflict (403) is expected");
				
			}
			else if (shouldCatch != null && shouldCatch.StartsWith("/"))
			{
				var re = shouldCatch.Trim('/');
				Assert.IsTrue(Regex.IsMatch(this._status.Result, re),
					"response does not match regex: " + shouldCatch);
			}
			this._response = this._status.Deserialize<dynamic>();
			this._responseDictionary = this._status.Deserialize<Dictionary<string, object>>();
		}

		protected void Skip(string version, string reason)
		{
			var versions = version.Split('-').Select(v => v.Trim(' ')).ToList();
			var first = new Version(this.PatchVersion(versions.First()));
			var second = new Version(this.PatchVersion(versions.Last()));
			if (this._versionNumber.CompareTo(first) <= 0
				|| this._versionNumber.CompareTo(second) <= 0)
				Assert.Pass("Skipped as test ran against "+ this._versionNumber +" : " + reason);
		} 
	
		//.net Version class needs atleast 2 significant numbers
		private string PatchVersion(string version)
		{
			return (version == "999" || version == "0") ? version + ".0" : version;
		}

		protected void IsTrue(object o)
		{
			if (o == null)
				Assert.Fail("null is not true value");
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

			o = Unbox(o);
			string message = "Unknown type:" + o.GetType().FullName;
			
			//The specified key exists and has a true value (ie not 0, false, undefined, null or the empty string)
			if (o is int)
			{
				var i = (int)o;
				if (i == 0) Assert.Fail("Expected int to be not 0");
			}
			else if (o is long)
			{
				var i = (long)o;
				if (i == 0) Assert.Fail("Expected long to be not 0");
			}
			else if (o is bool)
			{
				var b = (bool)o;
				if (!b) Assert.Fail("Expected bool to be true");
			}
			else if (o is string)
			{
				var s = (string) o;
				if (string.IsNullOrWhiteSpace(s)) Assert.Fail("Expected string to not be null or whitespace");
			}
			else
			{
				var s = _client.Serializer.Serialize(o);
				if (string.IsNullOrWhiteSpace(s))
					Assert.Fail(message);
			}

		}

		protected void IsFalse(object o)
		{
			if (o == null)
				return;
			if (o is ConnectionStatus)
			{
				var c = o as ConnectionStatus;
				if (c.RequestMethod == "HEAD" && c.Error == null)
				{
					Assert.Fail("HEAD request did not return error status but:" 
						+ c.Error.HttpStatusCode);
				}
				else if (c.RequestMethod == "HEAD") return;
			}
			o = Unbox(o);
			
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
				if (((JObject) o).HasValues) Assert.Fail("Expected object to be empty");
			}
			else
			{
				var s = _client.Serializer.Serialize(o);
				if (string.IsNullOrWhiteSpace(s))
					Assert.Fail(message);
			}
		}

		protected void IsLowerThan(object o, int value)
		{
			o = Unbox(o);
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

		private static object Unbox(object o)
		{
			if (o is JValue) o = ((JValue) o).Value;
			if (o is JObject) o = ((JToken) o).ToObject<Dictionary<string, object>>();
			if (o is ConnectionStatus) o = ((ConnectionStatus)o).Result;
			return o;
		}

		protected void IsGreaterThan(object o, int value)
		{
			o = Unbox(o);
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
			int l = -1;
			if (o is JArray) l = ((JArray) o).Count;
			if (o is string) l =  ((string) o).Length;
			Assert.AreEqual(l, value);
		}

		protected void IsMatch(object o, object value)
		{
			o = Unbox(o);
			string message = "Unknown type ismatch:" + o.GetType().FullName;
			if (o is int)
			{
				var i = (int)o;
				var v = (int)value;
				Assert.AreEqual(v,i);
			}
			else if (o is double)
			{
				var i = (double)o;
				var v = Convert.ToDouble((int)value);
				Assert.AreEqual(v,i);
			}
			else if (o is long)
			{
				var i = (long)o;
				if (value is int)
				{
					var v = (int) value;
					Assert.AreEqual(v, i);
				}
				else if (value is long)
				{
					var v = (long) value;
					Assert.AreEqual(v, i);
				}
			}
			else if (o is string)
			{
				var s = (string)o;
				var v = value.ToString();
				if (s.StartsWith("{") && !(value is string))
				{
					var json = _client.Serializer.Serialize(value);
					var nJson = JObject.Parse(s).ToString();
					var nOtherJson = JObject.Parse(json).ToString();
					Assert.AreEqual(nJson, nOtherJson);
				}
				else Assert.AreEqual(s, v);
			}
			else if (o is Dictionary<string, object>)
			{
				var d = value as Dictionary<string, object>;
				var dd = o as Dictionary<string, object>;
				if (d == null)
					d = (from x in value.GetType().GetProperties() select x)
						.ToDictionary(
							x => x.Name, 
							x => (x.GetGetMethod().Invoke(value, null) ?? ""));
				CollectionAssert.AreEquivalent(d.ToList(), dd.ToList());
			}
			else Assert.Fail(message);
		}
	}


}
