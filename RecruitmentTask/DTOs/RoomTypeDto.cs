namespace RecruitmentTask.DTOs
{
	public class RoomTypeDto
	{
		public string Code { get; set; }
		public string Description { get; set; }
		public List<string> Amenities { get; set; }
		public List<string> Features { get; set; }
	}
}