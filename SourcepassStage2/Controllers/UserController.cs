using DocumentBuilder.DocumentBuilders;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using SourcepassStage2.Repository;
using System.Net;
using System.Text;

namespace SourcepassStage2.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        [HttpPost("DownloadPdf")]
        public async Task<IActionResult> DownloadPdf(int userID)
        {
            return await DownloadPdfAsync(userID);
        }

        private async Task<IActionResult> DownloadPdfAsync(int userID)
        {
            var user = _userRepository.GetUserByID(userID);

            if (user != null)
            {
                string defaultPath = "DummyPDF/Output.pdf";
                string startupPath = Environment.CurrentDirectory;
                String pdffilePath = Path.Combine(startupPath, defaultPath.Replace("/", "\\"));

                StringBuilder sb = new StringBuilder();
                sb.Append("<html><head><meta charset='utf-8'/>");
                sb.Append("<style>ul li{list-style:none;margin-top:3px;margin-bottom:3px;}");
                sb.Append("h1{font-size: 23px;margin-bottom: 0px;margin-top: 0px;");
                sb.Append("font-weight: normal;font-family: Arial;}");
                sb.Append("h2{margin: 3px;margin-left: 0px;font-size: 16px;}");
                sb.Append("h3{margin: 3px;font-size: 16px;}");
                sb.Append("h4{margin: 3px;margin-left: 0px;font-size: 13px;}");
                sb.Append("h5{margin: 3px;font-size: 11px;font-weight: normal;font-family: Arial;}");
                sb.Append("</style>");
                sb.Append("</head>");
                sb.Append("<body style='text-align: center;margin:auto;margin-top:0px;margin-bottom:5px;'>");
                sb.Append("<h1>Sourcepass Test</h1>");
                sb.Append("</body>");
                sb.Append($"<h2>UserID : {user.UserID}</h2>");
                sb.Append($"<h2>Fullname : {user.Fullname}</h2>");
                sb.Append($"<h2>Email : {user.Email}</h2>");
                sb.Append($"<h2>Email : {user.Phonenumber}</h2>");
                sb.Append("</html>");

                var renderer = new ChromePdfRenderer();
                var pdf = renderer.RenderHtmlAsPdf(sb.ToString());
                pdf.SaveAs(pdffilePath);

                var fileName = System.IO.Path.GetFileName(pdffilePath);
                var content = await System.IO.File.ReadAllBytesAsync(pdffilePath);

                new FileExtensionContentTypeProvider()
                        .TryGetContentType(fileName, out string contentType);

                return File(content, contentType, fileName);
            }
            else
            {
                return Content("Error");
            }
        }
        

    }
}
