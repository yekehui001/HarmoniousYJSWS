using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmoniousYJSWS
{
    abstract class Singleton<T> where T : Singleton<T>
    {
        protected Singleton() { }
        public static T Instance { get; private set; }
        static Singleton()
        {
            Instance = Activator.CreateInstance<T>();
        }
    }
}
