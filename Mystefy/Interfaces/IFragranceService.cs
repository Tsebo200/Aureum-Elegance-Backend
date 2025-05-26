using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IFragranceService
{
    Task<IEnumerable<Fragrance>> GetAllFragrances();
        Task<Fragrance?> GetFragranceById(int id);
        Task<Fragrance> AddFragrance(Fragrance fragrance);
        Task<bool> UpdateFragrance(int id, Fragrance fragrance);
        Task<bool> DeleteFragrance(int id);
        Task<Fragrance?> GetFragranceByName(string name);

}
