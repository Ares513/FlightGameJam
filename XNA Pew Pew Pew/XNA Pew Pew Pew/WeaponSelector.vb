''' <summary>
''' UI class that shows which weapon is currently selected.
''' </summary>
''' <remarks></remarks>
Public Class WeaponSelector
    Dim baseTexture As Texture2D
    Dim highlightedTexture As Texture2D
    Dim reloadingTexture As Texture2D
    Dim missing_texture As Texture2D
    Public Sub New(CM As ContentManager)
        baseTexture = CM.Load(Of Texture2D)("progressBar")
        highlightedTexture = CM.Load(Of Texture2D)("SpellBoxEmpty")
        reloadingTexture = CM.Load(Of Texture2D)("LockedSkill")
        missing_texture = CM.Load(Of Texture2D)("cursorBase")
    End Sub
    Public Sub Draw(sb As SpriteBatch, position As Vector2, iconSize As Size, selected As Integer, weaps As List(Of WeaponBase))
        'base texture
        sb.Draw(baseTexture, New Rectangle(CInt(position.X), CInt(position.Y), iconSize.width * weaps.Count, iconSize.height), Color.White)
        For i = 0 To weaps.Count - 1 Step 1
            If IsNothing(weaps(i).WeaponIcon) Then
                sb.Draw(missing_texture, New Rectangle(CInt(position.X + i * iconSize.width), CInt(position.Y), iconSize.width, iconSize.height), Color.White)

            Else
                sb.Draw(weaps(i).WeaponIcon, New Rectangle(CInt(position.X + i * iconSize.width), CInt(position.Y), iconSize.width, iconSize.height), Color.White)

            End If
            If weaps(i).Reloading Then
                sb.Draw(reloadingTexture, New Rectangle(CInt(position.X + i * iconSize.width), CInt(position.Y), iconSize.width, iconSize.height), Color.White)

            End If
            If i = selected Then
                sb.Draw(highlightedTexture, New Rectangle(CInt(position.X + i * iconSize.width), CInt(position.Y), iconSize.width, iconSize.height), Color.White)

            End If
        Next
    End Sub
End Class
