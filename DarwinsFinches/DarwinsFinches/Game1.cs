using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DarwinsFinches
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Texture2D background;
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static ContentManager content;
        Camera Cam;
        EnemySpawner spawner = new EnemySpawner();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            content = Content;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            ControllerInput.Instance();
            Avatar.Instance();
            Level level = new Level(10, 10, 60);
            level.InsertGameObject(Avatar.Instance());
            level.InsertGameObject(new Enemy(5,5,5,Attributes.AttackType.Melee,"Red",Attributes.Weapons.Axe,0,5));
            LevelManager.Instance().SetLevel(level);
            SoundManager.Instance();
            SoundManager.Instance().BGM(new SongTrack("BGM\\BossBattle"));
            SoundManager.Instance().BGM().Play();
            SoundManager.Instance().Volume(.1f);
            Utilities.Instance().StartWatch();

            background = content.Load<Texture2D>(@"Background\background-0");
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();


            Cam = new Camera(Avatar.Instance().kinetics.position, new Rectangle(0,0,background.Width, background.Height));
     
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (ControllerInput.Instance().GetButton(Buttons.A).Released)
            {

                this.Exit();
            }

            if (ControllerInput.Instance().GetButton(Buttons.Y).Released ||
                ControllerInput.Instance().GetKey(Keys.Space).Released)
            {
                LevelManager.Instance().getCurrentLevel().InsertGameObject(new Enemy(5, 5, 5, Attributes.AttackType.Melee, "Red", Attributes.Weapons.Axe, 0, 5));
                SoundManager.Instance().AddSound(new SFX("SFX\\arrowhit"));
            }

            ControllerInput.Instance().Update();

            LevelManager.Instance().Update();
            LevelManager.Instance().getCurrentLevel().Collision();
            SoundManager.Instance().Update();
            Cam.Update();
            Cam.SetTarget(Avatar.Instance().kinetics.position);
            spawner.Update();            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,Cam.View());
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            LevelManager.Instance().Draw(spriteBatch);
            spriteBatch.End();

            
            spriteBatch.Begin();
            Utilities.Instance().DrawTime(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
        public void Test()
        {
            /*
            GeneticAlgorithm GA = new GeneticAlgorithm();
            Enemy testEnemy = new Enemy();
            testEnemy = GA.GenerateRandomEnemy();
            Console.WriteLine("Enemy Stats :\nHP : " + testEnemy.HP +
                                           "\nRoundSpawned : " + testEnemy.RoundSpawned +
                                           "\nScore : " + testEnemy.Score +
                                           "\nSpeed : " + testEnemy.Speed +
                                           "\nWeapon : " + testEnemy.Weapon +
                                           "\nColor : " + testEnemy.EnemyColor);
            List<Enemy> testMutate = new List<Enemy>(GA.MutatedSample())*/
        }
    }
}
