using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Comora;
using Demo.Source.Core;
using Demo.Source.Core.World;
using Demo.Source.Debug;
using Demo.Source.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Demo;

public class MAIN : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player _player;
    private SmoothCamera _camera;
    private DebugUIDrawer _debugUi;

    private TmxMap _map;
    private Texture2D _tileset;
    private int _tileWidth;
    private int _tileHeight;
    private int _tilesetTilesWide;
    private int _tilesetTilesHigh;
    private List<Rectangle> _collisions = new();

    public float Delta { get; private set; }

    public MAIN()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
        _graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 100d);
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _debugUi = new DebugUIDrawer(this);
        
        _player = new Player(Vector2.One * 100);

        _camera = new SmoothCamera(_graphics.GraphicsDevice, _player.T, 0.3f);
        
        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _map = new TmxMap("Content/demo_map.tmx");
        _tileset = Content.Load<Texture2D>(_map.Tilesets[0].Name);

        _tileWidth = _map.Tilesets[0].TileWidth;
        _tileHeight = _map.Tilesets[0].TileHeight;

        _tilesetTilesWide = _tileset.Width / _tileWidth;
        _tilesetTilesHigh = _tileset.Height / _tileHeight;
        
        foreach(var o in _map.Layers["Obstacles"].Tiles)
            Physics.AddObstacle(new Rectangle((int)(o.X * _tileWidth), (int)(o.Y * _tileHeight), (int)_tileWidth, (int)_tileHeight));
        
        _debugUi.Initialize();
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        Delta = gameTime.ElapsedGameTime.Milliseconds * 0.001f;
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _player.Update(Delta);
        _camera.UpdateSmoothedPosition(Delta);
        _camera.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        //Drawing circle
        _spriteBatch.Begin(_camera);
        var playerRect = new Rectangle(_player.Rect.X - (int) (_player.Rect.Width * 0.5f),
            _player.Rect.Y - (int) (_player.Rect.Height * 0.5f), _player.Rect.Width, _player.Rect.Width);
        _spriteBatch.Draw(Content.Load<Texture2D>("square"), new Rectangle(50, 50, 68, 34), null, Color.Red);
        _spriteBatch.Draw(Content.Load<Texture2D>("square"), new Rectangle(248, 300, 56, 76), null, Color.Green);
        _spriteBatch.Draw(Content.Load<Texture2D>("square"), playerRect, null, new Color(0.1f, 024f, 0.5f, 0.7f));

        for (var i = 0; i < _map.Layers[0].Tiles.Count; i++) 
        {
            int gid = _map.Layers[0].Tiles[i].Gid;
            
            if(gid != 0) 
            {
                int tileFrame = gid - 1;
                int column = tileFrame % _tilesetTilesWide;
                int row = (int)Math.Floor((double)tileFrame / (double)_tilesetTilesWide);

                float x = (i % _map.Width) * _map.TileWidth;
                float y = (float)Math.Floor(i / (double)_map.Width) * _map.TileHeight;

                Rectangle tilesetRec = new Rectangle(_tileWidth * column, _tileHeight * row, _tileWidth, _tileHeight);

                _spriteBatch.Draw(_tileset, new Rectangle((int)x, (int)y, _tileWidth, _tileHeight), tilesetRec, Color.White);
            }
        }
        
        _spriteBatch.End();
        _debugUi.Draw();
        
        base.Draw(gameTime);
    }
}