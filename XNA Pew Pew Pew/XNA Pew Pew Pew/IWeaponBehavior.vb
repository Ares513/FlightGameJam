Public Interface IWeaponBehavior
    ReadOnly Property WeaponData As WeaponDefinition


    ''' <summary>
    ''' This describes what the weapon actually does. More params will be added as more data becomes available.
    ''' </summary>
    ''' <param name="g"></param>
    ''' <remarks></remarks>
    Sub Fire(g As Game1, sb As SpriteBatch, gt As GameTime, ms As MouseState, ks As KeyboardState, projectiles As ProjectileManager, weaps As WeaponManager)
    Sub Update(gt As GameTime)
    ReadOnly Property CanFire As Boolean

End Interface
