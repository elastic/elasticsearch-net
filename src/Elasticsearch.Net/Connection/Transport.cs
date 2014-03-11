using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Exceptions;
using Elasticsearch.Net.Providers;
using Elasticsearch.Net.Serialization;

namespace Elasticsearch.Net.Connection
{
	public class Transport : ITransport
	{
		private readonly IConnectionConfigurationValues _configurationValues;
		private readonly IConnection _connection;
		private readonly IElasticsearchSerializer _serializer;
		private readonly IConnectionPool _connectionPool;
		private IDateTimeProvider _dateTimeProvider;
		private DateTime? _lastSniff = null;

		public Transport(
			IConnectionConfigurationValues configurationValues,
			IConnection connection, 
			IElasticsearchSerializer serializer,
			IDateTimeProvider dateTimeProvider = null
			)
		{
			_dateTimeProvider = dateTimeProvider;
			this._connection = connection;
			this._configurationValues = configurationValues;
			this._serializer = serializer ?? new ElasticsearchDefaultSerializer();
			this._connectionPool = this._configurationValues.ConnectionPool;
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

		/// <summary>
		/// Returns either the fixed maximum set on the connection configuration settings or the number of nodes
		/// </summary>
		private int GetMaximumRetries()
		{
			return this._configurationValues.MaxRetries.GetValueOrDefault(this._connectionPool.MaxRetries);
		}

		public ElasticsearchResponse DoRequest(string method, string path, object data = null, NameValueCollection queryString = null, int retried = 0, int? seed = null)
		{
			SniffIfInformationIsTooOld(retried);

			if (queryString != null)
				path += queryString.ToQueryString();

			var maxRetries = this.GetMaximumRetries();
			var postData = PostData(data);
			ElasticsearchResponse response = null;
			var exceptionMessage = "Unable to perform request: '{0} {1}' on any of the nodes after retrying {2} times."
				.F( method, path, retried);
			int initialSeed;
			var baseUri = this._connectionPool.GetNext(seed, out initialSeed);
			bool seenError = false;
			try
			{
				var uri = new Uri(baseUri, path);
				response = DoSyncRequest(method, uri, postData);
				if (response != null && response.SuccessOrKnownError)
					return response;
			}
			catch (Exception e)
			{
				seenError = true;
				this._connectionPool.MarkDead(baseUri);
				if (this._configurationValues.SniffsOnConnectionFault && retried == 0)
					this.Sniff();
				if (retried < maxRetries)
				{
					return this.DoRequest(method, path, data, null, ++retried, initialSeed);
				}
				else
					throw new OutOfNodesException(exceptionMessage, e);
			}
			finally
			{
				//make sure we always call markalive on the uri if the connection was succesful
				if (!seenError && response != null && response.SuccessOrKnownError)
					this._connectionPool.MarkAlive(baseUri);
			}
			this._connectionPool.MarkDead(baseUri);
			if (this._configurationValues.SniffsOnConnectionFault && retried == 0)
				this.Sniff();

			if (retried < maxRetries)
			{
				return this.DoRequest(method, path, data, null, ++retried, initialSeed);
			}	
			throw new OutOfNodesException(exceptionMessage);
		}

		private void SniffIfInformationIsTooOld(int retried)
		{
			var sniffLifeSpan = this._configurationValues.SniffInformationLifeSpan;
			var now = this._dateTimeProvider.Now();
			if (retried == 0 && this._lastSniff.HasValue &&
			    sniffLifeSpan.HasValue && sniffLifeSpan.Value < (now - this._lastSniff.Value))
				this.Sniff();
		}

		public Task<ElasticsearchResponse> DoRequestAsync(
			string method, string path, object data = null, NameValueCollection queryString = null, int retried = 0)
		{
			if (queryString != null)
				path += queryString.ToQueryString();

			var postData = PostData(data);
			int seed;
			var baseUri = this._connectionPool.GetNext(null, out seed);
			var uri = new Uri(baseUri, path);
			
			switch (method.ToLowerInvariant())
			{
				case "post": return this._connection.Post(uri, postData);
				case "put": return this._connection.Put(uri, postData);
				case "delete":
					return postData == null || postData.Length == 0
						? this._connection.Delete(uri)
						: this._connection.Delete(uri, postData);
				case "head": return this._connection.Head(uri);
				case "get": return this._connection.Get(uri);
			}
			throw new Exception("Unknown HTTP method " + method);
		}
	
		private ElasticsearchResponse DoSyncRequest(string method, Uri uri, byte[] postData)
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
			return null;
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