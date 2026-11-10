/********************************
Mã sinh viên: 202418931
Họ tên: Trần Thị Hà Lan
********************************/

using System;
namespace SmartHomeMini
{
    public class SmartDoor : IDevice, ILockable
    {
        private readonly string name;
        public bool IsOn { get; private set; }
        public bool IsLocked { get; private set; }

        public SmartDoor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Device name cannot be empty.", nameof(name));

            this.name = name.Trim();
            IsOn = false;
            IsLocked = false;
        }

        // ON/OFF mô tả chế độ hoạt động của thiết bị,
        // KHÔNG đồng nghĩa với việc khóa/mở khóa
        public void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"{name}: ON");
        }

        public void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"{name}: OFF");
        }

        public void Lock()
        {
            IsLocked = true;
            Console.WriteLine($"{name}: LOCKED");
        }

        public void Unlock()
        {
            IsLocked = false;
            Console.WriteLine($"{name}: UNLOCKED");
        }
    }
}