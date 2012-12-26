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
        public IIndicesResponse Map<T>(int maxRecursion = 0) where T : class
        {
            string type = this.TypeNameResolver.GetTypeNameFor<T>();
            return this.Map<T>(this.IndexNameResolver.GetIndexForType<T>(), type, maxRecursion);
        }
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the inferred type name for T under the specified index
        /// </para>
        /// </summary>
        public IIndicesResponse Map<T>(string index, int maxRecursion = 0) where T : class
        {
            string type = this.TypeNameResolver.GetTypeNameFor<T>();
            return this.Map<T>(index, type, maxRecursion);
        }
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the specified type name under the specified index
        /// </para>
        /// </summary>
        public IIndicesResponse Map<T>(string index, string type, int maxRecursion = 0) where T : class
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
        public IIndicesResponse Map(Type t, int maxRecursion = 0)
        {
            string type = this.TypeNameResolver.GetTypeNameForType(t);
            return this.Map(t, this.IndexNameResolver.GetIndexForType(t), type, maxRecursion);
        }
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the inferred type name for T under the specified index
        /// </para>
        /// </summary>
        public IIndicesResponse Map(Type t, string index, int maxRecursion = 0)
        {
            string type = this.TypeNameResolver.GetTypeNameForType(t);
            return this.Map(t, index, type, maxRecursion);
        }
        /// <summary>
        /// <para>Automatically map an object based on its attributes, this will also explicitly map strings to strings, datetimes to dates etc even 
        /// if they are not marked with any attributes.</para>
        /// <para>
        /// Type name is the specified type name under the specified index
        /// </para>
        /// </summary>
        public IIndicesResponse Map(Type t, string index, string type, int maxRecursion = 0)
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
        /// <summary>
        /// Get the current mapping for T at the default index
        /// </summary>
        public TypeMapping GetMapping<T>() where T : class
        {
            var index = this.IndexNameResolver.GetIndexForType<T>();
            return this.GetMapping<T>(index);
        }
        /// <summary>
        /// Get the current mapping for T at the specified index
        /// </summary>
        public TypeMapping GetMapping<T>(string index) where T : class
        {
            string type = this.TypeNameResolver.GetTypeNameFor<T>();
            return this.GetMapping(index, type);
        }
        /// <summary>
        /// Get the current mapping for T at the default index
        /// </summary>
        public TypeMapping GetMapping(Type t)
        {
            var index = this.IndexNameResolver.GetIndexForType(t);
            return this.GetMapping(t, index);
        }
        /// <summary>
        /// Get the current mapping for T at the specified index
        /// </summary>
        public TypeMapping GetMapping(Type t, string index)
        {
            string type = this.TypeNameResolver.GetTypeNameForType(t);
            return this.GetMapping(index, type);
        }


        /// <summary>
        /// Get the current mapping for type at the specified index
        /// </summary>
        public TypeMapping GetMapping(string index, string type)
        {
            string path = this.PathResolver.CreateIndexTypePath(index, type, "_mapping");

            ConnectionStatus status = this.Connection.GetSync(path);
            try
            {
                var mappings = this.Deserialize<IDictionary<string, TypeMapping>>(status.Result);

                if (status.Success)
                {
                    var mapping = mappings.First();
                    mapping.Value.Name = mapping.Key;

                    return mapping.Value;
                }
            }
            catch (Exception e)
            {
                //TODO LOG
            }
            return null;
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