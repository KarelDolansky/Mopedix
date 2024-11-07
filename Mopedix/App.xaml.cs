using Microsoft.Extensions.DependencyInjection;

namespace Mopedix
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var mainPage = _serviceProvider.GetRequiredService<MainPage>();
            MainPage = new NavigationPage(mainPage);
        }
    }
}
