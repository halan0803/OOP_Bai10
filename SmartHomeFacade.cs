/********************************
Mã sinh viên: 202418931
Họ tên: Trần Thị Hà Lan
********************************/

using System;
namespace SmartHomeMini
{
    // Facade: gom nhiều lệnh thiết bị thành một thao tác mức cao
    public sealed class SmartHomeFacade
    {
        private readonly IDevice light;
        private readonly IDevice fan;
        private readonly ILockable door;

        public SmartHomeFacade(IDevice light, IDevice fan, ILockable door)
        {
            if (light == null) throw new ArgumentNullException(nameof(light));
            if (fan == null) throw new ArgumentNullException(nameof(fan));
            if (door == null) throw new ArgumentNullException(nameof(door));

            this.light = light;
            this.fan = fan;
            this.door = door;
        }

        public void GoToWork()
        {
            Console.WriteLine("[GoToWork]");
            light.TurnOff();
            fan.TurnOff();
            door.Lock();
        }

        public void GoToSleep()
        {
            Console.WriteLine("[GoToSleep]");
            fan.TurnOff();
            door.Lock();
            light.TurnOn();

            int level = HomeConfiguration.Instance.NightLightLevel;
            Console.WriteLine($"Night light level: {level}%");
        }
    }
}