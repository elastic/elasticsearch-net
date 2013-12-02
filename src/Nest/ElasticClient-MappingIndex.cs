using System;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Nest
{
	public partial class ElasticClient
	{
		private static Regex StripIndex = new Regex(@"^index\.");

		/// <summary>
		/// Gets the index settings for the default index
		/// </summary>
		public IIndexSettingsResponse GetIndexSettings()
		{
			var index = this._connectionSettings.DefaultIndex;
			return this.GetIndexSettings(index);
		}
		/// <summary>
		/// Gets the index settings for the specified index
		/// </summary>
		public IIndexSettingsResponse GetIndexSettings(string index)
		{
			string path = this.PathResolver.CreateIndexPath(index, "_settings");
			var status = this.Connection.GetSync(path);

			var response = new IndexSettingsResponse();
			response.IsValid = false;
			try
			{
				var o = JObject.Parse(status.Result);
				var settingsObject = o.First.First.First.First;

				var settingsContainer = new JObject();
				// In indexsettings response all analyzers etc are delivered as settings so need to split up the settings key and make proper json
				foreach (JProperty s in settingsObject.Children<JProperty>())
				{
					var name = StripIndex.Replace(s.Name, "");
					if (name.StartsWith("analysis"))
					{
						var key = name.Split('.');
						RewriteIndexSettingsResponseToIndexSettingsJSon(settingsContainer, key, s.Value);
					}
					else
					{
						RewriteIndexSettingsResponseToIndexSettingsJSon(settingsContainer, new[] { name }, s.Value);
					}
				}

				response.Settings = this.Deserialize<IndexSettings>(settingsContainer);
				response.IsValid = true;
			}
			catch { }
			response.ConnectionStatus = status;
			return response;
		}

		/// <summary>
		/// Update the index settings for the default index
		/// </summary>
		public ISettingsOperationResponse UpdateSettings(IndexSettings settings)
		{
			var index = this._connectionSettings.DefaultIndex;
			return this.UpdateSettings(index, settings);
		}
		/// <summary>
		/// Update the index settings for the specified index
		/// </summary>
		public ISettingsOperationResponse UpdateSettings(string index, IndexSettings settings)
		{

			string path = this.PathResolver.CreateIndexPath(index, "_settings");
			settings.Settings = settings.Settings
					.Where(kv => IndexSettings.UpdateWhiteList.Any(p => kv.Key.StartsWith(p))).ToDictionary(kv => kv.Key, kv => kv.Value);

			string data = this.Serializer.Serialize(settings, Formatting.None);
			var status = this.Connection.PutSync(path, data);

			var r = new SettingsOperationResponse();
			try
			{
				r = this.Deserialize<SettingsOperationResponse>(status.Result);
			}
			catch
			{

			}
			r.IsValid = status.Success;
			r.ConnectionStatus = status;
			return r;
		}

		/// <summary>
		/// Delete the default index
		/// </summary>
		public IIndicesResponse DeleteIndex<T>() where T : class
		{
			return this.DeleteIndex(this.Infer.IndexName<T>());
		}
		/// <summary>
		/// Delete the specified index
		/// </summary>
		public IIndicesResponse DeleteIndex(string index)
		{
			string path = this.PathResolver.CreateIndexPath(index);

			var status = this.Connection.DeleteSync(path);
			var r = this.Deserialize<IndicesResponse>(status);
			return r;
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