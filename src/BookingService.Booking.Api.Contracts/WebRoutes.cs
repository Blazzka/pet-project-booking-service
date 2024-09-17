namespace BookingService.Booking.Api.Contracts;

public static class WebRoutes
{
    private const string BasePath = "api";

    public static class Booking
    {
        public const string Path = BasePath + "/booking";
        public const string GetById = "{id}";
        public const string Cancel = GetById + "/cancel";
        public const string GetByFilter = "by-filter";
        public const string GetStatusById = GetById + "/status";
    }
}