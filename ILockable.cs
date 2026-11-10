/********************************
Mã sinh viên: 202418931
Họ tên: Trần Thị Hà Lan
********************************/

using System;
namespace SmartHomeMini
{
    // Chỉ thiết bị có khả năng khóa/mở khóa mới cần interface này
    public interface ILockable
    {
        void Lock();
        void Unlock();
    }
}