Public Class Missile
    Inherits WeaponBase
    Public Sub New(CM As ContentManager)
        MyBase.New(CM, "Missile")
        Weapon = New WeaponDefinition(1000, 2000, 3, 10000, 0, WeaponTypes.PROJECTILE, 5)
        ProjectileTexture = CM.Load(Of Texture2D)("cursorBase") 'Each weapon's projectile texture is fixed, load it here
        'cursorbase is a temporary placeholder
        ProjectileDeathTexture = CM.Load(Of Texture2D)("cursorBase")
        WeaponIcon = CM.Load(Of Texture2D)("flaming-mantle")
        ProjectileDeathDuration = 500
        detonateAtTarget = True
        chaseMouse = True
    End Sub
    Public Overrides Sub Fire(g As Game1, sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, projectiles As ProjectileManager, weaps As WeaponManager)
        CurrentMagazine -= 1
        'Shotgun fires a total of 7 shots; 3 above the main target and 3 below.
 
        projectiles.AddProjectile(New Vector2(0, CSng(g.GraphicsDevice.Viewport.Height * 0.5)), New Vector2(ms.X, ms.Y), Me)


        CheckReload()
    End Sub

End Class

