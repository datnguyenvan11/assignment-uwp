using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace singmysong.Entity
{
    class ProjectConfiguration
    {
        public const string MEMBER_REGISTER_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members";
        public const string GET_UPLOAD_URL = "https://2-dot-backup-server-003.appspot.com/get-upload-token";
        public const string MEMBER_LOGIN_URL =
            "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        public const string MEMBER_GET_INFORMATION = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        public const string SONG_CREATE_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        public const string SONG_GET_ALL = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        public const string SONG_GET_MINE = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs/get-mine";
        public static MemberCredential CurrentMemberCredential;
    }
}
