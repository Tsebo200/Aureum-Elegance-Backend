using System;

namespace Mystefy.DTOs;

public class FragranceIngredientsDTO
{
    public int FragranceID {get; set;}
    public int IngredientsID {get; set;}
    public decimal Amount {get; set;}

     public IncludeFragranceInFragranceIngredientsDTO? Fragrances {get; set;}
     public IncludeIngredientInFragranceIngredientsDTO? Ingredients {get; set;}
}

public class PostFragranceIngredientsDTO
{
    public required int FragranceID {get; set;}
    public required int IngredientsID {get; set;}
    public required decimal Amount {get; set;}

    
}

public class IncludeIngredientInFragranceIngredientsDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Cost { get; set; } = string.Empty;

        public DateTime ExpiryDate { get; set; }

        public bool IsExpired { get; set; }

    }

public class IncludeFragranceInFragranceIngredientsDTO{

    public string Name {get;set;}  = string.Empty;

   public string Description {get;set;}  = string.Empty;

   public decimal Volume  {get;set;} 

   public decimal Cost  {get;set;} 
   public DateTime ExpiryDate { get; set; }
}
