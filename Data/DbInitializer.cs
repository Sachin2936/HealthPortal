using System;
using System.Collections.Generic;
using System.Linq;
using HealthTipsPortal.Models;

namespace HealthTipsPortal.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.HealthTips.Any())
            {
                Console.WriteLine("📦 Health tips already exist, skipping seed.");
                return;
            }

            Console.WriteLine("🌱 Seeding health tips now...");

            var tips = new List<HealthTip>
            {
                new HealthTip
                {
                    Title = "Stay Hydrated",
                    TipText = "Drink 8 glasses of water daily.",
                    Category = "General",
                    CreatedAt = DateTime.Now
                },
                new HealthTip
                {
                    Title = "Morning Walk",
                    TipText = "Walking 30 minutes each morning boosts your metabolism.",
                    Category = "Fitness",
                    CreatedAt = DateTime.Now
                },
                new HealthTip
                {
                    Title = "Eat Greens",
                    TipText = "Include leafy vegetables for essential nutrients.",
                    Category = "Diet",
                    CreatedAt = DateTime.Now
                }
            };

            context.HealthTips.AddRange(tips);
            context.SaveChanges();

            Console.WriteLine("✅ Health tips seeded successfully.");
        }
    }
}
