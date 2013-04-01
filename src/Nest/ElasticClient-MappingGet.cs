using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public partial class ElasticClient
    {
		//TODO wrap in a BaseResponse

        /// <summary>
        /// Get the current mapping for T at the default index
        /// </summary>
        public RootObjectMapping GetMapping<T>() where T : class
        {
            var index = this.IndexNameResolver.GetIndexForType<T>();
            return this.GetMapping<T>(index);
        }
        /// <summary>
        /// Get the current mapping for T at the specified index
        /// </summary>
        public RootObjectMapping GetMapping<T>(string index) where T : class
        {
            string type = this.GetTypeNameFor<T>();
            return this.GetMapping(index, type);
        }
        /// <summary>
        /// Get the current mapping for T at the default index
        /// </summary>
        public RootObjectMapping GetMapping(Type t)
        {
            var index = this.IndexNameResolver.GetIndexForType(t);
            return this.GetMapping(t, index);
        }
        /// <summary>
        /// Get the current mapping for T at the specified index
        /// </summary>
        public RootObjectMapping GetMapping(Type t, string index)
        {
            string type = this.GetTypeNameFor(t);
            return this.GetMapping(index, type);
        }


        /// <summary>
        /// Get the current mapping for type at the specified index
        /// </summary>
        public RootObjectMapping GetMapping(string index, string type)
        {
            string path = this.PathResolver.CreateIndexTypePath(index, type, "_mapping");

            ConnectionStatus status = this.Connection.GetSync(path);
            try
            {
                var mappings = this.Deserialize<IDictionary<string, RootObjectMapping>>(status.Result);

                if (status.Success)
                {
                    var mapping = mappings.First();
                    mapping.Value.TypeNameMarker = mapping.Key;

                    return mapping.Value;
                }
            }
            catch (Exception e)
            {
                //TODO LOG
            }
            return null;
        }

    }
}