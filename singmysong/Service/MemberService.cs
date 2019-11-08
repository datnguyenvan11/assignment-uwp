using Newtonsoft.Json;
using singmysong.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace singmysong.Service
{
    class MemberService : IMemberService
    {
        public Member Register(Member member)
        {
            // tạo đối tượng httpclient giúp gửi dữ liệu đi. (hoặc lấy dữ liệu về)
            var httpClient = new HttpClient();
            // chuyển kiểu dữ liệu c# thành kiểu dữ liệu json.
            var dataToSend = JsonConvert.SerializeObject(member);
            // gói gém, gắn mác cho dữ liệu gửi đi, xác định kiểu dữ liệu là json, encode là utf8.
            var content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            // thực hiện gửi dữ liệu với phương thức post.
            var response = httpClient.PostAsync(ProjectConfiguration.MEMBER_REGISTER_URL, content).GetAwaiter().GetResult();
            // lấy kết quả trả về từ server.
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            // ép kiểu kết quả từ dữ liệu json sang dữ liệu của C#
            var responseMember = JsonConvert.DeserializeObject<Member>(jsonContent);
            // in ra id của member trả về.
            Debug.WriteLine("Register success with id: " + responseMember.id);
            return responseMember;
        }

        public MemberCredential Login(MemberLogin memberLogin)
        {
            var httpClient = new HttpClient();
            var dataToSend = JsonConvert.SerializeObject(memberLogin);
            var content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(ProjectConfiguration.MEMBER_LOGIN_URL, content).GetAwaiter().GetResult();
            var memberCredential = JsonConvert.DeserializeObject<MemberCredential>(response.Content.ReadAsStringAsync().Result);
            return memberCredential;
        }

        public Member GetInformation(string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(token);
            var response = httpClient.GetAsync(ProjectConfiguration.MEMBER_GET_INFORMATION).GetAwaiter().GetResult();
            var member = JsonConvert.DeserializeObject<Member>(response.Content.ReadAsStringAsync().Result);
            return member;
        }
    }
}
