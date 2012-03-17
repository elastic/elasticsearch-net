using System;
using System.Linq;
using System.Linq.Expressions;
using Nest.DSL;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nest
{
    public partial class ElasticClient
    {
				public QueryResponse<dynamic> Search(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> searcher)
				{
					var search = new SearchDescriptor<dynamic>();
					var descriptor = searcher(search);
					var path = this.GetPathForDynamic(descriptor);
					var query = ElasticClient.Serialize(descriptor);

					ConnectionStatus status = this.Connection.PostSync(path, query);
					var r = this.ToParsedResponse<QueryResponse<dynamic>>(status);
					return r;
				}
        public QueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class
        {
					var search = new SearchDescriptor<T>();
					var descriptor = searcher(search);

					var query = ElasticClient.Serialize(descriptor);
					var path = this.GetPathForTyped(descriptor);
					ConnectionStatus status = this.Connection.PostSync(path, query);
					var r = this.ToParsedResponse<QueryResponse<T>>(status);
					return r;
        }
				
				[Obsolete("string queries are super nasty!", false)]
				public QueryResponse<T> Search<T>(string query) where T : class
				{
					var descriptor = new SearchDescriptor<T>();
					var path = this.GetPathForTyped(descriptor);
					ConnectionStatus status = this.Connection.PostSync(path, query);
					var r = this.ToParsedResponse<QueryResponse<T>>(status);
					return r;
				}

				private string GetPathForDynamic(SearchDescriptor<dynamic> descriptor)
				{
					var indices = this.Settings.DefaultIndex;
					if (descriptor._Indices.HasAny())
						indices = string.Join(",", descriptor._Indices);
					else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
						indices = "_all";

					string types = (descriptor._Types.HasAny()) ? string.Join(",", descriptor._Types) : null;

					return this.PathJoin(indices, types, descriptor._Routing);
				}
				private string GetPathForTyped<T>(SearchDescriptor<T> descriptor) where T : class
				{
						var indices = this.Settings.DefaultIndex;
					if (descriptor._Indices.HasAny())
						indices = string.Join(",", descriptor._Indices);
					else if (descriptor._Indices != null || descriptor._AllIndices) //if set to empty array asume all
						indices = "_all";

					var types = this.InferTypeName<T>();
					if (descriptor._Types.HasAny())
						types = string.Join(",", descriptor._Types);
					else if (descriptor._Types != null || descriptor._AllTypes) //if set to empty array assume all
						types = null;

					return this.PathJoin(indices, types, descriptor._Routing);
				}
				private string PathJoin(string indices, string types, string routing)
				{
					string path = string.Concat(!string.IsNullOrEmpty(types) ?
													 this.CreatePath(indices, types) :
													 this.CreatePath(indices), "_search");
					if (!String.IsNullOrEmpty(routing))
					{
						path = "{0}?routing={1}".F(path, routing);
					}
					return path;
				}
    }
}