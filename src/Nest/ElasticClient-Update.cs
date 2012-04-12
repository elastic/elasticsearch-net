using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Nest
{
	public partial class ElasticClient
	{
    public UpdateResponse Update<T>(Action<UpdateDescriptor<T>> updateSelector) where T : class
		{
			var updateDescriptor = new UpdateDescriptor<T>();
			updateSelector(updateDescriptor);
			var data = ElasticClient.Serialize(updateDescriptor);
      var path = this.CreateUpdatePath<T>(updateDescriptor);
			return this._Update(path, data);
		}
    public UpdateResponse Update(Action<UpdateDescriptor<dynamic>> updateSelector)
    {
      var updateDescriptor = new UpdateDescriptor<dynamic>();
      updateSelector(updateDescriptor);
      var data = ElasticClient.Serialize(updateDescriptor);
      var path = this.CreateUpdatePath<dynamic>(updateDescriptor);
      return this._Update(path, data);
    }
		private string CreateUpdatePath<T>(UpdateDescriptor<T> s) where T : class
		{
      var index = s._Index ?? this.Settings.DefaultIndex;
      var type = s._Type ?? ElasticClient.GetTypeNameFor<T>();
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
