namespace Batcher.Tests.Helpers
{
    public interface IAccessHelper
    {
        object Call(object entity, string methodName, params object[] args);

        ReturnType Call<ReturnType>(object entity, string methodName, params object[] args);

        object CallGeneric<GenericType>(object entity, string methodName, params object[] args);

        ReturnType CallGeneric<GenericType, ReturnType>(object entity, string methodName, params object[] args);
    }
}