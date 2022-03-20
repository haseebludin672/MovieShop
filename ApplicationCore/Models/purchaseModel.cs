namespace ApplicationCore.Models;

public class PurchaseModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal? TotalPrice { get; set; }
    public System.Guid? PurchaseNumber { get; set; }
    public DateTime? PurchaseDateTime { get; set; }

    public MovieCardModel Movie { get; set; }
}
