using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	internal static class Obsolete
	{
		public static Func<TUp, TUp> UpCastSelector<TDown, TUp>(Func<TDown, TDown> oldSelector)
			where TDown : IRequestParameters, TUp, new()
			where TUp : IRequestParameters, new()
		{
			if (oldSelector == null) return null;
			return (s) => oldSelector(DownCastDescriptor<TDown, TUp>(s));
		}

		public static TDown DownCastDescriptor<TDown, TUp>(TUp instance)
			where TDown : IRequestParameters, new()
			where TUp : IRequestParameters, new()
		{
			return new TDown()
			{
				RequestConfiguration = instance.RequestConfiguration,
				DeserializationState = instance.DeserializationState,
				QueryString = instance.QueryString
			};
		}
	}
}