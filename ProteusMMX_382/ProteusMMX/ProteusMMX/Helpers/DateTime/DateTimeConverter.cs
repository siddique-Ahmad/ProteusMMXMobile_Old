using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Helpers.DateTime
{
    public class DateTimeConverter
    {
        /// <summary>
        /// Pass UTC DateTime and Destination time zone
        /// </summary>
        /// <param name="FromZoneID"> pass "Etc/UTC" for UTC time zone </param>
        /// <param name="ToZoneID"></param>
        /// <returns></returns>
        public static System.DateTime ConvertDateTimeToDifferentTimeZone(System.DateTime FromDateTime, string ToZoneID)
        {
            LocalDateTime fromLocal = LocalDateTime.FromDateTime(FromDateTime);
            //DateTimeZone fromZone = DateTimeZoneProviders.Tzdb[FromZoneID];
            DateTimeZone fromZone = DateTimeZoneProviders.Tzdb["Etc/UTC"];
            ZonedDateTime fromZoned = fromLocal.InZoneLeniently(fromZone);

            DateTimeZone toZone = DateTimeZoneProviders.Tzdb[ToZoneID];
            ZonedDateTime toZoned = fromZoned.WithZone(toZone);
            LocalDateTime toLocal = toZoned.LocalDateTime;
            return toLocal.ToDateTimeUnspecified();
        }

        /// <summary>
        /// Pass IANA TimeZoneID in ClientTimeZoneID
        /// </summary>
        /// <param name="ClientTimeZoneID">IANA TimeZoneID</param>
        /// <returns>current DateTime in different TimeZone</returns>
        public static System.DateTime ClientCurrentDateTimeByZone(string ClientTimeZoneID)
        {
            System.DateTime FromDateTime = System.DateTime.UtcNow;
            LocalDateTime fromLocal = LocalDateTime.FromDateTime(FromDateTime);
            DateTimeZone fromZone = DateTimeZoneProviders.Tzdb["Etc/UTC"];
            ZonedDateTime fromZoned = fromLocal.InZoneLeniently(fromZone);

            DateTimeZone toZone = DateTimeZoneProviders.Tzdb[ClientTimeZoneID];
            ZonedDateTime toZoned = fromZoned.WithZone(toZone);
            LocalDateTime toLocal = toZoned.LocalDateTime;
            return toLocal.ToDateTimeUnspecified();
        }

    }
}
