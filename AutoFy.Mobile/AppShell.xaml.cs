using AutoFy.Mobile.Views;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using Microsoft.Extensions.DependencyInjection;

namespace AutoFy.Mobile;

public partial class AppShell : Shell
{
    private readonly IServiceProvider _serviceProvider;

    public AppShell(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;

        RegisterRoutes();
        BuildShell();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
#if ANDROID
        StatusBar.SetColor(Colors.White);
        StatusBar.SetStyle(StatusBarStyle.DarkContent);
#endif
    }

    private void RegisterRoutes()
    {
        // Detail / secondary pages
        Routing.RegisterRoute(nameof(AboutView), typeof(AboutView));
        Routing.RegisterRoute(nameof(VehicleDetailsView), typeof(VehicleDetailsView));
        Routing.RegisterRoute(nameof(AddVehicleView), typeof(AddVehicleView));
        Routing.RegisterRoute(nameof(AddFuelEntryView), typeof(AddFuelEntryView));
        Routing.RegisterRoute(nameof(FuelHistoryView), typeof(FuelHistoryView));
        Routing.RegisterRoute(nameof(AddReminderView), typeof(AddReminderView));
        Routing.RegisterRoute(nameof(AddServiceRecordView), typeof(AddServiceRecordView));
        Routing.RegisterRoute(nameof(ServiceHistoryView), typeof(ServiceHistoryView));
    }

    private void BuildShell()
    {
        Items.Add(CreateFlyoutItem("Начало", nameof(HomeView), _serviceProvider.GetRequiredService<HomeView>));
        Items.Add(CreateFlyoutItem("Календар", nameof(CalendarView), _serviceProvider.GetRequiredService<CalendarView>));
        Items.Add(CreateFlyoutItem("Статистика", nameof(StatisticsView), _serviceProvider.GetRequiredService<StatisticsView>));
        Items.Add(CreateFlyoutItem("История", nameof(HistoryView), _serviceProvider.GetRequiredService<HistoryView>));
    }

    private static FlyoutItem CreateFlyoutItem<TView>(
        string title,
        string route,
        Func<TView> viewFactory)
        where TView : ContentPage
    {
        return new FlyoutItem
        {
            Title = title,
            Items =
            {
                new ShellContent
                {
                    Title = title,
                    Route = route,
                    ContentTemplate = new DataTemplate(() => viewFactory())
                }
            }
        };
    }
}