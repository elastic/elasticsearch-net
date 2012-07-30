using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial class ElasticClient
	{
		public IUpdateResponse Update<T>(Action<UpdateDescriptor<T>> updateSelector) where T : class
		{
			var updateDescriptor = new UpdateDescriptor<T>(this.TypeNameResolver);
			updateSelector(updateDescriptor);
			var data = JsonConvert.SerializeObject(updateDescriptor, Formatting.Indented, IndexSerializationSettings);
			//var data = ElasticClient.Serialize(updateDescriptor);
			var path = this.CreateUpdatePath<T>(updateDescriptor);
			return this._Update(path, data);
		}
		public IUpdateResponse Update(Action<UpdateDescriptor<dynamic>> updateSelector)
		{
			var updateDescriptor = new UpdateDescriptor<dynamic>(this.TypeNameResolver);
			updateSelector(updateDescriptor);
			var data = JsonConvert.SerializeObject(updateDescriptor, Formatting.Indented, IndexSerializationSettings);
			//var data = ElasticClient.Serialize(updateDescriptor);
			var path = this.CreateUpdatePath<dynamic>(updateDescriptor);
			return this._Update(path, data);
		}
		private string CreateUpdatePath<T>(UpdateDescriptor<T> s) where T : class
		{
			var index = s._Index ?? this.Settings.GetIndexForType<T>();
			var type = s._Type ?? this.TypeNameResolver.GetTypeNameFor<T>();
			var id = s._Id ?? this.GetIdFor(s._Object);

			index.ThrowIfNullOrEmpty("index");
			type.ThrowIfNullOrEmpty("type");
			id.ThrowIfNullOrEmpty("id");

			var path = this.CreatePath(index, type, id) + "/_update?";
			if (s._Consistency.HasValue)
				path += "consistency=" + Enum.GetName(typeof(Consistency), s._Consistency.Value);
			if (s._Replication.HasValue)
				path += "replication=" + Enum.GetName(typeof(Replication), s._Replication.Value);
			if (s._Refresh.HasValue)
				path += "refresh=" + s._Refresh.Value.ToString().ToLower();

			if (s._Timeout.HasValue)
				path += "timeout=" + s._Timeout.ToString();
			if (s._Timeout.HasValue)
				path += "timeout=" + s._Timeout.ToString();

			if (!string.IsNullOrWhiteSpace(s._Percolate))
				path += "percolate=" + s._Percolate;

			if (!string.IsNullOrWhiteSpace(s._Parent))
				path += "parent=" + s._Parent;

			if (!string.IsNullOrWhiteSpace(s._Routing))
				path += "routing=" + s._Routing;

			return path;
		}


		private UpdateResponse _Update(string path, string data)
		{
			var status = this.Connection.PostSync(path, data);
			var r = this.ToParsedResponse<UpdateResponse>(status);
			return r;
		}

	}
}
