using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.Attribute;
using SecretsSharing.Interface;
using SecretsSharing.Model;

namespace SecretsSharing.Controllers
{        
    [ApiController]
    [Route("api/v1/text")]
    public class TextController : ControllerBase
    {

        private readonly ITextManager _textManager;

        /// <summary>
        /// Get text manager from di
        /// </summary>
        /// <param name="textManager">instance text manager</param>
        public TextController(ITextManager textManager)
        {
            _textManager = textManager;
        }

        /// <summary>
        /// Upload user text
        /// </summary>
        /// <param name="model">model for upload text</param>
        /// <returns>uri for download text</returns>
        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadText(TextModel model)
        {
            var id = await _textManager.UploadText(model);
            var uri = new Uri($"{Request.Scheme}://{Request.Host}/api/v1/files/id={id}");
            return Ok(uri);
        }

        /// <summary>
        /// Get text by id
        /// </summary>
        /// <param name="id">text id</param>
        /// <returns>user text</returns>
        [HttpGet("id={id:guid}")]
        public IActionResult Get(Guid id)
        {
            var text = _textManager.GetText(id);
            if (text.IsDelete)
                _textManager.DeleteText(id);
            return Ok(text);
        }

        /// <summary>
        /// Get all user text by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>all UserText</returns>
        [Authorize]
        [HttpGet("getAll")]
        public IActionResult GetAll(Guid userId)
        {
            var texts = _textManager.GetAllForUser(userId);
            return Ok(texts);
        }

        /// <summary>
        /// Delete text by textId
        /// </summary>
        /// <param name="textId"></param>
        /// <returns>Ok</returns>
        [Authorize]
        [HttpDelete("delete")]
        public IActionResult Delete(Guid textId)
        {
            _textManager.DeleteText(textId);
            return Ok();
        }
    }
    
}