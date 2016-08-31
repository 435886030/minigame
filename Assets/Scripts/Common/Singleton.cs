//单例基类
namespace Assets.Scripts.Common
{
    /// <summary>
    /// 非MonoBehaviour类型的单件辅助基类，利用C#的语法性质简化单件类的定义和使用
    /// </summary>
    /// <typeparam name="T">
    /// 具体要作为单件的类型
    /// </typeparam>
    public class Singleton<T> where T : class, new()
    {
        /// <summary>
        /// 单件子类实例
        /// </summary>
        private static T instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Singleton{T}"/> class. 
        /// 单件的构造函数，不过由于是单件，所以不允许外部new，构造函数是不对外的.
        /// </summary>
        protected Singleton()
        {
        }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    CreateInstance();
                }

                return instance;
            }

            protected set
            {
                instance = value;
            }
        }

        /// <summary>
        /// The create instance.
        /// </summary>
        /// 创建单件实例
        public static void CreateInstance()
        {
            if (instance == null)
            {
                instance = new T();

                (instance as Singleton<T>).Init();
            }
        }

        /// <summary>
        /// The destroy instance.
        /// </summary>
        /// 删除单件实例
        public static void DestroyInstance()
        {
            if (instance != null)
            {
                (instance as Singleton<T>).UnInit();
                instance = null;
            }
        }

        /// <summary>
        /// The get instance.
        /// </summary>
        /// 返回单件实例
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T GetInstance()
        {
            if (instance == null)
            {
                CreateInstance();
            }

            return instance;
        }

        /// <summary>
        /// The has instance.
        /// </summary>
        /// 是否被实例化
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool HasInstance()
        {
            return instance != null;
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// 初始化
        /// @需要在派生类中实现
        public virtual void Init()
        {
        }

        /// <summary>
        /// The un init.
        /// </summary>
        /// 反初始化
        /// @需要在派生类中实现
        public virtual void UnInit()
        {
        }
    }
}
