using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Domain;
using Newtonsoft.Json;

namespace Nest
{
	public interface IMultiGetResponse
	{
		IEnumerable<IMultiGetHit<object>> Documents { get; }
		MultiGetHit<T> Get<T>(string id) where T : class;
		MultiGetHit<T> Get<T>(int id) where T : class;
		T Source<T>(string id) where T : class;
		T Source<T>(int id) where T : class;
		FieldSelection<T> GetFieldSelection<T>(string id) where T : class;
		FieldSelection<T> GetFieldSelection<T>(int id) where T : class;
	}

	[JsonObject]
	public class MultiGetResponse : BaseResponse, IMultiGetResponse
	{
		public MultiGetResponse()
		{
			this._Documents = new List<IMultiGetHit<object>>();
		}

		internal ICollection<IMultiGetHit<object>> _Documents { get; set; }

		public IEnumerable<IMultiGetHit<object>> Documents { get { return this._Documents.ToList(); } }


		public MultiGetHit<T> Get<T>(string id) where T : class
		{
			return this.Documents.OfType<MultiGetHit<T>>().FirstOrDefault(m => m.Id == id);
		}
		public MultiGetHit<T> Get<T>(int id) where T : class
		{
			return this.Get<T>(id.ToString(CultureInfo.InvariantCulture));
		}
		public T Source<T>(string id) where T : class
		{
			var multiHit = this.Get<T>(id);
			if (multiHit == null)
				return null;
			return multiHit.Source;
		}
		public T Source<T>(int id) where T : class
		{
			return this.Source<T>(id.ToString(CultureInfo.InvariantCulture));
		}
		public FieldSelection<T> GetFieldSelection<T>(string id) where T : class
		{
			var multiHit = this.Get<T>(id);
			if (multiHit == null)
				return null;
			return multiHit.FieldSelection;
		}
		public FieldSelection<T> GetFieldSelection<T>(int id) where T : class
		{
			return this.GetFieldSelection<T>(id.ToString(CultureInfo.InvariantCulture));
		}
	}
}
