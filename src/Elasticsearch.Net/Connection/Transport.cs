using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Serialization;
using PUrify;

namespace Elasticsearch.Net.Connection
{
	public class Transport : ITransport
	{
		protected internal readonly IConnectionConfigurationValues _configurationValues;
		protected internal readonly IConnection _connection;
		protected internal readonly IElasticsearchSerializer _serializer;

		private readonly IConnectionPool _connectionPool;
		private IDateTimeProvider _dateTimeProvider;
		private DateTime? _lastSniff = null;

		public IConnectionConfigurationValues Settings { get { return _configurationValues; } }
		public IElasticsearchSerializer Serializer { get { return _serializer; } }

		public Transport(
			IConnectionConfigurationValues configurationValues,
			IConnection connection, 
			IElasticsearchSerializer serializer,
			IDateTimeProvider dateTimeProvider = null
			)
		{
			this._configurationValues = configurationValues;
			this._connection = connection?? new HttpConnection(configurationValues);
			this._serializer = serializer ?? new ElasticsearchDefaultSerializer();
			this._connectionPool = this._configurationValues.ConnectionPool;

			//TODO: take the datetimeprovider from the connection pool?
			this._dateTimeProvider = dateTimeProvider ?? new DateTimeProvider();

			if (this._configurationValues.SniffsOnStartup)
				this.Sniff(fromStartup: true);
			else
				this._lastSniff = this._dateTimeProvider.Now();
		}

		private void Sniff(bool fromStartup = false)
		{
			this._connectionPool.Sniff(this._connection, fromStartup);
			this._lastSniff = this._dateTimeProvider.Now();
		}

		private void SniffIfInformationIsTooOld(int retried)
		{
			var sniffLifeSpan = this._configurationValues.SniffInformationLifeSpan;
			var now = this._dateTimeProvider.Now();
			if (retried == 0 && this._lastSniff.HasValue &&
			    sniffLifeSpan.HasValue && sniffLifeSpan.Value < (now - this._lastSniff.Value))
				this.Sniff();
		}

		/// <summary>
		/// Returns either the fixed maximum set on the connection configuration settings or the number of nodes
		/// </summary>
		private int GetMaximumRetries()
		{
			return this._configurationValues.MaxRetries.GetValueOrDefault(this._connectionPool.MaxRetries);
		}


		public ElasticsearchResponse DoRequest(
			string method,
			string path,
			object data = null,
			NameValueCollection queryString = null,
			int retried = 0,
			int? seed = null)
		{
			SniffIfInformationIsTooOld(retried);

			if (queryString != null) path += queryString.ToQueryString();

			var postData = PostData(data);
			ElasticsearchResponse response = null;
			
			int initialSeed;
			var baseUri = this._connectionPool.GetNext(seed, out initialSeed);
			bool seenError = false;

			try
			{
				var uri = CreateUriToPath(baseUri, path);
				response = _doRequest(method, uri, postData);
				if (response != null && response.SuccessOrKnownError)
					return response;
			}
			catch (Exception e)
			{
				seenError = true;
				return RetryRequest(method, path, data, retried, baseUri, initialSeed, e);
			}
			finally
			{
				//make sure we always call markalive on the uri if the connection was succesful
				if (!seenError && response != null && response.SuccessOrKnownError)
					this._connectionPool.MarkAlive(baseUri);
			}
			return RetryRequest(method, path, data, retried, baseUri, initialSeed, null);
		}

		private ElasticsearchResponse RetryRequest(
			string method, string path, object data, int retried, Uri baseUri,
			int initialSeed, Exception e)
		{
			var maxRetries = this.GetMaximumRetries();
			var exceptionMessage = "Unable to perform request: '{0} {1}' on any of the nodes after retrying {2} times."
				.F( method, path, retried);
			this._connectionPool.MarkDead(baseUri);
			if (this._configurationValues.SniffsOnConnectionFault && retried == 0)
				this.Sniff();
			if (retried < maxRetries)
			{
				return this.DoRequest(method, path, data, null, ++retried, initialSeed);
			}
			throw new OutOfNodesException(exceptionMessage, e);
		}

		private ElasticsearchResponse _doRequest(string method, Uri uri, byte[] postData)
		{
			switch (method.ToLowerInvariant())
			{
				case "post":
					return this._connection.PostSync(uri, postData);
				case "put":
					return this._connection.PutSync(uri, postData);
				case "delete":
					return postData == null || postData.Length == 0
						? this._connection.DeleteSync(uri)
						: this._connection.DeleteSync(uri, postData);
				case "head":
					return this._connection.HeadSync(uri);
				case "get":
					return this._connection.GetSync(uri);
			}
			throw new Exception("Unknown HTTP method " + method);
		}

		public Task<ElasticsearchResponse> DoRequestAsync(
			string method,
			string path,
			object data = null,
			NameValueCollection queryString = null,
			int retried = 0,
			int? seed = null)
		{
			SniffIfInformationIsTooOld(retried);
			
			if (queryString != null) path += queryString.ToQueryString();

			var postData = PostData(data);
			int initialSeed;
			var baseUri = this._connectionPool.GetNext(seed, out initialSeed);
			var uri = CreateUriToPath(baseUri, path);
			return _doRequestAsync(method, uri, postData).ContinueWith(t=>
			{
				if (t.IsCanceled)
					return null;
				if (t.IsFaulted)
					return this.RetryRequestAsync(method, path, data, retried, baseUri, initialSeed, t.Exception);
				if (t.Result.SuccessOrKnownError)
					return t;
				return this.RetryRequestAsync(method, path, data, retried, baseUri, initialSeed, null);
					
			}).Unwrap<ElasticsearchResponse>();
		}
		private Task<ElasticsearchResponse> RetryRequestAsync(
			string method, string path, object data, int retried, Uri baseUri,
			int initialSeed, Exception e)
		{
			var maxRetries = this.GetMaximumRetries();
			var exceptionMessage = "Unable to perform request: '{0} {1}' on any of the nodes after retrying {2} times."
				.F( method, path, retried);
			this._connectionPool.MarkDead(baseUri);
			if (this._configurationValues.SniffsOnConnectionFault && retried == 0)
				this.Sniff();
			if (retried < maxRetries)
			{
				return this.DoRequestAsync(method, path, data, null, ++retried, initialSeed);
			}
			throw new OutOfNodesException(exceptionMessage, e);
		}
		private Task<ElasticsearchResponse> _doRequestAsync(string method, Uri uri, byte[] postData)
		{
			switch (method.ToLowerInvariant())
			{
				case "post":
					return this._connection.Post(uri, postData);
				case "put":
					return this._connection.Put(uri, postData);
				case "delete":
					return postData == null || postData.Length == 0
						? this._connection.Delete(uri)
						: this._connection.Delete(uri, postData);
				case "head":
					return this._connection.Head(uri);
				case "get":
					return this._connection.Get(uri);
			}
			throw new Exception("Unknown HTTP method " + method);
		}
		
		private Uri CreateUriToPath(Uri baseUri, string path)
		{
			var s = this.Settings;
			if (s.QueryStringParameters != null)
			{
				var tempUri = new Uri(baseUri, path);
				var qs = s.QueryStringParameters.ToQueryString(tempUri.Query.IsNullOrEmpty() ? "?" : "&");
				path += qs;
			}
			var uri = path.IsNullOrEmpty() ? baseUri : new Uri(baseUri, path);
			return uri.Purify();
		}
		
		private byte[] PostData(object data)
		{
			var bytes = data as byte[];
			if (bytes != null)
				return bytes;

			var s = data as string;
			if (s != null)
				return s.Utf8Bytes();
			if (data == null) return null;
			var ss = data as IEnumerable<string>;
			if (ss != null)
				return (string.Join("\n", ss) + "\n").Utf8Bytes();
			
			var so = data as IEnumerable<object>;
			if (so == null)
				return this._serializer.Serialize(data);
			var joined = string.Join("\n", so
				.Select(soo => this._serializer.Serialize(soo, SerializationFormatting.None).Utf8String())) + "\n";
			return joined.Utf8Bytes();
		}
	}
}