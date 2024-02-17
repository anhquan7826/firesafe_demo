using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Province
{
    public required int ProvinceId { get; init; }

    [MaxLength(30)] public required string Name { get; init; }

    public static void ConfigureRelationship(EntityTypeBuilder<Province> builder)
    {
        builder.HasKey(p => p.ProvinceId);
    }

    public static void ConfigureInitialData(EntityTypeBuilder<Province> builder)
    {
        builder.HasData(
            new Province { ProvinceId = 1, Name = "An Giang" },
            new Province { ProvinceId = 2, Name = "Bà Rịa – Vũng Tàu" },
            new Province { ProvinceId = 3, Name = "Bắc Giang" },
            new Province { ProvinceId = 4, Name = "Bắc Kạn" },
            new Province { ProvinceId = 5, Name = "Bạc Liêu" },
            new Province { ProvinceId = 6, Name = "Bắc Ninh" },
            new Province { ProvinceId = 7, Name = "Bến Tre" },
            new Province { ProvinceId = 8, Name = "Bình Định" },
            new Province { ProvinceId = 9, Name = "Bình Dương" },
            new Province { ProvinceId = 10, Name = "Bình Phước" },
            new Province { ProvinceId = 11, Name = "Bình Thuận" },
            new Province { ProvinceId = 12, Name = "Cà Mau" },
            new Province { ProvinceId = 13, Name = "Cần Thơ" },
            new Province { ProvinceId = 14, Name = "Cao Bằng" },
            new Province { ProvinceId = 15, Name = "Đà Nẵng" },
            new Province { ProvinceId = 16, Name = "Đắk Lắk" },
            new Province { ProvinceId = 17, Name = "Đắk Nông" },
            new Province { ProvinceId = 18, Name = "Điện Biên" },
            new Province { ProvinceId = 19, Name = "Đồng Nai" },
            new Province { ProvinceId = 20, Name = "Đồng Tháp" },
            new Province { ProvinceId = 21, Name = "Gia Lai" },
            new Province { ProvinceId = 22, Name = "Hà Giang" },
            new Province { ProvinceId = 23, Name = "Hà Nam" },
            new Province { ProvinceId = 24, Name = "Hà Nội" },
            new Province { ProvinceId = 25, Name = "Hà Tĩnh" },
            new Province { ProvinceId = 26, Name = "Hải Dương" },
            new Province { ProvinceId = 27, Name = "Hải Phòng" },
            new Province { ProvinceId = 28, Name = "Hậu Giang" },
            new Province { ProvinceId = 29, Name = "Hòa Bình" },
            new Province { ProvinceId = 30, Name = "Hưng Yên" },
            new Province { ProvinceId = 31, Name = "Khánh Hòa" },
            new Province { ProvinceId = 32, Name = "Kiên Giang" },
            new Province { ProvinceId = 33, Name = "Kon Tum" },
            new Province { ProvinceId = 34, Name = "Lai Châu" },
            new Province { ProvinceId = 35, Name = "Lâm Đồng" },
            new Province { ProvinceId = 36, Name = "Lạng Sơn" },
            new Province { ProvinceId = 37, Name = "Lào Cai" },
            new Province { ProvinceId = 38, Name = "Long An" },
            new Province { ProvinceId = 39, Name = "Nam Định" },
            new Province { ProvinceId = 40, Name = "Nghệ An" },
            new Province { ProvinceId = 41, Name = "Ninh Bình" },
            new Province { ProvinceId = 42, Name = "Ninh Thuận" },
            new Province { ProvinceId = 43, Name = "Phú Thọ" },
            new Province { ProvinceId = 44, Name = "Phú Yên" },
            new Province { ProvinceId = 45, Name = "Quảng Bình" },
            new Province { ProvinceId = 46, Name = "Quảng Nam" },
            new Province { ProvinceId = 47, Name = "Quảng Ngãi" },
            new Province { ProvinceId = 48, Name = "Quảng Ninh" },
            new Province { ProvinceId = 49, Name = "Quảng Trị" },
            new Province { ProvinceId = 50, Name = "Sóc Trăng" },
            new Province { ProvinceId = 51, Name = "Sơn La" },
            new Province { ProvinceId = 52, Name = "Tây Ninh" },
            new Province { ProvinceId = 53, Name = "Thái Bình" },
            new Province { ProvinceId = 54, Name = "Thái Nguyên" },
            new Province { ProvinceId = 55, Name = "Thanh Hóa" },
            new Province { ProvinceId = 56, Name = "Thừa Thiên Huế" },
            new Province { ProvinceId = 57, Name = "Tiền Giang" },
            new Province { ProvinceId = 58, Name = "Thành phố Hồ Chí Minh" },
            new Province { ProvinceId = 59, Name = "Trà Vinh" },
            new Province { ProvinceId = 60, Name = "Tuyên Quang" },
            new Province { ProvinceId = 61, Name = "Vĩnh Long" },
            new Province { ProvinceId = 62, Name = "Vĩnh Phúc" },
            new Province { ProvinceId = 63, Name = "Yên Bái" }
        );
    }
}