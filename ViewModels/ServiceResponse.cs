namespace BangLuong.ViewModels
{
    public class ServiceResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        // Danh sách lỗi trả về
        public List<string> Errors { get; set; } = new List<string>();
    }
}
