using System;

namespace KIoc
{
    /// <summary>
    /// 對象信息,構造對像使用
    /// </summary>
    public interface IKContainer
    {
        /// <summary>
        /// 添加單例模式
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="TImplementation">實現類</typeparam>
        void AddSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface;
        /// <summary>
        /// 添加單例模式
        /// </summary>
        /// <typeparam name="TImplementation">實現類</typeparam>
        void AddSingleton<TImplementation>() where TImplementation : class;
        /// <summary>
        /// 添加單例
        /// </summary>
        /// <param name="instance">己實例化的對象</param>
        void AddSingleton<TImplementation>(TImplementation instance);
        /// <summary>
        /// 添加瞬時模式
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="TImplementation">實現類</typeparam>
        void AddTransient<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface;
        /// <summary>
        /// 添加瞬時模式
        /// </summary>
        /// <typeparam name="TImplementation">實現類</typeparam>
        void AddTransient<TImplementation>() where TImplementation : class;
        /// <summary>
        /// 獲取實例
        /// </summary>
        /// <typeparam name="T">添加時的KEY類型</typeparam>
        /// <returns>根據添加時的清況來確定返回接口或實例</returns>
        T Resolve<T>() where T : class;
        /// <summary>
        /// 獲取實例,TImplementation為添加時的類型，KEY
        /// </summary>
        /// <typeparam name="TInterface">返回接口類型</typeparam>
        /// <typeparam name="TImplementation">添加時的類型</typeparam>
        /// <returns>接口</returns>
        TInterface Resolve<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface; 
    }
}
