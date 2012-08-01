using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Reflection;

namespace Nest.Resolvers
{
	public class IdResolver
	{
    private static ConcurrentDictionary<Type, Func<object, string>> IdDelegates = new ConcurrentDictionary<Type, Func<object, string>>();

    internal Func<T, string> CreateIdSelector<T>() where T : class
    {
      //TODO this idselection stuff for the bulk seems obsolete.
      Func<T, string> idSelector = (@object) => this.GetIdFor(@object);
      return idSelector;
    }

    internal static Func<T, object> MakeDelegate<T, U>(MethodInfo @get)
    {
      var f = (Func<T, U>)Delegate.CreateDelegate(typeof(Func<T, U>), @get);
      return t => f(t);
    }

    public string GetIdFor<T>(T @object)
    {
      var type = typeof(T);
      Func<object, string> cachedLookup;
      if (IdDelegates.TryGetValue(type, out cachedLookup))
        return cachedLookup(@object);

      var esTypeAtt = new PropertyNameResolver().GetElasticPropertyFor(type);
      var propertyName = (esTypeAtt != null) ? esTypeAtt.IdProperty : string.Empty;
      if (string.IsNullOrWhiteSpace(propertyName))
        propertyName = "Id";

      var idProperty = type.GetProperty(propertyName);
      if (idProperty == null)
      {
        throw new Exception("Could not infer id for object of type" + type.FullName);
      }
      try
      {
        var getMethod = idProperty.GetGetMethod();
        var method = typeof(ElasticClient).GetMethod("MakeDelegate", BindingFlags.Static | BindingFlags.NonPublic);
        var generic = method.MakeGenericMethod(type, getMethod.ReturnType);
        Func<T, object> func = (Func<T, object>)generic.Invoke(null, new[] { getMethod });
        cachedLookup = o =>
        {
          T obj = (T)o;
          var v = func(obj);
          return v.ToString();
        };
        IdDelegates.TryAdd(type, cachedLookup);
        return cachedLookup(@object);

      }
      catch (Exception e)
      {
        var value = idProperty.GetValue(@object, null);
        return value.ToString();
      }
    }
	}
}
