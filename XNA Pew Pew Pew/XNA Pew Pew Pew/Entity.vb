Public MustInherit Class Entity
    Inherits SimpleEntity
    Public anim As Animation
    Public deathAnim As Animation
    Public deathAnimDuration As Integer 'the time, in ms, that a dead entity remains on-screen.
    Public deathAnimRemaining As Integer
    Public sheetOrigin As Vector2
    Public Rotation As Single

    Public Sub New(anim As Animation, deathAnim As Animation, deathAnimDuration As Integer, sheetOrigin As Vector2, Rotation As Single)
        deathAnimRemaining = deathAnimDuration
        Me.anim = anim
        Me.deathAnim = deathAnim
        Me.sheetOrigin = sheetOrigin
        Me.Rotation = Rotation

    End Sub
End Class
