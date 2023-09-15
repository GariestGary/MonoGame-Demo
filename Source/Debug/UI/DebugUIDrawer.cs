namespace Demo.Source.Debug;

using System;
using Myra;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;

public class DebugUIDrawer
{
    private Game _game;
    private Slider _fpsSlider;
    private Desktop _desktop;
    
    public DebugUIDrawer(Game game)
    {
        _game = game;
        MyraEnvironment.Game = game;
    }

    public void Initialize()
    {
        var grid = new Grid(){RowSpacing = 8, ColumnSpacing = 8};

        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        
        _fpsSlider = new HorizontalSlider();
        _fpsSlider.Maximum = 165;
        _fpsSlider.Minimum = 15;
        _fpsSlider.Value = 100;
        _fpsSlider.GridColumn = 2;
        _fpsSlider.ValueChanged += (s, e) => Console.WriteLine(e.NewValue.ToString());
        grid.Widgets.Add(_fpsSlider);

        //Apply 
        var button = new TextButton() {Text = "Update FrameRate"};
        button.Click += (s, e) => UpdateTargetFramerate();

        _desktop = new Desktop()
        {
            Root = grid
        };

        button.GridColumn = 1;
        
        grid.Widgets.Add(button);
        
        
    }
    
    private void UpdateTargetFramerate()
    {
        _game.TargetElapsedTime = TimeSpan.FromSeconds(1d / (int)_fpsSlider.Value);
        Console.WriteLine(_fpsSlider.Value.ToString());
    }

    public void Draw()
    {
        _desktop.Render();
    }
}