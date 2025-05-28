using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IFinishedProductPackaging
{
    Task<IEnumerable<FinishedProductPackaging>> GetAllFinishedProductPackaging();
    Task<FinishedProductPackaging?> GetFinishedProductPackagingById(int ProductID, int PackagingId);
    Task<FinishedProductPackaging> AddFinishedProductPackaging(FinishedProductPackaging finishedProductPackaging);
    Task<bool> UpdateFinishedProductPackaging(int ProductID, int PackagingId, FinishedProductPackaging finishedProductPackaging);
    Task<bool> DeleteFinishedProductPackaging(int ProductID, int PackagingId);
}
