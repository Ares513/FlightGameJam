Public Class HostileData
    Public Anim As Animation
    Public DeathAnim As Animation
    Public deathAnimDuration As Integer
    Public sheetOrigin As Vector2
    Public origin As Vector2
    Public Size As Size
    Public progBar As ProgressBar

    Public Sub New(inAnim As Animation, inDeathAnimation As Animation, deathAnimDuration As Integer, sheetOrigin As Vector2, origin As Vector2, Size As Size, progBar As ProgressBar)

    End Sub
End Class
Dim progBar As ProgressBar = New ProgressBar(CM.Load(Of Texture2D)("progressBar"), CM.Load(Of Texture2D)("progressBarFull"), New Size(128, 32), New Vector2(400, 500 - 32))
'temporary default animation loading
Dim defaultAnim As Animation
Dim defaultAnimDefs(0) As AnimationDefinition
        defaultAnimDefs(0) = New AnimationDefinition(1, 1, "stand", 100)
        defaultAnim = New Animation(CM.Load(Of Texture2D)("orbfull"), New Size(128, 128), defaultAnimDefs, 0, New Rectangle(0, 0, 128, 128))
        Hostiles.Add(New HostileEntity(defaultAnim, defaultAnim, 1000, Vector2.Zero, New Vector2(gd.Viewport.Bounds.Right, CSng(gd.Viewport.Bounds.Bottom / 2)), New Size(128, 128), progBar))

        Hostiles(Hostiles.Count - 1).Vel = New Vector2(-1, 0)