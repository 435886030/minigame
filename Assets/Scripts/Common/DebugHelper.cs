//日志辅助
namespace Assets.Scripts.Common
{
    public class DebugHelper
    {
        public static void Assert(bool condition)
        {
            UnityEngine.Assertions.Assert.IsTrue(condition);
        }

        public static void Assert(bool condition,string message)
        {
            UnityEngine.Assertions.Assert.IsTrue(condition,message);
        }

        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}
