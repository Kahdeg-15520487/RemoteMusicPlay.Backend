using LiteDB;
using RemoteMusicPlayServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1;

namespace RemoteMusicPlayServer.Service
{
    public class DatabaseService
    {
        public Music GetMusic(int id)
        {
            using (var db = new LiteDatabase(Constant.ConnectionString))
            {
                var musics = db.GetCollection<Music>("music");

                var music = musics.FindOne(m => m.Id.Equals(id));

                return music;
            }
        }

        public IEnumerable<Music> GetMusics()
        {
            using (var db = new LiteDatabase(Constant.ConnectionString))
            {
                var musics = db.GetCollection<Music>("music");

                var music = musics.FindAll();

                return music;
            }
        }

        public void RegisterMusic(Music music)
        {

            using (var db = new LiteDatabase(Constant.ConnectionString))
            {
                var musics = db.GetCollection<Music>("music");
                musics.Insert(music);
            }
        }
    }
}
