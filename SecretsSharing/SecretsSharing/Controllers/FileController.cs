using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.Attribute;
using SecretsSharing.Interface;
using SecretsSharing.Model;

namespace SecretsSharing.Controllers
{
    [ApiController]
    [Route("api/v1/file")]
    public class FileController : ControllerBase
    {
        private IFileManager _fileManager;

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        
        // [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromQuery]UploadFileModel model, IFormFile file)
        {
            try
            {
                var id = await _fileManager.UploadFileAsync(model, file);
                var uri = new Uri($"{Request.Scheme}://{Request.Host}/api/v1/files/id={id}");
                return Ok(uri);
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }
        
        [HttpGet("id={id:guid}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var file = _fileManager.GetFile(id);
            if (file == null)
                return NotFound();
            Response.Headers.Append("IsDelete", "true");
            // _fileManager.DeleteAutomatically(file);
            var fileType="application/octet-stream";
            var fileStream = new FileStream(file.Path, FileMode.Open);
            return File(fileStream, fileType, file.FileName);
        }
    }
}