using RemoteMusicPlayServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteMusicPlayServer.Service
{
    public class Mapper
    {
        public MusicDTO MapToDTO(Music music)
        {
            return new MusicDTO()
            {
                Id = music.Id,
                Name = music.Name
            };
        }
    }
}
