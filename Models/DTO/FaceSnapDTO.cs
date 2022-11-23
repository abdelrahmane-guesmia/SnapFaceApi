using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace SnapFaceApi.Models;
public class FaceSnapDTO
{
  [BsonElement("title")]
  [JsonPropertyName("Title")]
  public string? Title { get; set; }
  [BsonElement("description")]
  [JsonPropertyName("Description")]
  public string? Description { get; set; }
  [BsonElement("created_date")]
  [JsonPropertyName("CreatedDate")]
  public DateTime CreatedDate { get; set; }
  [BsonElement("snaps")]
  [JsonPropertyName("Snaps")]
  public int Snaps { get; set; }
  [BsonElement("image_url")]
  [JsonPropertyName("ImageUrl")]
  public string? ImageUrl { get; set; }
  [BsonElement("location")]
  [JsonPropertyName("Location")]
  public string? Location { get; set; }

}