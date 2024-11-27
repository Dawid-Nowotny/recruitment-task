using Newtonsoft.Json;
using RecruitmentTask.DTOs;
using RecruitmentTask.Services.Interfaces;

namespace RecruitmentTask.Services
{
	public class HotelService : IHotelService
	{
		public bool LoadData(out List<HotelDto> hotels, out List<BookingDto> bookings)
		{
			hotels = null;
			bookings = null;

			string hotelFilePath = "Data\\hotels.json";
			string bookingFilePath = "Data\\bookings.json";

			try
			{
				string hotelJson = File.ReadAllText(hotelFilePath);
				string bookingJson = File.ReadAllText(bookingFilePath);

				hotels = JsonConvert.DeserializeObject<List<HotelDto>>(hotelJson);
				var bookingsDynamic = JsonConvert.DeserializeObject<dynamic>(bookingJson);

				bookings = new List<BookingDto>();
				foreach (var bookingDynamic in bookingsDynamic)
				{
					bookings.Add(BookingDto.ParseFromJson(bookingDynamic));
				}

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
				return false;
			}
		}

		public void checkAvailability()
		{
			throw new NotImplementedException();
		}
	}
}
