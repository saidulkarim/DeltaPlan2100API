using System;
using System.Collections.Generic;

namespace DeltaPlan2100API.Models
{
    public partial class TblUserComments
    {
        public int CommentsId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmailAddress { get; set; }
        public string UserComments { get; set; }
    }

    public class ModelSendFeedback
    {
        public string user_name { get; set; }
        public string phone_no { get; set; }
        public string user_email { get; set; }
        public string user_comment { get; set; }
    }
}
