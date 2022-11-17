namespace SnapFaceApi.Models;

public class FaceSnapDTO
{
  public string Id { get; set; }
  public string? Title { get; set; }
  public string? Description { get; set; }
  public DateTime CreatedDate { get; set; }
  public int Snaps { get; set; }
  public string? ImageUrl { get; set; }
  public string? Location { get; set; }

}