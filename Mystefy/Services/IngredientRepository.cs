using System;
using Mystefy.Data;
using Mystefy.Interfaces;
using Mystefy.Models;

namespace Mystefy.Services;

public class IngredientRepository : IIngredientRepository
{
    private readonly MystefyDbContext _context;
    public IngredientRepository(MystefyDbContext context)
    {
        _context = context;
    }
    public async Task<Ingredients> CreateIngredientAsync(Ingredients ingredient)
    {
        var newIngredient = _context.Add(ingredient);
        await _context.SaveChangesAsync();
        return newIngredient.Entity;
    }
    public Ingredients? GetIngredientWithDetails(int ingredientId)
    {
        throw new NotImplementedException();
    }
    public Ingredients? GetIngredientWithRecipes(int ingredientId)
    {
        throw new NotImplementedException();
    }
    public void AddIngredientToRecipe(int ingredientId, int recipeId)
    {
        throw new NotImplementedException();
    }
    public Ingredients? GetIngredientByName(string ingredientName)
    {
        throw new NotImplementedException();
    }
    public Ingredients? GetIngredientByType(string ingredientType)
    {
        throw new NotImplementedException();
    }
    public Ingredients? GetIngredientByCost(string ingredientCost)
    {
        throw new NotImplementedException();
    }
    public Ingredients? GetIngredientByIsExpired(bool isExpired)
    {
        throw new NotImplementedException();
    }
    public Ingredients? UpdateIngredient(Ingredients ingredient)
    {
        throw new NotImplementedException();
    }
    public Ingredients? DeleteIngredient(int ingredientId)
    {
        throw new NotImplementedException();
    }
    public void RemoveIngredientFromRecipe(int ingredientId, int recipeId)
    {
        throw new NotImplementedException();
    }

    Task<Ingredients?> IIngredientRepository.GetIngredientWithDetails(int ingredientId)
    {
        throw new NotImplementedException();
    }

    Task<Ingredients?> IIngredientRepository.GetIngredientWithRecipes(int ingredientId)
    {
        throw new NotImplementedException();
    }

    Task IIngredientRepository.AddIngredientToRecipe(int ingredientId, int recipeId)
    {
        throw new NotImplementedException();
    }

    Task<Ingredients?> IIngredientRepository.GetIngredientByName(string ingredientName)
    {
        throw new NotImplementedException();
    }

    Task<Ingredients?> IIngredientRepository.GetIngredientByType(string ingredientType)
    {
        throw new NotImplementedException();
    }

    Task<Ingredients?> IIngredientRepository.GetIngredientByCost(string ingredientCost)
    {
        throw new NotImplementedException();
    }

    Task<Ingredients?> IIngredientRepository.GetIngredientByIsExpired(bool isExpired)
    {
        throw new NotImplementedException();
    }

    Task<Ingredients?> IIngredientRepository.UpdateIngredient(Ingredients ingredient)
    {
        throw new NotImplementedException();
    }

    Task<Ingredients?> IIngredientRepository.DeleteIngredient(int ingredientId)
    {
        throw new NotImplementedException();
    }

    Task IIngredientRepository.RemoveIngredientFromRecipe(int ingredientId, int recipeId)
    {
        throw new NotImplementedException();
    }
}
