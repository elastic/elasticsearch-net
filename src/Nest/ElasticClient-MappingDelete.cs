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
        /// Deletes the mapping for the inferred type name of T under the default index
        /// </summary>
        public IIndicesResponse DeleteMapping<T>() where T : class
        {
            string type = this.GetTypeNameFor<T>();
            return this.DeleteMapping<T>(this.IndexNameResolver.GetIndexForType<T>(), type);
        }
        /// <summary>
        /// Deletes the mapping for the inferred type name of T under the specified index
        /// </summary>
        public IIndicesResponse DeleteMapping<T>(string index) where T : class
        {
            string type = this.GetTypeNameFor<T>();
            return this.DeleteMapping<T>(index, type);
        }
        /// <summary>
        /// Deletes the mapping for the specified type name under the specified index
        /// </summary>
        public IIndicesResponse DeleteMapping<T>(string index, string type) where T : class
        {
            string path = this.PathResolver.CreateIndexTypePath(index, type);
            
			ConnectionStatus status = this.Connection.DeleteSync(path);
	        return this.Deserialize<IndicesResponse>(status, allow404: true);
        }

        /// <summary>
        /// Deletes the mapping for the inferred type name of T under the default index
        /// </summary>
        public IIndicesResponse DeleteMapping(Type t)
        {
            string index = this.IndexNameResolver.GetIndexForType(t);
            string type = this.GetTypeNameFor(t);
            return this.DeleteMapping(t, index, type);
        }
        /// <summary>
        /// Deletes the mapping for the inferred type name of T under the specified index
        /// </summary>
        public IIndicesResponse DeleteMapping(Type t, string index)
        {
            string type = this.GetTypeNameFor(t);
            return this.DeleteMapping(t, index, type);
        }
        /// <summary>
        /// Deletes the mapping for the specified type name under the specified index
        /// </summary>
        public IIndicesResponse DeleteMapping(Type t, string index, string type)
        {
            string path = this.PathResolver.CreateIndexTypePath(index, type);
            ConnectionStatus status = this.Connection.DeleteSync(path);

            var response = new IndicesResponse();
            try
            {
                response = this.Deserialize<IndicesResponse>(status.Result);
            }
            catch
            {
            }

            response.ConnectionStatus = status;
            return response;
        }
    }
}