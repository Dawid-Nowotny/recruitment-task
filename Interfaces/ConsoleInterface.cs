using RecruitmentTask.DTOs;
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
			List<HotelDto> hotels;
			List<BookingDto> bookings;

			bool success = _hotelService.LoadData(out hotels, out bookings);

			if (!success)
			{
				Console.WriteLine("Failed to load JSONs files.");
				return;
			}

			Console.WriteLine("Check room availability");

			while (true)
			{
				var input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					break;
				}

				try
				{
					if (input.StartsWith("Availability"))
					{
						var args = ParseArguments(input);

						if (args.Length < 3)
						{
							Console.WriteLine("Invalid input format.");
							continue;
						}

						var hotelId = args[0];
						var roomType = args[2];
						var dates = args[1].Split('-');
						var startDate = DateTime.ParseExact(dates[0], "yyyyMMdd", null);
						DateTime endDate;
						if (dates.Length > 1)
						{
							endDate = DateTime.ParseExact(dates[1], "yyyyMMdd", null);
						}
						else
						{
							endDate = startDate;
						}

						Console.WriteLine(hotelId);
						Console.WriteLine(roomType);
						Console.WriteLine(startDate);
						Console.WriteLine(endDate);

					}
					else
					{
						Console.WriteLine("Unknown command.");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error: {ex.Message}");
				}
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