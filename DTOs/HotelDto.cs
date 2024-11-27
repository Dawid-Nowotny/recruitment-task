﻿namespace RecruitmentTask.DTOs
{
	public class HotelDto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public List<RoomTypeDto> RoomTypes { get; set; }
		public List<RoomDto> Rooms { get; set; }
	}
}