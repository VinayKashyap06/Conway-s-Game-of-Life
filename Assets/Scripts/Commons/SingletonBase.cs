using UnityEngine;
using System.Collections;

namespace Commons
{
    public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
        private void Awake()
        {
            OnInitalize();
        }
        protected virtual void OnInitalize()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                DontDestroyOnLoad(instance);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }
}