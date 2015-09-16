''' <summary>
''' This is the main type for your game
''' </summary>
Public Class Game1
    Inherits Microsoft.Xna.Framework.Game

    Private WithEvents graphics As GraphicsDeviceManager
    Private WithEvents spriteBatch As SpriteBatch
    Public Weapons As WeaponManager
    Public Projectiles As ProjectileManager
    Public Hostiles As HostileManager
    Public Sub New()
        graphics = New GraphicsDeviceManager(Me)
        graphics.PreferredBackBufferWidth = 1600
        graphics.PreferredBackBufferHeight = 900
        Content.RootDirectory = "Content"
    End Sub

    ''' <summary>
    ''' Allows the game to perform any initialization it needs to before starting to run.
    ''' This is where it can query for any required services and load any non-graphic
    ''' related content.  Calling MyBase.Initialize will enumerate through any components
    ''' and initialize them as well.
    ''' </summary>
    Protected Overrides Sub Initialize()
        ' TODO: Add your initialization logic here
        Weapons = New WeaponManager(Content, GraphicsDevice)
        Projectiles = New ProjectileManager
        Hostiles = New HostileManager()
        LoadTempWeapons()
        LoadTempEnemies()
        Me.IsMouseVisible = True
        MyBase.Initialize()
    End Sub
    Private Sub LoadTempWeapons()
        Weapons.PlayerWeapons.Add(New Cannon(Content))
        Weapons.PlayerWeapons.Add(New Shotgun(Content))
        Weapons.PlayerWeapons.Add(New Missile(Content))
        Weapons.PlayerWeapons.Add(New EnergyBlast(Content))
        Weapons.PlayerWeapons.Add(New Minigun(Content))
    End Sub
    Private Sub LoadTempEnemies()
        Hostiles.AddHostile(Content, GraphicsDevice)
    End Sub
    ''' <summary>
    ''' LoadContent will be called once per game and is the place to load
    ''' all of your content.
    ''' </summary>
    Protected Overrides Sub LoadContent()
        ' Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = New SpriteBatch(GraphicsDevice)

        ' TODO: use Me.Content to load your game content here
    End Sub

    ''' <summary>
    ''' UnloadContent will be called once per game and is the place to unload
    ''' all content.
    ''' </summary>
    Protected Overrides Sub UnloadContent()
        ' TODO: Unload any non ContentManager content here
    End Sub

    ''' <summary>
    ''' Allows the game to run logic such as updating the world,
    ''' checking for collisions, gathering input, and playing audio.
    ''' </summary>
    ''' <param name="gameTime">Provides a snapshot of timing values.</param>
    Protected Overrides Sub Update(ByVal gameTime As GameTime)
        ' Allows the game to exit
        If GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed Then
            Me.Exit()
        End If

        ' TODO: Add your update logic here
        MyBase.Update(gameTime)
    End Sub

    ''' <summary>
    ''' This is called when the game should draw itself.
    ''' </summary>
    ''' <param name="gameTime">Provides a snapshot of timing values.</param>
    Protected Overrides Sub Draw(ByVal gameTime As GameTime)
        GraphicsDevice.Clear(Color.CornflowerBlue)
        Dim ms As MouseState = Mouse.GetState()
        Dim ks As KeyboardState = Keyboard.GetState()
        spriteBatch.Begin()
        Weapons.Update(Me, spriteBatch, gameTime, ms, ks, Projectiles)
        Projectiles.Draw(spriteBatch, gameTime, ms, ks, Hostiles)
        Hostiles.DrawHostiles(spriteBatch, gameTime)
        spriteBatch.End()
        ' TODO: Add your drawing code here
        MyBase.Draw(gameTime)
    End Sub

End Class
