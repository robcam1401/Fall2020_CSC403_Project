using Fall2020_CSC403_Project.code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Fall2020_CSC403_Project
{
    public class Weapon : BattleCharacter

    {
        public Image Img { get; set; }


        public Weapon(Vector2 position, Collider collider) : base(position, collider)
        {

        }
    }
}
