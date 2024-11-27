using System.Globalization;

namespace RecruitmentTask.DTOs
{
	public class BookingDto
	{
		public string HotelId { get; set; }
		public DateTime Arrival { get; set; }
		public DateTime Departure { get; set; }
		public string RoomType { get; set; }
		public string RoomRate { get; set; }

		public static BookingDto ParseFromJson(dynamic booking)
		{
			var arrival = DateTime.ParseExact((string)booking.arrival, "yyyyMMdd", CultureInfo.InvariantCulture);
			var departure = DateTime.ParseExact((string)booking.departure, "yyyyMMdd", CultureInfo.InvariantCulture);

			return new BookingDto
			{
				HotelId = booking.hotelId,
				Arrival = arrival,
				Departure = departure,
				RoomType = booking.roomType,
				RoomRate = booking.roomRate
			};
		}
	}
}