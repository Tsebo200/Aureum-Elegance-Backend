using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IPackagingRepository
{
        Task<Packaging> CreatePackagingAsync(Packaging packaging);

}