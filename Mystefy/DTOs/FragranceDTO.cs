using System;

namespace Mystefy.DTOs;

public class FragranceDTO
{
   public int Id {get;set;} 

   public string Name {get;set;}  = string.Empty;

   public string Description {get;set;}  = string.Empty;

   public decimal Volume  {get;set;} 

   public decimal Cost  {get;set;} 
   public DateTime ExpiryDate { get; set; }
     public List<FragranceIngredientInFragranceDTO>? FragranceIngredients {get; set;}

}

public class PostFragranceDTO
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Cost { get; set; }
    public required DateTime ExpiryDate { get; set; }
    public required decimal Volume { get; set; }

}

public class FragranceIngredientInFragranceDTO{

    public int IngredientsID {get; set;}
    public decimal Amount {get; set;}

    public List<IngredientsInFragranceIngredientsDTO>? Ingredients {get; set;}
}

public class IngredientsInFragranceIngredientsDTO
    {

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Cost { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }


    }
