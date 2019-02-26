using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Utf8Json.Internal.Emit
{
    public class DynamicAssembly
    {
#if NET45 || NET47
        readonly string moduleName;
#endif
        readonly AssemblyBuilder assemblyBuilder;
        readonly ModuleBuilder moduleBuilder;

        // don't expose ModuleBuilder
        // public ModuleBuilder ModuleBuilder { get { return moduleBuilder; } }

        readonly object gate = new object();

        // requires lock on mono environment. see: https://github.com/neuecc/MessagePack-CSharp/issues/161

        public TypeBuilder DefineType(string name, TypeAttributes attr)
        {
            lock (gate)
            {
                return moduleBuilder.DefineType(name, attr);
            }
        }

        public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent)
        {
            lock (gate)
            {
                return moduleBuilder.DefineType(name, attr, parent);
            }
        }

        public TypeBuilder DefineType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
        {
            lock (gate)
            {
                return moduleBuilder.DefineType(name, attr, parent, interfaces);
            }
        }

        public DynamicAssembly(string moduleName)
        {
#if NET45 || NET47
            this.moduleName = moduleName;
            this.assemblyBuilder = System.AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(moduleName), AssemblyBuilderAccess.RunAndSave,);
			this.moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName, moduleName + ".dll");
#else
#if NETSTANDARD
            this.assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(moduleName), AssemblyBuilderAccess.Run);
#else
            this.assemblyBuilder = System.AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(moduleName), AssemblyBuilderAccess.Run);
#endif

            this.moduleBuilder = assemblyBuilder.DefineDynamicModule(moduleName);
#endif
        }

#if NET45 || NET47

        public AssemblyBuilder Save()
        {
            assemblyBuilder.Save(moduleName + ".dll");
            return assemblyBuilder;
        }

#endif
    }
}
