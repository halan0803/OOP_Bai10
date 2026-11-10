**Mã sinh viên:** 202418931  
**Họ tên:** Trần Thị Hà Lan  

## 1. Giới thiệu

Chương trình Smart Home Mini mô phỏng hệ thống điều khiển nhà thông minh gồm ba thiết bị: SmartLight, SmartFan và SmartDoor.

Hệ thống áp dụng ba mẫu thiết kế Factory, Singleton và Facade để tạo thiết bị, quản lý cấu hình chung và điều khiển các chế độ hoạt động.

## 2. Vị trí sử dụng từng pattern và lợi ích đạt được

### Factory Pattern

- **Vị trí:** `DeviceFactory.cs`
- **Chức năng:** Tạo các đối tượng SmartLight, SmartFan và SmartDoor thông qua phương thức `Create()`.
- **Lợi ích:** Tách quá trình khởi tạo thiết bị khỏi client, giảm sự phụ thuộc vào các lớp cụ thể.

### Singleton Pattern

- **Vị trí:** `HomeConfiguration.cs`
- **Chức năng:** Quản lý cấu hình chung của ngôi nhà, bao gồm `HomeName` và `NightLightLevel`.
- **Lợi ích:** Đảm bảo các thành phần trong chương trình sử dụng cùng một đối tượng cấu hình.

### Facade Pattern

- **Vị trí:** `SmartHomeFacade.cs`
- **Chức năng:** Cung cấp hai phương thức `GoToWork()` và `GoToSleep()` để điều khiển nhiều thiết bị cùng lúc.
- **Lợi ích:** Đơn giản hóa việc điều khiển hệ thống, giúp client không cần gọi riêng từng thiết bị.

## 3. Một hạn chế của thiết kế