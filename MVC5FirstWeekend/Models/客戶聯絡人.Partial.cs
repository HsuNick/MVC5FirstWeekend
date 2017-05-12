namespace MVC5FirstWeekend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 驗證Email是否重複
            var 驗證信箱是否重複 = db.Database.SqlQuery<客戶聯絡人>("SELECT * FROM dbo.客戶聯絡人 WHERE Email=@p0 AND 客戶Id=@p1", Email, 客戶Id);

            if (驗證信箱是否重複.Count() > 0)
                yield return new ValidationResult("此Email已有人使用！", new string[] { "Email" });
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
