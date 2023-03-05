using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtobeLib
{
    /// <summary>
    /// サービスロケータークラス
    /// 任意の型を登録して自由に呼び出せるようにする
    /// </summary>
    public static class Locater
    {
        /// <summary>
        /// インスタンスを登録する関数
        /// </summary>
        /// <typeparam name="T">インスタンスの型</typeparam>
        /// <param name="service">登録するインスタンス</param>
        public static void Bind<T>(T service) where T : class { Service<T>.Bind(service); }

        /// <summary>
        /// インスタンスの登録を解除する関数
        /// </summary>
        /// <typeparam name="T">インスタンスの型</typeparam>
        /// <param name="service">登録しているインスタンス</param>
        public static void UnBind<T>(T service) where T : class { Service<T>.UnBind(service); }

        /// <summary>
        /// インスタンスの登録を解除する関数
        /// ※無条件で解除する
        /// </summary>
        /// <typeparam name="T">インスタンスの型</typeparam>
        public static void Clear<T>() where T : class { Service<T>.Clear(); }

        /// <summary>
        /// 登録しているインスタンスを取得する
        /// </summary>
        /// <typeparam name="T">インスタンスの型</typeparam>
        /// <returns>登録しているインスタンス</returns>
        public static T Get<T>() where T : class { return Service<T>.Get(); }
    }

    /// <summary>
    /// サービスロケーターからインスタンスを登録するクラス
    /// ※登録できるクラスは各型に付き、一つまで
    /// </summary>
    /// <typeparam name="T">登録するクラス</typeparam>
    public static class Service<T> where T : class
    {
        //インスタンス
        private static T s_instance = null;

        /// <summary>
        /// インスタンスを登録する関数
        /// </summary>
        /// <param name="service">登録するインスタンス</param>
        public static void Bind(T service) { s_instance = service; }

        /// <summary>
        /// インスタンスの登録を解除する関数
        /// </summary>
        /// <param name="service">登録しているインスタンス</param>
        public static void UnBind(T service)
        {
            //登録しているインスタンスが同じ場合、解除する
            if (s_instance == service) s_instance = null;
        }

        /// <summary>
        /// インスタンスの登録を解除する関数
        /// ※無条件で解除する
        /// </summary>
        public static void Clear(){ s_instance = null; }

        /// <summary>
        /// 登録しているインスタンスを取得する
        /// </summary>
        /// <returns>登録しているインスタンス</returns>
        public static T Get() { return s_instance; }
    }
}
