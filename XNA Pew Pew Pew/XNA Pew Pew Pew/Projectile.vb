
Public Class Projectile
    Inherits Entity
    Protected target As Vector2
    Protected slop As Single
    Protected speed As Single
    Protected explodeAtTarget As Boolean

    Protected deathDuration As Single
    Protected detonating As Boolean
    Protected hasDealtDamage As Boolean

    Public Event Detonated(sender As Projectile, hostile As HostileEntity)
    Protected detonationComplete As Boolean 'When fully expended
    Protected chaseMouse As Boolean 'Chases the mouse every update loop.
    Public WeaponRef As WeaponBase
    Public Sub New(animation As Animation, deathAnimation As Animation, deathAnimationDuration As Integer, size As Size, origin As Vector2, target As Vector2, speed As Single, explodeAtTarget As Boolean, WeaponRef As WeaponBase, Optional targetSlop As Single = 4, Optional chaseMouse As Boolean = False)
        MyBase.New(animation, deathAnimation, deathAnimationDuration, New Vector2(WeaponRef.SheetOrigin.X, WeaponRef.SheetOrigin.Y), 0.0F)
        Pos = origin
        Me.explodeAtTarget = explodeAtTarget
        Me.speed = speed
        Me.slop = targetSlop
        Me.anim = animation
        Me.WeaponRef = WeaponRef

        Me.Size = size
        CalculateVel()

        Me.chaseMouse = chaseMouse
        SetTarget(target)
    End Sub
#Region "Deprecated constructor"
    'Public Sub New(asset As Texture2D, explodeTexture As Texture2D, explodeDuration As Single, size As Size, origin As Vector2, target As Vector2, speed As Single, explodeAtTarget As Boolean, WeaponRef As WeaponBase, Optional targetSlop As Single = 4, Optional chaseMouse As Boolean = False)
    '    'Target slop represents how close it can get to the target on the y-axis for it to be considered 'detonated'. 
    '    Pos = origin
    '    Me.explodeAtTarget = explodeAtTarget
    '    Me.speed = speed
    '    Me.slop = targetSlop
    '    Me.tex = asset
    '    Me.deathTex = explodeTexture

    '    Me.deathDuration = explodeDuration
    '    Me.WeaponRef = WeaponRef

    '    Me.Size = size
    '    CalculateVel()
    '    isAnimated = False
    '    Me.chaseMouse = chaseMouse
    '    SetTarget(target)
    'End Sub
#End Region

    Protected Overridable Sub CalculateVel()
        Dim difference As Vector2
        difference = Vector2.Subtract(target, Pos)
        difference.Normalize()
        difference *= speed
        Vel = difference

    End Sub
    Public Sub SetTarget(newTarget As Vector2)
        target = newTarget
        CalculateVel()
        Rotation = CalculateRotation()
    End Sub
    Protected Function CollisionCheck(Hostiles As HostileManager) As Boolean
        Dim result As Boolean
        For Each Hostile In Hostiles.Hostiles
            If GetRectangle.Intersects(Hostile.GetRectangle) And Not hasDealtDamage And Hostile.Alive Then
                hasDealtDamage = True
                RaiseEvent Detonated(Me, Hostile)
                result = True
            End If
        Next
        Return result
    End Function

    Public Overridable Sub Update(sb As SpriteBatch, gt As GameTime, ms As MouseState, Hostiles As HostileManager, Optional adjustColor As Boolean = True)
        If chaseMouse Then
            If Not detonating Then
                SetTarget(New Vector2(ms.X, ms.Y))
            End If
        End If
        Pos += Vel
        If ShouldDetonate(Hostiles) Then
            Vel = Vector2.Zero
            detonating = True

        End If
        If detonating Then
            If deathAnimRemaining > 0 Then
                deathAnim.Update(gt)
                deathAnim.Draw(sb, GetRectangle, New Vector2(WeaponRef.SheetOrigin.X, WeaponRef.SheetOrigin.Y), Rotation, 0, Color.White)
            End If
            deathAnimRemaining -= gt.ElapsedGameTime.Milliseconds
        Else
            anim.Draw(sb, GetRectangle, New Vector2(WeaponRef.SheetOrigin.X, WeaponRef.SheetOrigin.Y), Rotation, 0, Color.White)
        End If


        'Now deprectaed
        'If Not detonating Then
        '    'New Vector2(GetRectangle.Center.X, GetRectangle.Center.Y)
        '    sb.Draw(tex, New Rectangle(CInt(Pos.X), CInt(Pos.Y), Size.Width, Size.Height), WeaponRef.SheetOrigin, Color.White, Rotation, Vector2.Zero, SpriteEffects.None, 0)
        'Else
        '    If currentDeathRemaining > 0 Then
        '        sb.Draw(tex, New Rectangle(CInt(Pos.X), CInt(Pos.Y), Size.Width, Size.Height), WeaponRef.SheetOrigin, Color.White, Rotation, Vector2.Zero, SpriteEffects.None, 0)
        '    Else
        '        detonationComplete = True

        '    End If
        'End If

    End Sub
    ''' <summary>
    ''' Assumes all image artifacts face directly to the right, calculates the proper facing given a target.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CalculateRotation() As Single
        'make the values large to reduce the likelihood of low-point error
        Dim multipliedPos As Vector2 = New Vector2(CSng(Pos.X + (Size.Width / 2)), CSng(Pos.Y + (Size.Height / 2)))
        multipliedPos *= 10000
        Dim multipliedTarget As Vector2 = target
        multipliedTarget *= 10000
        Dim angleInDegrees As Single = CSng(Math.Atan2(multipliedTarget.Y - multipliedPos.Y, multipliedTarget.X - multipliedPos.X))

        Return angleInDegrees + 90.0F
    End Function
    Protected Overridable Function ShouldDetonate(Hostiles As HostileManager) As Boolean
        If Not explodeAtTarget Then

            Return CollisionCheck(Hostiles)
        End If
        If chaseMouse Then
            'Due to the nature of mouse chasing, use absolute distance instead of y-axis. 
            'for example, if the projectile chased the mouse, what's relevant is when it's close to the mouse not how far laterally it's moved.
            If Vector2.Distance(Pos, target) <= slop Then
                Return True
            Else
                Return CollisionCheck(Hostiles)
            End If
        Else
            If Pos.X > target.X Then
                'The projectile has travelled the requisite distance along the screen. Is it within y-slop?
                If MathHelper.Distance(Pos.Y, target.Y) <= slop Then
                    'Target is within Y-Slop. Detonate!

                    Return True
                End If
            Else
                Return CollisionCheck(Hostiles)
            End If
        End If
        Return False
    End Function
End Class

