Public Class WeaponBase
    Implements IWeaponBehavior
    Protected Weapon As WeaponDefinition
    Protected FireTimeRemaining As Single
    Protected ReloadTimeRemaining As Single
    Protected CurrentMagazine As Integer
    Protected SpriteSheetOrigin As Rectangle = Nothing 'what place in the texture to draw
    Protected displayName As String = "Default Weapon"
    Public ProjectileTexture As Texture2D
    Public ProjectileDeathTexture As Texture2D
    Public WeaponIcon As Texture2D
    Public ProjectileDeathDuration As Single 'OPTIONAL; DEPENDENT ON DEFINITION
    Public detonateAtTarget As Boolean 'Whether or not the projectile explodes when it reaches its target.
    Public chaseMouse As Boolean 'Causes the projectile to follow the mouse.
    'Subclasses can determine how they use everything about this. These variables are not null-safe.
    Public defaultAnimation As Animation
    Public defaultDeathAnim As Animation

    Public ProjectileSize As Size
    Public ReadOnly Property Name As String
        Get
            Return displayName
        End Get
    End Property

    Public Sub New(CM As ContentManager, name As String)
        ProjectileTexture = CM.Load(Of Texture2D)("cursorBase")
        ProjectileSize = New Size(32, 32)
        SpriteSheetOrigin = New Rectangle(0, 0, ProjectileTexture.Width, ProjectileTexture.Height)
        displayName = name
    End Sub
    Public ReadOnly Property SheetOrigin As Rectangle
        Get
            Return SpriteSheetOrigin
        End Get
    End Property
    Public ReadOnly Property ReloadRemaining As Single
        Get
            Return ReloadTimeRemaining
        End Get
    End Property
    Public ReadOnly Property MagazineRemaining As Integer
        Get
            Return CurrentMagazine
        End Get
    End Property
    Public ReadOnly Property Reloading As Boolean
        Get
            Return CurrentMagazine <= 0
        End Get
    End Property
    Public Overridable ReadOnly Property CanFire As Boolean Implements IWeaponBehavior.CanFire
        Get
            If Not Reloading Then
                If FireTimeRemaining <= 0 Then
                    Return True
                End If
            End If
            Return False
        End Get
    End Property

 
    ''' <summary>
    ''' Fire occurs under the assumption that firing is allowed.
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="sb"></param>
    ''' <param name="gt"></param>
    ''' <remarks></remarks>
    Public Overridable Sub Fire(g As Game1, sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, projectiles As ProjectileManager, weaps As WeaponManager) Implements IWeaponBehavior.Fire
        CurrentMagazine -= 1
        CheckReload()
    End Sub
    ''' <summary>
    ''' CheckReload should be called at the end of every fire sequence.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CheckReload()
        If Reloading Then
            CurrentMagazine = 0
            ReloadTimeRemaining = Weapon.Reload
        End If
        FireTimeRemaining = WeaponData.AttackSpeed
    End Sub
    Public Sub Update(gt As GameTime) Implements IWeaponBehavior.Update
        FireTimeRemaining -= gt.ElapsedGameTime.Milliseconds
        If Reloading Then
            ReloadTimeRemaining -= gt.ElapsedGameTime.Milliseconds

        End If
        If ReloadTimeRemaining <= 0 And Reloading Then
            CurrentMagazine = Weapon.Magazine
        End If
    End Sub

    Public ReadOnly Property WeaponData As WeaponDefinition Implements IWeaponBehavior.WeaponData
        Get
            Return Weapon
        End Get
    End Property
End Class
