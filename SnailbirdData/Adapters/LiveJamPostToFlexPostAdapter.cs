using SnailbirdData.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnailbirdData.Adapters
{
    public static class LiveJamPostToFlexPostAdapter
    {
        public static FlexPost AdaptFlex(this LiveJamPost other) 
        {
            var post = new FlexPost(
                other.ID,
                other.Title,
                other.PostDate,
                new List<PostElement>
            {
                new PostParagraph {Text = other.Preamble },
                new PostYouTubeEmbed {VideosURL = other.VideoURL },
                new PostInstrumentList { Instruments = new List<Instrument>((other.Instruments)) }
            });

            return post;
        }
    }
}
