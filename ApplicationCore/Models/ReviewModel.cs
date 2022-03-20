namespace ApplicationCore.Models;

public class ReviewModel
{
    public string Title { get; set; }
    public string PosterURL { get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public decimal Rating { get; set; }
    public string ReviewText { get; set; }
}