using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		public IUpdateResponse Update<T>(Action<UpdateDescriptor<T, T>> updateSelector) where T : class
		{
			return this.Update<T, T>(updateSelector);
		}
		public IUpdateResponse Update<T, K>(Action<UpdateDescriptor<T, K>> updateSelector) 
			where T : class
			where K : class
		{
			var updateDescriptor = new UpdateDescriptor<T, K>();
			updateSelector(updateDescriptor);
			//var data = JsonConvert.SerializeObject(updateDescriptor, Formatting.Indented, IndexSerializationSettings);
			var data = this.Serialize(updateDescriptor);
			var path = this.CreateUpdatePath<T, K>(updateDescriptor);
			return this._Update(path, data);
		}
        private string CreateUpdatePath<T, K>(UpdateDescriptor<T, K> s)
            where T : class
            where K : class
        {
            var index = s._Index ?? this.IndexNameResolver.GetIndexForType<T>();
            var type = s._Type != null ? s._Type.Resolve(this._connectionSettings) : this.GetTypeNameFor<T>();
            var id = s._Id ?? this.IdResolver.GetIdFor(s._Object);

            index.ThrowIfNullOrEmpty("index");
            type.ThrowIfNullOrEmpty("type");
            id.ThrowIfNullOrEmpty("id");

            var querystringPairs = new List<string>();

            if (s._Consistency.HasValue)
                querystringPairs.Add("consistency=" + Enum.GetName(typeof(Consistency), s._Consistency.Value));
            if (s._Replication.HasValue)
                querystringPairs.Add("replication=" + Enum.GetName(typeof (Replication), s._Replication.Value));
            if (s._Refresh.HasValue)
                querystringPairs.Add("refresh=" + s._Refresh.Value.ToString().ToLower());
            if (s._Timeout.HasValue)
                querystringPairs.Add("timeout=" + s._Timeout);
            if (!string.IsNullOrWhiteSpace(s._Percolate))
                querystringPairs.Add("percolate=" + s._Percolate);
            if (!string.IsNullOrWhiteSpace(s._Parent))
                querystringPairs.Add("parent=" + s._Parent);
            if (!string.IsNullOrWhiteSpace(s._Routing))
                querystringPairs.Add("routing=" + s._Routing);
            if (s._RetriesOnConflict.HasValue)
                querystringPairs.Add("retries_on_conflict=" + s._RetriesOnConflict);

            return this.PathResolver.CreateIndexTypeIdPath(index, type, id, "_update") + "/?" + string.Join("&", querystringPairs);
		}


		private UpdateResponse _Update(string path, string data)
		{
			var status = this.Connection.PostSync(path, data);
			var r = this.ToParsedResponse<UpdateResponse>(status);
			return r;
		}

	}
}
