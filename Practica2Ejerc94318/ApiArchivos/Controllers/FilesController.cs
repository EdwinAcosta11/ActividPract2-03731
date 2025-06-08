using Microsoft.AspNetCore.Mvc;

namespace ApiArchivos.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly string _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        // GET: /api/files
        [HttpGet]
        public IActionResult GetFiles()
        {
            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            var files = Directory.GetFiles(_folderPath).Select(Path.GetFileName).ToList();
            return Ok(files);
        }

        // POST: /api/files/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("El archivo está vacío.");

            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);

            var filePath = Path.Combine(_folderPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { mensaje = "Archivo subido correctamente", nombre = file.FileName });
        }

        // GET: /api/files/download/{filename}
        [HttpGet("download/{filename}")]
        public IActionResult DownloadFile(string filename)
        {
            var filePath = Path.Combine(_folderPath, filename);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Archivo no encontrado.");

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = "application/octet-stream";
            return File(fileBytes, contentType, filename);
        }
    }
}
