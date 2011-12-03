using System;
using System.Collections.Generic;
using System.Linq;
using ElasticSearch.Client.Mapping;
using ElasticSearch.Client.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace ElasticSearch.Client
{
    public partial class ElasticClient
    {
        private static Regex StripIndex = new Regex(@"^index\.");
        public IndexSettingsResponse GetIndexSettings()
        {
            var index = this.Settings.DefaultIndex;
            return this.GetIndexSettings(index);
        }
        public IndexSettingsResponse GetIndexSettings(string index)
        {
            string path = this.CreatePath(index) + "_settings";
            var status = this.Connection.GetSync(path);

            var response = new IndexSettingsResponse();
            response.IsValid = false;
            try
            {
                var o = JObject.Parse(status.Result);
                var settingsObject = o.First.First.First.First;
                var settings = JsonConvert
                    .DeserializeObject<IndexSettings>(settingsObject.ToString());

                foreach (JProperty s in settingsObject.Children<JProperty>())
                {
                    settings.Add(StripIndex.Replace(s.Name, ""), s.Value.ToString());
                }
                

                response.Settings = settings;
                response.IsValid = true;
            }
            catch { }
            response.ConnectionStatus = status;
            return response;
        }

        public SettingsOperationResponse UpdateSettings(IndexSettings settings)
        {
            var index = this.Settings.DefaultIndex;
            return this.UpdateSettings(index, settings);
        }
        public SettingsOperationResponse UpdateSettings(string index, IndexSettings settings)
        {

            string path = this.CreatePath(index) + "_settings";
            settings.Settings = settings.Settings
                .Where(kv=>IndexSettings.UpdateWhiteList.Any(p=>
                {
                    return kv.Key.StartsWith(p);
                }
                )).ToDictionary(kv=>kv.Key, kv=>kv.Value);
            
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WritePropertyName("index");
                jsonWriter.WriteStartObject();
                foreach (var kv in settings.Settings)
                {
                    jsonWriter.WritePropertyName(kv.Key);
                    jsonWriter.WriteValue(kv.Value);
                }

                jsonWriter.WriteEndObject();
            }
           string data = sb.ToString();
            
           var status = this.Connection.PutSync(path, data);

            var r = new SettingsOperationResponse();
            try
            {
                r = JsonConvert.DeserializeObject<SettingsOperationResponse>(status.Result);
            }
            catch
            {
                
            }
            r.IsValid = status.Success;
            r.ConnectionStatus = status;
            return r;
        }

        public IndicesResponse CreateIndex(string index, IndexSettings settings)
        {
            string path = this.CreatePath(index);
            string data =  JsonConvert.SerializeObject(settings, Formatting.None, this.SerializationSettings);
            var status = this.Connection.PostSync(path,data);
            var response = new IndicesResponse();
            response.ConnectionStatus = status;
            try
            {
                response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
                response.IsValid = true;
            } 
            catch (Exception e) {
            }
            return response;
        }
        public IndicesResponse DeleteIndex<T>() where T : class
        {
            return this.DeleteIndex(this.Settings.DefaultIndex);
        }
        public IndicesResponse DeleteIndex(string index)
        {
            string path = this.CreatePath(index);

            var status = this.Connection.DeleteSync(path);
            var r = this.ToParsedResponse<IndicesResponse>(status);
            return r;
        }

    }    
}