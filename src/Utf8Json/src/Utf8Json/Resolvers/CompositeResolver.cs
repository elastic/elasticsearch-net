using System;
using System.Reflection;
using System.Reflection.Emit;
using Utf8Json.Internal.Emit;

namespace Utf8Json.Resolvers
{
    public sealed class CompositeResolver : IJsonFormatterResolver
    {
        public static readonly CompositeResolver Instance = new CompositeResolver();

        static bool isFreezed = false;
        static IJsonFormatter[] formatters = new IJsonFormatter[0];
        static IJsonFormatterResolver[] resolvers = new IJsonFormatterResolver[0];

        CompositeResolver()
        {
        }

        public static void Register(params IJsonFormatterResolver[] resolvers)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.resolvers = resolvers;
        }

        public static void Register(params IJsonFormatter[] formatters)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.formatters = formatters;
        }

        public static void Register(IJsonFormatter[] formatters, IJsonFormatterResolver[] resolvers)
        {
            if (isFreezed)
            {
                throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
            }

            CompositeResolver.resolvers = resolvers;
            CompositeResolver.formatters = formatters;
        }

        public static void RegisterAndSetAsDefault(params IJsonFormatterResolver[] resolvers)
        {
            Register(resolvers);
            JsonSerializer.SetDefaultResolver(CompositeResolver.Instance);
        }

        public static void RegisterAndSetAsDefault(params IJsonFormatter[] formatters)
        {
            Register(formatters);
            JsonSerializer.SetDefaultResolver(CompositeResolver.Instance);
        }

        public static void RegisterAndSetAsDefault(IJsonFormatter[] formatters, IJsonFormatterResolver[] resolvers)
        {
            Register(formatters);
            Register(resolvers);
            JsonSerializer.SetDefaultResolver(CompositeResolver.Instance);
        }

        public static IJsonFormatterResolver Create(params IJsonFormatter[] formatters)
        {
            return Create(formatters, new IJsonFormatterResolver[0]);
        }

        public static IJsonFormatterResolver Create(params IJsonFormatterResolver[] resolvers)
        {
            return Create(new IJsonFormatter[0], resolvers);
        }

        public static IJsonFormatterResolver Create(IJsonFormatter[] formatters, IJsonFormatterResolver[] resolvers)
        {
            return DynamicCompositeResolver.Create(formatters, resolvers);
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                isFreezed = true;

                foreach (var item in formatters)
                {
                    foreach (var implInterface in item.GetType().GetTypeInfo().ImplementedInterfaces)
                    {
                        var ti = implInterface.GetTypeInfo();
                        if (ti.IsGenericType && ti.GenericTypeArguments[0] == typeof(T))
                        {
                            formatter = (IJsonFormatter<T>)item;
                            return;
                        }
                    }
                }

                foreach (var item in resolvers)
                {
                    var f = item.GetFormatter<T>();
                    if (f != null)
                    {
                        formatter = f;
                        return;
                    }
                }
            }
        }
    }

    public abstract class DynamicCompositeResolver : IJsonFormatterResolver
    {
        const string ModuleName = "Utf8Json.Resolvers.DynamicCompositeResolver";

        static readonly DynamicAssembly assembly;

        static DynamicCompositeResolver()
        {
            assembly = new DynamicAssembly(ModuleName);
        }

#if DEBUG && (NET45 || NET47)
        public static AssemblyBuilder Save()
        {
            return assembly.Save();
        }
#endif

        public static IJsonFormatterResolver Create(IJsonFormatter[] formatters, IJsonFormatterResolver[] resolvers)
        {
            var id = Guid.NewGuid().ToString().Replace("-", "");
            var resolverType = assembly.DefineType("DynamicCompositeResolver_" + id, TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed, typeof(DynamicCompositeResolver));
            var cacheType = assembly.DefineType("DynamicCompositeResolverCache_" + id, TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed, null);
            var genericP = cacheType.DefineGenericParameters("T")[0];

            var resolverInstanceField = resolverType.DefineField("instance", resolverType, FieldAttributes.Public | FieldAttributes.Static);

            var f = cacheType.DefineField("formatter", typeof(IJsonFormatter<>).MakeGenericType(genericP), FieldAttributes.Static | FieldAttributes.Public);
            {
                var cctor = cacheType.DefineConstructor(MethodAttributes.Static, CallingConventions.Standard, Type.EmptyTypes);
                var il = cctor.GetILGenerator();
                il.EmitLdsfld(resolverInstanceField);
                il.EmitCall(typeof(DynamicCompositeResolver).GetMethod("GetFormatterLoop").MakeGenericMethod(genericP));
                il.Emit(OpCodes.Stsfld, f);
                il.Emit(OpCodes.Ret);
            }
            var cacheTypeT = cacheType.CreateTypeInfo().AsType();

            {
                var ctor = resolverType.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new[] { typeof(IJsonFormatter[]), typeof(IJsonFormatterResolver[]) });
                var il = ctor.GetILGenerator();
                il.EmitLdarg(0);
                il.EmitLdarg(1);
                il.EmitLdarg(2);
                il.Emit(OpCodes.Call, typeof(DynamicCompositeResolver).GetConstructors()[0]);
                il.Emit(OpCodes.Ret);
            }
            {
                var m = resolverType.DefineMethod("GetFormatter", MethodAttributes.Public | MethodAttributes.Virtual);

                var gpp = m.DefineGenericParameters("T")[0];
                m.SetReturnType(typeof(IJsonFormatter<>).MakeGenericType(gpp));

                var il = m.GetILGenerator();
                var formatterField = TypeBuilder.GetField(cacheTypeT.MakeGenericType(gpp), cacheTypeT.GetField("formatter", BindingFlags.Public | BindingFlags.Static | BindingFlags.GetField));
                il.EmitLdsfld(formatterField);
                il.Emit(OpCodes.Ret);
            }

            var resolverT = resolverType.CreateTypeInfo().AsType();
            var instance = Activator.CreateInstance(resolverT, new object[] { formatters, resolvers });
            var finfo = instance.GetType().GetField("instance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            finfo.SetValue(null, instance);

            return (IJsonFormatterResolver)instance;
        }

        public readonly IJsonFormatter[] formatters;
        public readonly IJsonFormatterResolver[] resolvers;

        public DynamicCompositeResolver(IJsonFormatter[] formatters, IJsonFormatterResolver[] resolvers)
        {
            this.formatters = formatters;
            this.resolvers = resolvers;
        }

        public IJsonFormatter<T> GetFormatterLoop<T>()
        {
            foreach (var item in formatters)
            {
                foreach (var implInterface in item.GetType().GetTypeInfo().ImplementedInterfaces)
                {
                    var ti = implInterface.GetTypeInfo();
                    if (ti.IsGenericType && ti.GenericTypeArguments[0] == typeof(T))
                    {
                        return (IJsonFormatter<T>)(object)item;
                    }
                }
            }

            foreach (var item in resolvers)
            {
                var f = item.GetFormatter<T>();
                if (f != null)
                {
                    return f;
                }
            }

            return null;
        }

        public abstract IJsonFormatter<T> GetFormatter<T>();
    }
}