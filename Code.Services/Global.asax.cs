using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Web;
using Code.ReleaseServices;
using Code.ReleaseServices.Core.Configuration;
using Code.ReleaseServices.Core.Services;
using Code.ReleaseServices.Windows;

namespace Code.Services
{
    public class Global : System.Web.HttpApplication, IContainerProviderAccessor
    {
        ///<summary>
        ///Provider that holds the application container. 
        ///</summary>
        private static IContainerProvider _containerProvider;

        ///<summary>
        ///Instance property that will be used by Autofac HttpModules to resolve and inject dependencies.
        ///</summary>
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        protected virtual void Application_Start(Object sender, EventArgs e)
        {
            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();

            builder.RegisterType<EnvironmentFeedsConfiguration>().As<IFeedsConfiguration>().InstancePerHttpRequest();
            builder.RegisterType<FeedLocator>().As<IFeedLocator>().InstancePerHttpRequest();
            builder.RegisterType<ReleaseService>().As<IReleaseService>().InstancePerHttpRequest();
            builder.RegisterType<JiraService>().As<IJiraService>().InstancePerHttpRequest();
            builder.RegisterType<EnvironmentJiraConfiguration>().As<IJiraConfiguration>().InstancePerHttpRequest();

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(builder.Build());
        }

        protected virtual void Session_Start(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_BeginRequest(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_EndRequest(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_Error(Object sender, EventArgs e)
        {
        }

        protected virtual void Session_End(Object sender, EventArgs e)
        {
        }

        protected virtual void Application_End(Object sender, EventArgs e)
        {
        }
    }
}