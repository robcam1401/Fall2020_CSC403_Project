using Fall2020_CSC403_Project.code;
using Fall2020_CSC403_Project.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using NAudio.Wave;

namespace Fall2020_CSC403_Project {
  public partial class FrmBattle : Form {
    public static FrmBattle instance = null;
    private Enemy enemy;
    private Player player;
    private WaveOutEvent waveOut;
    private AudioFileReader audioFile;
   private SoundPlayer attackSound;
   private bool shieldActivated = false;


        private FrmBattle() {
      InitializeComponent();
      player = Game.player;
      PlayAudio("data/Bg.wav");
      attackSound = new SoundPlayer(Resources.Kamehameha); // Load the attack sound from resources

        }
        private void PlayAudio(string filePath)
        {
            waveOut = new WaveOutEvent();
            audioFile = new AudioFileReader(filePath);
            waveOut.Init(audioFile);
            waveOut.Play();
        }

        private void SetVolume(float volume)
        {
            if (waveOut != null)
            {
                waveOut.Volume = volume;
            }
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar)sender;
            float volume = trackBar.Value / 100f; // Convert to a scale of 0 to 1
            SetVolume(volume);
        }



    public void Setup() {
      // update for this enemy
      picEnemy.BackgroundImage = enemy.Img;
      picEnemy.Refresh();
      BackColor = enemy.Color;
      picBossBattle.Visible = false;

      // Observer pattern
      enemy.AttackEvent += PlayerDamage;
      player.AttackEvent += EnemyDamage;

      // show health
      UpdateHealthBars();
        
        }

    public void SetupForBossBattle() {
      picBossBattle.Location = Point.Empty;
      picBossBattle.Size = ClientSize;
      picBossBattle.Visible = true;

            //SoundPlayer simpleSound = new SoundPlayer(Resources.final_battle);
            //simpleSound.Play();

            //tmrFinalBattle.Enabled = true;

            string audioFilePath = "data/Bg.wav";
            waveOut = new WaveOutEvent();
            audioFile = new AudioFileReader(audioFilePath); waveOut.Init(audioFile); waveOut.Play();
            tmrFinalBattle.Enabled = true;

            trackBarVolume.ValueChanged += VolumeTrackBar_ValueChanged;
        }

        private void VolumeTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                float volume = trackBarVolume.Value / 100.0f;
                audioFile.Volume = volume;
            }
        }

        private void FrmBattle_FormClosing(object sender, FormClosingEventArgs e)
        {
            waveOut?.Dispose();
            audioFile?.Dispose();

        }

     public static FrmBattle GetInstance(Enemy enemy) {
      if (instance == null) {
        instance = new FrmBattle();
        instance.enemy = enemy;
        instance.Setup();
      }
      return instance;
    }

    private void UpdateHealthBars() {
      float playerHealthPer = player.Health / (float)player.MaxHealth;
      float enemyHealthPer = enemy.Health / (float)enemy.MaxHealth;

      const int MAX_HEALTHBAR_WIDTH = 226;

      lblPlayerHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * playerHealthPer);

      lblEnemyHealthFull.Width = (int)(MAX_HEALTHBAR_WIDTH * enemyHealthPer);

      lblPlayerHealthFull.Text = player.Health.ToString();
      lblEnemyHealthFull.Text = enemy.Health.ToString();
    }

    private void btnAttack_Click(object sender, EventArgs e) {

            //player.OnAttack(-4);
            if (shieldActivated)
            {
                if (player.Health > 0)
                {
                    player.OnAttack(-2);
                }
            }
            else
            {
                if(enemy.Health > 0)
                {
                    enemy.OnAttack(-2);
                }
                if(player.Health > 0)
                {
                    player.OnAttack(-4);
                }
            }
      
            attackSound.Play();
            shieldActivated = false;
       UpdateHealthBars();
      if (player.Health <= 0 || enemy.Health <= 0) {
        instance = null;
        Close();
      }
            //shieldActivated = false;
    }

    private void EnemyDamage(int amount) {
      enemy.AlterHealth(amount);
    }

    private void PlayerDamage(int amount) {
      player.AlterHealth(amount);
    }

        private void tmrFinalBattle_Tick(object sender, EventArgs e) {
            picBossBattle.Visible = false;
            tmrFinalBattle.Enabled = false;
        }
    //introduces an run button the closes the battle when pressed
    //keeping the player and enemy health the same
    private void btnRun_Click(object sender, EventArgs e) {
                instance = null;
                Close();
    }
       // private bool sheildActivated = false;
        private void btnShield_Click(object sender, EventArgs e)
        {
            btnShield.Enabled = false;
            shieldActivated = true; 
        }
    

        private void FrmBattle_Load(object sender, EventArgs e)
        {

        }
    }
 }

