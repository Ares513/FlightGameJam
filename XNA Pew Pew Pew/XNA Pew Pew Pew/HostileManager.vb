Public Class HostileManager

    Public Hostiles As List(Of HostileEntity)
    Public Sub New()
        Hostiles = New List(Of HostileEntity)
    End Sub
    Public Sub DrawHostiles(sb As SpriteBatch, gt As GameTime)
        For Each hostile In Hostiles
            hostile.Draw(sb, gt)
        Next
    End Sub
    Public Sub AddHostile(CM As ContentManager, gd As GraphicsDevice)
        Dim progBar As ProgressBar = New ProgressBar(CM.Load(Of Texture2D)("progressBar"), CM.Load(Of Texture2D)("progressBarFull"), New Size(128, 32), New Vector2(400, 500 - 32))
        'temporary default animation loading
        Dim defaultAnim As Animation
        Dim defaultAnimDefs(0) As AnimationDefinition
        defaultAnimDefs(0) = New AnimationDefinition(1, 1, "stand", 100)
        defaultAnim = New Animation(CM.Load(Of Texture2D)("orbfull"), New Size(128, 128), defaultAnimDefs, 0, New Rectangle(0, 0, 128, 128))
        Hostiles.Add(New HostileEntity(defaultAnim, defaultAnim, 1000, Vector2.Zero, New Vector2(gd.Viewport.Bounds.Right, CSng(gd.Viewport.Bounds.Bottom / 2)), New Size(128, 128), progBar))
        
        Hostiles(Hostiles.Count - 1).Vel = New Vector2(-1, 0)
    End Sub
End Class
