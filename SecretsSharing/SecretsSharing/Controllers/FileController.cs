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

        /// <summary>
        /// get file manager from di
        /// </summary>
        /// <param name="fileManager"></param>
        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        
        /// <summary>
        /// upload users file
        /// </summary>
        /// <param name="model">model for upload file and set settings for work with files</param>
        /// <param name="file">user file</param>
        /// <returns>uri for downoload file</returns>
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
        
        /// <summary>
        /// downoload user file by file id
        /// </summary>
        /// <param name="id">file id</param>
        /// <returns>File by id or NoFound</returns>
        [HttpGet("id={id:guid}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            var file = _fileManager.GetFile(id);
            if (file == null)
                return NotFound();
            Response.Headers.Append("IsDelete", "true");
            var fileType="application/octet-stream";
            var fileStream = new FileStream(file.Path, FileMode.Open);
            return File(fileStream, fileType, file.FileName);
        }

        /// <summary>
        /// Get all user files
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>All files for by user id</returns>
        [Authorize]
        [HttpGet("getAll")]
        public async Task<OkObjectResult> GetAll(Guid userId)
        {
            var files = _fileManager.GetAllForUser(userId);
            return Ok(files);
        }

        /// <summary>
        /// Delete file by id
        /// </summary>
        /// <param name="fileId">file id</param>
        /// <returns>Ok if file delete or BadRequest in any cases</returns>
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