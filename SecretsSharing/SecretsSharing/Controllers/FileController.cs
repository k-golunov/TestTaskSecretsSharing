using System;
using System.Collections.Generic;
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
        
        [Authorize]
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

        [Authorize]
        [HttpGet("getAll")]
        public async Task<OkObjectResult> GetAll(Guid userId)
        {
            var files = _fileManager.GetAllForUser(userId);
            return Ok(files);
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            var response = _fileManager.DeleteFile(fileId);
            if (response)
                return Ok();
            return BadRequest("File not deleted");
        }
    }
}