﻿using DataAccess;
using SnailbirdData.Models.Post;

namespace SnailbirdData.Models.Entities
{
    public class LabFeedFlexPost : FlexPost, IEntity
    {
        public LabFeedFlexPost()
        : base()
        { }

        public override Post.Post Clone()
        {
            return new LabFeedFlexPost()
            {
                ID = ID,
                Title = Title,
                PostDate = PostDate,
                Elements = Elements
            };
        }
    }
}
