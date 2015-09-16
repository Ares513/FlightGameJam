Public Class EnergyBlast
    Inherits WeaponBase
    Public Sub New(CM As ContentManager)
        MyBase.New(CM, "Energy Blast")
        Weapon = New WeaponDefinition(30, 50, 30, 1000, 0, WeaponTypes.PROJECTILE, 5)
        ProjectileTexture = CM.Load(Of Texture2D)("bullets") 'Each weapon's projectile texture is fixed, load it here
        SpriteSheetOrigin = New Rectangle(10, 186, 12, 12)
        'cursorbase is a temporary placeholder
        ProjectileDeathTexture = CM.Load(Of Texture2D)("cursorBase")
        WeaponIcon = CM.Load(Of Texture2D)("fireburst")
        ProjectileDeathDuration = 500
        detonateAtTarget = True
        chaseMouse = True
        ProjectileSize = New Size(32, 32)
    End Sub
    Public Overrides Sub Fire(g As Game1, sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, projectiles As ProjectileManager, weaps As WeaponManager)
        CurrentMagazine -= 1

        projectiles.AddSinusoidalProjectile(New Vector2(0, CSng(g.GraphicsDevice.Viewport.Height * 0.5)), New Vector2(ms.X, ms.Y), Me, 30, 200, 25)


        CheckReload()
    End Sub

End Class

