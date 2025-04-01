using System;

namespace Mystefy.DTOs;

public class PackagingDTO
{

    public required string Name{ get; set; }
    public string? Type{ get; set; }
    public int Stock{ get; set; }

}

