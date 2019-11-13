using System.Linq;
using System.Text;

namespace SacredSkull.TidalUSDK.Extensions
{
    public static class StringExtensions
    {
        public static string JoinPathSegments(params object[] objs)
        {
            var strings = objs.Select(o => o.ToString()).ToArray();
            var sb = new StringBuilder(strings[0]);

            for (int i = 1; i < strings.Length; i++)
            {
                if (strings[i - 1].EndsWith('/') && strings[i].StartsWith('/'))
                {
                    sb.Append(strings[i].Substring(1));
                }
                else if (!strings[i - 1].EndsWith('/') && !strings[i].StartsWith('/'))
                {
                    sb.Append('/' + strings[i]);
                }
                else
                {
                    sb.Append(strings[i]);
                }
            }

            return sb.ToString();
        }
    }
}