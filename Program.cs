using RecruitmentTask.Interfaces;
using RecruitmentTask.Services;

var service = new HotelService();
var consoleInterface = new ConsoleInterface(service);

consoleInterface.Run();