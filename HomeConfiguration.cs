/********************************
Mã sinh viên: 202418931
Họ tên: Trần Thị Hà Lan
********************************/

using System;
namespace SmartHomeMini
{
    // Singleton: chỉ tạo một HomeConfiguration trong quá trình chạy
    public sealed class HomeConfiguration
    {
        private static readonly Lazy<HomeConfiguration> lazy =
            new Lazy<HomeConfiguration>(() => new HomeConfiguration());

        public static HomeConfiguration Instance
        {
            get { return lazy.Value; }
        }

        public string HomeName { get; set; } = "My Home";
        public int NightLightLevel { get; set; } = 30;

        // Không cho client tự tạo HomeConfiguration bằng new
        private HomeConfiguration()
        {
        }
    }
}