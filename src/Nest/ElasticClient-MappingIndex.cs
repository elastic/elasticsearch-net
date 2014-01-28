using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Nest
{
	public partial class ElasticClient
	{
		private static readonly Regex StripIndex = new Regex(@"^index\.");

		public IIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, GetIndexSettingsDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new GetIndexSettingsDescriptor());
			var pathInfo = descriptor.ToPathInfo<GetIndexSettingsQueryString>(this._connectionSettings, descriptor._QueryString);
			var status = this.RawDispatch.IndicesGetSettingsDispatch(pathInfo);

			return CreateIndexSettingsResponse(status);
		}


		public Task<IIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, GetIndexSettingsDescriptor> selector)
		{
			selector.ThrowIfNull("selector");
			var descriptor = selector(new GetIndexSettingsDescriptor());
			var pathInfo = descriptor.ToPathInfo<GetIndexSettingsQueryString>(this._connectionSettings, descriptor._QueryString);
			return this.RawDispatch.IndicesGetSettingsDispatchAsync(pathInfo)
				.ContinueWith(t => CreateIndexSettingsResponse(t.Result));
		}

		private IIndexSettingsResponse CreateIndexSettingsResponse(ConnectionStatus status)
		{
			var response = new IndexSettingsResponse {IsValid = false};
			try
			{
				var settingsContainer = SettingsContainer(status);
				response.Settings = this.Deserialize<IndexSettings>(settingsContainer);
				response.IsValid = true;
			}
			// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}
			response.ConnectionStatus = status;
			return response;
		}
		public IIndicesResponse DeleteIndex(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector)
		{
			return this.Dispatch<DeleteIndexDescriptor, DeleteIndexQueryString, IndicesResponse>(
				selector, 
				(p, d)=> this.RawDispatch.IndicesDeleteDispatch(p)
			);
		}
		public Task<IIndicesResponse> DeleteIndexAsync(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector)
		{
			return this.DispatchAsync<DeleteIndexDescriptor, DeleteIndexQueryString, IndicesResponse, IIndicesResponse>(
				selector, 
				(p, d)=> this.RawDispatch.IndicesDeleteDispatchAsync(p)
			);
		}
		
		//TODO although this gets the job done this looks a bit iffy, refactor
		private JObject SettingsContainer(ConnectionStatus status)
		{
			var o = JObject.Parse(status.Result);
			var settingsObject = o.First.First.First.First;

			var settingsContainer = new JObject();
			// In indexsettings response all analyzers etc are delivered as settings so need to split up the settings key and make proper json
			foreach (JProperty s in settingsObject.Children<JProperty>())
			{
				var name = StripIndex.Replace(s.Name, "");
				if (name.StartsWith("analysis."))
				{
					var keys = name.Split('.');
					RewriteIndexSettingsResponseToIndexSettingsJSon(settingsContainer, keys, s.Value);
				}
				else if (name.StartsWith("similarity."))
				{
					var keys = name.Split('.');
					var similaryKeys = new[] {keys[0], keys[1], string.Join(".", keys.Skip(2).ToArray())};
					RewriteIndexSettingsResponseToIndexSettingsJSon(settingsContainer, similaryKeys, s.Value);
				}
				else
				{
					RewriteIndexSettingsResponseToIndexSettingsJSon(settingsContainer, new[] {name}, s.Value);
				}
			}
			return settingsContainer;
		}

		/// <summary>
		/// Rewrites the index settings response to index settings json.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		private void RewriteIndexSettingsResponseToIndexSettingsJSon(JContainer container, string[] key, JToken value)
		{
			var thisKey = key.First();
			int indexer;
			
			if (key.Length > 2 || (key.Length == 2 && !int.TryParse(key.Last(), out indexer)))
			{
				var property = (JContainer)((JObject)container).GetValue(thisKey);
				if (property == null)
				{
					property = new JObject();
					((JObject)container).Add(thisKey, property);
				}
				RewriteIndexSettingsResponseToIndexSettingsJSon(property, key.Skip(1).ToArray(), value);
			}
			else if (key.Length == 2 && int.TryParse(key.Last(), out indexer))
			{
				var property = ((JObject)container).Property(thisKey);
				if (property == null)
				{
					property = new JProperty(thisKey, new JArray());
					container.Add(property);
				}
				var jArray = (JArray)property.Value;
				jArray.Add(value);
			}
			else
			{
				var property = new JProperty(thisKey, value);
				container.Add(property);
			}
		}

	}
}