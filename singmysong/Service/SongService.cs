using Newtonsoft.Json;
using singmysong.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace singmysong.Service
{
    class SongService : ISongService
    {

        public Song CreateSong(MemberCredential memberCredential, Song song)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
               new AuthenticationHeaderValue(memberCredential.token);
            var dataToSend = JsonConvert.SerializeObject(song);
            var content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(ProjectConfiguration.SONG_CREATE_URL, content).GetAwaiter().GetResult();
            var songdata = JsonConvert.DeserializeObject<Song>(response.Content.ReadAsStringAsync().Result);
            return songdata;
        }

        public List<Song> GetAllSong(MemberCredential memberCredential)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(memberCredential.token);
            var response = httpClient.GetAsync(ProjectConfiguration.SONG_GET_ALL).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<List<Song>>(response.Content.ReadAsStringAsync().Result);
        }

        public List<Song> GetMineSongs(MemberCredential memberCredential)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(memberCredential.token);
            var response = httpClient.GetAsync(ProjectConfiguration.SONG_GET_MINE).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<List<Song>>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
