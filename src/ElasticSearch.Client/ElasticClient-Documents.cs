using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
    public partial class ElasticClient
    {
        public void Delete<T>(int id) where T : class
        {
            this.Delete<T>(id.ToString());
        }

        public void Delete<T>(string id) where T : class
        {
            var index = this.Settings.DefaultIndex;
            index.ThrowIfNullOrEmpty("Cannot infer default index for current connection.");

            var typeName = this.InferTypeName<T>();

            this.Delete<T>(id, this.createPath(index, typeName));
        }

        public void Delete<T>(string index, string type, string id) where T : class
        {
            this.Delete<T>(id, index + "/" + type + "/");
        }

        public void Delete<T>(string index, string type, int id) where T : class
        {
            this.Delete<T>(id.ToString(), index + "/" + type + "/");
        }

        public void Delete<T>(string id, string path) where T : class
        {
            this.Connection.DeleteSync(path + id);
        }
    }
}
