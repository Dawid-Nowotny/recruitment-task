using RecruitmentTask.DTOs;
using RecruitmentTask.Exceptions;
using RecruitmentTask.Services.Interfaces;

namespace RecruitmentTask.Interfaces
{
	public class ConsoleInterface
	{
		private readonly IHotelService _hotelService;

		public ConsoleInterface(IHotelService hotelService) 
		{
			_hotelService = hotelService;
		}

		public void Run()
		{
			var (hotels, bookings) = LoadHotelsAndBookings();

			if (hotels == null || bookings == null)
			{
				Console.WriteLine("Failed to load JSON files.");
				return;
			}

			Console.WriteLine("Check room availability");
			HandleInput(hotels, bookings);
		}

		private (List<HotelDto>? hotels, List<BookingDto>? bookings) LoadHotelsAndBookings()
		{
			try
			{
				_hotelService.LoadData(out var hotels, out var bookings);
				return (hotels, bookings);
			}
			catch (DataLoadException ex)
			{
				Console.WriteLine($"Error loading data: {ex.Message}");
				return (null, null);
			}
		}

		private void HandleInput(List<HotelDto> hotels, List<BookingDto> bookings)
		{
			while (true)
			{
				var input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					break;
				}

				try
				{
					ProcessCommand(input, hotels, bookings);
				}
				catch (HotelNotFoundException ex)
				{
					Console.WriteLine(ex.Message);
				}
				catch (RoomTypeNotFoundException ex)
				{
					Console.WriteLine(ex.Message);
				}
				catch (InvalidDateException ex)
				{
					Console.WriteLine(ex.Message);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error: {ex.Message}");
				}
			}
		}

		private void ProcessCommand(string input, List<HotelDto> hotels, List<BookingDto> bookings)
		{
			if (input.StartsWith("Availability"))
			{
				var args = ParseArguments(input);

				if (args.Length < 3)
				{
					Console.WriteLine("Invalid input format.");
					return;
				}

				var hotelId = args[0];
				var roomType = args[2];
				var dates = args[1].Split('-');

				DateTime startDate = DateTime.ParseExact(dates[0], "yyyyMMdd", null);
				DateTime endDate;

				if (dates.Length > 1)
				{
					endDate = DateTime.ParseExact(dates[1], "yyyyMMdd", null);
				}
				else
				{
					endDate = startDate;
				}

				var available = _hotelService.CheckAvailability(hotels, bookings, hotelId, roomType, startDate, endDate);

				if (available)
				{
					Console.WriteLine("The selected room is available at the selected time.");
				}
				else
				{
					Console.WriteLine("The selected room is not available at the selected time.");
				}
			}
			else
			{
				Console.WriteLine("Unknown command.");
			}
		}

		private string[] ParseArguments(string input)
		{
			var startIndex = input.IndexOf('(') + 1;
			var endIndex = input.IndexOf(')');
			var arguments = input.Substring(startIndex, endIndex - startIndex);

			return arguments.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
							.Select(arg => arg.Trim())
							.ToArray();
		}
	}
}