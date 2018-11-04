using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NAudioInterface;
using RemoteMusicPlayServer.Model;
using RemoteMusicPlayServer.Service;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly DatabaseService dbservice;
        private readonly Mapper mapper;
        private readonly PlayService play;

        public MusicController(DatabaseService databaseService, Mapper mapper, PlayService playService)
        {
            dbservice = databaseService;
            this.mapper = mapper;
            play = playService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(dbservice.GetMusics().Select(m => mapper.MapToDTO(m)));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(mapper.MapToDTO(dbservice.GetMusic(id)));
        }

        [HttpGet("play/{id}")]
        public async Task<IActionResult> Play(int id)
        {
            var music = dbservice.GetMusic(id);

            await play.Play(music.Path);

            return Ok(mapper.MapToDTO(music));
        }

        [HttpGet("show/{show}")]
        public IActionResult Show(bool show)
        {
            if (show)
            {
                Program.Show();
            }
            else
            {
                Program.Hide();
            }

            return Ok(show);
        }

        [HttpPost("{fileName}")]
        public async Task<IActionResult> Upload(string fileName)
        {
            if (Request.HasFormContentType)
            {
                var form = Request.Form;
                foreach (var formFile in form.Files)
                {
                    var targetDirectory = Path.Combine(Constant.ContentRootPath, "uploads");

                    var savePath = Path.Combine(targetDirectory, fileName);

                    Console.WriteLine(savePath);

                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));

                    using (var fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        formFile.CopyTo(fileStream);
                    }

                    dbservice.RegisterMusic(new Music() { Name = Path.GetFileNameWithoutExtension(fileName), Path = savePath });
                }
            }

            return Ok();
        }
    }
}
