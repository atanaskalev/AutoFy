using AutoFy.Services.DTOs;

namespace AutoFy.Services.Interfaces;

public interface IHistoryService
{
    Task<IEnumerable<HistoryItemDto>> GetHistoryAsync(
        int? vehicleId,
        DateTime? date,
        string historyType);
}