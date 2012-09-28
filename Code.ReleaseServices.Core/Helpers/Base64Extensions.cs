using System;

namespace Code.ReleaseServices.Core.Helpers
{
    public static class Base64Extensions
    {
        public static string ToBase64(this string stringToEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(stringToEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string FromBase64(this string stringToDecode)
        {
            throw new NotImplementedException();
        }
    }
}