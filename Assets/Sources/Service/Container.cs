using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperTrunfo
{
    class Container
    {
        private static readonly Dictionary<Type, Object> instances = new Dictionary<Type, object>();
        private static bool isConfigured = false;

        static Container() {

        }

        public static T get<T>() {
            if(!isConfigured){
                Configuration.configure();
            }
            return (T) instances[typeof(T)];
        }

        public static void set(Object instance){
            instances[instance.GetType()] = instance;
        }

    }
}
