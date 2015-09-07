using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS.Web.Models
{
    public class FTpcPaging : ItemPaging
    {
        public List<FTopic> FTopics {get; set;}

        public FTpcPaging(List<FTopic> allFTopics, int currentPage, int tpcsPerPage, int numOfAllTpcs)
        {
            ItemsPerPage = tpcsPerPage;
            double a = (double)numOfAllTpcs / (double)ItemsPerPage;
            NumOfPages = (int)Math.Ceiling(a);
            if (currentPage != 0)
            {
                if (currentPage < NumOfPages)
                {
                    CurrentPage = currentPage;
                }
                else
                {
                    CurrentPage = NumOfPages;
                }
            }
            else
            {
                CurrentPage = NumOfPages;
            }

            FTopics = allFTopics.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
        }
    }
}