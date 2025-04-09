//The BatchIngredientsDTO file defines the structure of data exchanged between the client and the server. 
// This ensures that only the relevant fields (BatchID, IngredientsID, and Quantity) are exposed.
namespace Mystefy.DTOs
{
    public class BatchIngredientsDTO
    {
        public int BatchID { get; set; }
        public int IngredientsID { get; set; }
        public decimal Quantity { get; set; }
    }
}

