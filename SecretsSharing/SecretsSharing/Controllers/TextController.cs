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

        public TextController(ITextManager textManager)
        {
            _textManager = textManager;
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadText(TextModel model)
        {
            var id = await _textManager.UploadText(model);
            var uri = new Uri($"{Request.Scheme}://{Request.Host}/api/v1/files/id={id}");
            return Ok(uri);
        }

        [HttpGet("id={id:guid}")]
        public IActionResult Get(Guid id)
        {
            var text = _textManager.GetText(id);
            if (text.IsDelete)
                _textManager.DeleteText(id);
            return Ok(text);
        }

        [Authorize]
        [HttpGet("getAll")]
        public IActionResult GetAll(Guid userId)
        {
            var texts = _textManager.GetAllForUser(userId);
            return Ok(texts);
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult Delete(Guid textId)
        {
            _textManager.DeleteText(textId);
            return Ok();
        }
    }
    
}