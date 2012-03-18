using System;
using System.Text;

namespace Nest.FactoryDsl.Common
{
    public class Regex
    {
        public static int FlagsFromString(string flags)
        {
            int pFlags = 0;

            foreach (var s in flags.Split(new[]{'|'},StringSplitOptions.RemoveEmptyEntries))
            {
                if ("CASE_INSENSITIVE".Equals(s,StringComparison.CurrentCultureIgnoreCase))
                {
                    pFlags |= 2;
                }
                else if ("MULTILINE".Equals(s, StringComparison.CurrentCultureIgnoreCase))
                {
                    pFlags |= 8;
                }
                else if ("DOTALL".Equals(s, StringComparison.CurrentCultureIgnoreCase))
                {
                    pFlags |= 32;
                }
                else if ("UNICODE_CASE".Equals(s, StringComparison.CurrentCultureIgnoreCase))
                {
                    pFlags |= 64;
                }
                else if ("CANON_EQ".Equals(s, StringComparison.CurrentCultureIgnoreCase))
                {
                    pFlags |= 128;
                }
                else if ("UNIX_LINES".Equals(s, StringComparison.CurrentCultureIgnoreCase))
                {
                    pFlags |= 1;
                }
                else if ("LITERAL".Equals(s, StringComparison.CurrentCultureIgnoreCase))
                {
                    pFlags |= 16;
                }
                else if ("COMMENTS".Equals(s, StringComparison.CurrentCultureIgnoreCase))
                {
                    pFlags |= 4;
                }    
            }

            return pFlags;
        }

        public static string FlagsToString(int flags)
        {
            var sb = new StringBuilder();

            if ((flags & 2) != 0)
            {
                sb.Append("CASE_INSENSITIVE|");
            }
            if ((flags & 8) != 0)
            {
                sb.Append("MULTILINE|");
            }
            if ((flags & 32) != 0)
            {
                sb.Append("DOTALL|");
            }
            if ((flags & 64) != 0)
            {
                sb.Append("UNICODE_CASE|");
            }
            if ((flags & 128) != 0)
            {
                sb.Append("CANON_EQ|");
            }
            if ((flags & 1) != 0)
            {
                sb.Append("UNIX_LINES|");
            }
            if ((flags & 16) != 0)
            {
                sb.Append("LITERAL|");
            }
            if ((flags & 4) != 0)
            {
                sb.Append("COMMENTS|");
            }
            return sb.ToString();
        }

        // 001 System.out.println(Pattern.UNIX_LINES);
        // 002 System.out.println(Pattern.CASE_INSENSITIVE);
        // 004 System.out.println(Pattern.COMMENTS);
        // 008 System.out.println(Pattern.MULTILINE);
        // 016 System.out.println(Pattern.LITERAL);
        // 032 System.out.println(Pattern.DOTALL);
        // 064 System.out.println(Pattern.UNICODE_CASE);
        // 128 System.out.println(Pattern.CANON_EQ);
    }
}