using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTrunfo
{
    class Container
    {
        private static readonly Dictionary<Type, Object> instances = new Dictionary<Type, object>();

        static Container() {

            Configuration.configure();
        }

        public static T get<T>() {
            return (T) instances[typeof(T)];
        }

        public static void set(Object instance){
            instances[instance.GetType()] = instance;
        }

    }
}
