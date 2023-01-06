using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SecretsSharing.Interface;

namespace SecretsSharing.Middlewares
{
    public class DeleteFileMiddleware
    {
        private readonly RequestDelegate _next;
    
        public DeleteFileMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IFileManager fileManager)
        {
            await _next.Invoke(context);
            if (context.Response.Headers.ContainsKey("IsDelete"))
            {
                var id = context.Request.Headers[":path"].ToString().Split('=').Last();
                fileManager.DeleteFile(Guid.Parse(id));
            }
        }
    }
}