using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Connection.HttpClient
{
    public class ElasticsearchHttpClient : IConnection
    {
	    private IConnectionConfigurationValues _settings;

	    public ElasticsearchHttpClient(IConnectionConfigurationValues settings)
	    {
		    _settings = settings;
	    }

	    public ElasticsearchResponse DoSyncRequest(string method, Uri uri, byte[] data = null)
	    {
		    var client = new System.Net.Http.HttpClient();
		    HttpResponseMessage response = null;
		    byte[] result = null;
		    HttpContent content = null;
			if (data != null)
				content = new ByteArrayContent(data);
		    switch (method.ToLower())
		    {
			    case "head":
					response = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri) ).Result;
					result = response.Content.ReadAsByteArrayAsync().Result;
				    break;
			    case "delete":
					response = client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, uri) { Content = content }).Result;
					result = response.Content.ReadAsByteArrayAsync().Result;
				    break;
			    case "put":
					response = client.PutAsync(uri, content).Result;
					result = response.Content.ReadAsByteArrayAsync().Result;
				    break;
			    case "post":
					response = client.PostAsync(uri, content).Result;
					result = response.Content.ReadAsByteArrayAsync().Result;
				    break;
			    case "get":
					response = client.GetAsync(uri).Result;
					result = response.Content.ReadAsByteArrayAsync().Result;
				    break;
		    }
			return ElasticsearchResponse.Create(this._settings, (int)response.StatusCode, method, uri.ToString(), data, result);
	    }



	    public Task<ElasticsearchResponse> Get(Uri uri)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse GetSync(Uri uri)
	    {
		    return this.DoSyncRequest("get", uri);
	    }

	    public Task<ElasticsearchResponse> Head(Uri uri)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse HeadSync(Uri uri)
	    {
		    return this.DoSyncRequest("head", uri);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse> Post(Uri uri, byte[] data)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse PostSync(Uri uri, byte[] data)
	    {
		    return this.DoSyncRequest("post", uri, data);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse> Put(Uri uri, byte[] data)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse PutSync(Uri uri, byte[] data)
	    {
		    return this.DoSyncRequest("put", uri, data);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse> Delete(Uri uri)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse DeleteSync(Uri uri)
	    {
		    return this.DoSyncRequest("delete", uri);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse> Delete(Uri uri, byte[] data)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse DeleteSync(Uri uri, byte[] data)
	    {
		    return this.DoSyncRequest("delete", uri, data);
		    throw new NotImplementedException();
	    }

	    public bool Ping(Uri uri)
	    {
		    throw new NotImplementedException();
	    }

	    public IList<Uri> Sniff(Uri uri)
	    {
		    throw new NotImplementedException();
	    }
    }
}
