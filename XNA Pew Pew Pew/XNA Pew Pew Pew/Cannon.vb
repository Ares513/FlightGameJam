Public Class Cannon
    Inherits WeaponBase
    Public Sub New(CM As ContentManager)
        MyBase.New(CM, "Cannon")
        Weapon = New WeaponDefinition(100, 400, 20, 2000, 0, WeaponTypes.PROJECTILE, 5)
        ProjectileTexture = CM.Load(Of Texture2D)("cursorBase") 'Each weapon's projectile texture is fixed, load it here
        'cursorbase is a temporary placeholder
        ProjectileDeathTexture = CM.Load(Of Texture2D)("cursorBase")
        WeaponIcon = CM.Load(Of Texture2D)("fireball_2")
        ProjectileDeathDuration = 5000
        detonateAtTarget = True
        Dim defaultAnimDefs(0) As AnimationDefinition
        defaultAnimDefs(0) = New AnimationDefinition(1, 1, "stand", 100)
        defaultAnimation = New Animation(ProjectileTexture, New Size(ProjectileTexture.Width, ProjectileTexture.Height), defaultAnimDefs, 0, Nothing, Vector2.Zero)

    End Sub
    Public Overrides Sub Fire(g As Game1, sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, projectiles As ProjectileManager, weaps As WeaponManager)
        CurrentMagazine -= 1
        projectiles.AddProjectile(New Vector2(0, CSng(g.GraphicsDevice.Viewport.Height * 0.5)), New Vector2(ms.X, ms.Y), Me)
        CheckReload()
    End Sub

End Class

