using AutoFy.Mobile.Views;
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
    }

    private void BuildShell()
    {
        Items.Add(CreateFlyoutItem("Начало", nameof(HomeView), _serviceProvider.GetRequiredService<HomeView>));
        Items.Add(CreateFlyoutItem("Календар", nameof(CalendarView), _serviceProvider.GetRequiredService<CalendarView>));
        Items.Add(CreateFlyoutItem("Статистика", nameof(StatisticsView), _serviceProvider.GetRequiredService<StatisticsView>));
        Items.Add(CreateFlyoutItem("История", nameof(HistoryView), _serviceProvider.GetRequiredService<HistoryView>));
        Items.Add(CreateFlyoutItem("Настройки", nameof(SettingsView), _serviceProvider.GetRequiredService<SettingsView>));
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