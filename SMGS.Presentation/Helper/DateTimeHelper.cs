using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMGS.Presentation.Helper
{
    public class DateTimeHelper
    {
        public static int TotalChildInNode { get; set; } = 4;

        public DateTimeHelper()
        {

        }

        public static bool GetParentChildNodeTime(
            DateTime from, DateTime to, 
            out int fromNode, out int fromChildNode, 
            out int toNode, out int toChildNode)
        {
            if(from >= to)
            {
                fromNode = 0;
                fromChildNode = 0;
                toNode = 0;
                toChildNode = 0;

                return false;
            }
            else
            {
                fromNode = from.Hour;
                toNode = to.Hour;
                fromChildNode = from.Minute / (60 / TotalChildInNode);
                toChildNode = to.Minute / (60 / TotalChildInNode);

                return true;
            }
        }
    }
}