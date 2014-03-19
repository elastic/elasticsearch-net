using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

	    public ElasticsearchResponse<T> DoSyncRequest<T>(string method, Uri uri, byte[] data = null)
	    {
		    var client = new System.Net.Http.HttpClient();
		    HttpResponseMessage response = null;
		    Stream result = null;
		    HttpContent content = null;
			if (data != null)
				content = new ByteArrayContent(data);
		    switch (method.ToLower())
		    {
			    case "head":
					response = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri) ).Result;
				    break;
			    case "delete":
					response = client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, uri) { Content = content }).Result;
				    break;
			    case "put":
					response = client.PutAsync(uri, content).Result;
				    break;
			    case "post":
					response = client.PostAsync(uri, content).Result;
				    break;
			    case "get":
					response = client.GetAsync(uri).Result;
				    break;
		    }
			if (response == null)
				return ElasticsearchResponse<T>.CreateError(_settings, null, method, uri.ToString(), data);
			result = response.Content.ReadAsStreamAsync().Result;
			return ElasticsearchResponse<T>.Create(this._settings, (int)response.StatusCode, method, uri.ToString(), data, result);
	    }



	    public Task<ElasticsearchResponse<T>> Get<T>(Uri uri, object deserializeState = null)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse<T> GetSync<T>(Uri uri, object deserializeState = null)
	    {
		    return this.DoSyncRequest<T>("get", uri);
	    }

	    public Task<ElasticsearchResponse<T>> Head<T>(Uri uri)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse<T> HeadSync<T>(Uri uri)
	    {
		    return this.DoSyncRequest<T>("head", uri);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse<T>> Post<T>(Uri uri, byte[] data, object deserializeState = null)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse<T> PostSync<T>(Uri uri, byte[] data, object deserializeState = null)
	    {
		    return this.DoSyncRequest<T>("post", uri, data);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse<T>> Put<T>(Uri uri, byte[] data, object deserializeState = null)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse<T> PutSync<T>(Uri uri, byte[] data, object deserializeState = null)
	    {
		    return this.DoSyncRequest<T>("put", uri, data);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse<T>> Delete<T>(Uri uri, object deserializeState = null)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse<T> DeleteSync<T>(Uri uri, object deserializeState = null)
	    {
		    return this.DoSyncRequest<T>("delete", uri);
		    throw new NotImplementedException();
	    }

	    public Task<ElasticsearchResponse<T>> Delete<T>(Uri uri, byte[] data, object deserializeState = null)
	    {
		    throw new NotImplementedException();
	    }

	    public ElasticsearchResponse<T> DeleteSync<T>(Uri uri, byte[] data, object deserializeState = null)
	    {
		    return this.DoSyncRequest<T>("delete", uri, data);
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
