using System.Collections.Generic;
using System.Linq;
using Nest.Domain;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class MultiGetResponse : BaseResponse
	{
		public MultiGetResponse()
		{
			this._Documents = new List<IMultiGetHit<object>>();
		}

		internal ICollection<IMultiGetHit<object>> _Documents { get; set; }

		public IEnumerable<IMultiGetHit<object>> Documents { get { return this._Documents.ToList(); } }


		public MultiGetHit<T> GetWithMetaData<T>(string id) where T : class
		{
			return this.Documents.OfType<MultiGetHit<T>>().FirstOrDefault(m => m.Id == id);
		}
		public MultiGetHit<T> GetWithMetaData<T>(int id) where T : class
		{
			return this.GetWithMetaData<T>(id.ToString());
		}
		public T Get<T>(string id) where T : class
		{
			var multiHit = this.GetWithMetaData<T>(id);
			if (multiHit == null)
				return null;
			return multiHit.Source;
		}
		public T Get<T>(int id) where T : class
		{
			return this.Get<T>(id.ToString());
		}
		public FieldSelection<T> GetFieldSelection<T>(string id) where T : class
		{
			var multiHit = this.GetWithMetaData<T>(id);
			if (multiHit == null)
				return null;
			return multiHit.FieldSelection;
		}
		public FieldSelection<T> GetFieldSelection<T>(int id) where T : class
		{
			return this.GetFieldSelection<T>(id.ToString());
		}
	}
}
