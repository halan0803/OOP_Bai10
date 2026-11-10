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

Singleton sử dụng trạng thái dùng chung trong toàn bộ ứng dụng, có thể gây khó khăn cho việc kiểm thử độc lập và quản lý sự phụ thuộc giữa các thành phần.

Ngoài ra, khi bổ sung một loại thiết bị mới, DeviceFactory cần được cập nhật để hỗ trợ khởi tạo loại thiết bị đó.

## 4. Trả lời câu hỏi Phần G

**1. Factory đang che giấu quyết định thiết kế nào?**

Che giấu ánh xạ từ chuỗi loại thiết bị sang lớp triển khai cụ thể, cách gọi constructor và logic kiểm tra loại không hợp lệ. Client nhận `IDevice`, không cần biết nhánh `new SmartLight`, `new SmartFan`, `new SmartDoor`.

**2. Singleton bảo đảm điều gì, nhưng tạo ra rủi ro nào?**

Bảo đảm các lần gọi `HomeConfiguration.Instance` trong một tiến trình sử dụng cùng một đối tượng (với cách cài đặt bình thường), giúp thống nhất cấu hình. Rủi ro là trạng thái dùng chung, phụ thuộc toàn cục, test dễ ảnh hưởng lẫn nhau, khó thay thế bằng mock và khó chạy song song.

**3. Facade khác Factory ở mục đích chính như thế nào?**

Factory phục vụ **khởi tạo đối tượng**; Facade phục vụ **điều phối thao tác giữa các đối tượng đã tồn tại** theo quy trình mức cao. Facade không thay thế thiết bị hoặc Factory.

**4. Nếu bỏ SmartHomeFacade, client sẽ phải biết thêm chi tiết gì?**

Client phải giữ tham chiếu `IDevice` của đèn/quạt, `ILockable` của cửa; gọi `TurnOff/Lock/TurnOn` đúng thứ tự cho mỗi chế độ; tự truy cập cấu hình và in mức sáng. Logic sẽ bị lặp trong các giao diện/client khác nhau.