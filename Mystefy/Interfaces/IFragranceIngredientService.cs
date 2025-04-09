using System;
using Mystefy.Models;

namespace Mystefy.Interfaces;

public interface IFragranceIngredientService
{
     Task<IEnumerable<FragranceIngredient>> GetAllFragranceIngredients();
    Task<FragranceIngredient?> GetFragranceIgredientsById(int fragranceId, int ingredientId);
    Task<FragranceIngredient> AddFragranceIngredient(FragranceIngredient fragranceIngredient);
    Task<bool> UpdateFragranceIngredient(int fragranceId, int ingredientId, FragranceIngredient fragranceIngredient);
    Task<bool> DeleteFragranceIngredient(int fragranceId, int ingredientId);
}
