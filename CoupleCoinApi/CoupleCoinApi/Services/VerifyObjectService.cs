namespace CoupleCoinApi.Services
{
    public static class VerifyObjectService
    {
        public static bool VerifyObject(object obj)
        {
            var properties = obj.GetType().GetProperties();
            if (properties.Length == 0)
                return false;

            return true;
        }
    }
}
