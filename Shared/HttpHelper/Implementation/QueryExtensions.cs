using System.Web;

namespace Shared.HttpHelper.Implementation
{
    public static class QueryExtensions
    {
        public static string ToQueryString(this object @object)
        {

            var properties = from p in @object.GetType().GetProperties()
                             where p.GetValue(@object, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(@object, null).ToString());

            // queryString will be set to "Id=1&State=26&Prefix=f&Index=oo"
            var queryString = string.Join("&", properties.ToArray());
            return queryString;
        }
    }
}
