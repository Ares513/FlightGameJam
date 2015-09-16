Public Class Shotgun
    Inherits WeaponBase
    Public Sub New(CM As ContentManager)
        MyBase.New(CM, "Shotgun")
        Weapon = New WeaponDefinition(300, 1200, 28, 7000, 0, WeaponTypes.PROJECTILE, 5)
        ProjectileTexture = CM.Load(Of Texture2D)("cursorBase") 'Each weapon's projectile texture is fixed, load it here
        'cursorbase is a temporary placeholder
        ProjectileDeathTexture = CM.Load(Of Texture2D)("cursorBase")
        WeaponIcon = CM.Load(Of Texture2D)("firewall")
        ProjectileDeathDuration = 500
        detonateAtTarget = False
        SpriteSheetOrigin = New Rectangle(0, 0, ProjectileTexture.Width, ProjectileTexture.Height)
    End Sub
    Public Overrides Sub Fire(g As Game1, sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, projectiles As ProjectileManager, weaps As WeaponManager)
        CurrentMagazine -= 7
        'Shotgun fires a total of 7 shots; 3 above the main target and 3 below.
        Dim targets(6) As Vector2
        Dim c As Vector2 = New Vector2(ms.X, ms.Y)
        'where c represents the 'center' point
        Dim rnd As System.Random = New Random()
        targets(0) = c - New Vector2(20, 50)

        targets(1) = c - New Vector2(40, 100)
        targets(2) = c - New Vector2(60, 150)
        targets(3) = c
        targets(4) = c - New Vector2(20, -50)
        targets(5) = c - New Vector2(40, -100)
        targets(6) = c - New Vector2(60, -150)
        For Each target In targets
            projectiles.AddProjectile(
                New Vector2(0, CSng(g.GraphicsDevice.Viewport.Height * 0.5)), target, Me)

        Next
        CheckReload()
    End Sub

End Class

