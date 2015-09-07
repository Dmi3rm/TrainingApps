using System;
using System.Text;
using System.Web.Mvc;
using FS.Web.Models;
using System.Collections.Generic;

namespace FS.Web.HelpClasses
{
    public static class Paging
    {
        public static MvcHtmlString GetPages(this HtmlHelper html, ItemPaging itemPaging, string cssBtn, string cssInput, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder("Стр. ");

            if ((itemPaging.CurrentPage != 1) && (itemPaging.CurrentPage != 2) && (itemPaging.CurrentPage != itemPaging.NumOfPages - 1) && (itemPaging.CurrentPage != itemPaging.NumOfPages))
            {
                TagBuilder tagFirst = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(1) }, "1", "PageLink");
                TagBuilder tagBeforeCurrent = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.CurrentPage - 1) }, (itemPaging.CurrentPage - 1).ToString(), "PageLink");
                TagBuilder tagCurrent = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.CurrentPage) }, (itemPaging.CurrentPage).ToString(), "CurrentPageLink");
                TagBuilder tagPassCurrent = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.CurrentPage + 1) }, (itemPaging.CurrentPage + 1).ToString(), "PageLink");
                TagBuilder tagLast = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.NumOfPages) }, (itemPaging.NumOfPages).ToString(), "PageLink");

                result.Append(tagFirst.ToString());
                if (itemPaging.CurrentPage != 3)
                {
                    result.Append("..");
                }
                result.Append(tagBeforeCurrent.ToString());
                result.Append(tagCurrent.ToString());
                result.Append(tagPassCurrent.ToString());
                if (itemPaging.CurrentPage != itemPaging.NumOfPages - 2)
                {
                    result.Append("..");
                }
                result.Append(tagLast.ToString());
            }

            else
            {
                if (itemPaging.CurrentPage == 1)
                {
                    TagBuilder tagFirst = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(1) }, "1", "CurrentPageLink");
                    result.Append(tagFirst.ToString());
                    if (itemPaging.NumOfPages != 1)
                    {
                        TagBuilder tagSecond = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(2) }, "2", "PageLink");
                        result.Append(tagSecond.ToString());
                    }
                    if (itemPaging.NumOfPages == 3)
                    {
                        TagBuilder tagThird = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(3) }, "3", "PageLink");
                        result.Append(tagThird.ToString());
                    }
                    if (itemPaging.NumOfPages > 3)
                    {
                        result.Append("..");
                        TagBuilder tagLast = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.NumOfPages) }, (itemPaging.NumOfPages).ToString(), "PageLink");
                        result.Append(tagLast.ToString());
                    }
                }

                if (itemPaging.CurrentPage == 2)
                {
                    TagBuilder tagFirst = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(1) }, "1", "PageLink");
                    result.Append(tagFirst.ToString());
                    TagBuilder tagSecond = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(2) }, "2", "CurrentPageLink");
                    result.Append(tagSecond.ToString());
                    if (itemPaging.NumOfPages == 3)
                    {
                        TagBuilder tagThird = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(3) }, "3", "PageLink");
                        result.Append(tagThird.ToString());
                    }
                    if (itemPaging.NumOfPages > 3)
                    {
                        TagBuilder tagThird = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(3) }, "3", "PageLink");
                        result.Append(tagThird.ToString());
                        if (itemPaging.NumOfPages != 4)
                        {
                            result.Append("..");
                        }
                        TagBuilder tagLast = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.NumOfPages) }, (itemPaging.NumOfPages).ToString(), "PageLink");
                        result.Append(tagLast.ToString());
                    }
                }

                if (itemPaging.CurrentPage == itemPaging.NumOfPages - 1)
                {
                    if (itemPaging.NumOfPages > 3)
                    {
                        TagBuilder tagFirst = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(1) }, "1", "PageLink");
                        result.Append(tagFirst.ToString());
                        if (itemPaging.NumOfPages == 4)
                        {
                            TagBuilder tagSecond = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(2) }, "2", "PageLink");
                            result.Append(tagSecond.ToString());
                            TagBuilder tagThird = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(3) }, "3", "CurrentPageLink");
                            result.Append(tagThird.ToString());
                            TagBuilder tagForth = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(4) }, "4", "PageLink");
                            result.Append(tagForth.ToString());
                        }
                        else
                        {
                            result.Append("..");
                            TagBuilder tagBeforeCurrent = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.CurrentPage - 1) }, (itemPaging.CurrentPage - 1).ToString(), "PageLink");
                            result.Append(tagBeforeCurrent.ToString());
                            TagBuilder tagCurrent = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.CurrentPage) }, (itemPaging.CurrentPage).ToString(), "CurrentPageLink");
                            result.Append(tagCurrent.ToString());
                            TagBuilder tagLast = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.CurrentPage + 1) }, (itemPaging.CurrentPage + 1).ToString(), "PageLink");
                            result.Append(tagLast.ToString());
                        }
                    }
                }

                if (itemPaging.CurrentPage == itemPaging.NumOfPages)
                {
                    if (itemPaging.NumOfPages > 2)
                    {
                        TagBuilder tagFirst = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(1) }, "1", "PageLink");
                        result.Append(tagFirst.ToString());
                        if (itemPaging.NumOfPages == 3)
                        {
                            TagBuilder tagSecond = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(2) }, "2", "PageLink");
                            result.Append(tagSecond.ToString());
                            TagBuilder tagThird = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(3) }, "3", "CurrentPageLink");
                            result.Append(tagThird.ToString());
                        }
                        else
                        {
                            result.Append("..");
                            TagBuilder tagBeforeCurrent = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.CurrentPage - 1) }, (itemPaging.CurrentPage - 1).ToString(), "PageLink");
                            result.Append(tagBeforeCurrent.ToString());
                            TagBuilder tagCurrent = CreateTag("a", new List<string>() { "href" }, new List<string>() { pageUrl(itemPaging.CurrentPage) }, (itemPaging.CurrentPage).ToString(), "CurrentPageLink");
                            result.Append(tagCurrent.ToString());
                        }
                    }
                }
            }

            TagBuilder input = CreateTag("input", new List<string>() { "id", "class" }, new List<string> { "InputPageNumber", cssInput }, "");
            result.Append(input.ToString());

            TagBuilder button = CreateTag("button", new List<string> { "onclick", "class" }, new List<string> { "GoToPage(this)", cssBtn }, "Go");
            result.Append(button.ToString());

            return MvcHtmlString.Create(result.ToString());
        }


        public static TagBuilder CreateTag(string TagName, List<string> attrs, List<string> vals, string innerHtml, string cssClass="")
        {
            TagBuilder tag = new TagBuilder(TagName);
            for (int i = 0; i < attrs.Count; i++)
            {
                tag.MergeAttribute(attrs[i], vals[i]);
            }
            tag.InnerHtml = innerHtml;
            if (cssClass != "")
            {
                tag.AddCssClass(cssClass);
            }
            return tag;
        }
    }
}