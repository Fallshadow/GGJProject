using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assc.Utility
{
    public class Singleton<T> where T : Singleton<T>
    {
        private T _instance;
        public T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = System.Activator.CreateInstance<T>();
                    _instance.init();
                }
                return _instance;
            }
        }
        
        protected virtual void init(){}
    }
}
