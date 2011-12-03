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
                response.IsValid = true;
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

            var r = this.ToParsedResponse<IndicesResponse>(status);
            return r;
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
}