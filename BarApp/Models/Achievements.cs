using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarApp.Models
{
    public class Achievements
    {
        public int AchievementsId { get; set; }
        public string name { get; set; }
        public string qualifications { get; set; }
        public DateTime beginTime { get; set; }
        public DateTime expireTime { get; set; }
        public string description { get; set; }
    }
}
