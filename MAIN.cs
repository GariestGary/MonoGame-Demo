using System;
using System.Diagnostics;
using System.Windows.Forms;
using Comora;
using Demo.Source.Game;
using Demo.Source.Game.Input;
using Demo.Source.Game.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra;
using Myra.Graphics2D.UI;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Demo;

public class MAIN : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player _player;
    private PlayerInput _playerInput;
    private Camera _camera;
    private Desktop _desktop;
    private Slider _fpsSlider;
    private float _lastFPSValue;
    
    
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
        _player = new Player(Vector2.One * 100);
        _playerInput = new PlayerInput();
        _camera = new Camera(_graphics.GraphicsDevice);
        base.Initialize();
    }

    private void UpdateTargetFramerate()
    {
        TargetElapsedTime = TimeSpan.FromSeconds(1d / (int)_fpsSlider.Value);
        Console.WriteLine(_fpsSlider.Value.ToString());
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        MyraEnvironment.Game = this;
        
        var grid = new Grid(){RowSpacing = 8, ColumnSpacing = 8};

        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        
        _fpsSlider = new HorizontalSlider();
        _fpsSlider.Maximum = 165;
        _fpsSlider.Minimum = 15;
        _fpsSlider.Value = 100;
        _lastFPSValue = 100;
        grid.Widgets.Add(_fpsSlider);

        var button = new TextButton() {Text = "Update FrameRate"};
        button.Click += (s, e) => UpdateTargetFramerate();

        _desktop = new Desktop()
        {
            Root = grid
        };
        
        grid.Widgets.Add(button);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        Delta = gameTime.ElapsedGameTime.Milliseconds * 0.001f;
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _playerInput.Update();
        
        var newPos = _player.T.Position;
        
        newPos.X += _playerInput.NormalizedInput.X * Delta * 250f;
        newPos.Y += _playerInput.NormalizedInput.Y * Delta * 250f;
        
        _player.T.Position = newPos;
        
        _player.UpdatePosition();
        _camera.Position = new Vector2(_player.Rect.X, _player.Rect.Y);
        _camera.Update(gameTime);

        // TODO: Add your update logic here

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

        _spriteBatch.End();
        _desktop.Render();
        base.Draw(gameTime);
    }
}