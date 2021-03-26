using System;
using System.Collections.Generic;

namespace FromFarmer.Utilities.Operations
{
    public static class DbQueryOperation
    {
        public static string ThousandQueryConstructor(int pagesize, string head, string conscol, List<Int64> lst, string tail)
        {
            string res = "";
            string subs = "";
            subs += "(";
            int tot = lst.Count;
            int part = tot / pagesize;
            int subt = tot - (part * pagesize);
            for (int i = 0; i < part; i++)
            {
                string accu = "";
                accu += conscol + " in (";
                for (int j = 0; j < pagesize; j++)
                {
                    accu += lst[i * pagesize + j].ToString() + ",";
                }
                accu = accu.Substring(0, accu.Length - 1);
                accu += ") ";
                if ((i < part - 1)) accu += "or ";
                subs += accu;
            }
            if (subt > 0)
            {
                string accu = " ";
                if (part > 0) accu += "or ";
                accu += conscol + " in (";
                for (int i = 0; i < subt; i++)
                {
                    accu += lst[part * pagesize + i].ToString() + ",";
                }
                accu = accu.Substring(0, accu.Length - 1);
                accu += ") ";
                subs += accu;
            }
            subs += ")";
            res += head + " ";
            res += subs + " ";
            res += tail;
            return res;
        }

        public static string ThousandQueryConstructor(int pagesize, string head, string conscol, List<string> lst, string tail)
        {
            string res = "";
            string subs = "";
            subs += "(";
            int tot = lst.Count;
            int part = tot / pagesize;
            int subt = tot - (part * pagesize);
            for (int i = 0; i < part; i++)
            {
                string accu = "";
                accu += conscol + " in (";
                for (int j = 0; j < pagesize; j++)
                {
                    accu += "'" + lst[i * pagesize + j].ToString() + "',";
                }
                accu = accu.Substring(0, accu.Length - 1);
                accu += ") ";
                if ((i < part - 1)) accu += "or ";
                subs += accu;
            }
            if (subt > 0)
            {
                string accu = " ";
                if (part > 0) accu += "or ";
                accu += conscol + " in (";
                for (int i = 0; i < subt; i++)
                {
                    accu += "'" + lst[part * pagesize + i].ToString() + "',";
                }
                accu = accu.Substring(0, accu.Length - 1);
                accu += ") ";
                subs += accu;
            }
            subs += ")";
            res += head + " ";
            res += subs + " ";
            res += tail;
            return res;
        }
    }

}
