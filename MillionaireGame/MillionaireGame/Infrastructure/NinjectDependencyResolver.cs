using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MillionaireGame.BLL;
using MillionaireGame.DAL.Repository;
using Ninject;

namespace MillionaireGame.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            _kernel.Bind<IQuestionRepository>().To<QuestionRepository>();
            _kernel.Bind<IGameService>().To<GameService>();
        }
    }
}