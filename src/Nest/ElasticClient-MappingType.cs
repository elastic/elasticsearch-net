using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public partial class ElasticClient
    {
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the inferred type name for T under the default index
        /// </para>
        /// </summary>
        public IIndicesResponse MapFromAttributes<T>(int maxRecursion = 0) where T : class
        {
            string type = this.TypeNameResolver.GetTypeNameFor<T>();
			return this.MapFromAttributes<T>(this.IndexNameResolver.GetIndexForType<T>(), type, maxRecursion);
        }
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the inferred type name for T under the specified index
        /// </para>
        /// </summary>
		public IIndicesResponse MapFromAttributes<T>(string index, int maxRecursion = 0) where T : class
        {
            string type = this.TypeNameResolver.GetTypeNameFor<T>();
			return this.MapFromAttributes<T>(index, type, maxRecursion);
        }
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the specified type name under the specified index
        /// </para>
        /// </summary>
		public IIndicesResponse MapFromAttributes<T>(string index, string type, int maxRecursion = 0) where T : class
        {
            string path = this.PathResolver.CreateIndexTypePath(index, type, "_mapping");
            string map = this.CreateMapFor<T>(type, maxRecursion);

            ConnectionStatus status = this.Connection.PutSync(path, map);

            var response = new IndicesResponse();
            try
            {
                response = this.Deserialize<IndicesResponse>(status.Result);
                response.IsValid = true;
            }
            catch
            {
            }

            response.ConnectionStatus = status;
            return response;
        }

        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the inferred type name for T under the default index
        /// </para>
        /// </summary>
		public IIndicesResponse MapFromAttributes(Type t, int maxRecursion = 0)
        {
            string type = this.TypeNameResolver.GetTypeNameForType(t);
			return this.MapFromAttributes(t, this.IndexNameResolver.GetIndexForType(t), type, maxRecursion);
        }
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the inferred type name for T under the specified index
        /// </para>
        /// </summary>
		public IIndicesResponse MapFromAttributes(Type t, string index, int maxRecursion = 0)
        {
            string type = this.TypeNameResolver.GetTypeNameForType(t);
			return this.MapFromAttributes(t, index, type, maxRecursion);
        }
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the specified type name under the specified index
        /// </para>
        /// </summary>
		public IIndicesResponse MapFromAttributes(Type t, string index, string type, int maxRecursion = 0)
        {
            string path = this.PathResolver.CreateIndexTypePath(index, type, "_mapping");
            string map = this.CreateMapFor(t, type, maxRecursion);

            ConnectionStatus status = this.Connection.PutSync(path, map);

            var response = new IndicesResponse();
            try
            {
                response = this.Deserialize<IndicesResponse>(status.Result);
                response.IsValid = true;
            }
            catch
            {
            }

            response.ConnectionStatus = status;
            return response;
        }


        /// <summary>
        /// Verbosely and explicitly map an object using a TypeMapping object, this gives you exact control over the mapping. Index is the inferred default index
        /// </summary>
        public IIndicesResponse Map(TypeMapping typeMapping)
        {
            return this.Map(typeMapping, this.Settings.DefaultIndex);
        }
        /// <summary>
        /// Verbosely and explicitly map an object using a TypeMapping object, this gives you exact control over the mapping.
        /// </summary>
        public IIndicesResponse Map(TypeMapping typeMapping, string index)
        {
            string path = this.PathResolver.CreateIndexTypePath(index, typeMapping.Name, "_mapping");
            var mapping = new Dictionary<string, TypeMapping>();
            mapping.Add(typeMapping.Name, typeMapping);

            string map = JsonConvert.SerializeObject(mapping, Formatting.None, SerializationSettings);

            ConnectionStatus status = this.Connection.PutSync(path, map);

            var r = this.ToParsedResponse<IndicesResponse>(status);
            return r;
        }

        private string CreateMapFor<T>(string type, int maxRecursion = 0) where T : class
        {
            return this.CreateMapFor(typeof(T), type, maxRecursion);
        }
        private string CreateMapFor(Type t, string type, int maxRecursion = 0)
        {
            var writer = new TypeMappingWriter(t, type, maxRecursion);

            return writer.MapFromAttributes();
        }
    }
}