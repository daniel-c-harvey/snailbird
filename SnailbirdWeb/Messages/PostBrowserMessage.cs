﻿using Core;
using SnailbirdAdmin.Messages;
using SnailbirdData.Models.Post;

namespace SnailbirdWeb.Messages
{
    public enum PostBrowserAction
    {
        GetFeed,
        ViewPost
    }

    public abstract class PostBrowserMessage : MessageBase<PostBrowserAction>
    {
        protected PostBrowserMessage(PostBrowserAction action) : base(action) { }
    }

    public class PostBrowserGetFeedMessage : PostBrowserMessage
    {
        public Page Page { get; }

        public PostBrowserGetFeedMessage(Page page)
            : base(PostBrowserAction.GetFeed) 
        {
            Page = page;
        }
    }

    public class PostBrowserViewPostMessage<TPostModel> : PostBrowserMessage
    where TPostModel : Post, new()
    {
        public TPostModel Post { get; }

        public PostBrowserViewPostMessage(TPostModel post)
            : base(PostBrowserAction.ViewPost)
        {
            Post = post;
        }
    }
}
