using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllByShahar.DataAccess.Data;
using AllByShahar.DataAccess.Repository.IRepository;
using AllByShahar.Models;
using AllByShahar.Utility;
using System.Drawing;

namespace AllByShahar.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ICategoryRepository _dbCategory;
        private readonly ITypeItemRepository _dbTypeItem;
        private readonly IMenuItemRepository _dbMenuItem;




        public DbInitializer(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ICategoryRepository dbCategory,
            ITypeItemRepository dbTypeItem,
            IMenuItemRepository dbMenuItem)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _dbCategory = dbCategory;
            _dbTypeItem = dbTypeItem;
            _dbMenuItem = dbMenuItem;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.FrontDeskRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.ManagerRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.FrontDeskRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole)).GetAwaiter().GetResult();
                //הוספת מנהל
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@dotnetmastery.com",
                    Email = "admin@dotnetmastery.com",
                    EmailConfirmed = true,
                    FirstName = "Bhrugen",
                    LastName = "Patel"
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUser.FirstOrDefault(u => u.Email == "admin@dotnetmastery.com");

                _userManager.AddToRoleAsync(user, SD.ManagerRole).GetAwaiter().GetResult();

				//הוספת עובד דלפק
				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "desk@dotnetmastery.com",
					Email = "desk@dotnetmastery.com",
					EmailConfirmed = true,
					FirstName = "שחר",
					LastName = "כהן"
				}, "Admin123*").GetAwaiter().GetResult();

				ApplicationUser user2 = _db.ApplicationUser.FirstOrDefault(u => u.Email == "desk@dotnetmastery.com");

				_userManager.AddToRoleAsync(user2, SD.FrontDeskRole).GetAwaiter().GetResult();
				//הוספת לקוח
				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "Customer@dotnetmastery.com",
					Email = "Customer@dotnetmastery.com",
					EmailConfirmed = true,
					FirstName = "בני",
					LastName = "זקינוב"
				}, "Admin123*").GetAwaiter().GetResult();

				ApplicationUser user3 = _db.ApplicationUser.FirstOrDefault(u => u.Email == "Customer@dotnetmastery.com");

				_userManager.AddToRoleAsync(user3, SD.CustomerRole).GetAwaiter().GetResult();

				//הוספת תפריט קיים 
				//קטגוריות
				Category c1 =new Category();
                c1.Name = " איפור";
                c1.DisplayOrder = 1;
                Category c2 = new Category();
                c2.Name = "כלי כתיבה";
                c2.DisplayOrder = 2;
                Category c3 = new Category();
                c3.Name = "ביגוד";
                c3.DisplayOrder = 3;
                _dbCategory.Add(c1);
                _dbCategory.Add(c2);
                _dbCategory.Add(c3);
                _dbCategory.Save();
                //--------------------
                //הוספת סוגים
                TypeItem t1 = new TypeItem();
                t1.Name = "מסקרה";
                TypeItem t2 = new TypeItem();
                t2.Name = "סומק";
                TypeItem t3 = new TypeItem();
                t3.Name = "עטים";
                TypeItem t4 = new TypeItem();
                t4.Name = "מחקים";
                TypeItem t5 = new TypeItem();
                t5.Name = "חולצות";
                TypeItem t6 = new TypeItem();
                t6.Name = "כובעים";
                _dbTypeItem.Add(t1);    
                _dbTypeItem.Add(t2);
                _dbTypeItem.Add(t3);
                _dbTypeItem.Add(t4);
                _dbTypeItem.Add(t5);
                _dbTypeItem.Add(t6);
                _dbTypeItem.Save();
                //--------------------
                //הוספת פריטים
                MenuItem m1=new MenuItem();
                m1.Name = "MAYBELLINE -Sky High Mascara";
                m1.Description = "מסקרה SKY HIGH של מייבלין ניו-יורק, לאפקט הארכה מקסימלי ונפח מלא של הריסים";
                m1.Price = 50;
                m1.Image = "\\Images\\menuitems\\SKTGIGHT.png";
                m1.Category = c1;
                m1.TypeItem = t1;
                //--------------

                MenuItem m2 = new MenuItem();
                m2.Name = "סומק ושפתון קרמי - PALE PINK\r\n";
                m2.Description = "הסומק הפופולרי של בובי בראון בפורמולה קרמית, המשמש ללחיים ולשפתיים כאחד. מגיע באריזה קומפקטית עם מראה למריחה קלה בכל מקום";
                m2.Price = 153;
                m2.Image = @"\Images\menuitems\blush.jpg";
                m2.Category = c1;
                m2.TypeItem = t2;
                //--------------
                MenuItem m3 = new MenuItem();
                m3.Name = "עט דק";
                m3.Description = "עט שחור 0.5";
                m3.Price = 10;
                m3.Image = @"\Images\menuitems\PEN.png";
                m3.Category = c2;
                m3.TypeItem = t3;
                //--------------
                MenuItem m4 = new MenuItem();
                m4.Name = "מחק קטן- STAEDTLER";
                m4.Description = "מחק  איכותי לתיקון של שגיאות בטקסט שנכתב עם עפרון.\r\nהמחק עשוי מחומר חזק וידידותי לסביבה, אינו מכיל PVC או לטקס.";
                m4.Price = 5;
                m4.Image = "\\Images\\menuitems\\earaser.jpg";
                m4.Category = c2;
                m4.TypeItem = t4;
                //--------------
                MenuItem m5 = new MenuItem();
                m5.Name = "טישירט כותנה עם הדפס\r\n";
                m5.Description = "טישירט בגזרה קלאסית מג'רזי כותנה עם הדפס ופס ריב בצווארון.\r\n\r\n";
                m5.Price = 50;
                m5.Image = @"\Images\menuitems\TSHIRT.jpg";
                m5.Category = c3;
                m5.TypeItem = t5;
                //--------------
                MenuItem m6 = new MenuItem();
                m6.Name = "כובע בייסבול ";
                m6.Description = "כובע בייסבול פשוט לשני המינים מתכוונן צבע אחיד קל משקל כובעי אבא מזדמנים בחוץ ספורט ספורט כובע ספורט לנשים גברים\r\n.\r\n\r\n";
                m6.Price = 40;
                m6.Image = @"\Images\menuitems\HAT.jpg";
                m6.Category = c3;
                m6.TypeItem = t5;
                _dbMenuItem.Add(m1);
                _dbMenuItem.Add(m2);
                _dbMenuItem.Add(m3);
                 _dbMenuItem.Add(m4);
                _dbMenuItem.Add(m5);
                _dbMenuItem.Add(m6);
                _dbMenuItem.Save();




            }
            return;
        }
    }
}