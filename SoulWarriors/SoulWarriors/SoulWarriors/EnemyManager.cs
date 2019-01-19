using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SoulWarriors
{
    class EnemyManager
    {
        private Texture2D texture;
        private Rectangle initialFrame;
        private int frameCount;

        public List<Enemy> Enemies = new List<Enemy>();

        //public ShootManager EnemyShotManager;
        //private PlayeManager playerManager;
        //private Player2 player2;

        public int MinShipsPerWave = 5;
        public int MaxShipsPerWave = 8;
        private float nextWaveTimer = 0.0f;
        private float nextWaveMinTimer = 8.0f;
        private float shipSpawnTimer = 0.0f;
        private float shipSpawnWaitTime = 0.5f;

        //private float shipShotChance = 0.2f;

        private List<List<Vector2>> pathWaypoints =
            new List<List<Vector2>>();

        private Dictionary<int, int> waveSpawns = new Dictionary<int, int>();

        public bool Active = true;

        private Random rand = new Random();

        //waypoints cordinats were the enemy will go
        private void setUpWaypoints()
        {
            List<Vector2> path0 = new List<Vector2>();
            path0.Add(new Vector2(850, 300));
            path0.Add(new Vector2(-100, 300));
            pathWaypoints.Add(path0);
            waveSpawns[0] = 0;

            List<Vector2> path1 = new List<Vector2>();
            path1.Add(new Vector2(-50, 225));
            path1.Add(new Vector2(850, 225));
            pathWaypoints.Add(path1);
            waveSpawns[1] = 0;

            List<Vector2> path2 = new List<Vector2>();
            path2.Add(new Vector2(-100, 50));
            path2.Add(new Vector2(150, 50));
            path2.Add(new Vector2(200, 75));
            path2.Add(new Vector2(200, 125));
            path2.Add(new Vector2(150, 150));
            path2.Add(new Vector2(150, 175));
            path2.Add(new Vector2(200, 200));
            path2.Add(new Vector2(600, 200));
            path2.Add(new Vector2(850, 600));
            pathWaypoints.Add(path2);
            waveSpawns[2] = 0;

            List<Vector2> path3 = new List<Vector2>();
            path3.Add(new Vector2(600, -100));
            path3.Add(new Vector2(600, 250));
            path3.Add(new Vector2(580, 275));
            path3.Add(new Vector2(500, 250));
            path3.Add(new Vector2(500, 200));
            path3.Add(new Vector2(450, 175));
            path3.Add(new Vector2(400, 150));
            path3.Add(new Vector2(-100, 150));
            pathWaypoints.Add(path3);
            waveSpawns[3] = 0;
        }

        public EnemyManager(
            Texture2D texture,
            Rectangle initialFrame,
            int frameCount,
            //PlayeManager playerSprite,
            //Player2 player2,
            Rectangle screemBounds)
        {
            this.texture = texture;
            this.initialFrame = initialFrame;
            this.frameCount = frameCount;
            //this.playerManager = playerSprite;
            //this.player2 = player2;

            //EnemyShotManager = new ShootManager(
            //    texture,
            //    new Rectangle(517, 1, 5, 5),
            //    4,
            //    2,
            //    150f,
            //    screemBounds);

            setUpWaypoints();
        }
        //Spawn an enemy in a choosen path
        public void SpawnEnemy(int path)
        {
            Enemy thisEnemy = new Enemy(
                texture,
                pathWaypoints[path][0],
                initialFrame,
                frameCount);
            for (int i = 0; i < pathWaypoints[path].Count(); i++)
            {
                thisEnemy.AddWaypoint(pathWaypoints[path][i]);
            }
            Enemies.Add(thisEnemy);
        }
        // Determains how many enemies will spawn
        public void SpawnWave(int waveType)
        {
            waveSpawns[waveType] +=
                rand.Next(MinShipsPerWave, MaxShipsPerWave + 1);
        }
        //The time between each spawn of enemies
        private void updateWaveSpawns(GameTime gameTime)
        {
            shipSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shipSpawnTimer > shipSpawnWaitTime)
            {
                for (int i = waveSpawns.Count - 1; i >= 0; i--)
                {
                    if (waveSpawns[i] > 0)
                    {
                        waveSpawns[i]--;
                        SpawnEnemy(i);
                    }
                }
                shipSpawnTimer = 0f;
            }
            //The time between each wave
            nextWaveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (nextWaveTimer > nextWaveMinTimer)
            {
                SpawnWave(rand.Next(0, pathWaypoints.Count));
                nextWaveTimer = 0f;
            }
        }

        public void Update(GameTime gameTime)
        {
            //EnemyShotManager.Update(gameTime);

            //for (int i = Enemies.Count - 1; i >= 0; i--)
            //{
            //    Enemies[i].Update(gameTime);
            //    if (Enemies[i].IsActive() == false)
            //    {
            //        Enemies.RemoveAt(i);
            //    }
            //    else
            //    {
            //        if ((float)rand.Next(0, 1000) / 10 <= shipShotChance)
            //        {
            //            Vector2 fireLoc = Enemies[i].EnemySprite.Position;
            //            fireLoc += Enemies[i].gunOffset;

            //            Vector2 shotDirection =
            //                playerManager.Position -
            //                fireLoc;

            //            Vector2 shotDirection2 =
            //                player2.Position -
            //                fireLoc;

            //            shotDirection.Normalize();

            //            EnemyShotManager.Fireshot(
            //                fireLoc,
            //                shotDirection,
            //                false);
            //        }
            //    }
            //}
            //if (Active)
            //{
            //    updateWaveSpawns(gameTime);
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //EnemyShotManager.Draw(spriteBatch);

            //foreach (Enemy enemy in Enemies)
            //{
            //    enemy.Draw(spriteBatch);
            //}
        }
    }
}
