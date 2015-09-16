Public Class ProjectileManager
    Private Projectiles As List(Of Projectile)
    'To protect other programmers from adding projectiles improperly, this variable is declared as Private.
    'Every time a projectile is added, a handler for it should be included. Every time it hits an enemy, it should resolve.
    Public Sub New()
        Projectiles = New List(Of Projectile)
    End Sub
    Public Sub AddProjectile(origin As Vector2, target As Vector2, weapon As WeaponBase)
        If IsNothing(weapon.defaultAnimation) Or IsNothing(weapon.defaultDeathAnim) Then
            Dim defaultAnim As Animation
            Dim defaultAnimDefs(0) As AnimationDefinition
            defaultAnimDefs(0) = New AnimationDefinition(0, 0, "stand", 1000)
            defaultAnim = New Animation(weapon.ProjectileTexture, New Size(weapon.ProjectileTexture.Width, weapon.ProjectileTexture.Height), defaultAnimDefs, 0, New Rectangle(0, 0, weapon.ProjectileTexture.Width, weapon.ProjectileTexture.Height))
            Projectiles.Add(New Projectile(defaultAnim, defaultAnim, 1000, weapon.ProjectileSize, origin, target, weapon.WeaponData.Speed, weapon.detonateAtTarget, weapon, 4.0, weapon.chaseMouse))
        Else
            Projectiles.Add(New Projectile(weapon.defaultAnimation, weapon.defaultDeathAnim, 1000, weapon.ProjectileSize, origin, target, weapon.WeaponData.Speed, weapon.detonateAtTarget, weapon, 4.0, weapon.chaseMouse))

        End If

        'Projectiles.Add(New Projectile(weapon.ProjectileTexture, weapon.ProjectileDeathTexture, 500, weapon.ProjectileSize, origin, target, weapon.WeaponData.Speed, weapon.detonateAtTarget, weapon, weapon.detonateAtTarget))
        AddHandler Projectiles(Projectiles.Count - 1).Detonated, AddressOf CollidedProjectile
    End Sub
    Public Sub Draw(sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, Hostiles As HostileManager)
        For Each proj In Projectiles
            proj.Update(sb, gt, ms, Hostiles)

        Next
        
    End Sub
    Public Sub AddSinusoidalProjectile(origin As Vector2, target As Vector2, weapon As WeaponBase, Optional slop As Single = 6, Optional Amplitude As Single = 5, Optional Period As Single = 2 * MathHelper.Pi)
        Dim defaultAnim As Animation
        Dim defaultAnimDefs(0) As AnimationDefinition
        defaultAnimDefs(0) = New AnimationDefinition(0, 0, "stand", 1000)
        defaultAnim = New Animation(weapon.ProjectileTexture, New Size(weapon.SheetOrigin.Width, weapon.SheetOrigin.Height), defaultAnimDefs, 0, New Rectangle(0, 0, weapon.ProjectileTexture.Width, weapon.ProjectileTexture.Height), New Vector2(weapon.SheetOrigin.X, weapon.SheetOrigin.Y))
        Projectiles.Add(New SinusoidalProjectile(defaultAnim, defaultAnim, 1000, weapon.ProjectileSize, origin, target, weapon.WeaponData.Speed, weapon.detonateAtTarget, weapon, 4.0, False, 100, 25))

        AddHandler Projectiles(Projectiles.Count - 1).Detonated, AddressOf CollidedProjectile
    End Sub
    Protected Sub CollidedProjectile(sender As Projectile, hostile As HostileEntity)
        hostile.CurrentHealth = hostile.CurrentHealth - sender.WeaponRef.WeaponData.Damage
    End Sub
End Class
