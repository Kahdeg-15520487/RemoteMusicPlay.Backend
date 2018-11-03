using NAudio.Wave;
using System.Threading.Tasks;

namespace NAudioInterface
{
    public class PlayService
    {
        public async Task Play(string filePath)
        {
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
