using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Connection.HttpClient;
using Elasticsearch.Net.JsonNet;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Elasticsearch.Net.Integration.Yaml
{
	public class YamlTestsBase
	{
		protected static readonly ElasticsearchClient _client;
		protected static readonly Version _versionNumber;
		
		protected object _body;
		protected ElasticsearchResponse<DynamicDictionary> _status;
		protected dynamic _response;

		static YamlTestsBase()
		{
			var host = "localhost";
			if (Process.GetProcessesByName("fiddler").Any())
				host = "ipv4.fiddler";
			var uri = new Uri("http://"+host+":9200/");
			var settings = new ConnectionConfiguration(uri).UsePrettyResponses();

			var jsonNetSerializer = new ElasticsearchJsonNetSerializer();

			//_client = new ElasticsearchClient(settings);
			_client = new ElasticsearchClient(settings, serializer: jsonNetSerializer);
			var infoResponse = _client.Info();
			dynamic info = infoResponse.Response;
			_versionNumber = new Version(info.version.number);
		}

		public YamlTestsBase()
		{
			_client.IndicesDelete("*");
		}

		protected void Do(Func<ElasticsearchResponse<DynamicDictionary>> action, string shouldCatch = null)
		{
			try
			{
				this._status = action();
			}
			catch (ArgumentException e)
			{
				if (shouldCatch == "param" && e.Message.Contains("can't be null or empty"))
					return;
				throw;
			}
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
				Assert.IsTrue(Regex.IsMatch(Encoding.UTF8.GetString(this._status.ResponseRaw), re),
					"response does not match regex: " + shouldCatch);
			}
			this._response = this._status.Response;
		}

		protected void Skip(string version, string reason)
		{
			var versions = version.Split('-').Select(v => v.Trim(' ')).ToList();
			var first = new Version(this.PatchVersion(versions.First()));
			var second = new Version(this.PatchVersion(versions.Last()));
			if (_versionNumber.CompareTo(first) <= 0
				|| _versionNumber.CompareTo(second) <= 0)
				Assert.Pass("Skipped as test ran against "+ _versionNumber +" : " + reason);
		} 
	
		//.net Version class needs atleast 2 significant numbers
		private string PatchVersion(string version)
		{
			return (!version.Contains(".")) ? version + ".0" : version;
		}

		protected void IsTrue(object o)
		{
			if (o == null) Assert.Fail("null is not true value");
			if (o is IElasticsearchResponse)
			{
				var c = o as IElasticsearchResponse;
				if (c.RequestMethod == "HEAD" && c.Error != null)
				{
					Assert.Fail("HEAD request returned status:" + c.Error.HttpStatusCode);
				}
				else if (c.RequestMethod == "HEAD") return;
				//o = Encoding.UTF8.GetString(c.ResponseRaw);
			}

			o = Unbox(o);
			if (o == null) Assert.Fail("null is not true value");
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
			else if (o is DynamicDictionary)
			{
				Assert.Greater(((DynamicDictionary)o).Count, 0);
			}
			else
			{
				var s = _client.Serializer.Serialize(o);
				if (s == null || s.Length == 0)
					Assert.Fail(message);
			}

		}

		protected void IsFalse(object o)
		{
			if (o == null)
				return;
			if (o is IElasticsearchResponse)
			{
				var c = o as IElasticsearchResponse;
				if (c.RequestMethod == "HEAD" && c.Error == null)
				{
					Assert.Fail("HEAD request did not return error status but:" 
						+ c.Error.HttpStatusCode);
				}
				else if (c.RequestMethod == "HEAD") return;
			}
			o = Unbox(o);
			if (o == null)
				return;
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
				if (s == null || s.Length == 0)
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
			else if (o is decimal)
			{
				var i = (decimal) o;
				Assert.Less(i, value);
			}
			else Assert.Fail("unknown type for lt: " + o.GetType().FullName);
		}

		private static object Unbox(object o)
		{
			if (o is ElasticsearchDynamicValue) o = ((ElasticsearchDynamicValue) o).Value;
			if (o is JValue) o = ((JValue)o).Value;
			if (o is JArray) o = ((JArray) o).ToObject<object[]>();
			if (o is JObject) o = ((JToken) o).ToObject<Dictionary<string, object>>();
			if (o is ElasticsearchResponse<DynamicDictionary>) o = ((ElasticsearchResponse<DynamicDictionary>)o).Response;
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
			else if (o is decimal)
			{
				var i = (decimal) o;
				Assert.Greater(i, value);
			}
			else Assert.Fail("unknown type for gt: " + o.GetType().FullName);
		}
		
		protected void IsLength(object o, int value)
		{
			int l = -1;
			if (o is ElasticsearchDynamicValue) o = ((ElasticsearchDynamicValue) o).Value;
			if (o is IList) l = ((IList) o).Count;
			if (o is string) l =  ((string) o).Length;
			if (o is IDictionary) l = ((IDictionary) o).Count;
			if (o is IDictionary<string, object>) l = ((IDictionary<string, object>) o).Count;
			Assert.AreEqual(value, l);
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
			else if (o is decimal)
			{
				var i = (decimal)o;
				var v = Convert.ToDecimal(value);
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
				//string is json parse it and compare it
				if (s.StartsWith("{") && !(value is string))
				{
					var json = _client.Serializer.Serialize(value);
					var nJson = JObject.Parse(s).ToString();
					var nOtherJson = JObject.Parse(Encoding.UTF8.GetString(json)).ToString();
					Assert.AreEqual(nJson, nOtherJson);
				}
				//string represents a regex on the response body
				else if (v.StartsWith("/"))
				{
					var re = Regex.Replace(v, @"(^[\s\r\n]*?\/|\/[\s\r\n]*?$)", "");
					var r = Encoding.UTF8.GetString(this._status.ResponseRaw);
					Assert.IsTrue(Regex.IsMatch(r, re, RegexOptions.IgnorePatternWhitespace));
				}
				else Assert.AreEqual(s, v);
			}
			else if (o is object[] || o is IList<object>)
			{
				var oo = (o as object[]) ?? (o as IList<object>);
				var json = _client.Serializer.Serialize(value);
				var otherJson = _client.Serializer.Serialize(oo);
				var nJson = JArray.Parse(Encoding.UTF8.GetString(json)).ToString();
				var nOtherJson = JArray.Parse(Encoding.UTF8.GetString(otherJson)).ToString();
				Assert.AreEqual(nJson, nOtherJson);
			}
			else if (o is IDictionary<string, object>)
			{
				var d = value as IDictionary<string, object>;
				var dd = o as IDictionary<string, object>;
				if (d == null) //d is an anonymous object turn it into a dict
					d = (from x in value.GetType().GetProperties() select x)
						.ToDictionary(
							x => x.Name, 
							x => (x.GetGetMethod().Invoke(value, null) ?? ""));

				var equals = DynamicDictionary.Create(d)
					.SequenceEqual(dd);
				Assert.True(equals, "response did not match expected return");

			}
			else Assert.Fail(message);
		}
	}


}
