using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class StatisticsViewModel : BaseViewModel
{
    #region Fields

    private readonly IStatisticsService statisticsService;

    private string _totalCostText = "0.00 €";

    #endregion

    #region Init

    public StatisticsViewModel(IStatisticsService statisticsService)
    {
        this.statisticsService = statisticsService;

        Title = "Статистика";

        LoadStatisticsCommand = new Command(async () => await LoadStatisticsAsync());
    }

    #endregion

    #region Properties

    public string TotalCostText
    {
        get => _totalCostText;
        set => SetProperty(ref _totalCostText, value);
    }

    public ObservableCollection<VehicleStatisticsDto> Vehicles { get; } = new();

    #endregion

    #region Commands

    public ICommand LoadStatisticsCommand { get; }

    #endregion

    #region Methods

    private async Task LoadStatisticsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            Vehicles.Clear();

            var statistics = await statisticsService.GetStatisticsAsync();

            TotalCostText = statistics.TotalCostText;

            foreach (var vehicle in statistics.Vehicles)
                Vehicles.Add(vehicle);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}