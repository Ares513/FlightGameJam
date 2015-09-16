Public Class Minigun
    Inherits WeaponBase
    Public Sub New(CM As ContentManager)
        MyBase.New(CM, "Minigun")
        Dim rnd As System.Random = New System.Random()
        Weapon = New WeaponDefinition(5, 5, 200, 4000, 0, WeaponTypes.PROJECTILE, 10)

        ProjectileTexture = CM.Load(Of Texture2D)("cursorBase") 'Each weapon's projectile texture is fixed, load it here
        'cursorbase is a temporary placeholder
        ProjectileDeathTexture = CM.Load(Of Texture2D)("cursorBase")
        WeaponIcon = CM.Load(Of Texture2D)("forked-lightning")

        ProjectileSize = New Size(16, 16)
        ProjectileDeathDuration = 500
        detonateAtTarget = False
        chaseMouse = False
    End Sub
    Public Overrides Sub Fire(g As Game1, sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, projectiles As ProjectileManager, weaps As WeaponManager)
        CurrentMagazine -= 1
        'The minigun fires quickly and knows no fear.
        projectiles.AddProjectile(New Vector2(0, CSng(g.GraphicsDevice.Viewport.Height * 0.5)), New Vector2(ms.X, ms.Y), Me)


        CheckReload()
    End Sub

End Class