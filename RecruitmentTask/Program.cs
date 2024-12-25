using RecruitmentTask.Interfaces;
using RecruitmentTask.Services;

public class Program
{
	private static void Main(string[] args)
	{
		var service = new HotelService();
		var consoleInterface = new ConsoleInterface(service);

		consoleInterface.Run();
	}
}