/********************************
Mã sinh viên: 202418931
Họ tên: Trần Thị Hà Lan
********************************/

using System;
namespace SmartHomeMini
{
    // Factory: nơi duy nhất quyết định cần tạo lớp thiết bị cụ thể nào
    public static class DeviceFactory
    {
        public static IDevice Create(string type, string name)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Device type cannot be empty.", nameof(type));

            string normalizedType = type.Trim().ToLowerInvariant();

            switch (normalizedType)
            {
                case "light":
                    return new SmartLight(name);
                case "fan":
                    return new SmartFan(name);
                case "door":
                    return new SmartDoor(name);
                default:
                    throw new ArgumentException(
                        $"Unsupported device type: {type}", nameof(type));
            }
        }
    }
}