namespace Batcher.Tests.Helpers
{
    using System;
    using System.Reflection;

    internal class AccessHelper : IAccessHelper
    {
        public object Call(object entity, string methodName, params object[] args)
        {
            var mi = entity.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (mi != null)
            {
                try
                {
                    return mi.Invoke(entity, args);
                }
                catch (Exception ex)
                {
                    throw ex.InnerException ?? ex;
                }
            }

            return null;
        }

        public ReturnType Call<ReturnType>(object entity, string methodName, params object[] args)
        {
            var result = this.Call(entity, methodName, args);
            if (result == null)
            {
                return default(ReturnType);
            }

            return (ReturnType)result;
        }

        public object CallGeneric<GenericType>(object entity, string methodName, params object[] args)
        {
            var mi = entity.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            mi = mi.MakeGenericMethod(typeof(GenericType));
            if (mi != null)
            {
                try
                {
                    return mi.Invoke(entity, args);
                }
                catch (Exception ex)
                {
                    throw ex.InnerException ?? ex;
                }
            }

            return null;
        }

        public ReturnType CallGeneric<GenericType, ReturnType>(object entity, string methodName, params object[] args)
        {
            var result = this.CallGeneric<GenericType>(entity, methodName, args);
            if (result == null)
            {
                return default(ReturnType);
            }

            return (ReturnType)result;
        }
    }
}