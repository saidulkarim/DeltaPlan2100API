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
}
