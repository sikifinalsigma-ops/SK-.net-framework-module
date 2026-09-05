using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Server.Controller
{
    public class FileManagementController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> UploadFile()
        {
            if (!Request.Content.IsMimeMultipartContent())
                return BadRequest("并非文件请求。");

            var root = HttpContext.Current.Server.MapPath("~/UploadFiles");
            var folder = DateTime.Now.ToString("yyyyMMdd");
            var fullPath = Path.Combine(root, folder);
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            var provider = new MultipartFormDataStreamProvider(root);

            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var file in provider.FileData)
            {
                var fileName = file.Headers.ContentDisposition.FileName.Trim('"');
                var filePath = Path.Combine(fullPath, fileName);     
                File.Move(file.LocalFileName, filePath);
            }

            return Ok("文件上传成功。");
        }


        [HttpGet]
        public HttpResponseMessage DownloadFile(string fileName)
        {
            var filePath = HttpContext.Current.Server.MapPath("~/UploadFiles/" + fileName);

            if (!File.Exists(filePath))
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            else
                fileName = Path.GetFileName(filePath);
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            //预览模式
            /*
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
            {
                FileName = fileName
            };
            */
            return result;
        }




    }
}