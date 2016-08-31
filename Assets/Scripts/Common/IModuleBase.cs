namespace Assets.Scripts.Common
{
    //提供给ModuleManager(目前是GameApp)管理Module单例的一个桥梁
    public interface IModuleBase
    {
        void Update(float lastFrameTookSecond);

        void RegisterLogicEvent();
    }
}