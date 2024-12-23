﻿using SnailbirdAdminWeb.Client.API;
using SnailbirdAdminWeb.Client.ViewModels.EditFlex;
using SnailbirdData.Models.Post;

namespace SnailbirdAdminWeb.Client.ViewModels
{
    public class FlexPostManagerViewModel<TPost> 
    : PostManagerViewModel<TPost, EditFlexPostViewModel<TPost>>
    where TPost : FlexPost<TPost>, new()
    {
        public FlexPostManagerViewModel(IPostManagerClient<TPost> postManager) 
        : base(postManager) 
        { }
    }
}
