/********************************
Mã sinh viên: 202418931
Họ tên: Trần Thị Hà Lan
********************************/

using System;
namespace SmartHomeMini
{
    public class SmartLight : IDevice
    {
        private readonly string name;
        public bool IsOn { get; private set; }

        public SmartLight(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Device name cannot be empty.", nameof(name));

            this.name = name.Trim();
            IsOn = false;
        }

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
    }
}