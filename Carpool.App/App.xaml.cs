using Carpool.App.Extensions;
using Carpool.App.Services;
using Carpool.App.ViewModels;
using Carpool.App.ViewModels.Interfaces;
using Carpool.App.Factories;
using Carpool.App;
using Carpool.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using Carpool.App.Views;
using Carpool.BL;
using Carpool.BL.Facade;
using Carpool.BL.Models;
using Carpool.DAL.Factories;
using Carpool.DAL.UnitOfWork;
using Microsoft.Extensions.Options;
using AutoMapper.Mappers;
using AutoMapper;

namespace Carpool.App
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("cs");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs");

            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.AddJsonFile(@"appsettings.json", false, true);
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddBLServices();

            services.AddSingleton<IProfileViewModel, ProfileViewModel>();
            services.AddSingleton<ILoginViewModel, LoginViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IMyRidesViewModel, MyRidesViewModel>();
            services.AddSingleton<IHomePageViewModel, HomePageViewModel>();
            services.AddSingleton<IEditCarViewModel, EditCarViewModel>();
            services.AddSingleton<IJoinRideViewModel, JoinRideViewModel >();
            services.AddSingleton<IEditRideViewModel, EditRideViewModel>();
            services.AddSingleton<IAddCarViewModel, AddCarViewModel>();

            services.AddSingleton<IMediator, Mediator>();

            services.AddSingleton<CarFacade>();
            services.AddSingleton<UserFacade>();
            services.AddSingleton<RideFacade>();
            services.AddSingleton<PassengerFacade>();

            services.AddSingleton<MainWindow>();

            services.AddSingleton<IDbContextFactory<CarpoolDbContext>>(provider => new CarpoolDbContextFactory(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();


        }


        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var dbContextFactory = _host.Services.GetRequiredService<IDbContextFactory<CarpoolDbContext>>();


            await using (var dbx = await dbContextFactory.CreateDbContextAsync())
            {
                    await dbx.Database.MigrateAsync();
            }



            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }

}