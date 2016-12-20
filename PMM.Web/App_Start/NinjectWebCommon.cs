[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PMM.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PMM.Web.App_Start.NinjectWebCommon), "Stop")]

namespace PMM.Web.App_Start
{
    using System;
    using System.Web;
    using PMM.Core;
    using PMM.Core.Data;
    using PMM.Data;
    using PMM.Service;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbContext>().To<IocDbContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind<IWebHelper>().To<WebHelper>();
            kernel.Bind<IAuthenticationService>().To<FormsAuthenticationService>();
            kernel.Bind<IWorkContext>().To<WorkContextService>();
            kernel.Bind<ICityService>().To<CityService>();
            kernel.Bind<IMandalService>().To<MandalService>();
            kernel.Bind<IYagnaSevaDetailService>().To<YagnaSevaDetailService>();
            kernel.Bind<IUserDetailService>().To<UserDetailService>();
            kernel.Bind<ISevaGradeService>().To<SevaGradeService>();
            kernel.Bind<ISevaTypeService>().To<SevaTypeService>();
            kernel.Bind<IReportService>().To<ReportService>();
            
        }
    }
}