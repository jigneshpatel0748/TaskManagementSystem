using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload(string type)
        {
            try
            {
                var fileOutPutPath = new List<string>();
                var formCollection = await Request.ReadFormAsync();
                var files = formCollection.Files.ToList();
                var folderName = Path.Combine("Resources", type);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = $"{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}_{file.FileName}";

                            if (!Directory.Exists(pathToSave))
                            {
                                Directory.CreateDirectory(pathToSave);
                            }

                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            string fileUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/{dbPath.Replace("\\", "/")}";
                            fileOutPutPath.Add(fileUrl);
                        }
                    }
                }
                else
                {
                    return BadRequest();
                }
                return Ok(fileOutPutPath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
