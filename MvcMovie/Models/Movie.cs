using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Display(Name = "Tiêu Đề")]
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Title { get; set; }
        [Display(Name = "Tóm tắt")]
        [StringLength(int.MaxValue, MinimumLength = 10)]
        public string Summary { get; set; }
        [Display(Name = "Ngày phát hành")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [CheckDateGreaterThanToday]
        public DateTime ReleaseDate { get; set; }
        [StringLength(10)]
        [Display(Name = "Thể loại")]
        [Genre]
        public string Genre { get; set; }
        [Display(Name = "Giá")]
        [DisplayFormat(DataFormatString = "{0:0,0} VNĐ")]
        [Range(5000, double.MaxValue)]
        public decimal Price { get; set; }
        [Display(Name = "Xếp hạng")]
        [Range(1, 5)]
        public double Rated { get; set; }
    }
    public class MoiveDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
    public class CheckDateGreaterThanTodayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Dữ liệu ngày phải lớn hơn ngày hôm nay");
        }
    }
    public class GenreAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string grenre = value.ToString();
            var danhSachTheLoai = new List<string>() { "Kiếm hiệp", "XHĐ", "Thiếu nhi" };
            if (danhSachTheLoai.Contains(grenre))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Nhập một trong ba loại Kiếm hiệp, XHĐ, Thiếu nhi");
}
    }
}