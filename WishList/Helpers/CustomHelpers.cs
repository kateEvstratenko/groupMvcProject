using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WishList.ViewModels;
using Microsoft.AspNet.Identity;

namespace WishList.Helpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString Gift(this HtmlHelper html, GiftViewModel gift, bool moreButton = false)
        {
            var tag = new TagBuilder("div");
            tag.AddCssClass("panel panel-primary gift");
            
            var innerTag = new TagBuilder("div");
            innerTag.AddCssClass("panel-heading");
            var innerTagH = new TagBuilder("h2");
            innerTagH.SetInnerText(gift.Name);
            innerTag.InnerHtml += innerTagH.ToString();
            tag.InnerHtml += innerTag.ToString();

            innerTag = new TagBuilder("button");
            innerTag.AddCssClass("myCustomLikeButton");
            innerTag.Attributes.Add("name", "giftLikeId-" + gift.Id);

            innerTagH = new TagBuilder("img");
            innerTagH.Attributes.Add("src", "/Content/images/like.png");
            innerTagH.Attributes.Add("width", "10px");
            innerTagH.Attributes.Add("height", "10px");
            innerTag.InnerHtml += innerTagH;

            innerTagH = new TagBuilder("p");
            innerTagH.Attributes.Add("id", "giftLikeId-" + gift.Id);
            innerTagH.SetInnerText(gift.LikesCount.ToString());
            innerTag.InnerHtml += innerTagH;
            var likeTag = innerTag;
            
            innerTag = new TagBuilder("div");
            innerTag.AddCssClass("panel-body");

            innerTagH = new TagBuilder("img");
            innerTagH.AddCssClass("giftImage");
            innerTagH.Attributes.Add("src",gift.Logo);

            innerTag.InnerHtml += innerTagH.ToString();

            var infoTag = new TagBuilder("pre");
            innerTagH = new TagBuilder("h4");
            innerTagH.SetInnerText(gift.About);

            infoTag.InnerHtml += innerTagH.ToString();

            innerTag.InnerHtml += infoTag.ToString();
            innerTag.InnerHtml += likeTag.ToString();
            tag.InnerHtml += innerTag.ToString();

            
            if (!moreButton) return new MvcHtmlString(tag.ToString());

            innerTag = new TagBuilder("span");
            innerTag.AddCssClass("giftMoreButton");
            innerTagH = new TagBuilder("a");
            innerTagH.Attributes.Add("href", "/Gift/ViewGift/" + gift.Id);
            innerTagH.SetInnerText("More...");
            innerTag.InnerHtml += innerTagH.ToString();
            tag.InnerHtml += innerTag.ToString();

            return new MvcHtmlString(tag.ToString());
        }
    }
}