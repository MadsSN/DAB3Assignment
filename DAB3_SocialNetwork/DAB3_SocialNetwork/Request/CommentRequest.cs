using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_SocialNetwork.Models;

namespace DAB3_SocialNetwork
{
    public class CommentRequest
    {
        public string PostId { get; set; }
        public Comment Comment { get; set; }

    }
}
