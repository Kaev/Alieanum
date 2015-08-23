using System;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace LudumDare33ByKaev
{
    class Game
    {
        public enum State
        {
            Start,
            Playing,
            GameOver
        }

        RenderWindow m_Window;
        float m_EnemyTimer;
        float m_EnemyRocketTimer;
        float m_PlayerProjectileTimer;
        float m_PlayerProjectileSideTimer;
        
        State m_State = new State();

        public static ObjectManager ObjectManager = new ObjectManager();

        public static CollisionManager CollisionManager = new CollisionManager();

        public static ItemManager ItemManager = new ItemManager();

        public static SoundManager SoundManager = new SoundManager();

        public static uint WindowHeight = 600;

        public static uint WindowWidth = 800;

        void OnClosed(object sender, EventArgs e)
        {
            m_Window.Close();
        }

        public void Run()
        {
            m_Window = new RenderWindow(new VideoMode(WindowWidth, WindowHeight), "Ludum Dare 33 Entry by Kaev");
            m_Window.SetVisible(true);
            m_Window.SetFramerateLimit(60);
            m_Window.Closed += new EventHandler(OnClosed);

            Background background0 = new Background(0);
            background0.Position = new Vector2f(-WindowWidth, -WindowHeight);
            ObjectManager.AddObject("bg0", background0);

            Background background1 = new Background(1);
            background1.Position = new Vector2f(WindowWidth + background1.Sprite.GetGlobalBounds().Width, WindowHeight / 2);
            ObjectManager.AddObject("bg1", background1);

            Clock clock = new Clock();

            Player player = new Player();
            ObjectManager.AddObject("player", player);

            StartMenu start = new StartMenu();
            ObjectManager.AddObject("start", start);

            MainMenu menu = new MainMenu();
            ObjectManager.AddObject("menu", menu);

            GameOverMenu gameOverMenu = new GameOverMenu();
            ObjectManager.AddObject("gameover", gameOverMenu);

            Random rand = new Random();

            m_State = State.Start;

            while (m_Window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                m_Window.DispatchEvents();

                switch (m_State)
                {
                    case State.Start:
                        m_Window.Clear(Color.Black);
                        ObjectManager.UpdateObjects();
                        ObjectManager.DrawObjects(m_Window);

                        if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                        {
                            start.Visible = false;
                            menu.Visible = true;
                            m_State = State.Playing;
                        }

                        m_Window.Display();
                        break;
                    case State.Playing:
                        m_Window.Clear(Color.Black);

                        Player playerObject = (Player)ObjectManager.GetObject("player");
                        if (playerObject.AlreadyHit)
                        {
                            gameOverMenu.Visible = true;
                            m_State = State.GameOver;
                        }

                        m_PlayerProjectileTimer += ObjectManager.GetTimeDelta();
                        m_PlayerProjectileSideTimer += ObjectManager.GetTimeDelta();

                        if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                        {
                            if (m_PlayerProjectileTimer > 0.65f)
                                if (ItemManager.PlayerHasItem[0])
                                {
                                    PlayerProjectileLaser bullet = new PlayerProjectileLaser();
                                    bullet.Position = new Vector2f(player.Position.X + player.Width / 2 - bullet.Width / 2, player.Position.Y);
                                    string name = "playerProjectileLaser_" + rand.Next().ToString();
                                    ObjectManager.AddObject(name, bullet);
                                    SoundManager.PlaySound("laser");
                                    m_PlayerProjectileTimer = 0;
                                }
                                else
                                {
                                    PlayerProjectile bullet = new PlayerProjectile();
                                    bullet.Position = new Vector2f(player.Position.X + player.Width / 2 - bullet.Width / 2, player.Position.Y);
                                    string name = "playerProjectile_" + rand.Next().ToString();
                                    ObjectManager.AddObject(name, bullet);
                                    SoundManager.PlaySound("laser");
                                    m_PlayerProjectileTimer = 0;
                                }

                            if (ItemManager.PlayerHasItem[1])
                                if (m_PlayerProjectileSideTimer > 1f)
                                {
                                    PlayerProjectileSide bullet = new PlayerProjectileSide();
                                    bullet.Position = new Vector2f(player.Position.X + player.Width / 2 - bullet.Width / 2, player.Position.Y + bullet.Height / 2);
                                    string name = "playerProjectileSide_" + rand.Next().ToString();
                                    ObjectManager.AddObject(name, bullet);
                                    m_PlayerProjectileSideTimer = 0;
                                }
                        }

                        m_EnemyTimer += ObjectManager.GetTimeDelta();
                        m_EnemyRocketTimer += ObjectManager.GetTimeDelta();

                        if (m_EnemyTimer > 1.75f)
                        {
                            Enemy enemy = new Enemy();
                            enemy.Position += new Vector2f((float)rand.NextDouble() * WindowWidth, 0 - enemy.Height);
                            string name = "enemy_" + rand.Next().ToString();
                            ObjectManager.AddObject(name, enemy);
                            m_EnemyTimer = 0;
                        }
                        if (m_EnemyRocketTimer > 2.8f)
                        {
                            EnemyRocket enemyRocket;
                            // Spawn rocket left
                            if (rand.Next(2) == 0)
                            {
                                enemyRocket = new EnemyRocket(false);
                                enemyRocket.Sprite.Scale = new Vector2f(-1f, 1f);
                                enemyRocket.Position += new Vector2f(0, (float)rand.NextDouble() * WindowHeight);
                            }
                            else // Spawn rocket right
                            {
                                enemyRocket = new EnemyRocket(true);
                                enemyRocket.Position += new Vector2f(WindowWidth, (float)rand.NextDouble() * WindowHeight);
                            }
                            string name = "enemyRocket_" + rand.Next().ToString();
                            ObjectManager.AddObject(name, enemyRocket);
                            m_EnemyRocketTimer = 0;
                        }

                        foreach (KeyValuePair<string, GameObject> pair in ObjectManager.GetAllObjects().ToList())
                        {
                            Enemy enemy = pair.Value as Enemy;
                            if (enemy != null)
                                if (enemy.Position.Y > WindowHeight)
                                    ObjectManager.RemoveObject(pair.Key);

                            EnemyProjectile enemyProjectile = pair.Value as EnemyProjectile;
                            if (enemyProjectile != null)
                                if (enemyProjectile.Position.Y > WindowHeight)
                                    ObjectManager.RemoveObject(pair.Key);
                        }

                        ObjectManager.UpdateObjects();
                        ObjectManager.DrawObjects(m_Window);

                        m_Window.Display();

                        break;
                    case State.GameOver:
                        if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
                        {
                            Player playerObj = (Player)ObjectManager.GetObject("player");
                            playerObj.ResetPosition();
                            playerObj.ResetPlayerState();

                            ObjectManager.Reset();

                            m_PlayerProjectileTimer = 0;
                            m_EnemyTimer = 0;

                            m_Window.Clear(Color.Black);
                            ObjectManager.UpdateObjects();
                            ObjectManager.DrawObjects(m_Window);
                            m_Window.Display();

                            MainMenu.ResetScore();

                            m_State = State.Playing;
                        }
                        break;
                }          
            }
        }
    }
}
