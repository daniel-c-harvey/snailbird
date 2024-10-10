using SnailbirdData.Models.Post;
using SnailbirdData.Models.Entities;

namespace SnailbirdData.Adapters
{
    public static class LiveJamPostToFlexPostAdapter
    {
        public static FlexPost AdaptFlex(this LiveJamPost other) 
        {
            var post = new StudioFeedFlexPost(
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
