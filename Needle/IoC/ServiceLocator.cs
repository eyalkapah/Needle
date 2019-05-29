using System;
using Microsoft.Extensions.DependencyInjection;

namespace Needle.IoC
{
    public class ServiceLocator : IDisposable
    {
        private static ServiceLocator _instance;
        private static IServiceScope _serviceScope;

        public static ServiceLocator Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = new ServiceLocator();
                return _instance;
            }
        }

        private ServiceLocator()
        {
        }

        public static void Init(IServiceProvider provider)
        {
            _serviceScope = provider.CreateScope();
        }

        public T GetService<T>() where T : class
        {
            return _serviceScope.ServiceProvider.GetService<T>();
        }

        public object GetService(Type viewModel)
        {
            return _serviceScope.ServiceProvider.GetService(viewModel);
        }


        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _serviceScope?.Dispose();
            }
        }

        

        #endregion
    }
}
