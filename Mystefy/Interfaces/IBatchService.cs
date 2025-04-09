using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IBatchService
{
     Task<IEnumerable<Batch>> GetAllBatches();
        Task<Batch?> GetBatchById(int id);
        Task<Batch> AddBatch(Batch batch);
        Task<bool> UpdateBatch(int id, Batch batch);
        Task<bool> DeleteBatch(int id);

}
