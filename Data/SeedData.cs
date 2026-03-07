using dotnetDeneme.Models;
using Microsoft.AspNetCore.Identity;

namespace dotnetDeneme.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Identity servislerini çağırıyoruz
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>(); // Kendi user sınıfınız varsa (örn: AppUser) burayı değiştirin.

            // 1. Rolleri Kontrol Et ve Ekle
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                // Rol veritabanında var mı diye bakıyoruz
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Yoksa oluşturuyoruz (Böylece ID sadece ilk seferinde atanır ve sabit kalır)
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Admin Kullanıcısını Kontrol Et ve Ekle
            string adminEmail = "admin@example.com"; // Kendi admin mailinizi yazın
            string adminPassword = "Admin_123456"; // Identity'nin şifre kurallarına uygun olmalı

            // E-posta adresiyle kullanıcıyı arıyoruz

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                AppUser adminUser = new()
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    EmailConfirmed = true // Şifre sıfırlama vb. için genelde true verilir
                };

                var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    // Kullanıcı başarıyla oluşturulduysa Admin rolünü atıyoruz
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
