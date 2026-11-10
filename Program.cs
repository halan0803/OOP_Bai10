/********************************
Mã sinh viên: 202418931
Họ tên: Trần Thị Hà Lan
********************************/

using System;
using System.IO;
namespace SmartHomeMini
{
    public static class Program
    {
        private static int passed;
        private static int failed;

        public static void Main(string[] args)
        {
            Console.WriteLine("===== SMART HOME MINI TESTS =====");

            bool allPassed = RunAll();

            if (!allPassed)
            {
                Environment.ExitCode = 1;
            }
        }

        public static bool RunAll()
        {
            passed = 0;
            failed = 0;

            Run("T1", "Factory creates 3 correct types", TestFactoryTypes);
            Run("T2", "Factory rejects unsupported camera", TestFactoryInvalidType);
            Run("T3", "Singleton returns the same reference", TestSingleton);
            Run("T4", "GoToWork turns off light/fan and locks door", TestGoToWork);
            Run("T5", "GoToSleep sets states and prints light level", TestGoToSleep);
            Run("T6", "Factory ignores case and extra spaces", TestFactoryCaseInsensitive);
            Run("T7", "Only the door can lock and unlock", TestDoorLockable);

            Console.WriteLine();
            Console.WriteLine($"RESULT: {passed} passed, {failed} failed.");

            return failed == 0;
        }

        private static void Run(string id, string description, Action test)
        {
            try
            {
                test();
                passed++;
                Console.WriteLine($"[PASS] {id}: {description}");
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"[FAIL] {id}: {ex.Message}");
            }
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception(message);
            }
        }

        // T1: Kiểm tra Factory tạo đúng loại thiết bị
        private static void TestFactoryTypes()
        {
            IDevice light = DeviceFactory.Create("light", "Light");
            IDevice fan = DeviceFactory.Create("fan", "Fan");
            IDevice door = DeviceFactory.Create("door", "Door");

            Assert(light is SmartLight, "light was not SmartLight");
            Assert(fan is SmartFan, "fan was not SmartFan");
            Assert(door is SmartDoor, "door was not SmartDoor");
        }

        // T2: Kiểm tra Factory với loại thiết bị không hợp lệ
        private static void TestFactoryInvalidType()
        {
            bool thrown = false;

            try
            {
                DeviceFactory.Create("camera", "Camera");
            }
            catch (ArgumentException)
            {
                thrown = true;
            }

            Assert(thrown, "Expected ArgumentException for camera");
        }

        // T3: Kiểm tra Singleton
        private static void TestSingleton()
        {
            HomeConfiguration config1 = HomeConfiguration.Instance;
            HomeConfiguration config2 = HomeConfiguration.Instance;

            Assert(
                ReferenceEquals(config1, config2),
                "Different Singleton instances"
            );
        }

        // T4: Kiểm tra chế độ GoToWork
        private static void TestGoToWork()
        {
            // Khởi tạo thiết bị thông qua Factory
            IDevice light = DeviceFactory.Create("light", "Work light");
            IDevice fan = DeviceFactory.Create("fan", "Work fan");
            IDevice door = DeviceFactory.Create("door", "Work door");

            // Thiết lập trạng thái ban đầu
            light.TurnOn();
            fan.TurnOn();
            ((ILockable)door).Unlock();

            // Khởi tạo Facade
            SmartHomeFacade facade =
                new SmartHomeFacade(light, fan, (ILockable)door);

            // Thực hiện chế độ đi làm
            facade.GoToWork();

            // Kiểm tra kết quả
            Assert(!((SmartLight)light).IsOn, "Light must be OFF");
            Assert(!((SmartFan)fan).IsOn, "Fan must be OFF");
            Assert(((SmartDoor)door).IsLocked, "Door must be LOCKED");
        }

        // T5: Kiểm tra chế độ GoToSleep
        private static void TestGoToSleep()
        {
            // Khởi tạo thiết bị thông qua Factory
            IDevice light = DeviceFactory.Create("light", "Sleep light");
            IDevice fan = DeviceFactory.Create("fan", "Sleep fan");
            IDevice door = DeviceFactory.Create("door", "Sleep door");

            // Thiết lập trạng thái ban đầu
            fan.TurnOn();

            HomeConfiguration config = HomeConfiguration.Instance;
            int oldLevel = config.NightLightLevel;
            config.NightLightLevel = 30;

            // Khởi tạo Facade
            SmartHomeFacade facade =
                new SmartHomeFacade(light, fan, (ILockable)door);

            // Lưu kết quả in ra Console
            StringWriter output = new StringWriter();
            TextWriter original = Console.Out;

            try
            {
                Console.SetOut(output);
                facade.GoToSleep();
            }
            finally
            {
                Console.SetOut(original);
                config.NightLightLevel = oldLevel;
            }

            // Kiểm tra trạng thái các thiết bị
            Assert(((SmartLight)light).IsOn, "Night light must be ON");
            Assert(!((SmartFan)fan).IsOn, "Fan must be OFF");
            Assert(((SmartDoor)door).IsLocked, "Door must be LOCKED");

            // Kiểm tra mức sáng được thông báo
            Assert(
                output.ToString().Contains("Night light level: 30%"),
                "Night light level must be printed"
            );
        }

        // T6: Kiểm tra Factory không phân biệt chữ hoa/thường
        private static void TestFactoryCaseInsensitive()
        {
            IDevice device =
                DeviceFactory.Create("  LiGhT  ", "Case light");

            Assert(
                device is SmartLight,
                "Type normalization did not work"
            );
        }

        // T7: Kiểm tra khả năng khóa/mở khóa của cửa
        private static void TestDoorLockable()
        {
            IDevice light = DeviceFactory.Create("light", "Plain light");
            IDevice door = DeviceFactory.Create("door", "Secure door");

            // Đèn không được hỗ trợ khóa
            Assert(!(light is ILockable), "Light must not be lockable");

            // Cửa phải hỗ trợ khóa
            Assert(door is ILockable, "Door must implement ILockable");

            ILockable lockable = (ILockable)door;
            SmartDoor concreteDoor = (SmartDoor)door;

            // Kiểm tra Lock()
            lockable.Lock();
            Assert(concreteDoor.IsLocked, "Lock() did not lock the door");

            // Kiểm tra Unlock()
            lockable.Unlock();
            Assert(!concreteDoor.IsLocked, "Unlock() did not unlock the door");
        }
    }
}