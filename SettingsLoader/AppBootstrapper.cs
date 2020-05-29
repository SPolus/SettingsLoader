using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using SettingsLoader.ViewModels;

namespace SettingsLoader
{
    public class AppBootstrapper : BootstrapperBase 
    {
        SimpleContainer _container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ShellViewModel>();

            _container
                .PerRequest<PortSettingsViewModel>()
                .PerRequest<TableViewModel>()
                .PerRequest<ConfigurationViewModel>()
                .PerRequest<HelpViewModel>()
                .PerRequest<AboutViewModel>();

            //GetType().Assembly.GetTypes()
            //    .Where(type => type.IsClass)
            //    .Where(type => type.Name.EndsWith("ViewModel"))
            //    .ToList()
            //    .ForEach(viewModelType =>
            //    _container.RegisterPerRequest(viewModelType, viewModelType.ToString(), viewModelType));
            
            
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}