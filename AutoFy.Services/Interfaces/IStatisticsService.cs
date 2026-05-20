using AutoFy.Services.DTOs;

namespace AutoFy.Services.Interfaces;

public interface IStatisticsService
{
    Task<StatisticsSummaryDto> GetStatisticsAsync();
}