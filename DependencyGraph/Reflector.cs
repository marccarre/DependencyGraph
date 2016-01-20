using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyGraph
{
    public class Reflector
    {
        private Assembly assembly;

        public Reflector(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public DirectedGraph<Type> GetDependencyGraph()
        {
            DirectedGraph<Type> graph = new DirectedGraph<Type>();
            foreach (Type x in assembly.GetTypes())
            {
                foreach (Type y in GetDependentClasses(x))
                {
                    graph.AddEdge(y, x);
                }
            }
            return graph;
        }

        public ISet<Type> GetDependentClasses(Type type)
        {
            HashSet<Type> dependentClasses = new HashSet<Type>();
            dependentClasses.UnionWith(GetDependentClassesFromConstructor(type));
            dependentClasses.UnionWith(GetDependentClassesFromInheritance(type));
            dependentClasses.UnionWith(GetDependentClassesFromInterface(type));
            dependentClasses.UnionWith(GetDependentClassesFromMethods(type));
            dependentClasses.UnionWith(GetDependentClassesFromMembers(type));
            dependentClasses.UnionWith(GetDependentClassesFromTemporaryVariable(type));
            return dependentClasses;
        }

        public IEnumerable<Type> GetDependentClassesFromConstructor(Type type)
        {
            return from t in assembly.GetTypes()
                   from c in t.GetConstructors()
                   from pi in c.GetParameters()
                   where (t != type) && (pi.ParameterType == type)
                   select t;
        }

        public IEnumerable<Type> GetDependentClassesFromInheritance(Type type)
        {
            return from t in assembly.GetTypes()
                   where (t != type) && (t.BaseType == type)
                   select t;
        }

        public IEnumerable<Type> GetDependentClassesFromInterface(Type type)
        {
            return from t in assembly.GetTypes()
                   where (t != type) && type.IsAssignableFrom(t)
                   select t;
        }

        public IEnumerable<Type> GetDependentClassesFromMethods(Type type)
        {
            return from t in assembly.GetTypes()
                   from m in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                   from pi in m.GetParameters()
                   where (t != type) && (pi.ParameterType == type)
                   select t;
        }

        public IEnumerable<Type> GetDependentClassesFromMembers(Type type)
        {
            return from t in assembly.GetTypes()
                   from f in t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                   where (t != type) && (f.FieldType == type)
                   select t;
        }

        public IEnumerable<Type> GetDependentClassesFromTemporaryVariable(Type type)
        {
            return from t in assembly.GetTypes()
                   from m in t.GetMethods()
                   where (m.GetMethodBody() != null)
                   from lv in m.GetMethodBody().LocalVariables
                   where (t != type) && (lv.LocalType == type)
                   select t;
        }
    }
}
