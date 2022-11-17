using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnapFaceApi.Models;

namespace SnapFaceApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FaceSnapController : ControllerBase
  {
    private readonly FaceSnapContext _context;

    public FaceSnapController(FaceSnapContext context)
    {
      _context = context;
    }

    // GET: api/FaceSnap
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FaceSnapDTO>>> GetFaceSnapItems()
    {
      return await _context.FaceSnapItems.Select(x => ItemToDTO(x)).ToListAsync();
    }

    // GET: api/FaceSnap/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FaceSnapDTO>> GetFaceSnapItem(string id)
    {
      var faceSnapItem = await _context.FaceSnapItems.FindAsync(id);

      if (faceSnapItem == null)
      {
        return NotFound();
      }

      return ItemToDTO(faceSnapItem);
    }

    // PUT: api/FaceSnap/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFaceSnapItem(string id, FaceSnapDTO faceSnapDTO)
    {
      if (id != faceSnapDTO.Id)
      {
        return BadRequest();
      }

      var faceSnapItem = await _context.FaceSnapItems.FindAsync(id);
      if (faceSnapItem == null)
      {
        return NotFound();
      }

      faceSnapItem.Title = faceSnapDTO.Title;
      faceSnapItem.Description = faceSnapDTO.Description;
      faceSnapItem.Snaps = faceSnapDTO.Snaps;
      faceSnapItem.Location = faceSnapDTO.Location;
      faceSnapItem.ImageUrl = faceSnapDTO.ImageUrl;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) when (!FaceSnapItemExists(id))
      {
        return NotFound();
      }

      return NoContent();
    }

    // POST: api/FaceSnap
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<FaceSnapDTO>> PostFaceSnapItem(FaceSnapDTO faceSnapDTO)
    {

      var faceSnapItem = DTOtoItem(faceSnapDTO);
      _context.FaceSnapItems.Add(faceSnapItem);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {
        if (FaceSnapItemExists(faceSnapItem.Id))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }
      return CreatedAtAction(nameof(GetFaceSnapItem), new { id = faceSnapItem.Id }, faceSnapItem);
    }

    // DELETE: api/FaceSnap/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFaceSnapItem(string id)
    {
      var faceSnapItem = await _context.FaceSnapItems.FindAsync(id);
      if (faceSnapItem == null)
      {
        return NotFound();
      }

      _context.FaceSnapItems.Remove(faceSnapItem);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool FaceSnapItemExists(string id)
    {
      return _context.FaceSnapItems.Any(e => e.Id == id);
    }

    private static FaceSnapDTO ItemToDTO(FaceSnapItem faceSnapItem) =>

      new FaceSnapDTO
      {
        Id = faceSnapItem.Id,
        Title = faceSnapItem.Title,
        Description = faceSnapItem.Description,
        CreatedDate = faceSnapItem.CreatedDate,
        Snaps = faceSnapItem.Snaps,
        ImageUrl = faceSnapItem.ImageUrl,
        Location = faceSnapItem.Location
      };

    private static FaceSnapItem DTOtoItem(FaceSnapDTO faceSnapDTO) =>

      new FaceSnapItem
      {
        Id = faceSnapDTO.Id,
        Title = faceSnapDTO.Title,
        Description = faceSnapDTO.Description,
        CreatedDate = faceSnapDTO.CreatedDate,
        Snaps = faceSnapDTO.Snaps,
        ImageUrl = faceSnapDTO.ImageUrl,
        Location = faceSnapDTO.Location
      };

  }
}

