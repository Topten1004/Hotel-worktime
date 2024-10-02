namespace Worktime.Global
{
    public static class TimeConvert
    {
        public static DateTime ConvertToGMT11(DateTime dt)
        {
            // Specify the UTC offset for New Caledonia (GMT+11:00)
            TimeSpan utcOffset = TimeSpan.FromHours(11);

            // Convert the local time to New Caledonia time (GMT+11:00)
            DateTime newCaledoniaTime = dt.ToUniversalTime().Add(utcOffset);

            return newCaledoniaTime;
        }
    }
}
