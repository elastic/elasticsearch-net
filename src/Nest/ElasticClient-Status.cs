using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
    public partial class ElasticClient
    {
        /// <summary>
        ///  Get the Status of all indexes
        /// </summary>
        /// <returns></returns>
        public IStatusResponse Status()
        {
            return this.Status("_all", new StatusParams());
        }
        /// <summary>
        ///  Get the Status of all indexes with query string parameters
        /// </summary>
        /// <returns></returns>
        public IStatusResponse Status(StatusParams parameters)
        {
            return this.Status("_all", parameters);
        }
        
        /// <summary>
        /// Get the Status an index
        /// </summary>
        public IStatusResponse Status(string index)
        {
            index.ThrowIfNull("index");
            return this.Status(new[] { index }, new StatusParams());
        }
        /// <summary>
        /// Get the Status an index with query string parameters
        /// </summary>
        public IStatusResponse Status(string index, StatusParams parameters)
        {
            index.ThrowIfNull("index");
            return this.Status(new[] { index }, parameters);
        }

        /// <summary>
        /// Get the Status of multiple indices at once.
        /// </summary>
        public IStatusResponse Status(IEnumerable<string> indices)
        {
            indices.ThrowIfNull("indices");
            string path = this.PathResolver.CreateIndexPath(indices, "_status");
            return this._Status(path, new StatusParams());
        }
        /// <summary>
        /// Get the Status of multiple indices at once with query string parameters
        /// </summary>
        public IStatusResponse Status(IEnumerable<string> indices, StatusParams parameters)
        {
            indices.ThrowIfNull("indices");
            string path = this.PathResolver.CreateIndexPath(indices, "_status");
            return this._Status(path, parameters);
        }
        
        /// <summary>
        /// refresh the connection settings default index for type T
        /// </summary>
        public IStatusResponse Status<T>() where T : class
        {
            var index = this.Infer.IndexName<T>();
            index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

            return Status(index, new StatusParams());
        }
        /// <summary>
        /// refresh the connection settings default index for type T with query string parameters
        /// </summary>
        public IStatusResponse Status<T>(StatusParams parameters) where T : class
        {
            var index = this.Infer.IndexName<T>();
            index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

            return Status(index, parameters);
        }


        private IStatusResponse _Status(string path, StatusParams parameters)
        {
            var pathWithParameters = _BuildStatusUrl(path, parameters);
            var status = this.Connection.GetSync(pathWithParameters);
            var r = this.Deserialize<StatusResponse>(status);
            return r;
        }

        private string _BuildStatusUrl(string path, StatusParams parameters)
        {
            path.ThrowIfNullOrEmpty("path");

            if (parameters == null)
                return path;

            var queryParameterStrings = new List<string>();
            if (parameters.Recovery)
                queryParameterStrings.Add("recovery=true");

            if (parameters.Snapshot)
                queryParameterStrings.Add("snapshot=true");

            if (queryParameterStrings.Count > 0)
            {
                return path + "?" + string.Join("&", queryParameterStrings);
            }
            return path;
        }
    }
}
