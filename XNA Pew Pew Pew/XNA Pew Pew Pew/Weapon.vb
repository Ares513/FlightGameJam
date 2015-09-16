Public Class WeaponDefinition


    Public Sub New(dmg As Integer, fireRate As Single, magazineSize As Integer, reloadTime As Single, armorPenetration As Single, weaponType As WeaponTypes, speed As Single)
        Me.dmg = dmg
        Me.fireRate = fireRate
        Me.magazineSize = magazineSize
        Me.reloadTime = reloadTime
        Me.armorPenetration = armorPenetration
        Me.weaponType = weaponType
        projectileSpeed = speed
    End Sub
    ''' <summary>
    ''' Allows only the weapon subclass to set the speed of the projectile after instantiation.
    ''' </summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpeed(value As Single)

    End Sub
    Dim projectileSpeed As Single
    ''' <summary>
    ''' Represents how fast the projectile travels along the screen.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Speed As Single
        Get
            Return projectileSpeed
        End Get

    End Property
    Dim weaponType As WeaponTypes
    ''' <summary>
    ''' Represents the weapon type. Once set it shouldn't be adjusted. It determines how the weapon behaves in combat, and though the WeaponBehavior interface handles what actually happens, this is a good marker.
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Type As WeaponTypes
        Get
            Return weaponType
        End Get
    End Property
    Dim dmg As Single
    ''' <summary>
    ''' The amount of damage, before armor processing that the weapon does. the 'base' damage if you will.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Damage As Single
        Get
            Return dmg
        End Get
        Set(value As Single)
            dmg = value
        End Set
    End Property

    Dim fireRate As Single
    ''' <summary>
    ''' The delay, in milliseconds in between each attack.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AttackSpeed As Single
        Get
            Return fireRate
        End Get
        Set(value As Single)
            fireRate = value
        End Set
    End Property
    ''' <summary>
    ''' The size of the magazine.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Magazine As Integer
        Get
            Return magazineSize
        End Get
        Set(value As Integer)
            magazineSize = value
        End Set
    End Property
    Dim magazineSize As Integer
    ''' <summary>
    ''' The time it takes to reload, in milliseconds.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Reload As Single
        Get
            Return reloadTime
        End Get
        Set(value As Single)
            reloadTime = value
        End Set
    End Property

    Dim reloadTime As Single
    ''' <summary>
    ''' Reflects the different types this weapon is or isn't effective against. If null, then this weapon does full damage to all enemies.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DamageModifiers As List(Of DamageAdjustment)
        Get
            Return adjustments
        End Get
        Set(value As List(Of DamageAdjustment))
            adjustments = value
        End Set
    End Property
    Dim adjustments As List(Of DamageAdjustment)
    ''' <summary>
    ''' Reflects the amount of armor this weapon will ignore each time it hits a target. Armor penetration does not increase damage, merely ignores some portion of the armor.
    ''' That is to say, a target with 5 armor, a weapon with 2 AP and 20 damage would do 17 damage; the target behaves as if it has 3 armor instead of 5. 
    ''' Armor penetration cannot reduce the armor below zero.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property AP As Single
        Get
            Return armorPenetration
        End Get
        Set(value As Single)
            armorPenetration = value
        End Set
    End Property
    Dim armorPenetration As Single

End Class
Public Enum WeaponTypes As Integer
    PROJECTILE = 0
    BEAM = 1
    BURST = 2
    NO_TARGET_PROJECTILE = 3
    SWEEPING_BEAM = 4
    SCREEN_NUKE = 5
End Enum
''' <summary>
''' Represents a single damage adjustment for a single weapon. If the Percentage value isn't zero, it is proceessed first followed by the 'amount' or flat amount.
''' </summary>
''' <remarks></remarks>
Public Class DamageAdjustment
    Public targetType As String
    Public percentage As Single
    Public amount As Single
    Public Function Process(base As Single, targetTypes As String) As Single
        If targetTypes.ToLower.Contains(targetType) Then
            'Percentage first, assuming that the percentage is a value
            If percentage > 0 Then
                If percentage < 1 Then
                    Return base * percentage
                End If
            Else
                If base + amount <= 0 Then
                    Return 1
                End If
                Return base + amount
            End If
        End If
        Return base
    End Function
End Class
''' <summary>
''' Manages processing of how different types of enemies take damage.
''' </summary>
''' <remarks></remarks>
Public Class DamageCalculator


End Class