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
	    private IConnectionSettings2 _settings;

	    public ElasticsearchHttpClient(IConnectionSettings2 settings)
	    {
		    _settings = settings;
	    }

	    public ElasticsearchResponse DoSyncRequest(string method, string path, byte[] data = null)
	    {
		    var client = new System.Net.Http.HttpClient();
		    var uri = new Uri(this._settings.Uri, path);
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



	    public Task<ElasticsearchResponse> Get(string path)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse GetSync(string path)
	    {
		    return this.DoSyncRequest("get", path);
	    }

	    public Task<ElasticsearchResponse> Head(string path)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse HeadSync(string path)
	    {
		    return this.DoSyncRequest("head", path);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse> Post(string path, byte[] data)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse PostSync(string path, byte[] data)
	    {
		    return this.DoSyncRequest("post", path, data);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse> Put(string path, byte[] data)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse PutSync(string path, byte[] data)
	    {
		    return this.DoSyncRequest("put", path, data);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse> Delete(string path)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse DeleteSync(string path)
	    {
		    return this.DoSyncRequest("delete", path);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse> Delete(string path, byte[] data)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse DeleteSync(string path, byte[] data)
	    {
		    return this.DoSyncRequest("delete", path, data);
		    throw new NotImplementedException();
	    }
    }
}
