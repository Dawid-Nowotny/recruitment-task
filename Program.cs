using Newtonsoft.Json;
using RecruitmentTask.DTOs;

string hotelFilePath = "Data\\hotels.json";
string bookingFilePath = "Data\\bookings.json";

try
{
	string hotelJson = File.ReadAllText(hotelFilePath);
	string bookingJson = File.ReadAllText(bookingFilePath);

	var hotels = JsonConvert.DeserializeObject<List<HotelDto>>(hotelJson);
	var bookingsDynamic = JsonConvert.DeserializeObject<dynamic>(bookingJson);

	if (hotels == null || bookingsDynamic == null)
	{
		Console.WriteLine("Deserialization failed.");
		return;
	}

	var bookings = new List<BookingDto>();
	foreach (var bookingDynamic in bookingsDynamic)
	{
		bookings.Add(BookingDto.ParseFromJson(bookingDynamic));
	}

	foreach (var hotel in hotels)
	{
		Console.WriteLine(hotel.Name);
		Console.WriteLine(hotel.Id);
		foreach (var roomType in hotel.RoomTypes)
		{
			Console.WriteLine(roomType.Description);
			Console.WriteLine(roomType.Code);
			Console.WriteLine(string.Join(", ", roomType.Amenities ?? new List<string>()));
			Console.WriteLine(string.Join(", ", roomType.Features ?? new List<string>()));
		}
	}

	foreach (var booking in bookings)
	{
		Console.WriteLine(booking.HotelId);
		Console.WriteLine(booking.Arrival.ToShortDateString());
		Console.WriteLine(booking.Departure.ToShortDateString());
		Console.WriteLine(booking.RoomType);
		Console.WriteLine(booking.RoomRate);
	}
}
catch (Exception ex)
{
	Console.WriteLine($"An error occurred: {ex.Message}");
}