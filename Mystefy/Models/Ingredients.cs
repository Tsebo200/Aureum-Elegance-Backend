using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mystefy.Models;

namespace Mystefy.Models;

//Ingredients entity - maps how my table in the database will look like
public class Ingredients
{
    [Key] // Attribute Annotation to behave as a primary key
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]//these influence how the table will behave
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
    public bool IsExpired { get; set; }
}

// we can set up rules for our properties on how they behave in our database using the []