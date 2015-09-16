Public Class HostileEntity
    Inherits Entity

    Protected Health As Single = 5000
    Protected MaximumHealth As Single = 5000
    Protected Speed As Single 'Some enemies may behave differently.
    Protected HealthBar As ProgressBar
    Public ReadOnly Property Alive As Boolean
        Get
            Return CurrentHealth > 0
        End Get
        
    End Property
    Public ReadOnly Property Dead As Boolean
        Get
            Return Not Alive
        End Get
    End Property
    Public Property CurrentHealth As Single
        Get
            Return Health
        End Get
        Set(value As Single)

            Health = MathHelper.Clamp(value, 0, MaximumHealth)
        End Set
    End Property
    Public Property MaxHealth As Single
        Get
            Return MaximumHealth
        End Get
        Set(value As Single)
            MaximumHealth = value
        End Set
    End Property
#Region "Deprecated constructor"
    'Public Sub New(tex As Texture2D, origin As Vector2, Size As Size, progBar As ProgressBar)

    '    Texture = tex
    '    HealthBar = progBar
    '    AdditionalInit(origin, Size)
    'End Sub

#End Region
    Public Sub New(inAnim As Animation, inDeathAnimation As Animation, deathAnimDuration As Integer, sheetOrigin As Vector2, origin As Vector2, Size As Size, progBar As ProgressBar)
        MyBase.New(inAnim, inDeathAnimation, deathAnimDuration, sheetOrigin, 0.0F)
        HealthBar = progBar
        AdditionalInit(origin, Size)
    End Sub
    ''' <summary>
    ''' Sets the health, allowing only values within bounds.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetHealth(value As Single)
        Dim setVal As Single = MathHelper.Clamp(value, 0, MaxHealth)
        CurrentHealth = setVal
    End Sub
    ''' <summary>
    ''' Sets the max health to any value.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetMaxHealth(value As Single)
        MaxHealth = value
    End Sub
    ''' <summary>
    ''' Because the aspects of the two overloaded subs differ in only a handful of aspects, they change those
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AdditionalInit(origin As Vector2, size As Size)
        Pos = origin

        Me.Size = size

    End Sub
    'Advances the entity by its velocity and draws it.
    Public Sub Draw(sb As SpriteBatch, gt As GameTime)
        HealthBar.Position = Me.Pos - New Vector2(0, HealthBar.Size.Height)
        If Alive Then
        


                HealthBar.MaxValue = CInt(MaxHealth)
                HealthBar.MinValue = 0
                HealthBar.SetValue(CInt(CurrentHealth))

                HealthBar.Draw(sb, True, Nothing)

            anim.Draw(sb, GetRectangle, sheetOrigin, Rotation, 0, Color.White)
            Pos += Vel
        Else
            If deathAnimRemaining > 0 Then
                deathAnimRemaining -= gt.ElapsedGameTime.Milliseconds
                deathAnim.Draw(sb, GetRectangle, sheetOrigin, Rotation, 0, Color.White)
            End If
        End If
    End Sub

End Class
