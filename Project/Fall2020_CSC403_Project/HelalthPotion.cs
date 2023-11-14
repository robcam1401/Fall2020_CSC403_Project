using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Fall2020_CSC403_Project
{
    public class HealthPotion : BattleCharacter

    {
        public Image Img { get; set; }


        public HealthPotion(Vector2 position, Collider collider, int healthAmount) : base(position, collider)
        {

        }

    }
}
