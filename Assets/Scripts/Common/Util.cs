//工具类 
using UnityEngine;

namespace Assets.Scripts.Common
{
    public class Util
    {
        public static void DontDestroyOnLoadWrapper(GameObject obj)
        {
            if(obj != null)
            {
                Object.DontDestroyOnLoad(obj);
            }
            else
            {
                DebugHelper.Assert(false);
            }
        }
    }
}
