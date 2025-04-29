namespace BookingRoom.DTOs
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        // Lấy thông tin tên loại phòng thay vì object RoomType
        public string RoomTypeName { get; set; }
    }
}
