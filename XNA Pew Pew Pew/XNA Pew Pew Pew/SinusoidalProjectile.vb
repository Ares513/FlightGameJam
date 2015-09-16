Public Class SinusoidalProjectile
    Inherits Projectile
    Protected duration As Single
    Protected amp As Single
    Protected period As Single
    Private initialTarget As Vector2
    Public Sub New(animation As Animation, deathAnimation As Animation, deathAnimationDuration As Integer, size As Size, origin As Vector2, target As Vector2, speed As Single, explodeAtTarget As Boolean, WeaponRef As WeaponBase, Optional targetSlop As Single = 4, Optional chaseMouse As Boolean = False, Optional Amplitude As Single = 5, Optional Period As Single = 2 * MathHelper.Pi)
        MyBase.New(animation, deathAnimation, deathAnimationDuration, size, origin, target, speed, explodeAtTarget, WeaponRef, targetSlop, chaseMouse)
        initialTarget = target
        amp = Amplitude
        Me.period = Period

    End Sub
    Protected Overrides Sub CalculateVel()
        Dim difference As Vector2
        difference = Vector2.Subtract(target, Pos)
        difference.Normalize()
        difference *= New Vector2(speed, amp)
        Vel = difference

    End Sub
    Public Overrides Sub Update(sb As SpriteBatch, gt As GameTime, ms As MouseState, Hostiles As HostileManager, Optional adjustColor As Boolean = True)
        duration += 1
        If Not ShouldDetonate(Hostiles) Then
            Dim newTarget As Vector2

            Dim adjustedValue As Single = CSng(amp * Math.Sin(duration / period))
            newTarget = New Vector2(initialTarget.X, initialTarget.Y + adjustedValue)

            SetTarget(newTarget)
        Else

        End If
        MyBase.Update(sb, gt, ms, Hostiles)
    End Sub
    Protected Overrides Function ShouldDetonate(Hostiles As HostileManager) As Boolean
        If Not CollisionCheck(Hostiles) Then
            Return Pos.X >= initialTarget.X - slop

        Else
            Return True
        End If
        
    End Function
End Class
