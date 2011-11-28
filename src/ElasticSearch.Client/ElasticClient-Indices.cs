using System;
using System.Collections.Generic;
using System.Linq;
using ElasticSearch.Client.Mapping;
using ElasticSearch.Client.Settings;
using Newtonsoft.Json;

namespace ElasticSearch.Client
{
    public partial class ElasticClient
    {
        public IndicesResponse ClearCache()
        {
            return this.ClearCache(null, ClearCacheOptions.All);
        }

        public IndicesResponse ClearCache<T>() where T : class
        {
            return this.ClearCache(new List<string> {this.Settings.DefaultIndex}, ClearCacheOptions.All);
        }

        public IndicesResponse ClearCache<T>(ClearCacheOptions options) where T : class
        {
            return this.ClearCache(new List<string> {this.Settings.DefaultIndex}, options);
        }

        public IndicesResponse ClearCache(ClearCacheOptions options)
        {
            return this.ClearCache(null, options);
        }

        public IndicesResponse ClearCache(List<string> indices, ClearCacheOptions options)
        {
            string path = "/_cache/clear";
            if (indices != null && indices.Any(s => !string.IsNullOrEmpty(s)))
            {
                path = "/" + string.Join(",", indices.Where(s => !string.IsNullOrEmpty(s)).ToArray()) + path;
            }
            if (options != null && options != ClearCacheOptions.All)
            {
                var caches = new List<string>();
                if ((options & ClearCacheOptions.Id) == ClearCacheOptions.Id)
                {
                    caches.Add("id=true");
                }
                if ((options & ClearCacheOptions.Filter) == ClearCacheOptions.Filter)
                {
                    caches.Add("filter=true");
                }
                if ((options & ClearCacheOptions.FieldData) == ClearCacheOptions.FieldData)
                {
                    caches.Add("field_data=true");
                }
                if ((options & ClearCacheOptions.Bloom) == ClearCacheOptions.Bloom)
                {
                    caches.Add("bloom=true");
                }

                path += "?" + string.Join("&", caches.ToArray());
            }

            ConnectionStatus status = this.Connection.PostSync(path, string.Empty);
            var response = new IndicesResponse();
            try
            {
                response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
            }
            catch
            {
            }
            response.ConnectionStatus = status;
            return response;
        }

        public IndicesResponse CreateIndex(string index, IndexSettings settings)
        {
            string path = this.CreatePath(index);
            var status = this.Connection.PostSync(path, JsonConvert.SerializeObject(settings, Formatting.None, this.SerializationSettings));

            try
            {
                var response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
                response.ConnectionStatus = status;
                
                return response;
            } 
            catch
            {
                return new IndicesResponse();
            }
        }
        public IndicesResponse DeleteIndex<T>() where T : class
        {
            return this.DeleteIndex(this.Settings.DefaultIndex);
        }
        public IndicesResponse DeleteIndex(string index)
        {
            string path = this.CreatePath(index);

            var status = this.Connection.DeleteSync(path);
            var response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
            response.ConnectionStatus = status;

            return response;
        }

        public IndicesResponse DeleteMapping<T>() where T : class
        {
            string type = this.InferTypeName<T>();
            return this.DeleteMapping<T>(this.Settings.DefaultIndex, type);
        }

        public IndicesResponse DeleteMapping<T>(string index) where T : class
        {
            string type = this.InferTypeName<T>();
            return this.DeleteMapping<T>(index, type);
        }

        public IndicesResponse DeleteMapping<T>(string index, string type) where T : class
        {
            string path = this.CreatePath(index, type);
            ConnectionStatus status = this.Connection.DeleteSync(path);

            var response = new IndicesResponse();
            try
            {
                response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
            }
            catch
            {
            }

            response.ConnectionStatus = status;
            return response;
        }

        public IndicesResponse Map<T>() where T : class
        {
            string type = this.InferTypeName<T>();
            return this.Map<T>(this.Settings.DefaultIndex, type);
        }

        public IndicesResponse Map<T>(string index) where T : class
        {
            string type = this.InferTypeName<T>();
            return this.Map<T>(index, type);
        }

        public IndicesResponse Map<T>(string index, string type) where T : class
        {
            string path = this.CreatePath(index, type) + "_mapping";
            string map = this.CreateMapFor<T>(type);

            ConnectionStatus status = this.Connection.PutSync(path, map);

            var response = new IndicesResponse();
            try
            {
                response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result);
            }
            catch
            {
            }

            response.ConnectionStatus = status;
            return response;
        }
        public IndicesResponse Map(TypeMapping typeMapping)
        {
            return this.Map(typeMapping, this.Settings.DefaultIndex);
        }
        public IndicesResponse Map(TypeMapping typeMapping, string index)
        {
            string path = this.CreatePath(index, typeMapping.Name) + "_mapping";
            var mapping = new Dictionary<string, TypeMapping>();
            mapping.Add(typeMapping.Name, typeMapping);

            string map = JsonConvert.SerializeObject(mapping, Formatting.None, this.SerializationSettings);

            ConnectionStatus status = this.Connection.PutSync(path, map);

            var response = JsonConvert.DeserializeObject<IndicesResponse>(status.Result, this.SerializationSettings);

            response.ConnectionStatus = status;

            return response;
        }

        public TypeMapping GetMapping<T>() where T : class
        {
            return this.GetMapping<T>(this.Settings.DefaultIndex);
        }
        public TypeMapping GetMapping<T>(string index) where T : class
        {
            string type = this.InferTypeName<T>();
            return this.GetMapping(index, type);
        }

        public TypeMapping GetMapping(string index, string type)
        {
            string path = this.CreatePath(index, type) + "_mapping";

            ConnectionStatus status = this.Connection.GetSync(path);

            var mappings = JsonConvert.DeserializeObject<IDictionary<string, TypeMapping>>(status.Result, this.SerializationSettings);

            if (status.Success)
            {
                var mapping = mappings.First();
                mapping.Value.Name = mapping.Key;

                return mapping.Value;
            }

            return null;
        }

        private string CreateMapFor<T>(string type) where T : class
        {
            var writer = new TypeMappingWriter<T>(type, this.PropertyNameResolver);

            return writer.MapFromAttributes();
        }
    }

    [Flags]
    public enum ClearCacheOptions
    {
        Id = 0x1,
        Filter = 0x2,
        FieldData = 0x4,
        Bloom = 0x8,
        All = Id | Filter | FieldData | Bloom
    }
}