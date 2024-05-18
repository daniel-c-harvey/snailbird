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

    public class PostBrowserViewPostMessage : PostBrowserMessage
    {
        public LiveJamPost Post { get; }

        public PostBrowserViewPostMessage(LiveJamPost post)
            : base(PostBrowserAction.ViewPost)
        {
            Post = post;
        }
    }
}
