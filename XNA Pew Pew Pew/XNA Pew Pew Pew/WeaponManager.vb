Public Class WeaponManager
    Public PlayerWeapons As List(Of WeaponBase)
    Private currentWeapon As Integer = 0
    Private Selector As WeaponSelector 'UI element only. Key events are handled in this class.
    Public Property CurrentWeaponIndex As Integer
        Get
            Return currentWeapon
        End Get
        Set(value As Integer)
            currentWeapon = CInt(MathHelper.Clamp(value, 0, PlayerWeapons.Count - 1))
        End Set
    End Property
    Private AmmoBar As ProgressBar
    Public Sub New(CM As ContentManager, gd As GraphicsDevice)
        PlayerWeapons = New List(Of WeaponBase)
        Dim emptyTexture As Texture2D = CM.Load(Of Texture2D)("progressBar")

        AmmoBar = New ProgressBar(emptyTexture, CM.Load(Of Texture2D)("progressBarFull"), New Size(CInt(gd.Viewport.Width * 0.3), CInt(gd.Viewport.Height * 0.1)), New Vector2(0, 0))
        AmmoBar.Font = CM.Load(Of SpriteFont)("DefaultFont")
        AmmoBar.WriteCurrentValueAsTitle = True
        AmmoBar.ShowTitle = True
        AmmoBar.Title = "Reloading!"
        Selector = New WeaponSelector(CM)
    End Sub
    Private Sub DrawWeaponAmmoBar(sb As SpriteBatch, ms As MouseState)
        AmmoBar.Draw(sb, True, ms)
    End Sub
    Public Sub Update(g As Game1, sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, pm As ProjectileManager)
        If PlayerWeapons.Count = 0 Then
            Exit Sub
        End If
        SelectWeapon(ks)
        AmmoBar.MaxValue = PlayerWeapons(CurrentWeaponIndex).WeaponData.Magazine
        AmmoBar.SetValue(PlayerWeapons(CurrentWeaponIndex).MagazineRemaining)

        If Not PlayerWeapons(CurrentWeaponIndex).Reloading Then
            AmmoBar.WriteCurrentValueAsTitle = True

        Else
            AmmoBar.Title = "Reloading! " + CStr((PlayerWeapons(CurrentWeaponIndex).ReloadRemaining))
            AmmoBar.WriteCurrentValueAsTitle = False
        End If
        'Write the value when not reloading
        DrawWeaponAmmoBar(sb, ms)
        DrawWeaponSelector(sb)
        If ms.LeftButton = ButtonState.Pressed Then
            If PlayerWeapons(CurrentWeaponIndex).CanFire Then
                PlayerWeapons(CurrentWeaponIndex).Fire(g, sb, gt, ms, ks, pm, Me)
            End If
        End If
        For Each weap In PlayerWeapons
            weap.Update(gt)
        Next

    End Sub
    Private Sub SelectWeapon(ks As KeyboardState)
        Dim pressed As Keys() = ks.GetPressedKeys()
        Dim result As Integer
        If pressed.Count > 0 Then

            For Each key In pressed
                Try
                    Dim name As String = [Enum].GetName(GetType(Keys), key).Substring(1)

                    If IsNumeric(name) Then
                        Dim index As Integer = CInt(name)
                        If index = 0 Then
                            index = 1
                            'otherwise, we'd have index -1
                        End If
                        result = index - 1
                    End If
                Catch ex As Exception
                End Try
            Next
            If result <= PlayerWeapons.Count - 1 And result >= 0 Then
                'bounds check
                currentWeapon = result
            End If
        End If

    End Sub
    Private Sub DrawWeaponSelector(sb As SpriteBatch)
        Dim ammoBarRect As Rectangle = New Rectangle(CInt(AmmoBar.Position.X), CInt(AmmoBar.Position.Y), AmmoBar.Size.width, AmmoBar.Size.height)
        Dim individualSize As Size = New Size(CInt(ammoBarRect.Width / PlayerWeapons.Count), CInt(ammoBarRect.Height))
        Selector.Draw(sb, New Vector2(ammoBarRect.Left, ammoBarRect.Bottom), New Size(CInt(ammoBarRect.Width / PlayerWeapons.Count), CInt(ammoBarRect.Height)), currentWeapon, PlayerWeapons)

    End Sub
End Class
