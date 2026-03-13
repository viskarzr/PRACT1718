using System;
using System.Collections.Generic;

namespace PRACT1718.ModelsBD;

public partial class Product
{
    public int Id { get; set; }

    public string NameTovar { get; set; } = null!;

    public decimal PriceUnit { get; set; }

    public DateOnly DateStart { get; set; }

    public string NumberPart { get; set; } = null!;

    public int SizePart { get; set; }

    public string? NameFirm { get; set; }

    public int? SizeRentPart { get; set; }

    public decimal? PriceSold { get; set; }

    public DateOnly? DateSold { get; set; }
}
