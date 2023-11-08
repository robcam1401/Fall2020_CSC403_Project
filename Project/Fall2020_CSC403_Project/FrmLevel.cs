using Fall2020_CSC403_Project.code;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Fall2020_CSC403_Project {
  public partial class FrmLevel : Form {
    private Player player;

    private Enemy enemyPoisonPacket;
    private Enemy bossKoolaid;
    private Enemy enemyCheeto;
    private Character[] walls;

    private DateTime timeBegin;
    private FrmBattle frmBattle;
        private SoundPlayer backgroundMusic;

    public FrmLevel() {
      InitializeComponent();
      backgroundMusic = new SoundPlayer("data/Bg.wav");
        }

    private void FrmLevel_Load(object sender, EventArgs e) {
      const int PADDING = 7;
      const int NUM_WALLS = 13;

      player = new Player(CreatePosition(picPlayer), CreateCollider(picPlayer, PADDING));
      bossKoolaid = new Enemy(CreatePosition(picBossKoolAid), CreateCollider(picBossKoolAid, PADDING));
      enemyPoisonPacket = new Enemy(CreatePosition(picEnemyPoisonPacket), CreateCollider(picEnemyPoisonPacket, PADDING));
      enemyCheeto = new Enemy(CreatePosition(picEnemyCheeto), CreateCollider(picEnemyCheeto, PADDING));

      bossKoolaid.Img = picBossKoolAid.BackgroundImage;
      enemyPoisonPacket.Img = picEnemyPoisonPacket.BackgroundImage;
      enemyCheeto.Img = picEnemyCheeto.BackgroundImage;

      bossKoolaid.Color = Color.Red;
      enemyPoisonPacket.Color = Color.Green;
      enemyCheeto.Color = Color.FromArgb(255, 245, 161);
      backgroundMusic.PlayLooping();
      walls = new Character[NUM_WALLS];
      for (int w = 0; w < NUM_WALLS; w++) {
        PictureBox pic = Controls.Find("picWall" + w.ToString(), true)[0] as PictureBox;
        walls[w] = new Character(CreatePosition(pic), CreateCollider(pic, PADDING));
      }

      Game.player = player;
      timeBegin = DateTime.Now;
    }

        public void StartBackgroundMusic()
        {
            backgroundMusic.PlayLooping();
        }

        private Vector2 CreatePosition(PictureBox pic) {
      return new Vector2(pic.Location.X, pic.Location.Y);
    }

    private Collider CreateCollider(PictureBox pic, int padding) {
      Rectangle rect = new Rectangle(pic.Location, new Size(pic.Size.Width - padding, pic.Size.Height - padding));
      return new Collider(rect);
    }

    private void FrmLevel_KeyUp(object sender, KeyEventArgs e) {
      player.ResetMoveSpeed();
    }

    private void tmrUpdateInGameTime_Tick(object sender, EventArgs e) {
      TimeSpan span = DateTime.Now - timeBegin;
      string time = span.ToString(@"hh\:mm\:ss");
      lblInGameTime.Text = "Time: " + time.ToString();
            UpdateHealthBars();
    }
        //updates the player healh bar in the level screen.
        private void UpdateHealthBars() {
            float playerHealthPer = player.Health / (float)player.MaxHealth;

            const int MAX_HEALTHBAR_LEVEL_WIDTH = 226;

            lblPlayerHealthFullLevel.Width = (int)(MAX_HEALTHBAR_LEVEL_WIDTH * playerHealthPer);

            lblPlayerHealthFullLevel.Text = player.Health.ToString();
        }
    //code review: Co'Niya
    // code works, it checks if enemy's health is 0 and if so it makes their existence null meaning they disappear from the screen.
    //I think there was a bug where if the player died and you click on the enemy again and attacked it still disappeared.
    private void tmrPlayerMove_Tick(object sender, EventArgs e) {
      // move player
      player.Move();
            //checks for a specific enemy's health and if that health is zero 
            //the player image will be removed from the level window and
            //the picture for the enemy will be set to null
            if (enemyPoisonPacket.Health <= 0)
            {
                Controls.Remove(picEnemyPoisonPacket);
                picEnemyPoisonPacket = null;
            }
            if (enemyCheeto.Health <= 0)
            {
                Controls.Remove(picEnemyCheeto);
                picEnemyCheeto = null;
            }
            if (bossKoolaid.Health <= 0)
            {
                Controls.Remove(picBossKoolAid);
                picBossKoolAid = null;
            }

            // check collision with walls
            if (HitAWall(player)) {
        player.MoveBack();
      }

      // check collision with enemies
      //checks for a null case for the enemys
      //if null case is found the battle screen will not commence
      if (HitAChar(player, enemyPoisonPacket)) {
        if(picEnemyPoisonPacket != null) { 
            Fight(enemyPoisonPacket);
        }
      }
      else if (HitAChar(player, enemyCheeto)) {
                if(picEnemyCheeto != null) { 
        Fight(enemyCheeto);
            }
      }
      if (HitAChar(player, bossKoolaid)) {
                if(picBossKoolAid != null) {
        Fight(bossKoolaid);
                    }
      }

      // update player's picture box
      picPlayer.Location = new Point((int)player.Position.x, (int)player.Position.y);
    }

    private bool HitAWall(Character c) {
      bool hitAWall = false;
      for (int w = 0; w < walls.Length; w++) {
        if (c.Collider.Intersects(walls[w].Collider)) {
          hitAWall = true;
          break;
        }
      }
      return hitAWall;
    }

    private bool HitAChar(Character you, Character other) {
      return you.Collider.Intersects(other.Collider);
    }

    private void Fight(Enemy enemy) {
      player.ResetMoveSpeed();
      player.MoveBack();
      frmBattle = FrmBattle.GetInstance(enemy);
      frmBattle.Show();

      if (enemy == bossKoolaid) {
        frmBattle.SetupForBossBattle();
      }
      
    }
        


    private void FrmLevel_KeyDown(object sender, KeyEventArgs e) {
      switch (e.KeyCode) {
        case Keys.Left:
          player.GoLeft();
          break;

        case Keys.Right:
          player.GoRight();
          break;

        case Keys.Up:
          player.GoUp();
          break;

        case Keys.Down:
          player.GoDown();
          break;

        default:
          player.ResetMoveSpeed();
          break;
      }
    }

    private void lblInGameTime_Click(object sender, EventArgs e) {

    }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
