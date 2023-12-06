using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KIoc
{
    public class KContainer : IKContainer
    {
        #region 單例模式
        //private static readonly IKContainer _instance = new KContainer();
        //public static IKContainer Instance => _instance;

        // 使用 Lazy<T> 类来延迟创建单例对象，并确保线程安全，避免了加锁的开销。
        private static readonly Lazy<IKContainer> _lazyInstance = new Lazy<IKContainer>(() => new KContainer());
        public static IKContainer Instance => _lazyInstance.Value;
        private KContainer()
        {
            _SingletonInstance = new Dictionary<Type, object>();
            _transientContainer = new Dictionary<Type, Type>();
            _SingletonContainer = new Dictionary<Type, Type>();
            _allKeys = new List<Type>();
        }
        #endregion
        #region 容器
        /// <summary>
        /// 單例實例化后的容器
        /// </summary>
        private Dictionary<Type, object> _SingletonInstance;
        /// <summary>
        /// 單例容器
        /// </summary>
        private Dictionary<Type, Type> _SingletonContainer;
        /// <summary>
        /// 瞬態容器
        /// </summary>
        private Dictionary<Type, Type> _transientContainer;
        private List<Type> _allKeys;

        #endregion

        public void AddSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            var key = typeof(TInterface);
            //不允許重復註冊，包含單例與瞬態
            if (_allKeys.Contains(key))
            {
                if (HasKeyAndValue(key, typeof(TImplementation))) return;
                else
                    throw new Exception($"{key.FullName} had registered to Container");
            }
            _allKeys.Add(key);
            _SingletonContainer[key] = typeof(TImplementation);
        }

        public void AddSingleton<TImplementation>() where TImplementation : class
        {
            var key = typeof(TImplementation);
            //不允許重復註冊，包含單例與瞬態
            if (_allKeys.Contains(key))
            {
                if (HasKeyAndValue(key, key)) return;
                else
                    throw new Exception($"{key.FullName} had registered to Container");
            }
            _allKeys.Add(key);
            _SingletonContainer[key] = key;
        }

        public void AddSingleton<TImplementation>(TImplementation instance)
        {
            var key = typeof(TImplementation);
            //不允許重復註冊，包含單例與瞬態
            if (_allKeys.Contains(key))
            {
                if (HasKeyAndValue(key, key)) return;
                else
                    throw new Exception($"{key.FullName} had registered to Container");
            }
            _allKeys.Add(key);
            if (instance == null)
                _SingletonContainer[key] = key;
            else
            {
                _SingletonContainer[key] = key;
                _SingletonInstance[key] = instance;
            }
        }

        public void AddTransient<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            var key = typeof(TInterface);
            //不允許重復註冊，包含單例與瞬態
            if (_allKeys.Contains(key))
            {
                if (HasKeyAndValue(key, typeof(TImplementation))) return;
                else
                    throw new Exception($"{key.FullName} had registered to Container");
            }
            _allKeys.Add(key);
            _transientContainer[key] = typeof(TImplementation);
        }

        public void AddTransient<TImplementation>() where TImplementation : class
        {
            var key = typeof(TImplementation);
            //不允許重復註冊，包含單例與瞬態
            if (_allKeys.Contains(key))
            {
                if (HasKeyAndValue(key, key)) return;
                else
                    throw new Exception($"{key.FullName} had registered to Container");
            }

            _allKeys.Add(key);
            _transientContainer[key] = key;
        }

        private bool HasKeyAndValue(Type key, Type value)
        {
            return (_SingletonContainer.ContainsKey(key) && _SingletonContainer[key] == value) || (_transientContainer.ContainsKey(key) && _transientContainer[key] == value);
        }

        public T Resolve<T>() where T : class
        {
            return (T)CreateInstance(typeof(T));
        }

        public TInterface Resolve<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            return (TInterface)CreateInstance(typeof(TImplementation));
        }

        private object CreateInstance(Type typeKey)
        {
            if (!_allKeys.Contains(typeKey))
                throw new Exception($"{typeKey.FullName} has not be registered to Container");
            //先檢查是否為單例
            if (_SingletonInstance.ContainsKey(typeKey))
                return _SingletonInstance[typeKey];//如果有側直接返回
            else
            {//還未創建實例
                var isSingleton = _SingletonContainer.ContainsKey(typeKey);
                //創建實例
                var targetType = isSingleton ? _SingletonContainer[typeKey] : _transientContainer[typeKey];

                //查找構造函數，首先找有定義特性的構造函數，若沒有側找參數最多的構造函數
                var ctors = targetType.GetConstructors();
                var ctor = ctors.FirstOrDefault(c => c.IsDefined(typeof(KCtorInjectionAttribute), true));
                if (ctor is null)
                    ctor = ctors.OrderByDescending(c => c.GetParameters().Length).First();
                //構造參數
                List<object> paramList = new List<object>();
                var ctorParams = ctor.GetParameters();
                foreach (var item in ctorParams)
                {
                    var paramType = item.ParameterType;
                    var paramInstance = CreateInstance(paramType);
                    paramList.Add(paramInstance);
                }
                var result = Activator.CreateInstance(targetType, paramList.ToArray());
                if (isSingleton) _SingletonInstance[typeKey] = result;
                return result;
            }
        }
    }
}
