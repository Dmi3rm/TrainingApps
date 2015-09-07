using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS.Web.Models
{
    public class FMsgPaging : ItemPaging
    {
        public List<FMessage> FMessages {get; set;}

        public FMsgPaging(List<FMessage> allFMessages, int currentPage, int msgsPerPage, int numOfAllMsgs)
        {
            ItemsPerPage = msgsPerPage;
            double a = (double)numOfAllMsgs / (double)ItemsPerPage;
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

            FMessages = allFMessages.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
        }
    }
}